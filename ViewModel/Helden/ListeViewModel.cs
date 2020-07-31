using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.Logic.HeldenImport;
using MeisterGeister.Model;
using MeisterGeister.View.Helden;
using MeisterGeister.View.Windows;

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

        public Held SelectedHeld
        {
            get { return selectedHeld; }

            set
            {
                if (selectedHeld != null && !object.Equals(selectedHeld, value))
                {
                    selectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
                }

                if (Set(ref selectedHeld, value) && value != null)
                {
                    selectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
                    MainViewModel.Instance.SelectedHeld = selectedHeld;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        public ICollectionView HeldListe
        {
            get
            {
                if (heldListe == null)
                {
                    heldListe = new CollectionViewSource() { Source = MainViewModel.Instance.Helden };
                    heldListe.Filter += filterHeldengruppe;
                    heldListe.SortDescriptions.Add(new SortDescription("AktiveHeldengruppe", ListSortDirection.Descending));
                }
                return heldListe.View;
            }
        }

        /// <summary>
        /// Gibt die Anzahl an Helden zurück, die in einer anderen Regeledition vorliegen.
        /// </summary>
        public int HeldenAnzahlAnderesRegelsystem
        {
            get { return Global.ContextHeld.Liste<Held>().Where(h => h.Regelsystem != Global.Regeledition).Count(); }
        }

        public List<string> SortierungListe
        {
            get { return _sortierungListe; }
            set { _sortierungListe = value; OnChanged(nameof(SortierungListe)); }
        }

        public string SelectedSortierung
        {
            get { return _selectedSortierung; }

            set
            {
                _selectedSortierung = value;
                OnChanged(nameof(SelectedSortierung));
                SortHeldListe();
            }
        }

        public Base.CommandBase OnNewHeld
        {
            get
            {
                if (onNewHeld == null)
                {
                    onNewHeld = new Base.CommandBase(NewHeld, null);
                }

                return onNewHeld;
            }
        }

        public Base.CommandBase OnDeleteHeld
        {
            get
            {
                if (onDeleteHeld == null)
                {
                    onDeleteHeld = new Base.CommandBase(DeleteHeld, null);
                }

                return onDeleteHeld;
            }
        }

        public Base.CommandBase OnDeleteHeldAll
        {
            get
            {
                if (onDeleteHeldAll == null)
                {
                    onDeleteHeldAll = new Base.CommandBase(DeleteHeldAll, null);
                }

                return onDeleteHeldAll;
            }
        }

        public Base.CommandBase OnExportHeld
        {
            get
            {
                if (onExportHeld == null)
                {
                    onExportHeld = new Base.CommandBase(ExportHeld, null);
                }

                return onExportHeld;
            }
        }

        public Base.CommandBase OnExportDemoHelden
        {
            get
            {
                if (onExportDemoHelden == null)
                {
                    onExportDemoHelden = new Base.CommandBase(ExportDemoHelden, null);
                }

                return onExportDemoHelden;
            }
        }

        public Base.CommandBase OnImportDemoHelden
        {
            get
            {
                if (onImportDemoHelden == null)
                {
                    onImportDemoHelden = new Base.CommandBase(ImportDemoHelden, null);
                }

                return onImportDemoHelden;
            }
        }

        public Base.CommandBase OnImportHeld
        {
            get
            {
                if (onImportHeld == null)
                {
                    onImportHeld = new Base.CommandBase(ImportHeldCommand, null);
                }

                return onImportHeld;
            }
        }

        public Base.CommandBase OnCloneHeld
        {
            get
            {
                if (onCloneHeld == null)
                {
                    onCloneHeld = new Base.CommandBase(CloneHeld, null);
                }

                return onCloneHeld;
            }
        }

        public Base.CommandBase OnClearUpdateHinweis
        {
            get
            {
                if (onClearUpdateHinweis == null)
                {
                    onClearUpdateHinweis = new Base.CommandBase(ClearUpdateHinweis, null);
                }

                return onClearUpdateHinweis;
            }
        }

        public Base.CommandBase OnDownloadHelden
        {
            get
            {
                if (onDownloadHelden == null)
                {
                    onDownloadHelden = new Base.CommandBase(ShowHeldenDownloadWindow, null);
                }

                return onDownloadHelden;
            }
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;
            MainViewModel.Instance.PropertyChanged += MainViewModel_PropertyChanged;
        }

        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Einstellungen.IsReadOnlyChanged -= IsReadOnlyChanged;
            MainViewModel.Instance.PropertyChanged -= MainViewModel_PropertyChanged;
        }

        public void ImportHeldCommand(object sender)
        {
            var pfad = ChooseFile("Held importieren", "", false, false, "xml", "xls", "xlsx", "xlsb");
            if (pfad != null)
            {
                Global.SetIsBusy(true, string.Format("{0} wird importiert...", pfad));

#if !DEBUG
                try
                {
#endif
                Held h = ImportHeld(pfad);
                SelectedHeld = null;
                SelectedHeld = h;

                // Liste aktualisieren!
  //              heldListe = null;
  //              OnChanged(nameof(HeldListe));

                MainViewModel.Instance.InvalidateHelden();
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
            {
                throw new System.IO.FileNotFoundException("Die Datei konnte nicht gefunden werden.", pfad);
            }

            try
            {
                System.IO.FileStream fs = System.IO.File.OpenRead(pfad);
                fs.Close();
            }
            catch
            {
                throw new ArgumentException(string.Format("Die Datei {0} konnte nicht geöffnet werden.", pfad));
            }
            var isHeldenSoftware = false;
            var isHeldenblatt = false;
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
            {
                throw new ArgumentException(string.Format("Die Datei {0} ist in keinem bekannten Format", pfad));
            }

            Held existing = null;
            var overwrite = true;
            if (!isHeldenblatt && (existing = Global.ContextHeld.Liste<Held>().Where(hl => hl.HeldGUID == hGuid).FirstOrDefault()) != null)
            {
                //überschreiben?
                var result = ConfirmYesNoCancel(
                    "Held importieren",
                    string.Format("Es existiert bereits der Held \"{1}\" mit der Guid {0}. Soll dieser überschrieben werden?\n\nBei \"Nein\" wird eine Kopie mit einer neuen Guid angelegt.", hGuid, existing.Name)
                );
                if (result == 0)
                {
                    return null;
                }
                else if (result == 1)
                {
                    overwrite = false;
                }
            }
            if (isHeldenSoftware)
            {
                HeldenImportResult heldenImportResult = HeldenSoftwareImporter.ImportHeldenSoftwareFile(pfad, overwrite ? Guid.Empty : Guid.NewGuid());
                importHeld = heldenImportResult.Held;
                HeldenSoftwareImporter.ShowLogWindow(heldenImportResult);
            }
            else if (isHeldenblatt)
            {
                importHeld = HeldenblattImporter.ImportHeldenblattFile(pfad);
            }
            else
            {
                importHeld = Held.Import(pfad, overwrite ? Guid.Empty : Guid.NewGuid());
            }

            if (existing != null && overwrite && (isHeldenblatt || isHeldenSoftware))
            { 
                // MeisterGeister spezifische Daten beim Reimport übernehmen
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
                SelectedHeld = existing;

                //lstPflanze.AddRange(existing.Held_Pflanze.Select(t => t.Pflanze));


                //if (lstPflanze.Count > 0)
                //{
                //    while (existing.Held_Pflanze.Count > 0)
                //    {
                //        Held_Pflanze hp = existing.Held_Pflanze.ToList()[0];
                //        existing.Held_Pflanze.Remove(hp);
                //        Global.ContextHeld.UpdateList<Held_Pflanze>();
                //        importHeld.Held_Pflanze.Add(hp);
                //    }
                //}
                DeleteHeld(true);
            //    Global.ContextHeld.Save();
            }

            if (overwrite)
            {
                MainViewModel.Instance.Helden.Add(importHeld);
            }
                
            HeldListe.Refresh();

            MainViewModel.Instance.HeldenGruppe.Refresh();
            SortHeldListe();
          //  importHeld.UpdateLists;

            return importHeld;
        }

        private Held selectedHeld = null;

        private bool _isReadOnly = Einstellungen.IsReadOnly;

        private CollectionViewSource heldListe = null;

        private List<string> _sortierungListe = new List<string>() { "Held", "Spieler" };

        private SortDescription nameSort = new SortDescription("Name", ListSortDirection.Ascending);

        private SortDescription spielerSort = new SortDescription("Spieler", ListSortDirection.Ascending);

        private string _selectedSortierung = "Held";

        private Base.CommandBase onNewHeld = null;

        private Base.CommandBase onDeleteHeld = null;

        private Base.CommandBase onDeleteHeldAll = null;

        private Base.CommandBase onExportHeld = null;

        private Base.CommandBase onExportDemoHelden = null;

        private Base.CommandBase onImportDemoHelden = null;

        private Base.CommandBase onImportHeld = null;

        private Base.CommandBase onCloneHeld = null;

        private Base.CommandBase onClearUpdateHinweis = null;

        private Base.CommandBase onDownloadHelden = null;

        private void SortHeldListe()
        {
            if (SelectedSortierung == "Held")
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

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedHeld))
            {
                SelectedHeld = MainViewModel.Instance.SelectedHeld;
            }
        }

        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = Einstellungen.IsReadOnly;
            OnChanged(nameof(IsReadOnly));
        }

        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
        }

        private void filterHeldengruppe(object sender, FilterEventArgs f)
        {
            var h = (Held)f.Item;
            f.Accepted = (h.Regelsystem == Global.Regeledition) && !(h.HeldGUID.ToString("D").ToUpperInvariant().StartsWith("00000000-0000-0000-045C"));
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
                HeldListe.Refresh();
                SortHeldListe();
            }
        }

        private void DeleteHeld(object sender)
        {
            Held h = SelectedHeld;
            if (h != null && 
                (!IsReadOnly || ((sender is bool) && ((bool)sender) == true)))
            {
                if (((sender is bool) && ((bool)sender) == true) || Confirm("Held löschen", string.Format("Sind Sie sicher, dass Sie den Helden '{0}' löschen möchten?", h.Name)))
                {
                    if (!Global.ContextHeld.Delete<Held>(h))
                        Global.ContextHeld.Delete<Held>((Held)(MainViewModel.Instance.HeldenGruppe).CurrentItem);//;((MainViewModel.Instance.HeldenGruppe).SourceCollection as List<Held>).FirstOrDefault(t => t.Name == h.Name));
                    //Liste aktualisieren                    
                    MainViewModel.Instance.Helden.Remove(h);
                    HeldListe.Refresh();
                    MainViewModel.Instance.HeldenGruppe.Refresh();
                    SelectedHeld = null;
                    SelectedHeld = HeldListe.SourceCollection.Cast<Held>().FirstOrDefault();
                }
            }
        }

        private void DeleteHeldAll(object sender)
        {
            if (!IsReadOnly && Confirm("Alle Helden löschen", string.Format("Sind Sie sicher, dass Sie alle Helden ({0}) endgültig löschen möchten?", HeldListe.SourceCollection.Cast<Held>().Count())))
            {
                Global.SetIsBusy(true, "Alle Helden werden gelöscht...");

                Global.ContextHeld.DeleteAll<Held>();
                MainViewModel.Instance.Helden.Clear();
                HeldListe.Refresh();
                MainViewModel.Instance.HeldenGruppe.Refresh();

                SelectedHeld = null;

                Global.SetIsBusy(false);
            }
        }

        // public string ExportHeld(string pfad)
        private void ExportHeld(object sender)
        {
            Held h = SelectedHeld;
            if (h != null)
            {
                var pfad = ChooseFile("Held exportieren", h.Name, true, false, "xml");
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

        private void ExportDemoHelden(object sender)
        {
            if (!System.IO.Directory.Exists("Daten\\Helden\\Demohelden"))
            {
                System.IO.Directory.CreateDirectory("Daten\\Helden\\Demohelden");
            }

            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
            foreach (Held h in HeldListe.SourceCollection.Cast<Held>())
            {
                var fileName = System.IO.Path.Combine("Daten\\Helden\\Demohelden", View.General.ViewHelper.GetValidFilename(System.IO.Path.ChangeExtension(h.Name, "xml")));
                h.Export(fileName, true);
            }
            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
        }

        private void ImportDemoHelden(object sender)
        {
            if (!System.IO.Directory.Exists("Daten\\Helden\\Demohelden"))
            {
                return;
            }

            Global.SetIsBusy(true, "Demo-Helden werden importiert...");

            MeisterGeister.Model.Service.SerializationService.DestroyInstance();
            foreach (var fileName in System.IO.Directory.EnumerateFiles("Daten\\Helden\\Demohelden", "*.xml", System.IO.SearchOption.TopDirectoryOnly))
            {
                var h = Held.Import(fileName, true);
                MainViewModel.Instance.Helden.Add(h);
            }
            Model.Service.SerializationService.DestroyInstance();
            Held.UpdateLists();
            HeldListe.Refresh();

            Global.SetIsBusy(false);
        }

        private void CloneHeld(object sender)
        {
            Global.SetIsBusy(true, "Held wird kopiert...");

            Held h = SelectedHeld.Clone();
            MainViewModel.Instance.Helden.Add(h);
            HeldListe.Refresh();
            SortHeldListe();
            Global.SetIsBusy(false);
        }

        private void ClearUpdateHinweis(object obj)
        {
            if (SelectedHeld != null)
            {
                SelectedHeld.UpdateHinweis = string.Empty;
            }
        }

        private void ShowHeldenDownloadWindow(object obj)
        {
            var token = Einstellungen.HeldenSoftwareOnlineToken;
            var isTokenValid = IsTokenValid(token);

            if (!isTokenValid)
            {
                token = AddToken();
                isTokenValid = IsTokenValid(token);
            }
            if (isTokenValid)
            {
                var downloadHeldenViewModel = new DownloadHeldenViewModel(token);
                var downloadHeldenView = new DownloadHeldenView
                {
                    DataContext = downloadHeldenViewModel
                };
                downloadHeldenView.ShowDialog();
                MainViewModel.Instance.InvalidateHelden();
            }
        }

        private bool IsTokenValid(string token)
        {
            return !string.IsNullOrEmpty(token);
        }

        private string AddToken()
        {
            var dialog = new InputWindow
            {
                Title = "Token eingeben",
                Beschreibung = "Bitte einen HeldenSoftware-Online-Token eingeben."
            };
            dialog.ShowDialog();
            var defined = dialog.OK_Click;
            var tokenEntered = dialog.Wert;

            if (defined && !string.IsNullOrEmpty(tokenEntered))
            {
                Einstellungen.HeldenSoftwareOnlineToken = tokenEntered;
            }

            return tokenEntered;
        }
    }
}
