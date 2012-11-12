/* GUIDs für Zauber, Talente, VorNachteile und Sonderfertigkeiten und Änderungen an der Held-Tabelle */
/* Änderungen an der Held-Tabelle */

UPDATE [Held] SET [RSKopf] = 0 WHERE [RSKopf] is NULL 
GO
UPDATE [Held] SET [RSBrust] = 0 WHERE [RSBrust] IS NULL 
GO
UPDATE [Held] SET [RSBauch] = 0 WHERE [RSBauch] IS NULL 
GO
UPDATE [Held] SET [RSRücken] = 0 WHERE [RSRücken] IS NULL 
GO
UPDATE [Held] SET [RSArmL] = 0 WHERE [RSArmL] IS NULL 
GO
UPDATE [Held] SET [RSArmR] = 0 WHERE [RSArmR] IS NULL 
GO
UPDATE [Held] SET [RSBeinL] = 0 WHERE [RSBeinL] IS NULL 
GO
UPDATE [Held] SET [RSBeinR] = 0 WHERE [RSBeinR] IS NULL 
GO
UPDATE [Held] SET [WundenKopf] = 0 WHERE [WundenKopf] IS NULL 
GO
UPDATE [Held] SET [WundenBrust] = 0 WHERE [WundenBrust] IS NULL 
GO
UPDATE [Held] SET [WundenBauch] = 0 WHERE [WundenBauch] IS NULL 
GO
UPDATE [Held] SET [WundenArmL] = 0 WHERE [WundenArmL] IS NULL 
GO
UPDATE [Held] SET [WundenArmR] = 0 WHERE [WundenArmR] IS NULL 
GO
UPDATE [Held] SET [WundenBeinL] = 0 WHERE [WundenBeinL] IS NULL 
GO
UPDATE [Held] SET [WundenBeinR] = 0 WHERE [WundenBeinR] IS NULL 
GO

ALTER TABLE [Held] ALTER COLUMN [RSKopf] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [RSBrust] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [RSBauch] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [RSRücken] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [RSArmL] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [RSArmR] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [RSBeinL] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [RSBeinR] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [WundenKopf] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [WundenBrust] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [WundenBauch] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [WundenArmL] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [WundenArmR] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [WundenBeinL] int NOT NULL 
GO
ALTER TABLE [Held] ALTER COLUMN [WundenBeinR] int NOT NULL 
GO
ALTER TABLE [Held] ADD [Bild] nvarchar(500) NULL
GO
UPDATE [Held] SET [Bild]=[BildLink]
GO
ALTER TABLE [Held] DROP COLUMN [BildLink]
GO

/* GUIDs für Zauber, Talente, VorNachteile und Sonderfertigkeiten */
ALTER TABLE [Zauber] ADD [ZauberGUID] uniqueidentifier NOT NULL  ROWGUIDCOL default newid()
GO
UPDATE [Zauber] SET [ZauberGUID]='00000000-0000-0000-00CA-' + Replicate('0', 12-LEN(ZauberID)) + CAST(ZauberID as nvarchar)
GO

CREATE TABLE [Held_Zauber2] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[ZauberGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	[ZfW] int DEFAULT 0, 
	[Repräsentation] nvarchar(50) NOT NULL DEFAULT 'Mag', 
	[Bemerkung] nvarchar(300)
)
GO

INSERT INTO [Held_Zauber2] ( [HeldGUID], [ZauberGUID], [ZfW], [Repräsentation], [Bemerkung] ) SELECT T.[HeldGUID], A.[ZauberGUID], T.[ZfW], T.[Repräsentation], T.[Bemerkung] FROM [Zauber] as A, [Held_Zauber] as T where A.[ZauberID]=T.[ZauberID]
GO
DROP TABLE [Held_Zauber]
GO
sp_rename 'Held_Zauber2', 'Held_Zauber'
GO
ALTER TABLE [Held_Zauber] ADD CONSTRAINT [PK_Held_Zauber] PRIMARY KEY ([HeldGUID], [ZauberGUID], [Repräsentation])
GO
ALTER TABLE [Held_Zauber] ADD CONSTRAINT Held_Zauber_HeldFK FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO

ALTER TABLE Zauber DROP COLUMN [ZauberID]
GO
ALTER TABLE Zauber ADD CONSTRAINT [PK_Zauber] PRIMARY KEY ([ZauberGUID]);
GO

