using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Kampf.Logic;

using System.ComponentModel;

namespace MeisterGeister.ViewModel.Kampf
{
    public class GegnerViewModel : Base.ViewModelBase
    {
        public GegnerViewModel(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, string[], string> chooseFile, Action<string, Exception> showError) : 
            base(popup, confirm, confirmYesNoCancel, chooseFile, showError)
        {
            LoadDaten();
        }

        public void LoadDaten()
        {
            Guid tmp = (SelectedGegnerBase == null) ? Guid.Empty : SelectedGegnerBase.GegnerBaseGUID;
            SelectedGegnerBase = null;
            if (Global.ContextHeld != null)
            {
                GegnerBaseListe = Global.ContextHeld.Liste<GegnerBase>().OrderBy(h => h.Name).ToList();
                if (tmp != Guid.Empty)
                    SelectedGegnerBase = GegnerBaseListe.Where(h => h.GegnerBaseGUID == tmp).FirstOrDefault();
            }
        }

        #region Bindable Properties

        private GegnerBase selectedGegnerBase;
        public GegnerBase SelectedGegnerBase
        {
            get { return selectedGegnerBase; }
            set {
                SaveGegner();
                selectedGegnerBase = value;
                OnChanged("SelectedGegnerBase");
                OnChanged("AngriffListe");
            }
        }

        private List<GegnerBase> gegnerBaseListe;
        public List<GegnerBase> GegnerBaseListe
        {
            get { return gegnerBaseListe; }
            set
            {
                gegnerBaseListe = value;
                OnChanged("GegnerBaseListe");
            }
        }

        private Kampfregel selectedKampfregel;
        public Kampfregel SelectedKampfregel
        {
            get { return selectedKampfregel; }
            set
            {
                selectedKampfregel = value;
                OnChanged("SelectedKampfregel");
            }
        }

        private List<GegnerBase> kampfregelListe;
        public List<GegnerBase> KampfregelListe
        {
            get { return kampfregelListe; }
            set
            {
                kampfregelListe = value;
                OnChanged("KampfregelListe");
            }
        }

        private Kampfregel selectedAddKampfregel;
        public Kampfregel SelectedAddKampfregel
        {
            get { return selectedAddKampfregel; }
            set { 
                selectedAddKampfregel = value;
                OnChanged("SelectedAddKampfregel");
            }
        }

        private GegnerBase_Kampfregel selectedGegnerKampfregel;
        public GegnerBase_Kampfregel SelectedGegnerKampfregel
        {
            get { return selectedGegnerKampfregel; }
            set { selectedGegnerKampfregel = value; OnChanged("SelectedGegnerKampfregel"); }
        }

        public List<GegnerBase_Angriff> AngriffListe
        {
            get { return (selectedGegnerBase==null)?null:selectedGegnerBase.GegnerBase_Angriff.OrderBy(ga => ga.Name).ToList(); }
        }

        private GegnerBase_Angriff selectedAngriff;
        public GegnerBase_Angriff SelectedAngriff
        {
            get { return selectedAngriff; }
            set { 
                selectedAngriff = value;
                OnChanged("SelectedAngriff");
            }
        }

        private string angriffAddName;
        public string AngriffAddName
        {
            get { return angriffAddName; }
            set { angriffAddName = value; }
        }

        #endregion

        #region Gegner_Angriff Add and Delete Commands
        private Base.CommandBase onNewAngriff = null;
        public Base.CommandBase OnNewAngriff
        {
            get
            {
                if (onNewAngriff == null)
                    onNewAngriff = new Base.CommandBase(NewAngriff, null);
                return onNewAngriff;
            }
        }

        private void NewAngriff(object sender)
        {
            if (SelectedGegnerBase == null)
                return;
            if (AngriffAddName == null || AngriffAddName == String.Empty || SelectedGegnerBase.GegnerBase_Angriff.Where(g => g.Name == AngriffAddName).Count() > 0)
            {
                PopUp("Der Name für einen neuen Angriff darf weder leer noch bereits verwendet sein.");
                return;
            }
            GegnerBase_Angriff ga = Global.ContextHeld.New<GegnerBase_Angriff>();
            ga.Name = AngriffAddName;
            ga.GegnerBaseGUID = SelectedGegnerBase.GegnerBaseGUID;
            ga.GegnerBase = SelectedGegnerBase;
            SelectedGegnerBase.GegnerBase_Angriff.Add(ga);
            SaveGegner();
            //Aktualisieren
            //OnChanged("SelectedGegnerBase");
            OnChanged("AngriffListe");
        }

