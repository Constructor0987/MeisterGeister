-- Literatur korrigieren
UPDATE [Literatur] SET [Abkürzung] = 'VTuU' WHERE [LiteraturGUID] = '00000000-0000-0000-0011-000000000011';

-- Neue Sonderfertigkeiten
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001658' ,N'Schattenpfade I' ,0 ,N'Magisch' ,N'Goldene Dächer, düstere Gassen 185' ,N'Gassenwissen 7, SF Ortkenntnis');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001659' ,N'Schattenpfade II' ,0 ,N'Magisch' ,N'Goldene Dächer, düstere Gassen 185' ,N'Gassenwissen 9, SF Ortkenntnis');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001660' ,N'Schattenpfade III' ,0 ,N'Magisch' ,N'Goldene Dächer, düstere Gassen 185' ,N'Gassenwissen 13, SF Ortkenntnis');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001661' ,N'Schattenpfade IV' ,0 ,N'Magisch' ,N'Goldene Dächer, düstere Gassen 185' ,N'Gassenwissen 15, SF Ortkenntnis');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001662' ,N'Berufsgeheimnis: Inrah' ,0 ,N'Allgemein' ,N'Kartenglück und Schicksalszeichen 184' ,N'KL 11, IN 13, V Prophezeien');

 INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001658' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001659' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001660' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001661' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001662' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);



-- eventuell weitere Daten-Updates ergänzen...

--Daten-Update: TODO - Gegner Uthuria und Untote

--Daten-Update: TODO - Rassen, Kulturen und Namen (vor allem für Riesland, Myranor und Uthuria)
