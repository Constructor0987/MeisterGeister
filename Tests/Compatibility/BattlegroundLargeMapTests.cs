#if NET10_COMPAT_TESTS
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class BattlegroundLargeMapTests
    {
        [Fact]
        public void MapSizeAndPlayerNavigationFollowCheckbox()
        {
            var vm = (BattlegroundViewModel)RuntimeHelpers.GetUninitializedObject(typeof(BattlegroundViewModel));
            foreach (string name in new[] { "_propertyChanged", "_propertyChanging" })
            {
                var field = typeof(MeisterGeister.ViewModel.Base.ViewModelBase).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(vm, Activator.CreateInstance(field.FieldType));
            }
            var listener = new PropertyChangeListener();
            vm.PropertyChanged += listener.Record;
            Assert.Equal(10000, vm.ArenaGridSize);
            Assert.Equal(0.05, vm.MinMapZoom);
            vm.GrosseMap = true;
            Assert.Equal(100000, vm.ArenaGridSize);
            Assert.Equal(0.005, vm.MinMapZoom);
            Assert.Contains(nameof(vm.ArenaGridSize), listener.Changes);
            Assert.Contains(nameof(vm.MinMapZoom), listener.Changes);
            vm.PlayerGridOffsetX = 10000;
            vm.PlayerGridOffsetY = -10000;
            vm.Right();
            vm.Down();
            Assert.Equal(10080, vm.PlayerGridOffsetX);
            Assert.Equal(-10080, vm.PlayerGridOffsetY);
            vm.GrosseMap = false;
            Assert.Equal(10000, vm.ArenaGridSize);
            Assert.Equal(0, vm.PlayerGridOffsetX);
            Assert.Equal(0, vm.PlayerGridOffsetY);
            Assert.Equal(0.05, vm.MeisterZoom);
        }

        [Fact]
        public void ExpandedFogRoundTripsThroughPng()
        {
            RunInSta(() =>
            {
                var original = new WriteableBitmap(10, 10, 96, 96, PixelFormats.Bgra32, null);
                var fog = FogMap.Expand(original, 100);
                FogMap.Paint(fog, new Point(950, 950), 1, 10, false);
                using (var stream = new MemoryStream())
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(fog));
                    encoder.Save(stream);
                    stream.Position = 0;
                    var decoded = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    var loaded = new WriteableBitmap(decoded.Frames[0]);
                    Assert.Equal(0, Pixel(loaded, 99, 99));
                    Assert.Equal(unchecked((int)0xff000000), Pixel(loaded, 90, 90));
                    Assert.Equal(0, Pixel(loaded, 1, 1));
                }
            });
        }

        private sealed class PropertyChangeListener
        {
            public readonly List<string> Changes = new List<string>();
            public void Record(object sender, PropertyChangedEventArgs args) { Changes.Add(args.PropertyName); }
        }

        [Fact]
        public void ExpandedFogPreservesPixelsAndCanBePaintedAtFarEdge()
        {
            RunInSta(() =>
            {
                var original = new WriteableBitmap(1000, 1000, 96, 96, PixelFormats.Bgra32, null);
                original.WritePixels(new Int32Rect(20, 30, 1, 1), new[] { unchecked((int)0xff123456) }, 4, 0);
                var fog = FogMap.Expand(original, 10000);
                Assert.Equal(10000, fog.PixelWidth);
                Assert.Equal(10000, fog.PixelHeight);
                Assert.Equal(unchecked((int)0xff123456), Pixel(fog, 20, 30));
                Assert.Equal(unchecked((int)0xff000000), Pixel(fog, 9999, 9999));
                FogMap.Paint(fog, new Point(99990, 99990), 1, 10, false);
                Assert.Equal(0, Pixel(fog, 9999, 9999));
                FogMap.Paint(fog, new Point(99990, 99990), 1, 10, true);
                Assert.Equal(unchecked((int)0xff000000), Pixel(fog, 9999, 9999));
                Assert.Same(fog, FogMap.Expand(fog, 1000));
            });
        }

        [Fact]
        public void SettingsRoundTripAndOlderFilesRemainReadable()
        {
            string filename = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xml");
            try
            {
                var settings = Enumerable.Repeat(0d, 24).ToList();
                settings[23] = 1;
                var serializer = new BattlegroundXMLLoadSave();
                serializer.SaveMapToXML(new ObservableCollection<BattlegroundBaseObject>(), filename, false, settings, false);
                Assert.Equal(1, serializer.LoadSettingsFromXML(filename)[23]);

                var xml = new XmlSerializer(typeof(XmlCollectionHelper));
                XmlCollectionHelper saved;
                using (var stream = File.OpenRead(filename))
                    saved = (XmlCollectionHelper)xml.Deserialize(stream);
                saved.ObsDT.Single(table => table.TableName == "Settings").Columns.Remove("GrosseMap");
                using (var stream = File.Create(filename))
                    xml.Serialize(stream, saved);
                Assert.Equal(23, serializer.LoadSettingsFromXML(filename).Count);
            }
            finally
            {
                if (File.Exists(filename)) File.Delete(filename);
            }
        }

        private static int Pixel(WriteableBitmap bitmap, int x, int y)
        {
            var pixel = new int[1];
            bitmap.CopyPixels(new Int32Rect(x, y, 1, 1), pixel, 4, 0);
            return pixel[0];
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
