using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;

using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;
using System.Xml;
using System.Windows;

using MeisterGeister.Model.Extensions;
using System.Data.Entity;
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
            LoadHeldUserData();
            LoadGegnerUserData();
            LoadAudioUserData();
            LoadHUELampeColorUserData();
        }

        private bool heldLoaded = false;
        private void LoadHeldUserData()
        {
            if (heldLoaded)
                return;
            var queries = new IQueryable<object>[] {
                from a in Context.Held select a,
                from a in Context.Held_Pflanze select a,
                from a in Context.Held_Ausrüstung select a,
                from a in Context.Held_BFAusrüstung select a,
                from a in Context.Held_BFAusrüstung select a.Schild,
                from a in Context.Held_Waffe select a,
                from a in Context.Held_Fernkampfwaffe select a,
                from a in Context.Held_Rüstung select a,
                from a in Context.Ausrüstung where !a.AusrüstungGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Ausrüstung_Setting where !a.AusrüstungGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Waffe where !a.WaffeGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Fernkampfwaffe where !a.FernkampfwaffeGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Schild where !a.SchildGUID.StringConvert().StartsWith("00000000-0000-0000-") select a, //|| a.Held_BFAusrüstung.Count > 0
                from a in Context.Rüstung where !a.RüstungGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Held_Munition select a,
                from a in Context.Munition where !a.MunitionGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Zauberzeichen where !a.ZauberzeichenGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Zauberzeichen_Setting where !a.ZauberzeichenGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Held_Inventar select a,
                from a in Context.Inventar select a,
                from a in Context.GegnerBase where !a.GegnerBaseGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Kampfregel where !a.KampfregelGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.GegnerBase_Angriff where !a.GegnerBaseGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.GegnerBase_Kampfregel where !a.GegnerBaseGUID.StringConvert().StartsWith("00000000-0000-0000-") || !a.KampfregelGUID.StringConvert().StartsWith("00000000-0000-0000-")  select a,
                from a in Context.Held_Sonderfertigkeit select a,
                from a in Context.Sonderfertigkeit where !a.SonderfertigkeitGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Sonderfertigkeit_Setting where !a.SonderfertigkeitGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Held_VorNachteil select a,
                from a in Context.VorNachteil where !a.VorNachteilGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Held_Talent select a,
                from a in Context.Talent where !a.TalentGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Held_Zauber select a,
                from a in Context.Zauber where !a.ZauberGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Zauber_Setting where !a.ZauberGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
            };
            IEnumerable<object> results;
            foreach (IQueryable<object> q in queries)
                results = q.ToList();
            heldLoaded = true;
        }

        bool gegnerLoaded = false;
        private void LoadGegnerUserData()
        {
            if (gegnerLoaded)
                return;
            var queries = new IQueryable<object>[] {
                from a in Context.GegnerBase where !a.GegnerBaseGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.Kampfregel where !a.KampfregelGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.GegnerBase_Angriff where !a.GegnerBaseGUID.StringConvert().StartsWith("00000000-0000-0000-") select a,
                from a in Context.GegnerBase_Kampfregel where !a.GegnerBaseGUID.StringConvert().StartsWith("00000000-0000-0000-") || !a.KampfregelGUID.StringConvert().StartsWith("00000000-0000-0000-")  select a,
            };
            IEnumerable<object> results;
            foreach (IQueryable<object> q in queries)
                results = q.ToList();
            gegnerLoaded = true;
        }

        bool audioLoaded = false;
        private void LoadAudioUserData()
        {
            if (audioLoaded)
                return;
            var queries = new IQueryable<object>[] {
                from a in Context.Audio_Theme.Include("Audio_Theme1").Include("Audio_Playlist") select a,
                from a in Context.Audio_Playlist select a,
                from a in Context.Audio_Playlist_Titel select a,
                from a in Context.Audio_Titel select a,
            };
            IEnumerable<object> results;
            foreach (IQueryable<object> q in queries)
                results = q.ToList();
            audioLoaded = true;
        }


        bool hueLCLoaded = false;
        private void LoadHUELampeColorUserData()
        {
            if (hueLCLoaded)
                return;
            var queries = new IQueryable<object>[] {
                from a in Context.HUE_LampeColor select a,
                from a in Context.HUE_Szene select a
            };
            IEnumerable<object> results;
            foreach (IQueryable<object> q in queries)
                results = q.ToList();
            hueLCLoaded = true;
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
            return Context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave);
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
            using (Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
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
        public static void SerializeObject<T>(string fileName, T o, bool append = false) where T : class
        {
            using (Stream stream = File.Open(fileName, append?FileMode.Append:FileMode.Create))
            {
                SerializeObject<T>(stream, o);
                stream.Close();
            }
        }

        /// <summary>
        /// Save to a file (Append).
        /// </summary>
        public static void SerializeObjectExist<T>(string fileName, T o) where T : class
        {
            SerializeObject<T>(fileName, o, true);
        }
        #endregion

        #region ObjectState
        public virtual System.Data.Entity.EntityState GetEntityState(object entity)
        {
            return Context.ObjectStateManager.GetObjectStateEntry(entity).State;
        }

        public virtual IEnumerable<ObjectStateEntry> GetObjectStateEntriesWithEntityState<T>(System.Data.Entity.EntityState state)
        {
            return Context.ObjectStateManager.GetObjectStateEntries(state).Where(os => os.Entity is T);
        }

        public virtual IEnumerable<T> GetObjectsWithEntityState<T>(System.Data.Entity.EntityState state)
        {
            return Context.ObjectStateManager.GetObjectStateEntries(state).Where(os => os.Entity is T).Select(os => (T)os.Entity);
        }

        public virtual IDictionary<object, System.Data.Entity.EntityState> GetChangedEntities()
        {
            IEnumerable<ObjectStateEntry> objectStates = Context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified);
            IDictionary<object, System.Data.Entity.EntityState> ret = new Dictionary<object, EntityState>();
            foreach (ObjectStateEntry os in objectStates)
                ret.Add(os.Entity, os.State);
            return ret;
        }

        public virtual IDictionary<T, System.Data.Entity.EntityState> GetChangedEntities<T>() where T : class
        {
            IEnumerable<ObjectStateEntry> objectStates = Context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified).Where(os => os.Entity is T);
            IDictionary<T, System.Data.Entity.EntityState> ret = new Dictionary<T, EntityState>();
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
            LoadHeldUserData();
            // Mögliche Erweiterung der ObjectContextExtension mit einer weiteren Methode, die eine Filterbedigung auf die verknüpfte IEnumerable anwendet
            // So kann man z.B. einen Guid/ID-Filter realisieren, um Userdaten von Stammdaten zu trennen bzw Stammdaten nicht zu überschreiben