        private Base.CommandBase onDeleteAngriff = null;
        public Base.CommandBase OnDeleteAngriff
        {
            get
            {
                if (onDeleteAngriff == null)
                    onDeleteAngriff = new Base.CommandBase(DeleteAngriff, null);
                return onDeleteAngriff;
            }
        }

        private void DeleteAngriff(object sender)
        {
            GegnerBase_Angriff h = SelectedAngriff;
            if (h != null && SelectedGegnerBase != null)
            {
                if (Confirm("Angriff löschen", string.Format("Sind Sie sicher, dass Sie den Angriff '{0}' löschen möchten?", h.Name))
                    )//&& Global.ContextHeld.Delete<GegnerBase_Angriff>(h))
                {
                    SelectedGegnerBase.GegnerBase_Angriff.Remove(h);
                    SaveGegner();
                    //OnChanged("SelectedGegnerBase");
                    OnChanged("AngriffListe");
                }
            }
        }
        #endregion

        #region Kampfregel Commands

        private Base.CommandBase onNewKampfregel = null;
        public Base.CommandBase OnNewKampfregel
        {
            get
            {
                if (onNewKampfregel == null)
                    onNewKampfregel = new Base.CommandBase(NewKampfregel, null);
                return onNewKampfregel;
            }
        }

        private void NewKampfregel(object sender)
        {
            Kampfregel kr = Global.ContextHeld.New<Kampfregel>();
            Global.ContextHeld.Insert<Kampfregel>(kr);
            //Aktualisieren
            OnChanged("KampfregelListe");
            SelectedKampfregel = kr;
        }

        private Base.CommandBase onAddGegnerKampfregel = null;
        public Base.CommandBase OnAddGegnerKampfregel
        {
            get
            {
                if (onAddGegnerKampfregel == null)
                    onAddGegnerKampfregel = new Base.CommandBase(AddGegnerKampfregel, null);
                return onAddGegnerKampfregel;
            }
        }

        private void AddGegnerKampfregel(object sender)
        {
            if (SelectedAddKampfregel == null || SelectedGegnerBase == null)
                return;
            GegnerBase_Kampfregel gk = SelectedGegnerBase.GegnerBase_Kampfregel.Where(_gk => _gk.KampfregelGUID == SelectedAddKampfregel.KampfregelGUID).FirstOrDefault();
            if (gk == null)
            {
                gk = Global.ContextHeld.New<GegnerBase_Kampfregel>();
                gk.KampfregelGUID = SelectedAddKampfregel.KampfregelGUID;
                gk.Kampfregel = SelectedAddKampfregel;
                gk.GegnerBaseGUID = SelectedGegnerBase.GegnerBaseGUID;
                gk.GegnerBase = SelectedGegnerBase;
                SelectedGegnerBase.GegnerBase_Kampfregel.Add(gk);
                SaveGegner();
                OnChanged("SelectedGegnerBase"); //Aktualisieren
            }
            //Aktualisieren
            SelectedGegnerKampfregel = gk;
        }

        private Base.CommandBase onDeleteKampfregel = null;
        public Base.CommandBase OnDeleteKampfregel
        {
            get
            {
                if (onDeleteKampfregel == null)
                    onDeleteKampfregel = new Base.CommandBase(DeleteKampfregel, null);
                return onDeleteKampfregel;
            }
        }

        private void DeleteKampfregel(object sender)
        {
            Kampfregel h = SelectedKampfregel;
            if (h != null && h.Usergenerated)
            {
                if (Confirm("Kampfregel löschen", string.Format("Sind Sie sicher, dass Sie die Kampfregel '{0}' löschen möchten?", h.Name))
                    )//&& Global.ContextHeld.Delete<GegnerBase_Angriff>(h))
                {
                    Global.ContextHeld.Delete<Kampfregel>(h);
                    OnChanged("KampfregelListe");
                    OnChanged("SelectedGegnerBase");
                }
            }
        }

        private Base.CommandBase onDeleteGegnerKampfregel = null;
        public Base.CommandBase OnDeleteGegnerKampfregel
        {
            get
            {
                if (onDeleteGegnerKampfregel == null)
                    onDeleteGegnerKampfregel = new Base.CommandBase(DeleteGegnerKampfregel, null);
                return onDeleteGegnerKampfregel;
            }
        }

