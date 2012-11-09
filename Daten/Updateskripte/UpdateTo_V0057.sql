-- Torsionswaffenvarianten, Literaturkorrektur in Ausrüstung
UPDATE Fernkampfwaffe SET Laden=12 WHERE FernkampfwaffeGUID='00000000-0000-0000-0002-000000000003';
UPDATE Fernkampfwaffe SET Laden=4 WHERE FernkampfwaffeGUID='00000000-0000-0000-0002-000000000004';
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000046','Arbalette (Bolzen)',0,200,'(4) ZWE, HOR, ALM','Aventurisches Arsenal 41 /  WdS 127','Aventurien',NULL);
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000047','Arbalone (Bolzen)',0,480,'(2) HOR','Aventurisches Arsenal 43 /  WdS 127','Aventurien',NULL);
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000048','Balestra (Bolzen)',0,150,'(3) HOR, ZWE','Aventurisches Arsenal 41 /  WdS 127','Aventurien',NULL);
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000049','Balestrina (Bolzen)',0,60,'(5) HOR, ZWE, MEN, ALM','Aventurisches Arsenal 41 /  WdS 127','Aventurien',NULL);
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000050','Arbalette (Repetierer)',0,200,'(4) ZWE, HOR, ALM','Aventurisches Arsenal 41 /  WdS 127','Aventurien','bis zu 7 Kugeln, verklemmt bei 19-20');
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000051','Arbalone (Repetierer)',0,480,'(2) HOR','Aventurisches Arsenal 43 /  WdS 127','Aventurien','bis zu 7 Kugeln, verklemmt bei 19-20');
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000052','Balestra (Repetierer)',0,150,'(3) HOR, ZWE','Aventurisches Arsenal 41 /  WdS 127','Aventurien','bis zu 3 Kugeln, verklemmt bei 19-20');
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000053','Balestrina (Repetierer)',0,60,'(5) HOR, ZWE, MEN, ALM','Aventurisches Arsenal 41 /  WdS 127','Aventurien','bis zu 3 Kugeln, verklemmt bei 19-20');
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000054','Arbalette (Bolzenrepetierer)',0,200,'(4) ZWE, HOR, ALM','Aventurisches Arsenal 41 /  WdS 127','Aventurien','bis zu 3 Bolzen, verklemmt bei 19-20');
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000055','Arbalone (Bolzenrepetierer)',0,480,'(2) HOR','Aventurisches Arsenal 43 /  WdS 127','Aventurien','bis zu 3 Bolzen, verklemmt bei 19-20');
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000046',2.5,8,'Bolzen',0,6,2,7,0,NULL,NULL,1,11,22,33,66,110,2,1,0,-1,-2,30);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000047',4,10,'Bolzen',0,6,3,8,0,NULL,NULL,1,17,33,66,132,275,4,2,0,-1,-3,40);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000048',2,5,'Bolzen',0,6,2,4,0,NULL,NULL,1,11,22,33,55,83,2,1,0,0,-1,12);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000049',1.5,5,'Bolzen',0,6,1,6,0,NULL,NULL,1,2,4,8,15,25,2,1,0,0,-1,4);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000050',2.5,8,'Kugel',0,6,2,5,0,NULL,NULL,1,10,20,30,60,100,2,1,0,-1,-2,27);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000051',4,10,'Kugel',0,6,3,6,0,NULL,NULL,1,15,30,60,120,250,4,2,0,-1,-3,37);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000052',2,5,'Kugel',0,6,2,2,0,NULL,NULL,1,10,20,30,50,75,2,1,0,0,-1,11);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000053',1.5,5,'Kugel',0,6,1,4,0,NULL,NULL,0,2,4,8,15,25,2,1,0,0,-1,3);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000054',2.5,8,'Bolzen',0,6,2,7,0,NULL,NULL,1,11,22,33,66,110,2,1,0,-1,-2,27);
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden]) VALUES ('00000000-0000-0000-0002-000000000055',4,10,'Bolzen',0,6,3,8,0,NULL,NULL,1,17,33,66,132,275,4,2,0,-1,-3,37);

INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000046', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000047', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000048', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000049', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000050', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000051', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000052', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000053', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000054', 'Armbrust');
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0002-000000000055', 'Armbrust');


