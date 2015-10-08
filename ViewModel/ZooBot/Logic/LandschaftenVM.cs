using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Model;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.ZooBot.Logic
{
    public class LandschaftenVM : Base.ViewModelBase
    {
        Dictionary<Guid, LandschaftItem> landschaftItems = new Dictionary<Guid, LandschaftItem>();

        public LandschaftenVM()
        {
            //Landschaften laden
            Landschaftsgruppen = new List<LandschaftsgruppeItem>();
            foreach(var lg in Global.ContextHeld.Liste<Landschaftsgruppe>())
            {
                var lgi = new LandschaftsgruppeItem(lg, this);
                Landschaftsgruppen.Add(lgi);
                lgi.PropertyChanged += lgi_PropertyChanged;
            }
        }

        void lgi_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var lg = sender as LandschaftsgruppeItem;
            if (lg != null && e.PropertyName == "IsSelected")
                OnChanged("SelectedItems");
        }

        public Dictionary<Guid, LandschaftItem> LandschaftItems
        {
            get { return landschaftItems; }
            protected set { Set(ref landschaftItems, value); }
        }

        public IList<Landschaft> SelectedItems
        {
            get { return LandschaftItems.Values.Where(l => l.IsSelected).Select(l => l.Landschaft).ToList(); }
        }

        //Für die Anzeige
        public IList<LandschaftsgruppeItem> Landschaftsgruppen
        { get; set; }

    }

    public class LandschaftItem : Base.ViewModelBase
    {
        public LandschaftItem(Landschaft l)
        {
            Landschaft = l;
        }

        private Landschaft landschaft;
        public Landschaft Landschaft
        {
            get { return landschaft; }
            set { landschaft = value; }
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set { Set(ref isSelected, value); }
        }
    }

    public class LandschaftsgruppeItem : Base.ViewModelBase
    {
        public LandschaftsgruppeItem(Landschaftsgruppe g, LandschaftenVM parentVM)
        {
            Landschaftsgruppe = g;
            Landschaften = new ExtendedObservableCollection<LandschaftItem>();
            foreach(var l in g.Landschaft)
            {
                LandschaftItem li = null;
                if (parentVM.LandschaftItems.ContainsKey(l.LandschaftGUID))
                    li = parentVM.LandschaftItems[l.LandschaftGUID];
                else
                {
                    li = new LandschaftItem(l);
                    parentVM.LandschaftItems.Add(l.LandschaftGUID, li);
                }
                li.PropertyChanged += li_PropertyChanged;
                Landschaften.Add(li);
            }
        }

        bool ignoreChangedEvent = false;
        void li_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (ignoreChangedEvent)
                return;
            var li = sender as LandschaftItem;
            if(li != null && e.PropertyName == "IsSelected")
            {
                bool allSelected = li.IsSelected, noneSelected = !li.IsSelected;
                foreach(var l in Landschaften)
                {
                    if (l.IsSelected && !noneSelected)
                        noneSelected = false;
                    if (!l.IsSelected && allSelected)
                        allSelected = false;
                    if (!allSelected && !noneSelected)
                        break;
                }
                if (allSelected)
                    isSelected = true;
                else if (noneSelected)
                    isSelected = false;
                else
                    isSelected = null;
                //Immer auslösen, weil im Parent dann SelectedItems neu ausgewertet werden muss.
                OnChanged("IsSelected");
            }
        }

        private Landschaftsgruppe landschaftsgruppe;
        public Landschaftsgruppe Landschaftsgruppe
        {
            get { return landschaftsgruppe; }
            set { Set(ref landschaftsgruppe, value); }
        }

        ExtendedObservableCollection<LandschaftItem> landschaften;
        public ExtendedObservableCollection<LandschaftItem> Landschaften
        {
            get { return landschaften; }
            set { Set(ref landschaften, value); }
        }

        private bool? isSelected = false;
        public bool? IsSelected
        {
            get { return isSelected; }
            set {
                if(Set(ref isSelected, value) && value.HasValue)
                {
                    //disable event
                    ignoreChangedEvent = true;
                    foreach(var l in Landschaften)
                        l.IsSelected = value.Value;
                    //enable event
                    ignoreChangedEvent = false;
                }
            }
        }
    }
}
