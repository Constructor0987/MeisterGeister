using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Base;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.Menu
{
    public class MenuItemViewModel : ViewModelBase
    {
        public MenuItemViewModel() : base()
        {
            Children = new ExtendedObservableCollection<MenuItemViewModel>(); //TODO Auch Seperators erlauben
        }
        public bool HasChildren { get { return Children.Count > 0; } }
        public string Icon { get; set; }
        public string Header { get; set; }
        public string Tooltip { get; set; }
        public System.Windows.Input.ICommand Command { get; set; }
        public ExtendedObservableCollection<MenuItemViewModel> Children { get; set; }
    }
}
