using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MeisterGeister.View.Kalender
{
    public class KalenderGrid : UniformGrid
    {
        public KalenderGrid()
            : base()
        { }

        /// <summary>
        /// Compute the desired size of this UniformGrid by measuring all of the 
        /// children with a constraint equal to a cell's portion of the given
        /// constraint (e.g. for a 2 x 4 grid, the child constraint would be 
        /// constraint.Width*0.5 x constraint.Height*0.25).  The maximum child 
        /// width and maximum child height are tracked, and then the desired size
        /// is computed by multiplying these maximums by the row and column count 
        /// (e.g. for a 2 x 4 grid, the desired size for the UniformGrid would be
        /// maxChildDesiredWidth*2 x maxChildDesiredHeight*4).
        /// </summary>
        /// <param name="constraint">Constraint 
        /// <returns>Desired size</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            UpdateComputedValues();

            Size childConstraint = new Size(constraint.Width / _columns, constraint.Height / _rows);
            double maxChildDesiredWidth = 0.0;
            double maxChildDesiredHeight = 0.0;

            //  Measure each child, keeping track of maximum desired width and height.
            for (int i = 0, count = InternalChildren.Count; i < count; ++i)
            {
                UIElement child = InternalChildren[i];

                // Measure the child.
                child.Measure(childConstraint);
                Size childDesiredSize = child.DesiredSize;

                if (maxChildDesiredWidth < childDesiredSize.Width)
                {
                    maxChildDesiredWidth = childDesiredSize.Width;
                }

                if (maxChildDesiredHeight < childDesiredSize.Height)
                {
                    maxChildDesiredHeight = childDesiredSize.Height;
                }
            }

            return new Size((maxChildDesiredWidth * _columns), (maxChildDesiredHeight * _rows));
        }

        /// <summary>
        /// Arrange the children of this UniformGrid by distributing space evenly
        /// among all of the children, making each child the size equal to a cell's
        /// portion of the given arrangeSize (e.g. for a 2 x 4 grid, the child size 
        /// would be arrangeSize*0.5 x arrangeSize*0.25)
        /// </summary> 
        /// <param name="arrangeSize">Arrange size 
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Rect childBounds = new Rect(0, 0, arrangeSize.Width / _columns, arrangeSize.Height / _rows);
            double xStep = childBounds.Width;
            double xBound = arrangeSize.Width - 1.0;
            int column = FirstColumn;

            // Arrange and Position each child to the same cell size 
            foreach (UIElement child in InternalChildren)
            {
                int desiredColumn = GetColumn(child) - 1;
                if (desiredColumn >= 0 && desiredColumn <= _columns && desiredColumn != column)
                {
                    if(desiredColumn < column)
                        childBounds.Y += childBounds.Height;
                    column = desiredColumn;
                }

                childBounds.X = xStep * column;
                child.Arrange(childBounds);

                // only advance to the next grid cell if the child was not collapsed
                if (child.Visibility != Visibility.Collapsed)
                {
                    column++;
                    if (column > _columns)
                    {
                        childBounds.Y += childBounds.Height;
                        column = 0;
                    }
                }
            }

            return arrangeSize;
        } 

        /// <summary>
        /// If either Rows or Columns are set to 0, then dynamically compute these
        /// values based on the actual number of non-collapsed children.
        /// 
        /// In the case when both Rows and Columns are set to 0, then make Rows
        /// and Columns be equal, thus laying out in a square grid. 
        /// </summary> 
        private void UpdateComputedValues()
        { 
            _columns = Columns;
            _rows = Rows;
 
            //parameter checking. 
            if (FirstColumn >= _columns)
            { 
                //NOTE: maybe we shall throw here. But this is somewhat out of 
                //the MCC itself. We need a whole new panel spec.
                FirstColumn = 0; 
            }
 
            if ((_rows == 0) || (_columns == 0))
            { 
                int nonCollapsedCount = 0;
  
                // First compute the actual # of non-collapsed children to be laid out 
                for (int i = 0, count = InternalChildren.Count; i < count; ++i)
                { 
                    UIElement child = InternalChildren[i];
                    if (child.Visibility != Visibility.Collapsed)
                    {
                        nonCollapsedCount++; 
                    }
                } 
  
                // to ensure that we have at leat one row & column, make sure
                // that nonCollapsedCount is at least 1 
                if (nonCollapsedCount == 0)
                {
                    nonCollapsedCount = 1;
                } 
 
                if (_rows == 0) 
                { 
                    if (_columns > 0)
                    { 
                        // take FirstColumn into account, because it should really affect the result
                        _rows = (nonCollapsedCount + FirstColumn + (_columns - 1)) / _columns;
                    }
                    else
                    {
                        // both rows and columns are unset -- lay out in a square 
                        _rows = (int)Math.Sqrt(nonCollapsedCount); 
                        if ((_rows * _rows) < nonCollapsedCount)
                        { 
                            _rows++;
                        }
                        _columns = _rows;
                    } 
                }
                else if (_columns == 0) 
                { 
                    // guaranteed that _rows is not 0, because we're in the else clause of the check for _rows == 0
                    _columns = (nonCollapsedCount + (_rows - 1)) / _rows; 
                }
            }
        }
  
        private int _rows; 
        private int _columns;


        /// <summary>
        /// Column property. This is an attached property. 
        /// Grid defines Column property, so that it can be set
        /// on any element treated as a cell. Column property 
        /// specifies child's position with respect to columns. 
        /// </summary>
        /// <remarks> 
        /// <para> Columns are 0 - based. In order to appear in first column, element
        /// should have Column property set to <c>0</c>. </para>
        /// <para> Default value for the property is <c>0</c>. </para>
        /// </remarks> 
        public static readonly DependencyProperty ColumnProperty =
                DependencyProperty.RegisterAttached(
                      "Column",
                      typeof(int),
                      typeof(KalenderGrid),
                      new FrameworkPropertyMetadata(
                              0,
                              new PropertyChangedCallback(OnCellAttachedPropertyChanged)),
                      new ValidateValueCallback(IsIntValueNotNegative));

        /// <summary>
        /// <see cref="PropertyMetadata.PropertyChangedCallback"> 
        /// </see></summary>
        private static void OnCellAttachedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Visual child = d as Visual;

            if (child != null)
            {
                KalenderGrid grid = VisualTreeHelper.GetParent(child) as KalenderGrid;
                if (grid != null)
                {
                    grid.InvalidateMeasure();
                }
            }
        }

        /// <summary>
        /// Helper for setting Column property on a UIElement. 
        /// </summary>
        /// <param name="element">UIElement to set Column property on.
        /// <param name="value">Column property value.
        public static void SetColumn(UIElement element, int value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(ColumnProperty, value);
        }

        /// <summary>
        /// Helper for reading Column property from a UIElement. 
        /// </summary> 
        /// <param name="element">UIElement to read Column property from.
        /// <returns>Column property value.</returns> 
        [AttachedPropertyBrowsableForChildren()]
        public static int GetColumn(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return ((int)element.GetValue(ColumnProperty));
        }

        /// <summary>
        /// <see cref="DependencyProperty.ValidateValueCallback">
        /// </see></summary>
        private static bool IsIntValueNotNegative(object value)
        {
            return ((int)value >= 0);
        } 
    }
}
