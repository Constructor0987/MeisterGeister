-- Audio-Player TitelKategorie in die Theme-Liste integrieren
ALTER TABLE [Audio_Theme] ADD [Kategorie] nvarchar(500) NULL;
-- Audio Player Shuffle & Repeat Funktion speichern
ALTER TABLE [Audio_Playlist] ADD [Fading] bit NOT NULL DEFAULT 1;
-- Audio Player Force-Volume speichern
ALTER TABLE [Audio_Playlist] ADD [DoForce] bit NOT NULL DEFAULT 0;
ALTER TABLE [Audio_Playlist] ADD [ForceVolume] int NOT NULL DEFAULT 50;
--Audio Volume für Musik und Geräusche
INSERT INTO [Einstellung] (Kontext, Name, Typ, Wert) VALUES ('Audioplayer', 'GeneralMusikVolume', 'Integer','50');
INSERT INTO [Einstellung] (Kontext, Name, Typ, Wert) VALUES ('Audioplayer', 'GeneralGeräuscheVolume', 'Integer','100');
