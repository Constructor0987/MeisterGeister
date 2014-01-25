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
    public partial class Audio_Playlist_Titel : INotifyPropertyChanged
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
        public virtual System.Guid Audio_PlaylistGUID
        {
            get { return _audio_PlaylistGUID; }
            set
            {
                if (_audio_PlaylistGUID != value)
                {
                    if (Audio_Playlist != null && Audio_Playlist.Audio_PlaylistGUID != value)
                    {
                        Audio_Playlist = null;
                    }
                    _audio_PlaylistGUID = value;
                }
            }
    
        }
        private System.Guid _audio_PlaylistGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual System.Guid Audio_TitelGUID
        {
            get { return _audio_TitelGUID; }
            set
            {
                if (_audio_TitelGUID != value)
                {
                    if (Audio_Titel != null && Audio_Titel.Audio_TitelGUID != value)
                    {
                        Audio_Titel = null;
                    }
                    _audio_TitelGUID = value;
                }
            }
    
        }
        private System.Guid _audio_TitelGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool Aktiv
        {
            get { return _aktiv; }
            set
    		{ 
    			Set(ref _aktiv, value);
    		}
    
        }
        private bool _aktiv;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Volume
        {
            get { return _volume; }
            set
    		{ 
    			Set(ref _volume, value);
    		}
    
        }
        private int _volume;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool VolumeChange
        {
            get { return _volumeChange; }
            set
    		{ 
    			Set(ref _volumeChange, value);
    		}
    
        }
        private bool _volumeChange;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int VolumeMin
        {
            get { return _volumeMin; }
            set
    		{ 
    			Set(ref _volumeMin, value);
    		}
    
        }
        private int _volumeMin;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int VolumeMax
        {
            get { return _volumeMax; }
            set
    		{ 
    			Set(ref _volumeMax, value);
    		}
    
        }
        private int _volumeMax;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual long Pause
        {
            get { return _pause; }
            set
    		{ 
    			Set(ref _pause, value);
    		}
    
        }
        private long _pause;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool PauseChange
        {
            get { return _pauseChange; }
            set
    		{ 
    			Set(ref _pauseChange, value);
    		}
    
        }
        private bool _pauseChange;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual long PauseMin
        {
            get { return _pauseMin; }
            set
    		{ 
    			Set(ref _pauseMin, value);
    		}
    
        }
        private long _pauseMin;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual long PauseMax
        {
            get { return _pauseMax; }
            set
    		{ 
    			Set(ref _pauseMax, value);
    		}
    
        }
        private long _pauseMax;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual double Speed
        {
            get { return _speed; }
            set
    		{ 
    			Set(ref _speed, value);
    		}
    
        }
        private double _speed;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool TeilAbspielen
        {
            get { return _teilAbspielen; }
            set
    		{ 
    			Set(ref _teilAbspielen, value);
    		}
    
        }
        private bool _teilAbspielen;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> TeilStart
        {
            get { return _teilStart; }
            set
    		{ 
    			Set(ref _teilStart, value);
    		}
    
        }
        private Nullable<double> _teilStart;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual Nullable<double> TeilEnde
        {
            get { return _teilEnde; }
            set
    		{ 
    			Set(ref _teilEnde, value);
    		}
    
        }
        private Nullable<double> _teilEnde;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int Rating
        {
            get { return _rating; }
            set
    		{ 
    			Set(ref _rating, value);
    		}
    
        }
        private int _rating;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual double Länge
        {
            get { return _länge; }
            set
    		{ 
    			Set(ref _länge, value);
    		}
    
        }
        private double _länge;

        #endregion

        #region Navigation Properties
    
    	[DataMember]
        public virtual Audio_Playlist Audio_Playlist
        {
            get { return _audio_Playlist; }
            set
            {
                if (!ReferenceEquals(_audio_Playlist, value))
                {
                    var previousValue = _audio_Playlist;
                    _audio_Playlist = value;
                    FixupAudio_Playlist(previousValue);
                }
            }
        }
        private Audio_Playlist _audio_Playlist;
    
    	[DataMember]
        public virtual Audio_Titel Audio_Titel
        {
            get { return _audio_Titel; }
            set
            {
                if (!ReferenceEquals(_audio_Titel, value))
                {
                    var previousValue = _audio_Titel;
                    _audio_Titel = value;
                    FixupAudio_Titel(previousValue);
                }
            }
        }
        private Audio_Titel _audio_Titel;

        #endregion

        #region Association Fixup
    
        private void FixupAudio_Playlist(Audio_Playlist previousValue)
        {
    		OnChanged("Audio_Playlist");
            if (previousValue != null && previousValue.Audio_Playlist_Titel.Contains(this))
            {
                previousValue.Audio_Playlist_Titel.Remove(this);
            }
    
            if (Audio_Playlist != null)
            {
                if (!Audio_Playlist.Audio_Playlist_Titel.Contains(this))
                {
                    Audio_Playlist.Audio_Playlist_Titel.Add(this);
                }
                if (Audio_PlaylistGUID != Audio_Playlist.Audio_PlaylistGUID)
                {
                    Audio_PlaylistGUID = Audio_Playlist.Audio_PlaylistGUID;
                }
            }
        }
    
        private void FixupAudio_Titel(Audio_Titel previousValue)
        {
    		OnChanged("Audio_Titel");
            if (previousValue != null && previousValue.Audio_Playlist_Titel.Contains(this))
            {
                previousValue.Audio_Playlist_Titel.Remove(this);
            }
    
            if (Audio_Titel != null)
            {
                if (!Audio_Titel.Audio_Playlist_Titel.Contains(this))
                {
                    Audio_Titel.Audio_Playlist_Titel.Add(this);
                }
                if (Audio_TitelGUID != Audio_Titel.Audio_TitelGUID)
                {
                    Audio_TitelGUID = Audio_Titel.Audio_TitelGUID;
                }
            }
        }

        #endregion

    }
}
