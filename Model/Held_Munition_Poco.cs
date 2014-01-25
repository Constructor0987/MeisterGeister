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
    public partial class Held_Munition : INotifyPropertyChanged
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
        public virtual System.Guid HeldGUID
        {
            get { return _heldGUID; }
            set
            {
                if (_heldGUID != value)
                {
                    if (Held != null && Held.HeldGUID != value)
                    {
                        Held = null;
                    }
                    _heldGUID = value;
                }
            }
    
        }
        private System.Guid _heldGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid FernkampfwaffeGUID
        {
            get { return _fernkampfwaffeGUID; }
            set
            {
                if (_fernkampfwaffeGUID != value)
                {
                    if (Fernkampfwaffe != null && Fernkampfwaffe.FernkampfwaffeGUID != value)
                    {
                        Fernkampfwaffe = null;
                    }
                    _fernkampfwaffeGUID = value;
                }
            }
    
        }
        private System.Guid _fernkampfwaffeGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid MunitionGUID
        {
            get { return _munitionGUID; }
            set
            {
                if (_munitionGUID != value)
                {
                    if (Munition != null && Munition.MunitionGUID != value)
                    {
                        Munition = null;
                    }
                    _munitionGUID = value;
                }
            }
    
        }
        private System.Guid _munitionGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid TrageortGUID
        {
            get { return _trageortGUID; }
            set
            {
                if (_trageortGUID != value)
                {
                    if (Trageort != null && Trageort.TrageortGUID != value)
                    {
                        Trageort = null;
                    }
                    _trageortGUID = value;
                }
            }
    
        }
        private System.Guid _trageortGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool Gehärtet
        {
            get { return _gehärtet; }
            set
    		{ 
    			Set(ref _gehärtet, value);
    		}
    
        }
        private bool _gehärtet;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> Anzahl
        {
            get { return _anzahl; }
            set
    		{ 
    			Set(ref _anzahl, value);
    		}
    
        }
        private Nullable<int> _anzahl;

        #endregion

        #region Navigation Properties
    
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
        public virtual Held Held
        {
            get { return _held; }
            set
            {
                if (!ReferenceEquals(_held, value))
                {
                    var previousValue = _held;
                    _held = value;
                    FixupHeld(previousValue);
                }
            }
        }
        private Held _held;
    
    	[DataMember]
        public virtual Munition Munition
        {
            get { return _munition; }
            set
            {
                if (!ReferenceEquals(_munition, value))
                {
                    var previousValue = _munition;
                    _munition = value;
                    FixupMunition(previousValue);
                }
            }
        }
        private Munition _munition;
    
    	[DataMember]
        public virtual Trageort Trageort
        {
            get { return _trageort; }
            set
            {
                if (!ReferenceEquals(_trageort, value))
                {
                    var previousValue = _trageort;
                    _trageort = value;
                    FixupTrageort(previousValue);
                }
            }
        }
        private Trageort _trageort;

        #endregion

        #region Association Fixup
    
        private void FixupFernkampfwaffe(Fernkampfwaffe previousValue)
        {
    		OnChanged("Fernkampfwaffe");
            if (previousValue != null && previousValue.Held_Munition.Contains(this))
            {
                previousValue.Held_Munition.Remove(this);
            }
    
            if (Fernkampfwaffe != null)
            {
                if (!Fernkampfwaffe.Held_Munition.Contains(this))
                {
                    Fernkampfwaffe.Held_Munition.Add(this);
                }
                if (FernkampfwaffeGUID != Fernkampfwaffe.FernkampfwaffeGUID)
                {
                    FernkampfwaffeGUID = Fernkampfwaffe.FernkampfwaffeGUID;
                }
            }
        }
    
        private void FixupHeld(Held previousValue)
        {
    		OnChanged("Held");
            if (previousValue != null && previousValue.Held_Munition.Contains(this))
            {
                previousValue.Held_Munition.Remove(this);
            }
    
            if (Held != null)
            {
                if (!Held.Held_Munition.Contains(this))
                {
                    Held.Held_Munition.Add(this);
                }
                if (HeldGUID != Held.HeldGUID)
                {
                    HeldGUID = Held.HeldGUID;
                }
            }
        }
    
        private void FixupMunition(Munition previousValue)
        {
    		OnChanged("Munition");
            if (previousValue != null && previousValue.Held_Munition.Contains(this))
            {
                previousValue.Held_Munition.Remove(this);
            }
    
            if (Munition != null)
            {
                if (!Munition.Held_Munition.Contains(this))
                {
                    Munition.Held_Munition.Add(this);
                }
                if (MunitionGUID != Munition.MunitionGUID)
                {
                    MunitionGUID = Munition.MunitionGUID;
                }
            }
        }
    
        private void FixupTrageort(Trageort previousValue)
        {
    		OnChanged("Trageort");
            if (previousValue != null && previousValue.Held_Munition.Contains(this))
            {
                previousValue.Held_Munition.Remove(this);
            }
    
            if (Trageort != null)
            {
                if (!Trageort.Held_Munition.Contains(this))
                {
                    Trageort.Held_Munition.Add(this);
                }
                if (TrageortGUID != Trageort.TrageortGUID)
                {
                    TrageortGUID = Trageort.TrageortGUID;
                }
            }
        }

        #endregion

    }
}
