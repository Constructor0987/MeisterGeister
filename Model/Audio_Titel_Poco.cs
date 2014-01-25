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
    public partial class Audio_Titel : INotifyPropertyChanged
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
        public virtual System.Guid Audio_TitelGUID
        {
            get { return _audio_TitelGUID; }
            set
    		{ 
    			Set(ref _audio_TitelGUID, value);
    		}
    
        }
        private System.Guid _audio_TitelGUID;
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
        public virtual string Pfad
        {
            get { return _pfad; }
            set
    		{ 
    			Set(ref _pfad, value);
    		}
    
        }
        private string _pfad;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> Länge
        {
            get { return _länge; }
            set
    		{ 
    			Set(ref _länge, value);
    		}
    
        }
        private Nullable<double> _länge;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual ICollection<Audio_Playlist_Titel> Audio_Playlist_Titel
        {
            get
            {
                if (_audio_Playlist_Titel == null)
                {
                    var newCollection = new FixupCollection<Audio_Playlist_Titel>();
                    newCollection.CollectionChanged += FixupAudio_Playlist_Titel;
                    _audio_Playlist_Titel = newCollection;
                }
                return _audio_Playlist_Titel;
            }
            set
            {
                if (!ReferenceEquals(_audio_Playlist_Titel, value))
                {
                    var previousValue = _audio_Playlist_Titel as FixupCollection<Audio_Playlist_Titel>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupAudio_Playlist_Titel;
                    }
                    _audio_Playlist_Titel = value;
                    var newValue = value as FixupCollection<Audio_Playlist_Titel>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupAudio_Playlist_Titel;
                    }
                }
            }
        }
        private ICollection<Audio_Playlist_Titel> _audio_Playlist_Titel;

        #endregion

        #region Association Fixup
    
        private void FixupAudio_Playlist_Titel(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Audio_Playlist_Titel");
            if (e.NewItems != null)
            {
                foreach (Audio_Playlist_Titel item in e.NewItems)
                {
                    item.Audio_Titel = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Audio_Playlist_Titel item in e.OldItems)
                {
                    if (ReferenceEquals(item.Audio_Titel, this))
                    {
                        item.Audio_Titel = null;
                    }
                }
            }
        }

        #endregion

    }
}
