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
    public partial class Weg : INotifyPropertyChanged
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
        public virtual long ID
        {
            get { return _iD; }
            set
    		{ 
    			Set(ref _iD, value);
    		}
    
        }
        private long _iD;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual long Strecke
        {
            get { return _strecke; }
            set
            {
                if (_strecke != value)
                {
                    if (Strecke1 != null && Strecke1.ID != value)
                    {
                        Strecke1 = null;
                    }
                    _strecke = value;
                }
            }
    
        }
        private long _strecke;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int X
        {
            get { return _x; }
            set
    		{ 
    			Set(ref _x, value);
    		}
    
        }
        private int _x;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Y
        {
            get { return _y; }
            set
    		{ 
    			Set(ref _y, value);
    		}
    
        }
        private int _y;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Strecke Strecke1
        {
            get { return _strecke1; }
            set
            {
                if (!ReferenceEquals(_strecke1, value))
                {
                    var previousValue = _strecke1;
                    _strecke1 = value;
                    FixupStrecke1(previousValue);
                }
            }
        }
        private Strecke _strecke1;

        #endregion

        #region Association Fixup
    
        private void FixupStrecke1(Strecke previousValue)
        {
    		OnChanged("Strecke1");
            if (previousValue != null && previousValue.Weg.Contains(this))
            {
                previousValue.Weg.Remove(this);
            }
    
            if (Strecke1 != null)
            {
                if (!Strecke1.Weg.Contains(this))
                {
                    Strecke1.Weg.Add(this);
                }
                if (Strecke != Strecke1.ID)
                {
                    Strecke = Strecke1.ID;
                }
            }
        }

        #endregion

    }
}
