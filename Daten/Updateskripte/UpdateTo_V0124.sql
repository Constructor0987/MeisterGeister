--Möglichkeit AudioTheme in die Favoriteleiste zu integrieren
ALTER TABLE Audio_Theme add Favorite bit DEFAULT 0
GO

--Audio-Tool Volume von INT auf FLOAT
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [Volume] int NULL ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [Volume] drop DEFAULT ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [Volume] float NOT NULL ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [Volume] set default 50
GO
--Audio-Tool VolumeMin von INT auf FLOAT
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMin] int NULL ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMin] drop DEFAULT ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMin] float NOT NULL ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMin] set default 0
GO
--Audio-Tool VolumeMax von INT auf FLOAT
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMax] int NULL ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMax] drop DEFAULT ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMax] float NOT NULL ;
GO
ALTER TABLE [Audio_Playlist_Titel] ALTER COLUMN [VolumeMax] set default 100
GO