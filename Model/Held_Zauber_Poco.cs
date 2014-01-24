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
    public partial class Held_Zauber : INotifyPropertyChanged
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
        public virtual System.Guid ZauberGUID
        {
            get { return _zauberGUID; }
            set
            {
                if (_zauberGUID != value)
                {
                    if (Zauber != null && Zauber.ZauberGUID != value)
                    {
                        Zauber = null;
                    }
                    _zauberGUID = value;
                }
            }
    
        }
        private System.Guid _zauberGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> ZfW
        {
            get { return _zfW; }
            set
    		{ 
    			OnValidatePropertyChanging("ZfW",_zfW, value);
    			_zfW = value;
    			OnChanged("ZfW");
    		}
    
        }
        private Nullable<int> _zfW;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Repräsentation
        {
            get { return _repräsentation; }
            set
    		{ 
    			OnValidatePropertyChanging("Repräsentation",_repräsentation, value);
    			_repräsentation = value;
    			OnChanged("Repräsentation");
    		}
    
        }
        private string _repräsentation;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Bemerkung
        {
            get { return _bemerkung; }
            set
    		{ 
    			OnValidatePropertyChanging("Bemerkung",_bemerkung, value);
    			_bemerkung = value;
    			OnChanged("Bemerkung");
    		}
    
        }
        private string _bemerkung;

        #endregion

        #region Navigation Properties
    
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
        public virtual Zauber Zauber
        {
            get { return _zauber; }
            set
            {
                if (!ReferenceEquals(_zauber, value))
                {
                    var previousValue = _zauber;
                    _zauber = value;
                    FixupZauber(previousValue);
                }
            }
        }
        private Zauber _zauber;

        #endregion

        #region Association Fixup
    
        private void FixupHeld(Held previousValue)
        {
    		OnChanged("Held");
            if (previousValue != null && previousValue.Held_Zauber.Contains(this))
            {
                previousValue.Held_Zauber.Remove(this);
            }
    
            if (Held != null)
            {
                if (!Held.Held_Zauber.Contains(this))
                {
                    Held.Held_Zauber.Add(this);
                }
                if (HeldGUID != Held.HeldGUID)
                {
                    HeldGUID = Held.HeldGUID;
                }
            }
        }
    
        private void FixupZauber(Zauber previousValue)
        {
    		OnChanged("Zauber");
            if (previousValue != null && previousValue.Held_Zauber.Contains(this))
            {
                previousValue.Held_Zauber.Remove(this);
            }
    
            if (Zauber != null)
            {
                if (!Zauber.Held_Zauber.Contains(this))
                {
                    Zauber.Held_Zauber.Add(this);
                }
                if (ZauberGUID != Zauber.ZauberGUID)
                {
                    ZauberGUID = Zauber.ZauberGUID;
                }
            }
        }

        #endregion

    }
}
