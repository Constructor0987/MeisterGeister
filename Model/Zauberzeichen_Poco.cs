//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace MeisterGeister.Model
{
    [DataContract(IsReference=true)]
    public partial class Zauberzeichen : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
    	public event PropertyChangedEventHandler PropertyChanged;
    	
    	public void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region Primitive Properties
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid ZauberzeichenGUID
        {
            get { return _zauberzeichenGUID; }
            set
    		{ 
    			_zauberzeichenGUID = value;
    			OnChanged("ZauberzeichenGUID");
    		}
    
        }
        private System.Guid _zauberzeichenGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Name
        {
            get { return _name; }
            set
    		{ 
    			_name = value;
    			OnChanged("Name");
    		}
    
        }
        private string _name;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Typ
        {
            get { return _typ; }
            set
    		{ 
    			_typ = value;
    			OnChanged("Typ");
    		}
    
        }
        private string _typ;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid SonderfertigkeitGUID
        {
            get { return _sonderfertigkeitGUID; }
            set
            {
                if (_sonderfertigkeitGUID != value)
                {
                    if (Sonderfertigkeit != null && Sonderfertigkeit.SonderfertigkeitGUID != value)
                    {
                        Sonderfertigkeit = null;
                    }
                    _sonderfertigkeitGUID = value;
                }
            }
    
        }
        private System.Guid _sonderfertigkeitGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Lernkosten
        {
            get { return _lernkosten; }
            set
    		{ 
    			_lernkosten = value;
    			OnChanged("Lernkosten");
    		}
    
        }
        private int _lernkosten;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Komplexität
        {
            get { return _komplexität; }
            set
    		{ 
    			_komplexität = value;
    			OnChanged("Komplexität");
    		}
    
        }
        private int _komplexität;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Merkmal
        {
            get { return _merkmal; }
            set
    		{ 
    			_merkmal = value;
    			OnChanged("Merkmal");
    		}
    
        }
        private string _merkmal;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual double ReichweitenDivisor
        {
            get { return _reichweitenDivisor; }
            set
    		{ 
    			_reichweitenDivisor = value;
    			OnChanged("ReichweitenDivisor");
    		}
    
        }
        private double _reichweitenDivisor;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Bemerkung
        {
            get { return _bemerkung; }
            set
    		{ 
    			_bemerkung = value;
    			OnChanged("Bemerkung");
    		}
    
        }
        private string _bemerkung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Verbreitung
        {
            get { return _verbreitung; }
            set
    		{ 
    			_verbreitung = value;
    			OnChanged("Verbreitung");
    		}
    
        }
        private string _verbreitung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Komponenten
        {
            get { return _komponenten; }
            set
    		{ 
    			_komponenten = value;
    			OnChanged("Komponenten");
    		}
    
        }
        private string _komponenten;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Literatur
        {
            get { return _literatur; }
            set
    		{ 
    			_literatur = value;
    			OnChanged("Literatur");
    		}
    
        }
        private string _literatur;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Setting
        {
            get { return _setting; }
            set
    		{ 
    			_setting = value;
    			OnChanged("Setting");
    		}
    
        }
        private string _setting;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Sonderfertigkeit Sonderfertigkeit
        {
            get { return _sonderfertigkeit; }
            set
            {
                if (!ReferenceEquals(_sonderfertigkeit, value))
                {
                    var previousValue = _sonderfertigkeit;
                    _sonderfertigkeit = value;
                    FixupSonderfertigkeit(previousValue);
                }
            }
        }
        private Sonderfertigkeit _sonderfertigkeit;

        #endregion

        #region Association Fixup
    
        private void FixupSonderfertigkeit(Sonderfertigkeit previousValue)
        {
    		OnChanged("Sonderfertigkeit");
            if (previousValue != null && previousValue.Zauberzeichen.Contains(this))
            {
                previousValue.Zauberzeichen.Remove(this);
            }
    
            if (Sonderfertigkeit != null)
            {
                if (!Sonderfertigkeit.Zauberzeichen.Contains(this))
                {
                    Sonderfertigkeit.Zauberzeichen.Add(this);
                }
                if (SonderfertigkeitGUID != Sonderfertigkeit.SonderfertigkeitGUID)
                {
                    SonderfertigkeitGUID = Sonderfertigkeit.SonderfertigkeitGUID;
                }
            }
        }

        #endregion

    }
}
