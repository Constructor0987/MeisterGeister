using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Controls;
//Eigene usings
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister.ViewModel.Schmiede
{
    public class SchmiedeViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        ObservableCollection<Base.ToolViewModelBase> _schmieden;
        private Base.ToolViewModelBase _selectedSchmiede;

        #endregion

        #region //---- COMMANDS ----
        Base.CommandBase _addSchmiedeCommand;
        
        public Base.CommandBase AddSchmiedeCommand
        {
            get {
                if (_addSchmiedeCommand == null)
                    _addSchmiedeCommand = new Base.CommandBase(AddSchmiede, null);
                return _addSchmiedeCommand; 
            }
        }

        void AddSchmiede(object sender)
        {
            if (sender != null && (sender is Type) && (sender as Type).IsSubclassOf(typeof(Base.ToolViewModelBase)))
            {
                Base.ToolViewModelBase schmiede = Activator.CreateInstance((Type)sender) as Base.ToolViewModelBase;
                if (schmiede != null)
                {
                    this.Schmieden.Add(schmiede);
                    SelectedSchmiede = schmiede;
                }
            }
        }
        #endregion

        #region //---- EIGENSCHAFTEN ----

        public ObservableCollection<Base.ToolViewModelBase> Schmieden
        {
            get { return _schmieden; }
        }

        public Base.ToolViewModelBase SelectedSchmiede
        {
            get { return _selectedSchmiede; }
            set
            {
                if (!Object.Equals(_selectedSchmiede, value))
                {
                    _selectedSchmiede = value;
                    OnChanged("SelectedSchmiede");
                }
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public SchmiedeViewModel()
        {
            _schmieden = new ObservableCollection<Base.ToolViewModelBase>();
            _schmieden.CollectionChanged += this.SchmiedenChanged;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----
     
        public void LoadDaten()
        {

        }

        public void Refresh()
        {
            // derzeit nichts beim erneuten Anzeigen der Tabs erforderlich
        }

        #endregion

        #region //---- EVENTS ----
        void SchmiedenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (Base.ToolViewModelBase schmiede in e.NewItems)
                    schmiede.RequestClose += this.SchmiedeRequestClose;
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (Base.ToolViewModelBase schmiede in e.OldItems)
                    schmiede.RequestClose -= this.SchmiedeRequestClose;
        }

        void SchmiedeRequestClose(object sender, EventArgs e)
        {
            Base.ToolViewModelBase schmiede = sender as Base.ToolViewModelBase;
            this.Schmieden.Remove(schmiede);
        }

        #endregion
    }
}
