using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using System.ComponentModel;

using MeisterGeister.Logic.HeldenImport;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.View.Windows;
using MeisterGeister.Logic.Extensions;
using System.Windows.Data;

namespace MeisterGeister.ViewModel.Helden
{
    public class ListeViewModel : Base.ViewModelBase
    {
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
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;
            MainViewModel.Instance.PropertyChanged += MainViewModel_PropertyChanged;
        }
        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged -= IsReadOnlyChanged;
            MainViewModel.Instance.PropertyChanged -= MainViewModel_PropertyChanged;
        }

        void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedHeld")
                SelectedHeld = MainViewModel.Instance.SelectedHeld;
        }

        Held selectedHeld = null;
        public Held SelectedHeld
        {
            get { return selectedHeld; }
            set
            {
                if (selectedHeld != null && !object.Equals(selectedHeld, value))
                    selectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
                if (Set(ref selectedHeld, value) && value != null)
                {
                    selectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
                    MainViewModel.Instance.SelectedHeld = selectedHeld;
                }
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

        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            //if (new string[] { "Name", "Spieler", "AktiveHeldengruppe" }.Contains(args.PropertyName))
            //    SortList();
        }

        private CollectionViewSource heldListe = null;
        public ICollectionView HeldListe
        {
            get {
                if (heldListe == null)
                {
                    heldListe = new CollectionViewSource() { Source = MainViewModel.Instance.Helden };
                    heldListe.Filter += filterHeldengruppe;
                    heldListe.SortDescriptions.Add(new SortDescription("AktiveHeldengruppe", ListSortDirection.Descending));
                }
                return heldListe.View;
            }
        }

        private void filterHeldengruppe(object sender, FilterEventArgs f)
        {
            Model.Held h = (Model.Held)f.Item;
            f.Accepted = (h.Regelsystem == Global.Regeledition) && !(h.HeldGUID.ToString("D").ToUpperInvariant().StartsWith("00000000-0000-0000-045C"));
        }

        /// <summary>
        /// Gibt die Anzahl an Helden zurück, die in einer anderen Regeledition vorliegen.
        /// </summary>
        public int HeldenAnzahlAnderesRegelsystem
        {
            get { return Global.ContextHeld.Liste<Held>().Where(h => h.Regelsystem != Global.Regeledition).Count(); }
        }

        List<string> _sortierungListe = new List<string>() { "Held", "Spieler" };
        public List<string> SortierungListe
        {
            get { return _sortierungListe; }
            set { _sortierungListe = value; OnChanged("SortierungListe"); }
        }

        private SortDescription nameSort = new SortDescription("Name", ListSortDirection.Ascending);
        private SortDescription spielerSort = new SortDescription("Spieler", ListSortDirection.Ascending);
        private string _selectedSortierung = "Held";
        public string SelectedSortierung
        {
            get { return _selectedSortierung; }
            set
            {
                _selectedSortierung = value;
                OnChanged("SelectedSortierung");
                //Liste umsortieren
                if (_selectedSortierung == "Held")
                {
                    HeldListe.SortDescriptions.Remove(spielerSort);
                    HeldListe.SortDescriptions.Add(nameSort);
                }
                else
                {
                    HeldListe.SortDescriptions.Remove(nameSort);
                    HeldListe.SortDescriptions.Add(spielerSort);
                }
            }
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
            h.Regelsystem = Global.Regeledition;
            h.AddBasisTalente();
            if (Global.ContextHeld.Insert<Held>(h))
            {
                //Liste aktualisieren
                MainViewModel.Instance.Helden.Add(h);
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
                    MainViewModel.Instance.Helden.Remove(h);
                    SelectedHeld = HeldListe.SourceCollection.Cast<Held>().FirstOrDefault();
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
            if (!IsReadOnly && Confirm("Alle Helden löschen", string.Format("Sind Sie sicher, dass Sie alle Helden ({0}) endgültig löschen möchten?", HeldListe.SourceCollection.Cast<Held>().Count() )))
            {
                Global.SetIsBusy(true, "Alle Helden werden gelöscht...");

                Global.ContextHeld.DeleteAll<Held>();
                MainViewModel.Instance.Helden.Clear();
                
                //Liste aktualisieren
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
            foreach (Held h in HeldListe.SourceCollection.Cast<Held>())
            {
                string fileName = System.IO.Path.Combine("Daten\\Helden\\Demohelden", View.General.ViewHelper.GetValidFilename(System.IO.Path.ChangeExtension(h.Name, "xml")));
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
            MainViewModel.Instance.InvalidateHelden();

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
            {
                HeldenImportResult heldenImportResult = HeldenSoftwareImporter.ImportHeldenSoftwareFile(pfad, overwrite ? Guid.Empty : Guid.NewGuid());
                importHeld = heldenImportResult.Held;
                HeldenSoftwareImporter.ShowLogWindow(heldenImportResult);
            }
            else if (isHeldenblatt)
                importHeld = HeldenblattImporter.ImportHeldenblattFile(pfad);
            else
                importHeld = Held.Import(pfad, overwrite ? Guid.Empty : Guid.NewGuid());

            if (existing != null && overwrite && (isHeldenblatt || isHeldenSoftware))
            { // MeisterGeister spezifische Daten beim Reimport übernehmen
                importHeld.Spieler = existing.Spieler;
                importHeld.Notizen = existing.Notizen;
                importHeld.Kampfwerte = existing.Kampfwerte;
                if (importHeld.CheckEnergieständeAbwechend(existing) 
                    && ConfirmYesNoCancel("Energiestände & Wunden", "Sollen die aktuellen Energiestände und Wunden beibehalten werden?") == 2)
                {
                    importHeld.LebensenergieAktuell = existing.LebensenergieAktuell;
                    importHeld.AusdauerAktuell = existing.AusdauerAktuell;
                    importHeld.AstralenergieAktuell = existing.AstralenergieAktuell;
                    importHeld.KarmaenergieAktuell = existing.KarmaenergieAktuell;
                    importHeld.Wunden = existing.Wunden;
                    importHeld.WundenArmL = existing.WundenArmL;
                    importHeld.WundenArmR = existing.WundenArmR;
                    importHeld.WundenBauch = existing.WundenBauch;
                    importHeld.WundenBeinL = existing.WundenBeinL;
                    importHeld.WundenBeinR = existing.WundenBeinR;
                    importHeld.WundenBrust = existing.WundenBrust;
                    importHeld.WundenKopf = existing.WundenKopf;
                }
            }

            MainViewModel.Instance.InvalidateHelden();
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
            MainViewModel.Instance.InvalidateHelden();

            Global.SetIsBusy(false);
        }

        private Base.CommandBase onClearUpdateHinweis = null;
        public Base.CommandBase OnClearUpdateHinweis
        {
            get
            {
                if (onClearUpdateHinweis == null)
                    onClearUpdateHinweis = new Base.CommandBase(ClearUpdateHinweis, null);
                return onClearUpdateHinweis;
            }
        }

        private void ClearUpdateHinweis(object obj)
        {
            if (SelectedHeld != null)
                SelectedHeld.UpdateHinweis = string.Empty;
        }

        private Base.CommandBase onDownloadHelden = null;
        public Base.CommandBase OnDownloadHelden
        {
            get
            {
                if (onDownloadHelden == null)
                    onDownloadHelden = new Base.CommandBase(DownloadHelden, null);
                return onDownloadHelden;
            }
        }

        private void DownloadHelden(object obj)
        {

            string token = Einstellungen.HeldenSoftwareOnlineToken;
            bool tokenExists = !string.IsNullOrEmpty(token);

            if (!tokenExists)
            {
                token = AddToken();
                tokenExists = !string.IsNullOrEmpty(token);
            }
            if (tokenExists)
            {
                Global.SetIsBusy(true, "Helden werden heruntergeladen...");
                var syncer = new HeldenSoftwareOnlineService(token);
                try
                {
                    ICollection<HeldenImportResult> result = syncer.DownloadHelden();
                    if (result != null)
                    {
                        MsgWindow window = null;
                        window = new MsgWindow("Download beendet", "Es wurden " + result.Count() + " Helden aktualisiert.", false);
                        window.ShowDialog();
                        window.Close();
                    }
                    MainViewModel.Instance.InvalidateHelden();
                }
                catch (Exception ex)
                {
                    string msg = "Beim Aufruf der HeldenSoftware-Online ist ein Fehler aufgetreten.";
                    var errWin = new MsgWindow("Abruf Heldenliste", msg, ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
                Global.SetIsBusy(false);
                // Asynchroner Download aktuell leider nicht möglich wegen Singleton-DataContext
                //var worker = syncer.DownloadHeldenAsync();
                //worker.ProgressChanged += DownloadProgressed;
                //worker.RunWorkerCompleted += DownloadCompleted;
            }
        }

        //private void DownloadProgressed(object sender, ProgressChangedEventArgs e)
        //{
        //    LoadDaten();
        //}

        //private void DownloadCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
            
        //}

        private string AddToken()
        {
            var dialog = new InputWindow();
            dialog.Title = "Token eingeben";
            dialog.Beschreibung = "Bitte einen HeldenSoftware-Online-Token eingeben.";
            dialog.ShowDialog();
            bool defined = dialog.OK_Click;
            string tokenEntered = dialog.Wert;

            if (defined && !string.IsNullOrEmpty(tokenEntered))
                Einstellungen.HeldenSoftwareOnlineToken = tokenEntered;
            
            return tokenEntered;
        }
    }
}
