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
    public partial class Sonderfertigkeit : INotifyPropertyChanged
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
        public virtual System.Guid SonderfertigkeitGUID
        {
            get { return _sonderfertigkeitGUID; }
            set
    		{ 
    			Set(ref _sonderfertigkeitGUID, value);
    		}
    
        }
        private System.Guid _sonderfertigkeitGUID;
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
        public virtual Nullable<bool> HatWert
        {
            get { return _hatWert; }
            set
    		{ 
    			Set(ref _hatWert, value);
    		}
    
        }
        private Nullable<bool> _hatWert;
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
        public virtual string Voraussetzungen
        {
            get { return _voraussetzungen; }
            set
    		{ 
    			Set(ref _voraussetzungen, value);
    		}
    
        }
        private string _voraussetzungen;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Held_Sonderfertigkeit> Held_Sonderfertigkeit
        {
            get
            {
                if (_held_Sonderfertigkeit == null)
                {
                    var newCollection = new FixupCollection<Held_Sonderfertigkeit>();
                    newCollection.CollectionChanged += FixupHeld_Sonderfertigkeit;
                    _held_Sonderfertigkeit = newCollection;
                }
                return _held_Sonderfertigkeit;
            }
            set
            {
                if (!ReferenceEquals(_held_Sonderfertigkeit, value))
                {
                    var previousValue = _held_Sonderfertigkeit as FixupCollection<Held_Sonderfertigkeit>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupHeld_Sonderfertigkeit;
                    }
                    _held_Sonderfertigkeit = value;
                    var newValue = value as FixupCollection<Held_Sonderfertigkeit>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupHeld_Sonderfertigkeit;
                    }
                }
            }
        }
        private ICollection<Held_Sonderfertigkeit> _held_Sonderfertigkeit;
    
    	[DataMember]
        public virtual ICollection<Sonderfertigkeit_Setting> Sonderfertigkeit_Setting
        {
            get
            {
                if (_sonderfertigkeit_Setting == null)
                {
                    var newCollection = new FixupCollection<Sonderfertigkeit_Setting>();
                    newCollection.CollectionChanged += FixupSonderfertigkeit_Setting;
                    _sonderfertigkeit_Setting = newCollection;
                }
                return _sonderfertigkeit_Setting;
            }
            set
            {
                if (!ReferenceEquals(_sonderfertigkeit_Setting, value))
                {
                    var previousValue = _sonderfertigkeit_Setting as FixupCollection<Sonderfertigkeit_Setting>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupSonderfertigkeit_Setting;
                    }
                    _sonderfertigkeit_Setting = value;
                    var newValue = value as FixupCollection<Sonderfertigkeit_Setting>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupSonderfertigkeit_Setting;
                    }
                }
            }
        }
        private ICollection<Sonderfertigkeit_Setting> _sonderfertigkeit_Setting;
    
    	[DataMember]
        public virtual ICollection<Zauberzeichen> Zauberzeichen
        {
            get
            {
                if (_zauberzeichen == null)
                {
                    var newCollection = new FixupCollection<Zauberzeichen>();
                    newCollection.CollectionChanged += FixupZauberzeichen;
                    _zauberzeichen = newCollection;
                }
                return _zauberzeichen;
            }
            set
            {
                if (!ReferenceEquals(_zauberzeichen, value))
                {
                    var previousValue = _zauberzeichen as FixupCollection<Zauberzeichen>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupZauberzeichen;
                    }
                    _zauberzeichen = value;
                    var newValue = value as FixupCollection<Zauberzeichen>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupZauberzeichen;
                    }
                }
            }
        }
        private ICollection<Zauberzeichen> _zauberzeichen;

        #endregion

        #region Association Fixup
    
        private void FixupHeld_Sonderfertigkeit(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Held_Sonderfertigkeit");
            if (e.NewItems != null)
            {
                foreach (Held_Sonderfertigkeit item in e.NewItems)
                {
                    item.Sonderfertigkeit = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Held_Sonderfertigkeit item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sonderfertigkeit, this))
                    {
                        item.Sonderfertigkeit = null;
                    }
                }
            }
        }
    
        private void FixupSonderfertigkeit_Setting(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Sonderfertigkeit_Setting");
            if (e.NewItems != null)
            {
                foreach (Sonderfertigkeit_Setting item in e.NewItems)
                {
                    item.Sonderfertigkeit = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Sonderfertigkeit_Setting item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sonderfertigkeit, this))
                    {
                        item.Sonderfertigkeit = null;
                    }
                }
            }
        }
    
        private void FixupZauberzeichen(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Zauberzeichen");
            if (e.NewItems != null)
            {
                foreach (Zauberzeichen item in e.NewItems)
                {
                    item.Sonderfertigkeit = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Zauberzeichen item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sonderfertigkeit, this))
                    {
                        item.Sonderfertigkeit = null;
                    }
                }
            }
        }

        #endregion

    }
}
