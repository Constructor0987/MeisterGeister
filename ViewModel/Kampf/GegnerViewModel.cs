﻿using System;
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
        public GegnerViewModel(Func<string> selectImage, Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, string[], string> chooseFile, Action<string, Exception> showError) : 
            base(popup, confirm, confirmYesNoCancel, chooseFile, showError)
        {
            this.selectImage = selectImage;

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
                RefreshTagListe();
                FilterListe();
            }
        }

        #region Select Image

        private Func<string> selectImage;

        private Base.CommandBase onSelectImage = null;
        public Base.CommandBase OnSelectImage
        {
            get
            {
                if (onSelectImage == null)
                    onSelectImage = new Base.CommandBase(SelectImage, null);
                return onSelectImage;
            }
        }

        private void SelectImage(object args)
        {
            if (SelectedGegnerBase != null && selectImage != null)
            {
                string path = selectImage();
                if (path != null)
                    SelectedGegnerBase.Bild = path;
            }
        }

        #endregion

        #region Bindable Properties

        private string _suchText = string.Empty;
        public string SuchText
        {
            get { return _suchText; }
            set
            {
                _suchText = value;
                OnChanged("SuchText");
                FilterListe();
            }
        }

        private GegnerBase selectedGegnerBase;
        public GegnerBase SelectedGegnerBase
        {
            get { return selectedGegnerBase; }
            set {
                SaveGegner();
                selectedGegnerBase = value;
                if (selectedGegnerBase != null)
                    RefreshTagListe();
                OnChanged("SelectedGegnerBase");
                OnChanged("AngriffListe");
                OnChanged("SelectedGegnerBaseIsNotNull");
            }
        }

        public bool SelectedGegnerBaseIsNotNull
        {
            get { return SelectedGegnerBase != null; }
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

        private List<GegnerBase> _filteredGegnerBaseListe;
        public List<GegnerBase> FilteredGegnerBaseListe
        {
            get { return _filteredGegnerBaseListe; }
            set
            {
                _filteredGegnerBaseListe = value;
                OnChanged("FilteredGegnerBaseListe");
            }
        }

        private List<string> _tagListe;
        public List<string> TagListe
        {
            get { return _tagListe; }
            set
            {
                _tagListe = value;
                OnChanged("TagListe");
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

        public List<Ausrüstung> WaffenListe
        {
            get { return Global.ContextInventar.AusruestungListe.Where(a => a.Waffe != null || a.Fernkampfwaffe != null).OrderBy(a => a.Name).ToList(); }
        }

        private Ausrüstung selectedWaffe;
        public Ausrüstung SelectedWaffe
        {
            get { return selectedWaffe; }
            set { 
                selectedWaffe = value;
                OnChanged("SelectedWaffe");
            }
        }
        
        public List<Rüstung> RüstungsListe
        {
            get { return Global.ContextInventar.RuestungListe.OrderBy(a => a.Name).ToList(); }
        }

        private Rüstung selectedRüstung;
        public Rüstung SelectedRüstung
        {
            get { return selectedRüstung; }
            set { 
                selectedRüstung = value;
                OnChanged("SelectedRüstung");
            }
        }

        #endregion

        #region Filtern

        /// <summary>
        /// Filtert die Gegner-Liste auf Basis des SuchTextes.
        /// </summary>
        private void FilterListe()
        {
            string suchText = _suchText.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');

            if (suchText == string.Empty) // kein Suchwort
                FilteredGegnerBaseListe = GegnerBaseListe.AsParallel().OrderBy(n => n.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredGegnerBaseListe = GegnerBaseListe.AsParallel().Where(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredGegnerBaseListe = GegnerBaseListe.AsParallel().Where(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }

        private void RefreshTagListe()
        {
            if (GegnerBaseListe == null)
                return;

            List<string> tagListe = new List<string>();
            string[] tags;
            foreach (var item in GegnerBaseListe)
            {
                tags = (item.Tags ?? string.Empty).Split(new char[] {',', ';', '/'});
                foreach (string tag in tags)
                {
                    if (!tagListe.Contains(tag.Trim()))
                        tagListe.Add(tag.Trim());
                }
            }
            tagListe.Sort();
            TagListe = tagListe;
        }

        #endregion

        #region RS Add/Delete
        private Base.CommandBase onAddRüstung = null;
        public Base.CommandBase OnAddRüstung
        {
            get
            {
                if (onAddRüstung == null)
                    onAddRüstung = new Base.CommandBase(AddRüstung, null);
                return onAddRüstung;
            }
        }

        private void AddRüstung(object args)
        {
            if (SelectedGegnerBase == null || SelectedRüstung == null)
                return;
            var rs = SelectedGegnerBase.RS as Rüstungsschutz;
            if (rs == null)
                return;
            var rsneu = SelectedRüstung + rs;
            rs.SetValues(rsneu);

            // Rüstung in Bemerkung speichern
            SelectedGegnerBase.Bemerkung += string.Format("\n{0} (Rüstung)", SelectedRüstung.Name);
        }

        private Base.CommandBase onRemoveRüstung = null;
        public Base.CommandBase OnRemoveRüstung
        {
            get
            {
                if (onRemoveRüstung == null)
                    onRemoveRüstung = new Base.CommandBase(RemoveRüstung, null);
                return onRemoveRüstung;
            }
        }

        private void RemoveRüstung(object args)
        {
            if (SelectedGegnerBase == null || SelectedRüstung == null)
                return;
            var rs = SelectedGegnerBase.RS as Rüstungsschutz;
            if (rs == null)
                return;
            var rsneu = rs - new Rüstungsschutz(SelectedRüstung);
            rs.SetValues(rsneu);

            // Rüstung aus Bemerkung löschen
            SelectedGegnerBase.Bemerkung = SelectedGegnerBase.Bemerkung.Replace(string.Format("\n{0} (Rüstung)", SelectedRüstung.Name), string.Empty);
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

        private void NewAngriff(object args)
        {
            if (SelectedGegnerBase == null)
                return;
            if (AngriffAddName == null || AngriffAddName == String.Empty)
            {
                PopUp("Der Name für einen neuen Angriff darf nicht leer sein.");
                return;
            }
            if (SelectedWaffe != null)
            {
                NewAngriffFromWaffe(SelectedWaffe);
                return;
            }
            GegnerBase_Angriff ga = Global.ContextHeld.New<GegnerBase_Angriff>();
            ga.Name = AngriffAddName;
            // Default-Werte
            ga.DK = "N"; ga.TPWürfelAnzahl = 1; ga.TPWürfel = 6;
            ga.AT = SelectedGegnerBase.AT;
            ga.PA = SelectedGegnerBase.PA;

            AddAngriff(ga);
        }

        private Base.CommandBase onNewAngriffFromWaffe = null;
        public Base.CommandBase OnNewAngriffFromWaffe
        {
            get
            {
                if (onNewAngriffFromWaffe == null)
                    onNewAngriffFromWaffe = new Base.CommandBase(NewAngriffFromWaffe, null);
                return onNewAngriffFromWaffe;
            }
        }

        private void NewAngriffFromWaffe(object args)
        {
            var ausr = args as Model.Ausrüstung;
            if (ausr == null)
                ausr = selectedWaffe;
            if (ausr == null)
                return;
            if (ausr.Waffe != null)
                AddAngriff(GegnerBase_Angriff.FromWaffe(ausr.Waffe, SelectedGegnerBase));
            if (ausr.Fernkampfwaffe != null)
                AddAngriff(GegnerBase_Angriff.FromFernkampfwaffe(ausr.Fernkampfwaffe, SelectedGegnerBase));
        }

        private GegnerBase_Angriff AddAngriff(GegnerBase_Angriff ga)
        {
            if (SelectedGegnerBase == null || ga == null)
                return null;
            var g = SelectedGegnerBase;
            string name = ga.Name; int i = 0;
            while (g.GegnerBase_Angriff.Any(gba => gba.Name == name))
                name = String.Format("{0} ({1})", ga.Name, ++i);
            ga.Name = name;
            ga.GegnerBaseGUID = g.GegnerBaseGUID;
            ga.GegnerBase = g;
            SelectedGegnerBase.GegnerBase_Angriff.Add(ga);
            SaveGegner();
            OnChanged("AngriffListe");
            return ga;
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

        private void DeleteAngriff(object args)
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

        private void NewKampfregel(object args)
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

        private void AddGegnerKampfregel(object args)
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

        private void DeleteKampfregel(object args)
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

        private void DeleteGegnerKampfregel(object args)
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

        private void NewGegnerBase(object args)
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

        private void DeleteGegnerBase(object args)
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
        private void ExportGegnerBase(object args)
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
        public void ImportGegnerBaseCommand(object args)
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
        private void CloneGegnerBase(object args)
        {
            if (SelectedGegnerBase != null)
            {
                SaveGegner();
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

        #region Parse Bemerkung
        private Base.CommandBase onParseBemerkung = null;
        public Base.CommandBase OnParseBemerkung
        {
            get {
                if (onParseBemerkung == null)
                    onParseBemerkung = new Base.CommandBase(ParseBemerkung, null);
                return onParseBemerkung; 
            }
        }

        private Base.CommandBase onParseBemerkungAll = null;
        public Base.CommandBase OnParseBemerkungAll
        {
            get
            {
                if (onParseBemerkungAll == null)
                    onParseBemerkungAll = new Base.CommandBase(ParseBemerkungAll, null);
                return onParseBemerkungAll;
            }
        }

        private void ParseBemerkungAll(object args)
        {
            foreach (GegnerBase g in GegnerBaseListe)
            {
                if (!g.GegnerBase_Angriff.Any())
                    g.ParseBemerkung();
            }
            OnChanged("AngriffListe");
        }

        private void ParseBemerkung(object args)
        {
            if (SelectedGegnerBase == null)
                return;
            SelectedGegnerBase.ParseBemerkung();
            OnChanged("AngriffListe");
        }
        #endregion

        #region private Methoden
        private void SaveGegner()
        {
            if (SelectedGegnerBase != null)
                Global.ContextHeld.Update<GegnerBase>(SelectedGegnerBase);
        }
        #endregion
    }

}
