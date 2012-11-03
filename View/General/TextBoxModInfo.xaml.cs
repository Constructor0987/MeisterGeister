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

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für TextBoxModInfo.xaml
    /// </summary>
    public partial class TextBoxModInfo : UserControl
    {
        public TextBoxModInfo()
        {
            InitializeComponent();
        }

        public List<dynamic> ModListe
        {
            get { return (List<dynamic>)GetValue(ModListeProperty); }
            set { SetValue(ModListeProperty, value); }
        }
        public static DependencyProperty ModListeProperty = DependencyProperty.Register("ModListe", typeof(List<dynamic>), typeof(TextBoxModInfo),
                new PropertyMetadata(null));

        public string WertName
        {
            get { return (string)GetValue(WertNameProperty); }
            set { SetValue(WertNameProperty, value); }
        }
        public static DependencyProperty WertNameProperty = DependencyProperty.Register("WertName", typeof(string), typeof(TextBoxModInfo),
                new PropertyMetadata(string.Empty));

        public int StartWert
        {
            get { return (int)GetValue(StartWertProperty); }
            set { SetValue(StartWertProperty, value); }
        }
        public static DependencyProperty StartWertProperty = DependencyProperty.Register("StartWert", typeof(int), typeof(TextBoxModInfo),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnCurrentStartWertChanged)));

        private static void OnCurrentStartWertChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxModInfo box = (TextBoxModInfo)d;
            box.IsWertGesenkt = (int)e.NewValue > box.Wert;
            box.IsWertGesteigert = (int)e.NewValue < box.Wert;
        }

        public int Wert
        {
            get { return (int)GetValue(WertProperty); }
            set { SetValue(WertProperty, value); }
        }
        public static DependencyProperty WertProperty = DependencyProperty.Register("Wert", typeof(int), typeof(TextBoxModInfo),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnCurrentWertChanged)));
        
        private static void OnCurrentWertChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxModInfo box = (TextBoxModInfo)d;
            box.IsWertGesenkt = box.StartWert > (int)e.NewValue;
            box.IsWertGesteigert = box.StartWert < (int)e.NewValue;
        }

        public bool IsErschwernis
        {
            get { return (bool)GetValue(IsErschwernisProperty); }
            set { SetValue(IsErschwernisProperty, value); }
        }
        public static DependencyProperty IsErschwernisProperty = DependencyProperty.Register("IsErschwernis", typeof(bool), typeof(TextBoxModInfo),
                new PropertyMetadata(false));

        public bool IsWertGesenkt
        {
            get { return (bool)GetValue(IsWertGesenktProperty); }
            set { SetValue(IsWertGesenktProperty, value); }
        }
        public static DependencyProperty IsWertGesenktProperty = DependencyProperty.Register("IsWertGesenkt", typeof(bool), typeof(TextBoxModInfo),
                new PropertyMetadata(false));

        public bool IsWertGesteigert
        {
            get { return (bool)GetValue(IsWertGesteigertProperty); }
            set { SetValue(IsWertGesteigertProperty, value); }
        }
        public static DependencyProperty IsWertGesteigertProperty = DependencyProperty.Register("IsWertGesteigert", typeof(bool), typeof(TextBoxModInfo),
                new PropertyMetadata(false));
    }
}