ALTER TABLE [Held_Zauber] ADD CONSTRAINT Zauber_FK FOREIGN KEY ([ZauberGUID])
		REFERENCES Zauber ([ZauberGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO

CREATE TABLE [Sonderfertigkeit2] (
	[SonderfertigkeitID] int NOT NULL IDENTITY,
	[SonderfertigkeitIDVorher] int NOT NULL,
	[SonderfertigkeitGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(100), 
	[HatWert] bit DEFAULT 0, 
	[Typ] nvarchar(100) DEFAULT 'Kampf', 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(500), 
	[Vorraussetzungen] ntext
)
GO

INSERT INTO Sonderfertigkeit2 ([SonderfertigkeitIDVorher], [Name], [HatWert], [Typ], [Literatur], [Vorraussetzungen], [Setting])
SELECT [SonderfertigkeitID], [Name], [HatWert], [Typ], [Literatur], [Vorraussetzungen], [Setting] FROM Sonderfertigkeit ORDER By SonderfertigkeitID
GO
UPDATE [Sonderfertigkeit2] SET [SonderfertigkeitGUID]='00000000-0000-0000-005F-' + Replicate('0', 12-LEN([SonderfertigkeitID])) + CAST([SonderfertigkeitID] as nvarchar)
GO

CREATE TABLE [Held_Sonderfertigkeit2] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[SonderfertigkeitGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Wert] nvarchar(1200)
)
GO

INSERT INTO [Held_Sonderfertigkeit2] ([HeldGUID],[SonderfertigkeitGUID],[Wert])
SELECT [HeldGUID],[SonderfertigkeitGUID],[Wert] FROM [Held_Sonderfertigkeit] as T, Sonderfertigkeit2 as A WHERE T.SonderfertigkeitID=A.SonderfertigkeitIDVorher
GO

DROP TABLE [Held_Sonderfertigkeit]
GO
sp_rename 'Held_Sonderfertigkeit2', 'Held_Sonderfertigkeit'
GO

ALTER TABLE [Held_Sonderfertigkeit] ADD CONSTRAINT [PK_Held_Sonderfertigkeit] PRIMARY KEY ([HeldGUID], [SonderfertigkeitGUID])
GO
ALTER TABLE [Held_Sonderfertigkeit] ADD CONSTRAINT Held_Sonderfertigkeit_HeldFK FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO

CREATE TABLE [Zauberzeichen2] (
	[ZauberzeichenGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(100) NOT NULL, 
	[Typ] nvarchar(50) NOT NULL, 
	[SonderfertigkeitGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Lernkosten] int NOT NULL DEFAULT 0, 
	[Komplexität] int NOT NULL DEFAULT 0, 
	[Merkmal] nvarchar(300), 
	[ReichweitenDivisor] float NOT NULL DEFAULT 0, 
	[Bemerkung] ntext, 
	[Verbreitung] nvarchar(300), 
	[Komponenten] ntext, 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(100) DEFAULT 'Aventurien'
)
GO
INSERT INTO [Zauberzeichen2]
	([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitGUID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting])
	Select [ZauberzeichenGUID],T.[Name],T.[Typ],[SonderfertigkeitGUID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],T.[Literatur],T.[Setting] FROM [Zauberzeichen] T, [Sonderfertigkeit2] A Where T.[SonderfertigkeitID]=A.[SonderfertigkeitIDVorher]
GO

DROP TABLE [Zauberzeichen]
GO
sp_rename 'Zauberzeichen2', 'Zauberzeichen'
GO
ALTER TABLE [Zauberzeichen] ADD CONSTRAINT [PK_Zauberzeichen] PRIMARY KEY ([ZauberzeichenGUID])
GO


ALTER TABLE [Sonderfertigkeit2] DROP COLUMN SonderfertigkeitID
GO
ALTER TABLE [Sonderfertigkeit2] DROP COLUMN SonderfertigkeitIDVorher
GO
DROP TABLE Sonderfertigkeit
GO
sp_rename 'Sonderfertigkeit2', 'Sonderfertigkeit'
GO
ALTER TABLE [Sonderfertigkeit] ADD CONSTRAINT [PK_Sonderfertigkeit] PRIMARY KEY ([SonderfertigkeitGUID])
GO
ALTER TABLE [Sonderfertigkeit] ADD CONSTRAINT [UQ_Sonderfertigkeit] UNIQUE ([Name])
GO

ALTER TABLE [Held_Sonderfertigkeit] ADD CONSTRAINT Held_Sonderfertigkeit_SonderfertigkeitFK FOREIGN KEY ([SonderfertigkeitGUID])
		REFERENCES Sonderfertigkeit ([SonderfertigkeitGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [Zauberzeichen] ADD CONSTRAINT FK_Zauberzeichen_Sonderfertigkeit FOREIGN KEY ([SonderfertigkeitGUID])
		REFERENCES Sonderfertigkeit ([SonderfertigkeitGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO


CREATE TABLE [Talent2] (
	[TalentGUID] uniqueidentifier NOT NULL ROWGUIDCOL DEFAULT newid(),
	[Talentname] nvarchar(100) NOT NULL, 
	[TalentgruppeID] int DEFAULT 7, 
	[Eigenschaft1] nvarchar(2), 
	[Eigenschaft2] nvarchar(2), 
	[Eigenschaft3] nvarchar(2), 
	[Talenttyp] nvarchar(100) DEFAULT 'Spezial', 
	[eBE] nvarchar(100), 
	[Spezialisierungen] nvarchar(250), 
	[Voraussetzungen] nvarchar(100), 
	[Steigerung] nchar(1), 
	[WikiLink] nvarchar(200), 
	[Untergruppe] nvarchar(200), 
	[Setting] nvarchar(100), 
	[Literatur] nvarchar(300)
)
GO
SET IDENTITY_INSERT [Talentgruppe] ON
GO
INSERT INTO [Talentgruppe] ([TalentgruppeID],[Gruppenname],[Kurzname]) VALUES (0,'Keine Gruppe', NULL)
GO
SET IDENTITY_INSERT [Talentgruppe] OFF
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000000','Kein Talent',0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000001','Abrichten',6,'MU','IN','CH','Spezial',NULL,'Dompteur, Echsenbändiger, Falkner, Hundeführer, Zureiter','10+: Tierkunde 4','B',NULL,NULL,NULL,'WdS 33')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000002','Ackerbau',6,'IN','FF','KO','Spezial',NULL,'evtl. nach angebauter Frucht oder nach Klimazone, zwergische Pilzzucht',NULL,'B',NULL,NULL,NULL,'WdS 33')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000003','Akrobatik',2,'MU','GE','KK','Spezial','BEx2','Balancieren, Bodenakrobatik, Schwingen, Sprünge, Winden','Körperbeherrschung 4','D',NULL,NULL,NULL,'WdS 19')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000004','Alchimie',6,'MU','KL','FF','Spezial',NULL,'Brandmittel, Gifte, Laborpraxis, Zauber-Elixiere, Farben/Lacke, Materialveredelung (Gläser, Porzellan, besondere Legierungen, Goldmacherei), Theoretische Alchimie (Elementarismus und Sympathetik)','Lesen/Schreiben, Rechnen 4','B',NULL,NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000005','Anatomie',5,'MU','KL','FF','Spezial',NULL,'nach Spezies','keine Totenangst','B',NULL,NULL,NULL,'WdS 27')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000006','Anderthalbhänder',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'E','Anderthalbhänder (Talent)','Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000007','Ansitzjagd (Armbrust)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000008','Ansitzjagd (Blasrohr)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000009','Ansitzjagd (Bogen)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000010','Ansitzjagd (Diskus)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000011','Ansitzjagd (Schleuder)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000012','Ansitzjagd (Wurfbeile)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000013','Ansitzjagd (Wurfmesser)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000014','Ansitzjagd (Wurfspeere)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000015','Armbrust',1,NULL,NULL,NULL,'Spezial','BE-5',NULL,NULL,'C',NULL,'Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000016','Athletik',2,'GE','KO','KK','Basis','BEx2','Hochsprung, Kraftakte, Langlauf, Sprint, Weitsprung',NULL,'D',NULL,NULL,NULL,'WdS 19')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000017','Bastardstäbe',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,'Myranor 223')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000018','Baukunst',5,'KL','KL','FF','Spezial',NULL,'Hochbau (Wohn- und Repräsentationsgebäude), Tiefbau (Brücken, Kanäle, Straßen), Wehranlagen','Lesen/Schreiben 4, Malen/Zeichnen 5, Rechnen 5','B',NULL,NULL,NULL,'WdS 27')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000019','Bela',1,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor','Myranor (HC) 223, 224')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000020','Belagerungswaffen',1,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,'D',NULL,'Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000021','Bergbau',6,'IN','KO','KK','Spezial',NULL,'Bergwerke, Kanalisiation, natürliche Höhlen, Sappeur, unterirdische Städte','10+: Gesteinskunde 4','B',NULL,NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000022','Betören',3,'IN','CH','CH','Spezial','BE-2','nach Kultur, Rahjakünste, Festgestaltung','Menschenkenntnis 4','B',NULL,NULL,NULL,'WdS 23')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000023','Blasrohr',1,NULL,NULL,NULL,'Spezial','BE-5',NULL,NULL,'D','Blasrohr (Talent)','Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000024','Bogen',1,NULL,NULL,NULL,'Spezial','BE-3',NULL,NULL,'E',NULL,'Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000025','Bogenbau',6,'KL','IN','FF','Spezial',NULL,'Armbrust, Bogen, Bolzen/Pfeile, Torsionswaffen','Holzbearbeitung 4','B',NULL,NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000026','Boote Fahren',6,'GE','KO','KK','Spezial',NULL,'Einmaster (Segeln), Flöße (Staken), Kanus/Kajaks (Paddeln), Ruderboote (Rudern), Eissegler',NULL,'B',NULL,NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000027','Brauer',6,'KL','FF','KK','Spezial',NULL,NULL,NULL,'B','Brauer (Talent)',NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000028','Brett-/Kartenspiel',5,'KL','KL','IN','Spezial',NULL,'nach Spiel',NULL,'B',NULL,NULL,NULL,'WdS 27')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000029','Diskus',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'D','Diskus (Talent)','Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000030','Dolche',1,NULL,NULL,NULL,'Basis','BE-1',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000031','Drucker',6,'KL','FF','KK','Spezial',NULL,'Buchdruck, Druckereitechnik, Pamphlete, Typographie','Lesen/Schreiben 6, Mechanik 4, Malen/Zeichnen 4','B','Drucker (Talent)',NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000032','Empathie',9,'MU','IN','IN','Spezial',NULL,NULL,NULL,'F',NULL,NULL,NULL,'WdH 250')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000033','Etikette',3,'KL','IN','CH','Spezial','BE-2','nach Kultur',NULL,'B',NULL,NULL,NULL,'WdS 23')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000034','Fährtensuchen',4,'KL','IN','KO','Basis',NULL,'Eis/Schnee, Gebirge, Grasland, Stadt, Sumpf, Unterirdisch, Wald/Dschungel, Wüste','10+: Sinnenschärfe 4','B',NULL,NULL,NULL,'WdS 25')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000035','Fahrzeug Lenken',6,'IN','CH','FF','Spezial',NULL,'Lastkarren, Streitwagen, Zuggespanne, Hunde- und Dachsschlitten',NULL,'B',NULL,NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000036','Fallenstellen',4,'KL','FF','KK','Spezial',NULL,'Gruben, Schlingen, Speere','10+: Wildnisleben 4','B',NULL,NULL,NULL,'WdS 25')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000037','Falschspiel',6,'MU','CH','FF','Spezial',NULL,'Würfel, Karten','Menschenkenntnis 4','B',NULL,NULL,NULL,'WdS 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000038','Fechtwaffen',1,NULL,NULL,NULL,'Spezial','BE-1',NULL,NULL,'E',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000039','Feinmechanik',6,'KL','FF','FF','Spezial',NULL,'Gold- und Silberschmied, Gravieren, Schlösser, Siegelstöcke, Trickwaffen/Fallen, Uhrwerke','Malen/Zeichnen 4','B',NULL,NULL,NULL,'WdS 35')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000040','Fesseln/Entfesseln',4,'FF','GE','KK','Spezial','BE','Entfesseln, Fesseln, Knotenkunde, Taue Spleißen, Netze Knüpfen',NULL,'B',NULL,NULL,NULL,'WdS 26')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000041','Feuersteinbearbeitung',6,'KL','FF','FF','Spezial',NULL,NULL,NULL,'B',NULL,NULL,NULL,'WdS 35')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000042','Feuerwaffen',1,NULL,NULL,NULL,'Spezial','BE-5',NULL,NULL,'C',NULL,NULL,'Myranor','MyA 110')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000043','Fischen/Angeln',4,'IN','FF','KK','Spezial',NULL,'Flüsse/Bäche, Hochseeangeln, Seen/Sumpf/Brackwasser, Strand/Riff, Seefischerei',NULL,'B',NULL,NULL,NULL,'WdS 26')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000044','Fleischer',6,'KL','FF','KK','Spezial',NULL,'Fische, Geflügel, Reptilien, Säugetiere',NULL,'B','Fleischer (Talent)',NULL,NULL,'WdS 35')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000045','Fliegen',2,'MU','IN','GE','Spezial','BE','Hexenbesen, Fliegender Teppich, weitere magische Fluggeräte (jeweils einzelnes Gerät pro Spezialisierung)',NULL,'D',NULL,NULL,NULL,'WdS 20')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000046','Fluggeräte Steuern',6,'MU','IN','FF','Spezial',NULL,'Ballon, fliegender Teppich, Gleiter, Insektopter',NULL,'B',NULL,NULL,NULL,'Myranor 212')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000047','Freies Fliegen',2,'MU','GE','KK','Basis/Spezial','BEx2','eigene Flügel, technomantische Hilfsmittel, Magie (nach Art)',NULL,'D',NULL,NULL,NULL,'Myranor 211')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000048','Gassenwissen',3,'KL','IN','CH','Spezial','BE-4','Beschatten, Hehlerei, Kontakte, Ortseinschätzung',NULL,'B',NULL,NULL,NULL,'WdS 23')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000049','Gaukeleien',2,'MU','CH','FF','Spezial','BEx2','Bauchreden, Feuerkunst, Jonglieren, Possenreißen, Taschenspielereien',NULL,'D',NULL,NULL,NULL,'WdS 20')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000050','Gefahreninstinkt',9,'KL','IN','IN','Spezial',NULL,NULL,NULL,'F',NULL,NULL,NULL,'WdH 251')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000051','Geister aufnehmen',10,'MU','IN','KO','Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Schamanentradition)',NULL,NULL,'WdZ 149')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000052','Geister bannen',10,'MU','CH','KK','Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Schamanentradition)',NULL,NULL,'WdZ 149')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000053','Geister binden',10,'KL','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Schamanentradition)',NULL,NULL,'WdZ 149')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000054','Geister rufen',10,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Schamanentradition)',NULL,NULL,'WdZ 149')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000055','Geographie',5,'KL','KL','IN','Spezial',NULL,'nach Region',NULL,'B',NULL,NULL,NULL,'WdS 27')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000056','Geräuschhexerei',9,'IN','CH','KO','Spezial',NULL,NULL,NULL,'F',NULL,NULL,NULL,'WdH 251')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000057','Gerber/Kürschner',6,'KL','FF','KO','Spezial',NULL,'Gerber, Kürschner, Trophäen',NULL,'B',NULL,NULL,NULL,'WdS 35')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000058','Geschichtswissen',5,'KL','KL','IN','Spezial',NULL,'nach Kultur oder Thema: Bau- und Kunstgeschichte, Militär-, Religions-, Wissenschaftsgeschichte','Lesen/Schreiben 4','B',NULL,NULL,NULL,'WdS 27')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000059','Gesteinskunde',5,'KL','IN','FF','Spezial',NULL,'Baugestein, Edelmetalle, Edelsteine',NULL,'B',NULL,NULL,NULL,'WdS 27')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000060','Glaskunst',6,'FF','FF','KO','Spezial',NULL,'Fensterscheiben, Glasbläserei, Linsenschleifen, Spiegel',NULL,'B',NULL,NULL,NULL,'WdS 35')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000061','Götter/Kulte',5,'KL','KL','IN','Basis',NULL,'je nach Glaubensausrichtung',NULL,'B',NULL,NULL,NULL,'WdS 27')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000062','Grobschmied',6,'FF','KO','KK','Spezial',NULL,'Drahtzieher/Sarwirker, Hufschmied, Plättner, Schwarzschmied, Spengler, Waffenschmied',NULL,'B','Grobschmied (Talent)',NULL,NULL,'WdS 35')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000063','Handel',6,'KL','IN','CH','Spezial',NULL,'nach Region (Thorwal, Weiden, Mhanadistan ...) oder nach Warengruppe (Korn, Metalle, Sklaven, Geldwechsel ...)','Rechnen 4','B',NULL,NULL,NULL,'WdS 35')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000064','Hauswirtschaft',6,'IN','CH','FF','Spezial',NULL,'Bewirtung/Feiern, Personal, Vorratshaltung',NULL,'B',NULL,NULL,NULL,'WdS 36')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000065','Heilkunde Gift',6,'MU','KL','IN','Spezial',NULL,'nach Art (alchimistische/Zaubergifte, mineralische Gifte, pflanzliche Gifte, tierische Gifte) oder Wirkung (Atemgifte, Einnahmegifte, Kontaktgifte, Waffengifte), Tierheilkunde',NULL,'B',NULL,NULL,NULL,'WdS 36')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000066','Heilkunde Krankheiten',6,'MU','KL','CH','Spezial',NULL,'nach Region, Tierheilkunde',NULL,'B',NULL,NULL,NULL,'WdS 36')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000067','Heilkunde Seele',6,'IN','CH','CH','Spezial',NULL,NULL,NULL,'B',NULL,NULL,NULL,'WdS 36')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000068','Heilkunde Wunden',6,'KL','CH','FF','Basis',NULL,'Brüche/Quetschungen, Innere Verletzungen, Schnitte, Tierheilkunde, Verbrennungen, Zahnleiden',NULL,'B',NULL,NULL,NULL,'WdS 37')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000069','Heraldik',5,'KL','KL','FF','Spezial',NULL,'nach Staat',NULL,'B',NULL,NULL,NULL,'WdS 28')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000070','Hiebwaffen',1,NULL,NULL,NULL,'Basis','BE-4',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000071','Holzbearbeitung',6,'KL','FF','KK','Basis',NULL,'Beinschnitzerei, Holzkunde,Holzwaffen, Küfer/Wagner, Möbelschreinerei/Tischlerei, Schnitzen',NULL,'B',NULL,NULL,NULL,'WdS 38')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000072','Hüttenkunde',5,'KL','IN','KO','Spezial',NULL,'nach Metall/Erz (Edelmetalle, Eisen/Stahl, Kupfer/Bronze/Messing, Zinn, Magische Metalle ...)',NULL,'B',NULL,NULL,NULL,'WdS 28')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000073','Infanteriewaffen',1,NULL,NULL,NULL,'Spezial','BE-3',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000074','Instrumentenbauer',6,'KL','IN','FF','Spezial',NULL,'Blasintrumente, Saiteninstrumente, Tasteninstrumente, Trommeln','Holzbearbeitung 4, Feinmechanik 4,Musizieren 4','B','Instrumentenbauer (Talent)',NULL,NULL,'WdS 38')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000075','Kartographie',6,'KL','KL','FF','Spezial',NULL,'Gebäude, Höhlen/Tunnel, Küsten/Meere, Landschaften, Stadt','Malen/Zeichnen 4','B',NULL,NULL,NULL,'WdS 38')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000076','Kettenkugel',1,NULL,NULL,NULL,'Spezial','Rakshazar 28','-2',NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000077','Kettenstäbe',1,NULL,NULL,NULL,'Spezial','BE-1',NULL,NULL,'E',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000078','Kettenwaffen',1,NULL,NULL,NULL,'Spezial','BE-3',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000079','Klettern',2,'MU','GE','KK','Basis','BEx2','Bergsteigen, Eisklettern, Freiklettern, Seilklettern',NULL,'D',NULL,NULL,NULL,'WdS 20')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000080','Kochen',6,'KL','IN','FF','Basis',NULL,'Backen/Braten, Festmahle, Haltbarmachen, Marschversorgung, Tränke, Vorkoster',NULL,'B',NULL,NULL,NULL,'WdS 38')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000081','Körperbeherrschung',2,'MU','IN','GE','Basis','BEx2','Fallen, Sprünge, Standfestigkeit',NULL,'D',NULL,NULL,NULL,'WdS 20')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000082','Kräuter Suchen',8,'MU','IN','FF','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000083','Kriegskunst',5,'MU','KL','CH','Spezial',NULL,'Logistik, Militärgeschichte, Monstren, Seegefechte, Strategie, Taktik (bei den Zwergen noch die Spezialisierung auf Drachen und deren Bekämpfung)',NULL,'B',NULL,NULL,NULL,'WdS 28')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000084','Kristallzucht',6,'KL','IN','FF','Spezial',NULL,NULL,NULL,'B',NULL,NULL,NULL,'WdS 38')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000085','Kryptographie',5,'KL','KL','IN','Spezial',NULL,NULL,'Lesen/Schreiben und Rechnen je 6, Sprachenkunde 4, Malen/Zeichnen 4','B',NULL,NULL,NULL,'WdS 28')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000086','Lanzenreiten',1,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,'E',NULL,'Bewaffnete AT-Technik',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000087','Lederarbeiten',6,'KL','FF','FF','Basis',NULL,'Gurte/Riemen,Lederkleidung, Lederrüstungen, Sättel, Schuhwerk',NULL,'B',NULL,NULL,NULL,'WdS 38')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000088','Lehren',3,'KL','IN','CH','Spezial',NULL,'nach Talentgruppe','10+: Menschenkenntnis 4','B',NULL,NULL,NULL,'WdS 23')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000089','Lesen/Schreiben (55 Zeichen von Amhas)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000090','Lesen/Schreiben (88 Zeichen des Sahihlam)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000091','Lesen/Schreiben (AhMaGao)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000092','Lesen/Schreiben (Alt-Imperiale-Glyphen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000093','Lesen/Schreiben (Alt-Marhym)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000094','Lesen/Schreiben (Alt-Nakramarische Bilderschrift)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000095','Lesen/Schreiben (Alt-Neristale Bilderschrift)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000096','Lesen/Schreiben (Alt-Vesayitisch)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000097','Lesen/Schreiben (Alte Tesumurrische Silbenzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000098','Lesen/Schreiben (Altes Alaani)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000099','Lesen/Schreiben (Altes Kemi)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Altes Kemi (Schrift)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000100','Lesen/Schreiben (Amulashtra)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000101','Lesen/Schreiben (Angram)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Angram (Schrift)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000102','Lesen/Schreiben (Angramm)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000103','Lesen/Schreiben (Arkanil)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'C',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000104','Lesen/Schreiben (Ashdaria)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000105','Lesen/Schreiben (Bramschoromk oder Baramun-Keilschrift)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000106','Lesen/Schreiben (Chrmk/Zelemja)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Chrmk',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000107','Lesen/Schreiben (Chuchas/Yash-Hualay-Glyphen)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'B','Chuchas',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000108','Lesen/Schreiben (Cromwyn-Runenschrift)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000109','Lesen/Schreiben (Dakrac)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000110','Lesen/Schreiben (Drakhard-Zinken)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000111','Lesen/Schreiben (Drakned-Glyphen)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'B',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000112','Lesen/Schreiben (Drayadalanische Schriftzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000113','Lesen/Schreiben (Eupherban-Codec)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000114','Lesen/Schreiben (Früh-Imperiale Bildzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000115','Lesen/Schreiben (Geheiligte Glyphen von Unau)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000116','Lesen/Schreiben (Gemein-Vesayitisch)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000117','Lesen/Schreiben (Gimaril)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000118','Lesen/Schreiben (Gjalskisch)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000119','Lesen/Schreiben (Grolmurische Silberzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000120','Lesen/Schreiben (Heilige Glyphen von Lamerinaxo)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000121','Lesen/Schreiben (Hjaldingsche Runen)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000122','Lesen/Schreiben (Hjaldingsche Runenzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000123','Lesen/Schreiben (Imperiale Lautzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000124','Lesen/Schreiben (Imperiale Zeichen)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000125','Lesen/Schreiben (Irrogolosh Glyphen)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000126','Lesen/Schreiben (Isdar-Rakshe)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000127','Lesen/Schreiben (Isdira/Asdharia)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Isdira (Schrift)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000128','Lesen/Schreiben (Kalshinish oder Shingwanische Knotenschrift)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000129','Lesen/Schreiben (Kerrishitische Silbenzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000130','Lesen/Schreiben (Kusliker Zeichen)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000131','Lesen/Schreiben (Mahapratische Silbenzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000132','Lesen/Schreiben (Mahrische Glyphen)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'B',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000133','Lesen/Schreiben (Mananesh-Glyphen)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000134','Lesen/Schreiben (Marham)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000135','Lesen/Schreiben (Nanduria)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000136','Lesen/Schreiben (Narkramarische Silbenzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000137','Lesen/Schreiben (Nedram Runen)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000138','Lesen/Schreiben (Nuoekaeshal)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000139','Lesen/Schreiben (Ouipu)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000140','Lesen/Schreiben (Rogari Glyphen)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000141','Lesen/Schreiben (Rogolan)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Rogolan (Schrift)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000142','Lesen/Schreiben (Sanskische Haken)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000143','Lesen/Schreiben (Torruksch Keilschrift)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000144','Lesen/Schreiben (Trollische Raumbilderschrift)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'C',NULL,NULL,'Aventurien, Rakshazar','WdS 30-32 / Rakshazar 34')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000145','Lesen/Schreiben (Tulamidya)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Tulamidya (Schrift)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000146','Lesen/Schreiben (Ur-Tulamidya)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Ur-Tulamidya (Schrift)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000147','Lesen/Schreiben (Ur-Tulamydia)',7,'KL','KL','FF','Spezial','Rakshazar 34',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000148','Lesen/Schreiben (Ur-Vesayitisch)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000149','Lesen/Schreiben (Uthrurim)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000150','Lesen/Schreiben (Vaestische Linienschrift)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000151','Lesen/Schreiben (Vesayitische Wort- und Silbenzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000152','Lesen/Schreiben (Vesayo-Silbenzeichen)',7,'KL','KL','FF','Spezial','Myranor (HC) 211',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000153','Lesen/Schreiben (Vesayo)',7,'KL','KL','FF','Spezial','Myranor (HC) 212',NULL,NULL,NULL,NULL,NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000154','Lesen/Schreiben (Wudu)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A',NULL,NULL,'Dunkle Zeiten','Ordnung ins Chaos 45')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000155','Lesen/Schreiben (Xah''Stsiva)',7,'KL','KL','FF','Spezial','Rakshazar 35',NULL,NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000156','Lesen/Schreiben (Zhayad)',7,'KL','KL','FF','Spezial',NULL,NULL,NULL,'A','Zhayad (Schrift)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000157','Liturgiekenntnis (Angrosch)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000158','Liturgiekenntnis (Aves)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000159','Liturgiekenntnis (Boron)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000160','Liturgiekenntnis (Dunkle Zeiten)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,'Dunkle Zeiten','Ordnung ins Chaos')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000161','Liturgiekenntnis (Efferd)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000162','Liturgiekenntnis (Firun)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000163','Liturgiekenntnis (H''Szint)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000164','Liturgiekenntnis (Hesinde)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000165','Liturgiekenntnis (Ifirn)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000166','Liturgiekenntnis (Ingerimm)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000167','Liturgiekenntnis (Kor)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000168','Liturgiekenntnis (Namenloser)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000169','Liturgiekenntnis (Nandus)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000170','Liturgiekenntnis (Peraine)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000171','Liturgiekenntnis (Phex)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000172','Liturgiekenntnis (Praios)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000173','Liturgiekenntnis (Rahja)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000174','Liturgiekenntnis (Rondra)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000175','Liturgiekenntnis (Swafnir)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000176','Liturgiekenntnis (Travia)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000177','Liturgiekenntnis (Tsa)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000178','Liturgiekenntnis (Zsahh)',11,'MU','IN','CH','Spezial',NULL,NULL,NULL,NULL,'Liturgiekenntnis',NULL,NULL,'WdG 238')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000179','Magiegespür',9,'MU','IN','IN','Spezial',NULL,NULL,NULL,'F',NULL,NULL,NULL,'WdH 254-255')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000180','Magiekunde',5,'KL','KL','IN','Spezial',NULL,'Artefaktherstellung, Dämonologie, Elementarismus, Magiehistorie, Magietheorie, Magische Analyse, Sphärologie, Zauberpraxis, Zauberwerkstatt','10+: Lesen/Schreiben 6','B',NULL,NULL,NULL,'WdS 28')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000181','Malen/Zeichnen',6,'KL','IN','FF','Basis',NULL,'Architektur, Landschaften, Porträt, technische Skizze',NULL,'B',NULL,NULL,NULL,'WdS 39')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000182','Maurer',6,'FF','GE','KK','Spezial',NULL,'Steinbau, Steinguss, Ziegelbau',NULL,'B','Maurer (Talent)',NULL,NULL,'WdS 39')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000183','Mechanik',5,'KL','KL','FF','Spezial',NULL,'Belagerungswaffen, Kräne/Hebewerke/Mahlwerke, Materialkunde, Pumpen und Wasserkraft, Windkraft','10+: Lesen/Schreiben, Malen/Zeichnen und Rechnen 6','B',NULL,NULL,NULL,'WdS 28')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000184','Menschenkenntnis',3,'KL','IN','CH','Basis',NULL,'nach Kultur',NULL,'B',NULL,NULL,NULL,'WdS 24')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000185','Metallguss',6,'KL','FF','KK','Spezial',NULL,'Klangkörper, Rohre, Reliefplatten/Statuen','Hüttenkunde 4','B',NULL,NULL,NULL,'WdS 39')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000186','Musizieren',6,'IN','CH','FF','Spezial',NULL,'Blechblasinstrumente, Flöten, Harfen, Kapellmeister/Dirigent, Lauten, Rhythmusinstrumente, Streichinstrumente, Tasteninstrumente',NULL,'B',NULL,NULL,NULL,'WdS 39')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000187','Nahrung Sammeln (Agrarland)',8,'MU','IN','FF','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000188','Nahrung Sammeln (Wildnis)',8,'MU','IN','FF','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000189','Orientierung',4,'KL','IN','IN','Basis',NULL,'Dschungel, Eis/Schnee, Gebirge, Grasland, Meer, Stadt, Unterirdisch, Wald, Wüste',NULL,'B',NULL,NULL,NULL,'WdS 26')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000190','Peitsche',1,NULL,NULL,NULL,'Spezial','BE-1',NULL,NULL,'E','Peitsche (Talent)','Bewaffnete AT-Technik',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000191','Pelura',3,'GE','FF','FF','Spezial','Das Königreich Almada 84',NULL,NULL,NULL,NULL,NULL,NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000192','Pflanzenkunde',5,'KL','IN','FF','Spezial',NULL,'nach Gelände: Dschungel, Gebirge, Grasland, Meer, Steppe, Sumpf, Tundra, Wald, Wüste',NULL,'B',NULL,NULL,NULL,'WdS 29')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000193','Philosophie',5,'KL','KL','IN','Spezial',NULL,NULL,NULL,'B',NULL,NULL,NULL,'WdS 29')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000194','Pirschjagd (Armbrust)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000195','Pirschjagd (Blasrohr)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000196','Pirschjagd (Bogen)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000197','Pirschjagd (Diskus)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000198','Pirschjagd (Schleuder)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000199','Pirschjagd (Wurfbeile)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000200','Pirschjagd (Wurfmesser)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000201','Pirschjagd (Wurfspeere)',8,'MU','IN','GE','Meta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'WdS 189-190')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000202','Prophezeien',9,'IN','IN','CH','Spezial',NULL,NULL,NULL,'F',NULL,NULL,NULL,'WdH 255')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000203','Raufen',1,NULL,NULL,NULL,'Basis','BE',NULL,NULL,'C',NULL,'Waffenloser Kampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000204','Rechnen',5,'KL','KL','IN','Basis',NULL,'Arithmetik, Buchführung, Geometrie',NULL,'B',NULL,NULL,NULL,'WdS 29')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000205','Rechtskunde',5,'KL','KL','IN','Spezial',NULL,'Gildenrecht, Kirchenrecht, Strafrecht, Staatsrecht oder nach jeweiligem Staat',NULL,'B',NULL,NULL,NULL,'WdS 29')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000206','Reiten',2,'CH','GE','KK','Spezial','BE-2','nach Tierart: Pferd/Esel/Muli, Kamel, Strauß, Reitechse, Flugechse, Hippogriff, goblinisches Reit-Wildschwein etc.',NULL,'D',NULL,NULL,NULL,'WdS 20')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000207','Ringen',1,NULL,NULL,NULL,'Basis','BE',NULL,NULL,'D',NULL,'Waffenloser Kampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000208','Ritualkenntnis (Alchimie)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000209','Ritualkenntnis (Alhanisch)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,'Dunkle Zeiten','Ordnung ins Chaos 52')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000210','Ritualkenntnis (Derwische)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000211','Ritualkenntnis (Druiden)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000212','Ritualkenntnis (Durro-Dûn)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000213','Ritualkenntnis (Geoden)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000214','Ritualkenntnis (Gildenmagie)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000215','Ritualkenntnis (Güldenländisch)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,'Dunkle Zeiten','Ordnung ins Chaos 52')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000216','Ritualkenntnis (Hexenmagie)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000217','Ritualkenntnis (Kophtanisch)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,'Dunkle Zeiten','Ordnung ins Chaos 52')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000218','Ritualkenntnis (Kristallomantie)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000219','Ritualkenntnis (Mudramulisch)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,'Dunkle Zeiten','Ordnung ins Chaos 52')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000220','Ritualkenntnis (Runenzauberei)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Runenkunde',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000221','Ritualkenntnis (Satuarisch)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,'Dunkle Zeiten','Ordnung ins Chaos 52')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000222','Ritualkenntnis (Scharlatanerie)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000223','Ritualkenntnis (Tapasuul)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,'Dunkle Zeiten','Ordnung ins Chaos 52')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000224','Ritualkenntnis (Vertrautenmagie)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 122')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000225','Ritualkenntnis (Zaubertänze)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000226','Ritualkenntnis (Zibilja)',10,NULL,NULL,NULL,'Spezial',NULL,NULL,NULL,NULL,'Ritualkenntnis (Tradition)',NULL,NULL,'WdZ 105ff.')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000227','Säbel',1,NULL,NULL,NULL,'Basis','BE-2',NULL,NULL,'D','Säbel (Talent)','Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000228','Sagen/Legenden',5,'KL','IN','CH','Basis',NULL,'nach Kultur',NULL,'B',NULL,NULL,NULL,'WdS 29')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000229','Schätzen',5,'KL','IN','IN','Spezial',NULL,'Antiquitäten, Materialwert, Schmuck, Handwerksgüter',NULL,'B',NULL,NULL,NULL,'WdS 29')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000230','Schauspielerei',3,'MU','KL','CH','Spezial',NULL,'Komödie, Posse, Tragödie','10+: Etikette, Sich Verkleiden, Singen, Überreden, Überzeugen 4','B',NULL,NULL,NULL,'WdS 24')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000231','Schleichen',2,'MU','IN','GE','Basis','BE','nach Gelände: Eis/Schnee, Gebäude, Geröll, Straßen, Wald/Dschungel',NULL,'D',NULL,NULL,NULL,'WdS 21')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000232','Schleuder',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'E','Schleuder (Talent)','Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000233','Schlösser Knacken',6,'IN','FF','FF','Spezial',NULL,'Fallen, Schlösser',NULL,'B',NULL,NULL,NULL,'WdS 39')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000234','Schnaps Brennen',6,'KL','IN','FF','Spezial',NULL,'alchimistische Destillation, Beeren-/Obstbrände, Kornbrände, Kräuterschnäpse, Lagerung',NULL,'B',NULL,NULL,NULL,'WdS 39')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000235','Schneidern',6,'KL','FF','FF','Basis',NULL,'Entwurf/Festgewänder, Flick-/Gebrauchsschneiderei, Maßschneidern, Tuchrüstungen',NULL,'B',NULL,NULL,NULL,'WdS 39')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000236','Schriftlicher Ausdruck',3,'KL','IN','IN','Spezial',NULL,'Schreiber, Schriftsteller','Lesen/Schreiben [endsprechende Schrift] 6','B',NULL,NULL,NULL,'WdS 24')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000237','Schwerter',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'E',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000238','Schwimmen',2,'GE','KO','KK','Basis','BEx2','Langstreckenschwimmen, Schnellschwimmen, Tauchen, Unterwasserkampf',NULL,'D',NULL,NULL,NULL,'WdS 21')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000239','Seefahrt',6,'FF','GE','KK','Spezial',NULL,'Ruderschiff, Segelschiff, Steuermann',NULL,'B',NULL,NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000240','Seiler',6,'FF','FF','KK','Spezial',NULL,NULL,NULL,'B','Seiler (Talent)',NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000241','Selbstbeherrschung',2,'MU','KO','KK','Basis',NULL,'Erschöpfung Ignorieren, Schmerz Unterdrücken, Störungen Ignorieren, Wunden Ignorieren',NULL,'D',NULL,NULL,NULL,'WdS 21')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000242','Sich Verkleiden',3,'MU','CH','GE','Spezial',NULL,'anderer Stand, anderes Geschlecht, andere Rasse, fremde Kultur',NULL,'B',NULL,NULL,NULL,'WdS 24')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000243','Sich Verstecken',2,'MU','IN','GE','Basis','BE-2','Gebäude, Gebirge, Grasland, Eis/Schnee, Stadt, Wald/Dschungel, Wüste',NULL,'D',NULL,NULL,NULL,'WdS 21')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000244','Singen',2,'IN','CH','CH','Basis','BE-3','Balladenvortrag, Chorgesang',NULL,'D',NULL,NULL,NULL,'WdS 21')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000245','Sinnenschärfe',2,'KL','IN','IN','Basis',NULL,'Hören, Riechen/Schmecken, Sehen, Tasten',NULL,'D',NULL,NULL,NULL,'WdS 21')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000246','Skifahren',2,'GE','GE','KO','Spezial','BE-2',NULL,NULL,'D',NULL,NULL,NULL,'WdS 21')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000247','Speere',1,NULL,NULL,NULL,'Spezial','BE-3',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000248','Sprachen Kennen (Abishat)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000249','Sprachen Kennen (Alaani)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000250','Sprachen Kennen (Alt-Broktharisch)',7,'KL','IN','CH','Spezial','Rakshazar 30',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000251','Sprachen Kennen (Alt-Imperial oder Alt-Güldenländisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 211, WnM 179',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000252','Sprachen Kennen (Alt-Marhym)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000253','Sprachen Kennen (Alt-Narkramarisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,'B','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000254','Sprachen Kennen (Alt-Neristal)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,'B','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000255','Sprachen Kennen (Alt-Orkisch)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000256','Sprachen Kennen (Alt-Ramsharij)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000257','Sprachen Kennen (Alt-Tesumurrisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000258','Sprachen Kennen (Alt-Xhalori)',7,'KL','IN','CH','Spezial','Rakshazar 33',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000259','Sprachen Kennen (Altes Kemi)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A','Altes Kemi (Sprache)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000260','Sprachen Kennen (Amhasa)',7,'KL','IN','CH','Spezial','Rakshazar 30',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000261','Sprachen Kennen (Angram)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A','Angram (Sprache)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000262','Sprachen Kennen (Angramm)',7,'KL','IN','CH','Spezial','Rakshazar 30',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000263','Sprachen Kennen (Angurak)',7,'KL','IN','CH','Spezial','Rakshazar 33',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000264','Sprachen Kennen (Asdharia)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'B',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000265','Sprachen Kennen (Asharielitisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000266','Sprachen Kennen (Ashdaria)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000267','Sprachen Kennen (Atak)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000268','Sprachen Kennen (Aureliani)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000269','Sprachen Kennen (Bansumitisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,'C','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000270','Sprachen Kennen (Bashurisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,'C','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000271','Sprachen Kennen (Boa''goram oder Banbarguinisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000272','Sprachen Kennen (Bosparano)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000273','Sprachen Kennen (Bramscho)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000274','Sprachen Kennen (Broktharisch)',7,'KL','IN','CH','Spezial','Rakshazar 30',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000275','Sprachen Kennen (Cirkel-Geheimsprache)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000276','Sprachen Kennen (Cromwyn)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000277','Sprachen Kennen (Dakrac)',7,'KL','IN','CH','Spezial','Rakshazar 33',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000278','Sprachen Kennen (Dorinthisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'B',NULL,NULL,'Myranor','WnM 179')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000279','Sprachen Kennen (Drachisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000280','Sprachen Kennen (Drayadalanisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000281','Sprachen Kennen (Erasumisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,'B','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000282','Sprachen Kennen (Eupherban-Haussprache)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000283','Sprachen Kennen (Ferkina)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A','Ferkina (Sprache)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000284','Sprachen Kennen (Früh-Imperial)',7,'KL','IN','CH','Spezial','Myranor (HC) 211, WnM 179',NULL,NULL,'B','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000285','Sprachen Kennen (Füchsisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000286','Sprachen Kennen (Garethi)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000287','Sprachen Kennen (Gemein-Amaunal oder AhMa)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000288','Sprachen Kennen (Gemein-Imperial)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000289','Sprachen Kennen (Gmer)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000290','Sprachen Kennen (Goblinisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000291','Sprachen Kennen (Goragari)',7,'KL','IN','CH','Spezial','Rakshazar 30',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000292','Sprachen Kennen (Grolmisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000293','Sprachen Kennen (Grolmurisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000294','Sprachen Kennen (Hiero-Amaunal oder AhMaGao)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000295','Sprachen Kennen (Hiero-Imperial)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000296','Sprachen Kennen (Hjaldingsch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32 / Myranor (HC) 211')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000297','Sprachen Kennen (Ipexco)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000298','Sprachen Kennen (Isdar-Rakshi)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000299','Sprachen Kennen (Isdira)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000300','Sprachen Kennen (Jiktisch)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000301','Sprachen Kennen (Kawash)',7,'KL','IN','CH','Spezial','Rakshazar 30',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000302','Sprachen Kennen (Kentorisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000303','Sprachen Kennen (Kerrishitisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000304','Sprachen Kennen (Koboldisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000305','Sprachen Kennen (Leonal oder Khorrzu)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000306','Sprachen Kennen (Loualilisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000307','Sprachen Kennen (Lyncal oder Fhi''Ai)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000308','Sprachen Kennen (Mahapratisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000309','Sprachen Kennen (Mahrisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000310','Sprachen Kennen (Marham)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000311','Sprachen Kennen (Mohisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000312','Sprachen Kennen (Molochisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000313','Sprachen Kennen (Myranisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,'A','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000314','Sprachen Kennen (Narkramarisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000315','Sprachen Kennen (Neckergesang)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000316','Sprachen Kennen (Nederi)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000317','Sprachen Kennen (Nedermannisch)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000318','Sprachen Kennen (Neristal)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000319','Sprachen Kennen (Nujuka)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000320','Sprachen Kennen (Oloarkh)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000321','Sprachen Kennen (Ologhaijan)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000322','Sprachen Kennen (Olura)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000323','Sprachen Kennen (Padir oder Bhagrach)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000324','Sprachen Kennen (Parnhal)',7,'KL','IN','CH','Spezial','Rakshazar 32',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000325','Sprachen Kennen (Porto-Imperial)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,'B','',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000326','Sprachen Kennen (Rabensprache)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000327','Sprachen Kennen (Raversaran)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000328','Sprachen Kennen (Rissoal)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,'Myranor','WdS 30-32 / Myranor (HC) 212')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000329','Sprachen Kennen (Rogolan)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A','Rogolan (Sprache)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000330','Sprachen Kennen (Rogorakshi)',7,'KL','IN','CH','Spezial','Rakshazar 30',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000331','Sprachen Kennen (Rssahh)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000332','Sprachen Kennen (Ruuz)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000333','Sprachen Kennen (Sanskitarisch)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000334','Sprachen Kennen (Shingwanisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000335','Sprachen Kennen (Shinoq)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000336','Sprachen Kennen (Slachkarisch)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000337','Sprachen Kennen (Sycc)',7,'KL','IN','CH','Spezial','Rakshazar 33',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000338','Sprachen Kennen (Tchif)',7,'KL','IN','CH','Spezial','Rakshazar 33',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000339','Sprachen Kennen (Thorwalsch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000340','Sprachen Kennen (Trollisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000341','Sprachen Kennen (Truag)',7,'KL','IN','CH','Spezial','Rakshazar 34',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000342','Sprachen Kennen (Tulamidya)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A','Tulamidya (Sprache)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000343','Sprachen Kennen (Ur-Tulamidya)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A','Ur-Tulamidya (Sprache)',NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000344','Sprachen Kennen (Ur-Tulamydia)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000345','Sprachen Kennen (Uthurim)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000346','Sprachen Kennen (Uzujuma)',7,'KL','IN','CH','Spezial','Rakshazar 34',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000347','Sprachen Kennen (Vaestisch)',7,'KL','IN','CH','Spezial','Rakshazar 31',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000348','Sprachen Kennen (Vinshinisch)',7,'KL','IN','CH','Spezial','Myranor (HC) 212',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000349','Sprachen Kennen (Wudu)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,'Dunkle Zeiten','Ordnung ins Chaos 45')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000350','Sprachen Kennen (Xah''Hombri)',7,'KL','IN','CH','Spezial','Rakshazar 33',NULL,NULL,NULL,'',NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000351','Sprachen Kennen (Yachyach)',7,'KL','IN','CH','Spezial','Myranor (HC) 211',NULL,NULL,NULL,'',NULL,NULL,'Myranor')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000352','Sprachen Kennen (Zelemja)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000353','Sprachen Kennen (Zhayad)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000354','Sprachen Kennen (Zhulchammaqra)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000355','Sprachen Kennen (ZLit)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000356','Sprachen Kennen (Zyklopäisch)',7,'KL','IN','CH','Spezial',NULL,NULL,NULL,'A',NULL,NULL,NULL,'WdS 30-32')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000357','Sprachenkunde',5,'KL','KL','IN','Spezial',NULL,'nach Sprachfamilie',NULL,'B',NULL,NULL,NULL,'WdS 30')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000358','Sprühwaffen',1,NULL,NULL,NULL,'Spezial','Rakshazar 28','-3',NULL,NULL,NULL,NULL,NULL,'Rakshazar')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000359','Staatskunst',5,'KL','IN','CH','Spezial',NULL,'Diplomatie, Intrige, Verwaltung',NULL,'B',NULL,NULL,NULL,'WdS 30')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000360','Stäbe',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000361','Steinmetz',6,'FF','FF','KK','Spezial',NULL,'Baugestein, Reliefs, Statuen','Gesteinskunde 4','B','Steinmetz (Talent)',NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000362','Steinschneider/Juwelier',6,'IN','FF','FF','Spezial',NULL,NULL,'10+: Gesteinskunde 4','B',NULL,NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000363','Stellmacher',6,'KL','FF','KK','Spezial',NULL,'Karren/Wagen, Schlitten, Streitwagen','Holzbearbeitung 4, Lederarbeiten 4, Grobschmied 4','B','Stellmacher (Talent)',NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000364','Sternkunde',5,'KL','KL','IN','Spezial',NULL,'Himmelskartographie, Horoskope, Navigation, Zeitbestimmung','10+: Lesen/Schreiben, Rechnen und Sinnenschärfe 6','B',NULL,NULL,NULL,'WdS 30')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000365','Stimmen Imitieren',2,'KL','IN','CH','Spezial','BE-4','Haustiere, Jagdwild, Menschliche Stimmen, Raubtiere, Vögel','Sinnenschärfe 4','D',NULL,NULL,NULL,'WdS 22')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000366','Stoffe Färben',6,'KL','FF','KK','Spezial',NULL,'alchimistische Farben, mineralische Farben, pflanzliche Farben, tierische Farben oder nach Art des Stoffes oder Stoffdruck',NULL,'B',NULL,NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000367','Tanzen',2,'CH','GE','GE','Basis','BEx2','Ausdruckstänze, Formationstänze, Höfische Tänze (Etikette 6+ als Voraussetzung), Kulttänze (Götter/Kulte 6+ als Voraussetzung), Meditationstänze',NULL,'D',NULL,NULL,NULL,'WdS 22')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000368','Taschendiebstahl',2,'MU','IN','FF','Spezial','BEx2','keine','10+: Menschenkenntnis 4','D',NULL,NULL,NULL,'WdS 22')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000369','Tätowieren',6,'IN','FF','FF','Spezial',NULL,'Naturalistische Darstellungen, Ornamente, Zaubertätowierungen','Malen/Zeichnen 4','B',NULL,NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000370','Tierkunde',5,'MU','KL','IN','Spezial',NULL,'Drachen, Jagdwild, Meeresgetier, Monstren, Nutztiere, Raubtiere, Schädlinge, Vielbeiner, Vögel oder nach Region',NULL,'B',NULL,NULL,NULL,'WdS 30')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000371','Töpfern',6,'KL','FF','FF','Spezial',NULL,'Gefäße, Statuetten, Ziegelei',NULL,'B',NULL,NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000372','Überreden',3,'MU','IN','CH','Basis',NULL,'Aufwiegeln, Betteln, Einschüchtern, Feilschen, Lügen','10+: Menschenkenntnis 4','B',NULL,NULL,NULL,'WdS 24')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000373','Überzeugen',3,'KL','IN','CH','Spezial','BE-4','Öffentliches Rede (Demagogie, Plädoyer, Predigt), Einzelgespräche (Verhör, Bekehrung), Diskussions-Rhetorik, schriftliche Rhetorik','Menschenkenntnis 4','B',NULL,NULL,NULL,'WdS 25')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000374','Viehzucht',6,'KL','IN','KK','Spezial',NULL,'nach Tierart: Rind, Schaf/Ziege, Schwein, Pferd, Hund, etc.',NULL,'B',NULL,NULL,NULL,'WdS 40')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000375','Webkunst',6,'FF','FF','KK','Spezial',NULL,'nach Material oder nach Technik (Häkeln, Klöppeln, Stricken, Teppichknüpfen, Weben)',NULL,'B',NULL,NULL,NULL,'WdS 41')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000376','Wettervorhersage',4,'KL','IN','IN','Spezial',NULL,'nach Geländetyp/Region',NULL,'B',NULL,NULL,NULL,'WdS 26')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000377','Wildnisleben',4,'IN','GE','KO','Basis',NULL,'Dschungel, Eis/Schnee, Gebirge, Meer, Steppe, Sumpf, Wald, Wüste',NULL,'B',NULL,NULL,NULL,'WdS 26')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000378','Winzer',6,'KL','FF','KK','Spezial',NULL,'Wein, Fruchtweine, Schaumweine, Weinkenner',NULL,'B','Winzer (Talent)',NULL,NULL,'WdS 41')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000379','Wurfbeile',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'D',NULL,'Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000380','Wurfmesser',1,NULL,NULL,NULL,'Basis','BE-3',NULL,NULL,'C','Wurfmesser (Talent)','Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000381','Wurfspeere',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'C',NULL,'Fernkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000382','Zechen',2,'IN','KO','KK','Basis',NULL,NULL,NULL,'D',NULL,NULL,NULL,'WdS 22')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000383','Zimmermann',6,'KL','FF','KK','Spezial',NULL,'Dachdecker, Holzkonstruktionen, Schiffszimmermann, Schiffbau (erfordert die Talente Lesen/Schreiben, Malen/Zeichnen, Rechnen und Seefahrt jeweils auf TaW 4)','10+: Holzbearbeitung 4','B','Zimmermann (Talent)',NULL,NULL,'WdS 41')
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000384','Zweihand-Hiebwaffen',1,NULL,NULL,NULL,'Spezial','BE-3',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000385','Zweihandflegel',1,NULL,NULL,NULL,'Spezial','BE-3',NULL,NULL,'D',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000386','Zweihandschwerter/-säbel',1,NULL,NULL,NULL,'Spezial','BE-2',NULL,NULL,'E',NULL,'Bewaffneter Nahkampf',NULL,NULL)
GO
INSERT INTO [Talent2] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Spezialisierungen],[Voraussetzungen],[Steigerung],[WikiLink],[Untergruppe],[Setting],[Literatur]) VALUES ('00000000-0000-0000-007a-000000000387','Zwergennase',9,'IN','IN','FF','Spezial',NULL,NULL,NULL,'F',NULL,NULL,NULL,'WdH 259')
GO

