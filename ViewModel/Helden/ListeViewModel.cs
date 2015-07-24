using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using System.ComponentModel;

using MeisterGeister.Logic.HeldenImport;

namespace MeisterGeister.ViewModel.Helden
{
    public class ListeViewModel : Base.ViewModelBase
    {
        private Held selectedHeld = new Held();
        private bool hasChanges = false;
        private List<Model.Held> _heldListe;

        public ListeViewModel()
        {

        }

        ///// <summary>
        ///// ViewModel mit Callbacks.
        ///// </summary>
        ///// <param name="popup">Ein OK-Popup-Dialog. (Nachricht)</param>
        ///// <param name="confirm">Bestätigung einer Ja-Nein-Frage. (Fenstertitel, Frage)</param>
        ///// <param name="confirmYesNoCancel">Bestätigen eines YesNoCancel-Dialoges (cancel=0, no=1, yes=2). (Fenstertitel, Frage)</param>
        ///// <param name="chooseFile">Wahl einer Datei. (Fenstertitel, Dateiname, zum speichern, Dateierweiterungen ...)</param>
        public ListeViewModel(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Action<string, Exception> showError) :
            base(popup, confirm, confirmYesNoCancel, chooseFile, showError)
        {
            if (Global.SelectedHeld != null)
                selectedHeld = Global.SelectedHeld;
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;
            LoadDaten();
        }

        public Held SelectedHeld
        {
            get { return selectedHeld; }
            set
            {
                if (selectedHeld != null)
                    selectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
                selectedHeld = value;
                Global.SelectedHeld = value;
                if (selectedHeld != null)
                    selectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;

                OnChanged("SelectedHeld");
            }
        }

        private bool _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }

        // TODO: Werden diese Methoden noch gebrauch?
        //private void SelectedHeldChanged()
        //{
        //    SelectedHeld = Global.SelectedHeld;
        //    if (SelectedHeld != null)
        //        SelectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
        //}

