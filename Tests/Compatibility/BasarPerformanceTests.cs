#if NET10_COMPAT_TESTS
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MeisterGeister.ViewModel.Basar;
using MeisterGeister.ViewModel.Basar.Logic;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class BasarPerformanceTests
    {
        private sealed class Ware : IHandelsgut
        {
            public string Name { get; set; }
            public string Kategorie { get; set; }
            public string Tags { get; set; }
            public double? Gewicht { get; set; }
            public string Pfad { get; set; }
            public string ME { get; set; }
            public string Bemerkung { get; set; }
            public string Literatur { get; set; }
            public string Preis { get; set; }
        }

        private sealed class Observer
        {
            public readonly List<string[]> Published = new List<string[]>();

            public void OnChanged(object sender, PropertyChangedEventArgs args)
            {
                if (args.PropertyName == nameof(BasarViewModel.FilteredBasarItemListe))
                    Published.Add(((BasarViewModel)sender).FilteredBasarItemListe.Select(item => item.Name).ToArray());
            }
        }

        [Fact]
        public void FilteringPublishesOnlyTheFinalSortedList()
        {
            var viewModel = (BasarViewModel)RuntimeHelpers.GetUninitializedObject(typeof(BasarViewModel));
            var baseType = typeof(MeisterGeister.ViewModel.Base.ViewModelBase);
            foreach (string name in new[] { "_propertyChanged", "_propertyChanging" })
            {
                var field = baseType.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(viewModel, Activator.CreateInstance(field.FieldType));
            }
            viewModel.BasarItemListe = new List<BasarItem>
            {
                CreateItem("Zange", "Werkzeug", "Eisen", true),
                CreateItem("Beil", "Waffen", "Eisen", true),
                CreateItem("Axt", "Waffen", "Eisen", false),
                CreateItem("Bogen", "Waffen", "Holz", true)
            };

            var observer = new Observer();
            var published = observer.Published;
            viewModel.PropertyChanged += observer.OnChanged;

            viewModel.SuchText = " WAFfen  Eisen ";
            Assert.Single(published);
            Assert.Equal(new[] { "Axt", "Beil" }, published[0]);

            published.Clear();
            viewModel.NurVorhandeneWaren = true;
            Assert.Single(published);
            Assert.Equal(new[] { "Beil" }, published[0]);

            published.Clear();
            viewModel.SuchText = "";
            Assert.Single(published);
            Assert.Equal(new[] { "Beil", "Bogen", "Zange" }, published[0]);
            GC.KeepAlive(observer);
        }

        private static BasarItem CreateItem(string name, string kategorie, string tags, bool verfuegbar)
        {
            return new BasarItem
            {
                Item = new Ware { Name = name, Kategorie = kategorie, Tags = tags },
                ImGebietVorhanden = verfuegbar
            };
        }
    }
}
#endif
