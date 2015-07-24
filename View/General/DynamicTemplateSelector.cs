using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Provides a means to specify DataTemplates to be selected from within WPF code
    /// </summary>
    [ContentProperty("Templates")]
    public class DynamicTemplateSelector : DataTemplateSelector
    {
        public TemplateCollection Templates { get; set; }

        /// <summary>
        /// Overriden base method to allow the selection of the correct DataTemplate
        /// </summary>
        /// <param name="item">The item for which the template should be retrieved</param>
        /// <param name="container">The object containing the current item</param>
        /// <returns>The <see cref="DataTemplate"/> to use when rendering the <paramref name="item"/></returns>
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            //This should ensure that the item we are getting is in fact capable of holding our property
            //before we attempt to retrieve it.
            if (!(container is UIElement))
                return base.SelectTemplate(item, container);

            //First, we gather all the templates associated with the current control through our dependency property
            if (Templates == null || Templates.Count == 0)
                return base.SelectTemplate(item, container);

            //Then we go through them checking if any of them match our criteria
            foreach (var template in Templates)
                //In this case, we are checking whether the type of the item
                //is the same as the type supported by our DataTemplate
                if (template.Value.IsInstanceOfType(item))
                    //And if it is, then we return that DataTemplate
                    return template.DataTemplate;

            //If all else fails, then we go back to using the default DataTemplate
            return base.SelectTemplate(item, container);
        }
    }

    /// <summary>
    /// Holds a collection of <see cref="Template"/> items
    /// for application as a control's DataTemplate.
    /// </summary>
    public class TemplateCollection : List<Template>
    {

    }

    /// <summary>
    /// Provides a link between a value and a <see cref="DataTemplate"/>
    /// for the <see cref="DynamicTemplateSelector"/>
    /// </summary>
    /// <remarks>
    /// In this case, our value is a <see cref="System.Type"/> which we are attempting to match
    /// to a <see cref="DataTemplate"/>
    /// </remarks>
    public class Template : DependencyObject
    {
        /// <summary>
        /// Provides the value used to match this <see cref="DataTemplate"/> to an item
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Type), typeof(Template));

        /// <summary>
        /// Provides the <see cref="DataTemplate"/> used to render items matching the <see cref="Value"/>
        /// </summary>
        public static readonly DependencyProperty DataTemplateProperty =
           DependencyProperty.Register("DataTemplate", typeof(DataTemplate), typeof(Template));

        /// <summary>
        /// Gets or Sets the value used to match this <see cref="DataTemplate"/> to an item
        /// </summary>
        public Type Value
        { get { return (Type)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }

        /// <summary>
        /// Gets or Sets the <see cref="DataTemplate"/> used to render items matching the <see cref="Value"/>
        /// </summary>
        public DataTemplate DataTemplate
        { get { return (DataTemplate)GetValue(DataTemplateProperty); } set { SetValue(DataTemplateProperty, value); } }
    }
}
