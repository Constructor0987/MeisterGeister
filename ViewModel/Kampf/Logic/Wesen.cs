using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    [DataContract(IsReference=true)]
    public class Wesen 
    {
        private List<Mod.IModifikator> _modifikatoren = new List<Mod.IModifikator>();
        public List<Mod.IModifikator> Modifikatoren
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