#if !DEBUG
            try
            {
#endif
                DeleteHeldData(held);
                Held output = Context.AttachObjectGraph(held,
                    h => h.Held_Pflanze,
                    h => h.Held_Pflanze.First().Pflanze,
                    h => h.Held_Munition,
                    h => h.Held_Inventar,
                    h => h.Held_Inventar.First().Inventar,
                    h => h.Held_Ausrüstung,
                    h => h.Held_Ausrüstung.First().Held_Fernkampfwaffe,
                    h => h.Held_Ausrüstung.First().Held_Fernkampfwaffe.Fernkampfwaffe.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_Fernkampfwaffe.Fernkampfwaffe.Ausrüstung.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_Fernkampfwaffe.Fernkampfwaffe.Ausrüstung.Ausrüstung_Setting.First().WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_Rüstung,
                    h => h.Held_Ausrüstung.First().Held_Rüstung.Rüstung.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_Rüstung.Rüstung.Ausrüstung.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_Rüstung.Rüstung.Ausrüstung.Ausrüstung_Setting.First().WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung,
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung.Held_Waffe,
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung.Held_Waffe.Waffe.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung.Held_Waffe.Waffe.Ausrüstung.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung.Held_Waffe.Waffe.Ausrüstung.Ausrüstung_Setting.First().WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung.Schild.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung.Schild.Ausrüstung.WithoutUpdate(),
                    h => h.Held_Ausrüstung.First().Held_BFAusrüstung.Schild.Ausrüstung.Ausrüstung_Setting.First().WithoutUpdate(),
                    h => h.Held_Sonderfertigkeit.First().AlwaysInsert(), //simply always insert
                    h => h.Held_Sonderfertigkeit.First().Sonderfertigkeit.WithoutUpdate(),
                    h => h.Held_Sonderfertigkeit.First().Sonderfertigkeit.Sonderfertigkeit_Setting.First().WithoutUpdate(),
                    h => h.Held_Talent,
                    h => h.Held_Talent.First().Talent.WithoutUpdate(),
                    h => h.Held_VorNachteil.First().AlwaysInsert(),
                    h => h.Held_VorNachteil.First().VorNachteil.WithoutUpdate(),
                    h => h.Held_Zauber,
                    h => h.Held_Zauber.First().Zauber.WithoutUpdate(),
                    h => h.Held_Zauber.First().Zauber.Zauber_Setting.First().WithoutUpdate()                    
                );
                Save(); //TODO ??: Besser wäre ein check, ob was überschrieben wird und ein Aufruf, des Save aus dem UI
                var newheld = Context.Held.Where(h => h.HeldGUID == output.HeldGUID).FirstOrDefault();
                //Manuelle Reperatur der Schilde
                foreach (var bfha in held.Held_Ausrüstung.Where(a => a.Held_BFAusrüstung != null).Select(a => a.Held_BFAusrüstung).Where(a => a.Schild != null))
                {
                    var ha = newheld.Held_Ausrüstung.Where(a => a.HeldAusrüstungGUID == bfha.HeldAusrüstungGUID).FirstOrDefault();
                    Context.LoadProperty(ha, "Held_BFAusrüstung");
                    if (ha != null && ha.Held_BFAusrüstung != null)
                    {
                        var schild = Context.Schild.Where(a => a.SchildGUID == bfha.Schild.SchildGUID).FirstOrDefault();
                        ha.Held_BFAusrüstung.Schild = schild;
                    }
                }
                Save();
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

        public Held GetHeld(Guid heldGuid)
        {
            LoadHeldUserData();
            return Context.Held.Where(h => h.HeldGUID == heldGuid).First();
        }

        public Guid CloneHeld(Guid heldGuid, Guid newGuid)
        {
            var held = GetHeld(heldGuid);
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
            var held = GetHeld(heldGuid);
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
                    if (string.IsNullOrEmpty(held.Regelsystem)) // falls keine Regeledition gesetzt, DSA 4.1 annehmen
                        held.Regelsystem = "DSA 4.1";
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
            LoadHeldUserData();
            if(h==null)
                return false;
            if (Context.Held.Where(a => a.HeldGUID == h.HeldGUID).Count() == 0)
                return true;
            List<object> toDelete = new List<object>();

            toDelete.AddRange(Context.Held_Ausrüstung.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Held_Pflanze.Where(a => a.HeldGUID == h.HeldGUID).AsEnumerable<object>());
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
        public bool InsertOrUpdateGegner(GegnerBase gegner)
        {
            LoadGegnerUserData();
            // Mögliche erweiterung der ObjectContextExtension mit einer weiteren Methode, die eine Filterbedigung auf die verknüpfte IEnumerable anwendet
            // So kann man z.B. einen Guid/ID-Filter realisieren um userdaten von stammdaten zu trennen bzw stammdaten nciht zu überschreiben
            try
            {
                DeleteGegnerData(gegner);
                Context.AttachObjectGraph(gegner,
                    h => h.GegnerBase_Angriff,
                    h => h.GegnerBase_Kampfregel,
                    h => h.GegnerBase_Kampfregel.First().Kampfregel
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

        public GegnerBase GetGegner(Guid gegnerGuid)
        {
            LoadGegnerUserData();
            return Context.GegnerBase.Where(h => h.GegnerBaseGUID == gegnerGuid).First();
        }

        public void ExportGegner(Guid gegnerGuid, string pfad)
        {
            var gegner = GetGegner(gegnerGuid);
            if (gegner != null)
            {
                SerializeObject<GegnerBase>(pfad, gegner);
            }
        }

        public Guid ImportGegner(string pfad)
        {
            return ImportGegner(pfad, Guid.Empty);
        }

        public Guid ImportGegner(string pfad, Guid newGuid)
        {
            //Userdaten geladen
            GegnerBase gegner = DeserializeObjectFromFile<GegnerBase>(pfad);
            if (newGuid != Guid.Empty)
                gegner = ReplaceGuid(gegner, newGuid);
            if (gegner != null)
            {
                if (InsertOrUpdateGegner(gegner))
                    return gegner.GegnerBaseGUID;
            }
            return Guid.Empty;
        }

        public Guid CloneGegner(Guid gegnerGuid, Guid newGuid)
        {
            var gegner = GetGegner(gegnerGuid);
            if (gegner != null)
            {
                GegnerBase newGegner = ReplaceGuid(gegner, newGuid);
                if (InsertOrUpdateGegner(newGegner))
                    return newGuid;
            }
            return Guid.Empty;
        }

        private static GegnerBase ReplaceGuid(GegnerBase gegner, Guid newGuid)
        {
            string xml = SerializeObject<GegnerBase>(gegner);
            xml = xml.Replace(String.Format("<GegnerBaseGUID>{0}</GegnerBaseGUID>", gegner.GegnerBaseGUID), String.Format("<GegnerBaseGUID>{0}</GegnerBaseGUID>", newGuid));
            GegnerBase newHeld = DeserializeObject<GegnerBase>(xml);
            return newHeld;
        }

        public bool DeleteGegnerData(GegnerBase a)
        {
            LoadGegnerUserData();
            if (a == null)
                return false;
            if (Context.GegnerBase.Where(h => h.GegnerBaseGUID == a.GegnerBaseGUID).Count() == 0)
                return true;
            List<object> toDelete = new List<object>();

            toDelete.AddRange(Context.GegnerBase_Angriff.Where(h => h.GegnerBaseGUID == a.GegnerBaseGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.GegnerBase_Kampfregel.Where(h => h.GegnerBaseGUID == a.GegnerBaseGUID).AsEnumerable<object>());

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

        #region Audio

        public bool InsertOrUpdateAudio(Audio_Playlist audio)
        {
            LoadAudioUserData();
#if !DEBUG
            try
            {
#endif
            
            DeleteAudioData(audio);            
            Audio_Playlist output = Context.AttachObjectGraph(audio,
                    h => h.Audio_Playlist_Titel,
                    h => h.Audio_Playlist_Titel.First().Audio_Titel
                );
            Save();
            return output != null;
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

        private void AddAllTitlesToDictionary(Audio_Theme theme, Dictionary<Guid, Audio_Titel> titles, List<Audio_Playlist_Titel> nullTitle)
        {
            foreach(var playlist in theme.Audio_Playlist)
            {
                foreach(var pt in playlist.Audio_Playlist_Titel)
                {
                    if (pt.Audio_Titel == null)
                        nullTitle.Add(pt);
                    else
                    {
                        if (!titles.ContainsKey(pt.Audio_Titel.Audio_TitelGUID))
                            titles.Add(pt.Audio_Titel.Audio_TitelGUID, pt.Audio_Titel);
                    }
                }
            }
            foreach(var subtheme in theme.Audio_Theme1)
            {
                AddAllTitlesToDictionary(subtheme, titles, nullTitle);
            }
        }

        public bool InsertOrUpdateAudio(Audio_Theme aTheme)
        {
            LoadAudioUserData();

            DeleteAudioData(aTheme);

            //Gather all Titles and null entries
            Dictionary<Guid, Audio_Titel> titles = new Dictionary<Guid, Audio_Titel>();
            List<Audio_Playlist_Titel> nullTitles = new List<Audio_Playlist_Titel>();
            AddAllTitlesToDictionary(aTheme, titles, nullTitles);
            //and repair them
            foreach(var title in nullTitles)
            {
                if (titles.ContainsKey(title.Audio_TitelGUID))
                    title.Audio_Titel = titles[title.Audio_TitelGUID];
                else
                {
                    //TODO Import Log oder so
                    System.Diagnostics.Debug.WriteLine(String.Format("Audio Titel {0} aus Playlist {1} konnte nicht gefunden werden und wird deshalb nicht importiert.", title.Audio_TitelGUID, title.Audio_Playlist.Name));
                    title.Audio_Playlist.Audio_Playlist_Titel.Remove(title);
                }
            }

            Audio_Theme output = Context.AttachObjectGraph(aTheme,
                    h => h.Audio_Playlist,
                    h => h.Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel,
                //Child-Ebene 1
                    h => h.Audio_Theme1,
                    h => h.Audio_Theme1.First().Audio_Playlist,
                    h => h.Audio_Theme1.First().Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel,
                //2
                    h => h.Audio_Theme1.First().Audio_Theme1,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel,
                //3
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel,
                //4
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel,
                //5
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel,
            
                //6
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel,
                //7
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist,
                    h => h.Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Theme1.First().Audio_Playlist.First().Audio_Playlist_Titel.First().Audio_Titel
            );

#if !DEBUG
            try
            {
#endif

            try
            {
                Save();
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException dbe)
            {
                throw;
            }
            catch(System.Data.Entity.Core.UpdateException ue)
            {
                //Context.GetEntityKeyFromPrimaryKey(ue.StateEntries[0].Entity);
                throw;
            }
#if !DEBUG
        }
            catch (Exception)
            {
                throw;
                //Debug.WriteLine(e.Message);
                //Debug.WriteLine(e.InnerException);
                //return false;
            }
#endif
            return output != null;
        }

        public Audio_Playlist GetAudioPlaylist(Guid audioPlaylistGuid)
        {
            LoadAudioUserData();
            return Context.Audio_Playlist.Where(h => h.Audio_PlaylistGUID == audioPlaylistGuid).FirstOrDefault();
        }

        public Audio_Theme GetAudioTheme(Guid audioThemeGuid)
        {
            LoadAudioUserData();
            return Context.Audio_Theme.Where(h => h.Audio_ThemeGUID == audioThemeGuid).FirstOrDefault();
        }

        public void ExportAudioPlaylist(Guid audioPlaylistGuid, string pfad)
        {
            //Userdaten geladen
            var o = GetAudioPlaylist(audioPlaylistGuid);
            if (o != null)
            {
                //keine Themes mitexportieren
                o.Audio_Theme.Clear();
                //keine anderen playlisten mitexportieren. Also alle anderen entfernen
                foreach (var pt in o.Audio_Playlist_Titel)
                    pt.Audio_Titel.Audio_Playlist_Titel = pt.Audio_Titel.Audio_Playlist_Titel.Where(apt => apt.Audio_PlaylistGUID == o.Audio_PlaylistGUID).ToList();

                SerializeObjectExist<Audio_Playlist>(pfad, o);
            }
        }


        public void ExportAudioTheme(Guid audioThemeGuid, string pfad)
        {
            //Userdaten geladen
            var o = GetAudioTheme(audioThemeGuid);
            if (o != null)
            {
                SerializeObjectExist<Audio_Theme>(pfad, o);
            }
        }


        public void ExportAudioTheme(Audio_Theme aTheme, string pfad)
        {
            if (aTheme != null)
                ExportAudioTheme(aTheme.Audio_ThemeGUID, pfad);
        }

        /// <summary>
        /// Importiert Playlisten und Themes aus einer XML-Datei. Bestehende Daten werden überschrieben.
        /// </summary>
        /// <param name="pfad">Xml-Datei</param>
        public Guid ImportAudio(string pfad, string soll)
        {
            string msgBoxText_ErrorAudioDaten = "Die ausgewählte Datei  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) +
                    "' beinhaltet keine kompletten Audio-Daten-Strukturen." + Environment.NewLine + 
                    "Falls es sich um einzelne Audio-Playlisten oder Audio-Themes handelt, benutzen Sie die Schaltflächen im jeweiligen Bereich";

            var typ = MeistergeisterFileType(pfad);
            // Falsche Datei ausgewählt Var1
            if (typ == "Audio_Playlist" && soll == "")
            {     
                MessageBox.Show(msgBoxText_ErrorAudioDaten, "Inkompatibler Inhalt der Datei", MessageBoxButton.OK, MessageBoxImage.Error);
                return Guid.Empty;
            }
            else
            if (typ == "Audio_Playlist")
            {
                if (typ != soll)
                {
                    string messageBoxText = "Die ausgewählte Datei  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + 
                        "' enthält eine Audio-Playlist ohne Audio-Theme Informationen."+Environment.NewLine + "Soll die Audio-Playlist trotzdem eingefügt werden?";
                    string caption = "Falscher Inhalt der Datei";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;

                    if (MessageBox.Show(messageBoxText, caption, button, icon) != MessageBoxResult.Yes)
                        return Guid.Empty;
                }
                var ap = DeserializeObjectFromFile<Audio_Playlist>(pfad);
                if (ap != null && InsertOrUpdateAudio(ap))
                {
                    return ap.Audio_PlaylistGUID;
                }
            }
            else 
            if (typ == "Audio_Theme")
            {
                var at = DeserializeObjectFromFile<Audio_Theme>(pfad);

                // Falsche Datei ausgewählt Var2
                if (soll == "" && at != null && at.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E"))
                {
                    MessageBox.Show(msgBoxText_ErrorAudioDaten, "Inkompatibler Inhalt der Datei", MessageBoxButton.OK, MessageBoxImage.Error);
                    return Guid.Empty;
                }
                else
                if (at != null && at.Audio_ThemeGUID == Guid.Parse("00000000-0000-0000-0000-00000000A11E") && soll == "")
                {
                    if(InsertOrUpdateAudio(at))
                        return at.Audio_ThemeGUID;
                }
                else
                if (at != null)
                {
                    if (typ != soll)
                    {
                        string messageBoxText = "Die ausgewählte Datei  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) +
                            "' enthält ein Audio-Theme; nicht nur Audio-Playlist Informationen." + Environment.NewLine + "Soll das Audio-Theme trotzdem eingefügt werden?";
                        string caption = "Falscher Inhalt der Datei";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Warning;

                        if (MessageBox.Show(messageBoxText, caption, button, icon) != MessageBoxResult.Yes)
                            return Guid.Empty;
                    }
                    if (InsertOrUpdateAudio(at))
                    {
                        return at.Audio_ThemeGUID;
                    }
                }
            }
            return Guid.Empty;
        }

        public bool DeleteAudioData(Audio_Playlist a)
        {
            LoadAudioUserData();
            if(a==null)
                return false;
            if (Context.Audio_Playlist.Where(h => h.Audio_PlaylistGUID == a.Audio_PlaylistGUID).Count() == 0)
                return true;
            List<object> toDelete = new List<object>();

            toDelete.AddRange(Context.Audio_Playlist_Titel.Where(h => h.Audio_PlaylistGUID == a.Audio_PlaylistGUID).AsEnumerable<object>());
            toDelete.AddRange(Context.Audio_Playlist_Titel.Where(h => h.Audio_PlaylistGUID == a.Audio_PlaylistGUID).AsEnumerable<object>());

            try
            {
                foreach (object o in toDelete)
                    Context.DeleteObject(o);
            }
            catch
            {
                return false;
            }

            //verwaiste Titel entfernen
            toDelete.Clear();
            toDelete.AddRange(Context.Audio_Titel.Where(h => h.Audio_Playlist_Titel.Count == 0).AsEnumerable<object>());
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
        
        public bool DeleteHUELampeColorData(HUE_LampeColor a)
        {
            LoadHUELampeColorUserData();
            if(a==null)
                return false;
            if (Context.HUE_LampeColor.Where(h => h.HUE_LampeColorGUID == a.HUE_LampeColorGUID).Count() == 0)
                return true;
            List<object> toDelete = new List<object>();

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

        public bool DeleteAudioData(Audio_Theme a)
        {
            LoadAudioUserData();
            if(a==null)
                return false;
            if (Context.Audio_Theme.Where(h => h.Audio_ThemeGUID == a.Audio_ThemeGUID).Count() == 0)
                return true;
            List<object> toDelete = new List<object>();

            //toDelete.AddRange(Context.Audio_Playlist_Titel.Where(h => h.Audio_PlaylistGUID == a.Audio_PlaylistGUID).AsEnumerable<object>());

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

        private static Audio_Playlist ReplaceGuid(Audio_Playlist audio, Guid newGuid)
        {
            string xml = SerializeObject<Audio_Playlist>(audio);
            xml = xml.Replace(String.Format("<Audio_PlaylistGUID>{0}</Audio_PlaylistGUID>", audio.Audio_PlaylistGUID), String.Format("<Audio_PlaylistGUID>{0}</Audio_PlaylistGUID>", newGuid));
            Audio_Playlist newAudio = DeserializeObject<Audio_Playlist>(xml);
            return newAudio;
        }
        #endregion

        public static string MeistergeisterFileType(string xmlFile)
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
                {
                    string typ = line.Substring(1, line.IndexOf(' '));
                    return typ.Trim();
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
                if (fs != null)
                    fs.Close();
            }
            return null;
        }

        public static bool IsMeistergeisterFile(string xmlFile, string typ = null)
        {
            var t = MeistergeisterFileType(xmlFile);
            if (t == null)
                return false;
            if (typ == null)
                return true;
            return t.Equals(typ);
        }

    }
}
