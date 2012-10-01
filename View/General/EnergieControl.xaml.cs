using System;
using System.Collections.Generic;
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
// Eigene Usings
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für EnergieControl.xaml
    /// </summary>
    public partial class EnergieControl : UserControl
    {
        public EnergieControl()
        {
            InitializeComponent();
            SetEnergieArt();
        }

        public EnergieEnum Energie
        {
            get { return (EnergieEnum)GetValue(EnergieProperty); }
            set { SetValue(EnergieProperty, value); }
        }
        public static readonly DependencyProperty EnergieProperty = DependencyProperty.Register(
          "Energie", typeof(EnergieEnum), typeof(EnergieControl), new FrameworkPropertyMetadata(
              EnergieEnum.Lebensenergie, FrameworkPropertyMetadataOptions.AffectsRender, OnEnergieChanged));

        private static void OnEnergieChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnergieControl energieControl = (EnergieControl)d;
            energieControl.SetEnergieArt();
        }

        public void SetEnergieArt()
        {
            switch (Energie)
            {
                case EnergieEnum.Lebensenergie:
                    _rectangleAktuell.Fill = Brushes.Firebrick;
                    _rectangleMod.Fill = Brushes.Firebrick;
                    _rectangleÜberMax.Fill = Brushes.Maroon;
                    break;
                case EnergieEnum.Ausdauer:
                    _rectangleAktuell.Fill = Brushes.SteelBlue;
                    _rectangleMod.Fill = Brushes.SteelBlue;
                    _rectangleÜberMax.Fill = Brushes.DarkBlue;
                    break;
                case EnergieEnum.Astralenergie:
                    _rectangleAktuell.Fill = Brushes.BlueViolet;
                    _rectangleMod.Fill = Brushes.BlueViolet;
                    _rectangleÜberMax.Fill = Brushes.DarkMagenta;
                    break;
                case EnergieEnum.Karmaenergie:
                    _rectangleAktuell.Fill = Brushes.Gold;
                    _rectangleMod.Fill = Brushes.Gold;
                    _rectangleÜberMax.Fill = Brushes.Orange;
                    break;
                default:
                    break;
            }
        }

        private void Control_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (SelectedKämpfer != null)
            {
                if (e.Delta < 0)
                {
                    switch (Energie)
                    {
                        case EnergieEnum.Lebensenergie:
                            SelectedKämpfer.LebensenergieAktuell--;
                            break;
                        case EnergieEnum.Ausdauer:
                            SelectedKämpfer.AusdauerAktuell--;
                            break;
                        case EnergieEnum.Astralenergie:
                            SelectedKämpfer.AstralenergieAktuell--;
                            break;
                        case EnergieEnum.Karmaenergie:
                            SelectedKämpfer.KarmaenergieAktuell--;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (Energie)
                    {
                        case EnergieEnum.Lebensenergie:
                            SelectedKämpfer.LebensenergieAktuell++;
                            break;
                        case EnergieEnum.Ausdauer:
                            SelectedKämpfer.AusdauerAktuell++;
                            break;
                        case EnergieEnum.Astralenergie:
                            SelectedKämpfer.AstralenergieAktuell++;
                            break;
                        case EnergieEnum.Karmaenergie:
                            SelectedKämpfer.KarmaenergieAktuell++;
                            break;
                        default:
                            break;
                    }
                }
                SetEnergie();
            }
        }

        public IKämpfer SelectedKämpfer
        {
            get { return (IKämpfer)GetValue(SelectedKämpferProperty); }
            set { SetValue(SelectedKämpferProperty, value); }
        }
        public static readonly DependencyProperty SelectedKämpferProperty = DependencyProperty.Register(
          "SelectedKämpfer", typeof(IKämpfer), typeof(EnergieControl), new FrameworkPropertyMetadata(
              null, FrameworkPropertyMetadataOptions.AffectsRender, OnKämpferChanged));

        private static void OnKämpferChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnergieControl energieControl = (EnergieControl)d;
            energieControl.SetEnergie();
            // Event registrieren zur Aktualisierung der Anzeige, wenn sich die Energiestände ändern
            if (energieControl.SelectedKämpfer != null)
                energieControl.SelectedKämpfer.PropertyChanged += energieControl.SelectedKämpfer_PropertyChanged;
        }

        private void SelectedKämpfer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LebensenergieAktuell" || e.PropertyName == "LebensenergieMax"
                || e.PropertyName == "AusdauerAktuell" || e.PropertyName == "AusdauerMax"
                || e.PropertyName == "AstralenergieAktuell" || e.PropertyName == "AstralenergieMax"
                || e.PropertyName == "KarmaenergieAktuell" || e.PropertyName == "KarmaenergieMax")
                SetEnergie();
        }

        public void SetEnergie()
        {
            int energieAktuell = 0;
            int energieMax = 0;
            _labelInfo.Content = null;

            if (SelectedKämpfer != null)
            {
                switch (Energie)
                {
                    case EnergieEnum.Lebensenergie:
                        energieAktuell = SelectedKämpfer.LebensenergieAktuell;
                        energieMax = SelectedKämpfer.LebensenergieMax;
                        _labelInfo.Content = SelectedKämpfer.LebensenergieStatus;
                        _labelInfo.ToolTip = SelectedKämpfer.LebensenergieStatusDetails;
                        break;
                    case EnergieEnum.Ausdauer:
                        energieAktuell = SelectedKämpfer.AusdauerAktuell;
                        energieMax = SelectedKämpfer.AusdauerMax;
                        _labelInfo.Content = SelectedKämpfer.AusdauerStatus;
                        _labelInfo.ToolTip = SelectedKämpfer.AusdauerStatusDetails;
                        break;
                    case EnergieEnum.Astralenergie:
                        energieAktuell = SelectedKämpfer.AstralenergieAktuell;
                        energieMax = SelectedKämpfer.AstralenergieMax;
                        break;
                    case EnergieEnum.Karmaenergie:
                        energieAktuell = SelectedKämpfer.KarmaenergieAktuell;
                        energieMax = SelectedKämpfer.KarmaenergieMax;
                        break;
                    default:
                        break;
                }
            }

            if (energieAktuell < 0)
            {
                _rectangleAktuell.Width = 0;
                _rectangleMod.Width = 100;
                _rectangleÜberMax.Width = 0;
            }
            else if (energieAktuell <= energieMax)
            {
                _rectangleAktuell.Width = Math.Max(0, (double)energieAktuell / energieMax * 100.0);
                _rectangleMod.Width = Math.Max(0, (double)(energieMax - energieAktuell)
                                                   / energieMax * 100.0);
                _rectangleÜberMax.Width = 0;
            }
            else
            {
                if (energieMax == 0)
                    _rectangleAktuell.Width = 0;
                else
                    _rectangleAktuell.Width = 100;
                _rectangleMod.Width = 0;
                double w = Math.Max(0, (double)(energieAktuell - energieMax) / energieMax * 100.0);
                if (double.IsInfinity(w))
                    _rectangleÜberMax.Width = 100;
                else
                    _rectangleÜberMax.Width = w;

            }
        }
    }

    public enum EnergieEnum
    {
        Lebensenergie,
        Ausdauer,
        Astralenergie,
        Karmaenergie
    }
}