using System;
using MeisterGeister.Model;
using NUnit.Framework;
using Global = MeisterGeister.Global;
using MeisterGeister.View.SpielerScreen;
using MeisterGeister.ViewModel.SpielerScreen;
using MeisterGeister.View.General;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.IO;
using System.Windows.Markup;
using System.Text;
using MeisterGeister;


namespace MeisterGeister_Tests
{
    [TestFixture, RequiresSTA]
    public class InteractiveUI_Tests
    {
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
            var w = CreateWindow(typeof(SpielerScreenControlView));
            w.DataContext = vm;
            w.ShowDialog();
        }

        Window CreateWindow(Type control)
        {
            const string xamlTemplate =
                @"<Window>
                    <Window.Resources>
                    </Window.Resources>
                    <v:{0} />
                </Window>";
            var xaml = String.Format(xamlTemplate, control.Name);

            Dictionary<string, Type> userTypes = new Dictionary<string, Type>();
            userTypes.Add("v", control);
            var window = (Window)ParseXAML(xaml, userTypes);

            return window;
        }

        public static object ParseXAML(string xaml, IDictionary<string, Type> userNamespaceTypes = null)
        {
            var context = new ParserContext();
            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            if (userNamespaceTypes != null)
                foreach (var abbreviation in userNamespaceTypes.Keys)
                {
                    context.XamlTypeMapper.AddMappingProcessingInstruction(abbreviation, userNamespaceTypes[abbreviation].Namespace, userNamespaceTypes[abbreviation].Assembly.FullName);
                    context.XmlnsDictionary.Add(abbreviation, abbreviation);
                }
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            return XamlReader.Load((Stream)new MemoryStream(Encoding.UTF8.GetBytes(xaml)), context);
        }

    }
}
