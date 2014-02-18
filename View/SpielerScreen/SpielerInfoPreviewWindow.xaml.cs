using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeisterGeister.View.SpielerScreen
{
    /// <summary>
    /// Interaktionslogik für SpielerInfoPreviewWindow.xaml
    /// </summary>
    public partial class SpielerInfoPreviewWindow : Window
    {
        private VisualBrush _brush;
        public VisualBrush Brush 
        {
            get { return _brush; }
            set
            {
                _brush = value;

                double width = SpielerWindow.Instance.Width;
                double height = SpielerWindow.Instance.Height;
                Height = Width / width * height + 25;
                _rectangle.Fill = value;
            }
        }

        public SpielerInfoPreviewWindow()
        {
            InitializeComponent();
        }

        public SpielerInfoPreviewWindow(VisualBrush vb)
        {
            InitializeComponent();

            Brush = vb;
        }
    }
}
