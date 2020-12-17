using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MeisterGeister.Daten;
using MeisterGeister.View.General;
//Eigene usings
using MeisterGeister.ViewModel.MeisterSpicker.Logic;
using Base = MeisterGeister.ViewModel.Base;

namespace MeisterGeister.ViewModel.MeisterSpicker
{
    public class MeisterSpickerViewModel : Base.ToolViewModelBase
    {

        #region //---- Variablen ----

        private OleDbCommand _cmd = new OleDbCommand();
        public OleDbCommand cmd
        {
            get { return _cmd; }
            set { Set(ref _cmd, value); }
        }

        private OleDbConnection _con = new OleDbConnection();
        public OleDbConnection con
        {
            get { return _con; }
            set { Set(ref _con, value); }
        }

        private OleDbDataAdapter _da = new OleDbDataAdapter();
        public OleDbDataAdapter da
        {
            get { return _da; }
            set { Set(ref _da, value); }
        }

        #endregion
        #region //---- FELDER ----

        // Felder
        private string _suchTextGegenstand = string.Empty;
        private string _suchTextBeschreibung = string.Empty;

        // Listen
        private List<Model.Handelsgut> _handelsgutListe;
        private List<Model.Waffe> _waffeListe;
        private List<Model.Fernkampfwaffe> _fernkampfwaffeListe;
        private List<Model.Schild> _schildListe;
        private List<Model.Rüstung> _rüstungListe;
        private List<MeisterSpickerItem> _MeisterSpickerItemListe;
        private List<MeisterSpickerItem> _filteredMeisterSpickerItemListe;

        //Commands

        #endregion

        #region //---- EIGENSCHAFTEN ----

        private DataTable _spickerdatatable = new DataTable();
        public DataTable SpickerDatatable
        {
            get { return _spickerdatatable; }
            set { Set(ref _spickerdatatable, value); }
        }


        private DataGrid _spickerData = new DataGrid();
        public DataGrid SpickerData
        {
            get { return _spickerData; }
            set { Set(ref _spickerData, value); }
        }

        private DataRowView _spickerDataViewSelected = null;
        public DataRowView SpickerDataViewSelected
        {
            get { return _spickerDataViewSelected; }
            set { Set(ref _spickerDataViewSelected, value); }
        }

        private DataView _spickerDataView = new DataView();
        public DataView SpickerDataView
        {
            get { return _spickerDataView; }
            set { Set(ref _spickerDataView, value); }
        }

        public string SuchTextGegenstand
        {
            get { return _suchTextGegenstand; }
            set
            {
                Set(ref _suchTextGegenstand, value);
                FilterListe();
            }
        }
        public string SuchTextBeschreibung
        {
            get { return _suchTextBeschreibung; }
            set
            {
                Set(ref _suchTextBeschreibung, value);
                FilterListe();
            }
        }

        #region //---- LISTEN ----

        public List<Model.Handelsgut> HandelsgutListe
        {
            get { return _handelsgutListe; }
            set
            {
                _handelsgutListe = value;
                OnChanged("HandelsgutListe");
            }
        }

        public List<Model.Waffe> WaffeListe
        {
            get { return _waffeListe; }
            set
            {
                _waffeListe = value;
                OnChanged("WaffeListe");
            }
        }

        public List<Model.Fernkampfwaffe> FernkampfwaffeListe
        {
            get { return _fernkampfwaffeListe; }
            set
            {
                _fernkampfwaffeListe = value;
                OnChanged("FernkampfwaffeListe");
            }
        }

        public List<Model.Schild> SchildListe
        {
            get { return _schildListe; }
            set
            {
                _schildListe = value;
                OnChanged("SchildListe");
            }
        }

        public List<Model.Rüstung> RüstungListe
        {
            get { return _rüstungListe; }
            set
            {
                _rüstungListe = value;
                OnChanged("RüstungListe");
            }
        }

        public List<MeisterSpickerItem> MeisterSpickerItemListe
        {
            get { return _MeisterSpickerItemListe; }
            set
            {
                _MeisterSpickerItemListe = value;
                OnChanged("MeisterSpickerItemListe");
            }
        }

        public List<MeisterSpickerItem> FilteredMeisterSpickerItemListe
        {
            get { return _filteredMeisterSpickerItemListe; }
            set
            {
                _filteredMeisterSpickerItemListe = value;
                OnChanged("FilteredMeisterSpickerItemListe");
            }
        }

