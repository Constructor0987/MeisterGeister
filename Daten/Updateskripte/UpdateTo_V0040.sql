-- Guids für (fast) alle!
DROP TABLE [Fernkampfwaffe_Talent]
GO
DROP TABLE [Held_Fernkampfwaffe]
GO
DROP TABLE [Held_Inventar]
GO
DROP TABLE [Held_Rüstung]
GO
DROP TABLE [Held_Schild]
GO
DROP TABLE [Held_Waffe]
GO
DROP TABLE [Waffe_Talent]
GO
DROP TABLE [Inventar]
GO
DROP TABLE [Fernkampfwaffe]
GO
DROP TABLE [Handelsgut]
GO
DROP TABLE [Kultur]
GO
DROP TABLE [Rasse]
GO
DROP TABLE [Rüstung]
GO
DROP TABLE [Schild]
GO
DROP TABLE [Waffe]
GO

CREATE TABLE [Fernkampfwaffe] (
  [FernkampfwaffeGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(200) NOT NULL
, [Preis] float NOT NULL DEFAULT 0
, [Munitionspreis] float NULL
, [Munitionsgewicht] float NULL
, [Munitionsart] nvarchar(13) NULL
, [Gewicht] int NOT NULL DEFAULT 0
, [Improvisiert] bit NOT NULL DEFAULT 0
, [TPWürfel] int NULL
, [TPWürfelAnzahl] int NULL
, [TPBonus] int NULL
, [AusdauerSchaden] bit NOT NULL DEFAULT 0
, [TPKKSchwelle] int NULL
, [TPKKSchritt] int NULL
, [Verwundend] bit NOT NULL DEFAULT 0
, [RWSehrNah] int NULL
, [RWNah] int NULL
, [RWMittel] int NULL
, [RWWeit] int NULL
, [RWSehrWeit] int NULL
, [TPSehrNah] int NULL
, [TPNah] int NULL
, [TPMittel] int NULL
, [TPWeit] int NULL
, [TPSehrWeit] int NULL
, [Laden] int NULL
, [Verbreitung] nvarchar(300) NULL
, [Literatur] nvarchar(300) NULL
, [Setting] nvarchar(100) NULL
, [Bemerkung] ntext NULL
)
GO
CREATE TABLE [Handelsgut] (
  [HandelsgutGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(500) NULL
, [Preis] nvarchar(100) NULL DEFAULT 0
, [Gewicht] float NULL
, [ME] nvarchar(100) NULL
, [Kategorie] nvarchar(500) NULL
, [Tags] nvarchar(1000) NULL
, [Bemerkung] ntext NULL
, [Literatur] nvarchar(500) NULL
)
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


CREATE TABLE [Rüstung] (
  [RüstungGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(200) NOT NULL
, [Preis] float NOT NULL DEFAULT 0
, [Gewicht] int NOT NULL DEFAULT 0
, [Gruppe] nvarchar(100) NULL
, [Verarbeitung] int NULL DEFAULT 0
, [Art] nvarchar(1) NULL
, [Kopf] int NULL
, [Brust] int NULL
, [Rücken] int NULL
, [Bauch] int NULL
, [LArm] int NULL
, [RArm] int NULL
, [LBein] int NULL
, [RBein] int NULL
, [gRS] int NULL
, [gBE] int NULL
, [RS] int NULL
, [BE] int NULL
, [Verbreitung] nvarchar(300) NULL
, [Literatur] nvarchar(300) NULL
, [Setting] nvarchar(100) NULL
, [Bemerkung] ntext NULL
)
GO
CREATE TABLE [Schild] (
  [SchildGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(200) NOT NULL
, [Preis] float NOT NULL DEFAULT 0
, [Gewicht] int NOT NULL DEFAULT 0
, [Größe] nvarchar(10) NULL
, [Typ] nvarchar(2) NOT NULL DEFAULT 'S'
, [WMAT] int NOT NULL DEFAULT 0
, [WMPA] int NOT NULL DEFAULT 0
, [INI] int NOT NULL DEFAULT 0
, [BF] int NOT NULL DEFAULT 0
, [Verbreitung] nvarchar(300) NULL
, [Literatur] nvarchar(300) NULL
, [Setting] nvarchar(100) NULL
, [Bemerkung] ntext NULL
)
GO
CREATE TABLE [Waffe] (
  [WaffeGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(200) NULL
, [TPWürfel] int NOT NULL DEFAULT 6
, [TPWürfelAnzahl] int NOT NULL DEFAULT 0
, [TPBonus] int NOT NULL DEFAULT 0
, [AusdauerSchaden] bit NOT NULL DEFAULT 0
, [TPKKSchwelle] int NULL
, [TPKKSchritt] int NULL
, [INI] int NULL
, [WMAT] int NULL
, [WMPA] int NULL
, [BF] int NULL
, [Gewicht] int NOT NULL DEFAULT 0
, [Länge] int NULL
, [Preis] float NOT NULL DEFAULT 0
, [DK] nvarchar(5) NULL
, [Bemerkung] ntext NULL
, [Verbreitung] nvarchar(300) NULL
, [Literatur] nvarchar(300) NULL
)
GO

CREATE TABLE [Inventar] (
  [InventarGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Name] nvarchar(500) NULL
, [Preis] nvarchar(100) NULL DEFAULT 0
, [Gewicht] float NULL
, [ME] nvarchar(100) NULL
, [Kategorie] nvarchar(500) NULL
, [Tags] nvarchar(1000) NULL
, [Bemerkung] ntext NULL
, [Literatur] nvarchar(500) NULL
, [HandelsgutGUID] uniqueidentifier NULL
)
GO

CREATE TABLE [Waffe_Talent] (
  [WaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Talentname] nvarchar(100) NOT NULL
)
GO

CREATE TABLE [Fernkampfwaffe_Talent] (
  [FernkampfwaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Talentname] nvarchar(100) NOT NULL
)
GO
CREATE TABLE [Held_Fernkampfwaffe] (
  [HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Angelegt] bit NOT NULL DEFAULT 0
, [Ort] nvarchar(50) NOT NULL DEFAULT 'ArmR'
, [FernkampfwaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Talentname] nvarchar(100) NULL
, [Anzahl] int NULL
)
GO
CREATE TABLE [Held_Inventar] (
  [HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Angelegt] bit NOT NULL DEFAULT 0
, [Ort] nvarchar(50) NOT NULL DEFAULT 'Rucksack'
, [InventarGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Anzahl] int NULL
)
GO
CREATE TABLE [Held_Rüstung] (
  [HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Angelegt] bit NOT NULL DEFAULT 0
, [Ort] nvarchar(50) NOT NULL DEFAULT 'ArmR'
, [RüstungGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Anzahl] int NULL
, [Strukturpunkte] int NULL
)
GO
CREATE TABLE [Held_Schild] (
  [HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Angelegt] bit NOT NULL DEFAULT 0
, [Ort] nvarchar(50) NOT NULL DEFAULT 'ArmL'
, [SchildGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Anzahl] int NULL
, [BF] int NOT NULL DEFAULT 0
)
GO
CREATE TABLE [Held_Waffe] (
  [HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Angelegt] bit NOT NULL DEFAULT 0
, [Ort] nvarchar(50) NOT NULL DEFAULT 'ArmR'
, [WaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Talentname] nvarchar(100) NULL
, [Anzahl] int NULL
, [BF] int NOT NULL DEFAULT 0
)
GO


ALTER TABLE [Fernkampfwaffe] ADD CONSTRAINT [PK_Fernkampfwaffe] PRIMARY KEY ([FernkampfwaffeGUID])
GO
ALTER TABLE [Fernkampfwaffe_Talent] ADD CONSTRAINT [PK_Fernkampfwaffe_Talent] PRIMARY KEY ([FernkampfwaffeGUID],[Talentname])
GO
ALTER TABLE [Handelsgut] ADD CONSTRAINT [PK_Handelsgut] PRIMARY KEY ([HandelsgutGUID])
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [PK_Held_Fernkampfwaffe] PRIMARY KEY ([HeldGUID],[Ort],[FernkampfwaffeGUID])
GO
ALTER TABLE [Held_Inventar] ADD CONSTRAINT [PK_Held_Inventar] PRIMARY KEY ([HeldGUID],[Ort],[InventarGUID])
GO
ALTER TABLE [Held_Rüstung] ADD CONSTRAINT [PK_Held_Rüstung] PRIMARY KEY ([HeldGUID],[Ort],[RüstungGUID])
GO
ALTER TABLE [Held_Schild] ADD CONSTRAINT [PK_Held_Schild] PRIMARY KEY ([HeldGUID],[Ort],[SchildGUID])
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [PK_Held_Waffe] PRIMARY KEY ([HeldGUID],[Ort],[WaffeGUID])
GO
ALTER TABLE [Inventar] ADD CONSTRAINT [PK_Inventar] PRIMARY KEY ([InventarGUID])
GO
ALTER TABLE [Rüstung] ADD CONSTRAINT [PK_Rüstung] PRIMARY KEY ([RüstungGUID])
GO
ALTER TABLE [Schild] ADD CONSTRAINT [PK_Schild] PRIMARY KEY ([SchildGUID])
GO
ALTER TABLE [Waffe] ADD CONSTRAINT [PK__Waffe__0000000000000238] PRIMARY KEY ([WaffeGUID])
GO
ALTER TABLE [Waffe_Talent] ADD CONSTRAINT [PK_Waffe_Talent] PRIMARY KEY ([WaffeGUID],[Talentname])
GO
ALTER TABLE [Fernkampfwaffe_Talent] ADD CONSTRAINT [fk_FernkampfwaffeTalent_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Fernkampfwaffe_Talent] ADD CONSTRAINT [fk_FernkampfwaffeTalent_Waffe] FOREIGN KEY ([FernkampfwaffeGUID]) REFERENCES [Fernkampfwaffe]([FernkampfwaffeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [fk_HeldFernkampfwaffe_Fernkampfwaffe] FOREIGN KEY ([FernkampfwaffeGUID]) REFERENCES [Fernkampfwaffe]([FernkampfwaffeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [fk_HeldFernkampfwaffe_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [fk_HeldFernkampfwaffe_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Inventar] ADD CONSTRAINT [fk_HeldInventar_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Inventar] ADD CONSTRAINT [fk_HeldInventar_Inventar] FOREIGN KEY ([InventarGUID]) REFERENCES [Inventar]([InventarGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Rüstung] ADD CONSTRAINT [fk_HeldRüstung_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Rüstung] ADD CONSTRAINT [fk_HeldRüstung_Rüstung] FOREIGN KEY ([RüstungGUID]) REFERENCES [Rüstung]([RüstungGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Schild] ADD CONSTRAINT [fk_HeldSchild_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Schild] ADD CONSTRAINT [fk_HeldSchild_Schild] FOREIGN KEY ([SchildGUID]) REFERENCES [Schild]([SchildGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Waffe] FOREIGN KEY ([WaffeGUID]) REFERENCES [Waffe]([WaffeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Waffe_Talent] ADD CONSTRAINT [fk_WaffeTalent_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Waffe_Talent] ADD CONSTRAINT [fk_WaffeTalent_Waffe] FOREIGN KEY ([WaffeGUID]) REFERENCES [Waffe]([WaffeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO


CREATE TABLE [Held_Munition] (
  [HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [FernkampfwaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [MunitionGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Ort] nvarchar(50) NOT NULL DEFAULT 'ArmR'
, [Anzahl] int NULL
)
GO

CREATE TABLE [Munition]
(
	[MunitionGUID] uniqueidentifier NOT NULL DEFAULT newid()
, [Art] nvarchar(13) NOT NULL
, [Name] nvarchar(50) NOT NULL
, [Preismodifikator] int NOT NULL default 1
, [Bemerkungen] nvarchar(300) NULL
, [Literatur] nvarchar(300) NULL default 'Aventurisches Arsenal 46'
)
GO

ALTER TABLE [Munition] ADD CONSTRAINT [PK_Munition] PRIMARY KEY ([MunitionGUID])
GO
ALTER TABLE [Held_Munition] ADD CONSTRAINT [PK_Held_Munition] PRIMARY KEY ([HeldGUID],[Ort],[FernkampfwaffeGUID],[MunitionGUID])
GO
ALTER TABLE [Held_Munition] ADD CONSTRAINT [fk_HeldMunition_Fernkampfwaffe] FOREIGN KEY ([FernkampfwaffeGUID]) REFERENCES [Fernkampfwaffe]([FernkampfwaffeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Munition] ADD CONSTRAINT [fk_HeldMunition_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Held_Munition] ADD CONSTRAINT [fk_HeldMunition_Munition] FOREIGN KEY ([MunitionGUID]) REFERENCES [Munition]([MunitionGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO



INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('dd79ac50-1159-4724-90ac-64a8b92128e2','Schleuderblei','Stein',0,NULL,'Aventurisches Arsenal 21')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('3282aad5-9bb4-4b24-be0c-ee945a17df30','Schleuderblei','Schleuderblei',1,'Fernkampf um 1 erleichtert, TP + 1','Aventurisches Arsenal 21')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('f48e5df6-71ca-4e66-b8b2-096ffd45c09e','Pfeil','Jagdpfeil',1,'wiederverwendbar','Aventurisches Arsenal 46')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('39d9d665-1451-475a-a1e5-85393b5a1157','Pfeil','Kriegspfeil',1,'Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','Aventurisches Arsenal 46')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('489d33ec-b45e-4401-a053-0ee0863b07d9','Pfeil','Gehärteter Kriegspfeil',4,'RS/2, Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','Aventurisches Arsenal 47')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('f584ebae-65a0-4845-8067-8c8204b9b0ac','Pfeil','Kettenbrecher',3,'RS durch Kettengeflecht wird ignoriert, RS von Mischrüstungen (Spiegelpanzer, Baburiner Hut, ...) wird halbiert','Aventurisches Arsenal 47')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('13dbe54a-ac2f-47e1-b185-fa4054ce1d0d','Pfeil','Stumpfer Pfeil',2,'Halbe Reichweite, richtet TP(A) an, Scharfschütze kann einen gezielten Schuß +4 zum niederwerfen machen (KK + TP(A)/2 oder man liegt am Boden)','Aventurisches Arsenal 47')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('ee7472b3-ac10-4336-8a12-3861f5abde00','Pfeil','Sehnen-Seilschneider',3,'TP+1W6, RS*2, Halbe Reichweite, siehe Ogerfänger','Aventurisches Arsenal 47')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('bf528fe3-919f-47c0-80c5-c4105637e4bb','Pfeil','Brandpfeil',8,'Halbe Reichweite, wie stumpfer Pfeil','Aventurisches Arsenal 48')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('1bb8318c-6e92-4338-9638-9f3f760708ee','Pfeil','Singender Pfeil',2,'macht Geräusche im Flug, je nach Qualität bis zehnfacher Preis und TP Abzug bis zu 2','Aventurisches Arsenal 48')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-0000-123456789012','Bolzen','Jagdbolzen',1,'wiederverwendbar','Aventurisches Arsenal 46                                                                          ')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('41a55c73-8be5-4b2e-af11-0ee08263d438','Bolzen','Kriegsbolzen',1,'Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','Aventurisches Arsenal 46                  ')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('eb9c13d1-9fe9-448b-98fe-b36dd8b0172e','Bolzen','Gehärteter Kriegsbolzen',4,'RS/2, Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','Aventurisches Arsenal 47  ')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('44a1bb8a-8abf-4ef7-b697-910342fd8dfa','Bolzen','Kettenbrecher',3,'RS durch Kettengeflecht wird ignoriert, RS von Mischrüstungen (Spiegelpanzer, Baburiner Hut, ...) wird halbiert','Aventurisches Arsenal 47')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('4f208452-97d2-4505-b15f-c85fa03ca627','Bolzen','Stumpfer Bolzen',2,'Halbe Reichweite, richtet TP(A) an, Scharfschütze kann einen gezielten Schuß +4 zum niederwerfen machen (KK + TP(A)/2 oder man liegt am Boden)','Aventurisches Arsenal 47')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('9bcddf48-bd7e-4e66-894a-8825c127e91c','Bolzen','Sehnen-Seilschneider',3,'TP+1W6, RS*2, Halbe Reichweite, siehe Ogerfänger','Aventurisches Arsenal 47                                ')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('523faa41-e6a4-4f2d-82ab-598f37341a2f','Bolzen','Brandbolzen',8,'Halbe Reichweite, wie stumpfer Bolzen','Aventurisches Arsenal 48                                                      ')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('8051b558-206d-4935-8950-927fa6bed499','Bolzen','Singender Bolzen',2,'macht Geräusche im Flug, je nach Qualität bis zehnfacher Preis und TP Abzug bis zu 2','Aventurisches Arsenal 48')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('04f0dcb6-b3cf-4a6d-8668-73ea322c380e','Blasrohrpfeil','Blasrohrpfeil',1,'vergiftet','')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('23bd6f90-4c07-45fc-bccf-81e146105ace','Speer','Schleuderspeer',1,'Geschoß für eine Speerschleuder','Aventurisches Arsenal 22')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('3bdf1560-75e5-495c-a794-f76f033ce94e','Kugel','Bleikugel',1,NULL,'Aventurisches Arsenal 40')
GO

INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000001','Arbalette',600,2.5,8,'Kugel',200,0,6,2,5,0,NULL,NULL,1,10,20,30,60,100,2,1,0,-1,-2,30,'(4) ZWE, HOR, ALM','Aventurisches Arsenal 41','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000002','Arbalone',800,4,10,'Kugel',480,0,6,3,6,0,NULL,NULL,1,15,30,60,120,250,4,2,0,-1,-3,40,'(2) HOR','Aventurisches Arsenal 43','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000003','Balestra',500,2,5,'Kugel',150,0,6,2,2,0,NULL,NULL,1,10,20,30,50,75,2,1,0,0,-1,4,'(3) HOR, ZWE','Aventurisches Arsenal 41','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000004','Balestrina',450,1.5,5,'Kugel',60,0,6,1,4,0,NULL,NULL,0,2,4,8,15,25,2,1,0,0,-1,2,'(5) HOR, ZWE, MEN, ALM','Aventurisches Arsenal 41','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000005','Balläster',200,0.6,5,'Kugel',120,0,6,1,4,0,NULL,NULL,0,10,20,30,60,100,3,1,0,-1,-1,8,'(8) ZWE, BOR, GAR, ALM, HOR, KHU, ALA','Aventurisches Arsenal 18','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000006','Blasrohr',40,0,0,'Blasrohrpfeil',15,0,6,1,-1,0,NULL,NULL,0,2,5,10,20,40,0,0,0,0,-2,2,'(8) MEN, WAL, BRA, CHA','Aventurisches Arsenal 83','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000007','Borndorn',40,NULL,NULL,NULL,30,0,6,1,2,0,13,5,0,2,4,6,8,15,1,0,0,0,-1,NULL,'(6) BOR, ALB, GAR, XER, ALM, HOR, ARA, ORO, MEN, BRA, ALA, CHA','Aventurisches Arsenal 72','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000008','Diskus',25,NULL,NULL,NULL,30,0,6,1,3,0,13,3,0,5,10,20,30,60,1,0,0,0,-1,NULL,'(6) MAR, SKR, SHI, KHU, THA, sehr selten auch bei Exilanten in Festum oder auch Al''Anfa','Aventurisches Arsenal 81','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000009','Dolch',20,NULL,NULL,NULL,20,1,6,1,0,0,13,5,0,1,3,5,7,10,0,0,0,-1,-1,NULL,'(18) alle Regionen ausser FIR, EHE, WAL','Aventurisches Arsenal 12','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000010','Dschadra',120,NULL,NULL,NULL,80,0,6,1,4,0,12,3,0,5,10,15,25,40,3,2,1,0,0,NULL,'(10) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 79','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000011','Efferdbart',80,NULL,NULL,NULL,90,0,6,1,3,0,12,3,0,3,6,10,15,25,2,1,0,-1,-2,NULL,'(4) THO, ALB, HOR, GAR (Perricum), ARA, BOR','Aventurisches Arsenal 54, Errata 3','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000012','Eisenwalder',400,1.5,2,'Bolzen',100,0,6,1,3,0,NULL,NULL,1,5,10,15,20,40,1,0,0,0,-1,3,'(6) ZWE, ALM, HOR','Aventurisches Arsenal 87','Aventurien','Füllen des Magazins dauert 20 Aktionen, das Magazin fasst 10 Schuss')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000013','Elfenbogen',0,4,3,'Pfeil',25,0,6,1,5,0,NULL,NULL,1,10,25,50,100,200,3,2,1,1,0,3,'(8) ELF','Aventurisches Arsenal 90','Aventurien','unverkäuflich')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000014','Fledermaus',10,NULL,NULL,NULL,20,0,6,1,2,0,NULL,NULL,0,0,5,10,15,25,NULL,0,0,0,-1,2,'(8) bei den Brillantzwergen  (10) in KHO, ART, MHA','Aventurisches Arsenal 88','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000015','Gandrasch-Armbrust M1024 (doppelt gespannt)',600,2.5,4,'Bolzen',280,0,6,2,8,0,NULL,NULL,1,10,25,50,100,150,2,1,0,0,0,30,'(2) ZWE, GAR','Aventurisches Arsenal 42','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000016','Gandrasch-Armbrust M1024',600,2.5,4,'Bolzen',280,0,6,2,2,0,NULL,NULL,0,5,15,25,40,60,1,0,0,0,0,12,'(2) ZWE, GAR','Aventurisches Arsenal 42','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000017','Granatapfel',0,NULL,NULL,NULL,40,0,6,4,0,0,NULL,NULL,0,0,5,10,15,20,NULL,NULL,NULL,NULL,NULL,NULL,'(1) HOR, MEN','Aventurisches Arsenal 44','Aventurien','unverkäuflich')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000018','Holzspeer',10,NULL,NULL,NULL,60,0,6,1,2,0,12,3,0,5,10,15,25,40,1,0,0,-1,-2,NULL,'(16) alle Regionen ausser FIR, KHO','Aventurisches Arsenal 19','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000019','Jagddiskus',30,NULL,NULL,NULL,35,0,6,2,4,1,13,3,0,5,10,20,30,60,1,0,0,0,-1,NULL,'(6) MAR, SKR, SHI','Aventurisches Arsenal 81','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000020','Jagdspieß',80,NULL,NULL,NULL,75,0,6,1,4,0,12,3,0,5,10,15,20,30,0,0,-1,-2,-2,NULL,'(16) ELF','Aventurisches Arsenal, Errata 1, Errata 4','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000021','Kampfdiskus',60,NULL,NULL,NULL,50,0,6,1,5,0,13,3,0,10,20,30,45,60,1,0,0,0,0,NULL,'(4) MAR, SKR, SHI sehr selten auch bei Exilanten in Festum oder auch Al''Anfa','Aventurisches Arsenal 81, Errata 4','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an. Der Kampfdiskus setzt eine KK von 14 zum korrekten Einsatz vorraus, ansonsten gilt er als improvisierte Wurfwaffe')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000022','Kettenkugel',150,NULL,NULL,NULL,250,1,6,2,2,0,NULL,NULL,0,0,2,5,8,15,NULL,1,0,0,0,2,NULL,'Wege des Schwertes 130, Errata','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000023','Kompositbogen',80,0.25,2,'Pfeil',25,0,6,1,5,0,NULL,NULL,1,10,20,30,50,80,2,1,1,0,0,3,'(16) KHO, MHA, ARA, ORO, ART, KHU, GOR, THA','Aventurisches Arsenal 79','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000024','Krähenfüße, 10 Stück',20,NULL,NULL,NULL,10,0,4,1,1,0,NULL,NULL,0,0,0,0,0,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Katakomben und Kavernen 125','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000025','Kriegsbogen',100,0.6,4,'Pfeil',45,0,6,1,7,0,NULL,NULL,1,10,20,40,80,150,3,2,1,0,0,4,'(4) WEI, AND, GAR, TOB, GAL','Aventurisches Arsenal 44','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000026','Kurzbogen',45,0.25,2,'Pfeil',20,0,6,1,4,0,NULL,NULL,1,5,15,25,40,60,1,1,0,0,-1,2,'(16) alle Regionen ausser FIR, NIV, GLO, EHE','Aventurisches Arsenal 20','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000027','Langbogen',60,0.4,3,'Pfeil',30,0,6,1,6,0,NULL,NULL,1,10,25,50,100,200,3,2,1,0,-1,4,'(16) AND, ALB, WEI, TOB','Aventurisches Arsenal 45','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000028','Lasso',12,NULL,NULL,NULL,40,0,6,1,4,0,NULL,NULL,0,0,2,5,10,15,NULL,0,0,-1,-2,1,'(8) RIV, SVE, ORK, BOR, WEI, ARA, ORO, KHO','Aventurisches Arsenal 14','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000029','Leichte Armbrust',180,1.5,3,'Bolzen',150,0,6,1,6,0,NULL,NULL,1,10,15,25,40,60,1,1,0,0,-1,15,'(10) alle ausser FIR, EHE, ELF, NIV, ORK, ACH, WAL','Aventurisches Arsenal 45','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000030','Orkischer Reiterbogen',120,0.25,2,'Pfeil',40,0,6,1,5,0,NULL,NULL,1,5,15,30,60,100,3,1,0,-1,-2,3,'(8) Ork','Aventurisches Arsenal 93','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000031','Schleuder',15,0.5,3,'Schleuderblei',10,0,6,1,2,0,NULL,NULL,0,0,5,15,25,40,NULL,0,0,0,0,2,'(6) WEI, BOR, TOB (bei den jeweils ansässigen Goblins), ZYK','Aventurisches Arsenal 21, Errata 1','Aventurien','Der Munitionspreis ist für spezielle Bleikugeln, die 1TP mehr anrichten und die Fernkampf-Probe um 1 Punkt erleichtern.')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000032','Schneidezahn',60,NULL,NULL,NULL,50,0,6,1,4,0,13,3,0,0,5,10,15,30,NULL,1,1,0,-1,NULL,'(10) THO, SVE bei thowalschen Söldner-Ottaskin','Aventurisches Arsenal 76','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000033','Schweres Wurfnetz',60,NULL,NULL,NULL,200,0,6,1,6,0,NULL,NULL,0,0,0,0,5,5,NULL,NULL,NULL,NULL,NULL,1,'(6) GLO, NIV, SVE, THO, XER, ARA, MHA, GOR, ALA, BRA','Aventurisches Arsenal 22','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000034','Speer',30,NULL,NULL,NULL,80,0,6,1,3,0,12,3,0,5,10,15,25,40,1,0,0,-1,-2,NULL,'(18) alle Regionen','Aventurisches Arsenal 21','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000035','Speerschleuder',25,1.5,60,'Speer',30,0,6,1,3,0,12,3,1,5,15,25,35,50,2,1,0,0,-1,2,'(10) NIV, GLO, RIV, ORK, ARA, MHA, GOR, KHO, ART, ACH','Aventurisches Arsenal 22','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000036','Stabschleuder',15,0.5,3,'Schleuderblei',40,0,6,1,3,0,12,3,0,0,5,20,40,60,NULL,0,0,0,0,2,'(16) alle ausser FIR, NIV, GLO, EHE','Aventurisches Arsenal 45','Aventurien','Der Munitionspreis ist für spezielle Bleikugeln, die 1TP mehr anrichten.')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000037','Stein, Flasche',0,NULL,NULL,NULL,10,1,6,1,0,0,13,3,0,1,2,4,8,12,0,0,0,-1,-1,NULL,'alle Regionen ausser FIR, NIV, GLO, EHE','Aventurisches Arsenal 17','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000038','Windenarmbrust',350,2,4,'Bolzen',200,0,6,2,6,0,NULL,NULL,1,10,30,60,100,180,4,2,0,-1,-3,30,'(7) GAL, GAR, ALB, ZWE, ALM, HOR, ARA, KHU, MEN, ALA','Aventurisches Arsenal 46','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000039','Wurfbeil',35,NULL,NULL,NULL,60,0,6,1,3,0,13,3,0,0,5,10,15,25,NULL,1,1,0,-1,NULL,'(8) FIT, GLO, NIV, SVE, THO, ORK, AND, WIE, BOR, TOB','Aventurisches Arsenal 77','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000040','Wurfdolch',30,NULL,NULL,NULL,20,0,6,1,1,0,13,5,0,2,4,6,8,15,1,0,0,0,-1,NULL,'(7) ALB, GAR, GAL, XER, ALM, HOR, ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, BRA, ALA, CHA','Aventurisches Arsenal 71','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000041','Wurfkeule',18,NULL,NULL,NULL,35,0,6,2,4,1,13,3,0,0,5,15,25,40,NULL,1,1,1,0,NULL,'(12) NIV, deutlich seltener auch den Sippen RIV und BOR','Aventurisches Arsenal 77','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000042','Wurfmesser',15,NULL,NULL,NULL,10,0,6,1,0,0,13,5,0,2,4,6,8,15,1,0,0,0,-1,NULL,'(8) ALB, GAR, GAR, XER, ALM, HOR, ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, BRA, ALA, CHA','Aventurisches Arsenal 71','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000043','Wurfnetz',35,NULL,NULL,NULL,80,0,6,1,2,0,NULL,NULL,0,0,0,0,5,5,NULL,NULL,NULL,NULL,NULL,1,'(6) GLO, NIV, SVE, THO, XER, ARA, MHA, GOR, ALA, BRA','Aventurisches Arsenal 22','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000044','Wurfscheibe, -ring, -stern',35,NULL,NULL,NULL,10,0,6,1,1,0,13,5,0,2,4,8,12,20,1,0,0,0,0,NULL,'(6) ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, ALA','Aventurisches Arsenal 72','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000045','Wurfspeer',30,NULL,NULL,NULL,80,0,6,1,4,0,12,3,1,5,10,15,25,40,3,1,0,-1,-1,NULL,'(13) ALM, ARA, ORO, MHA, GOR, KHU, THA, HOR, ART, ALA','Aventurisches Arsenal 21, 23','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO


INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000001','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000002','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000003','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000004','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000005','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000006','Blasrohr')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000007','Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000008','Diskus')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000009','Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000010','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000011','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000012','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000013','Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000014','Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000015','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000016','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000017','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000017','Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000018','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000019','Diskus')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000020','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000021','Diskus')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000022','Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000023','Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000024','Fallenstellen')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000025','Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000026','Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000027','Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000028','Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000029','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000030','Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000031','Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000032','Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000033','Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000034','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000035','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000036','Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000037','Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000038','Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000039','Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000040','Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000041','Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000042','Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000043','Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000044','Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000045','Wurfspeere')
GO


INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000001','Achfawar',6,1,4,0,13,4,0,-1,0,4,100,140,300,'S','beachte Regelung im Arsenal','(2) ACH','Aventurisches Arsenal 93')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000002','Amazonensäbel',6,1,4,0,11,4,1,0,0,2,75,100,180,'N','Vom Pferderücken aus gegen den Fußlämpfer richtet ein Amazonensäbel zwei zusätzliche TP an.','(12) WEI, GAR, ALB, HOR, ALM, ARA, ORO','Aventurisches Arsenal 38')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000003','Andergaster',6,3,2,0,14,2,-3,0,-2,3,220,200,350,'S','Der Schlag eines Andergasters kann mit Fechtwaffen und Dolchen nocht pariert werden.','(6) RIV, WEI, GAR, AND, ALB, ALM, GAL, HOR','Aventurisches Arsenal 32')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000004','Anderthalbhänder',6,1,5,0,11,4,1,0,0,1,100,115,250,'NS','','(7) WEI, GAR, ALB, ALM, HOR','Aventurisches Arsenal 25')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000005','Arbach',6,1,4,0,12,3,0,0,-1,2,100,90,120,'N','Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe zwei zusätzliche TP an.','(10) ORK,  SVE (nur bei ORKS)','Aventurisches Arsenal 92')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000006','Baccanaq',6,1,4,0,12,4,-1,0,-2,5,80,80,180,'N','beachte Regelung im Arsenal','(4) ALA, BRA, CHA','Aventurisches Arsenal 83')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000007','Barbarenschwert',6,1,5,0,13,2,-1,0,-1,4,100,90,200,'N','Vom Pferderücken aus gegen Fußkämpfer, richtet das Barbarenschwert zwei zusätzliche TP an. Erfordert mindest KK von 15 für einhändige Führung mit dem Talent Schwerter','(6) THO (nur Gjalskerland), FIR, GAR (nur Trollzacken)','Aventurisches Arsenal 74')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000008','Barbarensteitaxt',6,3,2,0,15,1,-2,-1,-4,3,250,120,150,'N','Erfordert mindest KK von 15','(6) FIR, THO, GAR (nur Trollzacken)','Aventurisches Arsenal 75')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000009','Basiliskenzunge',6,1,2,0,12,4,-1,0,-1,4,25,30,70,'H','Mit einer Basiliskenzunge  können Schläge von Kettenwaffen (mit der Außnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht parriert werden.','(6) ALM, HOR, ZYK, KHU, THA, MEN, BR, ALA','Aventurisches Arsenal 67')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000010','Bastardschwert',6,1,5,0,12,4,-1,0,-1,2,120,110,200,'N','Erfordert mindest KK von 15 für einhändige Führung mit dem Talent Schwerter','(8) GLO, BOR, ORK, THO, AND, WEI, ALB','Aventurisches Arsenal 25')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000011','Beil',6,1,3,0,11,4,-1,-1,-2,5,70,50,20,'N','','(18) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 11')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000012','Beil (Stein)',6,1,3,0,11,4,-1,-1,-2,6,80,50,15,'N','','(18)ORK, EHE, ALA, BRA, WAL, CHA','Aventurisches Arsenal 11')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000013','Bock',6,1,2,0,10,5,-1,0,0,0,120,20,80,'H','','(4) THO, AND, ALB, GAR, GAL, XER, HOR, ALM, MEN, MHA, GOR, KHU, THA, ALA','Aventurisches Arsenal 95')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000014','Borndorn (im Nahkampf)',6,1,2,0,12,5,0,0,-1,1,30,40,40,'H','Es gelten die üblichen Parade-Einschränkungen für Dolche','(6) BOR, ALB, GAR, XER, ALM, HOR, ARA, ORO, MEN, BRA, ALA, CHA','Aventurisches Arsenal 72')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000015','Boronssichel',6,2,6,0,13,3,-2,0,-3,3,160,180,400,'S','Der Schlag einer Boronsichel kann mit Fechtwaffen und Dolchen nicht pariert werden.','(5) ALM, ARA, ORO, ART, MHA','Aventurisches Arsenal 33')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000016','Brabakbengel',6,1,5,0,13,3,0,0,-1,1,120,90,100,'N','Mit dem zentralen Dorn kann mit dem Talent Dolche auch gestochen werden: 1W6 TP, AT um 3 erschwert, eigene nächste PA ebenfalls um 3 erschwert.','(7) GLO, THO, ORK, AND, ALB, GAL RHA, XER, MEN, BRA, ALA, THA','Aventurisches Arsenal 26')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000017','Breitschwert',6,1,4,0,12,3,0,0,-1,1,80,85,120,'N','','(15) SVE, THO, ORK, ZYK, MEN, BRA, ALA','Aventurisches Arsenal 26')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000018','Byakka',6,1,5,0,14,2,-1,0,-2,3,130,100,90,'N','Ein Stich wird auf das Talent Dolche (AT erschwert um 3, eigene nächste PA ebenfalls erschwert um 3) abgelegt','(9) ORK, SVE, AND (nur bei Orks)','Aventurisches Arsenal 92')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000019','Degen',6,1,3,0,12,5,2,0,-1,3,40,90,150,'N','Mit dem Degen können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.','(6) ALB, GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 60')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000020','Dolch',6,1,1,0,12,5,0,0,-1,2,20,30,20,'H','','(18) alle Regionen außer FIR, EHE, WAL','Aventurisches Arsenal 11')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000021','Doppelkhunchomer',6,1,6,0,12,2,-1,0,-1,2,150,130,250,'NS','beachte Regelung im Arsenal','(5) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 78, ERRATA 4')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000022','Drachentöter',6,3,5,0,20,1,-3,-2,-4,3,400,400,0,'P','Wird immer von Spießgespann (siehe Sonderfertigkeit) geführt.','(2) ZWE','Aventurisches Arsenal 86')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000023','Drachenzahn',6,1,2,0,11,4,0,0,0,0,40,40,120,'H','Mit einem Drachenzahn  können Schläge von Kettenwaffen (mit der Außnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht parriert werden.','(12) ZWE','Aventurisches Arsenal 86')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000024','Dreizack',6,1,4,0,13,3,0,0,-1,5,90,140,50,'S','','(5) THO, ALB, HOR, MEN, BRA, ALA, THA, KHU, GAR (perricum), ARA, ORO, MAR, XER, BOR','Aventurisches Arsenal 18')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000025','Dreschflegel',6,1,3,0,12,3,-2,-1,-4,6,100,150,15,'S','Schläge eines Dreschflegels können mit Dolchen oder Fechtwaffen nicht pariert werden, ein Dreschflegel ignoriert den Verteidigungsbonus von Schilden.','(18) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 12')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000026','Dschadra',6,1,5,0,12,4,-1,0,-2,6,80,200,120,'S','Die hier angegebenen Werte gelten für die Verwendung im Nahkampf.','(10) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 78')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000027','Eberfänger',6,1,2,0,12,4,0,0,-1,1,40,40,60,'H','','(12) THO, SVE, RIV, BOR, AND, WEI, TOB, GAL, GAR, ALB, ZWE','Aventurisches Arsenal 19')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000028','Echsische Axt',6,1,5,0,12,4,0,0,-1,3,90,150,0,'NS','Ein Stich wird auf das Talent Speere (AT erschwert um 3) abgelegt ein erfolgreicher Einsatz des Dorns richtet 1W64 TP an.Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.','(2) ACH','Aventurisches Arsenal 94, ERRATA 5')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000029','Echsisches Szepter',6,1,3,0,11,3,-1,0,-1,4,120,90,0,'N',' unverkäuflich','nur bei Achaz-Schamanen','Aventurisches Arsenal 55')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000030','Efferdbart',6,1,4,0,13,3,0,0,-1,3,90,120,80,'NS',' oder nach Geweihtem','(4) THO, ALB, HOR, GAR (Perricum), ARA, BOR','Aventurisches Arsenal 54')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000031','Entermesser',6,1,3,0,12,4,0,0,0,2,70,75,50,'N','','(12) GLO, RIV, SVE, THO, ALB, HOR, ZYK, MEN, BRA, ALA, CHA, THA, KHU, ORO, ARA, GAR, XER, BOR','Aventurisches Arsenal 12')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000032','Fackel',6,1,0,0,11,5,-2,-2,-3,8,30,50,0.5,'HN','Richtet der Schlag SP an, kommen noch 1W-1 SP Feuerschaden hinzu! Fällt eine 6, so erlischt die Fackel.','nach Bedarf','Aventurisches Arsenal 17')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000034','Felsspalter',6,2,2,0,14,2,-1,0,-2,2,150,120,300,'N','beachte die Regelung im Arsenal, Ein (zwergischer) Waffenmeister (Felsspalter) hat gegen alle Gegner der Größenklassen groß oder größer eine Attacke-Erleichterung von 2 Punkten, egal welches (mit dem Felsspalter erlaubte und von ihm bereits erlernte) Angriffsmanöver er durchführt. Er darf Finten und Defensiven Kampfstil nutzen. Der Hammerschlag ist für ihn um 2 Punkte erleichtert (gegen große Wesen also sogar um 4 Punkte). Um diese SF zu erlernen, muss der zwergische Kämpfer eine GE und eine KK von jeweils 17 aufweisen','(5) ZWE','Aventurisches Arsenal  87, ERRATA 4')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000035','Fleischerbeil',6,1,2,0,11,4,-1,-2,-3,2,60,30,20,'H','','(12) alle Regionen ausser FIR, NIV, ELF, EHE, WAL','Aventurisches Arsenal 13')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000036','Florett',6,1,3,0,13,5,3,1,-1,4,30,90,180,'N','Mit dem Florett können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.','(3) ALM, HOR','Aventurisches Arsenal 61')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000037','Geißel',6,1,-1,0,14,5,-1,0,-4,5,30,100,15,'N','','(6) alle Regionen ausser FIR, NIV, ELF, EHE, THO, ZWE, WAL','Aventurisches Arsenal 13')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000038','Glefe',6,1,4,0,13,3,-1,0,-2,5,120,200,45,'S','','(10) BOR, WEI, GAR, AND, ALB, ALM, TOB, GAL, XER, RHA, HOR, ZYK, ARA','Aventurisches Arsenal 33')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000039','Großer Sklaventod',6,2,4,0,13,2,-2,0,-2,3,160,140,350,'NS','beachte Regelung im Arsenal','(4) ALA, BRA, CHA','Aventurisches Arsenal 84, ERRATA 4')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000040','Gruufhai',6,1,6,0,14,2,-2,-1,-2,3,180,120,120,'N','Schläge eines Gruufhai können mit Dolchen und Fechtwaffen nicht pariert werden. Kann ab KK 18 Einhändig mit Hiebwaffen geführt werden (TP/KK dann 15/3).','(8) ORK, SVE, AND (nur bei Orks)','Aventurisches Arsenal 93')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000041','Hakendolch',6,1,1,0,12,4,0,0,1,-2,50,60,90,'HN','beachte Regelung im Arsenal, Mit dem Hakendolch können auch die Angriffe von Kettenwaffen nicht pariert werden.(Errata WdS S.4)','(3) ORO, MHA, GOR, KHU, THA, MAR, SHI','Aventurisches Arsenal 61')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000042','Hakenspieß',6,1,3,0,13,4,0,-1,-1,5,120,250,70,'S','','(12) WIE, GAR, AND, ALB, ALM, TOB, XER, GAL, RHA, HOR, ARA','Aventurisches Arsenal 33')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000043','Haumesser',6,1,3,0,13,3,-1,0,-2,3,90,50,40,'HN','Für Haumesser gilt nicht der TP-Bonus für Reiter','(12) alle Regionen ausser FIR, NIV, ELF, EHE, WAL','Aventurisches Arsenal 13')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000044','Hellebarde',6,1,5,0,12,3,0,0,-1,5,150,200,75,'S','Schläge einer Hellebarde können mit Dolchen und Fechtwaffen nicht parriert werden.','(10) GLO, BOR, WEI, GAR, AND, ALB, ALM, TOB, GAL, RHA, XER, HOR, ARA','Aventurisches Arsenal 34')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000045','Holzfälleraxt',6,2,0,0,12,2,-2,-1,-4,5,160,110,80,'N','Schläge mit der Holzfälleraxt können von Dolchen und Fechtwaffen nicht pariert werden','(12) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 13')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000046','Holzspeer',6,1,3,0,12,5,0,-1,-3,5,60,150,10,'S','Ein Steinspitze richtet gegen nur mit Stoff, dünnem Leder oder Fell gerüstete Gegner einen TP mehr an.','(16) alle Regionen ausser FIR, KHO','Aventurisches Arsenal 19')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000047','Jagdmesser',6,1,2,0,12,5,-1,0,-2,3,15,30,50,'H','','(14) alle Regionen ausser EHE und WAL','Aventurisches Arsenal 19')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000048','Jagdmesser (elfische Version)',6,1,2,0,12,5,-1,0,-2,1,15,30,0,'H','','(6) FIR, ELF','Aventurisches Arsenal 90')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000049','Jagdspieß',6,1,6,0,12,4,-1,0,-1,3,80,200,80,'S','Kann ab KK 16 einhändig geführt werden (TP/KK dann 13/5).','(8) alle außer EHE, ZWE','Aventurisches Arsenal 20')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000050','Jagdspieß (elfische Variante)',6,1,6,0,12,4,-1,0,-1,2,75,200,80,'S','beachte Regelung im Arsenal (S. 91). Kann ab KK 16 einhändig geführt werden (TP/KK dann 13/5).','(16) ELF','Aventurisches Arsenal 20, 90, ERRATA 4')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000051','Kampfstab',6,1,1,0,12,4,1,0,0,5,80,150,40,'NS','Das Entwaffnen aus der AT ist um 2 Punkte erleichtert.','(8) alle Regionen ausser FIR, ZWE, ACH, WAL','Aventurisches Arsenal 62')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000052','Kettenstab',6,1,2,0,13,4,2,1,0,2,100,120,120,'HN','beachte Regelung im Arsenal. Erfordert mindestens GE 15. Das Entwaffnen aus der AT ist um 3 Punkte erleichtert.','(6) ARA, ORO, MAR, SKR, KHU, THA, SHI','Aventurisches Arsenal 65')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000053','Kettenstab (Drei-Glieder-Stab)',6,1,2,0,13,4,2,1,1,3,100,130,180,'HN','beachte Regelung im Arsenal, Der Kettenstab/Drei-Glieder-Stab ignoriert den PA Bonus von Schilden.','(2) ORO, MAR, SKR, KHU','Aventurisches Arsenal 65')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000054','Keule',6,1,2,0,11,3,0,0,-2,3,100,80,15,'N','','(18) alle','Aventurisches Arsenal 66')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000055','Khunchomer',6,1,4,0,12,3,0,0,0,2,90,80,130,'N','Vom Pferderücken aus gegen Fußkämpfer, richtet ein Khunchomer zwei zusätzliche TP an.','(12) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 79')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000056','Knochenkeule, groß',6,2,2,0,14,2,-2,-1,-3,2,220,150,0,'N','Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.','je nach Schamane','Aventurisches Arsenal 55')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000057','Knochenkeule, klein',6,1,2,0,11,4,0,-1,-1,4,80,50,0,'N','','je nach Schamane, unverkäuflich','Aventurisches Arsenal 54')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000058','Knochenkeule, mittel',6,1,3,0,11,3,0,0,-1,3,110,100,0,'N','','je nach Schamane unverkäuflich','Aventurisches Arsenal 54')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000059','Knüppel',6,1,1,0,11,4,0,0,-2,6,60,80,1,'N','','(18) alle','Aventurisches Arsenal 14')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000060','Korspieß',6,2,2,0,12,3,0,0,-1,3,140,180,200,'S','Schläge mit einem Korspieß können mit Dolchen und Fechtwaffen nicht pariert werden.','(4) ALM, HOR, ORO, MHA, GOR, KHU, MAR, THA, ALA','Aventurisches Arsenal 55')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000061','Kriegsfächer',6,1,2,0,12,5,0,0,1,3,50,40,250,'H','beachte Regelung im Arsenal','(2) ARA, ORO','Aventurisches Arsenal 80')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000062','Kriegsflegel',6,1,6,0,12,2,-1,-1,-2,5,120,150,50,'S','','(6) BOR, WEI, GAR, AND, ALB, ALM, TOB, GAL, RHA, XER, HOR, ARA','Aventurisches Arsenal 34')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000063','Kriegshammer',6,2,3,0,14,2,-2,-1,-3,2,180,100,120,'N','Schläge eines Kriegshammers können mit Dolchen und Fechtwaffen nicht pariert werden. Kann ab KK18 einhändig geführt werden mit dem Talent Hiebwaffen (TP/KK dann 15/3),  die TP/KK betragen dann 15/3','(6) alle Regionen ausser NIV, ELF, EHE, WAL','Aventurisches Arsenal 34')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000064','Kriegslanze',6,1,3,0,12,5,-2,-2,-4,5,150,300,120,'P','Die hier angegebenen Werte gelten für die Zweckfremde Verwendung als improvisierter Speer.','(8) BOR, WEI, GAR, AND, ALB, ALM, HOR, GAL, XER, RHA, ARA','Aventurisches Arsenal 38')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000065','Kurzschwert',6,1,2,0,11,4,0,0,-1,1,40,50,80,'HN','','(16) alle Regionen ausser FIR, NIV, EHE, WAL','Aventurisches Arsenal 27')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000066','Kusliker Säbel',6,1,3,0,12,4,1,0,0,1,70,80,160,'N','','(6) ALB, HAO, ZYK, MEN, BA, ALA','Aventurisches Arsenal 62')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000067','Langdolch',6,1,2,0,12,4,0,0,1,1,30,40,45,'H','beachte Regelung im Arsenal','(14) alle ausser FIR, EHE, ORK, WAL','Aventurisches Arsenal 66')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000068','Langschwert',6,1,4,0,11,4,0,0,0,1,80,95,180,'N','','(14) alle Regionen ausser FIR, NIV, EHE, WAL','Aventurisches Arsenal 27')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000069','Lindwurmschläger',6,1,4,0,11,3,-1,0,-1,1,95,50,120,'HN','beachte die Regelung im Arsenal','(12) ZWE','Aventurisches Arsenal 88')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000070','Linkhand',6,1,1,0,12,5,0,0,1,0,30,30,90,'H','beachte Regelung im Arsenal','(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 63')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000071','Magierdegen',6,1,2,0,13,5,1,0,-2,4,25,75,150,'N','Mit dem Magierdegen können Schläge von Kettenwaffen, Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwertern nicht pariert werden.','nur in Orten mit Magierakademien, wird nur an lizensierte Gildenmagier verkauft.','Aventurisches Arsenal 56')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000072','Magierrapier',6,1,3,0,12,5,1,0,-1,4,35,80,200,'N','Mit dem Magierdegen können Schläge von Kettenwaffen, Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwertern nicht pariert werden.','nur in Orten mit Magierakademien, wird nur an lizensierte Gildenmagier verkauft.','Aventurisches Arsenal 56')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000073','Magierstab (kurz)',6,1,0,0,11,4,0,-1,-1,0,70,100,0,'N','Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.','je nach Magier, unverkäuflich','Aventurisches Arsenal 56')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000074','Magierstab (sehr kurz)',6,1,-1,0,11,5,-1,-1,-1,0,60,100,0,'N','Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.','je nach Magier, unverkäuflich','Aventurisches Arsenal 57')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000075','Magierstab als Stab',6,1,1,0,11,5,0,-1,-1,0,90,150,0,'NS','Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.','je nach Magier, unverkäuflich','Aventurisches Arsenal 56')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000076','Magierstab m. Kristallk.',6,1,1,0,11,4,-2,-1,-2,0,150,150,0,'N','Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.','je nach Magier, unverkäuflich','Aventurisches Arsenal 56, ERRATA 3')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000077','Mengbilar',6,1,1,0,12,5,-2,0,-3,7,20,25,200,'H','beachte Regelung im Arsenal','(2) GAL, XER, RHA, ORO, MAR, SHI, MEN, BRA, ALA','Aventurisches Arsenal 68')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000078','Menschenfänger',0,0,0,0,0,0,-2,-4,-3,4,200,200,200,'S','beachte Regelung im Arsenal','(2) WEI, BOR, TOB, GAR, GAL, XER, RHA, ALM, HOR, THA, ALA','Aventurisches Arsenal 67')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000079','Messer',6,1,0,0,12,6,-2,-2,-3,4,10,25,10,'H','','(18) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 14')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000080','Messer (Steinklinge)',6,1,1,0,12,6,-2,-2,-3,6,12,20,5,'H','','(12) FIR, EHE, ORK, WAL, ALA, BRA, CHA','Aventurisches Arsenal 14')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000081','Meucheldolch',6,1,1,0,12,4,-1,0,-3,1,15,20,80,'H','beachte Regelung im Arsenal / BF, Gewicht und Länge können abweichen','sehr selten und üblicherweise nicht frei verkäuflich (außer GLO, XER, GAL, RHA, MAR, MEN und ALA)','Aventurisches Arsenal 67')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000082','Molokdeschnaja',6,1,4,0,11,3,0,0,0,3,100,100,90,'N','Vom Pferderücken aus gegen Fußkämpfer, richtet die Molokdeschnaja  zwei zusätzliche TP an.','(6) Bor, NIV, RIV, SVE','Aventurisches Arsenal 75')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000083','Morgenstern',6,1,5,0,14,2,-1,-1,-2,2,140,100,100,'N','Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist).','(8) GLO, THO, ORK, WEI, GAL, XER, RHA, AND, ALB, GAR, ALA','Aventurisches Arsenal 27')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000084','Nachtwind',6,1,4,0,11,5,2,0,0,0,70,100,500,'N','beachte Regelung im Arsenal. Erfordert mindestens GE 16 für einhändige Führung mit dem Talent Schwerter.','(4) MAR, SHI, KHU, THA','Aventurisches Arsenal 82')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000085','Neethaner Langaxt',6,2,2,0,13,4,-2,-1,-3,5,160,180,160,'S','Schläge einer Neethaner Langaxt können mit Dolchen und Fechtwaffen nicht pariert werden.','(4) HOR, MEN, BRA','Aventurisches Arsenal 35')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000086','Neunschwänzige',6,1,1,0,14,4,-1,-1,-4,5,80,120,60,'N','Bei einer AT/2 wird ein TP mehr angerichtet.','(4) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 15')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000087','Ochsenherde',6,3,3,0,17,1,-3,-2,-4,3,300,110,250,'N','Erfordert mindest KK 16. Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist). Eine 19 zählt bei dieser Waffe auch als Patzer.','(4) GLO, THO, ORK, WEI, TOB, GAL, XER, RHA, AND, ALB, GAR, ALA','Aventurisches Arsenal 28')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000088','Ogerfänger',6,1,2,0,12,4,0,0,-2,4,35,35,150,'H','Klinge bleibt in der Wunde stecken und verursacht pro SR 1W6 SP. Die Entfernung erzeugt 1W-1 bei gelungener Heilkunde Probe, sonst 2W6 SP','(8) THO, SVE, BOR, AND, WEI, TOB, GAL, GAR, ALB','Aventurisches Arsenal 20')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000089','Ogerschelle',6,2,2,0,15,1,-2,-1,-3,3,240,120,180,'N','Erfordert mindest KK 15. Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist).','(6) GLO, THO, ORK, WEI, TOB, GAL, XER, RHA, AND, ALB, GAR, ALA','Aventurisches Arsenal 28')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000090','Orchidee',6,1,1,0,12,5,0,-1,-2,3,35,0,180,'H','beachte Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Veteranenhand als unbewaffnet.Ein Kämpfer mit einer Orchidee besitzt allerdings einen Zonen-RS von 1, ein Kämpfer mit einer Veteranenhand einen Zonen-RS von 2.','(4) ORO, MENBRA, ALA, CHA','Aventurisches Arsenal 69')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000091','Orknase',6,1,5,0,12,2,-1,0,-1,4,110,100,75,'N','beachte Regeleung im Arsenal, Kann ab KK 14 einhändig geführt werden (TP/KK dann 13/3). Schläge der Waffe können von Dolchen und Fechtwaffen nicht','(14) THO, SVE, bei thorwalischen Söldner-Ottaskin','Aventurisches Arsenal 76, ERRATA 4')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000092','Pailos',6,2,4,0,14,2,-2,-1,-3,3,180,175,300,'S','beachte Regelung im Arsenal, Mit der Stoßklinge kann mit dem Talent Speere auch gestochen werden: 1W7 TP, AT um 5 erschwert, eigene nächste PA ebenfalls um 5 erschwert','(2) ZYK','Aventurisches Arsenal 85')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000093','Panzerarm',6,1,2,0,11,3,-1,0,0,-2,220,20,140,'H','','(4) ALB, HOR, ALM, ALA','Aventurisches Arsenal 97')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000094','Panzerstecher',6,1,4,0,12,3,0,-1,-1,0,80,90,120,'N','','(4) ALM, HOR','Aventurisches Arsenal 28')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000095','Partisane',6,1,5,0,13,3,0,0,-2,4,150,200,80,'S','','(8) WEI, BOR, TOB, AND, ALB, GAR, XER, RHA, ALM, HOR, THA, ALA','Aventurisches Arsenal 35')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000096','Peitsche',6,1,0,0,14,5,0,0,0,4,60,250,25,'S','In der DK Nahkampf ein AT-Malus von 8, im DK Handgemenge nicht zu verwenden.','(10) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 15')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000097','Pike',6,1,5,0,14,4,-2,-1,-2,6,180,350,50,'P',' GAL, XER, RHA, ALM, HOR, ARA','(12) BOR, GAR','Aventurisches Arsenal 36')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000098','Rabenschnabel',6,1,4,0,10,4,0,0,0,3,90,110,130,'N','Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet ein Rabenschnabel zwei zusätzliche TP an.','(8) ALM, ARA, MHA, GOR, KHU, KHO, HOR, ART, MEN, THA, ALA','Aventurisches Arsenal 39, 57')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000099','Rapier',6,1,3,0,12,4,1,0,0,2,45,90,120,'N','beachte Regelung im Arsenal','(6) ALB, GAR, ALM, HOR, ZYK, MEN, ALA','Aventurisches Arsenal 63')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000100','Rapier, Raufdegen (Almada)',6,1,3,0,12,4,1,0,0,2,45,90,120,'N','beachte Regelung im Arsenal','(6) ALB, GAR, ALM, HOR, ZYK, MEN, ALA','Aventurisches Arsenal 63')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000101','Reißer (echsische gezahnte Axt)',6,2,4,0,13,3,0,0,-2,4,110,140,0,'NS','Ein Stich wird auf das Talent Speere (AT erschwert um 4) abgelegt ein erfolgreicher Einsatz des Dorns richtet 1W65 TP an. Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.','(1) ACH','Aventurisches Arsenal 94, ERRATA 5')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000102','Richtschwert',6,3,4,0,13,2,-3,-2,-4,5,200,130,0,'N','','unverkäufliche Einzelstücke an allen Orten mit einer entsprechenden Gerichtsbarkeit','Aventurisches Arsenal 57')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000103','Robbentöter',6,1,3,0,12,4,0,0,0,2,70,90,200,'N','Vom Pferderücken aus gegen Fußkämpfer, richtet ein Robbentöter zwei zusätzliche TP an. Die Rückseite der Klinge ist meist bewusst stumpf gehalten, um bei Genickschlägen das Fell nicht zu verletzen. Die Manöver Stumpfer Schlag und Betäubungsschlag sind jeweils um 2 Punkte erleichtert. Bei der Waffenmeister-SF muss es heißen: »Ein Waffenmeister (Säbel) kann auch den Robbentöter einsetzen, ...','(4) FIR','Aventurisches Arsenal 91, ERRATA 5')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000104','Rondrakamm',6,2,2,0,12,3,0,0,0,1,130,130,0,'NS','','je nach Rondrageweihtem','Aventurisches Arsenal 58')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000105','Runaskraja',6,1,3,0,11,3,0,0,0,4,70,60,120,'N','','nur in Thorwal und Olport wird in Thorwal nur an lizensierte Gildenmagier verkauft','Aventurisches Arsenal 58')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000106','Säbel',6,1,3,0,12,4,1,0,0,2,60,90,100,'N','Vom PFerderücken aus gegen einen Fußkämpfer eingesetzt, richtet ein Säbel zwei zusätzliche TP an.','(10) GAR, ALB, LM, HOR, MEN, BRA, GAL, XER, RHA, ARA, ORO, ALA','Aventurisches Arsenal 39')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000107','Sägefischschwert',6,1,2,0,12,3,-1,-2,1,5,60,110,0,'N',' unverkäuflich','nur bei Tocamujac-Schamanen','Aventurisches Arsenal 55')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000108','Scheibendolch',6,1,2,0,11,4,0,0,-3,0,40,45,0,'H','siehe Reglung in der ERRATA','(10) WEI, AND, TOB, GAL, GAR, XER, RHA, ALB, ZWE, ALM, HOR','Aventurisches Arsenal 30, ERRATA 2')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000109','Schlagring',6,1,2,1,10,3,0,-1,-2,0,20,0,25,'H','beachte Regelung im Arsenal','(12) alle Regionen ausser FIR, ELF, EHE, ZWE, WAL','Aventurisches Arsenal 68')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000110','Schmiedehammer',6,1,4,0,14,2,-1,-1,-1,1,150,90,0,'N','Der Schmiedehammer ist eine geweihte Waffe und daher in der Lage gegen profane Waffen unempfindliche oder weniger empfindliche Wesen zu verletzen.','je nach Geweihtem','Aventurisches Arsenal 58')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000111','Schnitter',6,1,5,0,14,4,0,0,0,4,90,130,120,'NS','Kann ab GE 16 einhändig geführt werden (TP 1W3, TP/KK 15/5, WM 0/-1)','(10) ARA, ORO, MAR, SHI, KHU, MHA, THA, ACH, ALA, BRA, CHA','Aventurisches Arsenal 36')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000112','Schwerer Dolch',6,1,2,0,12,4,0,0,-1,1,30,35,40,'H','Mit dem Schweren Dolch  können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.','(16) alle ausser EHE, WAL','Aventurisches Arsenal 69')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000113','Sense',6,1,3,0,13,4,-2,-2,-4,7,100,160,30,'S','','(16) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 16')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000114','Sichel',6,1,2,0,12,5,-2,-2,-2,6,30,50,25,'H','','(12) alle Regionen außer FIR, EHE, WAL','Aventurisches Arsenal 16')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000115','Sklaventod',6,1,4,0,12,3,0,0,0,3,80,90,250,'N','Vom Pferderücken aus gegen Fußkämpfer, richtet ein Sklaventod zwei zusätzliche TP an.','(10) ALA, BRA, THA, CHA','Aventurisches Arsenal 84')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000116','Skraja',6,1,3,0,11,3,0,0,0,4,90,70,50,'N','Ein Stich mit dem Dorn der Skraja (AT auf das Talent Dolche, erschwert um 3) richtet 1W63 TP an.','(12) THO, SVE, bei thorwalischen Söldner-Ottaskin','Aventurisches Arsenal 76')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000117','Sonnenszepter',6,1,3,0,12,3,0,-1,-1,1,90,70,0,'N','','je nach Praios-Geweihtem','Aventurisches Arsenal 59')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000118','Speer',6,1,3,0,12,4,-1,0,-2,5,80,190,30,'S','Kann ab KK 15 einhändig geführt werden (TP/KK dann 13/5). Die Manöver Gegenhalten und Gezielter Stich sind für ihn um 2 Punkte erleichtert.','(18) alle Regionen','Aventurisches Arsenal 21, ERRATA 1')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000119','Spitzhacke',6,1,6,0,13,2,-3,-2,-4,5,200,100,30,'N','Einhändig ab KK 18, dann TP/KK 14/3','(8) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 16')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000120','Stockdegen',6,1,3,0,12,5,0,-1,-3,4,35,80,180,'N','','(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 64')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000122','Stoßspeer',6,2,2,0,11,4,-1,0,-1,3,150,200,100,'S','','(10) FIR, GLO, NIV, RIV, SVE, THO, ORK, WIE, BOR, TOB, AND, ALB, GAR, GAL, XER, RHA, ALM, HOR','Aventurisches Arsenal 22')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000123','Streitaxt',6,1,4,0,13,2,0,0,-1,2,120,90,50,'N','','(12) alle Regionen ausser ELF, EHE, WAL','Aventurisches Arsenal 30')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000124','Streitkolben',6,1,4,0,11,3,0,0,-1,1,120,75,50,'N','Vom Pferderücken aus gegen den Fußkämpfer eingesetzt, richtet der Streitkolben zwei zusätzliche TP an.','(12) alle Regionen ausser FIR, NIV, ELF, EHE, SVE, ACH, WAL','Aventurisches Arsenal 30')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000125','Stuhlbein',6,1,0,0,11,5,-1,-1,-1,8,40,40,0,'HN','siehe Regeln zu improvisierten Waffen','nach Bedarf','Aventurisches Arsenal 17')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000126','Sturmsense',6,1,4,0,13,3,-1,-1,-2,5,120,180,40,'S','','(12) BOR, WEI, GAR, AND, ALB, ALM, TOB, HOR, ARA','Aventurisches Arsenal 36')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000127','Turnierlanze',6,1,2,1,12,5,-2,-2,-4,8,120,300,50,'P','Die hier angegebenen Werte gelten für die Zweckentfremdung als improvisierter Speer. Ihre Werte im Reiterkampf siehe WdS.','(8) BOR, WEI, GAR, AND, ALB, ALM, HOR, GAL, XER, RHA, ARA','Aventurisches Arsenal 39')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000128','Turnierschwert',6,1,3,1,11,5,0,0,0,3,60,90,80,'N','Es existieren auch hölzerne, ähnlich ausgewuchtete Übrungsvarianten (1W1 TP (A), BF 5).','(10) BOR, WEI, AND, ALB, GAR, ALM, ARA, HOR','Aventurisches Arsenal 31')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000129','Tuzakmesser',6,1,6,0,12,4,1,0,0,1,100,130,400,'NS','beachte Regelung im Arsenal. Mindest GE 15, ansonsten pro Punkt unter 15 TP, INI und PA -1.','(4) BOR (nur Festum), ARA, ORO, MAR, SHI, KHU, THA','Aventurisches Arsenal 82')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000130','Veteranenhand, Orchidee',6,1,1,0,12,5,0,-1,-2,3,35,0,180,'H','beachte Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Veteranenhand als unbewaffnet.Ein Kämpfer mit einer Orchidee besitzt allerdings einen Zonen-RS von 1, ein Kämpfer mit einer Veteranenhand einen Zonen-RS von 2.','(4) ORO, MENBRA, ALA, CHA','Aventurisches Arsenal 69')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000131','Vorschlaghammer',6,1,5,0,13,2,-3,-2,-4,5,250,90,30,'NS','','(8) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 16')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000132','Vulkanglasdolch',6,1,-1,0,12,5,-2,-2,-3,6,30,30,0,'H','beachte Regelung im Arsenal','je nach Druide','Aventurisches Arsenal 59')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000133','Waqqif',6,1,2,0,12,5,-2,-1,-3,2,35,45,60,'H','Mit dem Waqqif können die Schläge von Kettenwaffen (mit der Ausnahme vonGeißel und Neunschänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwertern oder -säbeln nicht parriert werden.','(10) KHO, MHA, ARA, KHU, GOR, THA, ART','Aventurisches Arsenal 80')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000134','Warunker Hammer',6,1,6,0,14,3,-1,0,-1,2,150,150,150,'NS','Der Schlag eines Warunker Hammers  kann mit Fechtwaffen und Dolchen nicht pariert werden. Mit der Stoßklinge kann mit dem Talent Speere auch gestochen werden: 1W5 TP, AT um 3 erschwert','(4) WEI, GAR, ALM, TOB, GAL, RHA, XER','Aventurisches Arsenal 37')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000135','Wolfsmesser',6,1,3,0,12,4,1,0,0,1,50,90,250,'N','Vom Pferderücken aus gegen Fußkämpfer, richtet ein Wolfsmesser zwei zusätzliche TP an. Im Sinne der Parade-Einschränkungen von Fechtwaffen gilt das Wolfsmesser als Schwert.','(4) ELF','Aventurisches Arsenal 91, ERRATA 5')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000136','Wurfbeil (im Nahkampf)',6,1,3,0,10,4,-1,0,-2,2,50,40,35,'H','','(8) FIT, GLO, NIV, SVE, THO, ORK, AND, WIE, BOR, TOB','Aventurisches Arsenal 77')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000137','Wurfdolch (im Nahkampf)',6,1,1,0,12,5,-1,-1,-2,2,20,25,30,'H','Es gelten nicht nur die üblichen Parade-Einschränkungen für Dolche, sondern zudem ist das Wurfmesser noch eine imrovisierte Waffe für den Nahkampf','(7) ALB, GAR, GAL, XER, ALM, HOR, ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, BRA, ALA, CHA','Aventurisches Arsenal 71')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000138','Wurfkeule (im Nahkampf)',6,1,2,0,12,5,-1,-1,-1,3,35,40,18,'H','','(12) NIV, deutlich seltener auch den Sippen RIV und BOR','Aventurisches Arsenal 77')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000139','Wurfmesser (im Nahkampf)',6,1,-1,0,12,6,-1,-2,-3,2,10,20,15,'H','Es gelten nicht nur die üblichen Parade-Einschränkungen für Dolche, sondern zudem ist das Wurfmesser noch eine imrovisierte Waffe für den Nahkampf','(8) ALB, GAR, GAR, XER, ALM, HOR, ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, BRA, ALA, CHA','Aventurisches Arsenal 71')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000140','Wurfspeer (im Nahkampf)',6,1,3,0,11,5,-2,-1,-3,4,80,100,30,'N','','(13) ALM, ARA, ORO, MHA, GOR, KHU, KHO, THA, HOR, ART, ALA','Aventurisches Arsenal 23')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000141','Wurmspieß',6,1,5,0,13,4,0,0,-2,2,120,180,120,'S','','(9) ZWE','Aventurisches Arsenal 89')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000142','Zweihänder',6,2,4,0,12,3,-1,0,-1,2,160,155,250,'NS','Der Schlag eines Zweihänders kann mit Fechtwaffen und Dolchen nicht pariert werden.','(9) BOR, NIV, WEI, AND, GAR, ALB, ALM, GAL, RHA, XER, HOR','Aventurisches Arsenal 37')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000143','Zweililien',6,1,3,0,12,4,1,1,-1,4,80,140,200,'N','beachte Regelung im Arsenal. Erleichtert das entwaffnen aus der AT um 4 Punkte (Errata WdS S.4)','(6) ALB, HOR, ZWE, MHA','Aventurisches Arsenal 64')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000144','Zwergenschlägel',6,1,5,0,13,3,-1,0,-1,1,120,120,150,'N','Mit dem Zwergenschlägel können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.','(8) ZWE','Aventurisches Arsenal 89')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000145','Zwergenskraja',6,1,3,0,11,3,0,0,0,1,80,60,100,'HN','Ein Stich mit dem Dorn der Zwergenskraja (AT auf das Talent Dolche, erschwert um 4) richtet 1W63 TP an.','(8) ZWE','Aventurisches Arsenal 89, ERRATA 4')
GO


INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000001','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000002','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000002','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000003','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000004','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000004','Anderthalbhänder')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000005','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000005','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000006','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000007','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000008','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000009','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000010','Anderthalbhänder')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000010','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000011','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000012','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000013','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000014','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000015','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000016','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000017','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000018','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000019','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000020','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000021','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000022','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000023','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000024','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000025','Zweihandflegel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000026','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000027','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000028','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000029','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000030','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000031','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000032','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000034','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000035','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000036','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000037','Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000038','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000039','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000040','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000041','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000042','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000043','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000044','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000045','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000046','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000047','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000048','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000049','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000050','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000051','Stäbe')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000052','Kettenstäbe')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000053','Kettenstäbe')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000054','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000055','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000056','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000057','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000058','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000059','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000060','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000061','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000062','Zweihandflegel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000063','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000064','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000065','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000065','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000065','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000066','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000066','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000067','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000068','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000069','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000070','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000071','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000072','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000073','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000074','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000075','Stäbe')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000076','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000077','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000078','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000079','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000080','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000081','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000082','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000083','Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000084','Anderthalbhänder')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000084','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000085','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000085','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000086','Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000087','Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000088','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000089','Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000090','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000091','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000092','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000092','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000093','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000094','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000095','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000095','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000096','Peitsche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000097','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000098','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000099','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000099','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000100','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000100','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000101','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000102','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000102','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000103','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000103','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000103','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000104','Anderthalbhänder')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000104','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000105','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000106','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000106','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000107','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000108','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000109','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000110','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000111','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000111','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000111','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000112','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000113','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000114','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000115','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000116','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000117','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000118','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000119','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000120','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000122','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000123','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000124','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000125','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000126','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000127','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000128','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000129','Anderthalbhänder')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000129','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000130','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000131','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000132','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000133','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000133','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000134','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000134','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000135','Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000135','Schwerter')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000135','Säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000136','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000137','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000138','Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000139','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000140','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000141','Speere')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000141','Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000142','Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000143','Stäbe')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000144','Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0000-000000000145','Hiebwaffen')
GO


INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000001','Einfacher Holzschild',40,140,'groß','S',-1,3,-1,3,'(14) GLO, BOR, AND, GAR, TOB, GAL, XER, RHA, ALB, ALM, HOR, ZYK, MEN, MAR, WEI, ZWE','Aventurisches Arsenal 96, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000002','Verstärkter Holzschild',50,160,'groß','S',-2,3,-1,0,'(14) GLO, BOR, AND, GAR, TOB, GAL, XER, RHA, ALB, ALM, HOR, ZYK, MEN, MAR, WEI, ZWE','Aventurisches Arsenal 96, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000003','Lederschild',30,80,'groß','S',-1,3,0,5,'(12) FIR, GLO, NIV, ORK, KHO, MHA, ARA, ORO, ART, WAL, CHA, WEI','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000004','Mattenschild',60,100,'groß','S',-1,4,0,6,'(9) ACH, SKR, ORK, CHA ','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000005','Großer Lederschild',75,120,'groß','S',-1,4,-1,6,'(12) FIR, GLO, NIV, ORK, KHO, MHA, ARA, ORO, ART, WAL, CHA','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000006','Thorwalerschild',60,180,'groß','S',-2,4,-1,3,'(12) THO, SVE, RIV oder bei Thorwaler Solnder-Ottas','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000007','Großschild (Reiterschild)',100,200,'sehr groß','S',-2,5,-2,2,'(14) THO, BOR, AND, GAR, TOB, GAL, XER, RHA, ALB, ALM, HOR, WEI','Aventurisches Arsenal 96, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000008','Turmschild',120,280,'sehr groß','S',-5,7,-3,1,'(14) GAR, ALM, HOR, ALA, ZWE','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000009','Buckler',40,40,'klein','SP',0,1,0,0,'(10) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 96',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000010','Großer (Vollmetall-) Buckler',60,60,'klein','SP',0,2,0,-2,'(10) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 96',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000011','Panzerarm',140,220,'klein','SP',-2,1,0,-2,'(4) ALB, HOR, ALM, ALA','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000012','Drachenklaue',350,200,'klein','SP',-2,1,0,0,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000013','Bock',80,120,'klein','SP',-1,1,0,0,'(4) THO, AND, ALB, GAR, GAL, XER, HOR, ALM, MEN, MHA, GOR, KHU, THA, ALA, WEI','Aventurisches Arsenal 95, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000014','Hakendolch',90,50,NULL,'P',-1,3,0,-2,'(2) ORO, MHA, GOR, KHU, THA, MAR, SHI','Aventurisches Arsenal 61',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000015','Kriegsfächer',250,50,NULL,'P',0,2,1,3,'(2) ARA, ORO','Aventurisches Arsenal 80',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000016','Linkhand',90,30,NULL,'P',0,2,1,0,'(2) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 62',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000017','Langdolch',45,30,NULL,'P',0,1,0,1,'(14) alle ausser FIR, EHE, ORK, WAL','Aventurisches Arsenal 66',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000018','Linkhand mit Klingenfänger',60,35,NULL,'P',0,1,0,1,'(14) alle ausser FIR, EHE, ORK, WAL','Aventurisches Arsenal 66',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000019','Linkhand mit Klingenbrecher',70,40,NULL,'P',0,1,0,1,'(14) alle ausser FIR, EHE, ORK, WAL','Aventurisches Arsenal 66',NULL,NULL)
GO


INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000001','Amazonenrüstung',0,320,'Plattenrüstungen',2,'K',3,5,3,5,2,2,3,3,3,1,5,3,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,'unverkäuflich')
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000002','Anaurak',0,200,'Kleidung',0,NULL,1,1,1,1,1,1,1,1,1,4,1,4,NULL,'Aventurisches Arsenal 100',NULL,'Preis variabel')
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000003','Armschienen, Bronze',25,60,'Plattenrüstungen',0,'Z',0,0,0,0,2,2,0,0,0,0,NULL,NULL,'(4) ORK, GAR, ARA, ORO, ALM, HOR, ZYK, MEN, BRA, ALA','Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000004','Armschienen, Leder',15,40,'Lederrüstungen',0,'Z',0,0,0,0,1,1,0,0,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 102',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000005','Armschienen, Stahl',35,60,'Plattenrüstungen',0,'Z',0,0,0,0,3,3,0,0,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000006','Baburiner Hut',60,120,'Plattenrüstungen',1,'Z',4,0,1,0,0,0,0,0,0,0,2,1,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000007','Bart/Halsberge',45,40,'Plattenrüstungen',0,'Z',2,1,1,0,0,0,0,0,0,0,0,0,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000008','Beinschienen, Bronze',35,120,'Plattenrüstungen',0,'Z',0,0,0,0,0,0,2,2,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000009','Beinschienen, Leder',25,40,'Lederrüstungen',0,'Z',0,0,0,0,0,0,1,1,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000010','Beinschienen, Stahl',50,120,'Plattenrüstungen',0,'Z',0,0,0,0,0,0,3,3,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000011','Beintaschen/Schürze',90,80,'Plattenrüstungen',0,'Z',0,0,0,2,0,0,2,2,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000012','Ringmantel (Brabakmantel)',180,360,'Kette/Schuppe',1,NULL,0,3,3,3,2,2,2,2,2,1,3,3,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000013','Brigantina',350,240,'Kette/Schuppe',0,NULL,0,5,4,4,2,2,0,0,2,2,3,2,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000014','Bronzeharnisch',250,240,'Plattenrüstungen',0,NULL,0,5,4,4,0,0,0,0,2,2,3,3,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000015','Brustplatte, Leder',50,80,'Lederrüstungen',0,NULL,0,2,0,1,0,0,0,0,0,0,1,1,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000016','Brustplatte, Stahl',50,80,'Plattenrüstungen',0,NULL,0,2,0,1,0,0,0,0,0,0,1,1,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000017','Brustschalen',25,20,'Plattenrüstungen',0,'Z',0,2,0,0,0,0,0,0,0,0,1,0,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000018','Dicke Kleidung',0,120,'Kleidung',0,NULL,0,1,1,1,1,1,1,1,0,0,1,1,NULL,'Aventurisches Arsenal 100, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000019','Drachenhelm',80,120,'Plattenrüstungen',0,'Z',3,0,1,0,0,0,0,0,0,0,2,1,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000020','Eisenmantel',500,240,'Kette/Schuppe',1,NULL,0,5,5,5,2,2,2,2,3,2,4,3,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000021','Fellumhang/Fuhrmannsmantel',0,120,'Kleidung',0,'Z',0,1,2,0,1,1,1,1,0,0,1,0,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000022','Fünflagenharnisch',600,280,'Kette/Schuppe',0,NULL,0,5,5,4,0,0,1,1,3,3,3,2,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000023','Gambeson/Wattierter Waffenrock',40,120,'Tuchrüstungen',0,NULL,0,2,2,2,1,1,1,1,1,1,2,2,NULL,'Aventurisches Arsenal 101, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000024','Garether Platte',750,560,'Plattenrüstungen',1,NULL,0,6,5,6,5,5,4,4,4,3,6,4,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000025','Gestechrüstung',2500,1200,'Plattenrüstungen',0,'K',8,8,7,8,7,7,7,7,7,7,12,10,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000026','Gladiatorenschulter',180,160,'Plattenrüstungen',1,NULL,0,3,2,0,3,0,0,0,1,0,2,1,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000027','Hohe Stiefel',0,80,'Kleidung',0,'Z',0,0,0,0,0,0,1,1,0,0,0,0,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000028','Horasischer Reiterharnisch',1000,680,'Plattenrüstungen',2,'K',3,7,5,7,5,5,5,5,5,3,8,5,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000029','Iryanrüstung',125,140,'Lederrüstungen',1,NULL,0,3,2,2,0,0,1,1,1,0,3,2,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000030','Kettenbeinlinge, Paar',200,320,'Kette/Schuppe',0,'Z',0,0,0,0,0,0,4,4,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000031','Kettenhandschuhe, Paar',100,60,'Kette/Schuppe',0,'Z',0,0,0,0,1,1,0,0,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000032','Kettenhaube',80,140,'Kette/Schuppe',0,'Z',3,1,1,0,0,0,0,0,0,0,1,1,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000033','Kettenzeug',350,480,'Kette/Schuppe',0,'Z',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,1,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000034','Kettenhaube, mit Gesichtsschutz',100,160,'Kette/Schuppe',0,'Z',4,1,1,0,0,0,0,0,0,0,1,1,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000035','Kettenhemd, 1/2 Arm',150,260,'Kette/Schuppe',1,NULL,0,4,4,4,2,2,1,1,2,1,3,3,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000036','Kettenhemd, lang',180,400,'Kette/Schuppe',1,NULL,0,4,4,4,3,3,2,2,3,2,4,4,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000037','Kettenkragen',60,100,'Kette/Schuppe',1,'Z',2,1,1,0,0,0,0,0,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000038','Kettenmantel',500,480,'Kette/Schuppe',1,NULL,0,4,4,4,3,3,3,3,3,2,5,5,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000039','Kettenweste',100,200,'Kette/Schuppe',1,NULL,0,4,4,4,0,0,0,0,2,1,2,2,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000040','Krötenhaut',60,160,'Lederrüstungen',1,NULL,0,3,2,2,1,1,0,0,1,0,3,2,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000041','Kürass',110,160,'Plattenrüstungen',1,NULL,0,5,1,2,0,0,0,0,1,0,3,2,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000042','Kusliker Lamellar',500,300,'Plattenrüstungen',0,NULL,0,5,4,4,1,1,1,1,2,2,4,3,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000043','Lederharnisch',80,180,'Lederrüstungen',0,NULL,0,3,3,3,0,0,0,0,1,1,3,3,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000044','Lederhelm',20,60,'Lederrüstungen',0,'Z',2,0,0,0,0,0,0,0,0,0,1,1,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000045','Lederhelm, verstärkt',30,70,'Lederrüstungen',0,'Z',3,0,0,0,0,0,0,0,0,0,1,1,NULL,'Aventurisches Arsenal 102',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000046','Lederzeug',40,80,'Lederrüstungen',0,'Z',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,1,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000047','Lederhose',0,80,'Kleidung',0,'Z',0,0,0,1,0,0,1,1,0,0,0,0,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000048','Lederweste/Pelzweste',0,80,'Kleidung',0,NULL,0,1,1,1,0,0,-1,-1,0,0,1,1,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000049','Leichte Platte',250,300,'Plattenrüstungen',1,NULL,0,5,4,5,0,0,2,2,3,2,4,3,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000050','Löwenmähne',100,200,'Kette/Schuppe',1,'Z',2,2,2,0,1,1,0,0,1,0,1,1,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000051','Mammutonpanzer',1500,240,'Exotische Materialien',1,NULL,0,4,4,4,2,2,2,2,3,2,5,3,NULL,'Aventurisches Arsenal 103, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000052','Maraskanischer Hartholzharnisch',1200,280,'Exotische Materialien',1,NULL,0,4,4,4,1,1,1,1,2,1,4,2,NULL,'Aventurisches Arsenal 103, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000053','Mattenrücken',65,140,'Tuchrüstungen',0,NULL,1,1,3,0,0,0,0,0,0,0,1,0,NULL,'Aventurisches Arsenal 101',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000054','Morion',75,160,'Plattenrüstungen',1,'Z',3,0,0,0,0,0,0,0,0,0,2,1,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000055','Panzerbein',150,240,'Plattenrüstungen',0,'Z',0,0,0,0,0,0,4,4,0,0,2,2,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000056','Panzerhandschuhe, Paar',120,60,'Plattenrüstungen',0,'Z',0,0,0,0,2,2,0,0,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000057','Panzerschuh',120,40,'Plattenrüstungen',0,'Z',0,0,0,0,0,0,1,1,0,0,0,1,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000058','Plattenarme',200,120,'Plattenrüstungen',0,'Z',0,0,0,0,5,5,0,0,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000059','Plattenschultern',150,120,'Plattenrüstungen',0,'Z',0,1,1,0,2,2,0,0,0,0,NULL,NULL,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000060','Plattenzeug',500,380,'Plattenrüstungen',0,'Z',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,2,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000061','Ringelpanzer',550,280,'Kette/Schuppe',1,NULL,0,4,4,4,3,3,1,1,2,1,4,3,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000062','Schaller',60,160,'Plattenrüstungen',1,'Z',4,0,0,0,0,0,0,0,0,0,2,1,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000063','Schaller (mit Bart)',100,200,'Plattenrüstungen',0,'Z',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,1,NULL,'Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000064','Schuppenpanzer',1000,480,'Kette/Schuppe',0,NULL,0,5,5,5,3,3,3,3,3,3,5,5,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000065','Schuppenpanzer, lang',1200,720,'Kette/Schuppe',0,NULL,0,5,5,5,3,3,4,4,4,4,6,6,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000066','Spiegelpanzer',1000,400,'Kette/Schuppe',1,NULL,0,5,5,5,3,3,2,2,3,2,5,4,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000067','Stechhelm/Visierhelm',100,160,'Plattenrüstungen',0,'Z',5,0,0,0,0,0,0,0,0,0,2,2,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000068','Streifenschurz',40,120,'Lederrüstungen',1,'Z',0,0,0,2,0,0,2,2,0,0,1,0,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000069','Sturmhaube',70,140,'Plattenrüstungen',1,'Z',3,0,0,0,0,0,0,0,0,0,2,1,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000070','Tellerhelm',30,60,'Plattenrüstungen',0,'Z',2,0,0,0,0,0,0,0,0,0,1,1,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000071','Topfhelm',80,180,'Plattenrüstungen',0,'Z',5,0,0,0,0,0,0,0,0,0,2,2,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000072','Tuchrüstung',50,100,'Tuchrüstungen',0,NULL,0,2,2,2,0,0,0,0,1,1,2,2,NULL,'Aventurisches Arsenal 101, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000073','Unterzeug mit Kettenteilen',80,160,'Tuchrüstungen',0,NULL,0,2,2,1,2,2,1,1,1,1,2,1,NULL,'Aventurisches Arsenal 101',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000074','Wattierte Kappe',5,20,'Tuchrüstungen',0,'Z',1,0,0,0,0,0,0,0,0,0,0,0,NULL,'Aventurisches Arsenal 101',NULL,NULL)
GO
INSERT INTO [Rüstung] ([RüstungGUID],[Name],[Preis],[Gewicht],[Gruppe],[Verarbeitung],[Art],[Kopf],[Brust],[Rücken],[Bauch],[LArm],[RArm],[LBein],[RBein],[gRS],[gBE],[RS],[BE],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0000-000000000075','Wattiertes Unterzeug/Wattierte Unterkleidung',25,100,'Tuchrüstungen',0,NULL,0,1,1,1,1,1,1,1,0,0,1,1,NULL,'Aventurisches Arsenal 101, Wege des Schwerts 136',NULL,NULL)
GO