        private void DeleteGegnerKampfregel(object sender)
        {
            if (SelectedGegnerKampfregel == null || SelectedGegnerBase == null)
                return;
            GegnerBase_Kampfregel h = SelectedGegnerKampfregel;
            if (Confirm("Kampfregelzuweisung löschen", string.Format("Sind Sie sicher, dass Sie die Kampfregel '{0}' vom Gegner entfernen möchten?", SelectedGegnerKampfregel.Kampfregel.Name))
                    )//&& Global.ContextHeld.Delete<GegnerBase_Angriff>(h))
            {
                SelectedGegnerBase.GegnerBase_Kampfregel.Remove(SelectedGegnerKampfregel);
                SaveGegner();
                //Global.ContextHeld.Delete<GegnerBase_Kampfregel>(SelectedGegnerKampfregel);
                OnChanged("SelectedGegnerBase");
            }
        }

        #endregion

        #region Gegner Import/Export/New/Delete

        private Base.CommandBase onNewGegnerBase = null;
        public Base.CommandBase OnNewGegnerBase
        {
            get
            {
                if (onNewGegnerBase == null)
                    onNewGegnerBase = new Base.CommandBase(NewGegnerBase, null);
                return onNewGegnerBase;
            }
        }

        private void NewGegnerBase(object sender)
        {
            GegnerBase h = Global.ContextHeld.New<GegnerBase>();
            if (Global.ContextHeld.Insert<GegnerBase>(h))
            {
                //Liste aktualisieren
                LoadDaten();
                SelectedGegnerBase = h;
            }
        }

        private Base.CommandBase onDeleteGegnerBase = null;
        public Base.CommandBase OnDeleteGegnerBase
        {
            get
            {
                if (onDeleteGegnerBase == null)
                    onDeleteGegnerBase = new Base.CommandBase(DeleteGegnerBase, null);
                return onDeleteGegnerBase;
            }
        }

        private void DeleteGegnerBase(object sender)
        {
            GegnerBase h = SelectedGegnerBase;
            if (h != null)
            {
                if (Confirm("Gegner löschen", string.Format("Sind Sie sicher, dass Sie den Gegner '{0}' löschen möchten?", h.Name))
                    && Global.ContextHeld.Delete<GegnerBase>(h))
                {
                    //Liste aktualisieren
                    LoadDaten();
                    SelectedGegnerBase = GegnerBaseListe.FirstOrDefault();
                }
            }
        }

        private Base.CommandBase onExportGegnerBase = null;
        public Base.CommandBase OnExportGegnerBase
        {
            get
            {
                if (onExportGegnerBase == null)
                    onExportGegnerBase = new Base.CommandBase(ExportGegnerBase, null);
                return onExportGegnerBase;
            }
        }
        //        public string ExportGegnerBase(string pfad)
        private void ExportGegnerBase(object sender)
        {
            GegnerBase h = SelectedGegnerBase;
            if (h != null)
            {
                string pfad = ChooseFile("Gegner exportieren", h.Name, true, "xml");
                if (pfad != null)
                {
                    try
                    {
                        h.Export(pfad);
                        PopUp("Der Gegner wurde in \'" + pfad + "\' gespeichert.");
                        return;
                    }
                    catch (Exception ex)
                    {
                        ShowError("Beim Export ist ein Fehler aufgetreten.", ex);
                    }
                }
            }
            return;
        }

