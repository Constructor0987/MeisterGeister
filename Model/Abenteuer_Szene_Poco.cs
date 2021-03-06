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
    public partial class Abenteuer_Szene : INotifyPropertyChanged
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
        public virtual System.Guid SzeneGUID
        {
            get { return _szeneGUID; }
            set
    		{ 
    			Set(ref _szeneGUID, value);
    		}
    
        }
        private System.Guid _szeneGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid AbenteuerGUID
        {
            get { return _abenteuerGUID; }
            set
            {
                if (_abenteuerGUID != value)
                {
                    if (Abenteuer != null && Abenteuer.AbenteuerGUID != value)
                    {
                        Abenteuer = null;
                    }
                    _abenteuerGUID = value;
                }
            }
    
        }
        private System.Guid _abenteuerGUID;
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

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Abenteuer Abenteuer
        {
            get { return _abenteuer; }
            set
            {
                if (!ReferenceEquals(_abenteuer, value))
                {
                    var previousValue = _abenteuer;
                    _abenteuer = value;
                    FixupAbenteuer(previousValue);
                }
            }
        }
        private Abenteuer _abenteuer;
    
    	[DataMember]
        public virtual ICollection<Abenteuer_Ereignis> Abenteuer_Ereignis
        {
            get
            {
                if (_abenteuer_Ereignis == null)
                {
                    var newCollection = new FixupCollection<Abenteuer_Ereignis>();
                    newCollection.CollectionChanged += FixupAbenteuer_Ereignis;
                    _abenteuer_Ereignis = newCollection;
                }
                return _abenteuer_Ereignis;
            }
            set
            {
                if (!ReferenceEquals(_abenteuer_Ereignis, value))
                {
                    var previousValue = _abenteuer_Ereignis as FixupCollection<Abenteuer_Ereignis>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupAbenteuer_Ereignis;
                    }
                    _abenteuer_Ereignis = value;
                    var newValue = value as FixupCollection<Abenteuer_Ereignis>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupAbenteuer_Ereignis;
                    }
                }
            }
        }
        private ICollection<Abenteuer_Ereignis> _abenteuer_Ereignis;
    
    	[DataMember]
        public virtual ICollection<Abenteuer_Verweis> Abenteuer_Verweis
        {
            get
            {
                if (_abenteuer_Verweis == null)
                {
                    var newCollection = new FixupCollection<Abenteuer_Verweis>();
                    newCollection.CollectionChanged += FixupAbenteuer_Verweis;
                    _abenteuer_Verweis = newCollection;
                }
                return _abenteuer_Verweis;
            }
            set
            {
                if (!ReferenceEquals(_abenteuer_Verweis, value))
                {
                    var previousValue = _abenteuer_Verweis as FixupCollection<Abenteuer_Verweis>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupAbenteuer_Verweis;
                    }
                    _abenteuer_Verweis = value;
                    var newValue = value as FixupCollection<Abenteuer_Verweis>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupAbenteuer_Verweis;
                    }
                }
            }
        }
        private ICollection<Abenteuer_Verweis> _abenteuer_Verweis;
    
    	[DataMember]
        public virtual ICollection<Abenteuer_Verweis> Abenteuer_Verweis1
        {
            get
            {
                if (_abenteuer_Verweis1 == null)
                {
                    var newCollection = new FixupCollection<Abenteuer_Verweis>();
                    newCollection.CollectionChanged += FixupAbenteuer_Verweis1;
                    _abenteuer_Verweis1 = newCollection;
                }
                return _abenteuer_Verweis1;
            }
            set
            {
                if (!ReferenceEquals(_abenteuer_Verweis1, value))
                {
                    var previousValue = _abenteuer_Verweis1 as FixupCollection<Abenteuer_Verweis>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupAbenteuer_Verweis1;
                    }
                    _abenteuer_Verweis1 = value;
                    var newValue = value as FixupCollection<Abenteuer_Verweis>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupAbenteuer_Verweis1;
                    }
                }
            }
        }
        private ICollection<Abenteuer_Verweis> _abenteuer_Verweis1;

        #endregion

        #region Association Fixup
    
        private void FixupAbenteuer(Abenteuer previousValue)
        {
    		OnChanged("Abenteuer");
            if (previousValue != null && previousValue.Abenteuer_Szene.Contains(this))
            {
                previousValue.Abenteuer_Szene.Remove(this);
            }
    
            if (Abenteuer != null)
            {
                if (!Abenteuer.Abenteuer_Szene.Contains(this))
                {
                    Abenteuer.Abenteuer_Szene.Add(this);
                }
                if (AbenteuerGUID != Abenteuer.AbenteuerGUID)
                {
                    AbenteuerGUID = Abenteuer.AbenteuerGUID;
                }
            }
        }
    
        private void FixupAbenteuer_Ereignis(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Abenteuer_Ereignis");
            if (e.NewItems != null)
            {
                foreach (Abenteuer_Ereignis item in e.NewItems)
                {
                    item.Abenteuer_Szene = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Abenteuer_Ereignis item in e.OldItems)
                {
                    if (ReferenceEquals(item.Abenteuer_Szene, this))
                    {
                        item.Abenteuer_Szene = null;
                    }
                }
            }
        }
    
        private void FixupAbenteuer_Verweis(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Abenteuer_Verweis");
            if (e.NewItems != null)
            {
                foreach (Abenteuer_Verweis item in e.NewItems)
                {
                    item.Abenteuer_Szene = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Abenteuer_Verweis item in e.OldItems)
                {
                    if (ReferenceEquals(item.Abenteuer_Szene, this))
                    {
                        item.Abenteuer_Szene = null;
                    }
                }
            }
        }
    
        private void FixupAbenteuer_Verweis1(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Abenteuer_Verweis1");
            if (e.NewItems != null)
            {
                foreach (Abenteuer_Verweis item in e.NewItems)
                {
                    item.Abenteuer_Szene1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Abenteuer_Verweis item in e.OldItems)
                {
                    if (ReferenceEquals(item.Abenteuer_Szene1, this))
                    {
                        item.Abenteuer_Szene1 = null;
                    }
                }
            }
        }

        #endregion

    }
}