        #endregion

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public MeisterSpickerViewModel()
        {
            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Refresh()
        {
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SpickerDatatable = dt;
            SpickerDataView = SpickerDatatable.DefaultView;
        }

        public void Init()
        {
            loaddata();
        }

        /// <summary>
        /// Filtert die BasarItem-Liste auf Basis des SuchTextes.
        /// </summary>
        private void FilterListe()
        {
            SpickerDatatable.Clear();
            string suchText1 = _suchTextGegenstand.ToLower().Trim();
            string[] suchWorte1 = suchText1.Split(' ');

            string suchText2 = _suchTextBeschreibung.ToLower().Trim();
            string[] suchWorte2 = suchText2.Split(' ');


            if (suchWorte1.Length == 0 || (suchWorte1.Length == 1 && suchWorte1[0] == ""))
                cmd.CommandText = "SELECT * FROM Tabelle1";
            else
                cmd.CommandText = "SELECT * FROM Tabelle1 WHERE Gegenstand LIKE '%" +
                    (suchWorte1.Length == 1 ? suchWorte1[0] + "%'" :
                        (suchWorte1.Length > 1 ? string.Join("%' AND Gegenstand LIKE '%", suchWorte1) + "%'" : "%'"));


            if (!(suchWorte2.Length == 0 || (suchWorte2.Length == 1 && suchWorte2[0] == "")))
            {
                if (cmd.CommandText.Contains("WHERE"))
                {
                    cmd.CommandText += " OR M1 LIKE '%" +
                        (suchWorte2.Length == 1 ? suchWorte2[0] + "%'" :
                            (suchWorte2.Length > 1 ? string.Join("%' AND M1 LIKE '%", suchWorte2) + "%'" : "%'"));
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM Tabelle1 WHERE Gegenstand LIKE '%" +
                      (suchWorte2.Length == 1 ? suchWorte2[0] + "%'" :
                          (suchWorte2.Length > 1 ? string.Join("%' AND Gegenstand LIKE '%", suchWorte2) + "%'" : "%'"));
                    cmd.CommandText += " OR M1 LIKE '%" +
                            (suchWorte2.Length == 1 ? suchWorte2[0] + "%'" :
                                (suchWorte2.Length > 1 ? string.Join("%' AND M1 LIKE '%", suchWorte2) + "%'" : "%'"));
                }
                //else
                //    cmd.CommandText = "SELECT * FROM Tabelle1 WHERE M1 LIKE '%" +
                //    (suchWorte2.Length == 1 ? suchWorte2[0] + "%'" :
                //        (suchWorte2.Length > 1 ? string.Join("%' AND M1 LIKE '%", suchWorte2) + "%'" : "%'"));
            }
            Refresh();
        }

        #endregion

        private void loaddata()
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Closed)
                    return;
                con.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + DatabaseTools.DATABASE_FOLDER + "MeisterSpicker.mdb";

                cmd = new OleDbCommand();
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select * from Tabelle1";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SpickerDatatable = dt;
                SpickerDataView = SpickerDatatable.DefaultView;
            }
            catch (Exception ex)
            {
                con.Close();
                ViewHelper.ShowError(ex.Message.ToString(), ex);
            }
        }

        #region //---- EVENTS ----

        private Base.CommandBase _onBtnDeleteSuchTextGegenstandFilter = null;
        public Base.CommandBase OnBtnDeleteSuchTextGegenstandFilter
        {
            get
            {
                if (_onBtnDeleteSuchTextGegenstandFilter == null)
                    _onBtnDeleteSuchTextGegenstandFilter = new Base.CommandBase(DeleteSuchTextGegenstandFilter, null);
                return _onBtnDeleteSuchTextGegenstandFilter;
            }
        }
        void DeleteSuchTextGegenstandFilter(object obj)
        {
            SuchTextGegenstand = string.Empty;
        }

        private Base.CommandBase _onBtnDeleteSuchTextBeschreibungFilter = null;
        public Base.CommandBase OnBtnDeleteSuchTextBeschreibungFilter
        {
            get
            {
                if (_onBtnDeleteSuchTextBeschreibungFilter == null)
                    _onBtnDeleteSuchTextBeschreibungFilter = new Base.CommandBase(DeleteSuchTextBeschreibungFilter, null);
                return _onBtnDeleteSuchTextBeschreibungFilter;
            }
        }
        void DeleteSuchTextBeschreibungFilter(object obj)
        {
            SuchTextBeschreibung = string.Empty;
        }
        #endregion
    }
}
