using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Eigene Usings
using VM = MeisterGeister.ViewModel;
using MeisterGeister.View.General;
//Weitere Usings
using System.Diagnostics;

namespace MeisterGeister.View.Schmiede          
{
    /// <summary>
    /// Interaktionslogik für SchmiedeView.xaml
    /// </summary>
    public partial class SchmiedeView : UserControl
    {
        public SchmiedeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.Schmiede.SchmiedeViewModel).LoadDaten();
            }
            catch (Exception) { }
        }

        private void ButtonSchmiede_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && (sender is Button || sender is MenuItem))
            {
                object tag = null;
                if (sender is Button)
                    tag = ((Button)sender).Tag;
                else if (sender is MenuItem)
                    tag = ((MenuItem)sender).Tag;
                if (tag != null && tag is string)
                    StarteTab(tag.ToString(), _tabControlSchmiede.SelectedIndex);
            }
        }

        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e != null && e.Source != null && e.Source is TabItem
                && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                TabItem item = (TabItem)(e.Source);
                DragDrop.DoDragDrop(item, item, DragDropEffects.All);
            }
        }

        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            if (e != null && e.Source != null
                && (e.Source is TabItem || e.Source is StackPanel || e.Source is Image || e.Source is TextBlock))
            {
                TabItem target = null;
                if (e.Source is TabItem)
                    target = (TabItem)(e.Source);
                else if (e.Source is StackPanel)
                    target = (TabItem)((StackPanel)e.Source).Parent;
                else if (e.Source is Image)
                    target = (TabItem)((StackPanel)((Image)e.Source).Parent).Parent;
                else if (e.Source is TextBlock)
                    target = (TabItem)((StackPanel)((TextBlock)e.Source).Parent).Parent;
                if (e.Data != null)
                {
                    TabItemControl source = (TabItemControl)(e.Data.GetData(typeof(TabItemControl)));
                    if (!source.Equals(target))
                    {
                        if (target != null && target.Parent != null && target.Parent is TabControl)
                        {
                            TabControl tab = (TabControl)(target.Parent);
                            int sourceIndex = tab.Items.IndexOf(source);
                            int targetIndex = tab.Items.IndexOf(target);
                            tab.Items.Remove(source);
                            tab.Items.Insert(targetIndex, source);
                            tab.SelectedItem = source;
                        }
                    }
                }
            }
        }

        private void StarteTab(string tabName, int position = -1)
        {
            // falls Schmiede-Name nicht vorhanden, Tab-Erzeugung abbrechen
            if (!Schmiede.SchmiedeListe.ContainsKey(tabName)) return;

            Schmiede t = Schmiede.SchmiedeListe[tabName];
            Control con = t.CreateSchmiedeView();


            // falls View nicht erzeugt werden konnte, abbrechen
            if (con == null) return;

            if (con != null)
            {
                TabItemControl tab = new TabItemControl(con, tabName, t.Icon);
                if (position < 0)
                {
                    _tabControlSchmiede.Items.Add(tab);
                    _tabControlSchmiede.SelectedIndex = 0;
                }
                else
                {
                    _tabControlSchmiede.Items.Insert(position + 1, tab);
                    _tabControlSchmiede.SelectedIndex = position + 1;
                }
            }
        }



    }

    public class Schmiede
    {
        #region //---- SCHMIEDELISTE -----
        static Schmiede()
        {
            SchmiedeListe = new Dictionary<string, Schmiede>();

            SchmiedeListe.Add("Nahkampfwaffe", new Schmiede()
            {
                Name = "Nahkampfwaffe",
                Icon = "/DSA%20MeisterGeister;component/Images/Icons/nahkampf_01.png",
                ViewType = typeof(View.Schmiede.SchmiedeNahkampfwaffeView)
            });
            SchmiedeListe.Add("Fernkampfwaffe", new Schmiede()
            {
                Name = "Fernkampfwaffe",
                Icon = "/DSA%20MeisterGeister;component/Images/Icons/fernkampf.png",
                ViewType = typeof(View.Schmiede.SchmiedeFernkampfwaffeView)
            });
            SchmiedeListe.Add("Schild", new Schmiede()
            {
                Name = "Schild",
                Icon = "/DSA%20MeisterGeister;component/Images/Icons/schild.png",
                ViewType = typeof(View.Schmiede.SchmiedeSchildView)
            });
        }

        /// <summary>
        /// Eine Liste mit allen Schmieden.
        /// </summary>
        public static Dictionary<string, Schmiede> SchmiedeListe { get; private set; }

        #endregion

        #region //---- METHODEN ----

        // Konstruktor private, damit Tools nur über das Dictionary abgefrufen werden können
        private Schmiede() { }

        public Control CreateSchmiedeView()
        {
            Control schmiedeControl = null;

            object schmiedeObject = Activator.CreateInstance(ViewType);
            if (schmiedeObject is Control)
                schmiedeControl = (Control)schmiedeObject;

            return schmiedeControl;
        }

        #endregion

        #region //---- EIGENSCHAFTEN ----

        /// <summary>
        /// Name der Schmiede.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Pfad des Schmiede-Icons.
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// Typ der Schmiede.
        /// </summary>
        public Type ViewType { get; private set; }
        
        #endregion
    }
}
