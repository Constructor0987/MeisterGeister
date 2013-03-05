-- Audio-Player Gewichtung des Titels
ALTER TABLE Audio_Playlist_Titel ADD Rating int NOT NULL DEFAULT 0
GO
-- Audio-Player Titellänge
ALTER TABLE Audio_Titel ADD Länge float
GO
-- Audio-Player Playlist-Länge
ALTER TABLE Audio_Playlist ADD Länge float NOT NULL DEFAULT 0
GO
-- Audio-Player Titellänge
ALTER TABLE Audio_Playlist_Titel ADD Länge float NOT NULL DEFAULT 0
GO