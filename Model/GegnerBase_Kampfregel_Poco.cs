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
    public partial class GegnerBase_Kampfregel : INotifyPropertyChanged
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
        public virtual System.Guid GegnerBaseGUID
        {
            get { return _gegnerBaseGUID; }
            set
            {
                if (_gegnerBaseGUID != value)
                {
                    if (GegnerBase != null && GegnerBase.GegnerBaseGUID != value)
                    {
                        GegnerBase = null;
                    }
                    _gegnerBaseGUID = value;
                }
            }
    
        }
        private System.Guid _gegnerBaseGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid KampfregelGUID
        {
            get { return _kampfregelGUID; }
            set
            {
                if (_kampfregelGUID != value)
                {
                    if (Kampfregel != null && Kampfregel.KampfregelGUID != value)
                    {
                        Kampfregel = null;
                    }
                    _kampfregelGUID = value;
                }
            }
    
        }
        private System.Guid _kampfregelGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Erschwernis
        {
            get { return _erschwernis; }
            set
    		{ 
    			_erschwernis = value;
    			OnChanged("Erschwernis");
    		}
    
        }
        private int _erschwernis;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string TP
        {
            get { return _tP; }
            set
    		{ 
    			_tP = value;
    			OnChanged("TP");
    		}
    
        }
        private string _tP;
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

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Kampfregel Kampfregel
        {
            get { return _kampfregel; }
            set
            {
                if (!ReferenceEquals(_kampfregel, value))
                {
                    var previousValue = _kampfregel;
                    _kampfregel = value;
                    FixupKampfregel(previousValue);
                }
            }
        }
        private Kampfregel _kampfregel;
    
    	[DataMember]
        public virtual GegnerBase GegnerBase
        {
            get { return _gegnerBase; }
            set
            {
                if (!ReferenceEquals(_gegnerBase, value))
                {
                    var previousValue = _gegnerBase;
                    _gegnerBase = value;
                    FixupGegnerBase(previousValue);
                }
            }
        }
        private GegnerBase _gegnerBase;

        #endregion

        #region Association Fixup
    
        private void FixupKampfregel(Kampfregel previousValue)
        {
    		OnChanged("Kampfregel");
            if (previousValue != null && previousValue.GegnerBase_Kampfregel.Contains(this))
            {
                previousValue.GegnerBase_Kampfregel.Remove(this);
            }
    
            if (Kampfregel != null)
            {
                if (!Kampfregel.GegnerBase_Kampfregel.Contains(this))
                {
                    Kampfregel.GegnerBase_Kampfregel.Add(this);
                }
                if (KampfregelGUID != Kampfregel.KampfregelGUID)
                {
                    KampfregelGUID = Kampfregel.KampfregelGUID;
                }
            }
        }
    
        private void FixupGegnerBase(GegnerBase previousValue)
        {
    		OnChanged("GegnerBase");
            if (previousValue != null && previousValue.GegnerBase_Kampfregel.Contains(this))
            {
                previousValue.GegnerBase_Kampfregel.Remove(this);
            }
    
            if (GegnerBase != null)
            {
                if (!GegnerBase.GegnerBase_Kampfregel.Contains(this))
                {
                    GegnerBase.GegnerBase_Kampfregel.Add(this);
                }
                if (GegnerBaseGUID != GegnerBase.GegnerBaseGUID)
                {
                    GegnerBaseGUID = GegnerBase.GegnerBaseGUID;
                }
            }
        }

        #endregion

    }
}
