using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MeisterGeister.View.Menu.Logic
{
    public class ToolViewSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (!(item is ToolViewModelBase) || item == null)
                return base.SelectTemplate(item, container);
            Tool t = (item as ToolViewModelBase).Tool;
            var template = CreateTemplate(t.ViewModelType, t.ViewType);
            return template;
        }

        DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewModelType.Name, viewType.Name);

            Dictionary<string, Type> userTypes = new Dictionary<string,Type>();
            userTypes.Add("vm", viewModelType);
            userTypes.Add("v", viewType);
            var template = (DataTemplate)MeisterGeister.View.General.ViewHelper.ParseXAML(xaml, userTypes);

            return template;
        }
    }
}
