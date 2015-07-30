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
    public partial class Pflanze_Setting : INotifyPropertyChanged
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
        public virtual System.Guid ID
        {
            get { return _iD; }
            set
    		{ 
    			Set(ref _iD, value);
    		}
    
        }
        private System.Guid _iD;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid PflanzeGUID
        {
            get { return _pflanzeGUID; }
            set
            {
                if (_pflanzeGUID != value)
                {
                    if (Pflanze != null && Pflanze.PflanzeGUID != value)
                    {
                        Pflanze = null;
                    }
                    _pflanzeGUID = value;
                }
            }
    
        }
        private System.Guid _pflanzeGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid SettingGUID
        {
            get { return _settingGUID; }
            set
            {
                if (_settingGUID != value)
                {
                    if (Setting != null && Setting.SettingGUID != value)
                    {
                        Setting = null;
                    }
                    _settingGUID = value;
                }
            }
    
        }
        private System.Guid _settingGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Preis
        {
            get { return _preis; }
            set
    		{ 
    			Set(ref _preis, value);
    		}
    
        }
        private string _preis;
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
        public virtual Pflanze Pflanze
        {
            get { return _pflanze; }
            set
            {
                if (!ReferenceEquals(_pflanze, value))
                {
                    var previousValue = _pflanze;
                    _pflanze = value;
                    FixupPflanze(previousValue);
                }
            }
        }
        private Pflanze _pflanze;
    
    	[DataMember]
        public virtual Setting Setting
        {
            get { return _setting; }
            set
            {
                if (!ReferenceEquals(_setting, value))
                {
                    var previousValue = _setting;
                    _setting = value;
                    FixupSetting(previousValue);
                }
            }
        }
        private Setting _setting;

        #endregion

        #region Association Fixup
    
        private void FixupPflanze(Pflanze previousValue)
        {
    		OnChanged("Pflanze");
            if (previousValue != null && previousValue.Pflanze_Setting.Contains(this))
            {
                previousValue.Pflanze_Setting.Remove(this);
            }
    
            if (Pflanze != null)
            {
                if (!Pflanze.Pflanze_Setting.Contains(this))
                {
                    Pflanze.Pflanze_Setting.Add(this);
                }
                if (PflanzeGUID != Pflanze.PflanzeGUID)
                {
                    PflanzeGUID = Pflanze.PflanzeGUID;
                }
            }
        }
    
        private void FixupSetting(Setting previousValue)
        {
    		OnChanged("Setting");
            if (previousValue != null && previousValue.Pflanze_Setting.Contains(this))
            {
                previousValue.Pflanze_Setting.Remove(this);
            }
    
            if (Setting != null)
            {
                if (!Setting.Pflanze_Setting.Contains(this))
                {
                    Setting.Pflanze_Setting.Add(this);
                }
                if (SettingGUID != Setting.SettingGUID)
                {
                    SettingGUID = Setting.SettingGUID;
                }
            }
        }

        #endregion

    }
}