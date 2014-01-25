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
    public partial class Name : INotifyPropertyChanged
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
        public virtual int NameID
        {
            get { return _nameID; }
            set
    		{ 
    			Set(ref _nameID, value);
    		}
    
        }
        private int _nameID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Name1
        {
            get { return _name1; }
            set
    		{ 
    			Set(ref _name1, value);
    		}
    
        }
        private string _name1;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Herkunft
        {
            get { return _herkunft; }
            set
    		{ 
    			Set(ref _herkunft, value);
    		}
    
        }
        private string _herkunft;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Art
        {
            get { return _art; }
            set
    		{ 
    			Set(ref _art, value);
    		}
    
        }
        private string _art;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Bedeutung
        {
            get { return _bedeutung; }
            set
    		{ 
    			Set(ref _bedeutung, value);
    		}
    
        }
        private string _bedeutung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Geschlecht
        {
            get { return _geschlecht; }
            set
    		{ 
    			Set(ref _geschlecht, value);
    		}
    
        }
        private string _geschlecht;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<bool> KeineVorsilbe
        {
            get { return _keineVorsilbe; }
            set
    		{ 
    			Set(ref _keineVorsilbe, value);
    		}
    
        }
        private Nullable<bool> _keineVorsilbe;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<bool> KeineNachsilbe
        {
            get { return _keineNachsilbe; }
            set
    		{ 
    			Set(ref _keineNachsilbe, value);
    		}
    
        }
        private Nullable<bool> _keineNachsilbe;

        #endregion

    }
}
