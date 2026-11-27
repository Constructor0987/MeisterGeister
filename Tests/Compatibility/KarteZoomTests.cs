#if NET10_COMPAT_TESTS
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using MeisterGeister.View.Karte;
using WPFExtensions.Controls;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class KarteZoomTests
    {
        public class Viewport : INotifyPropertyChanged
        {
            private double zoom;
            private double translateX;
            private double translateY;
            public event PropertyChangedEventHandler PropertyChanged;

            public double Zoom
            {
                get { return zoom; }
                set { Set(ref zoom, value, nameof(Zoom)); }
            }
            public double TranslateX
            {
                get { return translateX; }
                set { Set(ref translateX, value, nameof(TranslateX)); }
            }
            public double TranslateY
            {
                get { return translateY; }
                set { Set(ref translateY, value, nameof(TranslateY)); }
            }

            private void Set(ref double field, double value, string name)
            {
                if (field == value)
                    return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        [Theory]
        [InlineData(120)]
        [InlineData(-120)]
        public void FirstWheelZoomPreservesViewportAfterWindowChanges(int delta)
        {
            RunInSta(() =>
            {
                var viewport = new Viewport { Zoom = 0.6, TranslateX = -100, TranslateY = -80 };
                foreach (double width in new[] { 1045.0, 900.0, 1045.0 })
                {
                    double initialZoom = viewport.Zoom;
                    double initialX = viewport.TranslateX;
                    double initialY = viewport.TranslateY;
                    var control = new KarteZoomControl
                    {
                        Mode = ZoomControlModes.Custom,
                        MinZoom = 0.09,
                        MaxZoom = 3,
                        MaxZoomDelta = 3,
                        ZoomDeltaMultiplier = 50,
                        WheelZoomsAlone = true,
                        LimitToContent = true,
                        AnimationLength = TimeSpan.Zero,
                        Content = new Canvas { Width = 20000, Height = 15000 },
                        Template = CreateTemplate()
                    };
                    control.SetBinding(ZoomControl.ZoomProperty, new Binding(nameof(Viewport.Zoom)) { Source = viewport, Mode = BindingMode.TwoWay });
                    control.SetBinding(ZoomControl.TranslateXProperty, new Binding(nameof(Viewport.TranslateX)) { Source = viewport, Mode = BindingMode.TwoWay });
                    control.SetBinding(ZoomControl.TranslateYProperty, new Binding(nameof(Viewport.TranslateY)) { Source = viewport, Mode = BindingMode.TwoWay });
                    var window = new Window { Content = control, Width = width, Height = 650, ShowInTaskbar = false };
                    try
                    {
                        window.Show();
                        window.UpdateLayout();

                        Assert.Equal(ZoomControlModes.Custom, control.Mode);
                        Assert.Equal(initialZoom, viewport.Zoom, 8);
                        Assert.Equal(initialX, viewport.TranslateX, 8);
                        Assert.Equal(initialY, viewport.TranslateY, 8);
                        AssertTransforms(control, initialZoom, initialX, initialY);

                        var wheel = new MouseWheelEventArgs(Mouse.PrimaryDevice, 0, delta)
                        {
                            RoutedEvent = UIElement.PreviewMouseWheelEvent
                        };
                        control.RaiseEvent(wheel);
                        double expectedZoom = Math.Max(0.09, Math.Min(3, initialZoom * (1 + delta / 10000.0 * 50)));
                        WaitForZoom(control, expectedZoom);

                        Assert.True(wheel.Handled);
                        Assert.Equal(expectedZoom, control.Zoom, 8);
                        Assert.Equal(expectedZoom, viewport.Zoom, 8);
                        AssertTransforms(control, expectedZoom, control.TranslateX, control.TranslateY);
                    }
                    finally
                    {
                        window.Close();
                    }
                }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void FillOriginalAndEdgeZoomStayStable(bool retainedTab)
        {
            RunInSta(() =>
            {
                var viewport = new Viewport { Zoom = 0.6, TranslateX = -100, TranslateY = -80 };
                KarteZoomControl oldControl = null;
                if (retainedTab)
                {
                    oldControl = CreateBoundControl(viewport);
                    var oldWindow = new Window { Content = oldControl, Width = 1045, Height = 650, ShowInTaskbar = false };
                    oldWindow.Show();
                    oldWindow.UpdateLayout();
                    oldWindow.Close();
                    SettleAnimations();
                }
                var control = CreateBoundControl(viewport);
                var window = new Window { Content = control, Width = 900, Height = 650, ShowInTaskbar = false };
                try
                {
                    window.Show();
                    window.UpdateLayout();
                    var presenter = (ZoomContentPresenter)control.Template.FindName("PART_Presenter", control);
                    double fillZoom = Math.Min(control.ActualWidth / presenter.ContentSize.Width,
                        control.ActualHeight / presenter.ContentSize.Height);
                    ((RadioButton)control.Template.FindName("FillButton", control)).IsChecked = true;
                    WaitForZoom(control, fillZoom);
                    SettleAnimations();
                    Assert.Equal(fillZoom, control.Zoom, 8);
                    AssertImageBounds(control);

                    double originalX = -(presenter.ContentSize.Width - presenter.DesiredSize.Width) / 2;
                    double originalY = -(presenter.ContentSize.Height - presenter.DesiredSize.Height) / 2;
                    ((RadioButton)control.Template.FindName("OriginalButton", control)).IsChecked = true;
                    WaitForZoom(control, 1);
                    SettleAnimations();
                    Assert.Equal(1, control.Zoom, 8);
                    Assert.Equal(originalX, control.TranslateX, 8);
                    Assert.Equal(originalY, control.TranslateY, 8);
                    Assert.Equal(control.TranslateX, viewport.TranslateX, 8);
                    Assert.Equal(control.TranslateY, viewport.TranslateY, 8);
                    AssertTransforms(control, 1, originalX, originalY);
                    AssertImageBounds(control);

                    foreach (int delta in new[] { -120, -120, 120, 120 })
                    {
                        double expected = Math.Max(control.MinZoom, Math.Min(control.MaxZoom,
                            control.Zoom * (1 + delta / 10000.0 * control.ZoomDeltaMultiplier)));
                        control.RaiseEvent(new MouseWheelEventArgs(Mouse.PrimaryDevice, 0, delta)
                        {
                            RoutedEvent = UIElement.PreviewMouseWheelEvent
                        });
                        WaitForZoom(control, expected);
                        double x = control.TranslateX;
                        double y = control.TranslateY;
                        SettleAnimations();
                        Assert.Equal(expected, control.Zoom, 8);
                        Assert.Equal(x, control.TranslateX, 8);
                        Assert.Equal(y, control.TranslateY, 8);
                        Assert.Equal(x, viewport.TranslateX, 8);
                        Assert.Equal(y, viewport.TranslateY, 8);
                        AssertTransforms(control, expected, x, y);
                        AssertImageBounds(control);
                    }
                    GC.KeepAlive(oldControl);
                }
                finally
                {
                    window.Close();
                }
            });
        }

        [Fact]
        public void SliderAfterFillAndReturningViewKeepLatestZoom()
        {
            RunInSta(() =>
            {
                var viewport = new Viewport { Zoom = 0.6, TranslateX = -100, TranslateY = -80 };
                var original = CreateBoundControl(viewport);
                var main = new Window { Content = original, Width = 1045, Height = 650, ShowInTaskbar = false };
                var floating = new Window { Width = 900, Height = 650, ShowInTaskbar = false };
                try
                {
                    main.Show();
                    main.UpdateLayout();
                    main.Content = null;
                    SettleAnimations();
                    var separate = CreateBoundControl(viewport);
                    floating.Content = separate;
                    floating.Show();
                    floating.UpdateLayout();
                    separate.ZoomToFill();
                    SettleAnimations();
                    separate.SetCurrentValue(ZoomControl.ZoomProperty, 0.9);
                    separate.GetBindingExpression(ZoomControl.ZoomProperty).UpdateSource();
                    separate.UpdateLayout();
                    SettleAnimations();
                    Assert.Equal(ZoomControlModes.Custom, separate.ViewportMode);
                    Assert.Equal(0.9, separate.Zoom, 8);
                    Assert.Equal(0.9, viewport.Zoom, 8);

                    floating.Content = null;
                    SettleAnimations();
                    main.Content = original;
                    main.UpdateLayout();
                    SettleAnimations();
                    Assert.Equal(0.9, original.Zoom, 8);
                    Assert.Equal(0.9, viewport.Zoom, 8);
                    Assert.Equal(original.TranslateX, viewport.TranslateX, 8);
                    Assert.Equal(original.TranslateY, viewport.TranslateY, 8);
                    AssertTransforms(original, 0.9, original.TranslateX, original.TranslateY);
                    original.ZoomToOriginal();
                    SettleAnimations();
                    Assert.Equal(1, original.Zoom, 8);
                    AssertImageBounds(original);
                    AssertTransforms(original, 1, original.TranslateX, original.TranslateY);
                }
                finally
                {
                    floating.Close();
                    main.Close();
                }
            });
        }

        [Fact]
        public void AnimatedWheelZoomInSeparateBattlemapKeepsViewportAfterAnimation()
        {
            RunInSta(() =>
            {
                var viewport = new Viewport { Zoom = 0.1, TranslateX = -250, TranslateY = -300 };
                var original = CreateBoundControl(viewport);
                original.Content = new Canvas { Width = 10000, Height = 10000 };
                original.MinZoom = 0.05;
                original.MaxZoom = 1.5;
                var main = new Window { Content = original, Width = 1200, Height = 800, ShowInTaskbar = false };
                var floating = new Window { Width = 900, Height = 650, ShowInTaskbar = false };
                try
                {
                    main.Show();
                    main.UpdateLayout();
                    main.Content = null;
                    var separate = CreateBoundControl(viewport);
                    separate.Content = new Canvas { Width = 10000, Height = 10000 };
                    separate.MinZoom = 0.05;
                    separate.MaxZoom = 1.5;
                    separate.MaxZoomDelta = 2;
                    floating.Content = separate;
                    floating.Show();
                    floating.UpdateLayout();

                    separate.RaiseEvent(new MouseWheelEventArgs(Mouse.PrimaryDevice, 0, 120)
                    {
                        RoutedEvent = UIElement.PreviewMouseWheelEvent
                    });
                    Assert.Equal(0.16, separate.Zoom, 8);
                    double x = separate.TranslateX;
                    double y = separate.TranslateY;
                    SettleAnimations();
                    Assert.Equal(0.16, viewport.Zoom, 8);
                    Assert.Equal(x, separate.TranslateX, 8);
                    Assert.Equal(y, separate.TranslateY, 8);
                    Assert.Equal(x, viewport.TranslateX, 8);
                    Assert.Equal(y, viewport.TranslateY, 8);
                    AssertTransforms(separate, 0.16, x, y);

                    floating.Content = null;
                    main.Content = original;
                    main.UpdateLayout();
                    SettleAnimations();
                    Assert.Equal(0.16, original.Zoom, 8);
                    Assert.Equal(original.TranslateX, viewport.TranslateX, 8);
                    Assert.Equal(original.TranslateY, viewport.TranslateY, 8);
                    AssertTransforms(original, 0.16, original.TranslateX, original.TranslateY);
                    AssertImageBounds(original);
                }
                finally
                {
                    floating.Close();
                    main.Close();
                }
            });
        }

        private static KarteZoomControl CreateBoundControl(Viewport viewport)
        {
            var control = new KarteZoomControl
            {
                Mode = ZoomControlModes.Custom,
                MinZoom = 0.09,
                MaxZoom = 3,
                MaxZoomDelta = 3,
                ZoomDeltaMultiplier = 50,
                WheelZoomsAlone = true,
                LimitToContent = true,
                AnimationLength = TimeSpan.FromMilliseconds(100),
                Content = new Canvas { Width = 1200, Height = 800 },
                Template = CreateTemplate()
            };
            control.SetBinding(ZoomControl.ZoomProperty, new Binding(nameof(Viewport.Zoom)) { Source = viewport, Mode = BindingMode.TwoWay });
            control.SetBinding(ZoomControl.TranslateXProperty, new Binding(nameof(Viewport.TranslateX)) { Source = viewport, Mode = BindingMode.TwoWay });
            control.SetBinding(ZoomControl.TranslateYProperty, new Binding(nameof(Viewport.TranslateY)) { Source = viewport, Mode = BindingMode.TwoWay });
            return control;
        }

        private static void SettleAnimations()
        {
            var frame = new DispatcherFrame();
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
            timer.Tick += (s, e) => frame.Continue = false;
            timer.Start();
            try { Dispatcher.PushFrame(frame); }
            finally { timer.Stop(); }
        }

        private static void WaitForZoom(KarteZoomControl control, double expectedZoom)
        {
            var frame = new DispatcherFrame();
            var timeout = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            EventHandler rendered = (s, e) =>
            {
                if (Math.Abs(control.Zoom - expectedZoom) < 1e-8)
                    frame.Continue = false;
            };
            timeout.Tick += (s, e) => frame.Continue = false;
            CompositionTarget.Rendering += rendered;
            try
            {
                timeout.Start();
                Dispatcher.PushFrame(frame);
            }
            finally
            {
                timeout.Stop();
                CompositionTarget.Rendering -= rendered;
            }
        }

        private static ControlTemplate CreateTemplate()
        {
            return (ControlTemplate)XamlReader.Parse(
                "<ControlTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" " +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" " +
                "xmlns:z=\"clr-namespace:WPFExtensions.Controls;assembly=WPFExtensions\" " +
                "xmlns:c=\"clr-namespace:WPFExtensions.Converters;assembly=WPFExtensions\" TargetType=\"{x:Type z:ZoomControl}\">" +
                "<ControlTemplate.Resources><c:EqualityToBooleanConverter x:Key=\"ModeConverter\" /></ControlTemplate.Resources>" +
                "<Grid><z:ZoomContentPresenter x:Name=\"PART_Presenter\" Content=\"{TemplateBinding Content}\" />" +
                "<StackPanel HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\">" +
                "<RadioButton x:Name=\"FillButton\" GroupName=\"ZoomMode\" Content=\"Fill\" " +
                "IsChecked=\"{Binding ViewportMode, RelativeSource={RelativeSource TemplatedParent}, Mode=TwoWay, " +
                "Converter={StaticResource ModeConverter}, ConverterParameter={x:Static z:ZoomControlModes.Fill}}\" />" +
                "<RadioButton x:Name=\"OriginalButton\" GroupName=\"ZoomMode\" Content=\"1:1\" " +
                "IsChecked=\"{Binding ViewportMode, RelativeSource={RelativeSource TemplatedParent}, Mode=TwoWay, " +
                "Converter={StaticResource ModeConverter}, ConverterParameter={x:Static z:ZoomControlModes.Original}}\" />" +
                "</StackPanel></Grid></ControlTemplate>");
        }

        private static void AssertImageBounds(KarteZoomControl control)
        {
            var presenter = (ZoomContentPresenter)control.Template.FindName("PART_Presenter", control);
            Point topLeft = presenter.TranslatePoint(new Point(), control);
            double width = presenter.ContentSize.Width * control.Zoom;
            double height = presenter.ContentSize.Height * control.Zoom;
            if (width <= control.ActualWidth)
                Assert.Equal((control.ActualWidth - width) / 2, topLeft.X, 6);
            else
            {
                Assert.True(topLeft.X <= 1e-6, $"Left edge: {topLeft.X}");
                Assert.True(topLeft.X + width >= control.ActualWidth - 1e-6, $"Right edge: {topLeft.X + width}");
            }
            if (height <= control.ActualHeight)
                Assert.Equal((control.ActualHeight - height) / 2, topLeft.Y, 6);
            else
            {
                Assert.True(topLeft.Y <= 1e-6, $"Top edge: {topLeft.Y}");
                Assert.True(topLeft.Y + height >= control.ActualHeight - 1e-6, $"Bottom edge: {topLeft.Y + height}");
            }
        }

        private static void AssertTransforms(KarteZoomControl control, double zoom, double x, double y)
        {
            var presenter = Assert.IsType<ZoomContentPresenter>(control.Template.FindName("PART_Presenter", control));
            var transforms = Assert.IsType<TransformGroup>(presenter.RenderTransform);
            var scale = Assert.Single(transforms.Children.OfType<ScaleTransform>());
            var translate = Assert.Single(transforms.Children.OfType<TranslateTransform>());
            Assert.Equal(zoom, scale.ScaleX, 8);
            Assert.Equal(zoom, scale.ScaleY, 8);
            Assert.Equal(x, translate.X, 8);
            Assert.Equal(y, translate.Y, 8);
        }

        private static void RunInSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try { action(); }
                catch (Exception ex) { failure = ex; }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
                ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
#endif
