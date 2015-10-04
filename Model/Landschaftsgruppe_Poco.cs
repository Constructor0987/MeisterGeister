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
    public partial class Landschaftsgruppe : INotifyPropertyChanged
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
        public virtual string LandschaftsgruppeID
        {
            get { return _landschaftsgruppeID; }
            set
    		{ 
    			Set(ref _landschaftsgruppeID, value);
    		}
    
        }
        private string _landschaftsgruppeID;
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
        public virtual ICollection<Landschaft> Landschaft
        {
            get
            {
                if (_landschaft == null)
                {
                    var newCollection = new FixupCollection<Landschaft>();
                    newCollection.CollectionChanged += FixupLandschaft;
                    _landschaft = newCollection;
                }
                return _landschaft;
            }
            set
            {
                if (!ReferenceEquals(_landschaft, value))
                {
                    var previousValue = _landschaft as FixupCollection<Landschaft>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupLandschaft;
                    }
                    _landschaft = value;
                    var newValue = value as FixupCollection<Landschaft>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupLandschaft;
                    }
                }
            }
        }
        private ICollection<Landschaft> _landschaft;

        #endregion

        #region Association Fixup
    
        private void FixupLandschaft(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Landschaft");
            if (e.NewItems != null)
            {
                foreach (Landschaft item in e.NewItems)
                {
                    if (!item.Landschaftsgruppe.Contains(this))
                    {
                        item.Landschaftsgruppe.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Landschaft item in e.OldItems)
                {
                    if (item.Landschaftsgruppe.Contains(this))
                    {
                        item.Landschaftsgruppe.Remove(this);
                    }
                }
            }
        }

        #endregion

    }
}
