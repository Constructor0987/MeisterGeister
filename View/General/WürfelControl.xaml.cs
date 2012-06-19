using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
// Eigene Usings
using MeisterGeister.Logic.General;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für WürfelControl.xaml
    /// </summary>
    public partial class WürfelControl : UserControl
    {
        public WürfelControl()
        {
            InitializeComponent();
        }

        public WürfelControl(uint ergebnis)
        {
            InitializeComponent();
            Ergebnis = ergebnis.ToString();
            ErgebnisText = "Gewürfelt";
        }

        public WürfelControl(uint ergebnis, WürfelEnum würfel)
        {
            InitializeComponent();
            Würfel = würfel;
            Ergebnis = ergebnis.ToString();
            ErgebnisText = "Gewürfelt";
        }

        private void SetContextMenu(WürfelEnum würfel)
        {
            // Context-Menü leeren
            ContextMenu.Items.Clear();

            // "Neu würfeln" Eintrag erzeugen
            MenuItem it = new MenuItem();
            it.Header = "Neu würfeln";
            it.Height = 28;
            it.Click += MenuItemWürfelÄndern_Click;
            ContextMenu.Items.Add(it);

            uint wAuge = 1;
            uint max = (uint)würfel;
            if (würfel == WürfelEnum._2W6)
            {
                wAuge = 2;
            }
            for (; wAuge <= max; wAuge++)
            {
                it = new MenuItem();
                it.Header = wAuge.ToString();
                it.Height = 28;
                it.Click += MenuItemWürfelÄndern_Click;
                ContextMenu.Items.Add(it);
            }
        }

        public void MenuItemWürfelÄndern_Click(object sender, RoutedEventArgs e)
        {
            uint ergebnis;
            if (((MenuItem)sender).Header.ToString() == "Neu würfeln")
            {
                ergebnis = Würfeln();
            }
            else
            {
                ergebnis = Convert.ToUInt32(((MenuItem)sender).Header.ToString());
            }
            Ergebnis = ergebnis.ToString();
            if (WürfelGeändert != null)
            {
                WürfelGeändert(ergebnis, ProbenErgebnis);
            }
        }

        private uint Würfeln()
        {
            uint ergebnis;
            uint anzahl = 1;
            uint seiten = 20;
            switch (Würfel)
            {
                case WürfelEnum._1W6:
                    seiten = 6;
                    break;
                case WürfelEnum._2W6:
                    seiten = 6;
                    anzahl = 2;
                    break;
                case WürfelEnum._1W20:
                    seiten = 20;
                    break;
                default:
                    break;
            }
            Würfel w = new Würfel(seiten);
            ergebnis = Convert.ToUInt32(w.Würfeln(anzahl).Summe);
            return ergebnis;
        }

        public event WürfelGeändertEventHandler WürfelGeändert;

        public new double Height
        {
            get { return Height; }
            set { base.Height = value; Width = value; }
        }

        public EigenschaftProbenErgebnis ProbenErgebnis
        {
            get { return (EigenschaftProbenErgebnis)GetValue(ProbenErgebnisProperty); }
            set { SetValue(ProbenErgebnisProperty, value); }
        }
        public static readonly DependencyProperty ProbenErgebnisProperty = DependencyProperty.Register(
          "ProbenErgebnis", typeof(EigenschaftProbenErgebnis), typeof(WürfelControl));

        public string Ergebnis
        {
            get { return (string)GetValue(ErgebnisProperty); }
            set { SetValue(ErgebnisProperty, value); }
        }
        public static readonly DependencyProperty ErgebnisProperty = DependencyProperty.Register(
          "Ergebnis", typeof(string), typeof(WürfelControl));

        public string ErgebnisText
        {
            get { return (string)GetValue(ErgebnisTextProperty) + ": " + Ergebnis; }
            set { SetValue(ErgebnisTextProperty, value + ": " + Ergebnis); }
        }
        public static readonly DependencyProperty ErgebnisTextProperty = DependencyProperty.Register(
          "ErgebnisText", typeof(string), typeof(WürfelControl));

        public WürfelEnum Würfel
        {
            get { return (WürfelEnum)GetValue(WürfelProperty); }
            set
            {
                SetValue(WürfelProperty, value);
                SetWürfelImage(value);
                
            }
        }

        private void SetWürfelImage(WürfelEnum value)
        {
            string w;
            switch (value)
            {
                case WürfelEnum._1W6:
                case WürfelEnum._2W6:
                    w = "w6_blank.png";
                    _textBlockErgebnis.Foreground = System.Windows.Media.Brushes.Black;
                    break;
                case WürfelEnum._1W20:
                default:
                    w = "w20_blank.png";
                    _textBlockErgebnis.Foreground = System.Windows.Media.Brushes.White;
                    break;
            }
            string imagesPath = "pack://application:,,/DSA MeisterGeister;component/Images/Icons/Wuerfel/" + w;
            Uri uri = new Uri(@imagesPath, UriKind.Absolute);
            BitmapImage bitmap = new BitmapImage(uri);
            _image.ImageSource = bitmap;
        }
        public static readonly DependencyProperty WürfelProperty = DependencyProperty.Register(
          "Würfel", typeof(WürfelEnum), typeof(WürfelControl));

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            SetContextMenu(Würfel);
        }

        private void WürfelControl1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Neu würfeln
            uint ergebnis = Würfeln();
            Ergebnis = ergebnis.ToString();

            // Event werfen
            if (WürfelGeändert != null)
            {
                WürfelGeändert(ergebnis, ProbenErgebnis);
            }
        }

        private void WürfelControl1_Loaded(object sender, RoutedEventArgs e)
        {
            SetWürfelImage(Würfel);
        }

    }

    public delegate void WürfelGeändertEventHandler(uint ergebnis, EigenschaftProbenErgebnis probenErgebnis);
}
