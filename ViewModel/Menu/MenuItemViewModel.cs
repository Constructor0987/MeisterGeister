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
        public virtual bool HasChildren { get { return Children.Count > 0; } }
        public virtual string Icon { get; set; }
        public virtual string Header { get; set; }
        public virtual string ToolTip { get; set; }
        public virtual System.Windows.Input.ICommand Command { get; set; }
        public virtual ExtendedObservableCollection<MenuItemViewModel> Children { get; set; }
    }
}
