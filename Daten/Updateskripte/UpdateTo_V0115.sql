ALTER TABLE Audio_Playlist add Favorite bit DEFAULT 0
GO
INSERT INTO Einstellung (Kontext, Name, Typ, Wert) VALUES ('Audioplayer', 'ShowPlaylistFavorite', 'Boolean','1')
