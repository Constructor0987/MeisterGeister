using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene usings
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using System.Collections.ObjectModel;

namespace MeisterGeister.ViewModel.Notiz
{
    public class NotizViewModel : Base.ToolViewModelBase
    {
        #region //SUBKLASSEN

        public class NotizItem : Base.ViewModelBase
        {
            #region //FELDER

            //Intern
            private Model.Notizen _entityNotiz;

            //UI
            private System.Windows.Visibility _isEditable = System.Windows.Visibility.Collapsed;
            private System.Windows.Visibility _isEditMode = System.Windows.Visibility.Collapsed;

            //Commands
            private Base.CommandBase _onDeleteNotiz;
            private Base.CommandBase _onAddNewNotiz;
            private Base.CommandBase _onEditNotiz;

            #endregion

            #region //EIGENSCHAFTEN


            //Intern
            public Model.Notizen EntityNotiz { get { return _entityNotiz; } set { _entityNotiz = value; OnChanged("EntityNotiz"); } }

            // UI
            public System.Windows.Visibility IsNewTab
            {
                get
                {
                    if (EntityNotiz == null)
                        return System.Windows.Visibility.Visible;
                    return System.Windows.Visibility.Collapsed;
                }
            }

            public System.Windows.Visibility IsDeletable
            {
                get
                {
                    if (EntityNotiz == null)
                        return System.Windows.Visibility.Collapsed;
                    return EntityNotiz.NotToDelete == true ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                }
            }

            public System.Windows.Visibility IsEditable
            {
                get { return _isEditable; }
                set
                {
                    _isEditable = value;
                    if (value == System.Windows.Visibility.Collapsed)
                        IsEditMode = System.Windows.Visibility.Collapsed;
                    OnChanged("IsEditable");
                }
            }

            public System.Windows.Visibility IsEditMode
            {
                get { return _isEditMode; }
                set { _isEditMode = value; OnChanged("IsEditMode"); OnChanged("IsNotEditMode"); }
            }

            public System.Windows.Visibility IsNotEditMode
            {
                get { if (_isEditMode == System.Windows.Visibility.Visible) return System.Windows.Visibility.Collapsed; else return System.Windows.Visibility.Visible; }
            }

            //Commands
            public Base.CommandBase OnDeleteNotiz
            {
                get { return _onDeleteNotiz; }
            }
            public Base.CommandBase OnAddNewNotiz
            {
                get { return _onAddNewNotiz; }
            }
            public Base.CommandBase OnEditNotiz
            {
                get { return _onEditNotiz; }
            }

            #endregion

            #region //KONSTRUKTOR

            public NotizItem(Model.Notizen notiz)
            {
                EntityNotiz = notiz;

                _onDeleteNotiz = new Base.CommandBase(DeleteNotiz, null);
                _onAddNewNotiz = new Base.CommandBase(AddNewNotiz, null);
                _onEditNotiz = new Base.CommandBase(EditNotiz, null);
            }

            #endregion

            #region //EVENTS

            public event EventHandler DeleteNotizItem;
            void DeleteNotiz(object sender)
            {
                if (DeleteNotizItem != null)
                {
                    DeleteNotizItem(this, new EventArgs());
                }
            }

            public event EventHandler AddNewNotizItem;
            void AddNewNotiz(object sender)
            {
                if (AddNewNotizItem != null)
                {
                    AddNewNotizItem(this, new EventArgs());
                }
            }

            void EditNotiz(object sender)
            {
                // Toggle Edit-Mode
                if (IsEditMode == System.Windows.Visibility.Collapsed)
                    IsEditMode = System.Windows.Visibility.Visible;
                else
                    IsEditMode = System.Windows.Visibility.Collapsed;
            }

            #endregion

            public double VerticalOffset { get; set; }
        }

        #endregion

        #region //---- FELDER ----

        //Felder

        //Listen + SelectedItems
        private NotizItem _selectedNotiz;
        private ObservableCollection<NotizItem> _notizListe;

        // Commands


        #endregion

        #region //---- EIGENSCHAFTEN ----


        public NotizItem SelectedNotiz
        {
            get { return _selectedNotiz; }
            set
            {
                _selectedNotiz = value;
                OnChanged("SelectedNotiz");
                foreach (var item in NotizListe)
                {
                    if (item.EntityNotiz == null)
                        item.IsEditable = System.Windows.Visibility.Collapsed;
                    else
                        item.IsEditable = item.EntityNotiz.NotToDelete == false && item == value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
            }
        }

        //Listen
        public ObservableCollection<NotizItem> NotizListe
        {
            get { return _notizListe; }
            set
            {
                _notizListe = value;
                OnChanged("NotizListe");
            }
        }

        // Commands


        #endregion

        #region //---- KONSTRUKTOR ----

        public NotizViewModel()
        {

        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            int selectedNotizID = _selectedNotiz != null ? _selectedNotiz.EntityNotiz.NotizID : -1;
            NotizListe = new ObservableCollection<NotizItem>();

            NotizItem tmpNotiz;
            foreach (var item in Global.ContextNotizen.NotizListe)
            {
                tmpNotiz = new NotizItem(item);
                tmpNotiz.DeleteNotizItem += (s, e) => { DeleteNotiz(s); };
                NotizListe.Add(tmpNotiz);
            }

            // Dummy-Notiz einfügen, für AddNewTab-Funktionalität
            tmpNotiz = new NotizItem(null);
            tmpNotiz.AddNewNotizItem += (s, e) => { AddNotiz(s); };
            NotizListe.Add(tmpNotiz);

            // zuletzt gewählte Notiz erneut auswählen
            if (selectedNotizID != -1 && NotizListe.Count > 0)
                SelectedNotiz = NotizListe.Where(n => (n.EntityNotiz != null ? n.EntityNotiz.NotizID : -1) == selectedNotizID).SingleOrDefault();
        }

        #endregion

        #region //---- EVENTS ----

        void DeleteNotiz(object sender)
        {
            if (sender is NotizItem)
            {
                if (System.Windows.MessageBox.Show("Soll die Notiz tatsächlich gelöscht werden?", "Notiz", System.Windows.MessageBoxButton.YesNoCancel)
                    == System.Windows.MessageBoxResult.Yes)
                {
                    NotizItem item = NotizListe.Where(value => value == (sender as NotizItem)).FirstOrDefault();
                    if (item != null)
                        NotizListe.Remove(item);
                    Global.ContextNotizen.Delete<Model.Notizen>(item.EntityNotiz);
                }
            }
        }

        void AddNotiz(object sender)
        {
            if (sender is NotizItem)
            {
                var inputView = new MeisterGeister.View.Windows.InputWindow();
                inputView.Title = "Neue Notiz";
                inputView.Beschreibung = "Bitte einen Namen für die Notiz eingeben.";
                inputView.ShowDialog();
                if (inputView.OK_Click && inputView.Wert.Trim() != string.Empty)
                {
                    // neue Notiz erzeugen
                    var newNotiz = new Model.Notizen();
                    newNotiz.Name = inputView.Wert;
                    newNotiz.NotToDelete = false;
                    Global.ContextNotizen.Insert<Model.Notizen>(newNotiz);

                    // Notizen neu laden
                    LoadDaten();

                    // Neue Notiz auswählen
                    SelectedNotiz = NotizListe.Where(n => n.EntityNotiz == newNotiz).FirstOrDefault();
                }
            }
        }


        #endregion

    }
}