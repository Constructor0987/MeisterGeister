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
        private static DatabaseDSAEntities serializationContext = null;

        public static DatabaseDSAEntities Context
        {
            get
            {
                return serializationContext;
            }
        }

        private static SerializationService instance = null;

        private SerializationService()
        {
            serializationContext = new DatabaseDSAEntities(ServiceBase.ConnectionString, true, true);
            LoadAllUserData();
        }

        public static SerializationService GetInstance()
        {
            return GetInstance(false);
        }

        public static SerializationService GetInstance(bool forceNew)
        {
            if (instance == null || forceNew)
                instance = new SerializationService();
            return instance;
        }

        public static void DestroyInstance()
        {
            if(serializationContext!=null)
                serializationContext.Dispose();
            serializationContext = null;
            instance = null;
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

        #region Insert/Update/Delete/Save/New/Discard
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

        public T New<T>() where T : class
        {
            return Context.CreateObject<T>();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public virtual void DiscardChanges()
        {
            Context.DiscardChanges();
        }
        #endregion

        #region De-/Serialize
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
        #endregion

        #region ObjectState
        public virtual System.Data.EntityState GetEntityState(object entity)
        {
            return Context.ObjectStateManager.GetObjectStateEntry(entity).State;
        }

        public virtual IEnumerable<ObjectStateEntry> GetObjectStateEntriesWithEntityState<T>(System.Data.EntityState state)
        {
            return Context.ObjectStateManager.GetObjectStateEntries(state).Where(os => os.Entity is T);
        }

        public virtual IEnumerable<T> GetObjectsWithEntityState<T>(System.Data.EntityState state)
        {
            return Context.ObjectStateManager.GetObjectStateEntries(state).Where(os => os.Entity is T).Select(os => (T)os.Entity);
        }

        public virtual IDictionary<object, System.Data.EntityState> GetChangedEntities()
        {
            IEnumerable<ObjectStateEntry> objectStates = Context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Modified);
            IDictionary<object, System.Data.EntityState> ret = new Dictionary<object, System.Data.EntityState>();
            foreach (ObjectStateEntry os in objectStates)
                ret.Add(os.Entity, os.State);
            return ret;
        }

        public virtual IDictionary<T, System.Data.EntityState> GetChangedEntities<T>() where T : class
        {
            IEnumerable<ObjectStateEntry> objectStates = Context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Modified).Where(os => os.Entity is T);
            IDictionary<T, System.Data.EntityState> ret = new Dictionary<T, System.Data.EntityState>();
            foreach (ObjectStateEntry os in objectStates)
                ret.Add(os.Entity as T, os.State);
            return ret;
        }
        #endregion

        #region Held
        /// <summary>
        /// Aktualisert den Held und alle an ihn angehängten Zuordnungstabellen sowie das Inventar. Stammdaten bleiben unangestastet.
        /// </summary>
        public bool InsertOrUpdateHeld(Held held)
        {
            // Mögliche Erweiterung der ObjectContextExtension mit einer weiteren Methode, die eine Filterbedigung auf die verknüpfte IEnumerable anwendet
            // So kann man z.B. einen Guid/ID-Filter realisieren, um Userdaten von Stammdaten zu trennen bzw Stammdaten nicht zu überschreiben
#if !DEBUG
            try
            {
#endif
                DeleteHeldData(held);
                Held output = Context.AttachObjectGraph(held,
                    h => h.Held_Inventar,
                    h => h.Held_Inventar.First().Inventar,
                    h => h.Held_Ausrüstung,
                    h => h.Held_Sonderfertigkeit,
                    h => h.Held_Talent,
                    h => h.Held_VorNachteil,
                    h => h.Held_Zauber
                );
                Save(); //TODO ??: Besser wäre ein check, ob was überschrieben wird und ein Aufruf, des Save aus dem UI
                return output!=null;
#if !DEBUG
        }
            catch (Exception e)
            {
                throw e;
                //Debug.WriteLine(e.Message);
                //Debug.WriteLine(e.InnerException);
                //return false;
            }
#endif
        }

        public Guid CloneHeld(Guid heldGuid)
        {
            return CloneHeld(heldGuid, Guid.NewGuid());
        }

        private static Held ReplaceGuid(Held held, Guid newGuid)
        {
            string xml = SerializeObject<Held>(held);
            xml = xml.Replace(String.Format("<HeldGUID>{0}</HeldGUID>", held.HeldGUID), String.Format("<HeldGUID>{0}</HeldGUID>", newGuid));
            Held newHeld = DeserializeObject<Held>(xml);
            return newHeld;
        }

        public Guid CloneHeld(Guid heldGuid, Guid newGuid)
        {
            Held held = Context.Held.Where(h => h.HeldGUID == heldGuid).First();
            if (held != null)
            {
                Held newHeld = ReplaceGuid(held, newGuid);
                if (InsertOrUpdateHeld(newHeld))
                    return newHeld.HeldGUID;
            }
            return Guid.Empty;
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
        /// Importiert einen Helden aus einer XML-Datei. Bestehende Daten werden überschrieben.
        /// </summary>
        /// <param name="pfad">Xml-Datei</param>
        public Guid ImportHeld(string pfad)
        {
            return ImportHeld(pfad, Guid.Empty);
        }
        
        /// <summary>
        /// Importiert einen Helden aus einer XML-Datei.
        /// </summary>
        /// <param name="pfad">Xml-Datei</param>
        /// <param name="newGuid">Wenn Guid.Empty, dann wird überschrieben, sonst wird eine Kopie mit der neuen Guid importiert.</param>
        /// <returns>Guid des importierten Helden oder Guid.Empty</returns>
        public Guid ImportHeld(string pfad, Guid newGuid)
        {
                //Userdaten geladen
                Held held = DeserializeObjectFromFile<Held>(pfad);;
                if (newGuid != Guid.Empty)
                    held = ReplaceGuid(held, newGuid);
                if (held != null)
                {
                    if (InsertOrUpdateHeld(held))
                        return held.HeldGUID;
                }
            return Guid.Empty;
        }

        /// <summary>
        /// Markiert alle an den Helden angehängten Informationen zum Löschen. Lediglich der Held selbst bleibt.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public bool DeleteHeldData(Held h)
        {
            if(h==null)
                return false;
            if (Context.Held.Where(a => a.HeldGUID == h.HeldGUID).Count() == 0)
                return true;
            List<object> toDelete = new List<object>();

            toDelete.AddRange(Context.Held_Ausrüstung.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Held_Inventar.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Held_Munition.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Held_Sonderfertigkeit.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Held_Talent.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Held_VorNachteil.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Held_Zauber.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());

            try
            {
                foreach (object o in toDelete)
                    Context.DeleteObject(o);
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Gegner
        /// <summary>
        /// Aktualisert den Gegner und alle an ihn angehängten Zuordnungstabellen. Stammdaten bleiben unangestastet.
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

        public Guid CloneGegner(Guid gegnerGuid, Guid newGuid)
        {
            Gegner gegner = Context.Gegner.Where(h => h.GegnerGUID == gegnerGuid).First();
            if (gegner != null)
            {
                string xml = SerializeObject<Gegner>(gegner);
                xml = xml.Replace(String.Format("<GegnerGUID>{0}</GegnerGUID>", gegnerGuid), String.Format("<GegnerGUID>{0}</GegnerGUID>", newGuid));
                Gegner newGegner = DeserializeObject<Gegner>(xml);
                if (InsertOrUpdateGegner(newGegner))
                    return newGuid;
            }
            return Guid.Empty;
        }
        #endregion

        public static bool IsMeistergeisterFile(string xmlFile)
        {
            System.IO.FileStream fs = null;
            System.IO.StreamReader sr = null;
            try
            {
                fs = new System.IO.FileStream(xmlFile, System.IO.FileMode.Open);
                sr = new System.IO.StreamReader(fs);
                char[] buffer = new char[400];
                sr.Read(buffer, 0, 400);
                string line = new string(buffer);
                if (line.Contains("MeisterGeister.Model"))
                    return true;
                
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
                if (fs != null)
                    fs.Close();
            }
            return false;
        }
    }
}
