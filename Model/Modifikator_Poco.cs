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
    public partial class Modifikator : INotifyPropertyChanged
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
        public virtual System.Guid ModifikatorGUID
        {
            get { return _modifikatorGUID; }
            set
    		{ 
    			Set(ref _modifikatorGUID, value);
    		}
    
        }
        private System.Guid _modifikatorGUID;
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
        public virtual string Beschreibung
        {
            get { return _beschreibung; }
            set
    		{ 
    			Set(ref _beschreibung, value);
    		}
    
        }
        private string _beschreibung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Json
        {
            get { return _json; }
            set
    		{ 
    			Set(ref _json, value);
    		}
    
        }
        private string _json;
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

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Gegner_Modifikator> Gegner_Modifikator
        {
            get
            {
                if (_gegner_Modifikator == null)
                {
                    var newCollection = new FixupCollection<Gegner_Modifikator>();
                    newCollection.CollectionChanged += FixupGegner_Modifikator;
                    _gegner_Modifikator = newCollection;
                }
                return _gegner_Modifikator;
            }
            set
            {
                if (!ReferenceEquals(_gegner_Modifikator, value))
                {
                    var previousValue = _gegner_Modifikator as FixupCollection<Gegner_Modifikator>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupGegner_Modifikator;
                    }
                    _gegner_Modifikator = value;
                    var newValue = value as FixupCollection<Gegner_Modifikator>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupGegner_Modifikator;
                    }
                }
            }
        }
        private ICollection<Gegner_Modifikator> _gegner_Modifikator;
    
    	[DataMember]
        public virtual ICollection<Held_Modifikator> Held_Modifikator
        {
            get
            {
                if (_held_Modifikator == null)
                {
                    var newCollection = new FixupCollection<Held_Modifikator>();
                    newCollection.CollectionChanged += FixupHeld_Modifikator;
                    _held_Modifikator = newCollection;
                }
                return _held_Modifikator;
            }
            set
            {
                if (!ReferenceEquals(_held_Modifikator, value))
                {
                    var previousValue = _held_Modifikator as FixupCollection<Held_Modifikator>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupHeld_Modifikator;
                    }
                    _held_Modifikator = value;
                    var newValue = value as FixupCollection<Held_Modifikator>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupHeld_Modifikator;
                    }
                }
            }
        }
        private ICollection<Held_Modifikator> _held_Modifikator;

        #endregion

        #region Association Fixup
    
        private void FixupGegner_Modifikator(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Gegner_Modifikator");
            if (e.NewItems != null)
            {
                foreach (Gegner_Modifikator item in e.NewItems)
                {
                    item.Modifikator = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Gegner_Modifikator item in e.OldItems)
                {
                    if (ReferenceEquals(item.Modifikator, this))
                    {
                        item.Modifikator = null;
                    }
                }
            }
        }
    
        private void FixupHeld_Modifikator(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Held_Modifikator");
            if (e.NewItems != null)
            {
                foreach (Held_Modifikator item in e.NewItems)
                {
                    item.Modifikator = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Held_Modifikator item in e.OldItems)
                {
                    if (ReferenceEquals(item.Modifikator, this))
                    {
                        item.Modifikator = null;
                    }
                }
            }
        }

        #endregion

    }
}
