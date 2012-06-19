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
    public partial class Sonderfertigkeit : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
    	public event PropertyChangedEventHandler PropertyChanged;
    	
    	public void NotifyPropertyChanged(String info)
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
        public virtual int SonderfertigkeitID
        {
            get { return _sonderfertigkeitID; }
            set
    		{ 
    			_sonderfertigkeitID = value;
    			NotifyPropertyChanged("SonderfertigkeitID");
    		}
    
        }
        private int _sonderfertigkeitID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Name
        {
            get { return _name; }
            set
    		{ 
    			_name = value;
    			NotifyPropertyChanged("Name");
    		}
    
        }
        private string _name;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<bool> HatWert
        {
            get { return _hatWert; }
            set
    		{ 
    			_hatWert = value;
    			NotifyPropertyChanged("HatWert");
    		}
    
        }
        private Nullable<bool> _hatWert;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Typ
        {
            get { return _typ; }
            set
    		{ 
    			_typ = value;
    			NotifyPropertyChanged("Typ");
    		}
    
        }
        private string _typ;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Setting
        {
            get { return _setting; }
            set
    		{ 
    			_setting = value;
    			NotifyPropertyChanged("Setting");
    		}
    
        }
        private string _setting;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Vorraussetzungen
        {
            get { return _vorraussetzungen; }
            set
    		{ 
    			_vorraussetzungen = value;
    			NotifyPropertyChanged("Vorraussetzungen");
    		}
    
        }
        private string _vorraussetzungen;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Literatur
        {
            get { return _literatur; }
            set
    		{ 
    			_literatur = value;
    			NotifyPropertyChanged("Literatur");
    		}
    
        }
        private string _literatur;

        #endregion
        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Held_Sonderfertigkeit> Held_Sonderfertigkeit
        {
            get
            {
                if (_held_Sonderfertigkeit == null)
                {
                    var newCollection = new FixupCollection<Held_Sonderfertigkeit>();
                    newCollection.CollectionChanged += FixupHeld_Sonderfertigkeit;
                    _held_Sonderfertigkeit = newCollection;
                }
                return _held_Sonderfertigkeit;
            }
            set
            {
                if (!ReferenceEquals(_held_Sonderfertigkeit, value))
                {
                    var previousValue = _held_Sonderfertigkeit as FixupCollection<Held_Sonderfertigkeit>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupHeld_Sonderfertigkeit;
                    }
                    _held_Sonderfertigkeit = value;
                    var newValue = value as FixupCollection<Held_Sonderfertigkeit>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupHeld_Sonderfertigkeit;
                    }
                }
            }
        }
        private ICollection<Held_Sonderfertigkeit> _held_Sonderfertigkeit;
    
    	[DataMember]
        public virtual ICollection<Zauberzeichen> Zauberzeichen
        {
            get
            {
                if (_zauberzeichen == null)
                {
                    var newCollection = new FixupCollection<Zauberzeichen>();
                    newCollection.CollectionChanged += FixupZauberzeichen;
                    _zauberzeichen = newCollection;
                }
                return _zauberzeichen;
            }
            set
            {
                if (!ReferenceEquals(_zauberzeichen, value))
                {
                    var previousValue = _zauberzeichen as FixupCollection<Zauberzeichen>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupZauberzeichen;
                    }
                    _zauberzeichen = value;
                    var newValue = value as FixupCollection<Zauberzeichen>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupZauberzeichen;
                    }
                }
            }
        }
        private ICollection<Zauberzeichen> _zauberzeichen;

        #endregion
        #region Association Fixup
    
        private void FixupHeld_Sonderfertigkeit(object sender, NotifyCollectionChangedEventArgs e)
        {
    		NotifyPropertyChanged("Held_Sonderfertigkeit");
            if (e.NewItems != null)
            {
                foreach (Held_Sonderfertigkeit item in e.NewItems)
                {
                    item.Sonderfertigkeit = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Held_Sonderfertigkeit item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sonderfertigkeit, this))
                    {
                        item.Sonderfertigkeit = null;
                    }
                }
            }
        }
    
        private void FixupZauberzeichen(object sender, NotifyCollectionChangedEventArgs e)
        {
    		NotifyPropertyChanged("Zauberzeichen");
            if (e.NewItems != null)
            {
                foreach (Zauberzeichen item in e.NewItems)
                {
                    item.Sonderfertigkeit = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Zauberzeichen item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sonderfertigkeit, this))
                    {
                        item.Sonderfertigkeit = null;
                    }
                }
            }
        }

        #endregion
    }
}
