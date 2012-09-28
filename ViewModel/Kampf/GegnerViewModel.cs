using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Kampf.Logic;

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

        #region Properties

        private GegnerBase selectedGegnerBase;
        public GegnerBase SelectedGegnerBase
        {
            get { return selectedGegnerBase; }
            set { 
                selectedGegnerBase = value;
                OnChanged("SelectedGegnerBase");
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

        private Kampfregel selectedAddKampfregel;
        public Kampfregel SelectedAddKampfregel
        {
            get { return selectedAddKampfregel; }
            set { 
                selectedAddKampfregel = value;
                OnChanged("SelectedAddKampfregel");
            }
        }
        #endregion

        #region Import/Export/New

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
    }

}
