--Literatur
CREATE TABLE [Literatur] (
   [LiteraturGUID] uniqueidentifier NOT NULL DEFAULT 'newid()' PRIMARY KEY,
   [Name] nvarchar(150) NOT NULL,
   [Abkürzung] nvarchar(50) NOT NULL,
   [Pfad] nvarchar(500),
   [Seitenoffset] int NOT NULL DEFAULT 0
)
GO
CREATE UNIQUE INDEX [UQ_Literatur_Name] ON [Literatur] ([Name] ASC)
GO
CREATE UNIQUE INDEX [UQ_Literatur_Abkürzung] ON [Literatur] ([Abkürzung] ASC)
GO

--Literaturliste
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000001' ,N'Wege der Helden' ,N'WdH' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000002' ,N'Wege des Schwerts' ,N'WdS' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000003' ,N'Wege der Zauberei' ,N'WdZ' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000004' ,N'Wege der Götter' ,N'WdG' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000005' ,N'Wege des Entdeckers' ,N'WdE' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000006' ,N'Liber Cantiones Deluxe' ,N'LCD' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000007' ,N'Wege der Alchimie' ,N'WdA' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000008' ,N'Wege der Alchimie – Preisliste' ,N'WdA-Preisliste' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000009' ,N'Stäbe, Ringe, Dschinnenlampen' ,N'SRD' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000010' ,N'Aventurisches Arsenal' ,N'AA' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000011' ,N'Von Toten und Untoten' ,N'VTuT' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000012' ,N'Tractatus contra Daemones' ,N'TCD' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000013' ,N'Elementare Gewalten' ,N'EG' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000014' ,N'Compendium Salamandris' ,N'CS' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000015' ,N'Stätten okkulter Geheimnisse' ,N'SoG' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000016' ,N'Stätten okkulter Geheimnisse – Bonusmaterial' ,N'SoG-Bonus' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000017' ,N'Zoo-Botanica Aventurica' ,N'ZBA' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000018' ,N'Handelsherr und Kiepenkerl' ,N'H&K' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000019' ,N'Efferds Wogen' ,N'EffWo' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000020' ,N'Al''Anfa und der tiefe Süden: Unter Piraten' ,N'UP' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000021' ,N'Katakomben und Kavernen' ,N'K&K' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000022' ,N'Geographia Aventurica' ,N'GA' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000023' ,N'Aventurischer Bote 123' ,N'AB123' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000024' ,N'Abenteuer 149: Esche und Kork' ,N'A149' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000025' ,N'Wege nach Myranor' ,N'WnM' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000026' ,N'Myranor (HC)' ,N'Myranor' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000027' ,N'Myranisches Arsenal' ,N'MyA' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000028' ,N'Myranische Magie' ,N'MyMa' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000029' ,N'Myranische Götter' ,N'MyG' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000030' ,N'Myranische Mysterien' ,N'MyMy' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000031' ,N'Codex Monstrosum' ,N'CM' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000032' ,N'Handelsfürsten und Wüstenkrieger' ,N'HuW' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000033' ,N'Die Dunklen Zeiten: Ordnung ins Chaos' ,N'OiC' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000034' ,N'Rakshazar: Buch der Helden' ,N'BdH' ,NULL ,0)
GO
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000035' ,N'Rakshazar: Buch der Klingen' ,N'BdK' ,NULL ,0)
GO

--Korrektur der Literaturangaben
UPDATE Ausrüstung SET Literatur = 'BdK 59' WHERE AusrüstungGUID='00000000-0000-0000-0004-000000000279'
GO
UPDATE Ausrüstung SET Literatur = REPLACE(Literatur, 'Rakshazar', 'BdK') WHERE Literatur like '%Rakshazar%'
GO
UPDATE [Ausrüstung] SET [Literatur] = N'MyA Errata 2' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000140'
GO
UPDATE [Ausrüstung] SET [Literatur] = N'MyA Errata 2' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000141'
GO

