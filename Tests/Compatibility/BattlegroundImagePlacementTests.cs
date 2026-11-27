#if NET10_COMPAT_TESTS
using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MeisterGeister.View.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class BattlegroundImagePlacementTests
    {
        [Theory]
        [InlineData(0.5, -800, -500, 100, 50, 2)]
        [InlineData(1.5, -3000, -1800, 40, 100, 0.5)]
        [InlineData(1.0, 0, 0, 100, 100, 1)]
        public void ImageIsCenteredInVisibleViewport(double zoom, double x, double y,
            double width, double height, double objectSize)
        {
            RunInSta(() =>
            {
                var viewport = new Canvas { Width = 800, Height = 600, ClipToBounds = true };
                var map = new Canvas { Width = 10000, Height = 10000 };
                var transforms = new TransformGroup();
                transforms.Children.Add(new ScaleTransform(zoom, zoom));
                transforms.Children.Add(new TranslateTransform(x, y));
                map.RenderTransform = transforms;
                Canvas.SetLeft(map, 37);
                Canvas.SetTop(map, 19);
                viewport.Children.Add(map);
                viewport.Measure(new Size(800, 600));
                viewport.Arrange(new Rect(0, 0, 800, 600));
                viewport.UpdateLayout();

                var image = new ImageObject("image.png", 123, 456)
                {
                    _imageOriginalWidth = width,
                    _imageOriginalHeigth = height,
                    ObjectSize = objectSize
                };
                MethodInfo centerImage = typeof(BattlegroundView).GetMethod("CenterImageInViewport",
                    BindingFlags.NonPublic | BindingFlags.Static);
                Assert.NotNull(centerImage);
                centerImage.Invoke(null, new object[] { viewport, map, image });

                Point actualCenter = map.TranslatePoint(new Point(image.ImagePositionX + image.ImageWidth / 2,
                    image.ImagePositionY + image.ImageHeight / 2), viewport);
                Assert.Equal(400, actualCenter.X, 8);
                Assert.Equal(300, actualCenter.Y, 8);
                Assert.Equal(width * objectSize, image.ImageWidth, 8);
                Assert.Equal(height * objectSize, image.ImageHeight, 8);
                Assert.Equal(image.ImagePositionX - 10, image.ZDisplayX, 8);
                Assert.Equal(image.ImagePositionY - 10, image.ZDisplayY, 8);
            });
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
