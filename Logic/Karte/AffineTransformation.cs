using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Logic.Karte
{
    public class AffineTransformation : IAffineTransformation
    {
        public double[][] M; // the matrix constructed by affineLsFit
        public double[][] Minv; // the inverse matrix constructed by affineLsFit
        public int dim; // the dimension of the points that the affine matrix can transform

        private double[] ArrayFromPoint(Point p)
        {
            return new double[] { p.X, p.Y };
        }

        private Point PointFromArray(double[] pts)
        {
            return new Point(pts[0], pts[1]);
        }

        public bool Fit(Point[] from_pts, Point[] to_pts)
        {
            return affineLsFit(from_pts.Select(p => ArrayFromPoint(p)).ToArray(), to_pts.Select(p => ArrayFromPoint(p)).ToArray());
        }
        
        public bool affineLsFit(double[][] from_pts, double[][] to_pts)
        {
            /*Fit an affine transformation to given point sets.
            More precisely: solve (least squares fit) matrix 'A'and 't' from
            'p ~= A*q+t', given vectors 'p' and 'q'.
            Works with arbitrary dimensional vectors (2d, 3d, 4d...).

            Adapted in 2014 by Bill Chadwick from the Python code here http://elonen.iki.fi/code/misc...
            which was written by Jarno Elonen <elonen@iki.fi> in 2007 and placed in the Public Domain.
            
            Extended in 2015 with an inverse transformation by Jonas Tampier.

            Based on paper "Fitting affine and orthogonal transformations
            between two sets of points, by Helmuth Späth (2003).*/

            if ((from_pts.Length != to_pts.Length) || (from_pts.Length < 1))
                // from_pts and to_pts must be of same size
                return false;

            dim = from_pts[0].Length; // # num of dimensions, assumed the same for all points
            if (from_pts.Length < dim)
                // Too few points => under-determined system.
                return false;

            double[][] q = new double[from_pts.Length][];
            double[][] p = new double[to_pts.Length][];

            // copy 'from' array and append a set of 1s
            for (int i = 0; i < from_pts.Length; i++)
            {
                q[i] = new double[dim + 1];
                for (int j = 0; j < dim; j++)
                    q[i][j] = from_pts[i][j];
                q[i][dim] = 1.0;
            }

            // copt 'to' array
            for (int i = 0; i < to_pts.Length; i++)
            {
                p[i] = new double[dim];
                for (int j = 0; j < dim; j++)
                    p[i][j] = to_pts[i][j];
            }

            // Make a (dim) x (dim+1) matrix and fill it
            double[,] c = new double[dim + 1, dim];
            for (int j = 0; j < dim; j++)
                for (int k = 0; k < dim + 1; k++)
                    for (int i = 0; i < q.Length; i++)
                        c[k, j] += q[i][k] * p[i][j];

            // Make a (dim+1) x (dim+1) matrix and fill it
            double[,] Q = new double[dim + 1, dim + 1];
            for (int qi = 0; qi < q.Length; qi++)
                for (int i = 0; i < (dim + 1); i++)
                    for (int j = 0; j < (dim + 1); j++)
                        Q[i, j] += q[qi][i] * q[qi][j];

            // Augment Q with c and identity and solve Q * a' = c by Gauss-Jordan
            M = new double[dim + 1][];
            for (int m = 0; m < M.Length; m++)
            {
                M[m] = new double[dim + 1 + dim];
                for (int i = 0; i < dim + 1; i++)
                    M[m][i] = Q[m, i];
                for (int i = 0; i < dim; i++) // augment with c
                    M[m][i + dim + 1] = c[m, i];
            }
            if (!gauss_jordan(M))
                // Error: singular matrix. Points are probably coplanar
                return false;

            //calculate the inverse matrix, too
            Minv = new double[dim + 1][];
            for (int m = 0; m < Minv.Length; m++)
            {
                Minv[m] = new double[dim + 1 + dim + 1];
                for (int i = 0; i < dim; i++)
                    Minv[m][i] = M[m][dim + 1 + i];
                Minv[m][dim + 1 + m] = 1;
            }
            Minv[dim][dim] = 1;
            if (!gauss_jordan(Minv))
                // Error: singular matrix.
                return false;


            return true;
        }

        // Ultra simple linear system solver. Replace this if you need speed.
        private bool gauss_jordan(double[][] m, double eps = 1.0/ 10000000000)
        {
            /*Puts given matrix (2D array) into the Reduced Row Echelon Form.
            Returns True if successful, False if 'm' is singular.
            NOTE: make sure all the matrix items support fractions! Int matrix will NOT work!
            Written by Jarno Elonen in April 2005, released into Public Domain */

            int h = m.Length;
            int w = m[0].Length;

            for (int y = 0; y < h; y++)
            {
                int maxrow = y;

                for (int y2 = y + 1; y2 < h; y2++) // Find max pivot
                    if (Math.Abs(m[y2][y]) > Math.Abs(m[maxrow][y]))
                        maxrow = y2;

                double[] temp = m[y];
                m[y] = m[maxrow];
                m[maxrow] = temp;

                if (Math.Abs(m[y][y]) <= eps) // Singular?
                    return false;

                for (int y2 = y + 1; y2 < h; y2++) // Eliminate column y
                {
                    double c = m[y2][y] / m[y][y];
                    for (int x = y; x < w; x++)
                        m[y2][x] -= m[y][x] * c;
                }
            }

            for (int y = h - 1; y > -1; y--) // Backsubstitute
            {
                double c = m[y][y];
                for (int y2 = 0; y2 < y; y2++)
                {
                    for (int x = w - 1; x > y - 1; x--)
                    {
                        m[y2][x] -= m[y][x] * m[y2][y] / c;
                    }
                }
                m[y][y] /= c;

                for (int x = h; x < w; x++) // Normalize row y
                    m[y][x] /= c;
            }

            return true;
        }

        public override string ToString()
        {
            string res = "";
            for (int j = 0; j < dim; j++)
            {
                string str = string.Format("x{0:D}' = ", j);
                for (int i = 0; i < dim; i++)
                    str += string.Format("x{0:D} * {1:F6} + ", i, M[i][j + dim + 1]);
                str += string.Format("{0:F6}", M[dim][j + dim + 1]);
                res += str + "\n";
            }
            return res;
        }

        public double[] transform(double[] pts)
        {
            double[] res = new double[pts.Length];
            for (int j = 0; j < dim; j++)
            {
                for (int i = 0; i < dim; i++)
                    res[j] += pts[i] * M[i][j + dim + 1];
                res[j] += M[dim][j + dim + 1];
            }
            return res;
        }

        public double[] transformback(double[] pts)
        {
            double[] res = new double[pts.Length];
            for (int j = 0; j < dim; j++)
            {
                for (int i = 0; i < dim; i++)
                    res[j] += pts[i] * Minv[i][j + dim + 1];
                res[j] += Minv[dim][j + dim + 1];
            }
            return res;
        }

        public Point transform(Point pts)
        {
            return PointFromArray(transform(ArrayFromPoint(pts)));
        }

        public Point transformback(Point pts)
        {
            return PointFromArray(transformback(ArrayFromPoint(pts)));
        }
    }
}
