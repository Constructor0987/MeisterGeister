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
using System.Windows.Controls.Primitives;
using MeisterGeister.ViewModel.Base;
using MeisterGeister.Logic.General.DataVirtualization;

namespace MeisterGeister.View.Kalender
{
    public partial class KalenderControl : UserControl
    {

        public KalenderControl()
        {
            DataContext = this;
            InitializeComponent();
            DateList = new DateList(Kalender, AnzuzeigendeTage);
            var c = DateList.Count;
            DSADateTime date = new DSADateTime();
            ErstesAngezeigtesDatumIndex = DateList.GetIndex(date);
        }

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

        public int AnzuzeigendeTage
        {
            get { return Kalender.DaysPerMonth + Kalender.DaysPerWeek * 2; }
        }

        public int TageProWoche
        {
            get { return Kalender.DaysPerWeek; }
        }

        private int erstesAngezeigtesDatumIndex = 0;
        //TODO automatisch aus SelectedDatum und anderen Informationen berechnen.
        public int ErstesAngezeigtesDatumIndex
        {
            get { return erstesAngezeigtesDatumIndex; }
            set { erstesAngezeigtesDatumIndex = value; }
        }


        /// <summary>
        /// Tage im Monat anzeigen
        /// </summary>
        void InitializeMonat()
        {
            var grid = PART_MonthGrid;
            for (int i = 0; i < AnzuzeigendeTage; i++)
            {
                // DateList[i + ErstesAngezeigtesDatumIndex].IsLoading und dann anderes Template anzeigen, welches den Text darstellt.
                var d = DateList[i + ErstesAngezeigtesDatumIndex];
                //TODO am besten nicht immer neue Buttons erstellen sondern die buttons wiederverwenden.
                //Button Style ist in den Grid-Resourcen
                var element = new Button();
                element.Command = OnSetDatum;
                element.CommandParameter = d;
                element.DataContext = d;
                element.Style = (Style)grid.FindResource("DayButton");
                grid.Children.Add(element);
                //this._btnMonthMode[i, j] = element;
            }
        }

        private CommandBase onSetDatum = null;
        CommandBase OnSetDatum
        {
            get
            {
                if (onSetDatum == null)
                    onSetDatum = new CommandBase(SetDatum, null);
                return onSetDatum;
            }
        }

        void SetDatum(object args)
        {
            var d = args as DataWrapper<DatumViewModel>;
            if(d != null && !d.IsLoading)
            {
                this.SelectedDatum = d.Data;
            }
        }

        /// <summary>
        /// Wochenbezeichnungen anzeigen
        /// </summary>
        void InitializeWochen()
        {
            //keine Ahnung wie ich bei den Wochen an die richtigen Daten kommen soll.
            //var grid = PART_WeeknumberGrid;
            //foreach (var wd in Kalender.WeekDayNames)
            //{
            //    var tb = new TextBlock();
            //    tb.DataContext = ...
            //}
        }

        /// <summary>
        /// Namen der Wochentage anzeigen
        /// </summary>
        void InitializeWochentage()
        {
            var grid = PART_WeekdayGrid;
            foreach(var wd in Kalender.WeekDayNames)
            {
                var tb = new TextBlock();
                tb.Text = wd;
                grid.Children.Add(tb);
            }
        }
    }
}
