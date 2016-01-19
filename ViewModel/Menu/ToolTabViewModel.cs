using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Base;
using System.Windows.Controls;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Menu
{
    public class ToolTabViewModel : ViewModelBase
    {
        public ToolTabViewModel(Tool t)
        {
            if (t == null)
                throw new ArgumentNullException("t");
            Tool = t;
            LogInfo log = Logger.PerformanceLogStart(string.Format("Init Tab {0}", t.Name));
            //View = t.CreateToolView();
            //TODO Create ViewmModel
            Logger.PerformanceLogEnd(log);
        }

        public Base.ToolViewModelBase ViewModel { get; protected set; }
        public Tool Tool { get; protected set; }

        //TODO Wie kommt das View ins TabControl? Mit DataTemplates? Dann müsste hier das VM rein anstelle des Views.
        // Hah: mit einem ContentTempalte Selector: https://jacobaloysious.wordpress.com/2013/08/19/mvvm-using-contenttemplateselector-in-tab-control-view/
        // Dann kann aus dem ViewModel abgeleitet werden welches View benutzt werden muss.
        // Das ToolViewModel kann dann das gewünschte View angeben.

        bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { Set(ref isSelected, value); }
        }
    }
}
