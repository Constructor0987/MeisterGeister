using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using System.ComponentModel;
using System.Reflection;
using ImpromptuInterface;
using System.Collections.Specialized;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class DependsOnModifikator : System.Attribute
    {
        public DependsOnModifikator(Type modType)
        {
            if (!typeof(IModifikator).IsAssignableFrom(modType))
                throw new ArgumentException("modType must be a subclass of IModifikator.");
            Type = modType;
        }

        public Type Type
        {
            get;
            private set;
        }

        private static Dictionary<Type, Dictionary<Type, List<string>>> cache = new Dictionary<Type, Dictionary<Type, List<string>>>();

        public static void OnModifikatorenChanged(object o, NotifyCollectionChangedEventArgs args)
        {
            if (!(o is INotifyPropertyChanged))
                return;
            IList<Type> changed = new List<Type>();
            if (args.NewItems != null)
                foreach (object m in args.NewItems)
                {
                    if(!changed.Contains(m.GetType()))
                        changed.Add(m.GetType());
                }
            if (args.OldItems != null)
                foreach (object m in args.OldItems)
                {
                    if (!changed.Contains(m.GetType()))
                        changed.Add(m.GetType());
                }
            foreach (Type t in changed)
            {
                if (!cache.ContainsKey(t) || !cache[t].ContainsKey(o.GetType()))
                {
                    List<string> methodnames = new List<string>();
                    foreach (PropertyInfo pi in o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        foreach (Attribute attr in Attribute.GetCustomAttributes(pi, typeof(DependsOnModifikator)))
                        {
                            DependsOnModifikator dp = attr as DependsOnModifikator;
                            if (dp != null && dp.Type.IsAssignableFrom(t))
                            {
                                try
                                {
                                    Impromptu.InvokeMemberAction(o, "OnChanged", pi.Name);
                                    //Impromptu.InvokeMemberAction(o, "PropertyChanged", o, new PropertyChangedEventArgs(pi.Name)); //geht so leider nicht, das wäre noch eleganter
                                    methodnames.Add(pi.Name);
                                }
                                catch (Exception e)
                                {
                                    System.Diagnostics.Debug.WriteLine(e.Message);
                                    System.Diagnostics.Debug.WriteLine(e.InnerException);
                                }
                            }
                        }
                    }
                    if (!cache.ContainsKey(t))
                        cache.Add(t, new Dictionary<Type, List<string>>());
                    cache[t].Add(o.GetType(), methodnames);
                }
                else
                {
                    foreach (string method in cache[t][o.GetType()])
                        Impromptu.InvokeMemberAction(o, "OnChanged", method);
                }
            }
        }
    }
}
