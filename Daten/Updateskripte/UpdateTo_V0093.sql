-- AudioTool Repeat mit Repeat nur ein Titel
ALTER TABLE [Audio_Playlist] ALTER COLUMN [Repeat] bit NULL;
-- AudioTool Hotkey Slider Volume speichern
INSERT INTO [Einstellung] (Kontext, Name, Typ, Wert) VALUES ('Audioplayer', 'GeneralHotkeyVolume', 'Integer','50');
-- AudioTool Fading Werte setzen
UPDATE [Audio_Playlist] SET [Fading] = 0 WHERE [Länge] < ANY
    (SELECT CONVERT(float, CONVERT(nvarchar, [Wert])) * 2 FROM 
        [Einstellung] WHERE [Kontext] = 'Audioplayer' AND [Name] = 'Fading');