        //private void SelectedHeldChanging()
        //{
        //    if (Global.SelectedHeld != null)
        //    {
        //        if (hasChanges)
        //            Global.ContextHeld.Update<Model.Held>(SelectedHeld);
        //        hasChanges = false;
        //        Global.SelectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
        //    }
        //}

        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (new string[] { "Name", "Spieler", "AktiveHeldengruppe" }.Contains(args.PropertyName))
                SortList();
        }

        public List<Model.Held> HeldListe
        {
            get { return _heldListe; }
            set
            {
                _heldListe = value;
                OnChanged("HeldListe");
            }
        }

        List<string> _sortierungListe = new List<string>() { "Held", "Spieler" };
        public List<string> SortierungListe
        {
            get { return _sortierungListe; }
            set { _sortierungListe = value; OnChanged("SortierungListe"); }
        }

        private string _selectedSortierung = "Held";
        public string SelectedSortierung
        {
            get { return _selectedSortierung; }
            set
            {
                _selectedSortierung = value;
                OnChanged("SelectedSortierung");
                //Liste aktualisieren
                LoadDaten();
            }
        }

        public void LoadDaten()
        {
            Guid tmp = (SelectedHeld == null) ? Guid.Empty : SelectedHeld.HeldGUID;
            SelectedHeld = null;
            if (Global.ContextHeld != null)
            {
                SortList();
                if (tmp != Guid.Empty)
                    SelectedHeld = HeldListe.Where(h => h.HeldGUID == tmp).FirstOrDefault();
            }
        }

        private void SortList()
        {
            if (SelectedSortierung == "Spieler")
                HeldListe = Global.ContextHeld.Liste<Held>().OrderByDescending(h => h.AktiveHeldengruppe).ThenBy(h => h.Spieler).ThenBy(h => h.Name).ToList();
            else
                HeldListe = Global.ContextHeld.Liste<Held>().OrderByDescending(h => h.AktiveHeldengruppe).ThenBy(h => h.Name).ThenBy(h => h.Spieler).ToList();
        }

        private Base.CommandBase onNewHeld = null;
        public Base.CommandBase OnNewHeld
        {
            get
            {
                if (onNewHeld == null)
                    onNewHeld = new Base.CommandBase(NewHeld, null);
                return onNewHeld;
            }
        }

        private void NewHeld(object sender)
        {
            Held h = Global.ContextHeld.New<Held>();
            h.AddBasisTalente();
            if (Global.ContextHeld.Insert<Held>(h))
            {
                //Liste aktualisieren
                LoadDaten();
            }
        }

        private Base.CommandBase onDeleteHeld = null;
        public Base.CommandBase OnDeleteHeld
        {
            get
            {
                if (onDeleteHeld == null)
                    onDeleteHeld = new Base.CommandBase(DeleteHeld, null);
                return onDeleteHeld;
            }
        }

        private void DeleteHeld(object sender)
        {
            Held h = SelectedHeld;
            if (h != null && !IsReadOnly)
            {
                if (Confirm("Held löschen", string.Format("Sind Sie sicher, dass Sie den Helden '{0}' löschen möchten?", h.Name))
                    && Global.ContextHeld.Delete<Held>(h))
                {
                    //Liste aktualisieren
                    LoadDaten();
                    SelectedHeld = HeldListe.FirstOrDefault();
                }
            }
        }

        private Base.CommandBase onDeleteHeldAll = null;
        public Base.CommandBase OnDeleteHeldAll
        {
            get
            {
                if (onDeleteHeldAll == null)
                    onDeleteHeldAll = new Base.CommandBase(DeleteHeldAll, null);
                return onDeleteHeldAll;
            }
        }

        private void DeleteHeldAll(object sender)
        {
            if (!IsReadOnly && Confirm("Alle Helden löschen", string.Format("Sind Sie sicher, dass Sie alle Helden ({0}) endgültig löschen möchten?", HeldListe.Count)))
            {
                Global.SetIsBusy(true, "Alle Helden werden gelöscht...");

                Global.ContextHeld.DeleteAll<Held>();

                //Liste aktualisieren
                LoadDaten();
                SelectedHeld = null;

                Global.SetIsBusy(false);
            }
        }

        private Base.CommandBase onExportHeld = null;
        public Base.CommandBase OnExportHeld
        {
            get
            {
                if (onExportHeld == null)
                    onExportHeld = new Base.CommandBase(ExportHeld, null);
                return onExportHeld;
            }
        }
        //        public string ExportHeld(string pfad)
        private void ExportHeld(object sender)
        {
            Held h = SelectedHeld;
            if (h != null)
            {
                string pfad = ChooseFile("Held exportieren", h.Name, true, false, "xml");
                if (pfad != null)
                {
                    try
                    {
                        h.Export(pfad);
                        PopUp("Der Held wurde in \'" + pfad + "\' gespeichert.");
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

        private Base.CommandBase onExportDemoHelden = null;
        public Base.CommandBase OnExportDemoHelden
        {
            get
            {
                if (onExportDemoHelden == null)
                    onExportDemoHelden = new Base.CommandBase(ExportDemoHelden, null);
                return onExportDemoHelden;
            }
        }
        private void ExportDemoHelden(object sender)
        {
            if (!System.IO.Directory.Exists("Daten\\Helden\\Demohelden"))
                System.IO.Directory.CreateDirectory("Daten\\Helden\\Demohelden");
            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
            foreach (Held h in HeldListe)
            {
                string fileName = View.General.ViewHelper.GetValidFilename(System.IO.Path.Combine("Daten\\Helden\\Demohelden", System.IO.Path.ChangeExtension(h.Name, "xml")));
                h.Export(fileName, true);
            }
            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
        }

        private Base.CommandBase onImportDemoHelden = null;
        public Base.CommandBase OnImportDemoHelden
        {
            get
            {
                if (onImportDemoHelden == null)
                    onImportDemoHelden = new Base.CommandBase(ImportDemoHelden, null);
                return onImportDemoHelden;
            }
        }
        private void ImportDemoHelden(object sender)
        {
            if (!System.IO.Directory.Exists("Daten\\Helden\\Demohelden"))
                return;

            Global.SetIsBusy(true, "Demo-Helden werden importiert...");

            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
            foreach (string fileName in System.IO.Directory.EnumerateFiles("Daten\\Helden\\Demohelden", "*.xml", System.IO.SearchOption.TopDirectoryOnly))
            {
                Held h = Held.Import(fileName, true);
            }
            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
            Held.UpdateLists();
            LoadDaten();

            Global.SetIsBusy(false);
        }

        private Base.CommandBase onImportHeld = null;
        public Base.CommandBase OnImportHeld
        {
            get
            {
                if (onImportHeld == null)
                    onImportHeld = new Base.CommandBase(ImportHeldCommand, null);
                return onImportHeld;
            }
        }
        public void ImportHeldCommand(object sender)
        {
            string pfad = ChooseFile("Held importieren", "", false, false, "xml", "xls", "xlsx", "xlsb");
            if (pfad != null)
            {
                Global.SetIsBusy(true, string.Format("{0} wird importiert...", pfad));

#if !DEBUG
                try
                {
#endif
                Held h = ImportHeld(pfad);
                SelectedHeld = h;
#if !DEBUG
                }
                catch (Exception ex)
                {
                    ShowError("Beim Import ist ein Fehler aufgetreten.", ex);
                }
#endif

                Global.SetIsBusy(false);
            }

        }
        public Held ImportHeld(string pfad)
        {
            Guid hGuid = Guid.Empty;
            Held importHeld = null;
            if (!System.IO.File.Exists(pfad))
                throw new System.IO.FileNotFoundException("Die Datei konnte nicht gefunden werden.", pfad);
            try
            {
                var fs = System.IO.File.OpenRead(pfad);
                fs.Close();
            }
            catch
            {
                throw new ArgumentException(String.Format("Die Datei {0} konnte nicht geöffnet werden.", pfad));
            }
            bool isHeldenSoftware = false; bool isHeldenblatt = false;
            if (HeldenSoftwareImporter.IsHeldenSoftwareFile(pfad))
            {
                hGuid = HeldenSoftwareImporter.GetGuidFromFile(pfad);
                isHeldenSoftware = true;
            }
            else if (Model.Service.SerializationService.IsMeistergeisterFile(pfad))
            {
                Held h = Model.Service.SerializationService.DeserializeObjectFromFile<Held>(pfad);
                hGuid = h.HeldGUID;
            }
            else if (HeldenblattImporter.IsHeldenblattFile(pfad))
            {
                hGuid = Guid.Empty;
                isHeldenblatt = true;
            }
            else
                throw new ArgumentException(String.Format("Die Datei {0} ist in keinem bekannten Format", pfad));
            Held existing = null;
            bool overwrite = true;
            if (!isHeldenblatt && (existing = Global.ContextHeld.Liste<Held>().Where(hl => hl.HeldGUID == hGuid).FirstOrDefault()) != null)
            {
                //überschreiben?
                int result = ConfirmYesNoCancel(
                    "Held importieren",
                    String.Format("Es existiert bereits der Held \"{1}\" mit der Guid {0} soll dieser überschrieben werden?\n\nBei \"Nein\" wird eine Kopie mit einer neuen Guid angelegt.", hGuid, existing.Name)
                );
                if (result == 0)
                    return null;
                else if (result == 1)
                    overwrite = false;
            }
            if (isHeldenSoftware)
                importHeld = HeldenSoftwareImporter.ImportHeldenSoftwareFile(pfad, overwrite ? Guid.Empty : Guid.NewGuid());
            else if (isHeldenblatt)
                importHeld = HeldenblattImporter.ImportHeldenblattFile(pfad);
            else
                importHeld = Held.Import(pfad, overwrite ? Guid.Empty : Guid.NewGuid());

            if (existing != null && overwrite && (isHeldenblatt || isHeldenSoftware))
            { // MeisterGeister spezifische Daten beim Reimport übernehmen
                importHeld.Spieler = existing.Spieler;
            }

            LoadDaten();
            return importHeld;
        }


        private Base.CommandBase onCloneHeld = null;
        public Base.CommandBase OnCloneHeld
        {
            get
            {
                if (onCloneHeld == null)
                    onCloneHeld = new Base.CommandBase(CloneHeld, null);
                return onCloneHeld;
            }
        }
        private void CloneHeld(object sender)
        {
            Global.SetIsBusy(true, "Held wird kopiert...");

            Held h = SelectedHeld.Clone();
            LoadDaten();

            Global.SetIsBusy(false);
        }
    }
}
