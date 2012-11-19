using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Arena.Logic
{

    /// <summary>
    /// Uniform hexagonal grid cell's metrics utility class.
    /// </summary>
    public class HexGridCell
    {
        private static int[] NEIGHBORS_DI = { 0, 1, 1, 0, -1, -1 };
        private static int[][] NEIGHBORS_DJ = { 
            new int[]{ -1, -1, 0, 1, 0, -1 }, new int[]{ -1, 0, 1, 1, 1, 0 } };

        private int[] CORNERS_DX; // array of horizontal offsets of the cell's corners
        private int[] CORNERS_DY; // array of vertical offsets of the cell's corners
        private int SIDE;

        private int mX = 0; // cell's left coordinate
        private int mY = 0; // cell's top coordinate

        private int mI = 0; // cell's horizontal grid coordinate
        private int mJ = 0; // cell's vertical grid coordinate


        private int radius = 1;
        /// <summary>
        /// Cell radius (distance from center to one of the corners)
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        private int height;
        /// <summary>
        /// Cell height
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        private int width;
        /// <summary>
        /// Cell width
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public static readonly int NUM_NEIGHBORS = 6;

        /**
         * @param radius Cell radius (distance from the center to one of the corners)
         */
        public HexGridCell(int radius)
        {
            this.radius = radius;
            width = radius * 2;
            height = (int)(((float)radius) * Math.Sqrt(3));
            SIDE = radius * 3 / 2;

            int[] cdx = { Radius / 2, SIDE, Width, SIDE, Radius / 2, 0 };
            CORNERS_DX = cdx;
            int[] cdy = { 0, 0, Height / 2, Height, Height, Height / 2 };
            CORNERS_DY = cdy;
        }

        /**
         * @return X coordinate of the cell's top left corner.
         */
        public int Left
        {
            get { return mX; }
        }

        /**
         * @return Y coordinate of the cell's top left corner.
         */
        public int Top
        {
            get { return mY; }
        }

        /**
         * @return X coordinate of the cell's center
         */
        public int CenterX
        {
            get { return mX + Radius; }
        }

        /**
         * @return Y coordinate of the cell's center
         */
        public int CenterY
        {
            get { return mY + Height / 2; }
        }

        /**
         * @return Horizontal grid coordinate for the cell.
         */
        public int IndexI
        {
            get { return mI; }
        }

        /**
         * @return Vertical grid coordinate for the cell.
         */
        public int IndexJ
        {
            get { return mJ; }
        }

        /**
         * @return Horizontal grid coordinate for the given neighbor.
         */
        public int getNeighborI(int neighborIdx)
        {
            return mI + NEIGHBORS_DI[neighborIdx];
        }

        /**
         * @return Vertical grid coordinate for the given neighbor.
         */
        public int getNeighborJ(int neighborIdx)
        {
            return mJ + NEIGHBORS_DJ[mI % 2][neighborIdx];
        }

        /**
         * Computes X and Y coordinates for all of the cell's 6 corners, clockwise,
         * starting from the top left.
         * 
         * @param cornersX Array to fill in with X coordinates of the cell's corners
         * @param cornersX Array to fill in with Y coordinates of the cell's corners
         */
        public void computeCorners(int[] cornersX, int[] cornersY)
        {
            for (int k = 0; k < NUM_NEIGHBORS; k++)
            {
                cornersX[k] = mX + CORNERS_DX[k];
                cornersY[k] = mY + CORNERS_DY[k];
            }
        }

        /**
         * Sets the cell's horizontal and vertical grid coordinates.
         */
        public void setCellIndex(int i, int j)
        {
            mI = i;
            mJ = j;
            mX = i * SIDE;
            mY = Height * (2 * j + (i % 2)) / 2;
        }

        /**
         * Sets the cell as corresponding to some point inside it (can be used for
         * e.g. mouse picking).
         */
        public void setCellByPoint(int x, int y)
        {
            int ci = (int)Math.Floor((float)x / (float)SIDE);
            int cx = x - SIDE * ci;

            int ty = y - (ci % 2) * Height / 2;
            int cj = (int)Math.Floor((float)ty / (float)Height);
            int cy = ty - Height * cj;

            if (cx > Math.Abs(Radius / 2 - Radius * cy / Height))
            {
                setCellIndex(ci, cj);
            }
            else
            {
                setCellIndex(ci - 1, cj + (ci % 2) - ((cy < Height / 2) ? 1 : 0));
            }
        }
    }
}