        private Base.CommandBase onImportGegnerBase = null;
        public Base.CommandBase OnImportGegnerBase
        {
            get
            {
                if (onImportGegnerBase == null)
                    onImportGegnerBase = new Base.CommandBase(ImportGegnerBaseCommand, null);
                return onImportGegnerBase;
            }
        }
        public void ImportGegnerBaseCommand(object sender)
        {
            string pfad = ChooseFile("Gegner importieren", "", false, "xml");
            if (pfad != null)
            {
#if !DEBUG
                try
                {
#endif
                GegnerBase h = ImportGegnerBase(pfad);
                SelectedGegnerBase = h;
#if !DEBUG
                }
                catch (Exception ex)
                {
                    ShowError("Beim Import ist ein Fehler aufgetreten.", ex);
                }
#endif
            }

        }
        public GegnerBase ImportGegnerBase(string pfad)
        {
            Guid hGuid = Guid.Empty;
            GegnerBase importGegnerBase = null;
            if (!System.IO.File.Exists(pfad))
                throw new System.IO.FileNotFoundException("Die Datei konnte nicht gefunden werden.", pfad);
            if (Model.Service.SerializationService.IsMeistergeisterFile(pfad))
            {
                GegnerBase h = Model.Service.SerializationService.DeserializeObjectFromFile<GegnerBase>(pfad);
                hGuid = h.GegnerBaseGUID;
            }
            else
                throw new ArgumentException(String.Format("Die Datei {0} ist in keinem bekannten Format", pfad));
            GegnerBase existing = null;
            bool overwrite = true;
            if ((existing = Global.ContextHeld.Liste<GegnerBase>().Where(hl => hl.GegnerBaseGUID == hGuid).FirstOrDefault()) != null)
            {
                //überschreiben?
                int result = ConfirmYesNoCancel(
                    "Gegner importieren",
                    String.Format("Es existiert bereits der Gegner \"{1}\" mit der Guid {0} soll dieser überschrieben werden?\n\nBei \"Nein\" wird eine Kopie mit einer neuen Guid angelegt.", hGuid, existing.Name)
                );
                if (result == 0)
                    return null;
                else if (result == 1)
                    overwrite = false;
            }
            importGegnerBase = GegnerBase.Import(pfad, overwrite ? Guid.Empty : Guid.NewGuid());
            LoadDaten();
            return importGegnerBase;
        }


        private Base.CommandBase onCloneGegnerBase = null;
        public Base.CommandBase OnCloneGegnerBase
        {
            get
            {
                if (onCloneGegnerBase == null)
                    onCloneGegnerBase = new Base.CommandBase(CloneGegnerBase, null);
                return onCloneGegnerBase;
            }
        }
        private void CloneGegnerBase(object sender)
        {
            if (SelectedGegnerBase != null)
            {
                GegnerBase h = SelectedGegnerBase.Clone();
                LoadDaten();
            }
        }
        #endregion

        #region Events
        public void OnAngriffChanged(object sender, PropertyChangedEventArgs args)
        {
            //TODO JT: Wann speichere ich den Gegner und die Daten?
        }

        public void OnGegnerKampfregelTPChanged(object sender, PropertyChangedEventArgs args)
        {
            //TODO JT: Wann speichere ich den Gegner und die Daten?
        }
        #endregion

        #region private Methoden
        private void SaveGegner()
        {
            if (SelectedGegnerBase != null)
                Global.ContextHeld.Update<GegnerBase>(SelectedGegnerBase);
        }

        private void ParseBemerkungen()
        {
            foreach (GegnerBase g in GegnerBaseListe)
            {
                if (g.Bemerkung != null && g.Bemerkung.Trim() != String.Empty)
                    foreach (string zeile in g.Bemerkung.Split(new char[] { '\n' }))
                    {
                        GegnerBase_Angriff ga = GegnerBase_Angriff.Parse(zeile);
                        if (ga != null)
                        {
                            string name = ga.Name; int i = 1;
                            while (g.GegnerBase_Angriff.Where(gba => gba.Name == name).Count() > 0)
                                name = String.Format("{0} ({1})", ga.Name, ++i);
                            g.GegnerBase_Angriff.Add(ga);
                        }
                        else
                        {
                            Dictionary<string, int> erschwernisse;
                            IEnumerable<Kampfregel> kampfregeln = Kampfregel.Parse(zeile, out erschwernisse);
                            if (kampfregeln != null && kampfregeln.Count() > 0)
                                foreach (Kampfregel kr in kampfregeln)
                                {
                                    if (g.GegnerBase_Kampfregel.Where(gbkr => gbkr.KampfregelGUID == kr.KampfregelGUID).Count() == 0)
                                    {
                                        string eName = erschwernisse.Keys.Where(e => kr.Name.ToUpperInvariant().Contains(e.ToUpperInvariant())).FirstOrDefault();
                                        var gkr = new GegnerBase_Kampfregel();
                                        gkr.KampfregelGUID = kr.KampfregelGUID;
                                        gkr.GegnerBaseGUID = g.GegnerBaseGUID;
                                        if (eName != null)
                                            gkr.Erschwernis = erschwernisse[eName];
                                        g.GegnerBase_Kampfregel.Add(gkr);
                                    }
                                }
                        }
                    }
            }
        }
        #endregion
    }

}
