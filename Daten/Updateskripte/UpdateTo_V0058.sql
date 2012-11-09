-- Regel: Ausdauer im Kampf
INSERT INTO Regeln (Name, Anwenden, Typ, Beschreibung) VALUES ('AusdauerImKampf', 1, 'Optional', 'Kampf: Ausdauerverlust (WdS 83)')
GO

-- Miserable Eigenschaft bugfix, Tags für Ausrüstung, Handelsgut Name not null
UPDATE [VorNachteil] set Vorteil=0, Nachteil=1, Typ='Nachteile' where VorNachteilID Between 340 and 347
GO
ALTER TABLE [Ausrüstung] Add Tags ntext NULL
GO
DELETE FROM [Handelsgut]
GO
ALTER TABLE [Handelsgut] alter column Name nvarchar(500) NOT NULL
GO

-- Myranor und Dunkle Zeiten Daten, Munitionstabellenänderungen
ALTER TABLE [Munition] Add Setting nvarchar(100) NULL
GO
ALTER TABLE [Munition] Add Probe int NOT NULL default(0)
GO
ALTER TABLE [Munition] Add Spitze nvarchar(50) NULL
GO
ALTER TABLE [Munition] ALTER COLUMN [Bemerkungen] ntext NULL
GO
ALTER TABLE [Munition] ALTER COLUMN Name nvarchar(100) NOT NULL
GO
ALTER TABLE [Handelsgut] Add Setting nvarchar(100) NULL
GO

DELETE FROM [Munition]
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000001','Schleuderblei','Stein',0,NULL,'AA 21','Aventurien',0,NULL)
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000002','Schleuderblei','Schleuderblei',1,'Fernkampf um 1 erleichtert, TP + 1','AA 21','Aventurien',0,NULL)
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000003','Pfeil','Jagdpfeil',1,'wiederverwendbar','AA 46 / MyA 105','Aventurien, Myranor',0,'Klingenspitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000004','Pfeil','Kriegspfeil',1,'Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 46 / MyA 106','Aventurien, Myranor',2,'Widerhaken-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000005','Pfeil','Gehärteter Kriegspfeil',4,'RS/2, Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 47 / MyA 105, 106','Aventurien, Myranor',4,'Gehärtete Widerhaken-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000006','Pfeil','Kettenbrecher',3,'RS durch Kettengeflecht wird ignoriert, RS von Mischrüstungen (Spiegelpanzer, Baburiner Hut, ...) wird halbiert','AA 47 / MyA 105','Aventurien, Myranor',4,'Kettenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000007','Pfeil','Stumpfer Pfeil',2,'Halbe Reichweite, richtet TP(A) an, Scharfschütze kann einen gezielten Schuß +4 zum niederwerfen machen (KK + TP(A)/2 oder man liegt am Boden)','AA 47 / MyA 106','Aventurien, Myranor',2,'Stumpfe Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000008','Pfeil','Sehnen-Seilschneider',3,'TP+1W6, RS*2, Halbe Reichweite, siehe Ogerfänger','AA 47 / MyA 106','Aventurien, Myranor',4,'Mondsichel-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000009','Pfeil','Brandpfeil',8,'Halbe Reichweite, wie stumpfer Pfeil','AA 48 / MyA 106','Aventurien, Myranor',4,'Brandkorbspitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000010','Pfeil','Singender Pfeil',2,'macht Geräusche im Flug, je nach Qualität bis zehnfacher Preis und TP Abzug bis zu 2','AA 48','Aventurien',4,NULL)
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000011','Bolzen','Jagdbolzen',1,'wiederverwendbar','AA 46 / MyA 105','Aventurien, Myranor',0,'Klingenspitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000012','Bolzen','Kriegsbolzen',1,'Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 46 / MyA 106','Aventurien, Myranor',2,'Widerhaken-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000013','Bolzen','Gehärteter Kriegsbolzen',4,'RS/2, Bei Wunde: pro KR 1W6 SP(A) oder bei Bewegung 1W6 SP, siehe Ogerfänger','AA 47 / MyA 105, 106','Aventurien, Myranor',4,'Gehärtete Widerhaken-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000014','Bolzen','Kettenbrecher',3,'RS durch Kettengeflecht wird ignoriert, RS von Mischrüstungen (Spiegelpanzer, Baburiner Hut, ...) wird halbiert','AA 47 / MyA 105','Aventurien, Myranor',4,'Kettenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000015','Bolzen','Stumpfer Bolzen',2,'Halbe Reichweite, richtet TP(A) an, Scharfschütze kann einen gezielten Schuß +4 zum niederwerfen machen (KK + TP(A)/2 oder man liegt am Boden)','AA 47 / MyA 106','Aventurien, Myranor',2,'Stumpfe Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000016','Bolzen','Sehnen-Seilschneider',3,'TP+1W6, RS*2, Halbe Reichweite, siehe Ogerfänger','AA 47 / MyA 106','Aventurien, Myranor',4,'Mondsichel-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000017','Bolzen','Brandbolzen',8,'Halbe Reichweite, wie stumpfer Bolzen','AA 48 / MyA 106','Aventurien, Myranor',4,'Brandkorb-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000018','Bolzen','Singender Bolzen',2,'macht Geräusche im Flug, je nach Qualität bis zehnfacher Preis und TP Abzug bis zu 2','AA 48','Aventurien',4,NULL)
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000019','Blasrohrpfeil','Blasrohrpfeil',1,'vergiftet','','Aventurien',0,NULL)
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000020','Speer','Schleuderspeer',1,'Geschoß für eine Speerschleuder','AA 22','Aventurien',0,NULL)
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000021','Kugel','Bleikugel',1,NULL,'AA 40','Aventurien',0,NULL)
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000022','Pfeil','Plattenstecher',4,'RS aus allen Formen von künstlichen Rüstungen wird halbiert. Natürlicher RS wirkt normal.','MyA 105','Myranor',4,'Plattenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000023','Pfeil','Gehärteter Kettenbrecher',12,'Durch Kettengeflecht und Stoff gelieferter RS kann ignoriert werden. Der RS aus Mischrüstungen und Lederrüstungen wird geviertelt. Auflistung siehe Arsenal. Jeglicher sonstige RS des Opfers wird halbiert.','MyA 105','Myranor',4,'Gehärtete Kettenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000024','Pfeil','Gehärteter Plattenstecher',16,'RS aus allen Formen von künstlichen Rüstungen wird geviertelt. Natürlicher RS wird halbiert.','MyA 105','Myranor',4,'Gehärtete Plattenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000025','Bolzen','Plattenstecher',4,'RS aus allen Formen von künstlichen Rüstungen wird halbiert. Natürlicher RS wirkt normal.','MyA 105','Myranor',4,'Plattenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000026','Pfeil','Gehärteter Jagdpfeil',5,'wiederverwendbar, RS des Opfers wird halbiert.','MyA 105','Myranor',4,'Gehärtete Klingenspitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000027','Pfeil','Gehärteter Sehnen-Seilschneider',12,'Reichweite wird halbiert. Über Probeaufschläge auf Seile entscheidet der Spielleiter anhand der genauen Situation, ebenso über Auswirkungen, die ein durchtrenntes Seil hat. Bei Einsatz auf Lebewesen verursacht die Spitze 1W6 TP zusätzlich.','MyA 105, 106','Myranor',4,'Gehärtete Mondsichel-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000028','Bolzen','Gehärteter Kettenbrecher',12,'Durch Kettengeflecht und Stoff gelieferter RS kann ignoriert werden. Der RS aus Mischrüstungen und Lederrüstungen wird geviertelt. Auflistung siehe Arsenal. Jeglicher sonstige RS des Opfers wird halbiert.','MyA 105','Myranor',4,'Gehärtete Kettenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000029','Bolzen','Gehärteter Plattenstecher',16,'RS aus allen Formen von künstlichen Rüstungen wird geviertelt. Natürlicher RS wird halbiert.','MyA 105','Myranor',4,'Gehärtete Plattenstecher-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000030','Bolzen','Gehärteter Sehnen-Seilschneider',12,'Reichweite wird halbiert. Über Probeaufschläge auf Seile entscheidet der Spielleiter anhand der genauen Situation, ebenso über Auswirkungen, die ein durchtrenntes Seil hat. Bei Einsatz auf Lebewesen verursacht die Spitze 1W6 TP zusätzlich.','MyA 105, 106','Myranor',4,'Gehärtete Mondsichel-Spitze')
GO
INSERT INTO [Munition] ([MunitionGUID],[Art],[Name],[Preismodifikator],[Bemerkungen],[Literatur],[Setting],[Probe],[Spitze]) VALUES ('00000000-0000-0000-000f-000000000031','Bolzen','Gehärteter Jagdbolzen',4,'wiederverwendbar, RS des Opfers wird halbiert.','MyA 105','Myranor',4,'Gehärtete Klingenspitze')
GO

