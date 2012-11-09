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
    public partial class Held_Talent : INotifyPropertyChanged
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
        public virtual System.Guid TalentGUID
        {
            get { return _talentGUID; }
            set
            {
                if (_talentGUID != value)
                {
                    if (Talent != null && Talent.TalentGUID != value)
                    {
                        Talent = null;
                    }
                    _talentGUID = value;
                }
            }
    
        }
        private System.Guid _talentGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> TaW
        {
            get { return _taW; }
            set
    		{ 
    			_taW = value;
    			OnChanged("TaW");
    		}
    
        }
        private Nullable<int> _taW;
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
        public virtual string Bemerkung
        {
            get { return _bemerkung; }
            set
    		{ 
    			_bemerkung = value;
    			OnChanged("Bemerkung");
    		}
    
        }
        private string _bemerkung;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> ZuteilungAT
        {
            get { return _zuteilungAT; }
            set
    		{ 
    			_zuteilungAT = value;
    			OnChanged("ZuteilungAT");
    		}
    
        }
        private Nullable<int> _zuteilungAT;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<int> ZuteilungPA
        {
            get { return _zuteilungPA; }
            set
    		{ 
    			_zuteilungPA = value;
    			OnChanged("ZuteilungPA");
    		}
    
        }
        private Nullable<int> _zuteilungPA;

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

        #endregion

        #region Association Fixup
    
        private void FixupHeld(Held previousValue)
        {
    		OnChanged("Held");
            if (previousValue != null && previousValue.Held_Talent.Contains(this))
            {
                previousValue.Held_Talent.Remove(this);
            }
    
            if (Held != null)
            {
                if (!Held.Held_Talent.Contains(this))
                {
                    Held.Held_Talent.Add(this);
                }
                if (HeldGUID != Held.HeldGUID)
                {
                    HeldGUID = Held.HeldGUID;
                }
            }
        }
    
        private void FixupTalent(Talent previousValue)
        {
    		OnChanged("Talent");
            if (previousValue != null && previousValue.Held_Talent.Contains(this))
            {
                previousValue.Held_Talent.Remove(this);
            }
    
            if (Talent != null)
            {
                if (!Talent.Held_Talent.Contains(this))
                {
                    Talent.Held_Talent.Add(this);
                }
                if (TalentGUID != Talent.TalentGUID)
                {
                    TalentGUID = Talent.TalentGUID;
                }
            }
        }

        #endregion

    }
}
