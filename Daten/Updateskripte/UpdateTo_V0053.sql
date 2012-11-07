-- Audio-Tabellen erster Entwurf
CREATE TABLE [Audio_Titel] (
	[Audio_TitelGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(100) NOT NULL, 
	[Pfad] nvarchar(200) NOT NULL,
	CONSTRAINT [PK_Audio_Titel] PRIMARY KEY ([Audio_TitelGUID])
)
GO

CREATE TABLE [Audio_Playlist] (
	[Audio_PlaylistGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(100) NOT NULL, 
	[Hintergrundmusik] bit NOT NULL DEFAULT 0, 
	[MaxSongsParallel] int NOT NULL DEFAULT 1,
	CONSTRAINT [PK_Audio_Playlist] PRIMARY KEY ([Audio_PlaylistGUID])
)
GO

CREATE TABLE [Audio_Playlist_Titel] (
	[Audio_PlaylistGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Audio_TitelGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Aktiv] bit NOT NULL DEFAULT 1, 
	[Volume] int NOT NULL DEFAULT 50, 
	[VolumeChange] bit NOT NULL DEFAULT 0, 
	[VolumeMin] int NOT NULL DEFAULT 0, 
	[VolumeMax] int NOT NULL DEFAULT 100, 
	[Pause] bigint NOT NULL DEFAULT 500, 
	[PauseChange] bit NOT NULL DEFAULT 0, 
	[PauseMin] bigint NOT NULL DEFAULT 0, 
	[PauseMax] bigint NOT NULL DEFAULT 1000,
	CONSTRAINT [PK_Audio_Playlist_Titel] PRIMARY KEY ([Audio_PlaylistGUID], [Audio_TitelGUID]),
	CONSTRAINT fk_Audio_Playlist_Titel_Titel FOREIGN KEY ([Audio_TitelGUID])
		REFERENCES Audio_Titel ([Audio_TitelGUID])
		ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT fk_Audio_Playlist_Titel_Playlist FOREIGN KEY ([Audio_PlaylistGUID])
		REFERENCES Audio_Playlist ([Audio_PlaylistGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO