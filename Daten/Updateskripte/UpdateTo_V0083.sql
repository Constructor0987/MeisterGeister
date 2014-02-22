--Relative Pfade für Audio

ALTER TABLE Audio_Titel ADD Datei nvarchar(500) NULL;
ALTER TABLE Audio_Playlist_Titel ADD Reihenfolge int NOT NULL DEFAULT 0;
--Audio Spieldauer beim Abspielen berechnen (Performance)
INSERT INTO [Einstellung] (Kontext, Name, Typ, Wert) VALUES ('Audioplayer', 'AudioSpieldauerBerechnen', 'Boolean','1');
