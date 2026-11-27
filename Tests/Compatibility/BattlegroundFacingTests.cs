#if NET10_COMPAT_TESTS
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf;
using MeisterGeister.ViewModel.Kampf.Logic;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    [CollectionDefinition("Battleground facing", DisableParallelization = true)]
    public class BattlegroundFacingCollection { }

    [Collection("Battleground facing")]
    public class BattlegroundFacingTests
    {
        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, -90)]
        public void FacingRotatesFrameButPreservesPortraitBehavior(bool hasToken, double expectedImageRotation)
        {
            RunInSta(() =>
            {
                var hero = new Held { Bild = "portrait.png", Token = hasToken ? "token.png" : null };
                var info = new KämpferInfo();
                typeof(KämpferInfo).GetField("_kämpfer", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(info, hero);
                hero.ki = info;
                hero.CreatureWidth = 160;
                hero.CreatureHeight = 80;
                int notifications = 0;
                hero.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(hero.RotateFrameDegrees)) notifications++;
                };

                hero.CalculateNewSightLineSektor(new Point(hero.CreatureX + 180, hero.CreatureY + 40), true);

                Assert.Equal(-90, hero.RotateFrameDegrees, 8);
                Assert.Equal(expectedImageRotation, hero.RotateImageDegrees, 8);
                Assert.Equal(6, hero.SightLineSektor);
                Assert.Equal(1, notifications);
            });
        }

        [Fact]
        public void PlayerScreenUsesTheSameFrameAndUprightElementBindings()
        {
            XNamespace wpf = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            var xaml = XDocument.Load(Path.Combine(AppContext.BaseDirectory, "SpielerWindow.xaml"));
            var template = xaml.Descendants(wpf + "DataTemplate")
                .Single(element => (string)element.Attribute("DataType") == "{x:Type model:BattlegroundCreature}");
            var frame = template.Descendants(wpf + "Rectangle")
                .Single(element => (string)element.Attribute("Stroke") == "#FF5D5757");
            Assert.Equal("0.5,0.5", (string)frame.Attribute("RenderTransformOrigin"));
            Assert.Equal("{Binding RotateFrameDegrees}", (string)frame.Descendants(wpf + "RotateTransform").Single().Attribute("Angle"));

            var labels = template.Descendants(wpf + "TextBlock")
                .Where(element => (string)element.Attribute("Canvas.Left") == "{Binding ZDisplayX}"
                    || (string)element.Attribute("Canvas.Left") == "{Binding CreatureNameX}").ToList();
            var images = template.Descendants(wpf + "Image")
                .Where(element => (string)element.Attribute("Canvas.Left") == "{Binding CreatureNameX}"
                    || (string)element.Attribute("Canvas.Left") == "{Binding ZDisplayX}").ToList();
            Assert.Equal(3, labels.Count);
            Assert.Equal(3, images.Count);
            Assert.All(labels.Concat(images), element =>
            {
                var transform = element.Descendants(wpf + "TranslateTransform").Single();
                Assert.Equal("{Binding FrameOffsetX}", (string)transform.Attribute("X"));
                Assert.Equal("{Binding FrameOffsetY}", (string)transform.Attribute("Y"));
            });

            var health = template.Descendants(wpf + "ProgressBar").Single();
            Assert.Equal("{Binding FrameBounds.X}", (string)health.Attribute("Canvas.Left"));
            Assert.Equal("{Binding FrameBounds.Y}", (string)health.Attribute("Canvas.Top"));
            Assert.Equal("{Binding FrameBounds.Height}", (string)health.Attribute("Height"));
            var longAction = template.Descendants(wpf + "Grid")
                .Single(element => (string)element.Attribute("Margin") == "{Binding FrameLongActionMargin}");
            Assert.Equal("{Binding FrameBounds.X}", (string)longAction.Attribute("Canvas.Left"));
            Assert.Equal("{Binding FrameBounds.Y}", (string)longAction.Attribute("Canvas.Top"));
            Assert.Empty(longAction.Descendants(wpf + "RotateTransform"));
        }

        [Fact]
        public void RecreatingMapViewsPreservesObjectsAndSettingsUntilCombatCloses()
        {
            RunInSta(() =>
            {
                var previousKampf = Global.CurrentKampf;
                try
                {
                    var kampf = new KampfViewModel(k => { }, (title, message) => true);
                    var map = kampf.GetOrCreateBodenplanViewModel();
                    var line = new PathLine(new Point(120, 150));
                    map.BattlegroundObjects.Add(line);
                    map.MeisterZoom = 0.75;
                    var firstGrid = new Grid();
                    map.AreanGrid = firstGrid;

                    var floatingMap = kampf.GetOrCreateBodenplanViewModel();
                    var floatingGrid = new Grid();
                    floatingMap.AreanGrid = floatingGrid;
                    Assert.Same(map, floatingMap);
                    Assert.Same(line, Assert.Single(floatingMap.BattlegroundObjects));
                    Assert.Equal(0.75, floatingMap.MeisterZoom);
                    Assert.Same(floatingGrid, map.AreanGrid);

                    var dockedMap = kampf.GetOrCreateBodenplanViewModel();
                    Assert.Same(map, dockedMap);
                    Assert.Same(line, Assert.Single(dockedMap.BattlegroundObjects));

                    var window = new Window();
                    bool closed = false;
                    window.Closing += (sender, args) => Assert.Equal(true, window.Tag);
                    window.Closed += (sender, args) => closed = true;
                    map.KampfWindow = window;
                    kampf.CloseCommand.Execute(null);
                    Assert.True(closed);
                    Assert.Null(map.AreanGrid);
                }
                finally
                {
                    Global.CurrentKampf = previousKampf;
                }
            });
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(6, 0, -90)]
        [InlineData(4, 0, 180)]
        [InlineData(11, 0, -45)]
        [InlineData(6, -72.5, -72.5)]
        public void OlderMapsRecoverFrameDirection(int sector, double imageRotation, double expected)
        {
            Assert.Equal(expected, BattlegroundCreature.GetLegacyFrameRotation(sector, imageRotation));
        }

        [Fact]
        public void FrameRotationIsWrittenToCreatureTable()
        {
            RunInSta(() =>
            {
                var hero = new Held { RotateFrameDegrees = 37.5 };
                var serializer = new BattlegroundXMLLoadSave();
                var create = typeof(BattlegroundXMLLoadSave).GetMethod("CreateStructurCreatureTbl", BindingFlags.Instance | BindingFlags.NonPublic);
                var add = typeof(BattlegroundXMLLoadSave).GetMethod("AddCreatureInfo", BindingFlags.Instance | BindingFlags.NonPublic);
                var table = (DataTable)create.Invoke(serializer, new object[] { hero });
                add.Invoke(serializer, new object[] { table, hero });
                using (var stream = new MemoryStream())
                {
                    table.WriteXml(stream, XmlWriteMode.WriteSchema);
                    stream.Position = 0;
                    var loaded = new DataTable();
                    loaded.ReadXml(stream);
                    Assert.Equal(37.5, Convert.ToDouble(loaded.Rows[0]["RotateFrameDegrees"]));
                }
            });
        }

        [Theory]
        [InlineData(0, 150, 150, 160, 80)]
        [InlineData(90, 190, 110, 80, 160)]
        [InlineData(180, 150, 150, 160, 80)]
        [InlineData(45, 145.1471862576, 105.1471862576, 169.7056274848, 169.7056274848)]
        public void ActionsRemainUprightAndOnTheSameSideOfTheRotatedFrame(
            double angle, double x, double y, double width, double height)
        {
            RunInSta(() =>
            {
                var creature = new Gegner(new GegnerBase { TokenSizeX = 2, TokenSizeY = 1 }) { ObjectSize = 1 };
                creature.CreatureX = 150;
                creature.CreatureY = 150;
                var root = new Canvas { Width = 500, Height = 500 };
                var actions = new Canvas { DataContext = creature };
                actions.SetBinding(Canvas.LeftProperty, new Binding("FrameBounds.X"));
                actions.SetBinding(Canvas.TopProperty, new Binding("FrameBounds.Y"));
                actions.SetBinding(FrameworkElement.WidthProperty, new Binding("FrameBounds.Width"));
                actions.SetBinding(FrameworkElement.HeightProperty, new Binding("FrameBounds.Height"));
                var button = new Button { Width = 30 };
                button.SetBinding(FrameworkElement.MarginProperty, new Binding("FrameActionMargin"));
                button.SetBinding(FrameworkElement.HeightProperty, new Binding("FrameActionHeight"));
                actions.Children.Add(button);
                root.Children.Add(actions);
                root.Measure(new Size(500, 500));
                root.Arrange(new Rect(0, 0, 500, 500));
                creature.RotateFrameDegrees = angle;
                root.UpdateLayout();

                Assert.Equal(x, creature.FrameBounds.X, 8);
                Assert.Equal(y, creature.FrameBounds.Y, 8);
                Assert.Equal(width, creature.FrameBounds.Width, 8);
                Assert.Equal(height, creature.FrameBounds.Height, 8);
                Assert.Equal(x - 150, creature.FrameOffsetX, 8);
                Assert.Equal(y - 150, creature.FrameOffsetY, 8);
                Assert.Equal(width, creature.FrameActionMargin.Left, 8);
                Assert.Equal(height, creature.FrameAttackDefenseMargin.Top, 8);
                Assert.Equal(width - 40, creature.FrameLongActionMargin.Left, 8);
                Assert.Equal(height - 20, creature.FrameLongActionMargin.Top, 8);

                Point point = button.TranslatePoint(new Point(15, button.ActualHeight / 2), root);
                Point topLeft = button.TranslatePoint(new Point(0, 0), root);
                Point topRight = button.TranslatePoint(new Point(30, 0), root);
                Assert.Equal(x + width, topLeft.X, 8);
                Assert.Equal(topLeft.Y, topRight.Y, 8);
                Assert.Equal(30, topRight.X - topLeft.X, 8);
                var hit = VisualTreeHelper.HitTest(root, point);
                Assert.NotNull(hit);
                DependencyObject current = hit.VisualHit;
                while (current != null && current != button) current = VisualTreeHelper.GetParent(current);
                Assert.Same(button, current);
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
            if (failure != null) ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
#endif
