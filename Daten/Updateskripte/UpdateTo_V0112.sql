-- Spalte für Update-Hinweise auf dem Held
ALTER TABLE [Held] ADD [UpdateHinweis] nvarchar(4000) NOT NULL DEFAULT '';
UPDATE [Held] SET [UpdateHinweis] = [UpdateHinweis] + nchar(13) + nchar(10) + 'Der Modifikator der Energien (LE/AU/AE/KE) wurde auf mehrere Werte aufgeteilt. Möglicherweise müssen die Werte manuell korrigiert werden. Werte vor der Änderung: (LE-Mod=' + COALESCE(CAST([LE_Mod] AS nvarchar(20)),'') + ', AU-Mod=' + COALESCE(CAST([AU_Mod] AS nvarchar(20)),'') + ', AE-Mod=' + COALESCE(CAST([AE_Mod] AS nvarchar(20)),'') + ', KE-Mod=' + COALESCE(CAST([KE_Mod] AS nvarchar(20)),'') WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held] WHERE [LE_Mod] <> 0 OR [AU_Mod] <> 0 OR [AE_Mod] <> 0 OR [KE_Mod] <> 0);

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
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013', [Wert] = '3', [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000015';
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013', [Wert] = '2', [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000014';
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013';
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000015' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000014';
UPDATE [VorNachteil] SET [Name] = 'Astrale Regeneration', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 3, [KostenGrund] = 0, [KostenFaktor] = 4 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000013';
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000014' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000015';

-- Schnelle Heilung
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137', [Wert] = '3', [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000139';
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137', [Wert] = '2', [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138';
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137';
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138';
UPDATE [VorNachteil] SET [Name] = 'Schnelle Heilung', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 3, [KostenGrund] = 0, [KostenFaktor] = 5 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000137';
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000138' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000139';

-- Karmale Regeneration
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447', [Wert] = '3', [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000449';
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447', [Wert] = '2', [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000448';
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447';
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000448' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000449';
UPDATE [VorNachteil] SET [Name] = 'Karmale Regeneration', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 3, [KostenGrund] = 0, [KostenFaktor] = 10 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000447';
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000448' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000449';