CREATE TABLE [Held_Talent2] (
	[TalentGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[TaW] int DEFAULT 0, 
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Bemerkung] nvarchar(300), 
	[ZuteilungAT] int, 
	[ZuteilungPA] int
)
GO

INSERT INTO [Held_Talent2] ([HeldGUID],[TalentGUID],[TaW],[Bemerkung],[ZuteilungAT],[ZuteilungPA])
SELECT [HeldGUID],A.[TalentGUID],[TaW],[Bemerkung],[ZuteilungAT],[ZuteilungPA] FROM [Held_Talent] as T, Talent2 as A WHERE T.[Talentname]=A.[Talentname]
GO
DROP TABLE [Held_Talent]
GO
sp_rename 'Held_Talent2', 'Held_Talent'
GO

CREATE TABLE [Fernkampfwaffe_Talent2] (
	[FernkampfwaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[TalentGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	CONSTRAINT [PK_Fernkampfwaffe_Talent] PRIMARY KEY ([FernkampfwaffeGUID], [TalentGUID]), 
	CONSTRAINT fk_FernkampfwaffeTalent_Waffe FOREIGN KEY ([FernkampfwaffeGUID])
		REFERENCES Fernkampfwaffe ([FernkampfwaffeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
INSERT INTO [Fernkampfwaffe_Talent2] ([FernkampfwaffeGUID], [TalentGUID])
SELECT [FernkampfwaffeGUID], A.[TalentGUID] FROM [Fernkampfwaffe_Talent] as T, Talent2 as A WHERE T.[Talentname]=A.[Talentname]
GO
DROP TABLE [Fernkampfwaffe_Talent]
GO
sp_rename 'Fernkampfwaffe_Talent2', 'Fernkampfwaffe_Talent'
GO

CREATE TABLE [Held_Ausrüstung2] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Angelegt] bit NOT NULL DEFAULT 0, 
	[AusrüstungGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[TalentGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Anzahl] int, 
	[BF] int NOT NULL DEFAULT 0, 
	[TrageortGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-001A-000000000011',
	CONSTRAINT [PK_Held_Ausrüstung] PRIMARY KEY ([HeldGUID], [AusrüstungGUID], [TrageortGUID]), 
	CONSTRAINT fk_HeldAusrüstung_Ausrüstung FOREIGN KEY ([AusrüstungGUID])
		REFERENCES Ausrüstung ([AusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_HeldAusrüstung_Held FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_TrageortGUID FOREIGN KEY ([TrageortGUID])
		REFERENCES Trageort ([TrageortGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

INSERT INTO [Held_Ausrüstung2] ([HeldGUID],[Angelegt],[AusrüstungGUID],[TalentGUID],[Anzahl],[BF],[TrageortGUID])
SELECT [HeldGUID],[Angelegt],[AusrüstungGUID],CASE WHEN A.[TalentGUID] is NULL THEN '00000000-0000-0000-0000-000000000000' ELSE A.[TalentGUID] END,[Anzahl],[BF],[TrageortGUID] FROM [Held_Ausrüstung] as T left join Talent2 as A ON T.[Talentname]=A.[Talentname]
GO
DROP TABLE [Held_Ausrüstung]
GO
sp_rename 'Held_Ausrüstung2', 'Held_Ausrüstung'
GO


CREATE TABLE [Waffe_Talent2] (
	[WaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[TalentGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	CONSTRAINT [PK_Waffe_Talent] PRIMARY KEY ([WaffeGUID], [TalentGUID]), 
	CONSTRAINT fk_WaffeTalent_Waffe FOREIGN KEY ([WaffeGUID])
		REFERENCES Waffe ([WaffeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
INSERT INTO [Waffe_Talent2] ([WaffeGUID], [TalentGUID])
SELECT [WaffeGUID], A.[TalentGUID] FROM [Waffe_Talent] as T, Talent2 as A WHERE T.[Talentname]=A.[Talentname]
GO
DROP TABLE [Waffe_Talent]
GO
sp_rename 'Waffe_Talent2', 'Waffe_Talent'
GO

DROP TABLE [Talent]
GO
sp_rename 'Talent2', 'Talent'
GO
ALTER TABLE [Talent] ADD CONSTRAINT [PK_Talent] PRIMARY KEY ([TalentGUID])
GO
ALTER TABLE [Talent] ADD CONSTRAINT Gruppe FOREIGN KEY ([TalentgruppeID])
		REFERENCES Talentgruppe ([TalentgruppeID])
		ON UPDATE CASCADE ON DELETE SET NULL
GO
CREATE UNIQUE INDEX [UQ_Talent] ON [Talent] (
	[Talentname]
)
GO

ALTER TABLE [Held_Talent] ADD CONSTRAINT [PK_Held_Talent] PRIMARY KEY ([TalentGUID], [HeldGUID])
GO
ALTER TABLE [Held_Talent] ADD CONSTRAINT Held_FK FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO

ALTER TABLE [Held_Talent] ADD CONSTRAINT Talent_FK FOREIGN KEY ([TalentGUID])
		REFERENCES Talent ([TalentGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [Fernkampfwaffe_Talent] ADD CONSTRAINT fk_FernkampfwaffeTalent_Talent FOREIGN KEY ([TalentGUID])
		REFERENCES Talent ([TalentGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [Held_Ausrüstung] ADD CONSTRAINT fk_HeldAusrüstung_Talent FOREIGN KEY ([TalentGUID])
		REFERENCES Talent ([TalentGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [Waffe_Talent] ADD	CONSTRAINT fk_WaffeTalent_Talent FOREIGN KEY ([TalentGUID])
		REFERENCES Talent ([TalentGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO


ALTER TABLE [VorNachteil] ADD [VorNachteilGUID] uniqueidentifier NOT NULL  ROWGUIDCOL default newid()
GO
UPDATE [VorNachteil] SET [VorNachteilGUID]=CAST('00000000-0000-0000-F024-' + Replicate('0', 12-LEN(CAST([VorNachteilID] as nvarchar))) + CAST([VorNachteilID] as nvarchar) as uniqueidentifier)
GO

CREATE TABLE [Held_VorNachteil2] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[VorNachteilGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Wert] nvarchar(255)
)
GO

INSERT INTO [Held_VorNachteil2] ([HeldGUID],[VorNachteilGUID],[Wert])
SELECT [HeldGUID],[VorNachteilGUID],[Wert] FROM [Held_VorNachteil] as T, VorNachteil as A WHERE T.VorNachteilID=A.VorNachteilID
GO
DROP TABLE [Held_VorNachteil]
GO
sp_rename 'Held_VorNachteil2', 'Held_VorNachteil'
GO

ALTER TABLE [Held_VorNachteil] ADD CONSTRAINT [PK_Held_VorNachteil] PRIMARY KEY ([HeldGUID], [VorNachteilGUID])
GO
ALTER TABLE [Held_VorNachteil] ADD CONSTRAINT Held_VorNachteil_HeldFK FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO

ALTER TABLE [VorNachteil] DROP CONSTRAINT [PK_VorNachteil]
GO
ALTER TABLE [VorNachteil] DROP CONSTRAINT [UQ__VorNachteil__000000000000014B]
GO
ALTER TABLE [VorNachteil] DROP COLUMN [VorNachteilID]
GO
ALTER TABLE [VorNachteil] ADD CONSTRAINT [PK_VorNachteil] PRIMARY KEY ([VorNachteilGUID]);
GO
--Datenbereinigung doppelte Myranordaten zusammenführen
UPDATE [VorNachteil] SET Setting='Aventurien' WHERE Setting is NULL
GO
UPDATE [VorNachteil] SET Setting='Aventurien, Myranor' WHERE [VorNachteilGUID] in ('00000000-0000-0000-f024-000000000131','00000000-0000-0000-f024-000000000136','00000000-0000-0000-f024-000000000170','00000000-0000-0000-f024-000000000172','00000000-0000-0000-f024-000000000173','00000000-0000-0000-f024-000000000175','00000000-0000-0000-f024-000000000330')
GO
DELETE [VorNachteil] WHERE [VorNachteilGUID] in ('00000000-0000-0000-f024-000000000388','00000000-0000-0000-f024-000000000390','00000000-0000-0000-f024-000000000391','00000000-0000-0000-f024-000000000392','00000000-0000-0000-f024-000000000398','00000000-0000-0000-f024-000000000464','00000000-0000-0000-f024-000000000472')
GO
ALTER TABLE [VorNachteil] ADD CONSTRAINT [UQ_VorNachteil] UNIQUE ([Name]);
GO

ALTER TABLE [Held_VorNachteil] ADD CONSTRAINT Held_VorNachteil_VorNachteilFK FOREIGN KEY ([VorNachteilGUID])
		REFERENCES VorNachteil ([VorNachteilGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO