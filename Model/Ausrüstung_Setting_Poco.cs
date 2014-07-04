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
    public partial class Ausrüstung_Setting : INotifyPropertyChanged
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
        public virtual System.Guid AusrüstungGUID
        {
            get { return _ausrüstungGUID; }
            set
            {
                if (_ausrüstungGUID != value)
                {
                    if (Ausrüstung != null && Ausrüstung.AusrüstungGUID != value)
                    {
                        Ausrüstung = null;
                    }
                    _ausrüstungGUID = value;
                }
            }
    
        }
        private System.Guid _ausrüstungGUID;
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
        public virtual double Preis
        {
            get { return _preis; }
            set
    		{ 
    			Set(ref _preis, value);
    		}
    
        }
        private double _preis;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Verbreitung
        {
            get { return _verbreitung; }
            set
    		{ 
    			Set(ref _verbreitung, value);
    		}
    
        }
        private string _verbreitung;
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
        public virtual Ausrüstung Ausrüstung
        {
            get { return _ausrüstung; }
            set
            {
                if (!ReferenceEquals(_ausrüstung, value))
                {
                    var previousValue = _ausrüstung;
                    _ausrüstung = value;
                    FixupAusrüstung(previousValue);
                }
            }
        }
        private Ausrüstung _ausrüstung;
    
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
    
        private void FixupAusrüstung(Ausrüstung previousValue)
        {
    		OnChanged("Ausrüstung");
            if (previousValue != null && previousValue.Ausrüstung_Setting.Contains(this))
            {
                previousValue.Ausrüstung_Setting.Remove(this);
            }
    
            if (Ausrüstung != null)
            {
                if (!Ausrüstung.Ausrüstung_Setting.Contains(this))
                {
                    Ausrüstung.Ausrüstung_Setting.Add(this);
                }
                if (AusrüstungGUID != Ausrüstung.AusrüstungGUID)
                {
                    AusrüstungGUID = Ausrüstung.AusrüstungGUID;
                }
            }
        }
    
        private void FixupSetting(Setting previousValue)
        {
    		OnChanged("Setting");
            if (previousValue != null && previousValue.Ausrüstung_Setting.Contains(this))
            {
                previousValue.Ausrüstung_Setting.Remove(this);
            }
    
            if (Setting != null)
            {
                if (!Setting.Ausrüstung_Setting.Contains(this))
                {
                    Setting.Ausrüstung_Setting.Add(this);
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
