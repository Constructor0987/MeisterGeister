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
        public GegnerViewModel(Func<string, string, string, string> input, Func<string> selectImage, Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Action<string, Exception> showError) : 
            base(popup, confirm, confirmYesNoCancel, chooseFile, showError)
        {
            this.selectImage = selectImage;
            this.changeTag = input;

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
                var tag = TagListe.Where(t => (t == null ? string.Empty : t.ToLower()) == _suchText.ToLower());
                if (tag != null)
                    SelectedTag = tag.FirstOrDefault();
                else
                    FilterListe();
                OnChanged("AktiveFilter");
            }
        }

        private bool _nurBenutzerdefiniert = false;
        public bool NurBenutzerdefiniert
        {
            get { return _nurBenutzerdefiniert; }
            set {
                if (Set(ref _nurBenutzerdefiniert, value))
                {
                    RefreshTagListe();
                    FilterListe();
                    OnChanged("AktiveFilter");
                }
            }
        }

        public string AktiveFilter
        {
            get
            {
                string s = SuchText.ToLower();
                if (!s.StartsWith((SelectedTag == null ? string.Empty : SelectedTag.ToLower())))
                    s += (SelectedTag == null ? string.Empty : " " + SelectedTag.ToLower());
                if(NurBenutzerdefiniert)
                    s += " NurUser";
                return s;
            }
        }

        private string _selectedTag = string.Empty;
        public string SelectedTag
        {
            get { return _selectedTag; }
            set
            {
                _selectedTag = value;
                OnChanged("SelectedTag");
                FilterListe();
                OnChanged("AktiveFilter");
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
                if (onDeleteGegnerBase != null)
                    onDeleteGegnerBase.Invalidate();
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
        /// Filtert die Gegner-Liste auf Basis des SuchTextes und des NurBenutzerdefiniert-Schalters.
        /// </summary>
        private void FilterListe()
        {
            string suchText = _suchText.ToLower().Trim();
            if (SelectedTag != null)
                suchText += " " + SelectedTag.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');

            if (suchText == string.Empty) // kein Suchwort
                if(NurBenutzerdefiniert)
                    FilteredGegnerBaseListe = GegnerBaseListe.AsParallel().Where(s => s.Usergenerated).OrderBy(n => n.Name).ToList();
                else
                    FilteredGegnerBaseListe = GegnerBaseListe.AsParallel().OrderBy(n => n.Name).ToList();
            else // hat Suchwörter
                FilteredGegnerBaseListe = GegnerBaseListe.AsParallel().Where(s => (!NurBenutzerdefiniert || s.Usergenerated) && s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }

        private void RefreshTagListe()
        {
            if (GegnerBaseListe == null)
                return;

            List<string> tagListe = new List<string>();
            List<string> tagsGegner;

            // Hinweis: Eine Paralellisierung der Schleife scheint sich nicht zu lohnen und ist teilweise sogar langsamer
            foreach (var item in GegnerBaseListe.Where(s=>(!NurBenutzerdefiniert || s.Usergenerated)))
            {
                tagsGegner = item.TagListe();
                foreach (string tag in tagsGegner)
                {
                    if (!tagListe.Contains(tag))
                        tagListe.Add(tag);
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

        private Base.CommandBase onClearFilter = null;
        public Base.CommandBase OnClearFilter
        {
            get
            {
                if (onClearFilter == null)
                    onClearFilter = new Base.CommandBase(ClearFilter, null);
                return onClearFilter;
            }
        }

        private void ClearFilter(object args)
        {
            SuchText = string.Empty;
            SelectedTag = null;
            NurBenutzerdefiniert = false;
        }
        #endregion

        #region Tag Commands

        private Base.CommandBase onDeleteTag = null;
        public Base.CommandBase OnDeleteTag
        {
            get
            {
                if (onDeleteTag == null)
                    onDeleteTag = new Base.CommandBase(DeleteTag, null);
                return onDeleteTag;
            }
        }

        private void DeleteTag(object args)
        {
            string tag = SelectedTag;
            if (tag != string.Empty)
            {
                if (Confirm("Stichwort löschen", string.Format("Sind Sie sicher, dass Sie das Stichwort '{0}' löschen möchten? Es wird aus allen Gegner-Vorlagen entfernt.", tag)))
                {
                    List<string> tagList = null;
                    foreach (var gegner in GegnerBaseListe)
                    {
                        tagList = gegner.TagListe();
                        if (tagList != null && tagList.Contains(tag))
                        {
                            tagList.Remove(tag);
                            gegner.Tags = string.Empty;
                            foreach (string t in tagList)
                            {
                                if (gegner.Tags != string.Empty)
                                    gegner.Tags += ", ";
                                gegner.Tags += t;
                            }
                        }
                    }
                    SaveGegner();
                    RefreshTagListe();
                }
            }
        }

        private Func<string, string, string, string> changeTag;

        private Base.CommandBase onChangeTag = null;
        public Base.CommandBase OnChangeTag
        {
            get
            {
                if (onChangeTag == null)
                    onChangeTag = new Base.CommandBase(ChangeTag, null);
                return onChangeTag;
            }
        }

        private void ChangeTag(object obj)
        {
            string tag = SelectedTag;
            if (tag != string.Empty && changeTag != null)
            {
                string newTagName = changeTag("Stichwort ändern", "Bitte den neuen Namen des Stichworts angeben.", SelectedTag);
                if (newTagName != null && newTagName != tag)
                {
                    newTagName = newTagName.Trim();
                    foreach (var gegner in GegnerBaseListe)
                    {
                        if (gegner.Tags != null && gegner.Tags.Contains(tag))
                        {
                            gegner.Tags = gegner.Tags.Replace(tag, newTagName);
                        }
                    }
                    SaveGegner();
                    RefreshTagListe();
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

        #region Interne Gegnerdaten
        private Base.CommandBase onLadeInterneGegnerDaten = null;
        public Base.CommandBase OnLadeInterneGegnerDaten
        {
            get
            {
                if (onLadeInterneGegnerDaten == null)
                    onLadeInterneGegnerDaten = new Base.CommandBase(LadeInterneGegnerDaten, null);
                return onLadeInterneGegnerDaten;
            }
        }

        private void LadeInterneGegnerDaten(object args)
        {
            if(Confirm("Interne Gegnerdaten laden", "Sollen alle vom Team ausgelieferten Gegner gelöscht und durch die internen Gegnerdaten ersetzt werden?\nManuell angelegte Gegner bleiben davon unberührt.\nAnschließend muss MeisterGeister neu gestartet werden."))
                MeisterGeister.Daten.DatabaseUpdate.InterneGegnerDatenEinfügen();
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
                    onDeleteGegnerBase = new Base.CommandBase(DeleteGegnerBase, CanDeleteGegnerBase);
                return onDeleteGegnerBase;
            }
        }

        private bool CanDeleteGegnerBase(object args)
        {
            GegnerBase h = SelectedGegnerBase;
            if (h == null)
                return false;
            return (h.Usergenerated || Global.INTERN);
        }

        private void DeleteGegnerBase(object args)
        {
            GegnerBase h = SelectedGegnerBase;
            if (h != null && CanDeleteGegnerBase(args))
            {
                if (Confirm("Gegner löschen", string.Format("{1}Sind Sie sicher, dass Sie den Gegner '{0}' löschen möchten?", h.Name, h.Usergenerated?"":"ACHTUNG! Dies ist ein mitausgelieferter Gegner!\n"))
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
                string pfad = ChooseFile("Gegner exportieren", h.Name, true, false, "xml");
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
            string pfad = ChooseFile("Gegner importieren", "", false, false, "xml");
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
            if (Model.Service.SerializationService.IsMeistergeisterFile(pfad, "GegnerBase"))
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
