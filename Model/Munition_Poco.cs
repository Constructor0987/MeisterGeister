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

namespace MeisterGeister.Model
{
    [DataContract(IsReference=true)]
    public partial class Munition : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
    	public event PropertyChangedEventHandler PropertyChanged;
    	
    	public void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region ValidatePropertyChanging
    	protected event Extensions.ValidatePropertyChangingEventHandler ValidatePropertyChanging;
    
    	protected void OnValidatePropertyChanging(String propertyName, object currentValue, object newValue)
    	{
    		if(ValidatePropertyChanging != null)
    		{
    			ValidatePropertyChanging(this, propertyName, currentValue, newValue);
    		}
    	}

        #endregion

        #region Primitive Properties
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid MunitionGUID
        {
            get { return _munitionGUID; }
            set
    		{ 
    			OnValidatePropertyChanging("MunitionGUID",_munitionGUID, value);
    			_munitionGUID = value;
    			OnChanged("MunitionGUID");
    		}
    
        }
        private System.Guid _munitionGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Art
        {
            get { return _art; }
            set
    		{ 
    			OnValidatePropertyChanging("Art",_art, value);
    			_art = value;
    			OnChanged("Art");
    		}
    
        }
        private string _art;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Name
        {
            get { return _name; }
            set
    		{ 
    			OnValidatePropertyChanging("Name",_name, value);
    			_name = value;
    			OnChanged("Name");
    		}
    
        }
        private string _name;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Preismodifikator
        {
            get { return _preismodifikator; }
            set
    		{ 
    			OnValidatePropertyChanging("Preismodifikator",_preismodifikator, value);
    			_preismodifikator = value;
    			OnChanged("Preismodifikator");
    		}
    
        }
        private int _preismodifikator;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Bemerkungen
        {
            get { return _bemerkungen; }
            set
    		{ 
    			OnValidatePropertyChanging("Bemerkungen",_bemerkungen, value);
    			_bemerkungen = value;
    			OnChanged("Bemerkungen");
    		}
    
        }
        private string _bemerkungen;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Literatur
        {
            get { return _literatur; }
            set
    		{ 
    			OnValidatePropertyChanging("Literatur",_literatur, value);
    			_literatur = value;
    			OnChanged("Literatur");
    		}
    
        }
        private string _literatur;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Setting
        {
            get { return _setting; }
            set
    		{ 
    			OnValidatePropertyChanging("Setting",_setting, value);
    			_setting = value;
    			OnChanged("Setting");
    		}
    
        }
        private string _setting;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Probe
        {
            get { return _probe; }
            set
    		{ 
    			OnValidatePropertyChanging("Probe",_probe, value);
    			_probe = value;
    			OnChanged("Probe");
    		}
    
        }
        private int _probe;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Spitze
        {
            get { return _spitze; }
            set
    		{ 
    			OnValidatePropertyChanging("Spitze",_spitze, value);
    			_spitze = value;
    			OnChanged("Spitze");
    		}
    
        }
        private string _spitze;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool Härtbar
        {
            get { return _härtbar; }
            set
    		{ 
    			OnValidatePropertyChanging("Härtbar",_härtbar, value);
    			_härtbar = value;
    			OnChanged("Härtbar");
    		}
    
        }
        private bool _härtbar;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Held_Munition> Held_Munition
        {
            get
            {
                if (_held_Munition == null)
                {
                    var newCollection = new FixupCollection<Held_Munition>();
                    newCollection.CollectionChanged += FixupHeld_Munition;
                    _held_Munition = newCollection;
                }
                return _held_Munition;
            }
            set
            {
                if (!ReferenceEquals(_held_Munition, value))
                {
                    var previousValue = _held_Munition as FixupCollection<Held_Munition>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupHeld_Munition;
                    }
                    _held_Munition = value;
                    var newValue = value as FixupCollection<Held_Munition>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupHeld_Munition;
                    }
                }
            }
        }
        private ICollection<Held_Munition> _held_Munition;

        #endregion

        #region Association Fixup
    
        private void FixupHeld_Munition(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Held_Munition");
            if (e.NewItems != null)
            {
                foreach (Held_Munition item in e.NewItems)
                {
                    item.Munition = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Held_Munition item in e.OldItems)
                {
                    if (ReferenceEquals(item.Munition, this))
                    {
                        item.Munition = null;
                    }
                }
            }
        }

        #endregion

    }
}
