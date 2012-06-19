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
    public partial class Rasse : INotifyPropertyChanged
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
        public virtual System.Guid RasseGUID
        {
            get { return _rasseGUID; }
            set
    		{ 
    			_rasseGUID = value;
    			NotifyPropertyChanged("RasseGUID");
    		}
    
        }
        private System.Guid _rasseGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Name
        {
            get { return _name; }
            set
    		{ 
    			_name = value;
    			NotifyPropertyChanged("Name");
    		}
    
        }
        private string _name;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Variante
        {
            get { return _variante; }
            set
    		{ 
    			_variante = value;
    			NotifyPropertyChanged("Variante");
    		}
    
        }
        private string _variante;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool Unspielbar
        {
            get { return _unspielbar; }
            set
    		{ 
    			_unspielbar = value;
    			NotifyPropertyChanged("Unspielbar");
    		}
    
        }
        private bool _unspielbar;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int GP
        {
            get { return _gP; }
            set
    		{ 
    			_gP = value;
    			NotifyPropertyChanged("GP");
    		}
    
        }
        private int _gP;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Größe
        {
            get { return _größe; }
            set
    		{ 
    			_größe = value;
    			NotifyPropertyChanged("Größe");
    		}
    
        }
        private int _größe;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string GrößeMod
        {
            get { return _größeMod; }
            set
    		{ 
    			_größeMod = value;
    			NotifyPropertyChanged("GrößeMod");
    		}
    
        }
        private string _größeMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Gewicht
        {
            get { return _gewicht; }
            set
    		{ 
    			_gewicht = value;
    			NotifyPropertyChanged("Gewicht");
    		}
    
        }
        private int _gewicht;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int MUMod
        {
            get { return _mUMod; }
            set
    		{ 
    			_mUMod = value;
    			NotifyPropertyChanged("MUMod");
    		}
    
        }
        private int _mUMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int KLMod
        {
            get { return _kLMod; }
            set
    		{ 
    			_kLMod = value;
    			NotifyPropertyChanged("KLMod");
    		}
    
        }
        private int _kLMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int INMod
        {
            get { return _iNMod; }
            set
    		{ 
    			_iNMod = value;
    			NotifyPropertyChanged("INMod");
    		}
    
        }
        private int _iNMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int CHMod
        {
            get { return _cHMod; }
            set
    		{ 
    			_cHMod = value;
    			NotifyPropertyChanged("CHMod");
    		}
    
        }
        private int _cHMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int FFMod
        {
            get { return _fFMod; }
            set
    		{ 
    			_fFMod = value;
    			NotifyPropertyChanged("FFMod");
    		}
    
        }
        private int _fFMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int GEMod
        {
            get { return _gEMod; }
            set
    		{ 
    			_gEMod = value;
    			NotifyPropertyChanged("GEMod");
    		}
    
        }
        private int _gEMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int KOMod
        {
            get { return _kOMod; }
            set
    		{ 
    			_kOMod = value;
    			NotifyPropertyChanged("KOMod");
    		}
    
        }
        private int _kOMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int KKMod
        {
            get { return _kKMod; }
            set
    		{ 
    			_kKMod = value;
    			NotifyPropertyChanged("KKMod");
    		}
    
        }
        private int _kKMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int LEMod
        {
            get { return _lEMod; }
            set
    		{ 
    			_lEMod = value;
    			NotifyPropertyChanged("LEMod");
    		}
    
        }
        private int _lEMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int AUMod
        {
            get { return _aUMod; }
            set
    		{ 
    			_aUMod = value;
    			NotifyPropertyChanged("AUMod");
    		}
    
        }
        private int _aUMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int AEMod
        {
            get { return _aEMod; }
            set
    		{ 
    			_aEMod = value;
    			NotifyPropertyChanged("AEMod");
    		}
    
        }
        private int _aEMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int MRMod
        {
            get { return _mRMod; }
            set
    		{ 
    			_mRMod = value;
    			NotifyPropertyChanged("MRMod");
    		}
    
        }
        private int _mRMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int INIMod
        {
            get { return _iNIMod; }
            set
    		{ 
    			_iNIMod = value;
    			NotifyPropertyChanged("INIMod");
    		}
    
        }
        private int _iNIMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Literatur
        {
            get { return _literatur; }
            set
    		{ 
    			_literatur = value;
    			NotifyPropertyChanged("Literatur");
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
    			NotifyPropertyChanged("Setting");
    		}
    
        }
        private string _setting;

        #endregion
        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Rasse_Farbe> Rasse_Farbe
        {
            get
            {
                if (_rasse_Farbe == null)
                {
                    var newCollection = new FixupCollection<Rasse_Farbe>();
                    newCollection.CollectionChanged += FixupRasse_Farbe;
                    _rasse_Farbe = newCollection;
                }
                return _rasse_Farbe;
            }
            set
            {
                if (!ReferenceEquals(_rasse_Farbe, value))
                {
                    var previousValue = _rasse_Farbe as FixupCollection<Rasse_Farbe>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupRasse_Farbe;
                    }
                    _rasse_Farbe = value;
                    var newValue = value as FixupCollection<Rasse_Farbe>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupRasse_Farbe;
                    }
                }
            }
        }
        private ICollection<Rasse_Farbe> _rasse_Farbe;
    
    	[DataMember]
        public virtual ICollection<Rasse_Kultur> Rasse_Kultur
        {
            get
            {
                if (_rasse_Kultur == null)
                {
                    var newCollection = new FixupCollection<Rasse_Kultur>();
                    newCollection.CollectionChanged += FixupRasse_Kultur;
                    _rasse_Kultur = newCollection;
                }
                return _rasse_Kultur;
            }
            set
            {
                if (!ReferenceEquals(_rasse_Kultur, value))
                {
                    var previousValue = _rasse_Kultur as FixupCollection<Rasse_Kultur>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupRasse_Kultur;
                    }
                    _rasse_Kultur = value;
                    var newValue = value as FixupCollection<Rasse_Kultur>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupRasse_Kultur;
                    }
                }
            }
        }
        private ICollection<Rasse_Kultur> _rasse_Kultur;

        #endregion
        #region Association Fixup
    
        private void FixupRasse_Farbe(object sender, NotifyCollectionChangedEventArgs e)
        {
    		NotifyPropertyChanged("Rasse_Farbe");
            if (e.NewItems != null)
            {
                foreach (Rasse_Farbe item in e.NewItems)
                {
                    item.Rasse = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Rasse_Farbe item in e.OldItems)
                {
                    if (ReferenceEquals(item.Rasse, this))
                    {
                        item.Rasse = null;
                    }
                }
            }
        }
    
        private void FixupRasse_Kultur(object sender, NotifyCollectionChangedEventArgs e)
        {
    		NotifyPropertyChanged("Rasse_Kultur");
            if (e.NewItems != null)
            {
                foreach (Rasse_Kultur item in e.NewItems)
                {
                    item.Rasse = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Rasse_Kultur item in e.OldItems)
                {
                    if (ReferenceEquals(item.Rasse, this))
                    {
                        item.Rasse = null;
                    }
                }
            }
        }

        #endregion
    }
}
