using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für ImageDuplicator.xaml
    /// </summary>
    public partial class ImageDuplicator : UserControl
    {
        public ImageDuplicator()
        {
            InitializeComponent();

            ImagePathList = new ObservableCollection<string>();
            for (int i = 0; i < Anzahl; i++)
                ImagePathList.Add(ImageSource);
        }

        public ObservableCollection<string> ImagePathList
        {
            get { return (ObservableCollection<string>)GetValue(ImagePathListProperty); }
            set { SetValue(ImagePathListProperty, value); }
        }
        public static DependencyProperty ImagePathListProperty = DependencyProperty.Register("ImagePathList", typeof(ObservableCollection<string>), typeof(ImageDuplicator),
                new PropertyMetadata(null));

        public int Anzahl
        {
            get { return (int)GetValue(AnzahlProperty); }
            set { SetValue(AnzahlProperty, value); }
        }
        public static DependencyProperty AnzahlProperty = DependencyProperty.Register("Anzahl", typeof(int), typeof(ImageDuplicator),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnCurrentAnzahlChanged)));

        private static void OnCurrentAnzahlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageDuplicator control = (ImageDuplicator)d;
            control.ImagePathList = new ObservableCollection<string>();
            for (int i = 0; i < (int)e.NewValue; i++)
                control.ImagePathList.Add(control.ImageSource);
        }

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(ImageDuplicator),
                new FrameworkPropertyMetadata("/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png", FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnCurrentImageSourceChanged)));

        private static void OnCurrentImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageDuplicator control = (ImageDuplicator)d;
            for (int i = 0; i < control.ImagePathList.Count; i++)
                control.ImagePathList[i] = (string)e.NewValue;
        }
    }
}