Update Ausrüstung set Literatur = Replace(Literatur, 'Aventurisches Arsenal', 'AA') Where Literatur like '%Aventurisches Arsenal%';
Update Ausrüstung set Literatur = Replace(Literatur, 'Katakomben und Kavernen', 'K&amp;K') Where Literatur like '%Katakomben und Kavernen%';
Update Ausrüstung set Literatur = Replace(Literatur, 'Wege des Schwertes', 'WdS') Where Literatur like '%Wege des Schwertes%';
Update Ausrüstung set Literatur = Replace(Literatur, 'Wege des Schwerts', 'WdS') Where Literatur like '%Wege des Schwerts%';
Update Ausrüstung set Literatur = Replace(Literatur, ', WdS', ' / WdS') Where Literatur like '%, WdS%';
Update Ausrüstung set Literatur = Replace(Literatur, 'ERRATA', 'Errata') Where Literatur like '%ERRATA%';
UPDATE Ausrüstung SET Literatur='WdS 130, Errata 1, 4' WHERE AusrüstungGUID='00000000-0000-0000-0002-000000000022';

DELETE FROM Munition;
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000001','Schleuderblei','Stein',0,NULL,'AA 21');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000002','Schleuderblei','Schleuderblei',1,'Fernkampf um 1 erleichtert, TP + 1','AA 21');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000003','Pfeil','Jagdpfeil',1,'wiederverwendbar','AA 46');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000004','Pfeil','Kriegspfeil',1,'Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 46');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000005','Pfeil','Gehärteter Kriegspfeil',4,'RS/2, Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 47');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000006','Pfeil','Kettenbrecher',3,'RS durch Kettengeflecht wird ignoriert, RS von Mischrüstungen (Spiegelpanzer, Baburiner Hut, ...) wird halbiert','AA 47');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000007','Pfeil','Stumpfer Pfeil',2,'Halbe Reichweite, richtet TP(A) an, Scharfschütze kann einen gezielten Schuß +4 zum niederwerfen machen (KK + TP(A)/2 oder man liegt am Boden)','AA 47');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000008','Pfeil','Sehnen-Seilschneider',3,'TP+1W6, RS*2, Halbe Reichweite, siehe Ogerfänger','AA 47');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000009','Pfeil','Brandpfeil',8,'Halbe Reichweite, wie stumpfer Pfeil','AA 48');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000010','Pfeil','Singender Pfeil',2,'macht Geräusche im Flug, je nach Qualität bis zehnfacher Preis und TP Abzug bis zu 2','AA 48');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000011','Bolzen','Jagdbolzen',1,'wiederverwendbar','AA 46                                                                          ');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000012','Bolzen','Kriegsbolzen',1,'Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 46                  ');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000013','Bolzen','Gehärteter Kriegsbolzen',4,'RS/2, Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 47  ');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000014','Bolzen','Kettenbrecher',3,'RS durch Kettengeflecht wird ignoriert, RS von Mischrüstungen (Spiegelpanzer, Baburiner Hut, ...) wird halbiert','AA 47');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000015','Bolzen','Stumpfer Bolzen',2,'Halbe Reichweite, richtet TP(A) an, Scharfschütze kann einen gezielten Schuß +4 zum niederwerfen machen (KK + TP(A)/2 oder man liegt am Boden)','AA 47');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000016','Bolzen','Sehnen-Seilschneider',3,'TP+1W6, RS*2, Halbe Reichweite, siehe Ogerfänger','AA 47                                ');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000017','Bolzen','Brandbolzen',8,'Halbe Reichweite, wie stumpfer Bolzen','AA 48                                                      ');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000018','Bolzen','Singender Bolzen',2,'macht Geräusche im Flug, je nach Qualität bis zehnfacher Preis und TP Abzug bis zu 2','AA 48');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000019','Blasrohrpfeil','Blasrohrpfeil',1,'vergiftet','');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000020','Speer','Schleuderspeer',1,'Geschoß für eine Speerschleuder','AA 22');
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur]) VALUES ('00000000-0000-0000-000F-000000000021','Kugel','Bleikugel',1,NULL,'AA 40');

-- Gegnertabellen-Überarbeitung, Bestiarium-Import
DROP TABLE Gegner_Kampfregel
GO
DROP TABLE Gegner_Angriff
GO
DROP TABLE Gegner
GO

