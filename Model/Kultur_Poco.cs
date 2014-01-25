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
using System.Runtime.CompilerServices;

namespace MeisterGeister.Model
{
    [DataContract(IsReference=true)]
    public partial class Kultur : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
    	public event PropertyChangedEventHandler PropertyChanged;
    	
    	/// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region ValidatePropertyChanging
    	protected event Extensions.ValidatePropertyChangingEventHandler ValidatePropertyChanging;
    
    	protected void OnValidatePropertyChanging(object currentValue, object newValue, [CallerMemberName] string propertyName = null)
    	{
    		if(ValidatePropertyChanging != null)
    		{
    			ValidatePropertyChanging(this, propertyName, currentValue, newValue);
    		}
    	}

        #endregion

        #region Set
    	/// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool Set<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;
    
    		OnValidatePropertyChanging(storage, value, propertyName);
    		storage = value;
    		OnChanged(propertyName);
            return true;
        }

        #endregion

        #region Primitive Properties
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid KulturGUID
        {
            get { return _kulturGUID; }
            set
    		{ 
    			Set(ref _kulturGUID, value);
    		}
    
        }
        private System.Guid _kulturGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Name
        {
            get { return _name; }
            set
    		{ 
    			Set(ref _name, value);
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
    			Set(ref _variante, value);
    		}
    
        }
        private string _variante;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int GP
        {
            get { return _gP; }
            set
    		{ 
    			Set(ref _gP, value);
    		}
    
        }
        private int _gP;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> SOmin
        {
            get { return _sOmin; }
            set
    		{ 
    			Set(ref _sOmin, value);
    		}
    
        }
        private Nullable<int> _sOmin;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> SOmax
        {
            get { return _sOmax; }
            set
    		{ 
    			Set(ref _sOmax, value);
    		}
    
        }
        private Nullable<int> _sOmax;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Voraussetzungen
        {
            get { return _voraussetzungen; }
            set
    		{ 
    			Set(ref _voraussetzungen, value);
    		}
    
        }
        private string _voraussetzungen;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int MUMod
        {
            get { return _mUMod; }
            set
    		{ 
    			Set(ref _mUMod, value);
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
    			Set(ref _kLMod, value);
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
    			Set(ref _iNMod, value);
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
    			Set(ref _cHMod, value);
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
    			Set(ref _fFMod, value);
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
    			Set(ref _gEMod, value);
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
    			Set(ref _kOMod, value);
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
    			Set(ref _kKMod, value);
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
    			Set(ref _lEMod, value);
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
    			Set(ref _aUMod, value);
    		}
    
        }
        private int _aUMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int MRMod
        {
            get { return _mRMod; }
            set
    		{ 
    			Set(ref _mRMod, value);
    		}
    
        }
        private int _mRMod;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Literatur
        {
            get { return _literatur; }
            set
    		{ 
    			Set(ref _literatur, value);
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
    			Set(ref _setting, value);
    		}
    
        }
        private string _setting;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Kultur_Name> Kultur_Name
        {
            get
            {
                if (_kultur_Name == null)
                {
                    var newCollection = new FixupCollection<Kultur_Name>();
                    newCollection.CollectionChanged += FixupKultur_Name;
                    _kultur_Name = newCollection;
                }
                return _kultur_Name;
            }
            set
            {
                if (!ReferenceEquals(_kultur_Name, value))
                {
                    var previousValue = _kultur_Name as FixupCollection<Kultur_Name>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupKultur_Name;
                    }
                    _kultur_Name = value;
                    var newValue = value as FixupCollection<Kultur_Name>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupKultur_Name;
                    }
                }
            }
        }
        private ICollection<Kultur_Name> _kultur_Name;
    
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
    
        private void FixupKultur_Name(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Kultur_Name");
            if (e.NewItems != null)
            {
                foreach (Kultur_Name item in e.NewItems)
                {
                    item.Kultur = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Kultur_Name item in e.OldItems)
                {
                    if (ReferenceEquals(item.Kultur, this))
                    {
                        item.Kultur = null;
                    }
                }
            }
        }
    
        private void FixupRasse_Kultur(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Rasse_Kultur");
            if (e.NewItems != null)
            {
                foreach (Rasse_Kultur item in e.NewItems)
                {
                    item.Kultur = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Rasse_Kultur item in e.OldItems)
                {
                    if (ReferenceEquals(item.Kultur, this))
                    {
                        item.Kultur = null;
                    }
                }
            }
        }

        #endregion

    }
}
