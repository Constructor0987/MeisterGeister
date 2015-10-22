-- Modifikatoren für abgeleitete Werte auftrennen
ALTER TABLE [Held] ADD [LE_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [LE_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [AU_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [AU_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [AE_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [AE_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [AE_pAsP] int DEFAULT 0;
ALTER TABLE [Held] ADD [KE_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [KE_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [KE_pKaP] int DEFAULT 0;

-- Spähtweihe DDZ korrigieren: Die SF kann in drei Stufen vorkommen.
UPDATE [Sonderfertigkeit] SET [Name] = N'Spätweihe Dunkle Zeiten I' WHERE [SonderfertigkeitGUID] = N'00000000-0000-0000-005f-000000000923';
INSERT INTO [Sonderfertigkeit] ([SonderfertigkeitGUID],[Name],[HatWert],[Typ],[Literatur]) VALUES (N'00000000-0000-0000-005f-000000001988',N'Spätweihe Dunkle Zeiten II',0,N'Klerikal',N'OiC 43');
INSERT INTO [Sonderfertigkeit] ([SonderfertigkeitGUID],[Name],[HatWert],[Typ],[Literatur]) VALUES (N'00000000-0000-0000-005f-000000001989',N'Spätweihe Dunkle Zeiten III',0,N'Klerikal',N'OiC 43');
INSERT INTO [Sonderfertigkeit_Setting] ([SonderfertigkeitGUID],[SettingGUID]) VALUES (N'00000000-0000-0000-005f-000000001988',N'00000000-0000-0000-5e77-000000000002');
INSERT INTO [Sonderfertigkeit_Setting] ([SonderfertigkeitGUID],[SettingGUID]) VALUES (N'00000000-0000-0000-005f-000000001989',N'00000000-0000-0000-5e77-000000000002');

-- DSA5 Anpassungen
ALTER TABLE [Held] ADD [LeiteigenschaftMagisch] nvarchar(2);
ALTER TABLE [Held] ADD [LeiteigenschaftKlerikal] nvarchar(2);

-- Stufen-VorNachteile auf einen Eintrag zusammenführen
-- Astrale Regeneration
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013', [Wert] = '3', [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000015'
GO
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013', [Wert] = '2', [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000014'
GO
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013'
GO
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000015' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000014'
GO
UPDATE [VorNachteil] SET [Name] = 'Astrale Regeneration', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 3, [KostenGrund] = 0, [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013'
GO
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000014' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000015'
GO
-- Schnelle Heilung
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137', [Wert] = '3', [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000139'
GO
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137', [Wert] = '2', [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138'
GO
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137'
GO
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138'
GO
UPDATE [VorNachteil] SET [Name] = 'Schnelle Heilung', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 3, [KostenGrund] = 0, [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137'
GO
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000139'
GO
-- Karmale Regeneration
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447', [Wert] = '3', [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000449'
GO
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447', [Wert] = '2', [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000448'
GO
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447'
GO
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000448' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000449'
GO
UPDATE [VorNachteil] SET [Name] = 'Karmale Regeneration', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 3, [KostenGrund] = 0, [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447'
GO
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000448' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000449'
GO
-- Karmatiker (Myranor)
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '6', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000455'
GO
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '5', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000454'
GO
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '4', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000453'
GO
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '3', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000452'
GO
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '2', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000451'
GO
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450'
GO
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000455' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000454' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000453' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000452' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000451'
GO
UPDATE [VorNachteil] SET [Name] = 'Karmatiker', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 6, [KostenGrund] = 0, [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450'
GO
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000455' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000454' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000453' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000452' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000451'
GO

-- TODO MT: Daten VorNachteile für DSA 4.1 überarbeiten (Kosten, etc.)
-- TODO MT: Held_VorNachteil Kosten eintragen

-- TODO: Mod-Werte auf neue Felder verteilen
-- [LE_ModGen] = Wert nach Rasse setzen und [LE_Mod] reduzieren
-- [LE_Mod] modifizieren um VN: Hohe Lebenskraft (+1 LeP pro Stufe; max. 6), Niedrige Lebenskraft (-1 LeP pro Stufe; max. 6)

-- [AU_ModGen] = Wert nach Rasse setzen und [AU_Mod] reduzieren
-- [AU_Mod] modifizieren um VN: Ausdauernd (+2 AuP pro Stufe; max. 3 Stufen, also 6 AuP), Kurzatmig (-2 AuP pro Stufe; max. 3 Stufen, also 6 AuP)

-- [AE_Mod] modifizieren um VN: Vollzauberer (+12 AsP), Halbzauberer (+6 AsP), Viertelzauberer (-6 AsP), Zauberhaar (+7 AsP), Astralmacht (+1 AsP pro Stufe; max. 6), Niedrige Astralkraft (-1 AsP pro Stufe; max. 6)
-- [KE_Mod] modifizieren um Vorteil Geweiht [zwölfgöttliche Kirche/H'Ranga/Angrosch/Gravesh/Xo'Artal-Stadtpantheon]; SF Spätweihe Alveranische Gottheit/Spätweihe Namenloser/Spätweihe (Xo'Artal-Pantheon)/Spätweihe (Xo'Artal-Pantheon) (+24 KaP), 
--				Geweiht [nicht-alveranische Gottheit]; SF Spätweihe Nichtalveranische Gottheit/Kontakt zum Großen Geist (+12 KaP), Vorteil Sacerdos I bis IV, Vorteil Hohe Karmaenergie (DDZ) (+1 KaP pro Stufe; max. 6); SF Spätweihe Dunkle Zeiten I bis III (+6 je Stufe)


--TODO Waffenset
