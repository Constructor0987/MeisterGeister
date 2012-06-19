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
        public virtual System.Guid Audio_PlaylistGUID
        {
            get { return _audio_PlaylistGUID; }
            set
    		{ 
    			_audio_PlaylistGUID = value;
    			NotifyPropertyChanged("Audio_PlaylistGUID");
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
    			_name = value;
    			NotifyPropertyChanged("Name");
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
    			_hintergrundmusik = value;
    			NotifyPropertyChanged("Hintergrundmusik");
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
    			_maxSongsParallel = value;
    			NotifyPropertyChanged("MaxSongsParallel");
    		}
    
        }
        private int _maxSongsParallel;

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
    		NotifyPropertyChanged("Audio_Playlist_Titel");
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

        #endregion
    }
}
