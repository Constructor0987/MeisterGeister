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
    public partial class Kreatur : INotifyPropertyChanged
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
        public virtual System.Guid GegnerBaseGUID
        {
            get { return _gegnerBaseGUID; }
            set
            {
                if (_gegnerBaseGUID != value)
                {
                    if (Beschwörbares != null && Beschwörbares.GegnerBaseGUID != value)
                    {
                        Beschwörbares = null;
                    }
                    _gegnerBaseGUID = value;
                }
            }
    
        }
        private System.Guid _gegnerBaseGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> LOBasis
        {
            get { return _lOBasis; }
            set
    		{ 
    			Set(ref _lOBasis, value);
    		}
    
        }
        private Nullable<int> _lOBasis;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string LOZufall
        {
            get { return _lOZufall; }
            set
    		{ 
    			Set(ref _lOZufall, value);
    		}
    
        }
        private string _lOZufall;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Beschwörbares Beschwörbares
        {
            get { return _beschwörbares; }
            set
            {
                if (!ReferenceEquals(_beschwörbares, value))
                {
                    var previousValue = _beschwörbares;
                    _beschwörbares = value;
                    FixupBeschwörbares(previousValue);
                }
            }
        }
        private Beschwörbares _beschwörbares;

        #endregion

        #region Association Fixup
    
        private void FixupBeschwörbares(Beschwörbares previousValue)
        {
    		OnChanged("Beschwörbares");
            if (previousValue != null && ReferenceEquals(previousValue.Kreatur, this))
            {
                previousValue.Kreatur = null;
            }
    
            if (Beschwörbares != null)
            {
                Beschwörbares.Kreatur = this;
                if (GegnerBaseGUID != Beschwörbares.GegnerBaseGUID)
                {
                    GegnerBaseGUID = Beschwörbares.GegnerBaseGUID;
                }
            }
        }

        #endregion

    }
}
