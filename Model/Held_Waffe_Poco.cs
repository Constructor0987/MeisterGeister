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
    public partial class Held_Waffe : INotifyPropertyChanged
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
        public virtual System.Guid HeldAusrüstungGUID
        {
            get { return _heldAusrüstungGUID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_heldAusrüstungGUID != value)
                    {
                        if (Held_BFAusrüstung != null && Held_BFAusrüstung.HeldAusrüstungGUID != value)
                        {
                            Held_BFAusrüstung = null;
                        }
                        _heldAusrüstungGUID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
    
        }
        private System.Guid _heldAusrüstungGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid WaffeGUID
        {
            get { return _waffeGUID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_waffeGUID != value)
                    {
                        if (Waffe != null && Waffe.WaffeGUID != value)
                        {
                            Waffe = null;
                        }
                        _waffeGUID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
    
        }
        private System.Guid _waffeGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<System.Guid> TalentGUID
        {
            get { return _talentGUID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_talentGUID != value)
                    {
                        if (Talent != null && Talent.TalentGUID != value)
                        {
                            Talent = null;
                        }
                        _talentGUID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
    
        }
        private Nullable<System.Guid> _talentGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int INI
        {
            get { return _iNI; }
            set
    		{ 
    			Set(ref _iNI, value);
    		}
    
        }
        private int _iNI;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int TPBonus
        {
            get { return _tPBonus; }
            set
    		{ 
    			Set(ref _tPBonus, value);
    		}
    
        }
        private int _tPBonus;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int WMAT
        {
            get { return _wMAT; }
            set
    		{ 
    			Set(ref _wMAT, value);
    		}
    
        }
        private int _wMAT;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int WMPA
        {
            get { return _wMPA; }
            set
    		{ 
    			Set(ref _wMPA, value);
    		}
    
        }
        private int _wMPA;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Held_BFAusrüstung Held_BFAusrüstung
        {
            get { return _held_BFAusrüstung; }
            set
            {
                if (!ReferenceEquals(_held_BFAusrüstung, value))
                {
                    var previousValue = _held_BFAusrüstung;
                    _held_BFAusrüstung = value;
                    FixupHeld_BFAusrüstung(previousValue);
                }
            }
        }
        private Held_BFAusrüstung _held_BFAusrüstung;
    
    	[DataMember]
        public virtual Talent Talent
        {
            get { return _talent; }
            set
            {
                if (!ReferenceEquals(_talent, value))
                {
                    var previousValue = _talent;
                    _talent = value;
                    FixupTalent(previousValue);
                }
            }
        }
        private Talent _talent;
    
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
    
        private bool _settingFK = false;
    
        private void FixupHeld_BFAusrüstung(Held_BFAusrüstung previousValue)
        {
    		OnChanged("Held_BFAusrüstung");
            if (previousValue != null && ReferenceEquals(previousValue.Held_Waffe, this))
            {
                previousValue.Held_Waffe = null;
            }
    
            if (Held_BFAusrüstung != null)
            {
                Held_BFAusrüstung.Held_Waffe = this;
                if (HeldAusrüstungGUID != Held_BFAusrüstung.HeldAusrüstungGUID)
                {
                    HeldAusrüstungGUID = Held_BFAusrüstung.HeldAusrüstungGUID;
                }
            }
        }
    
        private void FixupTalent(Talent previousValue)
        {
    		OnChanged("Talent");
            if (previousValue != null && previousValue.Held_Waffe.Contains(this))
            {
                previousValue.Held_Waffe.Remove(this);
            }
    
            if (Talent != null)
            {
                if (!Talent.Held_Waffe.Contains(this))
                {
                    Talent.Held_Waffe.Add(this);
                }
                if (TalentGUID != Talent.TalentGUID)
                {
                    TalentGUID = Talent.TalentGUID;
                }
            }
            else if (!_settingFK)
            {
                TalentGUID = null;
            }
        }
    
        private void FixupWaffe(Waffe previousValue)
        {
    		OnChanged("Waffe");
            if (previousValue != null && previousValue.Held_Waffe.Contains(this))
            {
                previousValue.Held_Waffe.Remove(this);
            }
    
            if (Waffe != null)
            {
                if (!Waffe.Held_Waffe.Contains(this))
                {
                    Waffe.Held_Waffe.Add(this);
                }
                if (WaffeGUID != Waffe.WaffeGUID)
                {
                    WaffeGUID = Waffe.WaffeGUID;
                }
            }
        }

        #endregion

    }
}
