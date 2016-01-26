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
    public partial class Fortbewegung_Modifikation : INotifyPropertyChanged
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
        public virtual int ID
        {
            get { return _iD; }
            set
    		{ 
    			Set(ref _iD, value);
    		}
    
        }
        private int _iD;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Fortbewegung
        {
            get { return _fortbewegung; }
            set
            {
                if (_fortbewegung != value)
                {
                    if (Fortbewegung1 != null && Fortbewegung1.ID != value)
                    {
                        Fortbewegung1 = null;
                    }
                    _fortbewegung = value;
                }
            }
    
        }
        private int _fortbewegung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Wegtyp
        {
            get { return _wegtyp; }
            set
            {
                if (_wegtyp != value)
                {
                    if (Wegtyp1 != null && Wegtyp1.ID != value)
                    {
                        Wegtyp1 = null;
                    }
                    _wegtyp = value;
                }
            }
    
        }
        private int _wegtyp;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual double Multiplikator
        {
            get { return _multiplikator; }
            set
    		{ 
    			Set(ref _multiplikator, value);
    		}
    
        }
        private double _multiplikator;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Fortbewegung Fortbewegung1
        {
            get { return _fortbewegung1; }
            set
            {
                if (!ReferenceEquals(_fortbewegung1, value))
                {
                    var previousValue = _fortbewegung1;
                    _fortbewegung1 = value;
                    FixupFortbewegung1(previousValue);
                }
            }
        }
        private Fortbewegung _fortbewegung1;
    
    	[DataMember]
        public virtual Wegtyp Wegtyp1
        {
            get { return _wegtyp1; }
            set
            {
                if (!ReferenceEquals(_wegtyp1, value))
                {
                    var previousValue = _wegtyp1;
                    _wegtyp1 = value;
                    FixupWegtyp1(previousValue);
                }
            }
        }
        private Wegtyp _wegtyp1;

        #endregion

        #region Association Fixup
    
        private void FixupFortbewegung1(Fortbewegung previousValue)
        {
    		OnChanged("Fortbewegung1");
            if (previousValue != null && previousValue.Fortbewegung_Modifikation.Contains(this))
            {
                previousValue.Fortbewegung_Modifikation.Remove(this);
            }
    
            if (Fortbewegung1 != null)
            {
                if (!Fortbewegung1.Fortbewegung_Modifikation.Contains(this))
                {
                    Fortbewegung1.Fortbewegung_Modifikation.Add(this);
                }
                if (Fortbewegung != Fortbewegung1.ID)
                {
                    Fortbewegung = Fortbewegung1.ID;
                }
            }
        }
    
        private void FixupWegtyp1(Wegtyp previousValue)
        {
    		OnChanged("Wegtyp1");
            if (previousValue != null && previousValue.Fortbewegung_Modifikation.Contains(this))
            {
                previousValue.Fortbewegung_Modifikation.Remove(this);
            }
    
            if (Wegtyp1 != null)
            {
                if (!Wegtyp1.Fortbewegung_Modifikation.Contains(this))
                {
                    Wegtyp1.Fortbewegung_Modifikation.Add(this);
                }
                if (Wegtyp != Wegtyp1.ID)
                {
                    Wegtyp = Wegtyp1.ID;
                }
            }
        }

        #endregion

    }
}