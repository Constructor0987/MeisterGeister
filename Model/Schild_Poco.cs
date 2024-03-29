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
    public partial class Schild : INotifyPropertyChanged
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
    			Set(ref _größe, value);
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
    			Set(ref _typ, value);
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
    			Set(ref _wMAT, value);
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
    			Set(ref _wMPA, value);
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
    			Set(ref _iNI, value);
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
    			Set(ref _bF, value);
    		}
    
        }
        private int _bF;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Pfad
        {
            get { return _pfad; }
            set
    		{ 
    			Set(ref _pfad, value);
    		}
    
        }
        private string _pfad;

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
    
    	[DataMember]
        public virtual ICollection<Held_BFAusrüstung> Held_BFAusrüstung
        {
            get
            {
                if (_held_BFAusrüstung == null)
                {
                    var newCollection = new FixupCollection<Held_BFAusrüstung>();
                    newCollection.CollectionChanged += FixupHeld_BFAusrüstung;
                    _held_BFAusrüstung = newCollection;
                }
                return _held_BFAusrüstung;
            }
            set
            {
                if (!ReferenceEquals(_held_BFAusrüstung, value))
                {
                    var previousValue = _held_BFAusrüstung as FixupCollection<Held_BFAusrüstung>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupHeld_BFAusrüstung;
                    }
                    _held_BFAusrüstung = value;
                    var newValue = value as FixupCollection<Held_BFAusrüstung>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupHeld_BFAusrüstung;
                    }
                }
            }
        }
        private ICollection<Held_BFAusrüstung> _held_BFAusrüstung;

        #endregion

        #region Association Fixup
    
        private void FixupAusrüstung(Ausrüstung previousValue)
        {
    		OnChanged("Ausrüstung");
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
    
        private void FixupHeld_BFAusrüstung(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Held_BFAusrüstung");
            if (e.NewItems != null)
            {
                foreach (Held_BFAusrüstung item in e.NewItems)
                {
                    item.Schild = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Held_BFAusrüstung item in e.OldItems)
                {
                    if (ReferenceEquals(item.Schild, this))
                    {
                        item.Schild = null;
                    }
                }
            }
        }

        #endregion

    }
}