CREATE TABLE [GegnerBase] (
	[GegnerBaseGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(100) NOT NULL, 
	[Typ] nvarchar(100), 
	[Bild] nvarchar(500), 
	[INIBasis] int NOT NULL DEFAULT 0, 
	[INIZufall] nvarchar(10) NOT NULL DEFAULT '1W6', 
	[Aktionen] int NOT NULL DEFAULT 2, 
	[PA] int NOT NULL DEFAULT 0, 
	[LE] int NOT NULL DEFAULT 0, 
	[AU] int NOT NULL DEFAULT 0, 
	[AE] int NOT NULL DEFAULT 0, 
	[KE] int NOT NULL DEFAULT 0, 
	[KO] int NOT NULL DEFAULT 0, 
	[MRGeist] int NOT NULL DEFAULT 0, 
	[MRKörper] int, 
	[GS] int NOT NULL DEFAULT 8, 
	[GS2] int, 
	[GS3] int, 
	[RSKopf] int NOT NULL DEFAULT 0, 
	[RSBrust] int NOT NULL DEFAULT 0, 
	[RSRücken] int NOT NULL DEFAULT 0, 
	[RSArmL] int NOT NULL DEFAULT 0, 
	[RSArmR] int NOT NULL DEFAULT 0, 
	[RSBauch] int NOT NULL DEFAULT 0, 
	[RSBeinL] int NOT NULL DEFAULT 0, 
	[RSBeinR] int NOT NULL DEFAULT 0, 
	[GW] int, 
	[Jagd] int, 
	[Beschwörung] int, 
	[Kontrolle] int, 
	[Beschwörungskosten] int, 
	[Tags] nvarchar(1000), 
	[Bemerkung] ntext, 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(100),
	CONSTRAINT [PK_GegnerBase] PRIMARY KEY ([GegnerBaseGUID])
)
GO

CREATE TABLE [Gegner] (
	[GegnerGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[GegnerBaseGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	[Name] nvarchar(100) NOT NULL, 
	[Bild] nvarchar(500), 
	[ATMod] int NOT NULL DEFAULT 0, 
	[INIBasis] int NOT NULL DEFAULT 0, 
	[PA] int NOT NULL DEFAULT 0, 
	[LE] int NOT NULL DEFAULT 0, 
	[AU] int NOT NULL DEFAULT 0, 
	[AE] int NOT NULL DEFAULT 0, 
	[KE] int NOT NULL DEFAULT 0, 
	[KO] int NOT NULL DEFAULT 0, 
	[MRGeist] int NOT NULL DEFAULT 0, 
	[MRKörper] int,
	[RSKopf] int NOT NULL DEFAULT 0, 
	[RSBrust] int NOT NULL DEFAULT 0, 
	[RSRücken] int NOT NULL DEFAULT 0, 
	[RSArmL] int NOT NULL DEFAULT 0, 
	[RSArmR] int NOT NULL DEFAULT 0, 
	[RSBauch] int NOT NULL DEFAULT 0, 
	[RSBeinL] int NOT NULL DEFAULT 0, 
	[RSBeinR] int NOT NULL DEFAULT 0, 
	[LEAktuell] int NOT NULL DEFAULT 0, 
	[AUAktuell] int NOT NULL DEFAULT 0, 
	[AEAktuell] int NOT NULL DEFAULT 0, 
	[KEAktuell] int NOT NULL DEFAULT 0, 
	[Wunden] int NOT NULL DEFAULT 0, 
	[WundenKopf] int NOT NULL DEFAULT 0, 
	[WundenBrust] int NOT NULL DEFAULT 0, 
	[WundenArmL] int NOT NULL DEFAULT 0, 
	[WundenArmR] int NOT NULL DEFAULT 0, 
	[WundenBauch] int NOT NULL DEFAULT 0, 
	[WundenBeinL] int NOT NULL DEFAULT 0, 
	[WundenBeinR] int NOT NULL DEFAULT 0, 
	[Bemerkung] ntext, 
	CONSTRAINT [PK_Gegner] PRIMARY KEY ([GegnerGUID]),
	CONSTRAINT fk_Gegner_GegnerBase FOREIGN KEY ([GegnerBaseGUID])
		REFERENCES GegnerBase ([GegnerBaseGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE [GegnerBase_Angriff] (
	[GegnerBaseGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Name] nvarchar(100) NOT NULL, 
	[TPWürfel] int NOT NULL DEFAULT 6, 
	[TPWürfelAnzahl] int NOT NULL DEFAULT 1, 
	[TPBonus] int NOT NULL DEFAULT 0, 
	[AT] int NOT NULL DEFAULT 0, 
	[DK] nvarchar(4), 
	[Reichweite] int, 
	[Bemerkung] ntext,
	CONSTRAINT [PK_GegnerBase_Angriff] PRIMARY KEY ([GegnerBaseGUID], [Name]), 
	CONSTRAINT fk_GegnerBaseAngriff_GegnerBase FOREIGN KEY ([GegnerBaseGUID])
		REFERENCES GegnerBase ([GegnerBaseGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE [GegnerBase_Kampfregel] (
	[GegnerBaseGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[KampfregelGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Erschwernis] int NOT NULL DEFAULT 0, 
	[TP] nvarchar(10), 
	[Bemerkung] ntext,
	CONSTRAINT [PK_GegnerBase_Kampfregel] PRIMARY KEY ([GegnerBaseGUID], [KampfregelGUID]), 
	CONSTRAINT fk_GegnerBaseKampfregel_GegnerBase FOREIGN KEY ([GegnerbaseGUID])
		REFERENCES GegnerBase ([GegnerBaseGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_GegnerBaseKampfregel_Kampfregel FOREIGN KEY ([KampfregelGUID])
		REFERENCES Kampfregel ([KampfregelGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

ALTER TABLE Bestiarium Add GS2 nvarchar(10) NULL
GO
ALTER TABLE Bestiarium Add GS3 nvarchar(10) NULL
GO

UPDATE Bestiarium SET 
	GS2 = substring(GS, CHARINDEX('/', [GS])+1, 100),
	GS = substring(GS, 0, CHARINDEX('/', [GS]))
WHERE CHARINDEX('/', [GS]) > 0
GO
UPDATE Bestiarium SET 
	GS3 = substring(GS2, CHARINDEX('/', [GS2])+1, 100),
	GS2 = substring(GS2, 0, CHARINDEX('/', [GS2]))
WHERE CHARINDEX('/', [GS2]) > 0
GO

ALTER TABLE Bestiarium Add MRKörper nvarchar(10) NULL
GO

UPDATE Bestiarium SET 
	MRKörper = substring(MR, CHARINDEX('/', [MR])+1, 100),
	MR = substring(MR, 0, CHARINDEX('/', [MR]))
WHERE CHARINDEX('/', [MR]) > 0
GO

INSERT INTO GegnerBase ([Name],[INIBasis],[INIZufall],[PA],[LE],[AU],[KO],[MRGeist],[MRKörper],[GS],[GS2],[GS3],[RSKopf],[RSBrust],[RSRücken],[RSArmL],[RSArmR],[RSBauch],[RSBeinL],[RSBeinR],[Bemerkung],[Literatur],[Setting])

SELECT [Name]
		,CAST(COALESCE([INI_Basis],0) as int)       as INIBasis
		,COALESCE([INI_Zufall], '1W6') as INIZufall
		,CAST(COALESCE([PA], 0) as int)            as PA
		,CAST(COALESCE([LE], 0) as int)            as LE
		,CAST(COALESCE([AU], 0)    as int)         as AU
		,CAST(COALESCE([KO], 0)    as int)         as KO
		,CAST(COALESCE([MR], 0)    as int)         as MRGeist
		,CAST(COALESCE(MRKörper,0) as int)         as MRKörper
		,CAST(COALESCE([GS], 0) as int)            as GS
		,GS2
		,GS3
		,CAST(COALESCE([RSKopf]  ,RS,0) as int)     as RSKopf    
		,CAST(COALESCE([RSBrust] ,RS,0) as int)     as RSBrust   
		,CAST(COALESCE([RSRücken],RS,0) as int)     as RSRücken  
		,CAST(COALESCE([RSArmL]  ,RS,0) as int)     as RSArmL    
		,CAST(COALESCE([RSArmR]  ,RS,0) as int)     as RSArmR    
		,CAST(COALESCE([RSBauch] ,RS,0) as int)     as RSBauch   
		,CAST(COALESCE([RSBeinL] ,RS,0) as int)     as RSBeinL   
		,CAST(COALESCE([RSBeinR] ,RS,0) as int)     as RSBeinR   
		,COALESCE([Kampfwerte] + NCHAR(13),'') + COALESCE([Besonderheiten] + NCHAR(13), '') + COALESCE([Sonderfertigkeiten], '') as Bemerkungen
		,[Quelle]                      as Literatur
		,'Aventurien' as Setting
	FROM [Bestiarium]
GO

DROP TABLE [Bestiarium]
GO