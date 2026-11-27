#if NET10_COMPAT_TESTS
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class BattlegroundMovementPathTests
    {
        [Fact]
        public void DirectionChangesKeepOnePathAndTrackCurrentSegment()
        {
            RunInSta(() =>
            {
                var start = new Point(100, 100);
                var firstTurn = new Point(400, 100);
                var secondTurn = new Point(400, 500);
                var end = new Point(700, 500);
                var path = new PathLine(start);

                path.ChangeLastPoint(firstTurn);
                Assert.Equal(start, path.CurrentSegmentStartPoint);
                path.AddNewPointToSeries(firstTurn);
                path.ChangeLastPoint(secondTurn);
                Assert.Equal(firstTurn, path.CurrentSegmentStartPoint);
                path.AddNewPointToSeries(secondTurn);
                path.ChangeLastPoint(end);

                Assert.Equal(secondTurn, path.CurrentSegmentStartPoint);
                Assert.Equal(start, path.GetStartPoint);
                Assert.Equal(end, path.GetEndPoint);
                var figure = Assert.Single(path.PathGeometryData.Figures);
                Assert.Equal(start, figure.StartPoint);
                Assert.Equal(3, figure.Segments.Count);
                Assert.Equal(firstTurn, Assert.IsType<LineSegment>(figure.Segments[0]).Point);
                Assert.Equal(secondTurn, Assert.IsType<LineSegment>(figure.Segments[1]).Point);
                Assert.Equal(end, Assert.IsType<LineSegment>(figure.Segments[2]).Point);
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
