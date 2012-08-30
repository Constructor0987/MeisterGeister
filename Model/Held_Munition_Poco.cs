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
    public partial class Held_Munition : INotifyPropertyChanged
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
        public virtual string Ort
        {
            get { return _ort; }
            set
    		{ 
    			_ort = value;
    			OnChanged("Ort");
    		}
    
        }
        private string _ort;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> Anzahl
        {
            get { return _anzahl; }
            set
    		{ 
    			_anzahl = value;
    			OnChanged("Anzahl");
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

        #endregion
    }
}
