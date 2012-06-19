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
    public partial class Schild : INotifyPropertyChanged
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
        public virtual System.Guid SchildGUID
        {
            get { return _schildGUID; }
            set
            {
                if (_schildGUID != value)
                {
                    if (Ausrüstung != null && Ausrüstung.AusrüstungGUID != value)
                    {
                        Ausrüstung = null;
                    }
                    _schildGUID = value;
                }
            }
    
        }
        private System.Guid _schildGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Größe
        {
            get { return _größe; }
            set
    		{ 
    			_größe = value;
    			NotifyPropertyChanged("Größe");
    		}
    
        }
        private string _größe;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Typ
        {
            get { return _typ; }
            set
    		{ 
    			_typ = value;
    			NotifyPropertyChanged("Typ");
    		}
    
        }
        private string _typ;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int WMAT
        {
            get { return _wMAT; }
            set
    		{ 
    			_wMAT = value;
    			NotifyPropertyChanged("WMAT");
    		}
    
        }
        private int _wMAT;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int WMPA
        {
            get { return _wMPA; }
            set
    		{ 
    			_wMPA = value;
    			NotifyPropertyChanged("WMPA");
    		}
    
        }
        private int _wMPA;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int INI
        {
            get { return _iNI; }
            set
    		{ 
    			_iNI = value;
    			NotifyPropertyChanged("INI");
    		}
    
        }
        private int _iNI;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int BF
        {
            get { return _bF; }
            set
    		{ 
    			_bF = value;
    			NotifyPropertyChanged("BF");
    		}
    
        }
        private int _bF;

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
            if (previousValue != null && ReferenceEquals(previousValue.Schild, this))
            {
                previousValue.Schild = null;
            }
    
            if (Ausrüstung != null)
            {
                Ausrüstung.Schild = this;
                if (SchildGUID != Ausrüstung.AusrüstungGUID)
                {
                    SchildGUID = Ausrüstung.AusrüstungGUID;
                }
            }
        }

        #endregion
    }
}
