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
    public partial class Held_Sonderfertigkeit : INotifyPropertyChanged
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
        public virtual int SonderfertigkeitID
        {
            get { return _sonderfertigkeitID; }
            set
            {
                if (_sonderfertigkeitID != value)
                {
                    if (Sonderfertigkeit != null && Sonderfertigkeit.SonderfertigkeitID != value)
                    {
                        Sonderfertigkeit = null;
                    }
                    _sonderfertigkeitID = value;
                }
            }
    
        }
        private int _sonderfertigkeitID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Wert
        {
            get { return _wert; }
            set
    		{ 
    			_wert = value;
    			OnChanged("Wert");
    		}
    
        }
        private string _wert;

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
        public virtual Sonderfertigkeit Sonderfertigkeit
        {
            get { return _sonderfertigkeit; }
            set
            {
                if (!ReferenceEquals(_sonderfertigkeit, value))
                {
                    var previousValue = _sonderfertigkeit;
                    _sonderfertigkeit = value;
                    FixupSonderfertigkeit(previousValue);
                }
            }
        }
        private Sonderfertigkeit _sonderfertigkeit;

        #endregion
        #region Association Fixup
    
        private void FixupHeld(Held previousValue)
        {
    		OnChanged("Held");
            if (previousValue != null && previousValue.Held_Sonderfertigkeit.Contains(this))
            {
                previousValue.Held_Sonderfertigkeit.Remove(this);
            }
    
            if (Held != null)
            {
                if (!Held.Held_Sonderfertigkeit.Contains(this))
                {
                    Held.Held_Sonderfertigkeit.Add(this);
                }
                if (HeldGUID != Held.HeldGUID)
                {
                    HeldGUID = Held.HeldGUID;
                }
            }
        }
    
        private void FixupSonderfertigkeit(Sonderfertigkeit previousValue)
        {
    		OnChanged("Sonderfertigkeit");
            if (previousValue != null && previousValue.Held_Sonderfertigkeit.Contains(this))
            {
                previousValue.Held_Sonderfertigkeit.Remove(this);
            }
    
            if (Sonderfertigkeit != null)
            {
                if (!Sonderfertigkeit.Held_Sonderfertigkeit.Contains(this))
                {
                    Sonderfertigkeit.Held_Sonderfertigkeit.Add(this);
                }
                if (SonderfertigkeitID != Sonderfertigkeit.SonderfertigkeitID)
                {
                    SonderfertigkeitID = Sonderfertigkeit.SonderfertigkeitID;
                }
            }
        }

        #endregion
    }
}
