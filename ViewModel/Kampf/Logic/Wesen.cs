using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    [DataContract(IsReference=true)]
    public class Wesen
    {
        #region ModifikatorChanged Event
        public Wesen()
        {
            Modifikatoren.CollectionChanged += OnModifikatorenChanged;
            ModifikatorenChanged += DependsOnModifikator.OnModifikatorenChanged;
        }

        private event NotifyCollectionChangedEventHandler ModifikatorenChanged;
        private void OnModifikatorenChanged(object o, NotifyCollectionChangedEventArgs args)
        {
            if (ModifikatorenChanged != null)
                ModifikatorenChanged(this, args);
        }
        #endregion

        private ObservableCollection<Mod.IModifikator> _modifikatoren = new ObservableCollection<Mod.IModifikator>();
        public ObservableCollection<Mod.IModifikator> Modifikatoren
        {
            get { return _modifikatoren; }
        }

        public int GetModifikatorCount<T>()
            where T : Mod.IModifikator, new()
        {
            return Modifikatoren.Where(m => m is T).Count();
        }

        /// <returns>Veränderung der Modifikatorenanzahl</returns>
        public int SetModifikatorCount(Type t, int targetCount)
        {
            var name = ImpromptuInterface.InvokeMemberName.Create;
            return ImpromptuInterface.Impromptu.InvokeMember(this, name("SetModifikatorCount", new[] { t }), targetCount);
        }

        /// <returns>Veränderung der Modifikatorenanzahl</returns>
        public int SetModifikatorCount<T>(int targetCount)
            where T : Mod.IModifikator, new()
        {
            IList<Mod.IModifikator> mods = Modifikatoren.Where(m => m is T).ToList();
            int change = 0;
            if (targetCount < mods.Count)
            {
                for (int i = 0; mods.Count > 0 && i < mods.Count - targetCount; i++)
                {
                    change--;
                    Modifikatoren.Remove(mods[i]);
                }
            }
            else if (targetCount > mods.Count)
            {
                for (int i = mods.Count; i < targetCount; i++)
                {
                    change++;
                    Modifikatoren.Add(new T());
                }
            }
            return change;
        }
    }
}
