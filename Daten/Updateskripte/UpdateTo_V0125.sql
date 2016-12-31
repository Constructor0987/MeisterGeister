--AudioTitel können in der Tonhöhe verändert werden
ALTER TABLE [Audio_Playlist_Titel] ADD [Pitch] float DEFAULT 0 NOT NULL
GO
--AudioTitel kann ein Echo hinzugefügt werden
ALTER TABLE [Audio_Playlist_Titel] Add Echo int NOT NULL default 0
GO