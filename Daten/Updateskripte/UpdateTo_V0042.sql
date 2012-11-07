-- NscMerkmal neue Daten. Rasse_Kultur neu, Kultur_Name neu, Korrektur der Rüstungswerte gRS und gBE.
ALTER TABLE [Rüstung] ALTER COLUMN [gRS] float NULL  
GO
ALTER TABLE [Rüstung] ALTER COLUMN [gBE] float NULL  
GO
UPDATE [Rüstung] SET [gRS] = (2 * Kopf + 4 * Brust + 4 * Rücken + 4 * Bauch + LArm + RArm + 2 * LBein + 2 * RBein)/20.0,
[gBE] = CASE WHEN Art='Z' THEN (2 * Kopf + 4 * Brust + 4 * Rücken + 4 * Bauch + LArm + RArm + 2 * LBein + 2 * RBein)/20.0 /(1+Verarbeitung) ELSE (2 * Kopf + 4 * Brust + 4 * Rücken + 4 * Bauch + LArm + RArm + 2 * LBein + 2 * RBein)/20.0-Verarbeitung END
WHERE [RüstungGUID] <= '00000000-0000-0000-0000-000000000075' and [RüstungGUID] <> '00000000-0000-0000-0000-000000000002'
GO
UPDATE [Rüstung] SET [gRS] = 1, [gBE] = 4 WHERE [RüstungGUID] = '00000000-0000-0000-0000-000000000002'
GO


DROP TABLE [Rasse]
GO
DROP TABLE [Kultur]
GO

CREATE TABLE [Kultur] (
  [KulturGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(200) NOT NULL
, [Variante] nvarchar(200) NOT NULL
, [GP] int NOT NULL DEFAULT 0
, [SOmin] int NULL
, [SOmax] int NULL
, [Voraussetzungen] nvarchar(300) NULL
, [MUMod] int NOT NULL DEFAULT 0
, [KLMod] int NOT NULL DEFAULT 0
, [INMod] int NOT NULL DEFAULT 0
, [CHMod] int NOT NULL DEFAULT 0
, [FFMod] int NOT NULL DEFAULT 0
, [GEMod] int NOT NULL DEFAULT 0
, [KOMod] int NOT NULL DEFAULT 0
, [KKMod] int NOT NULL DEFAULT 0
, [LEMod] int NOT NULL DEFAULT 0
, [AUMod] int NOT NULL DEFAULT 0
, [MRMod] int NOT NULL DEFAULT 0
, [Literatur] nvarchar(300) NULL
,	[Setting] nvarchar(100) NULL
,	CONSTRAINT [PK_Kultur] PRIMARY KEY ([KulturGUID])
)
GO
CREATE UNIQUE INDEX [Kultur_Unique] ON [Kultur] (
	[Name], 
	[Variante]
)
GO
CREATE TABLE [Rasse] (
  [RasseGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(50) NOT NULL
, [Variante] nvarchar(50) NOT NULL
, [Unspielbar] bit NOT NULL DEFAULT 0
, [GP] int NOT NULL DEFAULT 0
, [Größe] int NOT NULL DEFAULT 0
, [GrößeMod] nvarchar(10) NULL DEFAULT '2W20'
, [Gewicht] int NOT NULL DEFAULT 0
, [MUMod] int NOT NULL DEFAULT 0
, [KLMod] int NOT NULL DEFAULT 0
, [INMod] int NOT NULL DEFAULT 0
, [CHMod] int NOT NULL DEFAULT 0
, [FFMod] int NOT NULL DEFAULT 0
, [GEMod] int NOT NULL DEFAULT 0
, [KOMod] int NOT NULL DEFAULT 0
, [KKMod] int NOT NULL DEFAULT 0
, [LEMod] int NOT NULL DEFAULT 0
, [AUMod] int NOT NULL DEFAULT 0
, [AEMod] int NOT NULL DEFAULT 0
, [MRMod] int NOT NULL DEFAULT 0
, [INIMod] int NOT NULL DEFAULT 0
, [Literatur] nvarchar(300) NULL
, [Setting] nvarchar(100) NULL
,	CONSTRAINT [PK_Rasse] PRIMARY KEY ([RasseGUID])
)
GO
CREATE UNIQUE INDEX [Rasse_Unique] ON [Rasse] (
	[Name], 
	[Variante]
)
GO

CREATE TABLE [Kultur_Name] (
  [KulturGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Herkunft] nvarchar(300) NOT NULL
)
GO
ALTER TABLE [Kultur_Name] ADD CONSTRAINT [PK_Kultur_Name] PRIMARY KEY ([KulturGUID],[Herkunft])
GO
ALTER TABLE [Kultur_Name] ADD CONSTRAINT [fk_KulturName_Kultur] FOREIGN KEY ([KulturGUID]) REFERENCES [Kultur]([KulturGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

CREATE TABLE [Rasse_Kultur] (
  [RasseGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [KulturGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Unüblich] bit NOT NULL DEFAULT 0
)
GO
ALTER TABLE [Rasse_Kultur] ADD CONSTRAINT [PK_Rasse_Kultur] PRIMARY KEY ([RasseGUID],[KulturGUID])
GO
ALTER TABLE [Rasse_Kultur] ADD CONSTRAINT [fk_RasseKultur_Kultur] FOREIGN KEY ([KulturGUID]) REFERENCES [Kultur]([KulturGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Rasse_Kultur] ADD CONSTRAINT [fk_RasseKultur_Rasse] FOREIGN KEY ([RasseGUID]) REFERENCES [Rasse]([RasseGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- Kultur, Rasse, Kultur_Name, Rasse_Kultur Daten
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000001','Mittelländische Städte','Mittelländische Städte',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000002','Mittelländische Städte','Hafenstädte und Städte an großen Flüssen',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000003','Mittelländische Städte','Städte mit wichtigen Tempeln/Pilgerstätte',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000004','Mittelländische Städte','Siedlerstädte des Nordens',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000005','Mittelländische Städte','Städtischer Adel',1,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000006','Mittelländische Städte','Kannemünde / Mhanerhaven',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000007','Mittelländische Städte','Flüchtlinge aus borbaradianisch besetzten Städten',0,NULL,NULL,NULL,-1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000008','Mittelländische Städte','Maraskanische Exilanten (in Festum)',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000009','Mittelländische Landbevölkerung','Mittelländische Landbevölkerung',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000010','Mittelländische Landbevölkerung','Küstengebiete oder an großen Flüssen',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000011','Mittelländische Landbevölkerung','An einer wichtigen Handelsroute/Reichsstraße',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000012','Mittelländische Landbevölkerung','Region Weiden und Greifenfurt',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000013','Mittelländische Landbevölkerung','Gebirge',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000014','Mittelländische Landbevölkerung','Fernab der Zivilisation',4,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000015','Mittelländische Landbevölkerung','Landadel',3,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000016','Mittelländische Landbevölkerung','Borbardianisch besetzte Gebiete',0,NULL,NULL,NULL,-1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000017','Andergast und Nostria','Andergast und Nostria',6,NULL,12,NULL,0,0,0,0,0,0,0,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000018','Andergast und Nostria','Stadt',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000019','Andergast und Nostria','Landadel',3,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000020','Andergast und Nostria','Gebirge',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000021','Andergast und Nostria','Küstengebiete  ',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000022','Andergast und Nostria','Teshkalien',4,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000023','Bornland','Bornland',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000024','Bornland','Landstädte',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000025','Bornland','Küstenstädte',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000026','Bornland','Landadel',5,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000027','Svellttal und Nordlande','Svellttal und Nordlande',7,NULL,10,NULL,0,0,0,0,0,0,0,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000028','Svellttal und Nordlande','Stadt',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000029','Svellttal und Nordlande','Kleinstädte',-1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000030','Svellttal und Nordlande','Küstengebiete oder an großen Flüssen',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000031','Svellttal und Nordlande','Fern der Zivilisation',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000032','Svellttal und Nordlande','Flüchtlinge aus Glorania',-1,NULL,NULL,NULL,-1,0,0,0,0,0,0,0,-1,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000033','Horasreich','Horasreich',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000034','Horasreich','Hafenstädte und Städte an großen Flüssen',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000035','Horasreich','Städte mit wichtigen Tempeln/Pilgerstätte',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000036','Horasreich','Städtischer Adel',1,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000037','Horasreich','Dörfer an Küstengebieten oder an großen Flüssen',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000038','Horasreich','An einer wichtigen Handelsroute/Reichsstraße',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000039','Horasreich','Fernab der Zivilisation',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000040','Horasreich','Landadel',2,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000041','Zyklopeninseln','Zyklopeninseln',4,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000042','Zyklopeninseln','Landadel',2,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000043','Maraskan','Maraskan',5,NULL,NULL,NULL,0,0,1,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000044','Maraskan','Dschungel',5,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000045','Maraskan','Küstengebiete',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000046','Maraskan','Maraskanische Städte',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000047','Südaventurien','Südaventurien',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000048','Südaventurien','südliche Stadtstaaten',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000049','Südaventurien','Kolonialhäfen',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000050','Südaventurien','Dschungeldorf/Plantage',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000051','Südaventurien','Selem',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000052','Südaventurien','Maraskanische Exilanten (in Al''Anfa)',4,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000053','Bukanier','Bukanier',2,NULL,6,NULL,0,0,0,0,0,0,0,0,1,2,-1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000054','Almada','Almada',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000055','Almada','Städte am Yaquier',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000056','Almada','Städte mit wichtigen Tempeln/Pilgerstätte',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000057','Almada','Städtischer Adel',2,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000058','Almada','An einer wichtigen Handelsroute/Reichsstraße',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000059','Almada','Gebirge',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000060','Almada','Landadel',2,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000061','Amazonenburg','Amazonenburg',7,NULL,NULL,NULL,0,0,0,0,0,0,1,0,1,3,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000062','Aranien','Aranien',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000063','Aranien','Stadt (Anchopal, Baburin, Zorgan, Elburum, Llanka)',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000064','Aranien','Küstengebiete oder Barun-Ulah',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000065','Aranien','Adel',2,7,NULL,'V Adlig (Hoher Amtsadel) | V Adlig (Amtsadel) | V Adlig (Adliges Erbe) | V Adlig (Adlige Abstammung)',0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000066','Mhanadistan','Mhanadistan',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000067','Mhanadistan','An wichtigen Handelsrouten',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000068','Mhanadistan','Fern der Zivilisation',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000069','Mhanadistan','Küstengebiete oder an großen Flüssen',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000070','Mhanadistan','Tulamidische Nomadenstämme',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000071','Mhanadistan','Kasimiten',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000072','Tulamidische Stadtstaaten','Tulamidische Stadtstaaten',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000073','Tulamidische Stadtstaaten','See- oder Mhanadihafen',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000074','Tulamidische Stadtstaaten','Maraskanische Exilanten (in Khunchom)',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000075','Tulamidische Stadtstaaten','Kasimiten',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000076','Novadi','Novadi (Männer oder Achmad''Sunni)',3,NULL,NULL,NULL,1,0,0,0,0,0,0,0,0,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000077','Novadi','Wüstenoase (Männer oder Achmad''Sunni)',-2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000078','Novadi','Novadi (Frauen)',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,1,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000079','Novadi','Wüstenoase (Frauen)',-1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000080','Ferkina','Ferkina',11,NULL,5,NULL,2,-1,0,0,0,0,1,0,1,5,-2,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000081','Zahori','Zahori',2,NULL,8,NULL,0,0,0,1,0,0,0,0,0,0,-1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000082','Thorwal','Thorwal',4,NULL,10,NULL,0,0,0,0,0,0,0,0,0,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000083','Thorwal','Binnenland',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000084','Gjalskerland','Gjalskerland',9,NULL,6,NULL,0,0,0,0,0,0,1,0,0,5,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000085','Fjarninger','Fjarninger',11,NULL,4,NULL,1,-1,0,0,0,0,1,1,2,6,-1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000086','Dschungelstämme','Dschungelstämme',11,NULL,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000087','Dschungelstämme','Tschopukikuha',-1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000088','Dschungelstämme','Bergstämme',-1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000089','Dschungelstämme','Haipu',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000090','Darna','Darna',9,NULL,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000091','Verlorene Stämme','Verlorene Stämme',12,NULL,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000092','Verlorene Stämme','Shokubunga',-1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000093','Verlorene Stämme','Chirakah',-2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000094','Walsinsel-Utulus','Waldinsel-Utulus',10,NULL,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000095','Miniwatu','Miniwatu',14,NULL,6,NULL,0,0,0,0,0,0,0,0,0,-2,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000096','Tocamuyac','Tocamuyac',9,NULL,4,NULL,0,0,0,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000097','Nivesenstämme','Nivesenstämme',10,NULL,8,NULL,0,0,0,0,0,0,0,0,0,5,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000098','Nivesenstämme','Halbsesshafte Küstenstämme',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000099','Nuanaä-Lie','Nuanaä-Lie',7,NULL,5,NULL,0,0,0,0,0,0,0,0,0,5,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000100','Nuanaä-Lie','Nauoke',10,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000101','Norbardensippe','Norbardensippe',8,NULL,10,NULL,0,0,0,0,0,0,0,0,0,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000102','Trollzacken','Trollzacken',10,NULL,4,NULL,0,0,0,0,0,0,1,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000103','Auelfische Sippe','Auelfische Sippe',4,NULL,8,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000104','Auelfische Sippe','Hoher Norden',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000105','Elfische Siedlung','Elfische Siedlung',3,NULL,10,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000106','Elfische Siedlung','Südliche Mittellande (Almada, Garetien)',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000107','Elfische Siedlung','Großstadt (Lowangen, Punin)',-1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000108','Elfische Siedlung','Firnelfisch beeinflusste Siedlung (Olport, Keamonmund)',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000109','Elfische Siedlung','Waldelfisch beeinflusste Siedlung (Donnerbach, Gerasim, Kvarsim)',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000110','Steppenelfische Sippe','Steppenelfische Sippe',4,NULL,7,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000111','Waldelfische Sippe','Waldelfische Sippe',6,NULL,8,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000112','Firnelfische Sippe','Firnelfische Sippe',6,NULL,7,NULL,0,0,0,0,0,0,0,0,0,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000113','Firnelfische Sippe','Küste und Inseln',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000114','Ambosszwerge','Ambosszwerge',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000115','Erzzwerge','Erzzwerge',1,NULL,NULL,NULL,0,1,-1,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000116','Hügelzwerge','Hügelzwerge',4,NULL,NULL,NULL,0,0,1,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000117','Brillantzwerge','Brillantzwerge',3,NULL,NULL,NULL,1,0,0,1,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000118','Brobim','Brobim',0,NULL,4,NULL,1,0,0,0,-1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000119','Korogai','Korogai',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,1,2,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000120','Korogai','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000121','Korogai','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000122','Korogai','Drasdech (Handwerker)',5,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000123','Korogai','Khurkach (Krieger und Jäger)',5,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000124','Korogai','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000125','Mokolash','Mokolash',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000126','Mokolash','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000127','Mokolash','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000128','Mokolash','Drasdech (Handwerker)',5,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000129','Mokolash','Khurkach (Krieger und Jäger)',5,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000130','Mokolash','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000131','Olochtai','Olochtai',4,NULL,NULL,NULL,0,0,0,0,0,0,0,0,1,4,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000132','Olochtai','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000133','Olochtai','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000134','Olochtai','Drasdech (Handwerker)',5,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000135','Olochtai','Khurkach (Krieger und Jäger)',5,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000136','Olochtai','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000137','Orichai','Orichai',2,NULL,NULL,NULL,0,0,0,0,0,0,0,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000138','Orichai','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000139','Orichai','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000140','Orichai','Drasdech (Handwerker)',5,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000141','Orichai','Khurkach (Krieger und Jäger)',5,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000142','Orichai','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000143','Truanzhai','Truanzhai',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000144','Truanzhai','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000145','Truanzhai','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000146','Truanzhai','Drasdech (Handwerker)',5,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000147','Truanzhai','Khurkach (Krieger und Jäger)',5,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000148','Truanzhai','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000149','Tscharshai','Tscharshai',5,NULL,NULL,NULL,0,1,0,0,0,0,0,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000150','Tscharshai','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000151','Tscharshai','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000152','Tscharshai','Drasdech (Handwerker)',5,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000153','Tscharshai','Khurkach (Krieger und Jäger)',5,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000154','Tscharshai','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000155','Zolochai','Zolochai',4,NULL,NULL,NULL,1,0,0,0,0,0,0,0,1,2,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000156','Zolochai','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000157','Zolochai','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000158','Zolochai','Drasdech (Handwerker)',5,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000159','Zolochai','Khurkach (Krieger und Jäger)',5,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000160','Zolochai','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000161','Yurach','Yurach',0,NULL,3,NULL,-1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000162','Svellttal-Besatzer','Svellttal-Besatzer',0,NULL,5,NULL,0,0,0,0,0,0,0,0,0,0,2,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000163','Svellttal-Besatzer','Ergoch (Sklaven)',1,0,2,NULL,-2,0,0,0,0,0,1,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000164','Svellttal-Besatzer','Grishik (Bauern)',3,1,4,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000165','Svellttal-Besatzer','Drasdech (Handwerker)',6,2,4,NULL,0,0,0,0,1,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000166','Svellttal-Besatzer','Khurkach (Krieger und Jäger)',6,3,5,NULL,1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000167','Svellttal-Besatzer','Okwach (Elite-Krieger, Priester, Schamanen)',8,4,5,NULL,1,1,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000168','Goblinstamm-Mann','Goblinstamm-Mann',4,NULL,3,NULL,-1,0,0,0,0,0,1,0,1,4,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000169','Goblinstamm-Mann','Ebene',4,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000170','Goblinstamm-Mann','Gebirge',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000171','Goblinstamm-Mann','Schneegoblins',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000172','Goblinstamm-Frau','Goblinstamm-Frau',5,NULL,3,NULL,-1,0,0,0,0,0,1,0,1,4,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000173','Goblinstamm-Frau','Ebene',4,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000174','Goblinstamm-Frau','Gebirge',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000175','Goblinstamm-Frau','Schneeboglins',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000176','Goblinbande','Goblinbande',1,NULL,3,NULL,-1,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000177','Festumer Ghetto','Festumer Ghetto',6,NULL,5,NULL,-1,1,0,0,0,0,0,0,0,-2,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000178','Archaische Achaz','Archaische Achaz',9,NULL,5,NULL,0,0,0,0,0,0,0,0,0,0,1,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000179','Stammes-Achaz','Stammes-Achaz',10,NULL,4,NULL,0,0,0,0,0,0,0,0,1,3,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000180','Stammes-Achaz','Maraskan',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000181','Stammes-Achaz','Orkland',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000182','Stammes-Achaz','Echsensümpfe',1,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000183','Stammes-Achaz','Loch Harodrol',3,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000184','Stammes-Achaz','Waldinseln/Südaventurien',0,NULL,NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO

INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000185','Troll','Troll',0,NULL,-1,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO
INSERT INTO [Kultur] ([KulturGUID],[Name],[Variante],[GP],[SOmin],[SOmax],[Voraussetzungen],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[MRMod],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000186','Grolm','Grolm',0,NULL,-1,NULL,0,0,0,0,0,0,0,0,0,0,0,NULL)
GO


INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000001',N'Mittelländer',N'Mittelländer',0,0,160,N'2W20',-100,0,0,0,0,0,0,0,0,10,10,0,-4,0,N'Wege der Helden 25',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000002',N'Tulamide',N'Tulamide',0,0,155,N'2W20',-105,0,0,0,0,0,0,0,0,10,10,0,-4,0,N'Wege der Helden 25',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000003',N'Thorwaler',N'Thorwaler',0,5,168,N'2W20',-95,1,0,0,0,0,0,1,1,11,10,0,-5,0,N'Wege der Helden 26',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000004',N'Thorwaler',N'Fjarninger',0,5,173,N'2W20',-95,1,0,0,0,0,0,1,1,11,10,0,-5,0,N'Wege der Helden 26',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000005',N'Thorwaler',N'Gjalskerländer',0,5,168,N'2W20',-95,1,0,0,0,0,0,1,1,11,10,0,-5,0,N'Wege der Helden 26',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000006',N'Nivese',N'Nivese',0,4,155,N'2W20',-110,0,0,1,0,0,0,1,0,9,12,0,-5,0,N'Wege der Helden 26',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000007',N'Norbarde',N'Norbarde',0,3,158,N'2W20',-100,0,0,0,1,0,0,0,0,11,10,0,-4,0,N'Wege der Helden 27',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000008',N'Trollzacker',N'Trollzacker',0,7,195,N'1W20',-100,2,-1,0,0,0,0,1,1,11,18,0,-5,0,N'Wege der Helden 27',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000009',N'Trollzacker',N'Rochshaz',0,-1,215,N'1W20',-95,2,-1,0,0,-1,-1,2,2,12,20,0,-5,0,N'Wege der Helden 27',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000010',N'Waldmensch',N'Waldmensch',0,5,152,N'3W6',-110,0,0,0,1,0,1,1,-1,8,12,0,-6,0,N'Wege der Helden 28',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000011',N'Waldmensch',N'Tocamuyac',0,-2,142,N'3W6',-110,0,0,0,1,0,1,1,-1,8,12,0,-6,0,N'Wege der Helden 28',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000012',N'Utulu',N'Utulu',0,9,165,N'2W20',-110,0,0,0,0,0,1,1,0,11,12,0,-6,0,N'Wege der Helden 29',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000014',N'Elf',N'Auelf',0,20,168,N'2W20',-120,0,-1,1,0,0,2,0,-1,6,12,12,-2,0,N'Wege der Helden 29',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000015',N'Elf',N'Waldelf',0,23,166,N'1W20+4W6',-120,0,-1,2,0,0,2,0,-1,6,10,12,-2,0,N'Wege der Helden 29',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000016',N'Elf',N'Firnelf',0,24,156,N'2W20',-120,0,-1,1,0,0,2,1,-1,7,15,12,-1,0,N'Wege der Helden 29',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000017',N'Halbelf',N'Halbelf',0,3,158,N'1W20+4W6',-120,0,0,0,0,0,1,0,-1,8,10,-6,-4,0,N'Wege der Helden 31',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000018',N'Halbelf',N'Halbelf firnelfischer Abstammung',0,2,158,N'1W20+4W6',-120,0,0,0,0,0,1,0,-1,8,12,-6,-3,0,N'Wege der Helden 31',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000019',N'Halbelf',N'Halbelf nivesischer Abstammung',0,5,158,N'1W20+4W6',-120,0,0,1,0,0,1,0,-1,8,11,-6,-4,0,N'Wege der Helden 31',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000020',N'Halbelf',N'Halbelf thorwalscher Abstammung',0,1,158,N'1W20+4W6',-120,0,0,0,0,0,0,0,0,9,10,-6,-4,0,N'Wege der Helden 31',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000021',N'Zwerg',N'Erzzwerg',0,12,128,N'2W6',-80,0,0,0,0,1,-1,2,1,11,15,0,-4,0,N'Wege der Helden 32',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000022',N'Zwerg',N'Hügelzwerg',0,12,128,N'2W6',-80,0,0,0,0,1,-1,2,1,11,15,0,-4,0,N'Wege der Helden 32',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000023',N'Zwerg',N'Brilliantzwerg',0,12,128,N'2W6',-80,0,0,0,0,1,-1,2,1,10,18,0,-4,0,N'Wege der Helden 32',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000024',N'Zwerg',N'Ambosszwerg',0,16,128,N'2W6',-80,0,0,0,0,1,-1,2,2,12,18,0,-4,0,N'Wege der Helden 32',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000025',N'Zwerg',N'Wilder Zwerg',0,18,128,N'2W6',-80,0,0,0,0,1,-1,2,2,12,18,0,-4,0,N'Wege der Helden 32',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000027',N'Ork',N'Korogai-Mann',0,12,150,N'3W6',-95,2,-2,0,-2,-1,0,2,2,12,18,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000028',N'Ork',N'Korogai-Frau',0,2,145,N'3W6',-90,0,-2,0,-2,-1,0,1,1,10,15,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000029',N'Ork',N'Mokolash-Mann',0,12,150,N'3W6',-95,2,-2,0,-2,-1,0,2,2,12,18,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000030',N'Ork',N'Mokolash-Frau',0,2,145,N'3W6',-90,0,-2,0,-2,-1,0,1,1,10,15,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000031',N'Ork',N'Olochtai-Mann',0,12,150,N'3W6',-95,2,-2,0,-2,-1,0,2,2,12,18,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000032',N'Ork',N'Olochtai-Frau',0,2,145,N'3W6',-90,0,-2,0,-2,-1,0,1,1,10,15,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000033',N'Ork',N'Orichtai-Mann',0,12,150,N'3W6',-95,2,-2,0,-2,-1,0,2,2,12,18,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000034',N'Ork',N'Orichtai-Frau',0,2,145,N'3W6',-90,0,-2,0,-2,-1,0,1,1,10,15,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000035',N'Ork',N'Truanzhai-Mann',0,12,150,N'3W6',-95,2,-2,0,-2,-1,0,2,2,12,18,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000036',N'Ork',N'Truanzhai-Frau',0,2,145,N'3W6',-90,0,-2,0,-2,-1,0,1,1,10,15,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000037',N'Ork',N'Tscharshai-Mann',0,12,150,N'3W6',-95,2,-2,0,-2,-1,0,2,2,12,18,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000038',N'Ork',N'Tscharschai-Frau',0,2,145,N'3W6',-90,0,-2,0,-2,-1,0,1,1,10,15,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000039',N'Ork',N'Zholochai-Mann',0,12,150,N'4W6',-95,2,-2,0,-2,-1,0,2,2,12,18,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000040',N'Ork',N'Zholochai-Frau',0,2,145,N'3W6',-90,0,-2,0,-2,-1,0,1,1,10,15,0,-7,0,N'Wege der Helden 34',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000041',N'Halbork',N'Halbork',0,1,160,N'4W6',-100,1,-1,0,-2,0,0,1,1,11,15,0,-6,0,N'Wege der Helden 35',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000043',N'Goblin',N'Goblin-Mann',0,3,135,N'4W6',-100,-1,-2,0,0,2,2,0,-1,4,12,0,-5,2,N'Wege der Helden 35',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000044',N'Goblin',N'Goblin-Frau',0,7,135,N'4W6',-100,-1,-2,1,0,2,2,0,-1,4,12,0,-5,2,N'Wege der Helden 35',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000046',N'Achaz',N'Echsensumpf-/Regenwald-Achaz',0,14,167,N'3W6',-120,-1,0,2,0,1,1,1,-1,8,7,0,-2,0,N'Wege der Helden 36',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000047',N'Achaz',N'Orkland-Achaz',0,9,159,N'3W6',-120,-1,0,2,0,1,1,0,-1,8,7,0,-2,0,N'Wege der Helden 36',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000048',N'Achaz',N'Waldinsel-Achaz',0,5,207,N'3W6',-130,-1,0,2,0,1,1,0,-1,7,7,0,-2,0,N'Wege der Helden 36',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000049',N'Achaz',N'Maraskan-Achaz',0,16,172,N'3W6',-130,-1,0,2,0,1,1,0,-1,7,7,0,-2,0,N'Wege der Helden 36',null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000050',N'Troll',N'Troll',1,0,0,N'2W20',0,0,0,0,0,0,0,0,0,0,0,0,0,0,null,null)
GO
INSERT INTO [Rasse] ([RasseGUID],[Name],[Variante],[Unspielbar],[GP],[Größe],[GrößeMod],[Gewicht],[MUMod],[KLMod],[INMod],[CHMod],[FFMod],[GEMod],[KOMod],[KKMod],[LEMod],[AUMod],[AEMod],[MRMod],[INIMod],[Literatur],[Setting]) VALUES ('00000000-0000-0000-0000-000000000051',N'Grolm',N'Grolm',1,0,0,N'2W20',0,0,0,0,0,0,0,0,0,0,0,0,0,0,null,null)
GO

INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000001','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000002','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000002','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000002','Weidener Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000003','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000003','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000004','Weidener Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000005','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000006','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000007','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000008','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000009','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000010','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000010','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000011','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000011','Weidener Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000011','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000012','Weidener Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000013','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000013','Weidener Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000013','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000014','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000014','Weidener Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000014','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000015','Albernische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000015','Weidener Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000015','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000016','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000017','Andergastsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000018','Andergastsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000019','Andergastsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000020','Andergastsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000021','Andergastsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000022','Andergastsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000017','Nostrische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000018','Nostrische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000019','Nostrische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000020','Nostrische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000021','Nostrische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000022','Nostrische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000023','Bornländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000024','Bornländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000025','Bornländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000026','Bornländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000027','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000028','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000029','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000030','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000031','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000032','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000027','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000028','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000029','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000030','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000031','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000032','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000027','Norbardische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000028','Norbardische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000029','Norbardische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000030','Norbardische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000031','Norbardische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000032','Norbardische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000033','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000034','Horasische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000035','Horasische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000036','Horasische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000037','Horasische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000038','Horasische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000039','Horasische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000040','Horasische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000041','Zyklopäische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000042','Zyklopäische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000043','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000044','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000045','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000046','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000047','Südländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000048','Südländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000049','Südländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000050','Südländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000051','Südländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000052','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000053','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000053','Südländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000053','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000053','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000053','Thorwalsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000054','Almadanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000055','Almadanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000056','Almadanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000057','Almadanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000058','Almadanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000059','Almadanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000060','Almadanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000061','Aranische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000061','Garethische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000061','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000061','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000062','Aranische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000063','Aranische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000064','Aranische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000065','Aranische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000066','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000067','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000068','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000069','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000070','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000071','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000072','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000073','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000074','Maraskanische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000075','Tulamidische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000076','Novadische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000077','Novadische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000078','Novadische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000079','Novadische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000080','Ferkina Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000081','Zahori Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000082','Thorwalsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000083','Thorwalsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000084','Gjalskerländische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000085','Fjarningsche Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000086','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000087','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000088','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000089','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000090','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000091','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000092','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000093','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000094','Utulu Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000095','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000096','Waldmenschen Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000097','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000098','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000099','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000100','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000101','Norbardische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000102','Nivesische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000103','Trollzacker Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000104','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000105','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000106','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000107','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000108','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000109','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000110','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000111','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000112','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000113','Elfische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000114','Zwergische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000115','Zwergische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000116','Zwergische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000117','Zwergische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000118','Zwergische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000119','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000120','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000121','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000122','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000123','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000124','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000125','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000126','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000127','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000128','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000129','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000130','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000131','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000132','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000133','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000134','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000135','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000136','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000137','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000138','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000139','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000140','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000141','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000142','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000143','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000144','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000145','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000146','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000147','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000148','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000149','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000150','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000151','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000152','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000153','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000154','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000155','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000156','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000157','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000158','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000159','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000160','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000161','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000162','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000163','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000164','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000165','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000166','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000167','Orkische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000168','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000169','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000170','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000171','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000172','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000173','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000174','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000175','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000176','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000177','Goblinische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000178','Achaz Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000179','Achaz Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000180','Achaz Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000181','Achaz Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000182','Achaz Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000183','Achaz Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000184','Achaz Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000185','Trollische Namen')
GO
INSERT INTO [Kultur_Name] ([KulturGUID],[Herkunft]) VALUES ('00000000-0000-0000-0000-000000000186','Grolmische Namen')
GO



-- Script Date: 29.02.2012 14:18  - Generated by ExportSqlCe version 3.5.2.5
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000062',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000061',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000066',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000001',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000009',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000017',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000023',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000027',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000033',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000041',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000043',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000047',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000053',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000054',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000043',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000047',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000053',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000061',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000062',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000066',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000072',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000076',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000080',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-000000000081',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000082',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000084',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000085',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000023',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000053',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000003','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000082',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000084',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000085',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000023',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000053',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000004','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000082',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000084',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000085',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000023',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000053',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000005','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000097',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000099',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000023',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000084',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000085',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000006','00000000-0000-0000-0000-000000000101',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000101',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000023',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000084',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000007','00000000-0000-0000-0000-000000000097',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000008','00000000-0000-0000-0000-000000000080',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000008','00000000-0000-0000-0000-000000000102',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000008','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000008','00000000-0000-0000-0000-000000000062',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000009','00000000-0000-0000-0000-000000000102',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000096',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000062',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000047',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000053',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000086',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000090',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000091',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000010','00000000-0000-0000-0000-000000000095',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000086',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000096',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000053',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000062',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000094',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000011','00000000-0000-0000-0000-000000000095',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000086',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000088',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000062',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000047',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000053',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000087',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000092',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000094',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000089',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000091',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000093',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000012','00000000-0000-0000-0000-000000000096',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000111',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000023',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000103',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000105',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000110',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000014','00000000-0000-0000-0000-000000000101',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000103',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000111',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000101',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000105',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000015','00000000-0000-0000-0000-000000000110',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000103',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000112',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000084',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000085',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000097',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000101',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000105',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000016','00000000-0000-0000-0000-000000000110',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000114',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000115',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000021','00000000-0000-0000-0000-000000000117',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000114',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000116',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000022','00000000-0000-0000-0000-000000000117',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000114',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000115',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000116',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000023','00000000-0000-0000-0000-000000000117',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000115',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000033',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000054',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000072',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000116',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000117',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000024','00000000-0000-0000-0000-000000000114',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000025','00000000-0000-0000-0000-000000000118',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000027','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000028','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000029','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000030','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000031','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000032','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000033','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000034','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000035','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000036','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000037','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000038','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000039','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000082',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000119',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000125',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000131',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000137',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000143',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000149',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000155',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000040','00000000-0000-0000-0000-000000000162',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000041','00000000-0000-0000-0000-000000000162',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000041','00000000-0000-0000-0000-000000000001',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000041','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000041','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000041','00000000-0000-0000-0000-000000000084',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000041','00000000-0000-0000-0000-000000000161',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000119',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000125',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000131',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000137',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000143',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000149',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000155',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000161',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000001',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000168',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000176',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000043','00000000-0000-0000-0000-000000000177',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000119',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000027',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000009',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000017',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000125',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000131',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000137',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000143',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000149',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000155',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000161',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000001',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000172',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000176',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000044','00000000-0000-0000-0000-000000000177',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000046','00000000-0000-0000-0000-000000000043',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000046','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000046','00000000-0000-0000-0000-000000000178',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000046','00000000-0000-0000-0000-000000000179',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000047','00000000-0000-0000-0000-000000000179',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000048','00000000-0000-0000-0000-000000000043',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000048','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000048','00000000-0000-0000-0000-000000000178',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000048','00000000-0000-0000-0000-000000000179',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000049','00000000-0000-0000-0000-000000000043',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000049','00000000-0000-0000-0000-000000000047',1)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000049','00000000-0000-0000-0000-000000000178',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000049','00000000-0000-0000-0000-000000000179',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000050','00000000-0000-0000-0000-000000000185',0)
GO
INSERT INTO [Rasse_Kultur] ([RasseGUID],[KulturGUID],[Unüblich]) VALUES ('00000000-0000-0000-0000-000000000051','00000000-0000-0000-0000-000000000186',0)
GO