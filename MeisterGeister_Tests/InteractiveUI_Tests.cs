using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using MeisterGeister.View.SpielerScreen;
using MeisterGeister.ViewModel.SpielerScreen;
using NUnit.Framework;
using Global = MeisterGeister.Global;

namespace MeisterGeister_Tests
{
    [TestFixture, RequiresSTA]
    public class InteractiveUI_Tests
    {
        public static object ParseXAML(string xaml, IDictionary<string, Type> userNamespaceTypes = null)
        {
            var context = new ParserContext
            {
                XamlTypeMapper = new XamlTypeMapper(new string[0])
            };
            if (userNamespaceTypes != null)
            {
                foreach (var abbreviation in userNamespaceTypes.Keys)
                {
                    context.XamlTypeMapper.AddMappingProcessingInstruction(abbreviation, userNamespaceTypes[abbreviation].Namespace, userNamespaceTypes[abbreviation].Assembly.FullName);
                    context.XmlnsDictionary.Add(abbreviation, abbreviation);
                }
            }

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            return XamlReader.Load(new MemoryStream(Encoding.UTF8.GetBytes(xaml)), context);
        }

        [TestFixtureSetUp]
        public void SetupMethods()
        {
            if (Application.Current == null)
            {
                new Application();
                Application.ResourceAssembly = typeof(SpielerScreenControlView).Assembly;
                Application.Current.Resources.MergedDictionaries.Add(
                    Application.LoadComponent(
                        new Uri("DSA MeisterGeister;component/View/Themes/Style.xaml", UriKind.Relative)) as ResourceDictionary
                        );
            }

            Global.Init();
        }

        [TestFixtureTearDown]
        public void TearDownMethods()
        {
            Application.Current.Shutdown();
        }

        [SetUp]
        public void SetupTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        [Test]
        public void SpielerInfoViewTest()
        {
            SpielerScreenControlViewModel vm = SpielerScreenControlViewModel.Instance;
            Window w = CreateWindow(typeof(SpielerScreenControlView));
            w.DataContext = vm;
            w.ShowDialog();
        }

        private Window CreateWindow(Type control)
        {
            const string xamlTemplate =
                @"<Window>
                    <Window.Resources>
                    </Window.Resources>
                    <v:{0} />
                </Window>";
            var xaml = string.Format(xamlTemplate, control.Name);

            var userTypes = new Dictionary<string, Type>
            {
                { "v", control }
            };
            var window = (Window)ParseXAML(xaml, userTypes);

            return window;
        }
    }
}