Update [Ausrüstung] set Setting = 'Aventurien' WHERE Setting is null
GO
Update [Ausrüstung] set Literatur = Replace(Literatur,' , ',', ') WHERE Literatur like '% , %'
GO

/* Audio Tabellenänderungen */
ALTER TABLE [Audio_Playlist_Titel] ADD Speed float NOT NULL DEFAULT 1
GO
ALTER TABLE [Audio_Playlist_Titel] ADD TeilAbspielen bit NOT NULL DEFAULT 0
GO
ALTER TABLE [Audio_Playlist_Titel] ADD TeilStart float NULL
GO
ALTER TABLE [Audio_Playlist_Titel] ADD TeilEnde float NULL
GO

CREATE TABLE [Audio_Theme] (
	[Audio_ThemeGUID] uniqueidentifier NOT NULL ROWGUIDCOL DEFAULT newid(),
	[Name] nvarchar(100) NOT NULL,
	CONSTRAINT [PK_Audio_Theme] PRIMARY KEY ([Audio_ThemeGUID])
)
GO
CREATE TABLE [Audio_Theme_Playlist] (
	[Audio_ThemeGUID] uniqueidentifier NOT NULL default '00000000-0000-0000-0000-000000000000',
	[Audio_PlaylistGUID] uniqueidentifier NOT NULL default '00000000-0000-0000-0000-000000000000',
	CONSTRAINT [PK_Audio_Theme_Playlist] PRIMARY KEY ([Audio_ThemeGUID], [Audio_PlaylistGUID]),
	CONSTRAINT fk_Audio_Theme_Playlist_Playlist FOREIGN KEY ([Audio_PlaylistGUID])
		REFERENCES Audio_Playlist ([Audio_PlaylistGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO