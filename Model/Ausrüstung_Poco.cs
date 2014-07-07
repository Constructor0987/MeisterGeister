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
    public partial class Ausrüstung : INotifyPropertyChanged
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
        public virtual System.Guid AusrüstungGUID
        {
            get { return _ausrüstungGUID; }
            set
    		{ 
    			Set(ref _ausrüstungGUID, value);
    		}
    
        }
        private System.Guid _ausrüstungGUID;
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
        public virtual int Gewicht
        {
            get { return _gewicht; }
            set
    		{ 
    			Set(ref _gewicht, value);
    		}
    
        }
        private int _gewicht;
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
        public virtual string Bemerkung
        {
            get { return _bemerkung; }
            set
    		{ 
    			Set(ref _bemerkung, value);
    		}
    
        }
        private string _bemerkung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Tags
        {
            get { return _tags; }
            set
    		{ 
    			Set(ref _tags, value);
    		}
    
        }
        private string _tags;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string BasisAusrüstung
        {
            get { return _basisAusrüstung; }
            set
    		{ 
    			Set(ref _basisAusrüstung, value);
    		}
    
        }
        private string _basisAusrüstung;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Ausrüstung_Setting> Ausrüstung_Setting
        {
            get
            {
                if (_ausrüstung_Setting == null)
                {
                    var newCollection = new FixupCollection<Ausrüstung_Setting>();
                    newCollection.CollectionChanged += FixupAusrüstung_Setting;
                    _ausrüstung_Setting = newCollection;
                }
                return _ausrüstung_Setting;
            }
            set
            {
                if (!ReferenceEquals(_ausrüstung_Setting, value))
                {
                    var previousValue = _ausrüstung_Setting as FixupCollection<Ausrüstung_Setting>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupAusrüstung_Setting;
                    }
                    _ausrüstung_Setting = value;
                    var newValue = value as FixupCollection<Ausrüstung_Setting>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupAusrüstung_Setting;
                    }
                }
            }
        }
        private ICollection<Ausrüstung_Setting> _ausrüstung_Setting;
    
    	[DataMember]
        public virtual Fernkampfwaffe Fernkampfwaffe
        {
            get { return _fernkampfwaffe; }
            set
            {
                if (!ReferenceEquals(_fernkampfwaffe, value))
                {
                    var previousValue = _fernkampfwaffe;
                    _fernkampfwaffe = value;
                    FixupFernkampfwaffe(previousValue);
                }
            }
        }
        private Fernkampfwaffe _fernkampfwaffe;
    
    	[DataMember]
        public virtual ICollection<Held_Ausrüstung> Held_Ausrüstung
        {
            get
            {
                if (_held_Ausrüstung == null)
                {
                    var newCollection = new FixupCollection<Held_Ausrüstung>();
                    newCollection.CollectionChanged += FixupHeld_Ausrüstung;
                    _held_Ausrüstung = newCollection;
                }
                return _held_Ausrüstung;
            }
            set
            {
                if (!ReferenceEquals(_held_Ausrüstung, value))
                {
                    var previousValue = _held_Ausrüstung as FixupCollection<Held_Ausrüstung>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupHeld_Ausrüstung;
                    }
                    _held_Ausrüstung = value;
                    var newValue = value as FixupCollection<Held_Ausrüstung>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupHeld_Ausrüstung;
                    }
                }
            }
        }
        private ICollection<Held_Ausrüstung> _held_Ausrüstung;
    
    	[DataMember]
        public virtual Rüstung Rüstung
        {
            get { return _rüstung; }
            set
            {
                if (!ReferenceEquals(_rüstung, value))
                {
                    var previousValue = _rüstung;
                    _rüstung = value;
                    FixupRüstung(previousValue);
                }
            }
        }
        private Rüstung _rüstung;
    
    	[DataMember]
        public virtual Schild Schild
        {
            get { return _schild; }
            set
            {
                if (!ReferenceEquals(_schild, value))
                {
                    var previousValue = _schild;
                    _schild = value;
                    FixupSchild(previousValue);
                }
            }
        }
        private Schild _schild;
    
    	[DataMember]
        public virtual Waffe Waffe
        {
            get { return _waffe; }
            set
            {
                if (!ReferenceEquals(_waffe, value))
                {
                    var previousValue = _waffe;
                    _waffe = value;
                    FixupWaffe(previousValue);
                }
            }
        }
        private Waffe _waffe;

        #endregion

        #region Association Fixup
    
        private void FixupFernkampfwaffe(Fernkampfwaffe previousValue)
        {
    		OnChanged("Fernkampfwaffe");
            if (previousValue != null && ReferenceEquals(previousValue.Ausrüstung, this))
            {
                previousValue.Ausrüstung = null;
            }
    
            if (Fernkampfwaffe != null)
            {
                Fernkampfwaffe.Ausrüstung = this;
            }
        }
    
        private void FixupRüstung(Rüstung previousValue)
        {
    		OnChanged("Rüstung");
            if (previousValue != null && ReferenceEquals(previousValue.Ausrüstung, this))
            {
                previousValue.Ausrüstung = null;
            }
    
            if (Rüstung != null)
            {
                Rüstung.Ausrüstung = this;
            }
        }
    
        private void FixupSchild(Schild previousValue)
        {
    		OnChanged("Schild");
            if (previousValue != null && ReferenceEquals(previousValue.Ausrüstung, this))
            {
                previousValue.Ausrüstung = null;
            }
    
            if (Schild != null)
            {
                Schild.Ausrüstung = this;
            }
        }
    
        private void FixupWaffe(Waffe previousValue)
        {
    		OnChanged("Waffe");
            if (previousValue != null && ReferenceEquals(previousValue.Ausrüstung, this))
            {
                previousValue.Ausrüstung = null;
            }
    
            if (Waffe != null)
            {
                Waffe.Ausrüstung = this;
            }
        }
    
        private void FixupAusrüstung_Setting(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Ausrüstung_Setting");
            if (e.NewItems != null)
            {
                foreach (Ausrüstung_Setting item in e.NewItems)
                {
                    item.Ausrüstung = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Ausrüstung_Setting item in e.OldItems)
                {
                    if (ReferenceEquals(item.Ausrüstung, this))
                    {
                        item.Ausrüstung = null;
                    }
                }
            }
        }
    
        private void FixupHeld_Ausrüstung(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Held_Ausrüstung");
            if (e.NewItems != null)
            {
                foreach (Held_Ausrüstung item in e.NewItems)
                {
                    item.Ausrüstung = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Held_Ausrüstung item in e.OldItems)
                {
                    if (ReferenceEquals(item.Ausrüstung, this))
                    {
                        item.Ausrüstung = null;
                    }
                }
            }
        }

        #endregion

    }
}
