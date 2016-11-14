using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// http://stackoverflow.com/questions/4672867/can-i-use-a-different-template-for-the-selected-item-in-a-wpf-combobox-than-for
    /// </summary>
    public class ComboBoxTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedItemTemplate { get; set; }
        public DataTemplateSelector SelectedItemTemplateSelector { get; set; }
        public DataTemplate DropdownItemsTemplate { get; set; }
        public DataTemplateSelector DropdownItemsTemplateSelector { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var parent = container;
            while (parent != null && !(parent is ComboBoxItem) && !(parent is ComboBox))
                parent = VisualTreeHelper.GetParent(parent);
            var inDropDown = (parent is ComboBoxItem);
            return inDropDown
                ? DropdownItemsTemplate ??
                    ((DropdownItemsTemplateSelector != null)
                        ? DropdownItemsTemplateSelector.SelectTemplate(item, container)
                        : null)
                : SelectedItemTemplate ??
                    ((SelectedItemTemplateSelector != null)
                        ? SelectedItemTemplateSelector.SelectTemplate(item, container)
                        : null);
        }
    }

    public class ComboBoxTemplateSelectorExtension : MarkupExtension
    {
        public ComboBoxTemplateSelectorExtension() : base() { }

        public DataTemplate SelectedItemTemplate { get; set; }
        public DataTemplateSelector SelectedItemTemplateSelector { get; set; }
        public DataTemplate DropdownItemsTemplate { get; set; }
        public DataTemplateSelector DropdownItemsTemplateSelector { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ComboBoxTemplateSelector()
            {
                SelectedItemTemplate = SelectedItemTemplate,
                SelectedItemTemplateSelector = SelectedItemTemplateSelector,
                DropdownItemsTemplate = DropdownItemsTemplate,
                DropdownItemsTemplateSelector = DropdownItemsTemplateSelector
            };
        }
    }
}
