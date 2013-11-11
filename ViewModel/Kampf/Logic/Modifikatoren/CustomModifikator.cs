using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ImpromptuInterface;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    /// <summary>
    /// Factory für einen Modifikator, der sich dynamisch, wie andere Modifikatoren verhalten kann.
    /// </summary>
    public class CustomModifikatorFactory : IEnumerable<IDictionary<string,object>>, IDisposable
    {
        dynamic modifikatorObj;
        List<Type> types;
        IDictionary<string, object> modifikatorObjAsDictionary;
        Dictionary<string, string> auswirkungen;

        public CustomModifikatorFactory()
        {
            modifikatorObj = GetWorkingInstance();
            modifikatorObjAsDictionary = modifikatorObj as IDictionary<string, object>;
            types = new List<Type>();
            auswirkungen = new Dictionary<string, string>();
        }

        public void AddModifikator(Type modType)
        {
            if (!typeof(IModifikator).IsAssignableFrom(modType))
                throw new ArgumentException(String.Format("modType '{0}' ist kein IModifikator", modType));
            types.Add(modType);
            AddMembers(modType);
        }

        public void RemoveModifikator(Type modType)
        {
            if (!typeof(IModifikator).IsAssignableFrom(modType))
                throw new ArgumentException(String.Format("modType '{0}' ist kein IModifikator", modType));
            if (!types.Contains(modType))
                return;
            RemoveMembers(modType);
            types.Remove(modType);
        }

        public void SetModifikator(string methodName, string operatorString, int modWert)
        {
            //TODO JT: Talentname und Zaubername oder andere Filter müsste man noch erkennen und mitanzeigen
            auswirkungen[methodName] = methodName.Substring(5).Substring(0, methodName.Length - 8) + " " + operatorString + modWert;
            modifikatorObjAsDictionary[methodName] = GetApplyExpression(operatorString, modWert);
        }

        public IModifikator Finish()
        {
            if (Errors.Count > 0)
                throw new Exception("Es gibt noch Fehler");
            modifikatorObj.Auswirkung = String.Join(", ", auswirkungen.Values.ToArray());
            return Impromptu.ActLike<IModifikator>(modifikatorObj, types.ToArray());
        }

        public string Auswirkungen
        {
            get { return modifikatorObj.Auswirkungen; }
            set { modifikatorObj.Auswirkungen = value; }
        }

        public string Name
        {
            get { return modifikatorObj.Name; }
            set { modifikatorObj.Name = value; }
        }

        public string Literatur
        {
            get { return modifikatorObj.Literatur; }
            set { modifikatorObj.Literatur = value; }
        }

        public Dictionary<object, string> Errors
        {
            get
            {
                var d = new Dictionary<object, string>();
                foreach (var t in types)
                {
                    foreach (var m in t.GetMembers(BindingFlags.Instance))
                    {
                        if (m.Name.StartsWith("Apply") && m.Name.EndsWith("Mod"))
                            if (!(modifikatorObjAsDictionary[m.Name] is Func<int, int>))
                                d.Add(modifikatorObjAsDictionary[m.Name], "");
                        if(modifikatorObjAsDictionary[m.Name] == null)
                            d.Add(modifikatorObjAsDictionary[m.Name], String.Format("{0} ist Null.", m.Name));
                        Type vt = GetUnderlyingType(m);
                        if (vt == null || vt.IsAssignableFrom(modifikatorObjAsDictionary[m.Name].GetType()))
                            d.Add(modifikatorObjAsDictionary[m.Name], String.Format("{0}({1}) ist von einem anderen Typ als {2}.", m.Name, modifikatorObjAsDictionary[m.Name].GetType(), vt));
                    }
                }
                return d;
            }
        }
 
        static string[] iModifikatorMembers = new string[] { "Name", "Literatur", "Erstellt", "Auswirkung" };
        private static ExpandoObject GetWorkingInstance()
        {
            dynamic e = new ExpandoObject();
            e.Name = "Variabler Modifikator";
            e.Literatur = null as String;
            e.Erstellt = DateTime.Now;
            e.Auswirkung = String.Empty;
            return e;
        }

        public IDictionary<string, object> this[int i]
        {
            get
            {
                if (i >= types.Count)
                    //throw new IndexOutOfRangeException();
                    throw new ArgumentOutOfRangeException();
                return this[types[i]];
            }
        }

        private List<System.Collections.Specialized.INotifyCollectionChanged> objectWithRegisteredEvents = new List<System.Collections.Specialized.INotifyCollectionChanged>();
        public IDictionary<string, object> this[Type t]
        {
            get
            {
                if (!types.Contains(t))
                    //throw new IndexOutOfRangeException();
                    throw new ArgumentOutOfRangeException();
                var d = new ObservableDictionary<string, object>();
                //d.CollectionChanged
                d.CollectionChanged += d_CollectionChanged;
                objectWithRegisteredEvents.Add(d);
                foreach (var m in t.GetMembers(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (iModifikatorMembers.Contains(m.Name) || m.Name.StartsWith("get_") || m.Name.StartsWith("set_"))
                        continue;
                    if(m.Name.StartsWith("Apply") && m.Name.EndsWith("Mod"))
                        d.Add(m.Name, null);
                    else
                        d.Add(m.Name, modifikatorObjAsDictionary[m.Name]);
                }
                return d;
            }
        }

        void d_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                KeyValuePair<string, object> kvp = (KeyValuePair<string, object>)e.NewItems[0];
                if (!(kvp.Key.StartsWith("Apply") && kvp.Key.EndsWith("Mod")) && modifikatorObjAsDictionary.ContainsKey(kvp.Key))
                    modifikatorObjAsDictionary[kvp.Key] = kvp.Value;
            }
        }

        private void AddMembers(Type t)
        {
            foreach (var m in t.GetMembers(BindingFlags.Instance | BindingFlags.Public))
            {
                if (m.Name.StartsWith("get_") || m.Name.StartsWith("set_") || iModifikatorMembers.Contains(m.Name) || modifikatorObjAsDictionary.ContainsKey(m.Name))
                    continue;
                Type vt = GetUnderlyingType(m);
                if (vt == null)
                {
                    modifikatorObjAsDictionary[m.Name] = null;
                    if(m.Name.StartsWith("Apply") && m.Name.EndsWith("Mod"))
                        auswirkungen.Add(m.Name, null);
                }
                else
                    modifikatorObjAsDictionary[m.Name] = vt.GetDefaultValue();
            }
        }

        private Type GetUnderlyingType(MemberInfo m)
        {
            Type vt = null;
            switch (m.MemberType)
            {
                case MemberTypes.Field:
                    vt = ((FieldInfo)m).FieldType;
                    break;
                case MemberTypes.Property:
                    vt = ((PropertyInfo)m).PropertyType;
                    break;
                default:
                    break;
            }
            return vt;
        }

        private void RemoveMembers(Type t)
        {
            foreach (var m in t.GetMembers(System.Reflection.BindingFlags.Instance))
            {
                if (iModifikatorMembers.Contains(m.Name) || !modifikatorObjAsDictionary.ContainsKey(m.Name))
                    continue;
                if (m.Name.StartsWith("Apply") && m.Name.EndsWith("Mod"))
                    auswirkungen.Remove(m.Name);
                modifikatorObjAsDictionary.Remove(m.Name);
            }
        }

        private static Func<int, int> GetApplyExpression(string actionString, int modWert)
        {
            ParameterExpression wertParam = Expression.Parameter(typeof(int), "wert");
            ConstantExpression mod = Expression.Constant(modWert, typeof(int));
            Expression action = null;
            if (actionString == "+")
                action = BinaryExpression.Add(wertParam, mod);
            else if (actionString == "-")
                action = BinaryExpression.Subtract(wertParam, mod);
            else if (actionString == "/")
                action = Expression.Call(typeof(CustomModifikatorFactory), "Div", null, wertParam, mod);
            else if (actionString == "*")
                action = BinaryExpression.Multiply(wertParam, mod);
            else if (actionString == "=")
                action = mod;
            return Expression.Lambda<Func<int, int>>(action, new ParameterExpression[] { wertParam }).Compile();
        }

        private static int Div(int a, int b)
        {
            return (int)Math.Round((double)a / (double)b, MidpointRounding.AwayFromZero);
        }

        public int Count
        {
            get { return types.Count; }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~CustomModifikatorFactory()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            foreach (var o in objectWithRegisteredEvents)
                o.CollectionChanged -= d_CollectionChanged;
        }

        #region IEnumerable
        public IEnumerator<IDictionary<string, object>> GetEnumerator()
        {
            return new CustomModEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new CustomModEnumerator(this);
        }

        private class CustomModEnumerator : IEnumerator<IDictionary<string, object>>
        {
            CustomModifikatorFactory cf = null;
            int i = -1;

            public CustomModEnumerator(CustomModifikatorFactory cf)
            {
                this.cf = cf;
            }

            public IDictionary<string, object> Current
            {
                get { return cf[i]; }
            }

            public void Dispose()
            {
                cf = null;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return cf[i]; }
            }

            public bool MoveNext()
            {
                return ++i<cf.Count;
            }

            public void Reset()
            {
                i = -1;
            }
        }

        #endregion
    }
    
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            // Validate parameters.
            if (type == null) throw new ArgumentNullException("type");
            if (type == typeof(string))
                return String.Empty;

            // We want an Func<object> which returns the default.
            // Create that expression here.
            Expression<Func<object>> e = Expression.Lambda<Func<object>>(
                // Have to convert to object.
                Expression.Convert(
                // The default value, always get what the *code* tells us.
                    Expression.Default(type), typeof(object)
                )
            );

            // Compile and return the value.
            return e.Compile()();
        }
    }
}