-- Karmatiker (Myranor)
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '6', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000455';
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '5', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000454';
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '4', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000453';
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '3', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000452';
UPDATE [Held_VorNachteil] SET [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450', [Wert] = '2', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000451';
UPDATE [Held_VorNachteil] SET [Wert] = '1', [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450';
DELETE FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000455' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000454' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000453' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000452' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000451';
UPDATE [VorNachteil] SET [Name] = 'Karmatiker', [HatWert] = 1, [WertTyp] = 'int', [WertIsRoman] = 1, [WertMin] = 1, [WertMax] = 6, [KostenGrund] = 0, [KostenFaktor] = 6 WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450';
DELETE FROM [VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000455' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000454' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000453' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000452' OR [VorNachteilGUID] = '00000000-0000-0000-f024-000000000451';

-- Mod-Werte auf neue Felder verteilen
-- [LE_Mod] modifizieren um VN: Hohe Lebenskraft (+1 LeP pro Stufe; max. 6), Niedrige Lebenskraft (-1 LeP pro Stufe; max. 6)
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000113';
--#DO;
UPDATE [Held] SET [LE_Mod] = [LE_Mod] - CAST({1} AS int) WHERE [HeldGUID] = '{0}';
--#END;
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000232';
--#DO;
UPDATE [Held] SET [LE_Mod] = [LE_Mod] + CAST({1} AS int) WHERE [HeldGUID] = '{0}';
--#END;

-- [AE_Mod] modifizieren um VN: Vollzauberer (+12 AsP), Halbzauberer (+6 AsP), Viertelzauberer (-6 AsP), Zauberhaar (+7 AsP), Astralmacht (+1 AsP pro Stufe; max. 6), Niedrige Astralkraft (-1 AsP pro Stufe; max. 6)
UPDATE [Held] SET [AE_Mod] = [AE_Mod] - 12 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000151');
UPDATE [Held] SET [AE_Mod] = [AE_Mod] - 6 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000096');
UPDATE [Held] SET [AE_Mod] = [AE_Mod] + 6 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000150');
UPDATE [Held] SET [AE_Mod] = [AE_Mod] + 6 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000348');
UPDATE [Held] SET [AE_Mod] = [AE_Mod] - 7 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000158');
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000016';
--#DO;
UPDATE [Held] SET [AE_Mod] = [AE_Mod] - CAST({1} AS int) WHERE [HeldGUID] = '{0}';
--#END;
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000231';
--#DO;
UPDATE [Held] SET [AE_Mod] = [AE_Mod] + CAST({1} AS int) WHERE [HeldGUID] = '{0}';
--#END;

-- [AU_Mod] modifizieren um VN: Ausdauernd (+2 AuP pro Stufe; max. 3 Stufen, also 6 AuP), Kurzatmig (-2 AuP pro Stufe; max. 3 Stufen, also 6 AuP)
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000017';
--#DO;
UPDATE [Held] SET [AU_Mod] = [AU_Mod] - CAST({1} AS int) WHERE [HeldGUID] = '{0}';
--#END;
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000216';
--#DO;
UPDATE [Held] SET [AU_Mod] = [AU_Mod] + CAST({1} AS int) WHERE [HeldGUID] = '{0}';
--#END;

-- [KE_Mod] modifizieren um Vorteil Geweiht [zwölfgöttliche Kirche/H'Ranga/Angrosch/Gravesh/Xo'Artal-Stadtpantheon]; SF Spätweihe Alveranische Gottheit/Spätweihe Namenloser/Spätweihe (Xo'Artal-Pantheon)/Spätweihe (Xo'Artal-Pantheon) (+24 KaP), 
--				Geweiht [nicht-alveranische Gottheit]; SF Spätweihe Nichtalveranische Gottheit/Kontakt zum Großen Geist (+12 KaP), Vorteil Sacerdos I bis IV, Vorteil Hohe Karmaenergie (DDZ) (+1 KaP pro Stufe; max. 6); SF Spätweihe Dunkle Zeiten I bis III (+6 je Stufe)
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000320');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000321');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000322');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000323');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000528');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 12 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_VorNachteil] HVN WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000324');
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000361';
--#DO;
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - CAST({1} AS int) * 6 WHERE [HeldGUID] = '{0}';
--#END;
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000450';
--#DO;
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - CAST({1} AS int) * 6 WHERE [HeldGUID] = '{0}';
--#END;
--#FOREACH;
SELECT [HeldGUID], [Wert] FROM [Held_VorNachteil] WHERE [VorNachteilGUID] = '00000000-0000-0000-f024-000000000360';
--#DO;
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - CAST({1} AS int) WHERE [HeldGUID] = '{0}';
--#END;
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000000515');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000000517');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000001651');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 24 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000001651');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 18 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000001989');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 12 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000000516');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 12 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000001988');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 12 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000000485');
UPDATE [Held] SET [KE_Mod] = [KE_Mod] - 6 WHERE [HeldGUID] IN (SELECT [HeldGUID] FROM [Held_Sonderfertigkeit] WHERE [SonderfertigkeitGUID] = '00000000-0000-0000-005f-000000000923');

-- [LE_ModGen] = Wert nach Rasse setzen und [LE_Mod] reduzieren
-- [AU_ModGen] = Wert nach Rasse setzen und [AU_Mod] reduzieren
--#FOREACH;
SELECT [HeldGUID], [LEMod], [AUMod] FROM [Held] H, [Rasse] R WHERE UPPER(H.[Rasse])=UPPER(R.[Variante]) ;
--#DO;
UPDATE [Held] SET [LE_ModGen]=CAST({1} AS int), [LE_Mod] = [LE_Mod] - CAST({1} AS int), [AU_ModGen]=CAST({2} AS int), [AU_Mod] = [AU_Mod] - CAST({2} AS int) WHERE [HeldGUID] = '{0}';
--#END;
UPDATE [Held] SET [UpdateHinweis] = [UpdateHinweis] + nchar(13) + nchar(10) + 'Es konnte zu diesem Held keine passende Rasse gefunden werden. Der LE-Mod und der Rassenbonus müssen manuell angepasst werden.'
	WHERE UPPER([Rasse]) not in (SELECT UPPER([Variante]) from [Rasse]);