UPDATE Handelsgut SET Literatur = REPLACE(Literatur, 'Myranische Mysterien', 'MyMy') WHERE Literatur like '%Myranische Mysterien%'
GO
UPDATE Handelsgut SET Literatur = REPLACE(Literatur, 'Katakomben und Kavernen', 'K&K') WHERE Literatur like '%Katakomben und Kavernen%'
GO
UPDATE Handelsgut SET Literatur = 'WdA 190 / WdA-Preisliste 1' WHERE HandelsgutGUID IN ('00000000-0000-0000-002a-000000000367','00000000-0000-0000-002a-000000000368','00000000-0000-0000-002a-000000000369','00000000-0000-0000-002a-000000000372','00000000-0000-0000-002a-000000000377','00000000-0000-0000-002a-000000000379','00000000-0000-0000-002a-000000000392', '00000000-0000-0000-002a-000000000383', '00000000-0000-0000-002a-000000000387', '00000000-0000-0000-002a-000000000392', '00000000-0000-0000-002a-000000000395', '00000000-0000-0000-002a-000000000396', '00000000-0000-0000-002a-000000000398')
GO
UPDATE Handelsgut SET Literatur = REPLACE(Literatur, 'WdA Ergänzung', 'WdA-Preisliste') WHERE Literatur like '%WdA Ergänzung%'
GO
UPDATE Handelsgut SET Literatur = REPLACE(Literatur, 'Unter Piraten', 'UP') WHERE Literatur like '%Unter Piraten%'
GO
UPDATE [Handelsgut] SET [Literatur] = N'HuW 15 / Myranor 280, 304' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002314'
GO
UPDATE [Handelsgut] SET [Literatur] = N'WdA-Preisliste 1 / H&K 165, 166, 187' WHERE [HandelsgutGUID] IN ('00000000-0000-0000-002a-000000000079', '00000000-0000-0000-002a-000000000080')
GO
UPDATE [Handelsgut] SET [Literatur] = N'WdA-Preisliste 1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000378'
GO

UPDATE Rasse SET Literatur = REPLACE(Literatur, 'Ordnung ins Chaos', 'OiC') WHERE Literatur like '%Ordnung ins Chaos%'
GO
UPDATE Rasse SET Literatur = REPLACE(Literatur, 'Rakshazar', 'BdH') WHERE Literatur like '%Rakshazar%'
GO

UPDATE Kultur SET Literatur = REPLACE(Literatur, 'Rakshazar', 'BdH') WHERE Literatur like '%Rakshazar%'
GO

UPDATE Sonderfertigkeit SET Literatur = 'WdH 277, 73, Errata 1 / WnM 144' WHERE SonderfertigkeitGUID = '00000000-0000-0000-005f-000000000009'
GO
UPDATE Sonderfertigkeit SET Literatur = 'WdH 283 / WdS 77 / WnM 151' WHERE SonderfertigkeitGUID = '00000000-0000-0000-005f-000000000072'
GO
UPDATE Sonderfertigkeit SET Literatur = 'WdG 278 / MyG 188' WHERE SonderfertigkeitGUID = '00000000-0000-0000-005f-000000000889'
GO
UPDATE Sonderfertigkeit SET Literatur = 'OiC 76, 42, 43, 44' WHERE SonderfertigkeitGUID = '00000000-0000-0000-005f-000000000962'
GO
UPDATE Sonderfertigkeit SET Literatur = REPLACE(Literatur, 'Myranor (HC)', 'Myranor') WHERE Literatur like '%Myranor (HC)%'
GO
UPDATE Sonderfertigkeit SET Literatur = REPLACE(Literatur, 'Rakshazar', 'BdH') WHERE Literatur like '%Rakshazar%'
GO

UPDATE [Talent] SET [eBE] = N'-2' ,[Spezialisierungen] = N'' ,[Steigerung] = N'E' ,[Literatur] = N'BdH 28' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000076'
GO
UPDATE [Talent] SET [eBE] = N'-3' ,[Spezialisierungen] = N'' ,[Steigerung] = N'D', [Literatur] = N'BdH 28' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000358'
GO
UPDATE [Talent] SET [Literatur] = N'OiC 76, 42, 43, 44' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000160'
GO
UPDATE [Talent] SET [Literatur] = N'Myranor 211 / WnM 179' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000284'
GO
UPDATE Talent SET Literatur = REPLACE(Literatur, 'Myranor (HC)', 'Myranor') WHERE Literatur like '%Myranor (HC)%'
GO
UPDATE Talent SET Literatur = REPLACE(Literatur, 'Rakshazar', 'BdH') WHERE Literatur like '%Rakshazar%'
GO

UPDATE [Zauber] SET [Literatur] = N'SoG 163 / SoG-Bonus 1' WHERE [ZauberGUID]='00000000-0000-0000-00ca-000000000363'
GO
UPDATE [Zauber] SET [Literatur] = N'SoG 164 / SoG-Bonus 2' WHERE [ZauberGUID]='00000000-0000-0000-00ca-000000000364'
GO
