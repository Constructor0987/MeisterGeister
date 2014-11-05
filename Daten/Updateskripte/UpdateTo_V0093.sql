--- Korrektur Nachteil Glasknochen
UPDATE [VorNachteil] SET [Vorteil] = 0, [Nachteil] = 1 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000002';
-- AudioTool Repeat mit Repeat nur ein Titel
ALTER TABLE [Audio_Playlist] ALTER COLUMN [Repeat] bit NULL;
-- AudioTool Hotkey Slider Volume speichern
INSERT INTO [Einstellung] (Kontext, Name, Typ, Wert) VALUES ('Audioplayer', 'GeneralHotkeyVolume', 'Integer','50');

-- Platzhalter-Datei für:

--Daten-Update: TODO - Gegner Uthuria und Untote

--Daten-Update: TODO - Rassen, Kulturen und Namen (vor allem für Riesland, Myranor und Uthuria)
