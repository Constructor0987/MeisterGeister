using System;
using System.ComponentModel;
using ImpromptuInterface;
using System.Reflection;
using System.Collections.Generic;

namespace MeisterGeister.Model.Extensions
{
    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class DependentProperty : System.Attribute
    {
        private string _name;
        public DependentProperty(string propertyName)
        {
            _name = propertyName;
        }
        public string PropertyName
        {
            get { return _name; }
        }

        private static Dictionary<string, List<string>> cache = new Dictionary<string, List<string>>();

        public static void PropagateINotifyProperyChanged(object o, PropertyChangedEventArgs args)
        {
            if (!(o is INotifyPropertyChanged))
                return;
            if (!cache.ContainsKey(args.PropertyName))
            {
                List<string> methodnames = new List<string>();
                foreach (PropertyInfo pi in o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(pi, typeof(DependentProperty)))
                    {
                        DependentProperty dp = attr as DependentProperty;
                        if (dp != null && dp.PropertyName == args.PropertyName)
                        {
                            try
                            {
                                Impromptu.InvokeMemberAction(o, "NotifyPropertyChanged", pi.Name);
                                //Impromptu.InvokeMemberAction(o, "PropertyChanged", o, new PropertyChangedEventArgs(pi.Name)); //geht so leider nicht, das wäre noch eleganter
                                methodnames.Add(pi.Name);
                            }
                            catch (Exception e) { 
                                System.Diagnostics.Debug.WriteLine(e.Message);
                                System.Diagnostics.Debug.WriteLine(e.InnerException);
                            }
                        }
                    }
                }
                cache.Add(args.PropertyName, methodnames);
            }
            else
            {
                foreach(string method in cache[args.PropertyName])
                    Impromptu.InvokeMemberAction(o, "NotifyPropertyChanged", method);
            }
        }
    }
}
