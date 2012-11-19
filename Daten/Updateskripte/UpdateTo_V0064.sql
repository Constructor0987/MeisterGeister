-- Fremdschlüssel-Einschränkung für Audio_Theme_Playlist, Gegner um weitere Felder erweitert, Held Kampfwerte als ntext

ALTER TABLE [Audio_Theme_Playlist] ADD CONSTRAINT fk_Audio_Theme_Playlist_Theme FOREIGN KEY ([Audio_ThemeGUID])
      REFERENCES Audio_Theme ([Audio_ThemeGUID])
      ON UPDATE CASCADE ON DELETE CASCADE
GO

ALTER TABLE Gegner ADD Aktionen int not null default 2
GO
ALTER TABLE Gegner ADD GS int not null default 8
GO
ALTER TABLE Gegner ADD GS2 int NULL
GO
ALTER TABLE Gegner ADD GS3 int NULL
GO

UPDATE GegnerBase SET Aktionen = 2 WHERE Aktionen = 0 OR Aktionen is NULL
GO

Alter TABLE [Held] ALTER COLUMN [Kampfwerte] ntext
GO