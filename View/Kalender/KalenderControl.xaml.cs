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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeisterGeister.Logic.Kalender.DsaTool;
using MeisterGeister.ViewModel.Kalender;
using MeisterGeister.ViewModel.Kalender.Logic;

namespace MeisterGeister.View.Kalender
{
    [TemplatePart(Name="PART_Day", Type=typeof(Button))]
    [TemplatePart(Name = "PART_MonthGrid", Type = typeof(Button))]
    public partial class KalenderControl : UserControl
    {
        DateList dateList = null;
        protected DateList DateList
        {
            get { return dateList; }
            set { 
                if(dateList != null)
                    dateList.CollectionChanged -= dateList_CollectionChanged;
                dateList = value;
                dateList.CollectionChanged += dateList_CollectionChanged;
            }
        }

        void dateList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (dateList.IsLoading)
                return;
            DrawUI();
        }

        public KalenderControl()
        {
            InitializeComponent();
            DateList = new DateList(Kalender, AnzuzeigendeTage);
            var c = DateList.Count;
        }

        void DrawUI()
        {
            
            InitializeMonat();
            InitializeWochen();
            InitializeWochentage();
        }

        #region DependencyProperties
        public DSADateCalendar Kalender
        {
            get { return (DSADateCalendar)GetValue(KalenderProperty); }
            set { SetValue(KalenderProperty, value); }
        }
        public static readonly DependencyProperty KalenderProperty = DependencyProperty.Register(
          "Kalender", typeof(DSADateCalendar), typeof(KalenderControl),
                new FrameworkPropertyMetadata(new DSADateCalendarTwelve(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnKalenderChanged)));

        private static void OnKalenderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            KalenderControl control = (KalenderControl)d;
            //if (control.VM == null)
            //    control.VM = new VM.ProbeControlViewModel();
            //if (e.NewValue is Model.Held)
            //    control.VM.Held = e.NewValue as Model.Held;
        }

        public DatumViewModel SelectedDatum
        {
            get { return (DatumViewModel)GetValue(SelectedDatumProperty); }
            set { SetValue(SelectedDatumProperty, value); }
        }
        public static readonly DependencyProperty SelectedDatumProperty = DependencyProperty.Register(
          "SelectedDatum", typeof(DatumViewModel), typeof(KalenderControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSelectedDatumChanged)));

        private static void OnSelectedDatumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            KalenderControl control = (KalenderControl)d;
            //if (control.VM == null)
            //    control.VM = new VM.ProbeControlViewModel();
            //if (e.NewValue is Model.Held)
            //    control.VM.Held = e.NewValue as Model.Held;
        }
        #endregion

        private int AnzuzeigendeTage
        {
            get { return Kalender.DaysPerMonth + Kalender.DaysPerWeek * 2; }
        }

        private int ErstesAngezeigtesDatumIndex = 0;


        /// <summary>
        /// Tage im Monat anzeigen
        /// </summary>
        void InitializeMonat()
        {
            var grid = PART_MonthGrid;
            for (int i = 0; i < AnzuzeigendeTage; i++)
            {
                //TODO am besten nicht immer neue Buttons erstellen sondern die buttons wiederverwenden.
                //TODO Button mit events und style
                var element = new Button();
                // DateList[i + ErstesAngezeigtesDatumIndex].IsLoading und dann anderes Template anzeigen, welches den Text darstellt.
                var d = DateList[i + ErstesAngezeigtesDatumIndex];
                d.PropertyChanged += d_PropertyChanged;
                //TODO BINDING machen mit condtition ob initialisiert
                //element.Content = string.Format("{0}", DateList[i + ErstesAngezeigtesDatumIndex].Data.Text);
                //element.Click += new RoutedEventHandler(monthModeButton_Click);
                //element.PreviewMouseDown += new MouseButtonEventHandler(element_PreviewMouseDown);
                //element.PreviewMouseMove += new MouseEventHandler(element_PreviewMouseMove);
                grid.Children.Add(element);
                //this._btnMonthMode[i, j] = element;
            }
        }

        void d_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Wochenbezeichnungen anzeigen
        /// </summary>
        void InitializeWochen()
        {

        }

        /// <summary>
        /// Namen der Wochentage anzeigen
        /// </summary>
        void InitializeWochentage()
        {

        }
    }
}
