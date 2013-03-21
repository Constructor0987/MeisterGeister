--Datenkorrekturen bei den Sonderfertigkeiten
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001536' ,N'Zauber vereinigen' ,0 ,N'Magisch' ,N'WdH 291 / WdZ 17' ,N'SF% Repräsentation, UNITATIO 10')
GO
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001536' ,'00000000-0000-0000-5e77-000000000001' ,N'3')
GO
UPDATE [Sonderfertigkeit] SET [Name] = N'Kulturkunde (Wolfalben)' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001324'
GO

--Erweiterung der Audio-Tabellen
-- Audio-Player Theme-Hintergrund Lautstärke Modifikation
ALTER TABLE Audio_Theme ADD Hintergrund_VolMod int NOT NULL DEFAULT 100
GO
-- Audio-Player Theme-Klänge Lautstärke Modifikation
ALTER TABLE Audio_Theme ADD Klang_VolMod int NOT NULL DEFAULT 100
GO

--Sonderfertigkeiten mit unterschiedlichen Werten
ALTER TABLE [Held_Sonderfertigkeit] ALTER COLUMN [Wert] SET DEFAULT ''
GO
UPDATE [Held_Sonderfertigkeit] SET [Wert] = '' WHERE Wert IS NULL
GO
ALTER TABLE [Held_Sonderfertigkeit] ALTER COLUMN [Wert] nvarchar(1200) NOT NULL 
GO
ALTER TABLE [Held_Sonderfertigkeit] DROP CONSTRAINT [PK_Held_Sonderfertigkeit]
GO
ALTER TABLE [Held_Sonderfertigkeit] ADD CONSTRAINT [PK_Held_Sonderfertigkeit] PRIMARY KEY ([HeldGUID],[SonderfertigkeitGUID], [Wert])
GO