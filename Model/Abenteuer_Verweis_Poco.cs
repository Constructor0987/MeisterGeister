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
    public partial class Abenteuer_Verweis : INotifyPropertyChanged
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
        public virtual System.Guid VerweisGUID
        {
            get { return _verweisGUID; }
            set
    		{ 
    			_verweisGUID = value;
    			OnChanged("VerweisGUID");
    		}
    
        }
        private System.Guid _verweisGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid VonSzeneGUID
        {
            get { return _vonSzeneGUID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_vonSzeneGUID != value)
                    {
                        if (Abenteuer_Szene1 != null && Abenteuer_Szene1.SzeneGUID != value)
                        {
                            Abenteuer_Szene1 = null;
                        }
                        _vonSzeneGUID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
    
        }
        private System.Guid _vonSzeneGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid NachSzeneGUID
        {
            get { return _nachSzeneGUID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_nachSzeneGUID != value)
                    {
                        if (Abenteuer_Szene != null && Abenteuer_Szene.SzeneGUID != value)
                        {
                            Abenteuer_Szene = null;
                        }
                        _nachSzeneGUID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
    
        }
        private System.Guid _nachSzeneGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<System.Guid> VonEreignisGUID
        {
            get { return _vonEreignisGUID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_vonEreignisGUID != value)
                    {
                        if (Abenteuer_Ereignis1 != null && Abenteuer_Ereignis1.EreignisGUID != value)
                        {
                            Abenteuer_Ereignis1 = null;
                        }
                        _vonEreignisGUID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
    
        }
        private Nullable<System.Guid> _vonEreignisGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<System.Guid> NachEreignisGUID
        {
            get { return _nachEreignisGUID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_nachEreignisGUID != value)
                    {
                        if (Abenteuer_Ereignis != null && Abenteuer_Ereignis.EreignisGUID != value)
                        {
                            Abenteuer_Ereignis = null;
                        }
                        _nachEreignisGUID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
    
        }
        private Nullable<System.Guid> _nachEreignisGUID;

        #endregion
        #region Navigation Properties
    
    	[DataMember]
        public virtual Abenteuer_Ereignis Abenteuer_Ereignis
        {
            get { return _abenteuer_Ereignis; }
            set
            {
                if (!ReferenceEquals(_abenteuer_Ereignis, value))
                {
                    var previousValue = _abenteuer_Ereignis;
                    _abenteuer_Ereignis = value;
                    FixupAbenteuer_Ereignis(previousValue);
                }
            }
        }
        private Abenteuer_Ereignis _abenteuer_Ereignis;
    
    	[DataMember]
        public virtual Abenteuer_Ereignis Abenteuer_Ereignis1
        {
            get { return _abenteuer_Ereignis1; }
            set
            {
                if (!ReferenceEquals(_abenteuer_Ereignis1, value))
                {
                    var previousValue = _abenteuer_Ereignis1;
                    _abenteuer_Ereignis1 = value;
                    FixupAbenteuer_Ereignis1(previousValue);
                }
            }
        }
        private Abenteuer_Ereignis _abenteuer_Ereignis1;
    
    	[DataMember]
        public virtual Abenteuer_Szene Abenteuer_Szene
        {
            get { return _abenteuer_Szene; }
            set
            {
                if (!ReferenceEquals(_abenteuer_Szene, value))
                {
                    var previousValue = _abenteuer_Szene;
                    _abenteuer_Szene = value;
                    FixupAbenteuer_Szene(previousValue);
                }
            }
        }
        private Abenteuer_Szene _abenteuer_Szene;
    
    	[DataMember]
        public virtual Abenteuer_Szene Abenteuer_Szene1
        {
            get { return _abenteuer_Szene1; }
            set
            {
                if (!ReferenceEquals(_abenteuer_Szene1, value))
                {
                    var previousValue = _abenteuer_Szene1;
                    _abenteuer_Szene1 = value;
                    FixupAbenteuer_Szene1(previousValue);
                }
            }
        }
        private Abenteuer_Szene _abenteuer_Szene1;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupAbenteuer_Ereignis(Abenteuer_Ereignis previousValue)
        {
    		OnChanged("Abenteuer_Ereignis");
            if (previousValue != null && previousValue.Abenteuer_Verweis.Contains(this))
            {
                previousValue.Abenteuer_Verweis.Remove(this);
            }
    
            if (Abenteuer_Ereignis != null)
            {
                if (!Abenteuer_Ereignis.Abenteuer_Verweis.Contains(this))
                {
                    Abenteuer_Ereignis.Abenteuer_Verweis.Add(this);
                }
                if (NachEreignisGUID != Abenteuer_Ereignis.EreignisGUID)
                {
                    NachEreignisGUID = Abenteuer_Ereignis.EreignisGUID;
                }
            }
            else if (!_settingFK)
            {
                NachEreignisGUID = null;
            }
        }
    
        private void FixupAbenteuer_Ereignis1(Abenteuer_Ereignis previousValue)
        {
    		OnChanged("Abenteuer_Ereignis1");
            if (previousValue != null && previousValue.Abenteuer_Verweis1.Contains(this))
            {
                previousValue.Abenteuer_Verweis1.Remove(this);
            }
    
            if (Abenteuer_Ereignis1 != null)
            {
                if (!Abenteuer_Ereignis1.Abenteuer_Verweis1.Contains(this))
                {
                    Abenteuer_Ereignis1.Abenteuer_Verweis1.Add(this);
                }
                if (VonEreignisGUID != Abenteuer_Ereignis1.EreignisGUID)
                {
                    VonEreignisGUID = Abenteuer_Ereignis1.EreignisGUID;
                }
            }
            else if (!_settingFK)
            {
                VonEreignisGUID = null;
            }
        }
    
        private void FixupAbenteuer_Szene(Abenteuer_Szene previousValue)
        {
    		OnChanged("Abenteuer_Szene");
            if (previousValue != null && previousValue.Abenteuer_Verweis.Contains(this))
            {
                previousValue.Abenteuer_Verweis.Remove(this);
            }
    
            if (Abenteuer_Szene != null)
            {
                if (!Abenteuer_Szene.Abenteuer_Verweis.Contains(this))
                {
                    Abenteuer_Szene.Abenteuer_Verweis.Add(this);
                }
                if (NachSzeneGUID != Abenteuer_Szene.SzeneGUID)
                {
                    NachSzeneGUID = Abenteuer_Szene.SzeneGUID;
                }
            }
        }
    
        private void FixupAbenteuer_Szene1(Abenteuer_Szene previousValue)
        {
    		OnChanged("Abenteuer_Szene1");
            if (previousValue != null && previousValue.Abenteuer_Verweis1.Contains(this))
            {
                previousValue.Abenteuer_Verweis1.Remove(this);
            }
    
            if (Abenteuer_Szene1 != null)
            {
                if (!Abenteuer_Szene1.Abenteuer_Verweis1.Contains(this))
                {
                    Abenteuer_Szene1.Abenteuer_Verweis1.Add(this);
                }
                if (VonSzeneGUID != Abenteuer_Szene1.SzeneGUID)
                {
                    VonSzeneGUID = Abenteuer_Szene1.SzeneGUID;
                }
            }
        }

        #endregion
    }
}
