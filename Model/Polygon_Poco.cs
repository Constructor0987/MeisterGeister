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
    public partial class Polygon : INotifyPropertyChanged
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
        public virtual System.Guid PolygonGUID
        {
            get { return _polygonGUID; }
            set
    		{ 
    			Set(ref _polygonGUID, value);
    		}
    
        }
        private System.Guid _polygonGUID;
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
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> Left
        {
            get { return _left; }
            set
    		{ 
    			Set(ref _left, value);
    		}
    
        }
        private Nullable<double> _left;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> Top
        {
            get { return _top; }
            set
    		{ 
    			Set(ref _top, value);
    		}
    
        }
        private Nullable<double> _top;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> Right
        {
            get { return _right; }
            set
    		{ 
    			Set(ref _right, value);
    		}
    
        }
        private Nullable<double> _right;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> Bot
        {
            get { return _bot; }
            set
    		{ 
    			Set(ref _bot, value);
    		}
    
        }
        private Nullable<double> _bot;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Data
        {
            get { return _data; }
            set
    		{ 
    			Set(ref _data, value);
    		}
    
        }
        private string _data;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Gebiet> Gebiet
        {
            get
            {
                if (_gebiet == null)
                {
                    var newCollection = new FixupCollection<Gebiet>();
                    newCollection.CollectionChanged += FixupGebiet;
                    _gebiet = newCollection;
                }
                return _gebiet;
            }
            set
            {
                if (!ReferenceEquals(_gebiet, value))
                {
                    var previousValue = _gebiet as FixupCollection<Gebiet>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupGebiet;
                    }
                    _gebiet = value;
                    var newValue = value as FixupCollection<Gebiet>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupGebiet;
                    }
                }
            }
        }
        private ICollection<Gebiet> _gebiet;

        #endregion

        #region Association Fixup
    
        private void FixupGebiet(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Gebiet");
            if (e.NewItems != null)
            {
                foreach (Gebiet item in e.NewItems)
                {
                    if (!item.Polygon.Contains(this))
                    {
                        item.Polygon.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Gebiet item in e.OldItems)
                {
                    if (item.Polygon.Contains(this))
                    {
                        item.Polygon.Remove(this);
                    }
                }
            }
        }

        #endregion

    }
}
