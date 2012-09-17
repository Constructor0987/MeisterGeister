using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Helden
{
    public class ListeViewModel : Base.ViewModelBase
    {
        private Held selectedHeld = new Held();
        private bool hasChanges = false;
        private List<Model.Held> _heldListe;

        /// <summary>
        /// ViewModel mit Callbacks.
        /// </summary>
        /// <param name="popup">Ein OK-Popup-Dialog. (Nachricht)</param>
        /// <param name="confirm">Bestätigung einer Ja-Nein-Frage. (Fenstertitel, Frage)</param>
        /// <param name="confirmYesNoCancel">Bestätigen eines YesNoCancel-Dialoges (cancel=0, no=1, yes=2). (Fenstertitel, Frage)</param>
        /// <param name="chooseFile">Wahl einer Datei. (Fenstertitel, Dateiname, zum speichern, Dateierweiterungen ...)</param>
        public ListeViewModel(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, string[], string> chooseFile, Action<string, Exception> showError) : 
            base(popup, confirm, confirmYesNoCancel, chooseFile, showError)
        {
            if (Global.SelectedHeld != null)
                selectedHeld = Global.SelectedHeld;
            LoadDaten();
        }

        public Held SelectedHeld {
            get { return selectedHeld; }
            set {
                selectedHeld = value;
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }

        private void SelectedHeldChanged()
        {
            SelectedHeld = Global.SelectedHeld;
            if (SelectedHeld != null)
                SelectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
        }
        
        private void SelectedHeldChanging()
        {
            if (Global.SelectedHeld != null)
            {
                if(hasChanges)
                    Global.ContextHeld.Update<Model.Held>(SelectedHeld);
                hasChanges = false;
                Global.SelectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
            }
        }

        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            //if (new string[] { "Name", "BildLink", "Rasse", "Kultur", "Profession", "AktiveHeldengruppe" }.Contains(args.PropertyName))
            //    hasChanges = true;
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

        public void LoadDaten()
        {
            Guid tmp = (SelectedHeld==null)?Guid.Empty:SelectedHeld.HeldGUID;
            SelectedHeld = null;
            if (Global.ContextHeld != null)
            {
                HeldListe = Global.ContextHeld.Liste<Held>().OrderBy(h => h.Name).ToList();
                if(tmp != Guid.Empty)
                    SelectedHeld = HeldListe.Where(h => h.HeldGUID == tmp).FirstOrDefault();
            }
        }

        private Base.CommandBase onNewHeld = null;
        public Base.CommandBase OnNewHeld
        {
            get {
                if(onNewHeld == null)
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
            get {
                if(onDeleteHeld == null)
                    onDeleteHeld = new Base.CommandBase(DeleteHeld, null);
                return onDeleteHeld; 
            }
        }

        private void DeleteHeld(object sender)
        {
            Held h = SelectedHeld;
            if (h != null)
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
                string pfad = ChooseFile("Held exportieren", h.Name, true, "xml");
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
            get {
                if(onExportDemoHelden == null)
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
                string fileName = System.IO.Path.Combine("Daten\\Helden\\Demohelden", System.IO.Path.ChangeExtension(h.Name, "xml"));
                h.Export(fileName, true);
            }
            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
        }

        private Base.CommandBase onImportDemoHelden = null;
        public Base.CommandBase OnImportDemoHelden
        {
            get {
                if(onImportDemoHelden == null)
                    onImportDemoHelden = new Base.CommandBase(ImportDemoHelden, null);
                return onImportDemoHelden; 
            }
        }
        private void ImportDemoHelden(object sender)
        {
            if (!System.IO.Directory.Exists("Daten\\Helden\\Demohelden"))
                return;
            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
            foreach (string fileName in System.IO.Directory.EnumerateFiles("Daten\\Helden\\Demohelden", "*.xml", System.IO.SearchOption.TopDirectoryOnly))
            {
                Held h = Held.Import(fileName, true);
            }
            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
            LoadDaten();
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
            string pfad = ChooseFile("Held importieren", "", false, "xml");
            if (pfad != null)
            {
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
            }

        }
        public Held ImportHeld(string pfad)
        {
            Guid hGuid = Guid.Empty;
            Held importHeld = null;
            if(!System.IO.File.Exists(pfad))
                throw new System.IO.FileNotFoundException("Die Datei konnte nicht gefunden werden.", pfad);
            bool isHeldenSoftware = false;
            if (Logic.HeldenImport.HeldenSoftwareImporter.IsHeldenSoftwareFile(pfad))
            {
                hGuid = Logic.HeldenImport.HeldenSoftwareImporter.GetGuidFromFile(pfad);
                isHeldenSoftware = true;
            }
            else if (Model.Service.SerializationService.IsMeistergeisterFile(pfad))
            {
                Held h = Model.Service.SerializationService.DeserializeObjectFromFile<Held>(pfad);
                hGuid = h.HeldGUID;
            }
            else
                throw new ArgumentException(String.Format("Die Datei {0} ist in keinem bekannten Format", pfad));
            Held existing = null;
            bool overwrite = true;
            if ((existing = Global.ContextHeld.Liste<Held>().Where(hl => hl.HeldGUID == hGuid).FirstOrDefault()) != null)
            {
                //überschreiben?
                int result = ConfirmYesNoCancel(
                    "Held importieren",
                    String.Format( "Es existiert bereits der Held \"{1}\" mit der Guid {0} soll dieser überschrieben werden?\n\nBei \"Nein\" wird eine Kopie mit einer neuen Guid angelegt.", hGuid, existing.Name)
                );
                if (result == 0)
                    return null;
                else if (result == 1)
                    overwrite = false;
            }
            if (isHeldenSoftware)
                importHeld = Logic.HeldenImport.HeldenSoftwareImporter.ImportHeldenSoftwareFile(pfad, overwrite?Guid.Empty:Guid.NewGuid());
            else
                importHeld = Held.Import(pfad, overwrite?Guid.Empty:Guid.NewGuid());
            LoadDaten();
            return importHeld;
        }


        private Base.CommandBase onCloneHeld = null;
        public Base.CommandBase OnCloneHeld
        {
            get {
                if(onCloneHeld == null)
                    onCloneHeld = new Base.CommandBase(CloneHeld, null);
                return onCloneHeld;
            }
        }
        private void CloneHeld(object sender)
        {
            Held h = SelectedHeld.Clone();
            LoadDaten();
        }
    }
}
