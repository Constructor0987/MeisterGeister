using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;

using System.Data.Objects;
using System.Data.EntityClient;
using System.Data.Objects.SqlClient;
using System.Xml;

using MeisterGeister.Model.Extensions;
using System.Data;
using ImpromptuInterface;

namespace MeisterGeister.Model.Service
{
    /// <summary>
    /// Context ohne Proxies. Damit man es serialisieren kann.
    /// </summary>
    public class SerializationService
    {
        private static DatabaseDSAEntities serializationContext = new DatabaseDSAEntities(ServiceBase.ConnectionString, true, true);

        public static DatabaseDSAEntities Context
        {
            get
            {
                return serializationContext;
            }
        }

        public SerializationService()
        {
            LoadAllUserData();
        }

        /// <summary>
        /// Lädt nur die usergenerierten Daten in den Cache des Contexts.
        /// </summary>
        private void LoadAllUserData()
        {
            var queries = new IQueryable<object>[] {
                from a in Context.Held select a,
                from a in Context.Held_Ausrüstung select a,
                from a in Context.Ausrüstung where !a.AusrüstungGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Waffe where !a.WaffeGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Fernkampfwaffe where !a.FernkampfwaffeGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Schild where !a.SchildGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Rüstung where !a.RüstungGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Held_Munition select a,
                from a in Context.Munition where !a.MunitionGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Zauberzeichen where !a.ZauberzeichenGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Held_Inventar select a,
                from a in Context.Inventar select a,
                from a in Context.Gegner where !a.GegnerGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Kampfregel where !a.KampfregelGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Gegner_Angriff where !a.GegnerGUID.StringConvert().StartsWith("00000000-0000-0000-000") select a,
                from a in Context.Gegner_Kampfregel where !a.GegnerGUID.StringConvert().StartsWith("00000000-0000-0000-000") || !a.KampfregelGUID.StringConvert().StartsWith("00000000-0000-0000-000")  select a,
                //später mit Guid:
                from a in Context.Held_Sonderfertigkeit select a,
                from a in Context.Sonderfertigkeit where a.SonderfertigkeitID > 1107 select a,
                from a in Context.Held_VorNachteil select a,
                from a in Context.VorNachteil where a.VorNachteilID > 361 select a,
                from a in Context.Held_Talent select a,
                //Talent nicht, da es da keine Möglichkeit gibt userdaten zu finden.
                from a in Context.Held_Zauber select a,
                from a in Context.Zauber where a.ZauberID > 343 select a,
            };
            IEnumerable<object> results;
            foreach (IQueryable<object> q in queries)
                results = q.ToList();
        }

