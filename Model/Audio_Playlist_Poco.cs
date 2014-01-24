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
    public partial class Audio_Playlist : INotifyPropertyChanged
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
        public virtual System.Guid Audio_PlaylistGUID
        {
            get { return _audio_PlaylistGUID; }
            set
    		{ 
    			OnValidatePropertyChanging("Audio_PlaylistGUID",_audio_PlaylistGUID, value);
    			_audio_PlaylistGUID = value;
    			OnChanged("Audio_PlaylistGUID");
    		}
    
        }
        private System.Guid _audio_PlaylistGUID;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Name
        {
            get { return _name; }
            set
    		{ 
    			OnValidatePropertyChanging("Name",_name, value);
    			_name = value;
    			OnChanged("Name");
    		}
    
        }
        private string _name;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual bool Hintergrundmusik
        {
            get { return _hintergrundmusik; }
            set
    		{ 
    			OnValidatePropertyChanging("Hintergrundmusik",_hintergrundmusik, value);
    			_hintergrundmusik = value;
    			OnChanged("Hintergrundmusik");
    		}
    
        }
        private bool _hintergrundmusik;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual int MaxSongsParallel
        {
            get { return _maxSongsParallel; }
            set
    		{ 
    			OnValidatePropertyChanging("MaxSongsParallel",_maxSongsParallel, value);
    			_maxSongsParallel = value;
    			OnChanged("MaxSongsParallel");
    		}
    
        }
        private int _maxSongsParallel;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual double Länge
        {
            get { return _länge; }
            set
    		{ 
    			OnValidatePropertyChanging("Länge",_länge, value);
    			_länge = value;
    			OnChanged("Länge");
    		}
    
        }
        private double _länge;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Kategorie
        {
            get { return _kategorie; }
            set
    		{ 
    			OnValidatePropertyChanging("Kategorie",_kategorie, value);
    			_kategorie = value;
    			OnChanged("Kategorie");
    		}
    
        }
        private string _kategorie;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Key
        {
            get { return _key; }
            set
    		{ 
    			OnValidatePropertyChanging("Key",_key, value);
    			_key = value;
    			OnChanged("Key");
    		}
    
        }
        private string _key;
    	///<summary>Database persistent property</summary>
    	[DataMember]
        public virtual string Modifiers
        {
            get { return _modifiers; }
            set
    		{ 
    			OnValidatePropertyChanging("Modifiers",_modifiers, value);
    			_modifiers = value;
    			OnChanged("Modifiers");
    		}
    
        }
        private string _modifiers;

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
    
    	[DataMember]
        public virtual ICollection<Audio_Theme> Audio_Theme
        {
            get
            {
                if (_audio_Theme == null)
                {
                    var newCollection = new FixupCollection<Audio_Theme>();
                    newCollection.CollectionChanged += FixupAudio_Theme;
                    _audio_Theme = newCollection;
                }
                return _audio_Theme;
            }
            set
            {
                if (!ReferenceEquals(_audio_Theme, value))
                {
                    var previousValue = _audio_Theme as FixupCollection<Audio_Theme>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupAudio_Theme;
                    }
                    _audio_Theme = value;
                    var newValue = value as FixupCollection<Audio_Theme>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupAudio_Theme;
                    }
                }
            }
        }
        private ICollection<Audio_Theme> _audio_Theme;

        #endregion

        #region Association Fixup
    
        private void FixupAudio_Playlist_Titel(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Audio_Playlist_Titel");
            if (e.NewItems != null)
            {
                foreach (Audio_Playlist_Titel item in e.NewItems)
                {
                    item.Audio_Playlist = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Audio_Playlist_Titel item in e.OldItems)
                {
                    if (ReferenceEquals(item.Audio_Playlist, this))
                    {
                        item.Audio_Playlist = null;
                    }
                }
            }
        }
    
        private void FixupAudio_Theme(object sender, NotifyCollectionChangedEventArgs e)
        {
    		OnChanged("Audio_Theme");
            if (e.NewItems != null)
            {
                foreach (Audio_Theme item in e.NewItems)
                {
                    if (!item.Audio_Playlist.Contains(this))
                    {
                        item.Audio_Playlist.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Audio_Theme item in e.OldItems)
                {
                    if (item.Audio_Playlist.Contains(this))
                    {
                        item.Audio_Playlist.Remove(this);
                    }
                }
            }
        }

        #endregion

    }
}
