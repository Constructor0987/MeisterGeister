using System.Windows;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public static class VisualTreeExtensions
    {
        public static T FindAnchestor<T>(this DependencyObject current)
        where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
    }
}
