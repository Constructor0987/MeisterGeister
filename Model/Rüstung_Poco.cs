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
    public partial class Rüstung : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
    	public event PropertyChangedEventHandler PropertyChanged;
    	
    	public void NotifyPropertyChanged(String info)
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
        public virtual System.Guid RüstungGUID
        {
            get { return _rüstungGUID; }
            set
            {
                if (_rüstungGUID != value)
                {
                    if (Ausrüstung != null && Ausrüstung.AusrüstungGUID != value)
                    {
                        Ausrüstung = null;
                    }
                    _rüstungGUID = value;
                }
            }
    
        }
        private System.Guid _rüstungGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Gruppe
        {
            get { return _gruppe; }
            set
    		{ 
    			_gruppe = value;
    			NotifyPropertyChanged("Gruppe");
    		}
    
        }
        private string _gruppe;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> Verarbeitung
        {
            get { return _verarbeitung; }
            set
    		{ 
    			_verarbeitung = value;
    			NotifyPropertyChanged("Verarbeitung");
    		}
    
        }
        private Nullable<int> _verarbeitung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Art
        {
            get { return _art; }
            set
    		{ 
    			_art = value;
    			NotifyPropertyChanged("Art");
    		}
    
        }
        private string _art;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> Kopf
        {
            get { return _kopf; }
            set
    		{ 
    			_kopf = value;
    			NotifyPropertyChanged("Kopf");
    		}
    
        }
        private Nullable<int> _kopf;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> Brust
        {
            get { return _brust; }
            set
    		{ 
    			_brust = value;
    			NotifyPropertyChanged("Brust");
    		}
    
        }
        private Nullable<int> _brust;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> Rücken
        {
            get { return _rücken; }
            set
    		{ 
    			_rücken = value;
    			NotifyPropertyChanged("Rücken");
    		}
    
        }
        private Nullable<int> _rücken;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> Bauch
        {
            get { return _bauch; }
            set
    		{ 
    			_bauch = value;
    			NotifyPropertyChanged("Bauch");
    		}
    
        }
        private Nullable<int> _bauch;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> LArm
        {
            get { return _lArm; }
            set
    		{ 
    			_lArm = value;
    			NotifyPropertyChanged("LArm");
    		}
    
        }
        private Nullable<int> _lArm;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> RArm
        {
            get { return _rArm; }
            set
    		{ 
    			_rArm = value;
    			NotifyPropertyChanged("RArm");
    		}
    
        }
        private Nullable<int> _rArm;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> LBein
        {
            get { return _lBein; }
            set
    		{ 
    			_lBein = value;
    			NotifyPropertyChanged("LBein");
    		}
    
        }
        private Nullable<int> _lBein;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> RBein
        {
            get { return _rBein; }
            set
    		{ 
    			_rBein = value;
    			NotifyPropertyChanged("RBein");
    		}
    
        }
        private Nullable<int> _rBein;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> gRS
        {
            get { return _gRS; }
            set
    		{ 
    			_gRS = value;
    			NotifyPropertyChanged("gRS");
    		}
    
        }
        private Nullable<double> _gRS;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> gBE
        {
            get { return _gBE; }
            set
    		{ 
    			_gBE = value;
    			NotifyPropertyChanged("gBE");
    		}
    
        }
        private Nullable<double> _gBE;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> RS
        {
            get { return _rS; }
            set
    		{ 
    			_rS = value;
    			NotifyPropertyChanged("RS");
    		}
    
        }
        private Nullable<int> _rS;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> BE
        {
            get { return _bE; }
            set
    		{ 
    			_bE = value;
    			NotifyPropertyChanged("BE");
    		}
    
        }
        private Nullable<int> _bE;

        #endregion
        #region Navigation Properties
    
    	[DataMember]
        public virtual Ausrüstung Ausrüstung
        {
            get { return _ausrüstung; }
            set
            {
                if (!ReferenceEquals(_ausrüstung, value))
                {
                    var previousValue = _ausrüstung;
                    _ausrüstung = value;
                    FixupAusrüstung(previousValue);
                }
            }
        }
        private Ausrüstung _ausrüstung;

        #endregion
        #region Association Fixup
    
        private void FixupAusrüstung(Ausrüstung previousValue)
        {
    		NotifyPropertyChanged("Ausrüstung");
            if (previousValue != null && ReferenceEquals(previousValue.Rüstung, this))
            {
                previousValue.Rüstung = null;
            }
    
            if (Ausrüstung != null)
            {
                Ausrüstung.Rüstung = this;
                if (RüstungGUID != Ausrüstung.AusrüstungGUID)
                {
                    RüstungGUID = Ausrüstung.AusrüstungGUID;
                }
            }
        }

        #endregion
    }
}
