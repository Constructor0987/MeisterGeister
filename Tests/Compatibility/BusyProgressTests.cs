#if NET10_COMPAT_TESTS
using System;
using System.Runtime.CompilerServices;
using MeisterGeister.ViewModel;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class BusyProgressTests
    {
        private static void RunInSta(Action action)
        {
            Exception failure = null;
            var thread = new System.Threading.Thread(() =>
            {
                try { action(); }
                catch (Exception ex) { failure = ex; }
            });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(failure).Throw();
        }

        [Fact]
        public void ProductionBusyIndicatorIsVisibleOutsideHiddenMenuAndUpdatesSynchronously()
        {
            var directory = new System.IO.DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null && !System.IO.File.Exists(System.IO.Path.Combine(directory.FullName, "View", "MainView.xaml")))
                directory = directory.Parent;
            Assert.NotNull(directory);
            var document = System.Xml.Linq.XDocument.Load(System.IO.Path.Combine(directory.FullName, "View", "MainView.xaml"));
            System.Xml.Linq.XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";
            var indicator = System.Linq.Enumerable.Single(document.Descendants(), element => (string)element.Attribute(x + "Name") == "BusyStatusBorder");
            Assert.DoesNotContain(indicator.Ancestors(), element => (string)element.Attribute(x + "Name") == "Menue");

            RunInSta(() =>
            {
                var markup = "<Grid xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">" +
                    "<Grid.Resources><BooleanToVisibilityConverter x:Key=\"BooleanToVisibilityConverter1\" /></Grid.Resources>" +
                    indicator.ToString() + "</Grid>";
                var grid = (System.Windows.Controls.Grid)System.Windows.Markup.XamlReader.Parse(markup);
                var viewModel = CreateViewModel();
                var window = new System.Windows.Window { Width = 500, Height = 300, Content = grid, DataContext = viewModel, ShowActivated = false, ShowInTaskbar = false };
                try
                {
                    window.Show();
                    viewModel.SetBusyProgress(true, "Audio", 25, null, "25 von 100 Titeln überprüft");
                    MeisterGeister.View.ExtensionMethods.Refresh(window);
                    var border = (System.Windows.Controls.Border)grid.Children[0];
                    var panel = (System.Windows.Controls.StackPanel)border.Child;
                    var text = (System.Windows.Controls.TextBlock)panel.Children[0];
                    var bar = (System.Windows.Controls.ProgressBar)panel.Children[1];
                    var count = (System.Windows.Controls.TextBlock)panel.Children[2];
                    Assert.True(border.IsVisible);
                    Assert.True(bar.ActualWidth > 0);
                    Assert.Equal("Audio", text.Text);
                    Assert.Equal("25 von 100 Titeln überprüft", count.Text);
                    Assert.True(count.IsVisible);
                    Assert.Equal(25d, bar.Value);
                    Assert.False(bar.IsIndeterminate);

                    viewModel.SetBusyProgress(true, "Unterverzeichnisse werden durchsucht", 75, null, "75 von 100 Titeln überprüft");
                    MeisterGeister.View.ExtensionMethods.Refresh(window);
                    Assert.Equal(75d, bar.Value);
                    Assert.Equal("Unterverzeichnisse werden durchsucht", text.Text);
                    Assert.Equal("75 von 100 Titeln überprüft", count.Text);

                    viewModel.SetBusyProgress(true, "Audio", 0, null, "1 von 1000 Titeln überprüft");
                    MeisterGeister.View.ExtensionMethods.Refresh(window);
                    Assert.Equal(0d, bar.Value);
                    Assert.Equal("1 von 1000 Titeln überprüft", count.Text);

                    viewModel.SetBusyProgress(true, "Audio", 100, null, "0 von 0 Titeln überprüft");
                    MeisterGeister.View.ExtensionMethods.Refresh(window);
                    Assert.Equal("0 von 0 Titeln überprüft", count.Text);

                    viewModel.SetBusyProgress(true, "Laden", null);
                    MeisterGeister.View.ExtensionMethods.Refresh(window);
                    Assert.False(count.IsVisible);

                    viewModel.SetBusyProgress(false, null, null);
                    MeisterGeister.View.ExtensionMethods.Refresh(window);
                    Assert.False(border.IsVisible);
                }
                finally { window.Close(); }
            });
        }

        [Fact]
        public void RefreshProcessesBindingAndRenderBeforeReturningWithoutProcessingInput()
        {
            RunInSta(() =>
            {
                var element = new System.Windows.Controls.Grid();
                bool bindingUpdated = false;
                bool rendered = false;
                bool inputProcessed = false;
                element.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.DataBind, new Action(() => bindingUpdated = true));
                element.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() => rendered = true));
                element.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new Action(() => inputProcessed = true));

                MeisterGeister.View.ExtensionMethods.Refresh(element);

                Assert.True(bindingUpdated);
                Assert.True(rendered);
                Assert.False(inputProcessed);
            });
        }

        private readonly System.Collections.Generic.List<string> properties = new System.Collections.Generic.List<string>();

        private void RecordPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            properties.Add(args.PropertyName);
        }

        private static MainViewModel CreateViewModel()
        {
            var viewModel = (MainViewModel)RuntimeHelpers.GetUninitializedObject(typeof(MainViewModel));
            foreach (var name in new[] { "_propertyChanged", "_propertyChanging" })
            {
                var field = typeof(MeisterGeister.ViewModel.Base.ViewModelBase).GetField(name,
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                field.SetValue(viewModel, Activator.CreateInstance(field.FieldType));
            }
            return viewModel;
        }

        [Fact]
        public void BusyProgressSupportsDeterminateIndeterminateAndCompletionStates()
        {
            var viewModel = CreateViewModel();

            viewModel.SetBusyProgress(true, "Audio 25/100", 25);
            Assert.True(viewModel.IsBusyProgressVisible);
            Assert.False(viewModel.IsBusyProgressIndeterminate);
            Assert.Equal(25, viewModel.BusyProgress);
            Assert.Equal("Audio 25/100", viewModel.BusyProgressText);

            viewModel.SetBusyProgress(true, "Verzeichnisse werden gelesen", null);
            Assert.True(viewModel.IsBusyProgressVisible);
            Assert.True(viewModel.IsBusyProgressIndeterminate);
            Assert.Equal(0, viewModel.BusyProgress);
            Assert.Equal("Verzeichnisse werden gelesen", viewModel.BusyProgressText);

            viewModel.SetBusyProgress(true, "Fertig", 100);
            Assert.Equal(100, viewModel.BusyProgress);
            Assert.False(viewModel.IsBusyProgressIndeterminate);

            viewModel.SetBusyProgress(false, "Fertig", null);
            Assert.False(viewModel.IsBusyProgressVisible);
            Assert.Equal(0, viewModel.BusyProgress);
            Assert.Null(viewModel.BusyProgressText);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void InvalidProgressIsRejectedWithoutChangingState(int progress)
        {
            var viewModel = CreateViewModel();
            viewModel.SetBusyProgress(true, "Audio", 25);

            Assert.Throws<ArgumentOutOfRangeException>(() => viewModel.SetBusyProgress(true, "Invalid", progress));
            Assert.Throws<ArgumentOutOfRangeException>(() => Global.SetIsBusy(true, "Invalid", progress));
            Assert.Equal(25, viewModel.BusyProgress);
            Assert.Equal("Audio", viewModel.BusyProgressText);
            Assert.True(viewModel.IsBusyProgressVisible);
        }

        [Fact]
        public void BusyProgressNotifiesBindingsWithoutChangingQueueProgress()
        {
            var viewModel = CreateViewModel();
            viewModel.CurrentProgress = 42;
            viewModel.CurrentUserState = "Queue";
            viewModel.PropertyChanged += RecordPropertyChanged;

            viewModel.SetBusyProgress(true, "Audio", 25);

            Assert.Contains(nameof(MainViewModel.BusyProgress), properties);
            Assert.Contains(nameof(MainViewModel.BusyProgressText), properties);
            Assert.Contains(nameof(MainViewModel.IsBusyProgressVisible), properties);
            Assert.Equal(42, viewModel.CurrentProgress);
            Assert.Equal("Queue", viewModel.CurrentUserState);
        }

        [Fact]
        public void CancelCommandRequestsCancellationAndResetsAfterCompletion()
        {
            var viewModel = CreateViewModel();
            using (var cancellation = new System.Threading.CancellationTokenSource())
            {
                viewModel.SetBusyProgress(true, "Audio", 25, cancellation, "1 von 4 Titeln überprüft");
                Assert.True(viewModel.CancelBusyCommand.CanExecute(null));
                viewModel.CancelBusyCommand.Execute(null);
                Assert.True(cancellation.IsCancellationRequested);
                Assert.False(viewModel.CancelBusyCommand.CanExecute(null));
                Assert.Equal("Abbruch wird ausgeführt...", viewModel.BusyProgressText);
                Assert.Equal("1 von 4 Titeln überprüft", viewModel.BusyProgressCount);
                viewModel.SetBusyProgress(false, null, null);
                Assert.False(viewModel.IsBusyProgressVisible);
                Assert.False(viewModel.CanCancelBusy);
                Assert.Null(viewModel.BusyProgressCount);
            }
        }

        [Fact]
        public void ProductionCancelButtonRequestsCancellation()
        {
            var directory = new System.IO.DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null && !System.IO.File.Exists(System.IO.Path.Combine(directory.FullName, "View", "MainView.xaml")))
                directory = directory.Parent;
            Assert.NotNull(directory);
            var document = System.Xml.Linq.XDocument.Load(System.IO.Path.Combine(directory.FullName, "View", "MainView.xaml"));
            System.Xml.Linq.XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";
            var indicator = System.Linq.Enumerable.Single(document.Descendants(), element => (string)element.Attribute(x + "Name") == "BusyStatusBorder");
            var container = indicator.Parent;
            RunInSta(() =>
            {
                var markup = "<Grid xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">" +
                    "<Grid.Resources><BooleanToVisibilityConverter x:Key=\"BooleanToVisibilityConverter1\" /><SolidColorBrush x:Key=\"Background1\" Color=\"White\" /></Grid.Resources>" +
                    container.ToString() + "</Grid>";
                var grid = (System.Windows.Controls.Grid)System.Windows.Markup.XamlReader.Parse(markup);
                var viewModel = CreateViewModel();
                var window = new System.Windows.Window { Content = grid, DataContext = viewModel, ShowActivated = false, ShowInTaskbar = false };
                using (var cancellation = new System.Threading.CancellationTokenSource())
                {
                    try
                    {
                        window.Show();
                        viewModel.SetBusyProgress(true, "Audio", 25, cancellation);
                        MeisterGeister.View.ExtensionMethods.Refresh(window);
                        var panel = (System.Windows.Controls.Grid)grid.Children[0];
                        var button = (System.Windows.Controls.Button)panel.Children[1];
                        Assert.True(button.IsVisible);
                        Assert.True(button.Command.CanExecute(button.CommandParameter));
                        button.Command.Execute(button.CommandParameter);
                        Assert.True(cancellation.IsCancellationRequested);
                        Assert.False(viewModel.CanCancelBusy);
                    }
                    finally { window.Close(); }
                }
            });
        }
    }
}
#endif
