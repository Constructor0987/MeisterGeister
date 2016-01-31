﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeisterGeister.View.General
{
    public class GridBehaviour
    {
        #region RowCount Property

        /// <summary>
        /// Adds the specified number of Rows to RowDefinitions. 
        /// Default Height is Auto
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.RegisterAttached(
                "RowCount", typeof(int), typeof(GridBehaviour),
                new PropertyMetadata(-1, RowCountChanged));

        // Get
        public static int GetRowCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        // Set
        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        // Change Event - Adds the Rows
        public static void RowCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.RowDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.RowDefinitions.Add(
                    new RowDefinition() { Height = new GridLength(GetRowLength(obj)) });

            //SetStarRows(grid);
        }

        #endregion

        #region ColumnCount Property

        /// <summary>
        /// Adds the specified number of Columns to ColumnDefinitions. 
        /// Default Width is Auto
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached(
                "ColumnCount", typeof(int), typeof(GridBehaviour),
                new PropertyMetadata(-1, ColumnCountChanged));

        // Get
        public static int GetColumnCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnCountProperty);
        }

        // Set
        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }

        // Change Event - Add the Columns
        public static void ColumnCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = new GridLength(GetColumnLength(obj)) });

            //SetStarColumns(grid);
        }

        #endregion



        public static double GetColumnLength(DependencyObject obj)
        {
            return (double)obj.GetValue(ColumnLengthProperty);
        }

        public static void SetColumnLength(DependencyObject obj, double value)
        {
            obj.SetValue(ColumnLengthProperty, value);
        }

        public static readonly DependencyProperty ColumnLengthProperty =
            DependencyProperty.RegisterAttached("ColumnLength", typeof(double), typeof(GridBehaviour), new PropertyMetadata(Double.NaN));




        public static double GetRowLength(DependencyObject obj)
        {
            return (double)obj.GetValue(RowLengthProperty);
        }

        public static void SetRowLength(DependencyObject obj, double value)
        {
            obj.SetValue(RowLengthProperty, value);
        }

        public static readonly DependencyProperty RowLengthProperty =
            DependencyProperty.RegisterAttached("RowLength", typeof(double), typeof(GridBehaviour), new PropertyMetadata(Double.NaN));



        //#region StarRows Property

        ///// <summary>
        ///// Makes the specified Row's Height equal to Star. 
        ///// Can set on multiple Rows
        ///// </summary>
        //public static readonly DependencyProperty StarRowsProperty =
        //    DependencyProperty.RegisterAttached(
        //        "StarRows", typeof(string), typeof(GridBehaviour),
        //        new PropertyMetadata(string.Empty, StarRowsChanged));

        //// Get
        //public static string GetStarRows(DependencyObject obj)
        //{
        //    return (string)obj.GetValue(StarRowsProperty);
        //}

        //// Set
        //public static void SetStarRows(DependencyObject obj, string value)
        //{
        //    obj.SetValue(StarRowsProperty, value);
        //}

        //// Change Event - Makes specified Row's Height equal to Star
        //public static void StarRowsChanged(
        //    DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
        //        return;

        //    SetStarRows((Grid)obj);
        //}

        //#endregion

        //#region StarColumns Property

        ///// <summary>
        ///// Makes the specified Column's Width equal to Star. 
        ///// Can set on multiple Columns
        ///// </summary>
        //public static readonly DependencyProperty StarColumnsProperty =
        //    DependencyProperty.RegisterAttached(
        //        "StarColumns", typeof(string), typeof(GridBehaviour),
        //        new PropertyMetadata(string.Empty, StarColumnsChanged));

        //// Get
        //public static string GetStarColumns(DependencyObject obj)
        //{
        //    return (string)obj.GetValue(StarColumnsProperty);
        //}

        //// Set
        //public static void SetStarColumns(DependencyObject obj, string value)
        //{
        //    obj.SetValue(StarColumnsProperty, value);
        //}

        //// Change Event - Makes specified Column's Width equal to Star
        //public static void StarColumnsChanged(
        //    DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
        //        return;

        //    SetStarColumns((Grid)obj);
        //}

        //#endregion

        //private static void SetStarColumns(Grid grid)
        //{
        //    string[] starColumns =
        //        GetStarColumns(grid).Split(',');

        //    for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
        //    {
        //        if (starColumns.Contains(i.ToString()))
        //            grid.ColumnDefinitions[i].Width =
        //                new GridLength(1, GridUnitType.Star);
        //    }
        //}

        //private static void SetStarRows(Grid grid)
        //{
        //    string[] starRows =
        //        GetStarRows(grid).Split(',');

        //    for (int i = 0; i < grid.RowDefinitions.Count; i++)
        //    {
        //        if (starRows.Contains(i.ToString()))
        //            grid.RowDefinitions[i].Height =
        //                new GridLength(1, GridUnitType.Star);
        //    }
        //}
    }
}
