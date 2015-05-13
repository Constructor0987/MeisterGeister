using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ImpromptuInterface;
using MeisterGeister.Logic.Extensions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
using Aq.ExpressionJsonSerializer;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    /// <summary>
    /// Interface für CustomModifikatoren aus der CustomModifikatorFactory.
    /// NICHT auf andere Klassen anwenden.
    /// </summary>
    public interface ICustomModifikator : IModifikator
    {
        //IDictionary<string, Expression<Func<int, int>>> Methoden;
        //List<string> Types;
    }

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

        public bool AddModifikator(Type modType)
        {
            if (!typeof(IModifikator).IsAssignableFrom(modType))
                throw new ArgumentException(String.Format("modType '{0}' ist kein IModifikator", modType));
            if (types.Contains(modType))
                return false;
            types.Add(modType);
            modifikatorObj.Types.Add(modType.FullName);
            AddMembers(modType);
            return true;
        }

        public bool RemoveModifikator(Type modType)
        {
            if (!typeof(IModifikator).IsAssignableFrom(modType))
                throw new ArgumentException(String.Format("modType '{0}' ist kein IModifikator", modType));
            if (!types.Contains(modType))
                return false;
            RemoveMembers(modType);
            types.Remove(modType);
            modifikatorObj.Types.Remove(modType.FullName);
            return true;
        }

        public void SetModifikator(string methodName, string operatorString, int modWert)
        {
            //Talentname und Zaubername oder andere Filter erkennen und mitanzeigen
            string propertyName = methodName.Substring(5).Substring(0, methodName.Length - 8); //Name des zu modifizierenden Wertes aus dem Typ ausgelesen.
            if (propertyName == "Talentprobe" && types.Contains(typeof(IModTalentprobe)) || 
                propertyName == "Talentwert" && types.Contains(typeof(IModTalentwert)) || 
                propertyName == "Zauberprobe" && types.Contains(typeof(IModZauberprobe)) || 
                propertyName == "Zauberwert" && types.Contains(typeof(IModZauberwert)))
            {
                ISet<string> filterSet = null;
                string probe = propertyName.EndsWith("probe") ? "-Probe" : "";
                string filterName = propertyName.Left(6) + "name"; //Talentname oder Zaubername
                if (modifikatorObjAsDictionary.ContainsKey(filterName))
                    filterSet = modifikatorObjAsDictionary[filterName] as ISet<string>;
                if (filterSet != null)
                {
                    auswirkungen[methodName] = "";
                    foreach(string talentOderZauberName in filterSet)
                        auswirkungen[methodName] += ((auswirkungen[methodName].Length>0)?", ": "") + talentOderZauberName + probe + " " + operatorString + modWert;
                }
                else
                    auswirkungen[methodName] = propertyName + " " + operatorString + modWert;
            }
            else
                auswirkungen[methodName] = propertyName + " " + operatorString + modWert;
            modifikatorObj.Methoden[methodName] = GetApplyExpression(operatorString, modWert);
        }

        public ICustomModifikator Finish()
        {
            if (Errors.Count > 0)
                throw new Exception("Es gibt noch Fehler");
            foreach(var name in modifikatorObj.Methoden.Keys)
                modifikatorObjAsDictionary[name] = modifikatorObj.Methoden[name].Compile();
            modifikatorObj.Auswirkung = String.Join(", ", auswirkungen.Values.ToArray());
            return Impromptu.ActLike<ICustomModifikator>(modifikatorObj, types.ToArray());
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
            e.Methoden = new Dictionary<string, Expression>();
            e.Types = new List<string>();
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
                    if (m.Name.StartsWith("Apply") && m.Name.EndsWith("Mod"))
                    {
                        auswirkungen.Add(m.Name, null);
                        modifikatorObj.Methoden.Add(m.Name, null);
                    }
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
                {
                    auswirkungen.Remove(m.Name);
                    modifikatorObj.Methoden.Remove(m.Name);
                }
                modifikatorObjAsDictionary.Remove(m.Name);
            }
        }

        private static Expression<Func<int, int>> GetApplyExpression(string actionString, int modWert)
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
            return Expression.Lambda<Func<int, int>>(action, new ParameterExpression[] { wertParam });
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

        #region De/Serialisierung
        /// <summary>
        /// Serialisiert einen ICustomModifikator zu JSON, was in der Datenbank abgelegt werden kann.
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static string Serialize(ICustomModifikator mod)
        {
            // Impromptu Proxy entfernen
            var obj = Impromptu.UndoActLike(mod);
            // Klon erstellen, um das original nicht zu verändern (ist protected also per Reflection)
            var obj2 = Impromptu.InvokeMember(obj, "MemberwiseClone");
            var d = obj2 as IDictionary<string, object>;
            // Delegat-Methoden löschen, weil diese nicht serialisiert werden können
            foreach(var key in d.Keys.Where(k => k.StartsWith("Apply") && k.EndsWith("Mod")).ToArray())
            {
                d[key] = null;
            }
            string s = null;
            //in JSON umwandeln - mit speziellem Expressionconverter
            s = Newtonsoft.Json.JsonConvert.SerializeObject(d, new ExpressionJsonConverter(Assembly.GetAssembly(typeof(ICustomModifikator))) );
            return s;
        }


        /// <summary>
        /// Deserialisiert einen ICustomModifikator aus entsprechendem JSON.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ICustomModifikator Deserialize(string str)
        {
            IDictionary<string, object> d = null;
            //Converter für Expressions
            ExpressionJsonConverter converter =  new ExpressionJsonConverter(Assembly.GetAssembly(typeof(ICustomModifikator)) );
            //Deserialize als IDictionary
            d = (IDictionary<string, object>)Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(IDictionary<string, object>), converter);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(converter);
            //Manuelles Umwandeln von nicht korrekt umgesetzten Eigenschaften
            d["Methoden"] = ((JObject)d["Methoden"]).ToObject<IDictionary<string, Expression<Func<int, int>>>>(serializer);
            d["Types"] = ((JArray)d["Types"]).ToObject<List<string>>(serializer);
            string[] filternamen = new string[] { "Zaubername", "Talentname" };
            foreach(var filtername in filternamen)
            if(d.ContainsKey(filtername))
                d[filtername] = ((JArray)d[filtername]).ToObject<SortedSet<string>>(serializer);
            //Kompilieren der Delegat-Methoden aus den serialisierten Expressions
            var methoden = (IDictionary<string, Expression<Func<int, int>>>)d["Methoden"];
            foreach(string key in methoden.Keys)
            {
                if(d.ContainsKey(key))
                    d[key] = methoden[key].Compile();
                else
                    d.Add(key, methoden[key].Compile());
            }
            //Mit Impromptu wieder die gespeicherten Modifikator-Interfaces anwenden
            Type[] types = ((List<string>)d["Types"]).Select(t => Type.GetType(t)).ToArray();
            return Impromptu.ActLike<ICustomModifikator>(d.ToExpando(), types);
        }
        #endregion

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

        /// <summary>
        /// Extension method that turns a dictionary of string and object to an ExpandoObject
        /// </summary>
        public static ExpandoObject ToExpando(this IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;

            // go through the items in the dictionary and copy over the key value pairs)
            foreach (var kvp in dictionary)
            {
                // if the value can also be turned into an ExpandoObject, then do it!
                if (kvp.Value is IDictionary<string, object>)
                {
                    var expandoValue = ((IDictionary<string, object>)kvp.Value).ToExpando();
                    expandoDic.Add(kvp.Key, expandoValue);
                }
                else if (kvp.Value is System.Collections.ICollection)
                {
                    // iterate through the collection and convert any string-object dictionaries
                    // along the way into expando objects
                    var itemList = new List<object>();
                    foreach (var item in (System.Collections.ICollection)kvp.Value)
                    {
                        if (item is IDictionary<string, object>)
                        {
                            var expandoItem = ((IDictionary<string, object>)item).ToExpando();
                            itemList.Add(expandoItem);
                        }
                        else
                        {
                            itemList.Add(item);
                        }
                    }
                    expandoDic.Add(kvp.Key, itemList);
                }
                else
                {
                    expandoDic.Add(kvp);
                }
            }
            return expando;
        }
    }

}
