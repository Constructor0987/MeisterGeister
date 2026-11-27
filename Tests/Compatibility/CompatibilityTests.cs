#if NET10_COMPAT_TESTS
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Threading;
using MeisterGeister.Model.Extensions;
using MeisterGeister.Model.Service;
using MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class CompatibilityTests
    {
        [DataContract]
        public class Node
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public Node Next { get; set; }
        }

        public class Observable : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            [DependentProperty("Source")]
            public string Target { get; set; }
            public string LastChanged { get; private set; }
            public void OnChanged(string name)
            {
                LastChanged = name;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        [Fact]
        public void ArtifactModuleCanBeCreated()
        {
            RunInSta(() =>
            {
                using (var control = new ArtefaktGenerator.ArtGenControl(true))
                    Assert.NotNull(control);
            });
        }

        [Fact]
        public void AresModuleCanBeCreated()
        {
            RunInSta(() =>
            {
                using (var control = new Ares.MGPlugin.Controller())
                    Assert.NotNull(control);
            });
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AudioPathSearchRespectsConfiguredOption(bool searchSubdirectories)
        {
            string root = Path.Combine(Path.GetTempPath(), "MeisterGeisterAudioTest-" + Guid.NewGuid());
            string nested = Path.Combine(root, "nested");
            string missing = Path.Combine(root, "missing");
            Directory.CreateDirectory(nested);
            try
            {
                File.WriteAllText(Path.Combine(nested, "sample.wav"), "sample");
                RunInSta(() =>
                {
                    var row = new MeisterGeister.ViewModel.AudioPlayer.Logic.MusikZeileVM(searchSubdirectories);
                    row.StdPfad = new List<string> { root };
                    var title = new MeisterGeister.Model.Audio_Titel { Pfad = missing, Datei = "sample.wav" };

                    row.setTitelStdPfad(title);

                    Assert.Equal(searchSubdirectories ? root : missing, title.Pfad);
                    Assert.Equal(searchSubdirectories ? Path.Combine("nested", "sample.wav") : "sample.wav", title.Datei);
                });
            }
            finally
            {
                Directory.Delete(root, true);
            }
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

        [Fact]
        public void SerializerPreservesObjectReferences()
        {
            var node = new Node { Name = "Test" };
            node.Next = node;
            string xml = SerializationService.SerializeObject(node);
            var restored = SerializationService.DeserializeObject<Node>(xml);
            Assert.Equal("Test", restored.Name);
            Assert.Same(restored, restored.Next);
        }

        [Fact]
        public void DependentPropertyUsesDynamicInvocation()
        {
            var observable = new Observable();
            DependentProperty.PropagateINotifyProperyChanged(observable, new PropertyChangedEventArgs("Source"));
            Assert.Equal("Target", observable.LastChanged);
        }

        [Fact]
        public void CustomModifierProxySurvivesSerialization()
        {
            using (var factory = new CustomModifikatorFactory())
            {
                factory.Name = "Test";
                var modifier = factory.Finish();
                Assert.Equal("Test", modifier.Name);
                var data = (IDictionary<string, object>)ImpromptuInterface.Impromptu.UndoActLike(modifier);
                var originalMethods = data["Methoden"];
                string json = CustomModifikatorFactory.Serialize(modifier);
                Assert.Same(originalMethods, data["Methoden"]);
                var restored = CustomModifikatorFactory.Deserialize(json);
                Assert.Equal("Test", restored.Name);
            }
        }
    }
}
#endif