        public virtual bool Insert<T>(T aModelObject) where T : class
        {
            try
            {
                Context.AddObject(typeof(T).Name, aModelObject);
                Save();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        public virtual bool Update<T>(T aModelObject) where T : class
        {
            try
            {
                Context.ApplyCurrentValues(typeof(T).Name, aModelObject);
                Save();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        public virtual bool Delete<T>(T aModelObject) where T : class
        {
            try
            {
                //Context.AttachTo(typeof(T).Name, aModelObject);
                Context.DeleteObject(aModelObject);
                Save();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        public virtual bool Attach<T>(T aModelObject) where T : class
        {
            try
            {
                Context.AttachTo(typeof(T).Name, aModelObject);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        public virtual bool InsertOrUpdate<T>(T aModelObject) where T : class
        {
            try
            {
                Context.AddOrAttachInstance(aModelObject, true);
                Save();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        //Block deaktiviert, weil die neue Funktion LoadAllUserdata da ist.
        ///// <summary>
        ///// Die HeldenListe nur mit den Zuordnungstabellen und dem Inventar - ohne Stammdaten.
        ///// </summary>
        //public List<Held> HeldenListe()
        //{
        //    return HeldenListe(false);
        //}

        ///// <summary>
        ///// Die HeldenListe, entweder nur mit den Zuordnungstabellen, oder auch mit allen Stammdaten.
        ///// </summary>
        //public List<Held> HeldenListe(bool mitAllenStammdaten)
        //{
        //    if(mitAllenStammdaten)
        //        return Context.GetObjectSet<Held>()
        //        .Include("Held_Talent")
        //        .Include("Held_Sonderfertigkeit")
        //        .Include("Held_VorNachteil")
        //        .Include("Held_Fernkampfwaffe")
        //        .Include("Held_Inventar")
        //        .Include("Held_Rüstung")
        //        .Include("Held_Schild")
        //        .Include("Held_Waffe")
        //        .Include("Held_Zauber")

        //        .Include("Held_Talent.Talent")
        //        .Include("Held_Sonderfertigkeit.Sonderfertigkeit")
        //        .Include("Held_VorNachteil.VorNachteil")
        //        .Include("Held_Fernkampfwaffe.Fernkampfwaffe")
        //        .Include("Held_Inventar.Inventar")
        //        .Include("Held_Rüstung.Rüstung")
        //        .Include("Held_Schild.Schild")
        //        .Include("Held_Waffe.Waffe")
        //        .Include("Held_Zauber.Zauber")

        //        .ToList<Held>();
        //    //nur die Konnektoren und das Inventar
        //    return Context.GetObjectSet<Held>()
        //        .Include("Held_Talent")
        //        .Include("Held_Sonderfertigkeit")
        //        .Include("Held_VorNachteil")
        //        .Include("Held_Fernkampfwaffe")
        //        .Include("Held_Inventar")
        //        .Include("Held_Rüstung")
        //        .Include("Held_Schild")
        //        .Include("Held_Waffe")
        //        .Include("Held_Zauber")

        //        .Include("Held_Inventar.Inventar")

        //        .ToList<Held>();
        //}

        public T New<T>() where T : class
        {
            return Context.CreateObject<T>();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public static T DeserializeObject<T>(Stream stream) where T : class
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, true, null, new ProxyDataContractResolver());
            return (T)serializer.ReadObject(stream);
        }

        public static T DeserializeObject<T>(string xml) where T : class
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, true, null, new ProxyDataContractResolver());
            XmlTextReader reader = new XmlTextReader(new StringReader(xml));
            return (T)serializer.ReadObject(reader);
        }

        public static T DeserializeObjectFromFile<T>(string fileName) where T : class
        {
            T obj = null;
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                obj = DeserializeObject<T>(stream);
                stream.Close();
            }
            return obj;
        }

        public static void SerializeObject<T>(Stream stream, T o) where T : class
        {
            DataContractSerializer serializer = new DataContractSerializer(o.GetType(), null, int.MaxValue, false, true, null, new ProxyDataContractResolver());
            serializer.WriteObject(stream, o);
        }

        public static string SerializeObject<T>(T o) where T : class
        {
            string xml = String.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                SerializeObject<T>(stream, o);
                long pos = stream.Position;
                stream.Position = 0;
                StreamReader r = new StreamReader(stream);
                xml = r.ReadToEnd();
                stream.Position = pos;
            }
            return xml;
        }

        /// <summary>
        /// Save to a file.
        /// </summary>
        public static void SerializeObject<T>(string fileName, T o) where T : class
        {
            using (Stream stream = File.Open(fileName, FileMode.Create))
            {
                SerializeObject<T>(stream, o);
                stream.Close();
            }
        }

        #region Held
        /// <summary>
        /// Aktualisert den Held und alle an ihn angehängten Zuordnungstabellen sowie das Inventar. Stammdaten bleiben unangestastet.
        /// Bisher nur additiv. Es wird momentan nichts gelöscht.
        /// </summary>
        public bool InsertOrUpdateHeld(Held held)
        {
            // Mögliche erweiterung der ObjectContextExtension mit einer weiteren Methode, die eine Filterbedigung auf die verknüpfte IEnumerable anwendet
            // So kann man z.B. einen Guid/ID-Filter realisieren um userdaten von stammdaten zu trennen bzw stammdaten nciht zu überschreiben
            try
            {
                Context.AttachObjectGraph(held,
                    h => h.Held_Inventar,
                    h => h.Held_Inventar.First().Inventar,
                    h => h.Held_Ausrüstung,
                    h => h.Held_Sonderfertigkeit,
                    h => h.Held_Talent,
                    h => h.Held_VorNachteil,
                    h => h.Held_Zauber
                );
                Save(); //TODO ??: Besser wäre ein check, ob was überschrieben wird und ein Aufruf, des Save aus dem UI
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        public void ExportHeld(Guid heldGuid, string pfad)
        {
            //Userdaten geladen
            Held held = Context.Held.Where(h => h.HeldGUID == heldGuid).First();
            if (held != null)
            {
                SerializeObject<Held>(pfad, held);
            }
        }

        /// <summary>
        /// Mit überschreiben.
        /// </summary>
        public Guid ImportHeld(string pfad)
        {
            //Userdaten geladen
            Held held = DeserializeObjectFromFile<Held>(pfad);
            if (held != null)
            {
                if(InsertOrUpdateHeld(held))
                    return held.HeldGUID;
            }
            return Guid.Empty;
        }
        #endregion

        #region Gegner
        /// <summary>
        /// Aktualisert den Held und alle an ihn angehängten Zuordnungstabellen sowie das Inventar. Stammdaten bleiben unangestastet.
        /// Bisher nur additiv. Es wird momentan nichts gelöscht.
        /// </summary>
        public bool InsertOrUpdateGegner(Gegner gegner)
        {
            // Mögliche erweiterung der ObjectContextExtension mit einer weiteren Methode, die eine Filterbedigung auf die verknüpfte IEnumerable anwendet
            // So kann man z.B. einen Guid/ID-Filter realisieren um userdaten von stammdaten zu trennen bzw stammdaten nciht zu überschreiben
            try
            {
                Context.AttachObjectGraph(gegner,
                    h => h.Gegner_Angriff,
                    h => h.Gegner_Kampfregel,
                    h => h.Gegner_Kampfregel.First().Kampfregel
                );
                Save();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        public void ExportGegner(Guid gegnerGuid, string pfad)
        {
            //Userdaten geladen
            Gegner gegner = Context.Gegner.Where(h => h.GegnerGUID == gegnerGuid).First();
            if (gegner != null)
            {
                SerializeObject<Gegner>(pfad, gegner);
            }
        }

        /// <summary>
        /// Mit überschreiben.
        /// </summary>
        public Guid ImportGegner(string pfad)
        {
            //Userdaten geladen
            Gegner gegner = DeserializeObjectFromFile<Gegner>(pfad);
            if (gegner != null)
            {
                if (InsertOrUpdateGegner(gegner))
                    return gegner.GegnerGUID;
            }
            return Guid.Empty;
        }
        #endregion
    }
}
