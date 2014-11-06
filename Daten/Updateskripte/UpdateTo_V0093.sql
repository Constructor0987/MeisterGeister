-- AudioTool Repeat mit Repeat nur ein Titel
ALTER TABLE [Audio_Playlist] ALTER COLUMN [Repeat] bit NULL;
-- AudioTool Hotkey Slider Volume speichern
INSERT INTO [Einstellung] (Kontext, Name, Typ, Wert) VALUES ('Audioplayer', 'GeneralHotkeyVolume', 'Integer','50');

-- evtl. weitere Updates ergänzen...