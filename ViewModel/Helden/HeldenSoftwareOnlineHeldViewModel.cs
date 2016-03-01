using MeisterGeister.Logic.HeldenImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Helden
{
    public class HeldenSoftwareOnlineHeldViewModel : Base.ViewModelBase
    {
        public int HeldenId { get; set; }
        public string HeldenKey { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                    Set(ref _name, value);
            }
        }

        public string HeldLastChange { get; set; }

        private bool _isLoaded;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                if (_isLoaded != value)
                    Set(ref _isLoaded, value);
            }
        }

        public HeldenSoftwareOnlineHeldViewModel(HeldElement heldElement)
        {
            Name = heldElement.Name;
            HeldenKey = heldElement.HeldenKey;
            HeldenId = heldElement.HeldenId;
            HeldLastChange = heldElement.HeldLastChange;
            IsLoaded = false;
        }
    }
}
