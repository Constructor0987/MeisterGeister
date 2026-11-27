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
using MeisterGeister.ViewModel.Karte;
using DgSuche;
using System.Windows.Threading;
using WPFExtensions.Controls;

namespace MeisterGeister.View.Karte
{
    /// <summary>
    /// Interaktionslogik für KarteView.xaml
    /// </summary>
    public partial class KarteView : UserControl
    {
        private WrappingConverter dereGlobusToMapConverter;
        private WrappingConverter mapToDereGlobusConverter;

        public KarteView()
        {
            dereGlobusToMapConverter = new WrappingConverter();
            mapToDereGlobusConverter = new WrappingConverter();
            this.Resources.Add("DereGlobusToMapConverter", dereGlobusToMapConverter);
            this.Resources.Add("MapToDereGlobusConverter", mapToDereGlobusConverter);
            InitializeComponent();
            //VM = new KarteViewModel();
            DataContextChanged += KarteView_DataContextChanged;

            dTmr.Tick += new EventHandler(dTmr_Tick);
            dTmr.Interval = new TimeSpan(0, 0, 0, 0, 50);
        }

        void KarteView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            VM.MapZoomControl = MapScrollViewer;

            if(e.OldValue is KarteViewModel)
            {
                (e.OldValue as KarteViewModel).PropertyChanged -= VM_PropertyChanged;
            }
            if(VM != null)
            {
                VM.PropertyChanged += VM_PropertyChanged;
                RefreshConverter();
            }
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public KarteViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is KarteViewModel))
                    return null;
                return DataContext as KarteViewModel;
            }
            set {
                DataContext = value;
            }
        }

        void RefreshConverter()
        {
            if (VM == null)
                return;
            dereGlobusToMapConverter.Converter = VM.DereGlobusToMapConverter;
            mapToDereGlobusConverter.Converter = VM.MapToDereGlobusConverter;
        }

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DereGlobusToMapConverter" || e.PropertyName == "MapToDereGlobusConverter")
                RefreshConverter();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.ZoomControlSize = MapScrollViewer.RenderSize;
                VM.Refresh();
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VM.ZoomControlSize = MapScrollViewer.RenderSize;
        }

        private void DGSuche_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var win = button != null ? new Globus.SelectLandmarkeWindow(button.Name) : new Globus.SelectLandmarkeWindow();
            win.LandmarkeDoubleClicked += DGSuche_LandmarkeDoubleClicked;
            win.Owner = Application.Current.MainWindow;
            win.Show();
        }

        private void DGSuche_LandmarkeDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var win = sender as MeisterGeister.View.Globus.SelectLandmarkeWindow;
            if (win == null)
                return;

            Ortsmarke ort = win.SelectedItem;

            //switch (win.ButtonName)
            //{
            //    case "btnRouteStarting":
            //        VM.RouteStarting = ort;
            //        break;
            //    case "btnRouteEnding":
            //        VM.RouteEnding = ort;
            //        break;
            //    default:
            //        break;
            //}
        }

        DispatcherTimer dTmr = new DispatcherTimer();
        private void dTmr_Tick(object sender, EventArgs e)
        {
            VM.notZooming = true;
            dTmr.Stop();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dTmr.IsEnabled = false;
            dTmr.Start();
            VM.notZooming = false;
        }
    }

    public class KarteZoomControl : ZoomControl
    {
        private bool updatingViewport;
        private bool viewportTransaction;
        private readonly Dictionary<DependencyProperty, BindingBase> suspendedBindings = new Dictionary<DependencyProperty, BindingBase>();
        private static readonly DependencyProperty[] viewportProperties = { ZoomProperty, TranslateXProperty, TranslateYProperty };

        public static readonly DependencyProperty ViewportModeProperty = DependencyProperty.Register(
            nameof(ViewportMode), typeof(ZoomControlModes), typeof(KarteZoomControl),
            new FrameworkPropertyMetadata(ZoomControlModes.Custom, ViewportModeChanged));

        public ZoomControlModes ViewportMode
        {
            get { return (ZoomControlModes)GetValue(ViewportModeProperty); }
            set { SetValue(ViewportModeProperty, value); }
        }

        static KarteZoomControl()
        {
            // Keep the library's property animations disabled; animate the rendered transforms instead.
            ModeProperty.OverrideMetadata(typeof(KarteZoomControl),
                new FrameworkPropertyMetadata(ZoomControlModes.Custom, null, (obj, value) => ZoomControlModes.Custom));
            TranslateXProperty.OverrideMetadata(typeof(KarteZoomControl),
                new FrameworkPropertyMetadata(0.0, ViewportValueChanged, (obj, value) => ((KarteZoomControl)obj).CoerceTranslation((double)value, true)));
            TranslateYProperty.OverrideMetadata(typeof(KarteZoomControl),
                new FrameworkPropertyMetadata(0.0, ViewportValueChanged, (obj, value) => ((KarteZoomControl)obj).CoerceTranslation((double)value, false)));
            ZoomProperty.OverrideMetadata(typeof(KarteZoomControl),
                new FrameworkPropertyMetadata(1.0, ViewportValueChanged));
        }

        private static void ViewportValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = (KarteZoomControl)obj;
            if (control.IsLoaded && !control.viewportTransaction)
            {
                control.SetCurrentValue(ViewportModeProperty, ZoomControlModes.Custom);
                control.StopViewportAnimations();
            }
        }

        public KarteZoomControl()
        {
            Loaded += (s, e) =>
            {
                if (suspendedBindings.Count > 0)
                {
                    foreach (var property in viewportProperties)
                        if (suspendedBindings.TryGetValue(property, out BindingBase binding))
                            BindingOperations.SetBinding(this, property, binding);
                    suspendedBindings.Clear();
                    SetViewport(Zoom, TranslateX, TranslateY);
                }
                StopViewportAnimations();
            };
            Unloaded += (s, e) =>
            {
                var values = viewportProperties.ToDictionary(property => property, property => GetValue(property));
                foreach (var property in viewportProperties.Reverse())
                {
                    var binding = BindingOperations.GetBindingBase(this, property);
                    if (binding != null)
                    {
                        suspendedBindings[property] = binding;
                        BindingOperations.ClearBinding(this, property);
                    }
                }
                foreach (var property in viewportProperties)
                    SetCurrentValue(property, values[property]);
            };
        }

        private static void ViewportModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = (KarteZoomControl)obj;
            if (control.IsLoaded)
                control.ApplyViewportMode();
        }

        public new void ZoomToFill()
        {
            if (ViewportMode == ZoomControlModes.Fill)
                ApplyViewportMode();
            else
                SetCurrentValue(ViewportModeProperty, ZoomControlModes.Fill);
        }

        public new void ZoomToOriginal()
        {
            if (ViewportMode == ZoomControlModes.Original)
                ApplyViewportMode();
            else
                SetCurrentValue(ViewportModeProperty, ZoomControlModes.Original);
        }

        private void ApplyViewportMode()
        {
            var presenter = GetTemplateChild("PART_Presenter") as ZoomContentPresenter;
            if (presenter == null || presenter.ContentSize.Width <= 0 || presenter.ContentSize.Height <= 0)
                return;

            if (ViewportMode == ZoomControlModes.Custom)
                return;

            double zoom = ViewportMode == ZoomControlModes.Original ? 1 :
                Math.Min(ActualWidth / presenter.ContentSize.Width, ActualHeight / presenter.ContentSize.Height);
            SetViewport(zoom, -(presenter.ContentSize.Width - presenter.DesiredSize.Width) * zoom / 2,
                -(presenter.ContentSize.Height - presenter.DesiredSize.Height) * zoom / 2);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (ModifierMode == ZoomViewModifierMode.None &&
                (WheelZoomsAlone || (Keyboard.Modifiers & ModifierKeyZoom) != 0))
            {
                e.Handled = true;
                Point point = e.GetPosition(this);
                var presenter = (ZoomContentPresenter)GetTemplateChild("PART_Presenter");
                double factor = Math.Max(1 / MaxZoomDelta, Math.Min(MaxZoomDelta, 1 + e.Delta / 10000.0 * ZoomDeltaMultiplier));
                double zoom = Math.Max(MinZoom, Math.Min(MaxZoom, Zoom * factor));
                double ratio = zoom / Zoom;
                double originX = presenter.DesiredSize.Width / 2;
                double originY = presenter.DesiredSize.Height / 2;
                SetCurrentValue(ViewportModeProperty, ZoomControlModes.Custom);
                SetViewport(zoom, point.X - originX - (point.X - originX - TranslateX) * ratio,
                    point.Y - originY - (point.Y - originY - TranslateY) * ratio);
            }
            base.OnPreviewMouseWheel(e);
        }

        private double CoerceTranslation(double value, bool horizontal)
        {
            var presenter = GetTemplateChild("PART_Presenter") as ZoomContentPresenter;
            if (!LimitToContent || updatingViewport || presenter == null || !IsArrangeValid)
                return value;

            double content = horizontal ? presenter.ContentSize.Width : presenter.ContentSize.Height;
            double viewport = horizontal ? ActualWidth : ActualHeight;
            double origin = (horizontal ? presenter.DesiredSize.Width : presenter.DesiredSize.Height) / 2;
            double offset = origin * (1 - Zoom);
            if (content * Zoom <= viewport)
                return (viewport - content * Zoom) / 2 - offset;

            return Math.Max(viewport - content * Zoom - offset, Math.Min(-offset, value));
        }

        private void SetViewport(double zoom, double x, double y)
        {
            var presenter = (ZoomContentPresenter)GetTemplateChild("PART_Presenter");
            var transforms = (TransformGroup)presenter.RenderTransform;
            var scale = transforms.Children.OfType<ScaleTransform>().Single();
            var translate = transforms.Children.OfType<TranslateTransform>().Single();
            double oldZoom = scale.ScaleX;
            double oldX = translate.X;
            double oldY = translate.Y;

            viewportTransaction = true;
            try
            {
                updatingViewport = true;
                SetCurrentValue(ZoomProperty, zoom);
                SetCurrentValue(TranslateXProperty, x);
                SetCurrentValue(TranslateYProperty, y);
                updatingViewport = false;
                CoerceValue(TranslateXProperty);
                CoerceValue(TranslateYProperty);
                foreach (var property in viewportProperties)
                {
                    SetCurrentValue(property, GetValue(property));
                    GetBindingExpression(property)?.UpdateSource();
                }
            }
            finally
            {
                updatingViewport = false;
                viewportTransaction = false;
            }

            Animate(scale, ScaleTransform.ScaleXProperty, oldZoom, Zoom);
            Animate(scale, ScaleTransform.ScaleYProperty, oldZoom, Zoom);
            Animate(translate, TranslateTransform.XProperty, oldX, TranslateX);
            Animate(translate, TranslateTransform.YProperty, oldY, TranslateY);
        }

        private void Animate(System.Windows.Media.Animation.Animatable target, DependencyProperty property, double from, double to)
        {
            target.BeginAnimation(property, null);
            target.SetCurrentValue(property, to);
            if (AnimationLength > TimeSpan.Zero)
                target.BeginAnimation(property, new System.Windows.Media.Animation.DoubleAnimation(from, to, AnimationLength)
                {
                    FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop
                });
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SynchronizeViewport();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size result = base.ArrangeOverride(arrangeBounds);
            if (ViewportMode == ZoomControlModes.Fill)
                ApplyViewportMode();
            SynchronizeViewport();
            return result;
        }

        private void StopViewportAnimations()
        {
            var presenter = GetTemplateChild("PART_Presenter") as ZoomContentPresenter;
            if (presenter?.RenderTransform is TransformGroup transforms)
            {
                foreach (var transform in transforms.Children)
                {
                    if (transform is ScaleTransform scale)
                    {
                        scale.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                        scale.BeginAnimation(ScaleTransform.ScaleYProperty, null);
                    }
                    else if (transform is TranslateTransform translate)
                    {
                        translate.BeginAnimation(TranslateTransform.XProperty, null);
                        translate.BeginAnimation(TranslateTransform.YProperty, null);
                    }
                }
                SynchronizeViewport();
            }
        }

        private void SynchronizeViewport()
        {
            var presenter = GetTemplateChild("PART_Presenter") as ZoomContentPresenter;
            if (presenter == null || !(presenter.RenderTransform is TransformGroup transforms))
                throw new InvalidOperationException("Das Kartenzoom-Template benoetigt PART_Presenter mit Zoom-Transformationen.");

            foreach (var transform in transforms.Children)
            {
                if (transform is ScaleTransform scale)
                {
                    scale.ScaleX = Zoom;
                    scale.ScaleY = Zoom;
                }
                else if (transform is TranslateTransform translate)
                {
                    translate.X = TranslateX;
                    translate.Y = TranslateY;
                }
            }
        }
    }
}
