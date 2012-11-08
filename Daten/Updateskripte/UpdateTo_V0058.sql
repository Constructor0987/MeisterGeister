-- Regel: Ausdauer im Kampf
INSERT INTO Regeln (Name, Anwenden, Typ, Beschreibung) VALUES ('AusdauerImKampf', 1, 'Optional', 'Kampf: Ausdauerverlust (WdS 83)');

-- Miserable Eigenschaft bugfix, Tags für Ausrüstung, Handelsgut Name not null
UPDATE [VorNachteil] set Vorteil=0, Nachteil=1, Typ='Nachteile' where VorNachteilID Between 340 and 347;
ALTER TABLE [Ausrüstung] Add Tags ntext NULL;
DELETE FROM [Handelsgut];
ALTER TABLE [Handelsgut] alter column Name nvarchar(500) NOT NULL;

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

/* Talent */

INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Bela' ,7 ,NULL ,NULL ,NULL ,N'Spezial' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor' ,N'Myranor (HC) 223, 224')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Feuerwaffen' ,7 ,NULL ,NULL ,NULL ,N'Spezial' ,N'BE-5' ,NULL ,NULL ,N'C' ,NULL ,NULL ,N'Myranor' ,N'MyA 110')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Kettenkugel' ,7 ,NULL ,NULL ,NULL ,N'Spezial' ,N'Rakshazar 28' ,N'-2' ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (55 Zeichen von Amhas)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (88 Zeichen des Sahihlam)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (AhMaGao)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Alte Tesumurrische Silbenzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Alt-Imperiale-Glyphen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Alt-Marhym)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Alt-Nakramarische Bilderschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Alt-Neristale Bilderschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Alt-Vesayitisch)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Angramm)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Ashdaria)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Bramschoromk oder Baramun-Keilschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Cromwyn-Runenschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Dakrac)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Drayadalanische Schriftzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Eupherban-Codec)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Früh-Imperiale Bildzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Gemein-Vesayitisch)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Grolmurische Silberzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Heilige Glyphen von Lamerinaxo)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Hjaldingsche Runenzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Imperiale Lautzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Irrogolosh Glyphen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Isdar-Rakshe)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Kalshinish oder Shingwanische Knotenschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Kerrishitische Silbenzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Mahapratische Silbenzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Mananesh-Glyphen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Marham)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Narkramarische Silbenzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Nedram Runen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Nuoekaeshal)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Ouipu)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Rogari Glyphen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Sanskische Haken)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Torruksch Keilschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Ur-Tulamydia)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Ur-Vesayitisch)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Uthrurim)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Vaestische Linienschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Vesayitische Wort- und Silbenzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Vesayo)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Vesayo-Silbenzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Lesen/Schreiben (Xah''Stsiva)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'Rakshazar 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Pelura' ,3 ,N'GE' ,N'FF' ,N'FF' ,N'Spezial' ,N'Das Königreich Almada 84' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Abishat)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Broktharisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 30' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Imperial oder Alt-Güldenländisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Marhym)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Narkramarisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Neristal)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Orkisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Ramsharij)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Tesumurrisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Alt-Xhalori)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 33' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Amhasa)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 30' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Angramm)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 30' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Angurak)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 33' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Asharielitisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Ashdaria)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Bansumitisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Bashurisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Boa''goram oder Banbarguinisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Bramscho)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Broktharisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 30' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Cirkel-Geheimsprache)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Cromwyn)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Dakrac)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 33' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Drayadalanisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Erasumisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Eupherban-Haussprache)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Früh-Imperial)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Gemein-Amaunal oder AhMa)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Gemein-Imperial)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Gmer)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Goragari)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 30' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Grolmurisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Hiero-Amaunal oder AhMaGao)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Hiero-Imperial)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Ipexco)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Isdar-Rakshi)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Jiktisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Kawash)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 30' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Kentorisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Kerrishitisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Leonal oder Khorrzu)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Loualilisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Lyncal oder Fhi''Ai)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Mahapratisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Marham)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Myranisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Narkramarisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Nederi)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Nedermannisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Neristal)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Olura)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Padir oder Bhagrach)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Parnhal)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 32' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Porto-Imperial)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Raversaran)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Rogorakshi)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 30' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Sanskitarisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Shingwanisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Shinoq)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Slachkarisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Sycc)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 33' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Tchif)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 33' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Truag)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Ur-Tulamydia)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Uthurim)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Uzujuma)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 34' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Vaestisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 31' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Vinshinisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 212' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Xah''Hombri)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Rakshazar 33' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Rakshazar')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprachen Kennen (Yachyach)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'Myranor (HC) 211' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor')
GO
INSERT INTO [Talent] (  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES (N'Sprühwaffen' ,7 ,NULL ,NULL ,NULL ,N'Spezial' ,N'Rakshazar 28' ,N'-3' ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Rakshazar')
GO
UPDATE [Talent] SET [Setting] = N'Aventurien, Rakshazar' ,[Literatur] = N'WdS 30-32 / Rakshazar 34' WHERE [Talentname]=N'Lesen/Schreiben (Trollische Raumbilderschrift)'
GO
UPDATE [Talent] SET [Literatur] = N'WdS 30-32 / Myranor (HC) 211' WHERE [Talentname]=N'Sprachen Kennen (Hjaldingsch)'
GO
UPDATE [Talent] SET [Setting] = N'Myranor' ,[Literatur] = N'WdS 30-32 / Myranor (HC) 212' WHERE [Talentname]=N'Sprachen Kennen (Rissoal)'
GO


/* Ausrüstung */

INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000151' ,N'Abendstern' ,20 ,140 ,N'(12) DEME, GATH, HARP' ,N'MyA 22' ,N'Myranor' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. Patzer bei 19-20. Krontrollwurf um 2 erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000152' ,N'Akinax' ,15 ,30 ,N'(1) als Sammlerstück; sonst nur in Ailish-Merkhaba erhältlich' ,N'MyA 22, 23' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000153' ,N'Albenhammer' ,200 ,90 ,N'(1) als Sammlerstück; sonst nur in Ailish-Merkhaba erhältlich' ,N'MyA 23' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Manöver Stumpfer Schlag und Betäubungsschlag sind um 2 Punkte erleichtert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000154' ,N'Albensäbel' ,200 ,70 ,N'(1) als Sammlerstück; sonst nur in Ailish-Merkhaba erhältlich' ,N'MyA 24' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Weitere Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000155' ,N'Ameisenstachtel' ,5 ,10 ,N'(12) ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ESHB, GATH, GYLD, HARP, KARO, KORO, MAYE, MdSI, NARI, NARK, OCHO, SERO, SHIN, VALA' ,N'MyA 52, Errata 1' ,N'Myranor' ,N'Der Verweis auf das Stoßmesser bezieht sich auf den Myrmex. Ein Stoßmesser existiert nicht.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000156' ,N'Ashariel-Lanze' ,120 ,80 ,N'(5) ALA, CENT, CORA, XARX' ,N'MyA 24' ,N'Myranor' ,N'Z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000157' ,N'Baculus' ,60 ,180 ,N'(6) CANT, CENT, CORA, GATH, HARP, MAYE, NARI, OCHO, VALA' ,N'MyA 25' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Weitere Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000158' ,N'Balisong' ,4 ,12 ,N'(14) SHIN, THAL' ,N'MyA 25' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. Das Ziehen eines Balisong erfordert eine Aktion, das Öffenen eine weitere (bei gelungener FF-Probe nur eine freie Aktion.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000159' ,N'Barbaren-Schwertlanze' ,120 ,220 ,N'(12) CRAN, HALD' ,N'MyA 26, Errata 1' ,N'Myranor' ,N'kann von Dolchen und Fechtwaffen nicht pariert werden. Der Satz bezüglich der einhändigen Führung entfällt.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000160' ,N'Beil' ,70 ,50 ,N'(18) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, RHAC, SERO, SEVA, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 26, Errata 1' ,N'Myranor' ,N'wi')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000161' ,N'Caprodoros' ,50 ,150 ,N'(12) CANT, CENT, CORA, DEME, GATH, GYLD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, SEVA, VALA, XARX' ,N'MyA 27' ,N'Myranor' ,N'Z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000162' ,N'Cestus' ,5 ,20 ,N'(14) ANTH, CANT, CENT; CORA, CRAN, DEME; GATH, GYLD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, RHAC, SEVA, THAR, VALA, XARX' ,N'MyA 27, Errata 1' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000163' ,N'Cestus mit Dornen' ,5 ,20 ,N'(14) ANTH, CANT, CENT; CORA, CRAN, DEME; GATH, GYLD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, RHAC, SEVA, THAR, VALA, XARX' ,N'MyA 27' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000164' ,N'Chak' ,12 ,25 ,N'(10) MdSI' ,N'MyA 28, Errata 1' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. Das Ziehen eines Balisong erfordert eine Aktion, das Öffenen eine weitere (bei gelungener FF-Probe nur eine freie Aktion. Errata: 1')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000165' ,N'Chereb' ,60 ,85 ,N'(15) TALA' ,N'MyA 28' ,N'Myranor' ,N'Vom Reittier gegen Fußkämpfer richtet die Waffe 2 TP mehr an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000166' ,N'Chrattac-Krummschwert' ,60 ,70 ,N'(3) ANTH, KORO' ,N'MyA 29, Errata 1' ,N'Myranor' ,N'Regelung siehe Arsenal. Nicht-Chrattac können die Waffe führen, ihre Attacken und Paraden sind in diesem Fall jedoch um jeweils 3 Punkte erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000167' ,N'Chrattac-Speer' ,180 ,180 ,N'(3) ANTH, KORO' ,N'MyA 29, Errata 1' ,N'Myranor' ,N'Regelung siehe Arsenal. Nicht-Chrattac können die Waffe führen, ihre Attacken und Paraden sind in diesem Fall jedoch um jeweils 3 Punkte erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000168' ,N'Chu''Ur' ,85 ,120 ,N'(10) BANB' ,N'MyA 30' ,N'Myranor' ,N'Vom Reittier gegen Fußkämpfer richtet die Waffe 2 TP mehr an. Kann von Dolchen und Fechtwaffen nicht pariert werden. Weitere Regelung siehe Arsenal. Errata: 1 Bei einhändiger Führung mit dem Talent Hiebwaffen ändert sich lediglich die TP/KK, hierfür ist außerdem eine KK von 15 erforderlich.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000169' ,N'Dolch' ,10 ,20 ,N'(18) ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, SERO, SEVA, SHIN, TALA,  VALA' ,N'MyA 30' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000170' ,N'Doppelter-Abendstern' ,35 ,240 ,N'(10) GATH, HARP' ,N'MyA 31' ,N'Myranor' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. Patzer bei 19-20. Krontrollwurf um 2 erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000171' ,N'Doros' ,28 ,80 ,N'(12) CANT, CENT, CORA, DEME, GATH, GYLD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, VALA' ,N'MyA 31, Errata 1' ,N'Myranor' ,N'Regelung siehe Arsenal. Auch bei einhändiger Führung ist die Distanzklasse S.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000172' ,N'Dreach' ,85 ,110 ,N'(12) BANB' ,N'MyA 32' ,N'Myranor' ,N'Z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000173' ,N'Dreifacher-Abendstern' ,50 ,300 ,N'(8) GATH, HARP' ,N'MyA 32' ,N'Myranor' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. Patzer bei 19-20. Krontrollwurf um 2 erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000174' ,N'Dreiklingen-Katar' ,20 ,20 ,N'(3) MAKS' ,N'MyA 32, 33, Errata 1' ,N'Myranor' ,N'Regelung siehe Arsenal. Auch im Falle eines Bauchtreffers wird lediglich dann eine weitere Wunde angerichtet, wenn der Angreifer die Seitenklingen herausspringen lässt.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000175' ,N'Dreizack' ,10 ,90 ,N'(8) ANTH, CANT, CENT, CORA, CRAN, DEME, ESHB, GATH, GYL, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 33, Errata 1' ,N'Myranor' ,N'i')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000176' ,N'Drepani' ,40 ,60 ,N'(10) ANTH, CANT, CENT, CORA, GATH, Gyld, HARP, KORO, MAYE, NARI, OCHO, RHAC, SERO, THAR, VALA' ,N'MyA 33, 34, Errata 1' ,N'Myranor' ,N'Preis je Stück, kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. Weitere Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000177' ,N'Dreschfelgel' ,3 ,100 ,N'(18) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, RHAC, SERO, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 34' ,N'Myranor' ,N'kann von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000178' ,N'Dur Lamach' ,70 ,140 ,N'(9) ESHB' ,N'MyA 34' ,N'Myranor' ,N'Z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000179' ,N'Ekemon' ,35 ,200 ,N'(6) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XRAX' ,N'MyA 35' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000180' ,N'Enterbeil' ,0 ,30 ,N'(12) SERO, THAL' ,N'MyA 35' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Weitere Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000181' ,N'Fackel' ,15 ,40 ,N'(18) überall' ,N'MyA 35, Errata 1' ,N'Myranor' ,N'Richtet der Schlag SP an, kommen noch 1W-1 SP Feuerschaden hinzu! Fällt eine 6, so erlischt die Fackel.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000182' ,N'Falcata' ,20 ,40 ,N'(14) CENT, KARO, OCHO' ,N'MyA 36' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Weitere Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000183' ,N'Fangeisen' ,40 ,200 ,NULL ,N'MyA 138' ,N'Myranor' ,N'Das opfer kann ohne weiteres auf Abstand gehalten werden. Die GE des Opfers ist dann um 8 vermindert. Will das Opfer sich mit Gewalt befreien erleidet es (je nach Bösartigkeit der Konstruktion) 1W4 bis 2W4 TP und automatisch je eine Wunde pro KO/2 SP in der Zone Kopf. Im Verlauf von 30-FF Aktionen kann man sich jedoch ohne Probe langsam selbst befreien ohne Schaden zu nehmen.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000184' ,N'Fischforke' ,18 ,70 ,N'(8) MdSI, THAL' ,N'MyA 36' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000185' ,N'Friedenstifter' ,10 ,70 ,N'(18) ANTH, CANT, CENT, CORA, CRAN, DEME, GAHT, GYLD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, RHAC, SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 37' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000186' ,N'Garmesh' ,14 ,30 ,N'(14) ESHB, MAKS, NARK, SHIN, TALA, TESU, THAR' ,N'MyA 37, Errata 1' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000187' ,N'Gutnacht' ,20 ,180 ,N'(10) GATH' ,N'MyA 38' ,N'Myranor' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000188' ,N'Haifänger' ,32 ,20 ,N'(12) CANT, MdSI, Thal' ,N'MyA 38, Errata 1' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. Optional gilt: WM 0/-1 über Wasser und WM 1/0 unter Wasser')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000189' ,N'Haumesser' ,8 ,90 ,N'(16) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP' ,N'MyA 39' ,N'Myranor' ,N'i')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000190' ,N'Hellebarde' ,80 ,180 ,N'(12) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SEVA, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 40' ,N'Myranor' ,N'Schläge können mit Dolchen und Fechtwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000191' ,N'Holzfälleraxt' ,18 ,160 ,N'(12) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SEVA, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 40' ,N'Myranor' ,N'Schläge können mit Dolchen und Fechtwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000192' ,N'Holzspeer' ,2 ,60 ,N'(18) überall' ,N'MyA 40' ,N'Myranor' ,N'zw')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000193' ,N'Hornissenstachel' ,35 ,80 ,N'(6) CANT, CENT, CORA, CRAN, GYLD, JALD, HARP, MAYE, NARI, OCHO, THAR, VALA' ,N'MyA 40, 41' ,N'Myranor' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000194' ,N'Jagdmesser' ,10 ,15 ,N'(12) ALAM, ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, LORT, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SEVA, STAK, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 41' ,N'Myranor' ,N'i')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000195' ,N'Jagdspieß' ,30 ,80 ,N'(12) ALAM, ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, LORT, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SEVA, STAK, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 41, Errata 1' ,N'Myranor' ,N'(w)z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000196' ,N'Kampfstab' ,20 ,80 ,N'(8) ANTH, CANT, CENT, CORA, CRAN, DEME, ESHB, GATH, GYLD, HARP, KARO, KORO, MAKS, MAYE, NARI, NARK, OCHO, SERO, SHIN, TALA, THAR, VALA' ,N'MyA 42, Errata 1' ,N'Myranor' ,N'Das Entwaffnen aus der AT ist um 2 Punkte erleichtert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000197' ,N'Karmak' ,20 ,80 ,N'(16) TALA' ,N'MyA 42, Errata 1' ,N'Myranor' ,N'w. Im Falle einer einhändigen Führung betragen die TP 1W63 und der Waffenmodifi kator beläuft sich auf 0/-5.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000198' ,N'Karsh' ,20 ,20 ,N'(10) MAKS, THAR' ,N'MyA 43' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000199' ,N'Katar' ,20 ,20 ,N'(10) MAKS, THAR' ,N'MyA 43, Errata 1' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. Man kann den RS des Gegners ignorieren. Der sich aus dem RS des Gegners ergebende Probenzuschlag kann nicht ignoriert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000200' ,N'Katzenkralle' ,50 ,40 ,N'(6) ANTH, CANT, CENT, CORA, GYLD, MAKS, NARI, OCHO, SHIN, THAR, VALA' ,N'MyA 43, 44' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000201' ,N'Kentema' ,65 ,100 ,N'(12) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 44, 45, Errata 1' ,N'Myranor' ,N'"Regelung siehe Arsenal. Hinter den TP der per Waffenmeister geworfenen
Kentema fehlt ein Sternchen."')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000202' ,N'Keule' ,3 ,100 ,N'(18) überall' ,N'MyA 45' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000203' ,N'Khopesh' ,50 ,80 ,N'(6) TESU' ,N'MyA 45, 46' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000204' ,N'Khorrpranke' ,120 ,200 ,N'(12) KHAM; RHAC' ,N'MyA 46' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Weitere Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000205' ,N'Kleidersaum mit Giftklingen' ,40 ,10 ,NULL ,N'MyA 139' ,N'Myranor' ,N'Um Gegner zu treffen ist eine Peitschen AT 4 notwendig.  Damit das Gift wirkt muss wenigstens 1 SP angerichtet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000206' ,N'Knochenspeer' ,8 ,40 ,N'(10) überall' ,N'MyA 47, Errata 1' ,N'Myranor' ,N'Regelung siehe Arsenal. Bei einhändiger Führung beträgt der WM 0/-4.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000207' ,N'Knüppel' ,1 ,60 ,N'(18) überall' ,N'MyA 47, Errata 1' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000208' ,N'Kriegsbeil' ,28 ,120 ,N'(12) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, GATH, GYLD, HALD, HARP, KARO, KORO, MAKS, MAYE, NARI, NARK, OCHO, SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 47, Errata 1' ,N'Myranor' ,N'Der Satz bezüglich der Stein- und Knochenbeile wird gestrichen, da für diese ein eigener Eintrag vorhanden ist.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000209' ,N'Kriegsflegel' ,50 ,120 ,N'(8) CENT, CORA, CRAN, GATH, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, RHAC, SERO, VALA' ,N'MyA 48' ,N'Myranor' ,N'Schläge können mit Dolchen und Fechtwaffen nicht parriert werden, ignoriert den Verteidigungsbonus von Schilden')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000210' ,N'Kurzschwert' ,24 ,40 ,N'(16) ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HARP, KARO; KORO, MAKS, MAYE, NARI, OCHO; SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 48, Errata 1' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000211' ,N'Labrys' ,90 ,250 ,N'(8) OCHO' ,N'MyA 49, Errata 1' ,N'Myranor' ,N'Regelung siehe Arsenal. Der Satz bezüglich der einhändigen Führung entfällt.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000212' ,N'Lange Schwertlanze' ,100 ,180 ,N'(14) CORA, Gyld' ,N'MyA 49' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000213' ,N'Lev''thas-Klinge' ,60 ,60 ,N'(5) ERAS, MAYE' ,N'MyA 49, 50' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000214' ,N'Lyncil-Waldaxt' ,80 ,130 ,N'(12) LORT, SEVA, STAK (nur bei Lyncil)' ,N'MyA 50, Errata 1,2' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden. Die angegebenen Werte gelten für die Verwendung als Zweihand-Hiebwaffe, der unter den Besonderheiten aufgeführte Bonus entfällt daher. Die TP/KK bei einhändiger Führung beträgt 15/3, zudem ist dafür eine KK von 15 erforderlich und das Talent Hiebwaffen kommt zur Anwendung.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000215' ,N'Lyncil-Wildnismesser' ,60 ,40 ,N'(12) LORT, SEVA, STAK (nur bei Lyncil)' ,N'MyA 50, 51, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000216' ,N'Machira' ,22 ,35 ,N'(15) CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, KARO, LAKI, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 51, Errata 2' ,N'Myranor' ,N'Der sich aus dem RS des Gegners ergebende Probenzuschlag kann nicht ignoriert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000217' ,N'Messer' ,2 ,10 ,N'(18) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARK, OCHO, RHAC, SERO, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 52' ,N'Myranor' ,N'i')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000218' ,N'Myrmex' ,5 ,10 ,N'(12) ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ESHB, GATH, GYLD, HARP, KARO, KORO, MAYE, MdSI, NARI, NARK, OCHO, SERO, SHIN, VALA' ,N'MyA 52' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000219' ,N'Namabhum' ,120 ,500 ,N'(2) LORT, OCHO, SEVA' ,N'MyA 52, 53, Errata 2' ,N'Myranor' ,N'Richtet automatisch eine Wunde an sobald er mehr als KO/2-2 erzeugt. Weitere Reglung siehe Arsenal. Die nach dem ersten Satz der Besonderheiten aufgeführten Punkte werden gestrichen.Ab einer KK von 23 kann sich jedoch auch einhändig mit dem Talent Hiebwaffen und einem WM von 23/2 verwendet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000220' ,N'Neristustab' ,30 ,70 ,N'(12) KORO sowie in imperialen Nerenithya' ,N'MyA 53' ,N'Myranor' ,N'Entwaffnen aus der AT ist um 2 Punkte erleichtert. Weitere Reglung siehe Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000221' ,N'Neristustab mit Derpamiklingen' ,30 ,70 ,N'(12) KORO sowie in imperialen Nerenithya' ,N'MyA 53' ,N'Myranor' ,N'Derpamiklingen')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000222' ,N'Nordland Wurfaxt' ,15 ,60 ,N'(8) HALD, LORT, NYRA, OCHO, SESK, SEVA, STAK' ,N'MyA 91' ,N'Myranor' ,N'w')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000223' ,N'Pacator' ,10 ,70 ,N'(18) ANTH, CANT, CENT, CORA, CRAN, DEME, GAHT, GYLD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, RHAC, SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 54' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000224' ,N'Padir Speer' ,14 ,75 ,N'(4) ANTH, CANT, KORO, MAKS, NEBE, PARD, THAR' ,N'MyA 54, Errata 2' ,N'Myranor' ,N'Z. TP/KK bei einhändiger Führung beträgt 14/5.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000225' ,N'Panzerarm' ,5 ,10 ,N'(10) ANTH, CANT, CENT, CORA, CRAN, DEME,  GATH, GYLD, HARP, KARO, KORO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 54' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000226' ,N'Pata' ,70 ,120 ,N'(12) MAKS' ,N'MyA 55' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Gezielter Stich und Todesstoß sind um 1 erleichtert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000227' ,N'Peitsche' ,5 ,60 ,N'(10) ANTH, CANT, CENT, CORA, CRAN, DEME,  ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 55, Errata 2' ,N'Myranor' ,N'In der DK Nahkampf ein AT-Malus von 8, im DK Handgemenge nicht zu verwenden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000228' ,N'Phasganon' ,55 ,75 ,N'(10) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARi, OCHO, THAR, VALA, XARX' ,N'MyA 56' ,N'Myranor' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Weitere Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000229' ,N'Phugion' ,12 ,15 ,N'(16) CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, HARP, KARO, LAKI, MAYE, NARI, OCHO, THAR, VALA, Xarx' ,N'MyA 56, 57' ,N'Myranor' ,N'Schläge können mit Dolchen und Fechtwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000230' ,N'Reiteraxt' ,70 ,160 ,N'(6) ANTH, GATH, KENT, KORO, MAYE' ,N'MyA 57, Errata 2' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden. Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an, dort gilt die DK N und S. Bei einhändiger Führung betragen die TP weiterhin 2W61, auch INI und WM bleiben unverändert, allerdings setzt dies eine KK von 15 voraus. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000231' ,N'Rhomphaia' ,120 ,120 ,N'(10) CENT, GATH, HARP, KARO, OCHO' ,N'MyA 57, 58, Errata 2' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000232' ,N'Risso-Kriegsdolch' ,60 ,30 ,N'(8) CRAN, MdSI, SERO, THAL' ,N'MyA 58, Errata 2' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.  Nicht-Risso erleiden einen Malus von -1/-1.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000233' ,N'Rondrflamme' ,150 ,150 ,N'(4) RHAC' ,N'MyA 59, 60' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden. Weitere Reglung siehe Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000234' ,N'Rossmetzger' ,35 ,200 ,N'(6) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XRAX' ,N'MyA 59, Errata 2' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000235' ,N'Ruakar' ,45 ,150 ,N'(12) ESHB' ,N'MyA 60' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000236' ,N'Sa''Arka' ,0 ,90 ,NULL ,N'MyA 73' ,N'Myranor' ,N'Z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000237' ,N'Säbel' ,25 ,60 ,N'(9) ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 61' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet ein Säbel 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000238' ,N'Sagaris' ,30 ,90 ,N'(4) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 61, Errata 2' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet ein Säbel 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000239' ,N'Schlagring' ,5 ,20 ,N'(16) ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HARP, KARO; KORO, MAKS, MAYE, NARI, OCHO; SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 62' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000240' ,N'Schlangenzunge' ,50 ,20 ,N'SHIN, unverkäuflich' ,N'MyA 62, Errata 2' ,N'Myranor' ,N'Schläge können mit Dolchen und Fechtwaffen nicht parriert werden. Rüstungen schützen in normalem Umfang.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000241' ,N'Schleuderstab' ,0 ,60 ,N'(12) ANTH, DEME, ESHB, GATH, KENT, KHAM, KORO, MAKS, MAYE, NARK, RHAC, SESK' ,N'MyA 94' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000242' ,N'Schmiedehammer' ,10 ,100 ,N'(12) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SHIN, STAK, TALA, TESU, THAR, VALA, XARX' ,N'MyA 63' ,N'Myranor' ,N'i')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000243' ,N'Schwanenhals' ,35 ,60 ,N'(10) EHSB, NARK' ,N'MyA 63, Errata 2' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an. Nicht der Tod von Links wird dank der Waffenmeisterschaft erleichtert, sondern der Mehrfachangriff.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000244' ,N'Schwanzwaffe' ,30 ,15 ,N'(8) ANTH, CANT, CENT, CORA, CRAN, GATH, GYLD, HALD, HARP, KARO, KHAM, KORO, MAKS, MAYE, MdSI, NARI, NEBE, NYRA, OCHO, PARD, RHAC, SERO, SEVA, TESU, THAL, THAR, VALA' ,N'MyA 63' ,N'Myranor' ,N'erzeugt echte TP')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000245' ,N'Schwert' ,45 ,80 ,N'(4) CORA, CRAN, ESHB, GATH, GYLD, HALD, MAYE, SERO' ,N'MyA 64' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000246' ,N'Schwertlanze' ,75 ,150 ,N'(10) ANTH, CENT, DEME, ESHB, GATH, GYLD, HARP, KARO, KENT, KHAM, KORO, MAYE, NARK, RHAC, SERO, TESU' ,N'MyA 64' ,N'Myranor' ,N'Kann von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000247' ,N'Schwertstab' ,80 ,120 ,N'(4) CANT, CENT, ESHB, GYLD, HALD, HARP, MAYE, NARI, OCHO, SERO, THAR, VALA' ,N'MyA 64, 65' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000248' ,N'Schwertstab mit Sprungfeder' ,100 ,120 ,N'(3) CENT, CORA, GYLD, HARP' ,N'MyA 65' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000249' ,N'Sense' ,6 ,100 ,N'(14) ANTH, CANT, CENT; CORA, CRAN, DEME; ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARC, OCHO, RHAC, SERO, SHIN, TALA, TESU,  THAR, VALA, XARX' ,N'MyA 65' ,N'Myranor' ,N'iz')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000250' ,N'Servormesser' ,18 ,80 ,N'(7) ANTH, DEME, GATH, KHAM, LAKI' ,N'MyA 66, Errata 2' ,N'Myranor' ,N'Schläge können mit Dolchen und Fechtwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000251' ,N'Shinoqi-Hammer' ,45 ,180 ,NULL ,N'MyA 66' ,N'Myranor' ,N'Schläge können mit Dolchen und Fechtwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000252' ,N'Shogai (als Peitsche)' ,40 ,110 ,N'(3) CANT, MdSI, THAR' ,N'MyA 66, 67, Errata 2' ,N'Myranor' ,N'Regelung siehe Arsenal. Die Waffe ist mit dem Talent Dolche auch in der Distanzklasse H zu führen. Der erste Absatz der Besonderheiten entfällt, stattdessen verwendet die Waffe die normalen Regeln der Peitsche.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000253' ,N'Shogai (als Dolch)' ,40 ,110 ,N'(3) CANT, MdSI, THAR' ,N'MyA 66, 67, Errata 2' ,N'Myranor' ,N'Regelung siehe Arsenal. Die Waffe ist mit dem Talent Dolche auch in der Distanzklasse H zu führen. Der erste Absatz der Besonderheiten entfällt, stattdessen verwendet die Waffe die normalen Regeln der Peitsche.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000254' ,N'Sichel' ,5 ,30 ,N'(14) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SHIN, TALA, TESU, THAR, CALA, XARX' ,N'MyA 67' ,N'Myranor' ,N'i')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000255' ,N'Sintikon' ,120 ,15 ,N'(3) MdsI' ,N'MyA 67, Errata 2' ,N'Myranor' ,N'Damit die Giftwirkung gilt muss min. 1 SP gemacht werden')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000256' ,N'Skorpionsstachel' ,45 ,120 ,N'(5) ANTH, CANT, CENT, CORA, GAHT, GYLD, HARP, MAYE, NARI, OCHO, VALA, XARX' ,N'MyA 68' ,N'Myranor' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000257' ,N'Spälter' ,40 ,90 ,N'(12) ESHB, NARK' ,N'MyA 68' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000258' ,N'Spatha' ,0 ,80 ,N'(6) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XRAX' ,N'MyA 69' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000259' ,N'Speer' ,8 ,80 ,N'(16) ANTH, CANT, CENT, CORA, CRAN, DEME, GAHT, GYLD, HALD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 69, Errata 2' ,N'Myranor' ,N'WZ. Auch bei einhändiger Führung ist die Distanzklasse S.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000260' ,N'Spetum' ,60 ,150 ,N'(8) CENT, CRAN, HARP, KARO, OCHO' ,N'MyA 69, 70' ,N'Myranor' ,N'Z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000261' ,N'Spitzhacke' ,4 ,200 ,N'(18) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GAHT, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SHIN, STAK, TALA, TESU, THAR, VALA, XARX' ,N'MyA 70' ,N'Myranor' ,N'iz')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000262' ,N'Stabkeule' ,60 ,200 ,N'(16) überall' ,N'MyA 70, Errata 2' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an. Die angegebenen Werte gelten für die Verwendung als Zweihandwaffe, der unter den Besonderheiten aufgeführte Bonus entfällt dafür. Die TP/KK bei einhändiger Führung beträgt 13/4, zudem ist dafür eine KK von 15 erforderlich und das Talent Hiebwaffen kommt zur Anwendung.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000263' ,N'Steinbeil' ,3 ,70 ,N'(10) überall' ,N'MyA 71, Errata 2' ,N'Myranor' ,N'"Das Ziel eines Wurfbeils muss wenigstens 5 Schritt entfernt sein. Richtet eine Attacke dank einer Metallrüstung keinen Schaden
an, muss ein Bruchtest durchgeführt werden."')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000264' ,N'Steinmesser' ,2 ,12 ,N'(16) überall' ,N'MyA 71' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000265' ,N'Stoßharpune' ,15 ,100 ,N'(8) HALD, THAL' ,N'MyA 72, Errata 2' ,N'Myranor' ,N'WZ. Auch bei einhändiger Führung ist die Distanzklasse S.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000267' ,N'Streitkolben' ,25 ,120 ,N'(10) ANTH, CANT, CENT, CORA, CRAN, DEME, GAHT, GYLD, HALD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 72, 73, Errata 2' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000268' ,N'Stuhlbein' ,0 ,40 ,NULL ,N'MyA 73' ,N'Myranor' ,N'siehe Regeln zu improvisierten Waffen ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000269' ,N'Sturmsense' ,8 ,120 ,N'(10) CENT, CORA, CRAN, GATH, GYLD, HALD, KARO, MAYE, NARI, OCHO, RHAC, SERO, VALA' ,N'MyA 73' ,N'Myranor' ,N'Z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000270' ,N'Su Talun' ,60 ,100 ,N'ERAS, unverkäuflich' ,N'MyA 74, Errata 2' ,N'Myranor' ,N'z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000271' ,N'Su''Arka' ,30 ,90 ,N'ERAS, unverkäuflich' ,N'MyA 73, 74, Errata 2' ,N'Myranor' ,N'Entwaffnen aus der AT ist um 2 erleichtert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000272' ,N'Su''Run' ,60 ,30 ,N'ERAS, unverkäuflich' ,N'MyA 74, Errata 2' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000273' ,N'TonFa' ,10 ,20 ,N'(10) ESHB, KORO, MAKS, TALA, THAR, sowie in imperialen Nerenithya' ,N'MyA 75' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000274' ,N'Unterarmklingen' ,20 ,20 ,N'(3) ALAM, ANTH, CRAN, DEME, GATH, HALD, HARP, KARO, KENT, KHAM, KORO, MAKS, OCHO, STAK, THAR' ,N'MyA 75, 76' ,N'Myranor' ,N'erzeugen im waffenlosen Kampf echte TP')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000275' ,N'Washkedal' ,20 ,80 ,N'(16) je nach Verbreitung von Shingwasiedlungen, vor allem CANT, CENT, CORA, HARP, KORO, MAKS, MAYE, MdSI, NEBE, PARD, SHIN, THAR, VALA' ,N'MyA 76' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000276' ,N'Windschwinge' ,50 ,60 ,N'Vinisha' ,N'MyA 76, 77' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000277' ,N'Ximtar' ,39 ,80 ,N'DRAY, unverkäuflich' ,N'MyA 77' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000278' ,N'Xiphos' ,60 ,80 ,N'DRAY, unverkäuflich' ,N'MyA 77, 78, Errata 2' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an. Giftregelung siehe Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000279' ,N'Xyele' ,13 ,35 ,N'DRAY, unverkäuflich' ,N'MyA 78' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000280' ,N'Xyston' ,13 ,100 ,N'DRAY, unverkäuflich' ,N'MyA 78, 79' ,N'Myranor' ,N'z')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000281' ,N'Zungenkappe' ,3 ,2 ,N'(14) je nach Verbreitung von Shingwasiedlungen, vor allem CANT, CENT, CORA, HARP, KORO, MAKS, MAYE, MdSI, NEBE, PARD, SHIN, THAR, VALA' ,N'MyA 79, Errata 2' ,N'Myranor' ,N'erzeugt im Kampf echte TP')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000282' ,N'Echsenspalter' ,350 ,160 ,NULL ,N'AA 84, Errata 4 ' ,N'Dunkle Zeiten' ,N'Großer Sklaventod: beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000283' ,N'Elemer Säbel' ,250 ,80 ,NULL ,N'AA 84' ,N'Dunkle Zeiten' ,N'Sklaventod: Vom Pferderücken aus gegen Fußkämpfer, richtet ein Sklaventod zwei zusätzliche TP an. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000284' ,N'Echsenspalter' ,350 ,160 ,NULL ,N'AA 84, Errata 4' ,N'Dunkle Zeiten' ,N'Großer Sklaventod: beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000285' ,N'Reitersäbel' ,180 ,75 ,NULL ,N'AA 38' ,N'Dunkle Zeiten' ,N'Amazonensäbel: Vom Pferderücken aus gegen den Fußlämpfer richtet ein Amazonensäbel zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000286' ,N'Stachelkeule' ,100 ,120 ,NULL ,N'AA 26' ,N'Dunkle Zeiten' ,N'Brabakbengel: Mit dem zentralen Dorn kann mit dem Talent Dolche auch gestochen werden: 1W6 TP, AT um 3 erschwert, eigene nächste PA ebenfalls um 3 erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000287' ,N'Kophta-Szepter' ,0 ,90 ,NULL ,N'AA 59' ,N'Dunkle Zeiten' ,N'siehe Sonnenszepter')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000288' ,N'Kentema' ,65 ,100 ,NULL ,N'OiC 47' ,N'Dunkle Zeiten' ,N'Regelung siehe OiC.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000289' ,N'Schwertlanze' ,180 ,150 ,NULL ,N'OiC 48' ,N'Dunkle Zeiten' ,N'Regelung siehe OiC.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000290' ,N'Hornissenstachel' ,120 ,80 ,NULL ,N'OiC 47' ,N'Dunkle Zeiten' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000291' ,N'Skorpionsstachel' ,45 ,120 ,NULL ,N'OiC 47' ,N'Dunkle Zeiten' ,N'ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000292' ,N'Sichelschwert' ,160 ,80 ,NULL ,N'OiC 48' ,N'Dunkle Zeiten' ,N'Vom Pferderücken aus gegen Fußkämpfer, richtet das Sichelschwert zwei zusätzliche TP an. Alle Manöver zum Entwaffnen sind mit dem Sichelschwert um 2 Punkte erleichtert; Umreißen ist mit dieser Waffe erlaubt.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0001-000000000293' ,N'Immanschläger' ,25 ,65 ,NULL ,N'A149 85' ,N'Aventurien' ,N'Patzer schon bei 19 oder 20. Gilt als improvisierte Waffe. Gegen jede 2 (Re-) Aktion ist ein Bruchtest fällig.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000056' ,N'Einhand-Donnerkolben, doppelläufig mit gebohrtem Lauf mit Armbrustschaft' ,60 ,234 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal.. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000057' ,N'Einhand-Donnerkolben, doppelläufig mit gezogenem Lauf' ,200 ,234 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000058' ,N'Einhand-Donnerkolben, doppelläufig mit gezogenem Lauf mit Armbrustschaft' ,300 ,234 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000059' ,N'Einhand-Donnerkolben, dreiläufig' ,30 ,328 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000060' ,N'Einhand-Donnerkolben, dreiläufig mit Armbrustschaft' ,45 ,328 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000061' ,N'Einhand-Donnerkolben, dreiläufig mit gebohrtem Lauf' ,120 ,328 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000062' ,N'Einhand-Donnerkolben, dreiläufig mit gebohrtem Lauf mit Armbrustschaft' ,180 ,328 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000063' ,N'Einhand-Donnerkolben, dreiläufig mit gezogenem Lauf' ,600 ,328 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000064' ,N'Einhand-Donnerkolben, dreiläufig mit gezogenem Lauf mit Armbrustschaft' ,900 ,328 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000065' ,N'Einhand-Donnerkolben, gebohter Lauf' ,20 ,140 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000066' ,N'Einhand-Donnerkolben, gebohter Lauf mit Armbrustschaft' ,30 ,140 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000067' ,N'Einhand-Donnerkolben, gezogener Lauf' ,200 ,140 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000068' ,N'Einhand-Donnerkolben, gezogener Lauf mit Armbrustschaft' ,300 ,140 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr.Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000069' ,N'Eshbathi-Langbogen' ,70 ,30 ,N'(12) ESHB, NARK' ,N'MyA 84' ,N'Myranor' ,N'Vorraussetzung KK 15, darunter gelten die Ziele als eine Kategori kleiner')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000070' ,N'Faust-Bela' ,100 ,60 ,N'(13) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, SERO, THAR, VALA, XRARX' ,N'MyA 84' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000071' ,N'Faust-Harpune' ,120 ,75 ,N'(8) CANT, KORO, MdSI, VALA' ,N'MyA 85' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000072' ,N'Flügelspeer' ,25 ,100 ,N'(12) ANTH, DEME, GATH, KHAM, KORO, RHAC' ,N'MyA 85' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000073' ,N'Fustibalus' ,7 ,60 ,N'(12) CENT, HARP, KARO, NARI, OCHO, SEVA' ,N'MyA 86' ,N'Myranor' ,N'Nicht vom Reittier aus einsetzbar. Im Nahkampf kann die Waffe mit dem Talent "Speere" geführt werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000074' ,N'Kentori Bogen' ,90 ,40 ,N'(14) KENT' ,N'MyA 86' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000075' ,N'Klingenbummerang' ,30 ,50 ,NULL ,N'MyA 87' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000076' ,N'Kreuz-Armbrust' ,100 ,160 ,N'(6) MdSI' ,N'MyA 87' ,N'Myranor' ,N'Bei einer KK von 13 oder weniger verdoppelt sich die Ladezeit')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000077' ,N'Kreuz-Bela' ,100 ,160 ,N'(6) MdSI' ,N'MyA 87' ,N'Myranor' ,N'Bei einer KK von 13 oder weniger verdoppelt sich die Ladezeit')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000078' ,N'Kurzbogen' ,45 ,20 ,N'(16) überall' ,N'MyA 87, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000079' ,N'Langbogen' ,60 ,30 ,N'(12) HALD, LORT, OCHO, SEVA' ,N'MyA 88' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000080' ,N'Lasso' ,6 ,40 ,N'(10) ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ERAS, GATH, GYLD, HALD, HARP, KARO, KENT, KHAM, KORO, LAKI, MAKS, MAYE, MdSI, OCHO, RHAC, SERO, SESK, TALA, TESU, THAL, THAR, VALA, XARX' ,N'MyA 88' ,N'Myranor' ,N'Die TP gelten als Erschwernis auf einen Entfesseln Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000081' ,N'Leichte Armbrust' ,50 ,140 ,N'(9) ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, EHSB, GATH, GYLD, HALD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, RHAX, SERO, SEVA, TALA, THAR, VALA, XARX' ,N'MyA 89' ,N'Myranor' ,N'Bei einer KK von 12 oder weniger verdoppelt sich die Ladezeit')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000082' ,N'Leichte Bela' ,80 ,120 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, SERO, THAR, VALA, XARX' ,N'MyA 88' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000083' ,N'Mholuren-Stachel' ,200 ,200 ,N'(6) CANT, CRAN, GYLD, KORO, MAYE, MdSI, NEBE, OCHO, PARD, SERO, SHIN, TALA, THAL, THAR, VALA' ,N'MyA 90' ,N'Myranor' ,N'Die Werte gelten nur für unter Wasser, bei Schüssen in der Luft sind die TP um 1 erhöht, die Reichweite aber ist halbiert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000084' ,N'Nandoros' ,16 ,120 ,N'(12) CANTH, CANT, CENT, CORA, CRAN, DEME, GYLD, HALD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, SERO, SEVA, VALA, XARX' ,N'MyA 90' ,N'Myranor' ,N'Reglung siehe Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000085' ,N'Nordland-Wurfaxt' ,15 ,50 ,N'(8) HALD, LORT, NYRA, OCHO, SESK, SEVA, STAK' ,N'MyA 91' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000086' ,N'Pfeilschleuder' ,5 ,10 ,N'(8) CORA, GYLD, NARI, VALA' ,N'MyA 91, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000087' ,N'Rollenbogen' ,200 ,50 ,N'(12) CENT, CORA, HARP' ,N'MyA 92' ,N'Myranor' ,N'Reiter können den Hornbogen nur dann einsetzen, wenn sie die SF Berittener Schütze besitzen. Standartmunition= gehärtete Pfeile. Patzer bei 18-20. Bei Bestätigung zu dem 2W6 Wurf zusätzlich 2. Bei dem Ergebnis "Kameraden getroffen" tifft der Schütze bei einem ungereaden Wurd immer sich selbst und zwar an dem Arm, der den Bogen hält.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000088' ,N'Schleuder ' ,5 ,10 ,N'(16) überall' ,N'MyA 93' ,N'Myranor' ,N'Preis für diese Geschosse entspricht angefertigten Schleuderbleien, die 1 TP mehr anrichten und die FK Probe um 1 erleichtern. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000089' ,N'Schleuderstab' ,7 ,50 ,N'(12) ANTH, DEME, ESHB, GATH, KENT, KHAM, KORO, MAKS, MAYE, NARK, RHAC, SESK' ,N'MyA 94' ,N'Myranor' ,N'Preis für diese Geschosse entspricht angefertigten Schleuderbleien, die 1 TP mehr anrichten und die FK Probe um 1 erleichtern. Passende Steine lassen sich jedoch (fast) überall finden. Bei gezielten Schuss sind alle Zuschläge verdoppelt.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000090' ,N'Scharfschützen-Bela' ,300 ,200 ,N'(4) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, SERO, OCHO, THAR, VALA, XARX' ,N'MyA 93' ,N'Myranor' ,N'Wenn KK des Opfer übersteigt, wird dieser niedergeworfen.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000091' ,N'Schwere Bela' ,120 ,180 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, SERO, THAR, VALA, XARX' ,N'MyA 95' ,N'Myranor' ,N'Wenn KK des Opfer übersteigt, wird dieser niedergeworfen.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000092' ,N'Schwere Armbrust' ,150 ,280 ,N'(8) CORA, CRAN, GYLD, SERO' ,N'MyA 94' ,N'Myranor' ,N'Regelung siehe Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000093' ,N'Sharbakan' ,30 ,20 ,N'(6) CANT, MAKS, NEBE, PARD, THAR' ,N'MyA 95' ,N'Myranor' ,N'Damit das Gift des Pfeils wirkt, muss mindestens 1 SP verursacht werden')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000094' ,N'Sholai-Kurzbogen' ,25 ,25 ,N'(10) ESHB, NARK' ,N'MyA 96 ' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000095' ,N'Speerschleuder' ,5 ,30 ,N'(16) ANTH, CANTH, CENT, CORA, CRAN, DEME, ERAS, ESCHB, GAHT, GYLD, HALD, HARP, KARO, KENT, KHAM, KORO, LAKI, LORT, MAKS, MAYE, NARI, NARK, NEBE, NYRA, OCHO, PARD, RHAC, SESK, SEVA, SHIN, STAK, TALA, TESU, THAR, VALA' ,N'MyA 96, Errata 2' ,N'Myranor' ,N'Alle Proben sind um 2 erschwert.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000096' ,N'Springdorn' ,120 ,15 ,N'(6) CANT, CENT, CORA, GYLD, HARP, MAKS, NARI, SERO, THAR, VALA' ,N'MyA 96' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000097' ,N'Stahlbogen' ,100 ,50 ,N'(10) MAKS, THAR' ,N'MyA 142' ,N'Myranor' ,N'Bei einer KK von 16 oder weniger gelten alle Ziele als eine Klasse weiter entfernt und die TP sinken um 1')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000098' ,N'Vollmetall-Wurfaxt' ,15 ,50 ,N'(12) CENT, CORA, CRAN, GAHT, GYLD, HALD, HARP, KARO, NARI, OCHO, SERO, SEVA, STAK' ,N'MyA 98' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000099' ,N'Windbüchse' ,500 ,180 ,N'(6) Cent, CORA, HARP, SERO' ,N'MyA 98' ,N'Myranor' ,N'Für jeweils 1 SR erhält man Druckluft für 5 Schüsse und erwirbt 1 Punkt Erschöpfung. Luftmumpe notwenig. Weitere Reglung siehe Arsenal')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000100' ,N'Windzunge' ,80 ,35 ,N'(14) ESHB' ,N'MyA 99' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000101' ,N'Wurfdolch ' ,10 ,15 ,N'(18) ESHB, MAKS, NARK, SESK, SHIN, TALA, TESU, THAR' ,N'MyA 99' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000102' ,N'Wurfdorn (im Nahkampf)' ,12 ,20 ,N'(16) ANTH, CANTH, CENT, CORA, CRAN, ESCHB, GAHT, GYLD, HARP, KARO, KORO, MAKS, MAYE, MdSI, NARI, OCHO, RHAC, SERO, SESK, SEVA, SHIN, STAK, THAR, VALA' ,N'MyA 100' ,N'Myranor' ,N'kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000103' ,N'Wurfholz' ,12 ,40 ,N'(10) ANTH, BANB, CDEMTE, ESHB, GATH, KENT, KHAM, KORO, MAKS, MAYE, MdSI, NARK, RHAC, SESK, TALA, TESU' ,N'MyA 100' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000104' ,N'Wurfkolben' ,12 ,80 ,N'(12) CENT, CORA, CRAN, GATH, GYLD, HALD, HARP, KARO, LORT, NARI, NYRA, OCHO, SESK, SEVA, STAK, TALA' ,N'MyA 101' ,N'Myranor' ,N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000105' ,N'Wurfkolben, gefüllt' ,18 ,50 ,N'(7) CENT, CORA, GYLD, HALD, KARO, NARI, OCHO, STAK, TALA' ,N'MyA 101' ,N'Myranor' ,N'Genaue Wirkung der Substanzen hängt vom Inhalt ab. Nach wurf wird auf W20 geworfen ob dieser beschädigt wurde. Bei einer 20 ist dieser beschädigt und kann erst wieder gefüllt eingesetzt werden, wenn dieser repariert wurde.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000106' ,N'Wurfkralle ' ,12 ,40 ,N'(8) CANT, MAKS, NARI, NEBE, OCHO, PARD, SHIN (nur Nayamaunir), THAL, THAR, VALA' ,N'MyA 101' ,N'Myranor' ,N'w(i)')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000107' ,N'Wurfmesser ' ,8 ,10 ,N'(8) ANTH, CANT, CENT, CORA, CRAN, ESHB, GATH, GYLD, HARP, KARO, KORO, MAKS, MAYE, MdSI, NARI, OCHO, RHAC, SERO, SHIN, STAK, TALA, THAR, VALA' ,N'MyA 102' ,N'Myranor' ,N'w(i)')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000108' ,N'Wurfnetz' ,35 ,80 ,N'(8) ANTH, CANT, CENT, CORA, CRAN, ESHB, GATH, GYLD, HALD, KARO, KORO, MAKS, MAYE, MdSI, NARI, OCHO, RHAC, SERO, SHIN, STAK, TALA, TESU, THAR, VALA, XARX' ,N'MyA 102' ,N'Myranor' ,N'AT 8. Die TP gelten als Erschwernis auf einen Entfesseln Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000109' ,N'Wurfnetz, schwer' ,60 ,200 ,N'(8) ANTH, CANT, CENT, CORA, CRAN, ESHB, GATH, GYLD, HALD, KARO, KORO, MAKS, MAYE, MdSI, NARI, OCHO, RHAC, SERO, SHIN, STAK, TALA, TESU, THAR, VALA, XARX' ,N'MyA 102' ,N'Myranor' ,N'AT 10. Die TP gelten als Erschwernis auf einen Entfesseln Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000110' ,N'Wurfpfeil' ,5 ,15 ,N'(7) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, GATH, GYLD, HARP, KARO, KENT, KORO, LAKI, MAYE, MdSI, NARI, OCHO, RHAC, SEROC, SEVA, VALA, XARX' ,N'MyA 103' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000111' ,N'Wurfring' ,18 ,10 ,N'(5) CANT, CENT, CORA, GATH, GYLD, HARP, KORO, MAYE, MdSI, NARI, OCHO, SERO, SHIN, TAHR, VALA' ,N'MyA 103' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000112' ,N'Wurfscheibe' ,18 ,10 ,N'(5) CANT, CENT, CORA, GATH, GYLD, HARP, KORO, MAYE, MdSI, NARI, OCHO, SERO, SHIN, TAHR, VALA' ,N'MyA 103' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000113' ,N'Wurfspeer' ,5 ,50 ,N'(15) ALAM, ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KENT, KHAM, KORO, LAKI, LORT, MAKS, MAYE, NARI, NARK, NEBE, NYRA, OCHO; RHAC, SERO, SESK, SEVA, SHIN, STAK, TALA, TESU, THAR, VALA, XARX' ,N'MyA 104' ,N'Myranor' ,N'Wird der Angriff mit einem Schild abgewehrt, bleibt dieser stecken und verdoppelt so den AT-Malus des Schildes, während der PA-Bonus um 1 sinkt. Um Speer aus dem Schild zu ziehen, benötigt der Schildträger 3 Aktionen.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000114' ,N'Wurfstein' ,0 ,10 ,N'überall und jederzeit' ,N'MyA 104' ,N'Myranor' ,N'improvisierte Waffe, die Zuschläge steigen hier um 3 für sehr große Ziele. Patzer bei 19 oder 20')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000115' ,N'Zweihand-Donnerkolben' ,15 ,240 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000116' ,N'Zweihand-Donnerkolben mit Armbrustschaft' ,22.5 ,240 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000117' ,N'Zweihand-Donnerkolben, doppelläufig' ,30 ,400 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000118' ,N'Zweihand-Donnerkolben, doppelläufig mit Armbrustschaft' ,45 ,400 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000119' ,N'Zweihand-Donnerkolben, doppelläufig mit gebohrtem Lauf' ,60 ,400 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000120' ,N'Zweihand-Donnerkolben, doppelläufig mit gebohrtem Lauf mit Armbrustschaft' ,90 ,400 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000121' ,N'Zweihand-Donnerkolben, doppelläufig mit gezogenem Lauf' ,300 ,400 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000122' ,N'Zweihand-Donnerkolben, doppelläufig mit gezogenem Lauf mit Armbrustschaft' ,450 ,400 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000123' ,N'Zweihand-Donnerkolben, dreiläufig' ,45 ,560 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000124' ,N'Zweihand-Donnerkolben, dreiläufig mit Armbrustschaft' ,67.5 ,560 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000125' ,N'Zweihand-Donnerkolben, dreiläufig mit gebohrtem Lauf' ,160 ,560 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000126' ,N'Zweihand-Donnerkolben, dreiläufig mit gebohrtem Lauf mit Armbrustschaft' ,240 ,560 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000127' ,N'Zweihand-Donnerkolben, dreiläufig mit gezogenem Lauf' ,800 ,560 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000128' ,N'Zweihand-Donnerkolben, dreiläufig mit gezogenem Lauf mit Armbrustschaft' ,1200 ,560 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000129' ,N'Zweihand-Donnerkolben, gebohrter Lauf' ,30 ,240 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000130' ,N'Zweihand-Donnerkolben, geborhter Lauf mit Armbrustschaft' ,45 ,240 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000131' ,N'Zweihand-Donnerkolben, gezogener Lauf' ,300 ,240 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*.Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an. ')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000132' ,N'Zweihand-Donnerkolben, gezogener Lauf mit Armbrustschaft' ,450 ,240 ,N'(12) TALA' ,N'MyA 108, Errata 2' ,N'Myranor' ,N'siehe Regelwerk im Myranischen Arsenal.Die Waffe benötigt keine min. KK mehr. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000133' ,N'Bauchspanner-Armbrust' ,200 ,180 ,NULL ,N'OiC 48' ,N'Dunkle Zeiten' ,N'Wunde bei KO/2-2; Jeder Punkt unter KK 15 erhöht Ladezeit um 5 Aktionen')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000134' ,N'Palta' ,80 ,90 ,NULL ,N'OiC 47' ,N'Dunkle Zeiten' ,N'siehe Regelung Dunkle Zeiten')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000135' ,N'Arbalette (Bolzen)' ,0 ,200 ,N'(4) ZWE, HOR, ALM' ,N'AA 41 / WdS 127' ,N'Aventurien' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000136' ,N'Arbalone (Bolzen)' ,0 ,480 ,N'(2) HOR' ,N'AA 43 / WdS 127' ,N'Aventurien' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000137' ,N'Balestra (Bolzen)' ,0 ,150 ,N'(3) HOR, ZWE' ,N'AA 41 / WdS 127' ,N'Aventurien' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000138' ,N'Balestrina (Bolzen)' ,0 ,60 ,N'(5) HOR, ZWE, MEN, ALM' ,N'AA 41 / WdS 127' ,N'Aventurien' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000139' ,N'Arbalette (Repetierer)' ,0 ,200 ,N'(4) ZWE, HOR, ALM' ,N'AA 41 / WdS 127' ,N'Aventurien' ,N'bis zu 7 Kugeln, verklemmt bei 19-20')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000140' ,N'Arbalone (Repetierer)' ,0 ,480 ,N'(2) HOR' ,N'AA 43 / WdS 127' ,N'Aventurien' ,N'bis zu 7 Kugeln, verklemmt bei 19-20')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000141' ,N'Balestra (Repetierer)' ,0 ,150 ,N'(3) HOR, ZWE' ,N'AA 41 / WdS 127' ,N'Aventurien' ,N'bis zu 3 Kugeln, verklemmt bei 19-20')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000142' ,N'Balestrina (Repetierer)' ,0 ,60 ,N'(5) HOR, ZWE, MEN, ALM' ,N'AA 41 / WdS 127' ,N'Aventurien' ,N'bis zu 3 Kugeln, verklemmt bei 19-20')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000143' ,N'Arbalette (Bolzenrepetierer)' ,0 ,200 ,N'(4) ZWE, HOR, ALM' ,N'AA 41 / WdS 127' ,N'Aventurien' ,N'bis zu 3 Bolzen, verklemmt bei 19-20')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0002-000000000144' ,N'Arbalone (Bolzenrepetierer)' ,0 ,480 ,N'(2) HOR' ,N'AA 43 / WdS 127' ,N'Aventurien' ,N'bis zu 3 Bolzen, verklemmt bei 19-20')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000021' ,N'Blutbüffel-Schild' ,35 ,180 ,N'(14) KHAM, RHAC' ,N'MyA 134' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000022' ,N'Faustschild (Metall)' ,18 ,60 ,N'(16) ALAM, ANTH, CANT, CORA, CRAN, DEME, GATH, GYLD, HARP, KARO, KORO, MAKS, MAYE, NARI, OCHO, SERO, SEVA, SHIN, TALA, THAR, VALA' ,N'MyA 134, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000023' ,N'Flechtschild' ,10 ,100 ,N'(10) überall' ,N'MyA 134' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000024' ,N'Großschild' ,20 ,200 ,N'(12) CARN, GATH, GYLD, HALD, LORT, SERO' ,N'MyA 135' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000025' ,N'Holzschild' ,30 ,160 ,N'(18) ANTH, CANT, CENT, CORA, CARN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI. MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 135, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000026' ,N'Lederschild' ,80 ,280 ,N'(14) überall' ,N'MyA 135, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000027' ,N'Myrimoden-Reiterschild' ,25 ,100 ,N'(12) CANT, CENT, CORA, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA,  XARX' ,N'MyA 135' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000028' ,N'Myrimodenschild' ,25 ,160 ,N'(12) CANT, CENT, CORA, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA,  XARX' ,N'MyA 135' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000029' ,N'Myrimoden-Setzschild' ,45 ,320 ,N'(12) CANT, CENT, CORA, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA,  XARX' ,N'MyA 135' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000030' ,N'Panzerarm' ,85 ,80 ,N'(10) ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HARP, KARO, KORO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 135' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000031' ,N'Stoßbluckler' ,32 ,80 ,N'(10) CANT, CENT, CORA, GAHT, GYLD, HARP, MAKS, MAYE, NARI, THAR, VALA' ,N'MyA 71' ,N'Myranor' ,N'Regelung siehe Arsenal.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0003-000000000032' ,N'Abishai-Tartasche' ,12 ,40 ,N'(10) MdSI' ,N'MyA 133' ,N'Myranor' ,N'Bei Verwendung von 2 Tartaschen gilt INI 1')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000076' ,N'Myrmidonen-Armschienen' ,24 ,70 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 130, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000077' ,N'Armschienen, Blutbüffelleder' ,10 ,60 ,N'(10) ANTH, KHAM, RHAC' ,N'MyA 123' ,N'Myranor' ,N'Gilt jweils fü 1 Paar')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000078' ,N'Armschienen, Chitin' ,4 ,40 ,NULL ,N'MyA 123' ,N'Myranor' ,N'Gilt jweils fü 1 Paar')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000079' ,N'Armschienen, Chratholz' ,25 ,35 ,N'(4) ANTH, KORO' ,N'MyA 123' ,N'Myranor' ,N'Gilt jweils fü 1 Paar')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000080' ,N'Armschienen, Edelbronze' ,10 ,80 ,N'(10) TALA' ,N'MyA 123' ,N'Myranor' ,N'Gilt jweils fü 1 Paar')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000081' ,N'Armschienen, Karettan' ,40 ,50 ,N'(4) CANT, KORO, MAKS, MDSI' ,N'MyA 123' ,N'Myranor' ,N'Gilt jweils fü 1 Paar')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000082' ,N'Armschienen, Kochleder' ,4 ,40 ,N'(14) überall' ,N'MyA 123' ,N'Myranor' ,N'Gilt jweils fü 1 Paar')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000083' ,N'Armschienen, Stahl' ,18 ,80 ,N'(12) ALAM, ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, MAYE, NARI, OCHO, SERO, THAR, VALA, XARX' ,N'MyA 123, Errata 2' ,N'Myranor' ,N'Gilt jweils fü 1 Paar')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000084' ,N'Ban-Bargui-Helm' ,8 ,100 ,N'(6) BANB' ,N'MyA 124, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000085' ,N'Ban-Bargui-Panzer' ,250 ,400 ,N'(6) BANB' ,N'MyA 124, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000086' ,N'Beinschienen, Blutbüffelleder' ,10 ,90 ,N'(10) ANTH, KHAM, RHAC' ,N'MyA 124' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000087' ,N'Beinschienen, Chitin' ,5 ,40 ,NULL ,N'MyA 124' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000088' ,N'Beinschienen, Chratholz' ,30 ,50 ,N'(4) ANTH, KORO' ,N'MyA 124' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000089' ,N'Beinschienen, Edelbronze' ,12 ,120 ,N'(10) TALA' ,N'MyA 124' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000090' ,N'Beinschienen, Karettan' ,50 ,75 ,N'(4) CANT, KORO, MAKS, MDSI' ,N'MyA 124' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000091' ,N'Beinschienen, Kochleder' ,5 ,40 ,N'(14) überall' ,N'MyA 124' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000092' ,N'Beinschienen, Stahl' ,12 ,18 ,N'(12) ALAM, ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, MAYE, NARI, OCHO, SERO, THAR, VALA, XARX' ,N'MyA 124, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000093' ,N'Brigantin-Weste/ Karettanplatten' ,250 ,240 ,N'(6) CANT, KORO, MAKS, MDSI, VALA' ,N'MyA 125' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000094' ,N'Brigantin-Weste/ Stahlplatten' ,60 ,320 ,N'(12) ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, MAYE, NARI, OCHO, SERO, STAK, THAR, VALA, XARX' ,N'MyA 125' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000095' ,N'Bronzemantel' ,120 ,280 ,N'(10) TALA' ,N'MyA 125' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000096' ,N'Brünne' ,250 ,480 ,N'(6) CRAN, GYLD, HALD' ,N'MyA 125, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000097' ,N'Brustharnisch, Blutbüffelleder' ,25 ,120 ,N'(10) ANTH, KHAM, RHAC' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000098' ,N'Brustharnisch, Chratholz' ,75 ,60 ,N'(4) ANTH, KORO' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000099' ,N'Brustharnisch, Edelbronze' ,30 ,150 ,N'(10) TALA' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000100' ,N'Brustharnisch, Karettan' ,120 ,90 ,N'(4) CANT, KORO, MAKS, MDSI' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000101' ,N'Brustharnisch, Kochleder' ,12 ,80 ,N'(14) überall' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000102' ,N'Brustharnisch, Stahl' ,30 ,150 ,N'(12) ALAM, ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, MAYE, NARI, OCHO, SERO, THAR, VALA, XARX' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000103' ,N'Brustplatte' ,8 ,30 ,N'(16) überall' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000104' ,N'Bügelhelm' ,15 ,100 ,N'(16) ANTH, CANT, CENT, CORA, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SHIN; TALA, TESU, THAR, VALA, XARX' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000105' ,N'Calar' ,0 ,12 ,N'je nach Material und Qualität' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000106' ,N'Chitinthorax' ,40 ,120 ,N'(10) ESHB, NARAK' ,N'MyA 126' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000107' ,N'Dicke Kleidung' ,0 ,120 ,N'je nach Material und Qualität' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000108' ,N'Eberzahnhelm' ,60 ,100 ,N'(8) CANT; CENT, CORA, DEME, GATH, GYLD, HARP, KARO, KORO, LAKI, MAYE, NARI, OCHO, SEVA, VALA, XARX' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000109' ,N'Erasumische Lederrüstung' ,50 ,340 ,N'(6) ERAS' ,N'MyA 126, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000110' ,N'Fellmantel' ,0 ,120 ,N'je nach Material und Qualität' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000111' ,N'Gladiatorenhelm' ,30 ,200 ,N'(6) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARo, OCHO, SERO, THAR, CALA, XARX' ,N'MyA 127, Errata 2' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000112' ,N'Glockenhelm' ,20 ,180 ,N'(8) CENT, CORA; DEME, GATH, HARP, KARO, LAKI, NARI, OCHO' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000113' ,N'Hörnerhelm' ,30 ,200 ,N'(16) CRAN, GATH, GYLD, HALD, LORT' ,N'MyA 127, Errata 2' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000114' ,N'Kapuze' ,1 ,20 ,N'je nach Material und Qualität' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000115' ,N'Kegelhelm' ,15 ,100 ,N'(12) ESHB, MAKS, NARK, SHIN, TALA, TESU, THAR' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000116' ,N'Kettenhemd' ,50 ,400 ,N'(8) CRAN, GATH, GYLD, HALD, MAKS; SERO, STAK' ,N'MyA 128, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000117' ,N'Kettenweste' ,25 ,200 ,N'(12) CRAN, GATH, GYLD, HALD, MAKS, SERO, STAK' ,N'MyA 128, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000118' ,N'Khorriss' ,40 ,80 ,N'(14) KHAM, RHAC' ,N'MyA 128' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000119' ,N'Kilbanion' ,100 ,280 ,N'(12) MAKS, TALA' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000120' ,N'Kriegshaube' ,25 ,120 ,N'(14) ALAM, ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, LORT, MAKS, MNAYE, NARI, NARK, OCHO, RHAC, SERO, SEVA, SHIN, TALA, TESU, THAR, VALA, XARX' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000121' ,N'Lederhelm' ,8 ,80 ,N'(16) überall' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000122' ,N'Lederschurz' ,15 ,3 ,N'(16) überall' ,N'MyA 130' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000123' ,N'Linothorax' ,12 ,120 ,N'(12) ANTH, CANT, CENT, CORA, CRAN, DEME, GATH, GYLD, HALD, HARP, KARO, KENT, KORO, LAKI, MAYE, NARI, OCHO, SERO, STAK, THAR, VALA, XARX' ,N'MyA 130, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000124' ,N'Maskenhelm' ,25 ,150 ,N'(10) CANT, CENT, CORA, CRAN, GATH, GYLD, HALD, HARP, KARO, KORO; LORT, MAYE, NARI, OCHO, SERO; THAR; VALA, XARX' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000125' ,N'Myrmidonen-Beinteile' ,18 ,110 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 130' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000126' ,N'Myrmidonen-Helm' ,12 ,120 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 130' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000127' ,N'Myrmidonen-Karettanmaske' ,12 ,30 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 130, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000128' ,N'Myrmidonen-Maske' ,8 ,80 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 130' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000129' ,N'Myrmidonen-Schulter' ,0 ,0 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 130' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000130' ,N'Myrmidonen-Thorax' ,100 ,350 ,N'(8) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 130' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000131' ,N'Nietenpanzer' ,10 ,60 ,N'(16) TALA' ,N'MyA 130' ,N'Myranor' ,N'Nietenpanzer berhindern beim Schwimmen mit BEx4 anstatt BEx2 und neigen auch sonst dazu, Wasser aufzusaugen und leicht ihr Eigengewicht in Flüssigkeit aufzunehmen')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000132' ,N'Panzerarm' ,5 ,10 ,N'(10) ANTH, CANT, CENT, CORA, CRAN, DEME,  GATH, GYLD, HARP, KARO, KORO, MAYE, NARI, OCHO, THAR, VALA, XARX' ,N'MyA 54, Errata 2' ,N'Myranor' ,N'Regelung siehe Arsenal. Lediglich ein Arm erhält den Rüstungsschutz, solange nicht mehr als ein Panzerarm getragen wird.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000133' ,N'Prunkrobe' ,0 ,120 ,N'je nach Material und Qualität' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000134' ,N'Schaller' ,30 ,160 ,N'(12) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, SERO, THAR, VALA, XARX' ,N'MyA 127' ,N'Myranor' ,N'Die BE von Helmen sollte bei Sinnesschärfe-Proben dreifach angerechnet werden.')
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000135' ,N'Subarmalis' ,40 ,40 ,N'(12) CANT, CENT, CORA, CRAN, GYLD, HALD, HARP, KARO, MAYE, NARI, OCHO, SERO, THAR, VALA, XARX' ,N'MyA 131, Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000136' ,N'Toga' ,0 ,12 ,N'je nach Material und Qualität' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000137' ,N'Tuchrüstung' ,8 ,100 ,N'(14) ANTH, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO, KORO, LAKI, MAKS, MAYE, NARI, NARK, OCHO, RHAC, SERO, SEVA, SHIN, STAK, TALA, TESU, THAR; VALA, XARX' ,N'MyA 131' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000138' ,N'Umhang' ,0 ,12 ,N'je nach Material und Qualität' ,N'MyA 129' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000139' ,N'Unterwams mit Karettanplatten' ,180 ,100 ,N'(8) CANT, KORO, MdSI, VALA' ,N'MyA 131' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000140' ,N'Karettanhelm' ,40 ,80 ,NULL ,N'Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000141' ,N'Chratholzhelm' ,25 ,60 ,NULL ,N'Errata 2' ,N'Myranor' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000142' ,N'meisterlicher Ringmantel (Brabakmantel)' ,900 ,360 ,NULL ,N'AA 104' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000144' ,N'meisterliche Kettenbeinlinge, Paar' ,1000 ,320 ,NULL ,N'AA 104' ,NULL ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000145' ,N'meisterliche Kettenhandschuhe, Paar' ,500 ,60 ,NULL ,N'AA 104' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000146' ,N'meisterliche Kettenhaube' ,400 ,140 ,NULL ,N'AA 104 / WdS 136' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000147' ,N'meisterliches Kettenzeug' ,1750 ,480 ,NULL ,N'AA 104 / WdS 136' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000148' ,N'meisterliche Kettenhaube, mit Gesichtsschutz' ,500 ,160 ,NULL ,N'AA 104' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000149' ,N'meisterliches Kettenhemd, 1/2 Arm' ,750 ,260 ,NULL ,N'AA 104 / WdS 135' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000150' ,N'meisterliches Kettenhemd, lang' ,900 ,400 ,NULL ,N'AA 104 / WdS 135' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000151' ,N'meisterlicher Kettenmantel' ,2500 ,480 ,NULL ,N'AA 104 / WdS 135' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000152' ,N'meisterliche Kettenweste' ,500 ,200 ,NULL ,N'AA 104 / WdS 135' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000153' ,N'meisterlicher Ringelpanzer' ,550 ,280 ,NULL ,N'AA 104 / WdS 136' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000154' ,N'meisterlicher Spiegelpanzer' ,1000 ,400 ,NULL ,N'AA 104 / WdS 136' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000156' ,N'meisterlicher Kettenkragen' ,300 ,100 ,NULL ,N'AA 104' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Preis],  [Gewicht],  [Verbreitung],  [Literatur],  [Setting],  [Bemerkung]) 
 VALUES ('00000000-0000-0000-0004-000000000157' ,N'meisterliche Löwenmähne' ,500 ,200 ,NULL ,N'AA 104' ,N'Aventurien, Dunkle Zeiten' ,NULL)
GO
UPDATE [Ausrüstung] SET [Name] = N'Aerobela' ,[Preis] = 150 ,[Gewicht] = 100 ,[Verbreitung] = N'(6) CENT, CORA' ,[Literatur] = N'MyA 80' ,[Setting] = N'Myranor' ,[Bemerkung] = N'Standartmunition=Kettenstecher' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000046'
GO
UPDATE [Ausrüstung] SET [Name] = N'Albenbogen' ,[Preis] = 80 ,[Gewicht] = 25 ,[Verbreitung] = N'(8) SESK, SEVA' ,[Literatur] = N'MyA 81' ,[Setting] = N'Myranor' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000047'
GO
UPDATE [Ausrüstung] SET [Name] = N'Ban-Bargui-Hornbogen' ,[Preis] = 120 ,[Gewicht] = 35 ,[Verbreitung] = N'(12) BANB' ,[Literatur] = N'MyA 82' ,[Setting] = N'Myranor' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000048'
GO
UPDATE [Ausrüstung] SET [Name] = N'Blasrohr' ,[Preis] = 20 ,[Gewicht] = 15 ,[Verbreitung] = N'(10) CANT, MAKS, NEBE, SEVA, SHIN, THAR' ,[Literatur] = N'MyA 83' ,[Setting] = N'Myranor' ,[Bemerkung] = N'Es darf kein FK ausgeführt werden. Damit das Gift des Pfeils wirkt, muss mindestens 1 SP verursacht werden' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000049'
GO
UPDATE [Ausrüstung] SET [Name] = N'Bola' ,[Preis] = 8 ,[Gewicht] = 40 ,[Verbreitung] = N'(14) ALAM, ANTH, BANB, CANT, CENT, CORA, CRAN, DEME, ERAS, ESHB, GATH, GYLD, HALD, HARP, KARO. KENT, KHAM, KORO, LAKI, MAKS, MAYE, MdSI, NARI, NARK, RHAC, SERO, SESK, SHIN, TALA, TESU, THAL, VALA, XARX' ,[Literatur] = N'MyA 83, 84' ,[Setting] = N'Myranor' ,[Bemerkung] = N'Die TP gelten als Erschwernis auf einen Entfesseln Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000050'
GO
UPDATE [Ausrüstung] SET [Name] = N'Einhand-Donnerkolben' ,[Preis] = 10 ,[Gewicht] = 140 ,[Verbreitung] = N'(12) TALA' ,[Literatur] = N'MyA 108, Errata 2' ,[Setting] = N'Myranor' ,[Bemerkung] = N'siehe Regelwerk im Myranischen Arsenal' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000051'
GO
UPDATE [Ausrüstung] SET [Name] = N'Einhand-Donnerkolben mit Armbrustschaft' ,[Preis] = 15 ,[Gewicht] = 140 ,[Verbreitung] = N'(12) TALA' ,[Literatur] = N'MyA 108, Errata 2' ,[Setting] = N'Myranor' ,[Bemerkung] = N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000052'
GO
UPDATE [Ausrüstung] SET [Name] = N'Einhand-Donnerkolben, doppelläufig' ,[Preis] = 20 ,[Gewicht] = 234 ,[Verbreitung] = N'(12) TALA' ,[Literatur] = N'MyA 108, Errata 2' ,[Setting] = N'Myranor' ,[Bemerkung] = N'siehe Regelwerk im Myranischen Arsenal.. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000053'
GO
UPDATE [Ausrüstung] SET [Name] = N'Einhand-Donnerkolben, doppelläufig mit Armbrustschaft' ,[Preis] = 30 ,[Gewicht] = 234 ,[Verbreitung] = N'(12) TALA' ,[Literatur] = N'MyA 108, Errata 2' ,[Setting] = N'Myranor' ,[Bemerkung] = N'siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*mehr; Forschung für den Arbmbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000054'
GO
UPDATE [Ausrüstung] SET [Name] = N'Einhand-Donnerkolben, doppelläufig mit gebohrtem Lauf' ,[Preis] = 40 ,[Gewicht] = 234 ,[Verbreitung] = N'(12) TALA' ,[Literatur] = N'MyA 108, Errata 2' ,[Setting] = N'Myranor' ,[Bemerkung] = N'siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*; Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000055'
GO

/* Einstellungen */

UPDATE [Einstellungen] SET [WertInt] = 0 WHERE [Name]=N'SelectedTab'
GO

/* Fernkampfwaffe */

INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000056' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,50 ,40 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000057' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,60 ,50 ,120 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000058' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,60 ,50 ,120 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000059' ,NULL ,NULL ,NULL ,0 ,6 ,2 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,70 ,40 ,2 ,1 ,0 ,-1 ,-2 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000060' ,NULL ,NULL ,NULL ,0 ,6 ,2 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,70 ,40 ,2 ,1 ,0 ,-1 ,-2 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000061' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,70 ,40 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000062' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,70 ,40 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000063' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,60 ,70 ,120 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000064' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,60 ,70 ,120 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000065' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,30 ,40 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000066' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,30 ,40 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000067' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,9 ,0 ,NULL ,NULL ,0 ,15 ,30 ,60 ,30 ,120 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000068' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,9 ,0 ,NULL ,NULL ,0 ,15 ,30 ,60 ,30 ,120 ,2 ,1 ,0 ,-1 ,-2 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000069' ,0.04 ,3 ,N'Pfeil' ,0 ,6 ,1 ,5 ,0 ,NULL ,NULL ,0 ,5 ,15 ,30 ,50 ,100 ,1 ,0 ,0 ,0 ,-1 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000070' ,0.04 ,3 ,N'Bolzen' ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,2 ,5 ,10 ,15 ,20 ,2 ,1 ,0 ,0 ,-1 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000071' ,2 ,10 ,N'kurze Speere' ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,2 ,4 ,6 ,8 ,15 ,1 ,0 ,-1 ,-2 ,-3 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000072' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,5 ,15 ,30 ,45 ,60 ,1 ,1 ,0 ,0 ,0 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000073' ,0.5 ,5 ,N'Kugel' ,0 ,6 ,1 ,5 ,0 ,NULL ,NULL ,0 ,0 ,10 ,25 ,50 ,80 ,NULL ,1 ,1 ,0 ,-1 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000074' ,0 ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,5 ,15 ,40 ,80 ,120 ,2 ,1 ,1 ,0 ,0 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000075' ,0.04 ,3 ,N'Pfeil' ,0 ,6 ,1 ,5 ,0 ,NULL ,NULL ,0 ,0 ,5 ,15 ,25 ,40 ,NULL ,0 ,0 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000076' ,0.04 ,4 ,N'Bolzen' ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,10 ,15 ,20 ,25 ,40 ,2 ,1 ,0 ,-1 ,-2 ,15)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000077' ,0.04 ,4 ,N'Bolzen' ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,10 ,15 ,20 ,25 ,40 ,2 ,1 ,0 ,-1 ,-2 ,15)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000078' ,0.2 ,2 ,N'Pfeil' ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,5 ,15 ,25 ,40 ,60 ,1 ,1 ,0 ,0 ,-1 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000079' ,0.04 ,3 ,N'Pfeil' ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,10 ,25 ,50 ,100 ,200 ,3 ,2 ,1 ,0 ,-1 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000080' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,0 ,2 ,5 ,10 ,15 ,NULL ,0 ,0 ,-1 ,-2 ,1)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000081' ,0.03 ,4 ,N'Bolzen' ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,10 ,15 ,25 ,40 ,60 ,1 ,1 ,0 ,0 ,-1 ,15)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000082' ,0.04 ,3 ,N'Bolzen' ,0 ,6 ,2 ,2 ,0 ,NULL ,NULL ,0 ,5 ,10 ,20 ,40 ,60 ,3 ,2 ,1 ,0 ,-1 ,8)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000083' ,3 ,25 ,N'Pfeil' ,0 ,6 ,2 ,4 ,0 ,NULL ,NULL ,0 ,5 ,10 ,15 ,25 ,40 ,1 ,0 ,0 ,-2 ,-4 ,10)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000084' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,5 ,10 ,15 ,20 ,30 ,2 ,1 ,0 ,-1 ,-2 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000085' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,NULL ,5 ,10 ,15 ,25 ,NULL ,1 ,1 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000086' ,5 ,10 ,N'Bolzen' ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,NULL ,4 ,10 ,20 ,30 ,1 ,1 ,0 ,-1 ,-2 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000087' ,2 ,4 ,N'Pfeil' ,0 ,6 ,1 ,8 ,0 ,NULL ,NULL ,0 ,10 ,30 ,60 ,100 ,200 ,3 ,2 ,1 ,0 ,0 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000088' ,0.4 ,3 ,N'Stein' ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,0 ,5 ,15 ,25 ,40 ,NULL ,0 ,0 ,0 ,0 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000089' ,0.4 ,3 ,N'Stein' ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,0 ,5 ,20 ,40 ,60 ,NULL ,0 ,0 ,0 ,0 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000090' ,0.08 ,4 ,N'Bolzen' ,0 ,6 ,2 ,8 ,0 ,NULL ,NULL ,0 ,5 ,25 ,50 ,100 ,150 ,4 ,3 ,3 ,2 ,0 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000091' ,0.05 ,4 ,N'Bolzen' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,15 ,40 ,60 ,100 ,3 ,2 ,1 ,0 ,0 ,26)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000092' ,0.06 ,6 ,N'Bolzen' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,10 ,30 ,60 ,100 ,180 ,4 ,2 ,0 ,-1 ,-3 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000093' ,0.01 ,NULL ,N'Pfeil' ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,5 ,15 ,25 ,40 ,60 ,1 ,0 ,0 ,-1 ,-2 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000094' ,0.03 ,2 ,N'Pfeil' ,0 ,6 ,1 ,5 ,0 ,NULL ,NULL ,0 ,5 ,15 ,25 ,40 ,60 ,2 ,1 ,0 ,0 ,-1 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000095' ,5 ,50 ,N'Speer' ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,0 ,15 ,30 ,60 ,100 ,0 ,1 ,0 ,0 ,-1 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000096' ,0.05 ,2 ,NULL ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,1 ,2 ,3 ,4 ,5 ,1 ,0 ,0 ,0 ,-1 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000097' ,0.06 ,4 ,N'Pfeil' ,0 ,6 ,1 ,7 ,0 ,NULL ,NULL ,0 ,10 ,20 ,40 ,80 ,150 ,3 ,2 ,1 ,0 ,0 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000098' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,NULL ,5 ,10 ,15 ,25 ,NULL ,0 ,0 ,-1 ,-2 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000099' ,0.2 ,4 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,20 ,40 ,80 ,120 ,4 ,3 ,3 ,2 ,0 ,2)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000100' ,0.04 ,3 ,N'Pfeil' ,0 ,6 ,1 ,5 ,0 ,NULL ,NULL ,0 ,0 ,15 ,30 ,60 ,80 ,2 ,1 ,0 ,0 ,-1 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000101' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,2 ,4 ,6 ,8 ,15 ,1 ,0 ,0 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000102' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,2 ,4 ,6 ,8 ,15 ,1 ,0 ,0 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000103' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,0 ,5 ,15 ,25 ,40 ,NULL ,1 ,1 ,1 ,0 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000104' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,0 ,5 ,10 ,15 ,20 ,NULL ,2 ,1 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000105' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,NULL ,0 ,NULL ,NULL ,0 ,0 ,2 ,5 ,10 ,25 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000106' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,0 ,5 ,10 ,15 ,30 ,NULL ,1 ,1 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000107' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,1 ,0 ,NULL ,NULL ,0 ,2 ,4 ,6 ,8 ,15 ,1 ,0 ,0 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000108' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,NULL ,5 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,1)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000109' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,0 ,NULL ,5 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,1)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000110' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,2 ,4 ,6 ,8 ,15 ,1 ,0 ,0 ,0 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000111' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,1 ,0 ,NULL ,NULL ,0 ,2 ,4 ,8 ,12 ,20 ,1 ,0 ,0 ,0 ,0 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000112' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,1 ,0 ,NULL ,NULL ,0 ,2 ,4 ,8 ,12 ,20 ,1 ,0 ,0 ,0 ,0 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000113' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,5 ,10 ,15 ,20 ,30 ,1 ,1 ,0 ,-1 ,-2 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000114' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,NULL ,0 ,NULL ,NULL ,0 ,1 ,2 ,4 ,8 ,12 ,0 ,0 ,0 ,-1 ,-1 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000115' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,3 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,40 ,60 ,3 ,1 ,0 ,-1 ,-3 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000116' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,3 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,40 ,60 ,3 ,1 ,0 ,-1 ,-3 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000117' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,3 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,66 ,60 ,3 ,1 ,0 ,-1 ,-3 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000118' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,3 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,66 ,60 ,3 ,1 ,0 ,-1 ,-3 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000119' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,66 ,60 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000120' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,66 ,60 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000121' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,75 ,66 ,180 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000122' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,75 ,66 ,180 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000123' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,3 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,82 ,60 ,3 ,1 ,0 ,-1 ,-3 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000124' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,3 ,NULL ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,82 ,60 ,3 ,1 ,0 ,-1 ,-3 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000125' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,82 ,60 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000126' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,82 ,60 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000127' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,75 ,82 ,180 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000128' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,15 ,30 ,75 ,82 ,180 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000129' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,40 ,60 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000130' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,40 ,60 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000131' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,9 ,0 ,NULL ,NULL ,0 ,15 ,30 ,75 ,40 ,180 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000132' ,1.2 ,2 ,N'Kugel' ,0 ,6 ,2 ,9 ,0 ,NULL ,NULL ,0 ,15 ,30 ,75 ,40 ,180 ,3 ,1 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000133' ,2 ,5 ,NULL ,0 ,6 ,2 ,4 ,0 ,NULL ,NULL ,0 ,15 ,30 ,70 ,120 ,200 ,4 ,2 ,0 ,-1 ,-2 ,15)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000134' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,5 ,10 ,15 ,25 ,40 ,3 ,2 ,1 ,0 ,0 ,NULL)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000135' ,2.5 ,8 ,N'Bolzen' ,0 ,6 ,2 ,7 ,0 ,NULL ,NULL ,1 ,11 ,22 ,33 ,66 ,110 ,2 ,1 ,0 ,-1 ,-2 ,30)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000136' ,4 ,10 ,N'Bolzen' ,0 ,6 ,3 ,8 ,0 ,NULL ,NULL ,1 ,17 ,33 ,66 ,132 ,275 ,4 ,2 ,0 ,-1 ,-3 ,40)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000137' ,2 ,5 ,N'Bolzen' ,0 ,6 ,2 ,4 ,0 ,NULL ,NULL ,1 ,11 ,22 ,33 ,55 ,83 ,2 ,1 ,0 ,0 ,-1 ,12)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000138' ,1.5 ,5 ,N'Bolzen' ,0 ,6 ,1 ,6 ,0 ,NULL ,NULL ,1 ,2 ,4 ,8 ,15 ,25 ,2 ,1 ,0 ,0 ,-1 ,4)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000139' ,2.5 ,8 ,N'Kugel' ,0 ,6 ,2 ,5 ,0 ,NULL ,NULL ,1 ,10 ,20 ,30 ,60 ,100 ,2 ,1 ,0 ,-1 ,-2 ,27)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000140' ,4 ,10 ,N'Kugel' ,0 ,6 ,3 ,6 ,0 ,NULL ,NULL ,1 ,15 ,30 ,60 ,120 ,250 ,4 ,2 ,0 ,-1 ,-3 ,37)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000141' ,2 ,5 ,N'Kugel' ,0 ,6 ,2 ,2 ,0 ,NULL ,NULL ,1 ,10 ,20 ,30 ,50 ,75 ,2 ,1 ,0 ,0 ,-1 ,11)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000142' ,1.5 ,5 ,N'Kugel' ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,2 ,4 ,8 ,15 ,25 ,2 ,1 ,0 ,0 ,-1 ,3)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000143' ,2.5 ,8 ,N'Bolzen' ,0 ,6 ,2 ,7 ,0 ,NULL ,NULL ,1 ,11 ,22 ,33 ,66 ,110 ,2 ,1 ,0 ,-1 ,-2 ,27)
GO
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0002-000000000144' ,4 ,10 ,N'Bolzen' ,0 ,6 ,3 ,8 ,0 ,NULL ,NULL ,1 ,17 ,33 ,66 ,132 ,275 ,4 ,2 ,0 ,-1 ,-3 ,37)
GO
UPDATE [Fernkampfwaffe] SET [Laden] = 4 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000003'
GO
UPDATE [Fernkampfwaffe] SET [Laden] = 2 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000004'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = 0.15 ,[Munitionsgewicht] = 5 ,[TPBonus] = 5 ,[Verwundend] = 0 ,[RWSehrNah] = 5 ,[RWNah] = 10 ,[RWMittel] = 20 ,[RWWeit] = 35 ,[RWSehrWeit] = 50 ,[TPSehrNah] = 3 ,[TPNah] = 2 ,[Laden] = 6 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000046'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = 0.25 ,[Munitionsgewicht] = 3 ,[Munitionsart] = N'Pfeil' ,[TPWürfelAnzahl] = 1 ,[TPBonus] = 5 ,[Verwundend] = 0 ,[RWSehrNah] = 10 ,[RWNah] = 25 ,[RWMittel] = 50 ,[RWWeit] = 100 ,[RWSehrWeit] = 200 ,[TPSehrNah] = 3 ,[TPMittel] = 1 ,[TPWeit] = 1 ,[TPSehrWeit] = 0 ,[Laden] = 3 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000047'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = 0.05 ,[Munitionsgewicht] = 4 ,[Munitionsart] = N'Pfeil' ,[TPWürfelAnzahl] = 1 ,[TPBonus] = 6 ,[Verwundend] = 0 ,[RWSehrNah] = 5 ,[RWNah] = 20 ,[RWMittel] = 40 ,[RWWeit] = 60 ,[RWSehrWeit] = 80 ,[TPSehrNah] = 3 ,[TPNah] = 2 ,[TPMittel] = 2 ,[TPWeit] = 1 ,[Laden] = 4 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000048'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = 0.01 ,[Munitionsgewicht] = NULL ,[Munitionsart] = N'Pfeil' ,[TPBonus] = -1 ,[Verwundend] = 0 ,[RWNah] = 5 ,[RWMittel] = 10 ,[RWWeit] = 30 ,[RWSehrWeit] = 40 ,[TPSehrNah] = 0 ,[TPNah] = 0 ,[TPSehrWeit] = -2 ,[Laden] = 2 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000049'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = NULL ,[Munitionsgewicht] = NULL ,[Munitionsart] = NULL ,[TPWürfelAnzahl] = 1 ,[TPBonus] = 2 ,[Verwundend] = 0 ,[RWSehrNah] = 0 ,[RWNah] = 5 ,[RWMittel] = 10 ,[RWWeit] = 15 ,[RWSehrWeit] = 25 ,[TPSehrNah] = NULL ,[TPNah] = 0 ,[TPWeit] = 0 ,[TPSehrWeit] = -1 ,[Laden] = 1 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000050'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = NULL ,[Munitionsgewicht] = NULL ,[Munitionsart] = NULL ,[TPWürfelAnzahl] = 2 ,[TPBonus] = NULL ,[Verwundend] = 0 ,[RWSehrNah] = 5 ,[RWNah] = 10 ,[RWMittel] = 20 ,[RWWeit] = 30 ,[RWSehrWeit] = 40 ,[TPSehrNah] = 2 ,[TPNah] = 1 ,[TPSehrWeit] = -2 ,[Laden] = 30 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000051'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = NULL ,[Munitionsgewicht] = NULL ,[Munitionsart] = NULL ,[TPBonus] = NULL ,[Verwundend] = 0 ,[RWSehrNah] = 5 ,[RWNah] = 10 ,[RWMittel] = 20 ,[RWWeit] = 30 ,[RWSehrWeit] = 40 ,[TPWeit] = -1 ,[TPSehrWeit] = -2 ,[Laden] = 30 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000052'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = NULL ,[Munitionsgewicht] = NULL ,[Munitionsart] = NULL ,[TPWürfelAnzahl] = 2 ,[TPBonus] = NULL ,[RWSehrNah] = 5 ,[RWNah] = 10 ,[RWMittel] = 20 ,[RWWeit] = 50 ,[RWSehrWeit] = 40 ,[TPWeit] = -1 ,[TPSehrWeit] = -2 ,[Laden] = 30 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000053'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = NULL ,[Munitionsgewicht] = NULL ,[Munitionsart] = NULL ,[TPBonus] = NULL ,[Verwundend] = 0 ,[RWSehrNah] = 5 ,[RWNah] = 10 ,[RWMittel] = 20 ,[RWWeit] = 50 ,[RWSehrWeit] = 40 ,[Laden] = 30 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000054'
GO
UPDATE [Fernkampfwaffe] SET [Munitionspreis] = NULL ,[Munitionsgewicht] = NULL ,[Munitionsart] = NULL ,[TPWürfelAnzahl] = 1 ,[TPBonus] = 6 ,[Verwundend] = 0 ,[RWSehrNah] = 5 ,[RWNah] = 10 ,[RWMittel] = 20 ,[RWWeit] = 50 ,[RWSehrWeit] = 40 ,[TPSehrNah] = 2 ,[TPNah] = 1 ,[TPSehrWeit] = -2 ,[Laden] = 40 WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000055'
GO


/* Fernkampfwaffe_Talent */

INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000046' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000047' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000048' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000049' ,N'Blasrohr')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000050' ,N'Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000051' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000052' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000053' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000054' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000055' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000056' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000057' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000058' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000059' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000060' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000061' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000062' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000063' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000064' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000065' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000066' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000067' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000068' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000069' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000070' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000071' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000072' ,N'Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000073' ,N'Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000074' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000075' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000076' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000077' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000078' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000079' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000080' ,N'Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000081' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000082' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000083' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000084' ,N'Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000085' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000086' ,N'Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000087' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000088' ,N'Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000089' ,N'Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000090' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000091' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000092' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000093' ,N'Blasrohr')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000094' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000095' ,N'Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000096' ,N'Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000097' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000098' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000099' ,N'Bela')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000100' ,N'Bogen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000101' ,N'Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000102' ,N'Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000103' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000104' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000105' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000106' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000107' ,N'Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000108' ,N'Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000109' ,N'Schleuder')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000110' ,N'Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000111' ,N'Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000112' ,N'Wurfmesser')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000113' ,N'Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000114' ,N'Wurfbeile')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000115' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000116' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000117' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000118' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000119' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000120' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000121' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000122' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000123' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000124' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000125' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000126' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000127' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000128' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000129' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000130' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000131' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000132' ,N'Feuerwaffen')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000133' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000134' ,N'Wurfspeere')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000135' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000136' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000137' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000138' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000139' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000140' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000141' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000142' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000143' ,N'Armbrust')
GO
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000144' ,N'Armbrust')
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000046' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000047' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000048' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000049' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000050' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000051' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000052' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000053' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000054' AND [Talentname]=N'Armbrust'
GO
DELETE FROM [Fernkampfwaffe_Talent] WHERE [FernkampfwaffeGUID]='00000000-0000-0000-0002-000000000055' AND [Talentname]=N'Armbrust'
GO


/* GegnerBase */

INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('4dc211a6-d47a-4003-9018-417f3f671edb' ,N'Goblin (unerfahren)' ,NULL ,8 ,N'1W6' ,2 ,8 ,22 ,30 ,0 ,0 ,8 ,1 ,0 ,8 ,NULL ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'AT 10
' ,N'ZBA 102f' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('779fe073-6024-4e68-bf91-8d1a1bc587d1' ,N'Hamster' ,NULL ,0 ,N'1W6' ,2 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'' ,N'ZBA 141' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('7849b0b1-be97-49fe-a1a0-f0228511a13a' ,N'Karnickel' ,NULL ,0 ,N'1W6' ,2 ,0 ,5 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'' ,N'ZBA 108f' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('94366590-e094-4358-a46c-02c35ead7db2' ,N'Ork (Veteran)' ,NULL ,18 ,N'1W6' ,2 ,16 ,45 ,51 ,0 ,0 ,18 ,4 ,0 ,8 ,NULL ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'AT 19
Biss, Niederwerfen, Überrennen, Wuchtschlag, Aufmerksamkeit, Gegenhalten, Kampfreflexe, Meisterparade, Rüstungsgewöhnung I' ,N'ZBA 144ff' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('986f9420-ad65-4d56-991f-f4e994dfc454' ,N'Gobin (erfahren)' ,NULL ,13 ,N'1W6' ,2 ,10 ,27 ,35 ,0 ,0 ,11 ,3 ,0 ,9 ,NULL ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'AT 12
Ausweichen I (PA 10), Kampfreflexe' ,N'ZBA 102f' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('9e3e109c-98f2-4459-8301-4c8688c8e718' ,N'Krähe' ,NULL ,12 ,N'1W6' ,2 ,0 ,5 ,40 ,0 ,0 ,0 ,3 ,0 ,1 ,13 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Schnabel/Klauen: DK H AT 8 TP 1W6
Flugangriff, Sehr kleiner Gegner (AT+3/PA+6), Gezielter Angriff (bei Glücklicher Attacke; umgeht RS)
' ,N'ZBA 183f' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('a488ea44-8152-4412-9db2-81690de4fb14' ,N'Bergadler' ,NULL ,10 ,N'1W6' ,2 ,6 ,16 ,60 ,0 ,0 ,9 ,2 ,3 ,1 ,25 ,NULL ,2 ,2 ,2 ,2 ,2 ,2 ,2 ,2 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Schnabel/Klauen: DK H AT 6/14 TP 1W6+2
Flugangriff, Sturzflug, Gezielter Angriff / Verkrallen, Gelände (Gebirge)
' ,N'ZBA 64f' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('af410ee3-0871-456f-9dbe-099ae2da7c13' ,N'Goblin (Veteran)' ,NULL ,15 ,N'1W6' ,2 ,14 ,33 ,40 ,0 ,0 ,14 ,5 ,0 ,9 ,NULL ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'AT 16
Ausweichen I (PA 10), Kampfreflexe, Ausweichen II (PA 14), Reiterkampf, Scharfschütze' ,N'ZBA 102f' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('b0d01a8e-e407-4d76-90d3-9cf70dd3baba' ,N'Harpyie' ,NULL ,9 ,N'1W6' ,2 ,7 ,35 ,50 ,0 ,0 ,14 ,12 ,5 ,2 ,15 ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Klauen: DK N AT 14 TP 1W6+5
Flugangriff, Sturzflug, Gezielter Angriff (Sturzflugangriff, um Gegner in die Luft zu nehmen), Gelände (Baum, Gebirge)
' ,N'ZBA 108' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('b39a9d1f-d6fd-42f8-be33-751a1453c580' ,N'Ork (unerfahren)' ,NULL ,10 ,N'1W6' ,2 ,9 ,30 ,35 ,0 ,0 ,10 ,0 ,0 ,8 ,NULL ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'AT 13
Biss' ,N'ZBA 144ff' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('caef7fa9-795b-43f3-b330-e8f4ef579441' ,N'Fledermaus' ,NULL ,9 ,N'2W6' ,2 ,0 ,1 ,50 ,0 ,0 ,7 ,2 ,0 ,18 ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Biss: DK H AT 5 TP 1 SP
Flugangriff, Gezielter Angriff (SP statt TP), sehr kleiner Gegner (AT+4/PA+7), Gelände (Höhle)
' ,N'ZBA 97' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('d45a5af2-7d70-4c7a-a80d-daceca3a1486' ,N'Ziege' ,NULL ,0 ,N'1W6' ,2 ,0 ,22 ,30 ,0 ,0 ,0 ,1 ,5 ,2 ,5 ,10 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'' ,N'ZBA 189' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('d66790a6-3ac1-454c-9f11-2df7728004f9' ,N'Ork (erfahren)' ,NULL ,12 ,N'1W6' ,2 ,11 ,39 ,44 ,0 ,0 ,14 ,1 ,0 ,8 ,NULL ,NULL ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'AT 15
Biss, Niederwerfen, Überrennen, Wuchtschlag' ,N'ZBA 144ff' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('d7c0b2c9-e113-4003-8f9c-f00542311fe5' ,N'Murmeltier' ,NULL ,0 ,N'1W6' ,2 ,0 ,6 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'' ,N'ZBA 141f' ,N'Aventurien')
GO
INSERT INTO [GegnerBase] (  [GegnerBaseGUID],  [Name],  [Bild],  [INIBasis],  [INIZufall],  [Aktionen],  [PA],  [LE],  [AU],  [AE],  [KE],  [KO],  [MRGeist],  [MRKörper],  [GS],  [GS2],  [GS3],  [RSKopf],  [RSBrust],  [RSRücken],  [RSArmL],  [RSArmR],  [RSBauch],  [RSBeinL],  [RSBeinR],  [GW],  [Jagd],  [Beschwörung],  [Kontrolle],  [Beschwörungskosten],  [Tags],  [Bemerkung],  [Literatur],  [Setting]) 
 VALUES ('f13c94c3-c81b-447a-9511-77ae165f592b' ,N'Gebirgsbock' ,NULL ,5 ,N'1W6' ,2 ,4 ,38 ,50 ,0 ,0 ,10 ,0 ,4 ,6 ,NULL ,NULL ,2 ,2 ,2 ,2 ,2 ,2 ,2 ,2 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Stoß: DK H AT 10 TP 1W6+2
Gelände (Gebirge), Überrennen (4, 1W+4), Niederwerfen (3)
' ,N'ZBA 189' ,N'Aventurien')
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='cb394c3a-1435-455a-8851-9cbf431039ec'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='92122c0d-1cc7-47b5-88e9-0e008faceccf'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='9e4179a3-e4a2-4018-bea1-dae582fdcabc'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='d96fe37d-52b7-4f3c-aad0-5508eb11fb29'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='9f02019a-23f7-49e3-a854-57ea05a13634'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='c93e67d0-68a6-4ac9-a457-0e551ba5d120'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='1d7ecb6a-e267-4550-ac05-d2e7cfd232f1'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='84b6580b-9a23-4501-8d9d-d81d1cf5d434'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='9748b7f4-0cc2-4180-9b07-f87b10377ceb'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='aaf9dabf-bfc6-4b09-9930-6ff36cc62eec'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='5cae0365-975e-420d-bce4-63fcb4bc556d'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='9c7392bc-4e48-444a-8b51-3b2cac2680ef'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='c5ede20e-d7dc-4b55-91c9-69701062eecd'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='14cf5a70-0e01-4521-a43c-af26d328986e'
GO
DELETE FROM [GegnerBase] WHERE [GegnerBaseGUID]='2d1d45f3-4e9d-4a9c-85b2-61f63fd0cee7'
GO

/* Kultur */

INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000187' ,N'Agrim' ,N'Dämonenstädte' ,9 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 86' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000188' ,N'Agrim' ,N'Oberfläche' ,9 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 86' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000189' ,N'Amhasim' ,N'Amhasim' ,7 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 95' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000190' ,N'Amhasim' ,N'Landbevölkerung' ,0 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 95' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000191' ,N'Angurianer' ,N'Angurianer' ,12 ,NULL ,11 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,N'Rakshazar 105' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000192' ,N'Angurianer' ,N'Bevölkerung von An''Khoral' ,13 ,NULL ,11 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,N'Rakshazar 105' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000193' ,N'Brachtao' ,N'Kriegerkhez' ,8 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'Rakshazar 114' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000194' ,N'Brachtao' ,N'Händlerkhez' ,6 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 114' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000195' ,N'Brachtao' ,N'Hirtenkhez' ,7 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,N'Rakshazar 114' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000196' ,N'Broktharstämme' ,N'Broktharstämme' ,9 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 121' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000197' ,N'Broktharstämme' ,N'Sesshafter Stamm' ,10 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 121' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000198' ,N'Broktharstämme' ,N'Halbnomadischer Stamm' ,10 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 121' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000199' ,N'Broktharstämme' ,N'Normadischer Stamm' ,10 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 121' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000200' ,N'Broktharstämme' ,N'Barbarenhorde' ,11 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000201' ,N'Broktharstämme' ,N'Gebirksstamm' ,11 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000202' ,N'Broktharstämme' ,N'Isolierter Stamm' ,8 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000203' ,N'Broktharstämme' ,N'Jägerstamm' ,12 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000204' ,N'Broktharstämme' ,N'Küstenstamm' ,11 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000205' ,N'Broktharstämme' ,N'Reiterstamm' ,12 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000206' ,N'Broktharstämme' ,N'Städtischer Stamm' ,10 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000207' ,N'Broktharstämme' ,N'Steinzeitlicher Stamm' ,10 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 122' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000208' ,N'Cromor' ,N'Cromor Frauen' ,15 ,NULL ,10 ,NULL ,0 ,0 ,0 ,1 ,0 ,0 ,1 ,0 ,0 ,4 ,0 ,N'Rakshazar 131' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000209' ,N'Cromor' ,N'Cromor Männer' ,9 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 131' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000210' ,N'Donari Pfadwandler' ,N'Donari Pfadwandler' ,14 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,2 ,0 ,N'Rakshazar 140' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000211' ,N'Ipexco Tempelstädte' ,N'Ipexco Tempelstädte' ,5 ,NULL ,14 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 152' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000212' ,N'Ipexcostämme' ,N'Dschungelstamm' ,16 ,NULL ,7 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,2 ,0 ,N'Rakshazar 161' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000213' ,N'Ipexcostämme' ,N'Küstendorf' ,16 ,NULL ,7 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,2 ,0 ,N'Rakshazar 161' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000214' ,N'Ipexcostämme' ,N'Bergdorf' ,15 ,NULL ,7 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,2 ,0 ,N'Rakshazar 161' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000215' ,N'Irrogoliten' ,N'Irrogoliten' ,17 ,NULL ,14 ,NULL ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 169' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000216' ,N'Irrogoliten' ,N'Aussätzige' ,11 ,NULL ,14 ,NULL ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 169' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000217' ,N'Jiktachkao' ,N'Jiktachkao' ,8 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,2 ,2 ,0 ,N'Rakshazar 176' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000218' ,N'Jiktachkao' ,N'Jikten' ,10 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 176' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000219' ,N'Legiten' ,N'Frei' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'Rakshazar 186' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000220' ,N'Legiten' ,N'Unfrei' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'Rakshazar 186' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000221' ,N'Nagah-Archaen' ,N'R''Sar (Priesterelite)' ,9 ,NULL ,15 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 196' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000222' ,N'Nagah-Archaen' ,N'Thin''Chha (Adel/ Herrscherkaste)' ,7 ,NULL ,15 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 196, 197' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000223' ,N'Nagah-Archaen' ,N'Ybb''Iz (Handwerkerkaste' ,6 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 197' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000224' ,N'Nagah-Archaen' ,N'Xssa''Triash' ,11 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,0 ,N'Rakshazar 197' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000225' ,N'Nagah-Archaen' ,N'Yrachi (Ausgestoßene)' ,6 ,NULL ,3 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 197,98' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000226' ,N'Nagah-Hochlanddörfer' ,N'Nagah-Hochlanddörfer' ,8 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,3 ,0 ,N'Rakshazar 205' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000227' ,N'Nagah-Sumpfländer' ,N'Nagah-Sumpfländer' ,7 ,NULL ,8 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 212' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000228' ,N'Nedermannen' ,N'Nedermannen' ,11 ,NULL ,4 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 220' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000229' ,N'Parnhai' ,N'Dreistromland-Parnhai' ,4 ,NULL ,4 ,NULL ,-1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 234' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000230' ,N'Parnhai' ,N'Olaoduori-Parnhai' ,14 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 234' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000231' ,N'Parnhai' ,N'Erishuori-Parnhai' ,14 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,N'Rakshazar 234' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000232' ,N'Prophetenlager' ,N'Prophetenlager' ,9 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 243' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000233' ,N'Rochkotaii' ,N'Rochkotaii' ,11 ,NULL ,7 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,2 ,0 ,N'Rakshazar 252' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000234' ,N'Rochkotaii' ,N'Rochkotaii Sippensiedlung' ,11 ,NULL ,7 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,2 ,0 ,N'Rakshazar 252' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000235' ,N'Ronthar' ,N'Ronthar' ,11 ,NULL ,12 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 261' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000236' ,N'Sanskitarischer Reiterorden' ,N'Sanskitarischer Reiterorden' ,12 ,NULL ,6 ,NULL ,1 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,5 ,-1 ,N'Rakshazar 271' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000237' ,N'Sanskitarische Stadtstaaten' ,N'Sanskitarische Stadtstaaten (Oberschicht)' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 281' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000238' ,N'Sanskitarische Stadtstaaten' ,N'Sanskitarische Stadtstaaten (Stadtbevölkerung)' ,4 ,NULL ,8 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 281' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000239' ,N'Sanskitarische Stadtstaaten' ,N'Sanskitarische Stadtstaaten (Landbevölkerung)' ,2 ,NULL ,8 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 281' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000240' ,N'Sirdaksippen' ,N'Sirdaksippen' ,14 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'Rakshazar 288' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000241' ,N'Slachkare' ,N'Sesshafter Stamm' ,14 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 297' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000242' ,N'Slachkare' ,N'Halbnomadischer Stamm' ,14 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 297' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000243' ,N'Slachkare' ,N'Nomadischer Stamm' ,14 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 297' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000244' ,N'Slachkare' ,N'Slachkare' ,0 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 297' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000245' ,N'Tharaisippe' ,N'Tharaisippe' ,15 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,2 ,2 ,0 ,N'Rakshazar 306' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000246' ,N'Urgashkao' ,N'Händlerkhez' ,11 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 314, 315' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000247' ,N'Urgashkao' ,N'Handwerkerhez' ,8 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 314, 315' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000248' ,N'Urgashkao' ,N'Hirtenkhez' ,10 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 314, 315' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000249' ,N'Urgashkao' ,N'Hüterkhez' ,13 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 314, 315' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000250' ,N'Urgashkao' ,N'Kämpferkhez' ,13 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 314, 315' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000251' ,N'Vaestenclans' ,N'Vaestenclans' ,12 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 327' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000252' ,N'Vaestenclans' ,N'Nördliche Clans' ,13 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 327' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000253' ,N'Vaestenclans' ,N'Südliche Clans' ,13 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,2 ,0 ,N'Rakshazar 327' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000254' ,N'Xhul Wüstenwanderer' ,N'Xhul Wüstenwanderer' ,14 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 338' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000255' ,N'Xhul Wüstenwanderer' ,N'Xhoula' ,11 ,NULL ,6 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,3 ,0 ,N'Rakshazar 338' ,N'Rakshazar')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000256' ,N'Bosparanisches Reich' ,N'Bosparanisches Reich' ,1 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 9, 10' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000257' ,N'Bosparanisches Reich' ,N'Priesterliche Professionen' ,1 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 10' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000258' ,N'Cyclopea' ,N'Cyclopea' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 10' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000259' ,N'Nordprovinzen' ,N'Gratenfelser Land' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000260' ,N'Nordprovinzen' ,N'Albernia' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000261' ,N'Nordprovinzen' ,N'Mark Vadocia' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000262' ,N'Nordprovinzen' ,N'Königreich Windehag' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000263' ,N'Nordprovinzen' ,N'Wengenholm' ,3 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000264' ,N'Nordprovinzen' ,N'Garetia' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000265' ,N'Nordprovinzen' ,N'Greifenmark' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000266' ,N'Nordprovinzen' ,N'Baliho' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000267' ,N'Nordprovinzen' ,N'Rommilyser Mark' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000268' ,N'Nordprovinzen' ,N'Perricum' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000269' ,N'Nordprovinzen' ,N'Gratenfelser Land, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000270' ,N'Nordprovinzen' ,N'Albernia, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000271' ,N'Nordprovinzen' ,N'Mark Vadocia, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000272' ,N'Nordprovinzen' ,N'Königreich Windehag, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000273' ,N'Nordprovinzen' ,N'Wengenholm, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000274' ,N'Nordprovinzen' ,N'Garetia, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000275' ,N'Nordprovinzen' ,N'Greifenmark, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000276' ,N'Nordprovinzen' ,N'Baliho, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000277' ,N'Nordprovinzen' ,N'Rommilyser Mark, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000278' ,N'Nordprovinzen' ,N'Perricum, Oberschicht' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 11' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000279' ,N'Südaventurien (dunkle Zeiten)' ,N'Südaventurien (dunkle Zeiten)' ,1 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 12' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000280' ,N'Tulamidische Kulturen' ,N'Tulamidische Kulturen' ,1 ,NULL ,10 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,N'OiC 13' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000281' ,N'Diamantenes Sultanat' ,N'Diamantenes Sultanat' ,0 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,N'OiC 13' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000282' ,N'Elem' ,N'Elem' ,4 ,7 ,15 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,N'OiC 13' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000283' ,N'Wüstenstämme' ,N'Wüstenstämme' ,4 ,NULL ,8 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 14' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000284' ,N'Al''Mada' ,N'Al''Mada' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 14' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000285' ,N'Alhanien' ,N'Alhanien' ,1 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,N'OiC 14' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000286' ,N'Grolmensippe' ,N'Grolmensippe' ,0 ,NULL ,7 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 15' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000287' ,N'Hjaldinger' ,N'Hjaldinger' ,0 ,NULL ,8 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 16' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000288' ,N'Andergast und Nostria (dunkle Zeiten)' ,N'Andergast und Nostria (dunkle Zeiten)' ,3 ,NULL ,12 ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,N'OiC 17' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000289' ,N'Orkland' ,N'Ghorinchai' ,4 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 17' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000290' ,N'Orkland' ,N'Tordochai' ,2 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,N'OiC 17' ,N'Dunkle Zeiten')
GO
INSERT INTO [Kultur] (  [KulturGUID],  [Name],  [Variante],  [GP],  [SOmin],  [SOmax],  [Voraussetzungen],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [MRMod],  [Literatur],  [Setting]) 
 VALUES ('00000000-0000-0000-0000-000000000291' ,N'Dschungelstämme' ,N'Wudu' ,1 ,NULL ,NULL ,NULL ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,N'OiC 17' ,N'Dunkle Zeiten')
GO

/* Name */

SET IDENTITY_INSERT [Name] ON
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4326 ,N'Bodar ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4327 ,N'Colga' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4328 ,N'Colgan' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4329 ,N'Connar' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4330 ,N'Danos' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4331 ,N'Debrek ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4332 ,N'Delo ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4333 ,N'Dirion' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4334 ,N'Drego' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4335 ,N'Eldan' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4336 ,N'Erlwin ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4337 ,N'Eslam' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4338 ,N'Frumol(d)' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4339 ,N'Fürchtepraios' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4340 ,N'Gerbald ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4347 ,N'Glo(m)bo ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4348 ,N'Gordan' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4349 ,N'Götterfried ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4350 ,N'Gurvan ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4351 ,N'Halman' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4352 ,N'Halrik ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4353 ,N'Hesindion ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4354 ,N'Hlûtar' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4355 ,N'Igalf' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4356 ,N'Immo ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4357 ,N'Ingrimon ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4358 ,N'Jango' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4359 ,N'Jast ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4360 ,N'Jorna' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4361 ,N'Korobar ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4362 ,N'Leuendan ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4363 ,N'Luidor' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4364 ,N'Maselrich ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4365 ,N'Menzel ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4366 ,N'Moribert' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4367 ,N'Mupert ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4368 ,N'Olger' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4369 ,N'Perainepreis ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4370 ,N'Perainhilf ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4371 ,N'Perval ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4372 ,N'Phelix' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4373 ,N'Phexje ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4374 ,N'Pisidian' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4375 ,N'Plauter ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4376 ,N'Praiodar' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4378 ,N'Praiosmar' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4379 ,N'Praiotin ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4380 ,N'Raulbrin' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4387 ,N'Rhys' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4388 ,N'Rigbald ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4389 ,N'Rondred' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4391 ,N'Sequin' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4392 ,N'Sigerain' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4393 ,N'Sigman' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4394 ,N'Storko ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4395 ,N'Tabbert ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4396 ,N'Tanglan ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4397 ,N'Thidan' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4398 ,N'Tjolme ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4399 ,N'Tolak ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4400 ,N'Torm ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4401 ,N'Tsajan ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4402 ,N'Tsathalan ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4403 ,N'Tydor ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4404 ,N'Ucuriel' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4405 ,N'Udalf' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4406 ,N'Udildor' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4407 ,N'Uriel ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4408 ,N'Valman' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4410 ,N'Xandro ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4411 ,N'Xeppert' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4412 ,N'Xerwulf ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4413 ,N'Afra' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4414 ,N'Alrike' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4415 ,N'Alwene' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4416 ,N'Ardare' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4417 ,N'Ayla ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4418 ,N'Boroniane ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4419 ,N'Coris' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4420 ,N'Dirione' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4421 ,N'Dralina' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4422 ,N'Elsebeth' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4424 ,N'Emmea' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4425 ,N'Firunai' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4426 ,N'Gera ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4427 ,N'Ginte ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4428 ,N'Glenna' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4429 ,N'Greifriede ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4430 ,N'Hannafrid' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4432 ,N'Heiltrude' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4433 ,N'Ilpetta ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4434 ,N'Ingrimiane' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4435 ,N'Jadwiga' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4436 ,N'Khalidai ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4437 ,N'Lara' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4439 ,N'Leodane ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4440 ,N'Leonore' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4441 ,N'Leudane ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4442 ,N'Leuemara ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4443 ,N'Linai' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4444 ,N'Lutisana' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4445 ,N'Myrica ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4446 ,N'Noiona ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4447 ,N'Perainetreu' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4448 ,N'Phecaja ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4449 ,N'Phejanka' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4450 ,N'Phexla ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4451 ,N'Pomona' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4452 ,N'Praiadane' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4453 ,N'Prajosmin' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4454 ,N'Ragnar' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4456 ,N'Rahjade' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4457 ,N'Rohaja ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4458 ,N'Rondara ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4459 ,N'Rondraga ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4460 ,N'Rudane ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4462 ,N'Sari' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4463 ,N'Shannah ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4464 ,N'Susu' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4465 ,N'Travhild ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4466 ,N'Traviane ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4467 ,N'Tsabine ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4468 ,N'Tsaje' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4469 ,N'Tsalinde' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4470 ,N'Ucurinai ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4473 ,N'Wlimai' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4474 ,N'Xeta' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4475 ,N'Yasinai ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4476 ,N'Yelinde ' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4477 ,N'Yola' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4479 ,N'Zidonai' ,N'Garethische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4480 ,N'Ackerfrau' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4481 ,N'Ackergsind' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4482 ,N'Ackerknecht' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4483 ,N'Ackerleut' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4484 ,N'Ackermagd' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4485 ,N'Ackermann' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4486 ,N'Afteiker' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4487 ,N'Ahlenschmiede' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4488 ,N'Alriksröder' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4489 ,N'Alt' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4490 ,N'Altlapper' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4493 ,N'Bachfegerin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4494 ,N'Bader' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4495 ,N'Bartenwerper' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4496 ,N'Bäurin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4499 ,N'Bierwirth' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4501 ,N'Bogner' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4502 ,N'Braidan' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4503 ,N'Brettschneider' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4504 ,N'Brieger' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4505 ,N'Brodbäck' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4506 ,N'Brüher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4507 ,N'Buchweiz' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4509 ,N'Butterweck' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4510 ,N'Carben' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4514 ,N'Darpater' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4516 ,N'Dinckel' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4517 ,N'Dinkelkorn' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4518 ,N'Dorcken' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4519 ,N'Dreher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4522 ,N'Esleborn' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4523 ,N'Faßbinder' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4524 ,N'Fassmacher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4525 ,N'Feilenschmidt' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4526 ,N'Ferdokerin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4529 ,N'Fischerin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4530 ,N'Fleckschneider' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4531 ,N'Flickenrock' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4532 ,N'Flickenschuh' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4533 ,N'Flößler' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4535 ,N'Fuhrfrau' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4538 ,N'Garetherin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4539 ,N'Geißhirt' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4541 ,N'Gerber' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4544 ,N'Gerstenkorn' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4545 ,N'Gertenwald' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4548 ,N'Goblinhuser' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4551 ,N'Gropengießer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4552 ,N'Groß' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4554 ,N'Grunder' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4555 ,N'Hafergarb' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4556 ,N'Hafermeel' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4558 ,N'Hanfner' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4559 ,N'Hangemann' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4560 ,N'Harkasrodener' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4563 ,N'Harzscharrer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4564 ,N'Haspel' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4565 ,N'Hauer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4568 ,N'Hirtin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4569 ,N'Höker' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4570 ,N'Holzhauerin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4576 ,N'Hutmacher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4578 ,N'Jetsam' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4579 ,N'Jochmeier' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4581 ,N'Jung' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4582 ,N'Karde' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4583 ,N'Karolus' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4584 ,N'Kauderer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4586 ,N'Kerres' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4587 ,N'Kippwipper' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4588 ,N'Kleiber' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4589 ,N'Klein' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4590 ,N'Klüttenbäcker' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4591 ,N'Knochenheuer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4592 ,N'Kohlenbranter' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4593 ,N'Kohlenbrenner' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4594 ,N'Kohlköchel' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4598 ,N'Kramer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4599 ,N'Krondal' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4600 ,N'Kruger' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4601 ,N'Küfer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4602 ,N'Kuttelsieder' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4603 ,N'Landsknecht' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4605 ,N'Leisten' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4606 ,N'Linneweber' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4607 ,N'Linsenmaier' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4608 ,N'Linsenmeier' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4609 ,N'Lohgar' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4611 ,N'Machandel' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4612 ,N'Marktender' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4616 ,N'Mazker' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4620 ,N'Oberndorfer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4623 ,N'Ölschläger' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4624 ,N'Otresker' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4626 ,N'Panzerschmiedin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4627 ,N'Pecher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4628 ,N'Perensen' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4630 ,N'Peutler' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4631 ,N'Plattklopper' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4633 ,N'Pogel' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4634 ,N'Porst' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4635 ,N'Pulether' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4638 ,N'Reepschläger' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4639 ,N'Reisige' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4640 ,N'Riemerin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4641 ,N'Rocken' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4644 ,N'Roggenfeldt' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4645 ,N'Rottmeister' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4648 ,N'Schacherer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4649 ,N'Schinder' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4650 ,N'Schlunder' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4651 ,N'Schnitter' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4652 ,N'Schröter' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4653 ,N'Schultheiß' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4655 ,N'Sonnfelder' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4656 ,N'Spengler' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4657 ,N'Sprichbrecher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4658 ,N'Steckenknecht' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4661 ,N'Steinhauerin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4662 ,N'Strählmacher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4669 ,N'Trutzbacher' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4673 ,N'Unterbauer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4674 ,N'Uplegger' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4675 ,N'Venichel' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4676 ,N'Wagner' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4677 ,N'Weidener' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4678 ,N'Werg' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4679 ,N'Winkelhauser' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4682 ,N'Winzer' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4683 ,N'Wirker' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4684 ,N'Wirtin' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4685 ,N'Wollschlägl' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4687 ,N'Zaumschmied' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4691 ,N'Zwirnlein' ,N'Garethische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4692 ,N'Bennain ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Albernia) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4693 ,N'Binsböckel ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4694 ,N'Bregelsaum ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Warunk) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4695 ,N'Conchobair ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4696 ,N'Crumold ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4697 ,N'Culming ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4698 ,N'Darbonia' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4699 ,N'Dunkelstein ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4700 ,N'Eberstamm' ,N'Garethische Namen' ,N'Nachname, Adel' ,N' (Kosch) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4701 ,N'Ehrenstein ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Tobrien) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4702 ,N'Eslamabad ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4703 ,N'Eslamsgrund ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Beilunk) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4704 ,N'Faldahon ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4705 ,N'Falkenhag' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4706 ,N'Galahan ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4707 ,N'Gareth ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(kaiserlich/Garethien&amp;Almada) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4708 ,N'Greifax ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4709 ,N'Grötz Hardenfels' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4710 ,N'Hartsteen ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4711 ,N'Hirschfurten ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4712 ,N'Ibenburg ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4713 ,N'Löwenhaupt ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Weiden) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4714 ,N'Luring ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4715 ,N'Mersingen ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4716 ,N'Ni/Ui Llud ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4717 ,N'Pandlaril ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4718 ,N'Quandt ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4719 ,N'Quintian' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4720 ,N'Rabenmund ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Darpatien) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4721 ,N'Ragathsquell ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4722 ,N'Sanin ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Windhag) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4723 ,N'Schetzeneck ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4724 ,N'Schnattermoor ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4725 ,N'Sighelms Halm' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4726 ,N'Spogelsen ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4727 ,N'Stepahan ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4728 ,N'Stippwitz ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4729 ,N'Streitzig ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4730 ,N'Sturmfels ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4731 ,N'vom Berg ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4732 ,N'vom Großen Fluß' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Nordmarken) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4733 ,N'Weißenstein' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4734 ,N'Wengenholm ' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4735 ,N'Wertlingen ' ,N'Garethische Namen' ,N'Nachname, Adel' ,N'(Greifenfurt) ' ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4736 ,N'Zweifelfels' ,N'Garethische Namen' ,N'Nachname, Adel' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4737 ,N'Adailoé' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4738 ,N'Ahillea' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4739 ,N'Ailrianrod' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4740 ,N'Alaafiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4744 ,N'Alién' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4746 ,N'Alliaria' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4747 ,N'Alriel' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4748 ,N'Amarandel' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4749 ,N'Anasasiel' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4750 ,N'Anilinoé' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4751 ,N'Anion' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4752 ,N'Arikarion' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4753 ,N'Arion' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4754 ,N'Assinibion' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4755 ,N'Athabascael' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4756 ,N'Avalarion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4757 ,N'Aythya ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4758 ,N'Beanseidhe ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4759 ,N'Binneadriel' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4760 ,N'Caerleon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4761 ,N'Cairlinn ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4762 ,N'Cation ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4763 ,N'Cayuga ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4764 ,N'Céilibanna ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4765 ,N'Cinnamon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4766 ,N'Clairseana ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4767 ,N'Déhaoine' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4768 ,N'Diundriel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4769 ,N'Eilidiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4770 ,N'Eire ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4771 ,N'Eldariel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4772 ,N'Eriu ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4773 ,N'Erlcoron ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4774 ,N'Faelandel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4775 ,N'Failtiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4776 ,N'Fanaion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4777 ,N'Fermion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4778 ,N'Feynyalá' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4779 ,N'Feysiriel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4781 ,N'Floriel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4782 ,N'Gohálainn ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4783 ,N'Hiavatha ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4784 ,N'Imalaya ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4785 ,N'Iníon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4786 ,N'Iscoron ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4787 ,N'Kildare' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4788 ,N'Loriniel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4789 ,N'Manatiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4790 ,N'Mandaniel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4791 ,N'Miamiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4792 ,N'Navahon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4793 ,N'Nifrediel' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4794 ,N'Nurdhavalon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4795 ,N'Nyrociel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4797 ,N'Onioniel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4798 ,N'Osagiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4799 ,N'Raialamone ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4800 ,N'Rhianna ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4801 ,N'Rilaona ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4802 ,N'Saliniome ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4803 ,N'Sanaha' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4804 ,N'Seminolé ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4805 ,N'Senecion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4806 ,N'Shannon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4807 ,N'Shawneé ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4808 ,N'Shikasóron ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4809 ,N'Silanoliel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4810 ,N'Silmarillion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4811 ,N'Sionon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4812 ,N'Tatanka' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4814 ,N'Tuscaroria ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4815 ,N'Valandriel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4816 ,N'Vernel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4817 ,N'Vindariel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4818 ,N'Yarvala ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4819 ,N'Yicarilla ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4820 ,N'Yutahon' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4821 ,N'Aimsir ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4822 ,N'Alriel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4823 ,N'Anasasiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4824 ,N'Anion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4825 ,N'Ariacron ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4826 ,N'Arion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4827 ,N'Assinibion' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4828 ,N'Athabascael ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4829 ,N'Bandonion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4830 ,N'Caerleon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4831 ,N'Cailiil ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4832 ,N'Calimerion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4833 ,N'Calomel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4834 ,N'Cation' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4835 ,N'Cinnamon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4836 ,N'Clingoniel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4837 ,N'Diundradhar ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4838 ,N'Dungeon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4839 ,N'Éireannan ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4840 ,N'Elanor ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4842 ,N'Eriu ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4843 ,N'Erlcoron ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4844 ,N'Esoteriel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4845 ,N'Fehromon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4846 ,N'Fehrsil ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4847 ,N'Fermion ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4848 ,N'Ganglion' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4849 ,N'Gohálainn ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4850 ,N'Helauneval ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4851 ,N'Kiovar ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4852 ,N'Lakotavar ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4853 ,N'Lauriel' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4855 ,N'Mandaniel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4856 ,N'Miamiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4857 ,N'Monahan ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4859 ,N'Nurdhavalon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4860 ,N'Nyrociel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4862 ,N'Oneidavar ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4863 ,N'Onioniel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4864 ,N'Onondagan ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4865 ,N'Osagiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4866 ,N'Saladir ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4867 ,N'Setanta' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4868 ,N'Shannon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4869 ,N'Shirókiel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4870 ,N'Silanoliel ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4871 ,N'Sionon ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4872 ,N'Skyvaheri ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4873 ,N'Trálír ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4874 ,N'Valayar ' ,N'Elfische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4877 ,N'Harrkara ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4878 ,N'Ifrunna ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4879 ,N'Meruscha ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4880 ,N'Thorkat ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4881 ,N'Tulmath ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4882 ,N'Yurrgira' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4883 ,N'Alrogh ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4884 ,N'Bannoch ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4885 ,N'Braghad ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4886 ,N'Domnach ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4887 ,N'Dundan ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4888 ,N'Durtacht ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4889 ,N'Fredon ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4890 ,N'Lannoch ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4891 ,N'Marfogh ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4892 ,N'Murdoch ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4893 ,N'Sadrugh ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4894 ,N'Ullachan ' ,N'Gjalskerländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4895 ,N'Balrork ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4896 ,N'Baßtorg ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4897 ,N'Blorg ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4898 ,N'Goras ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4899 ,N'Grakvach ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4900 ,N'Grärch ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4901 ,N'Graulunkh ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4902 ,N'Grod ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4903 ,N'Gruufhairork ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4904 ,N'Gushrok ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4905 ,N'Hairork ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4907 ,N'Joruk ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4908 ,N'Karrag ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4909 ,N'Knurrz ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4910 ,N'Krrachtt' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4911 ,N'Kunzech ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4913 ,N'Morkash ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4914 ,N'Schlagto(gh) ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4915 ,N'Sharkhush ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4916 ,N'Shurda ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4917 ,N'Stickrott ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4918 ,N'Ugrashak ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4919 ,N'Ulughbeg ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4920 ,N'Urgrushak ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4921 ,N'Wosz(uch) ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4922 ,N'Yurek ' ,N'Orkische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4923 ,N'Agaguk ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4924 ,N'Alrikinnen ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4925 ,N'Amgigh ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4927 ,N'Ebnajok ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4928 ,N'Eggulik ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4929 ,N'Eikka ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4930 ,N'Em Sen' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4931 ,N'Finjhon' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4932 ,N'Jannojek ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4933 ,N'Jasu' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4934 ,N'JasuVaehn ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4935 ,N'Kanstai ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4936 ,N'Kayugh ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4937 ,N'Kiamu' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4938 ,N'Lafko ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4939 ,N'Lanak ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4940 ,N'Loschim' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4941 ,N'Määnan ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4942 ,N'Nanuk ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4943 ,N'Neajo' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4944 ,N'Nejhan' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4945 ,N'Niku ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4946 ,N'Nukka ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4947 ,N'Pekka ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4948 ,N'Pettajem ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4949 ,N'Pokku ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4950 ,N'Poukai' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4951 ,N'Rikka ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4952 ,N'Riku ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4953 ,N'Tsefajok ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4954 ,N'Tupilak ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4955 ,N'Türjuk ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4956 ,N'Ulo ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4957 ,N'Valeninnen ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4958 ,N'Akka ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4959 ,N'Beri' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4960 ,N'BeriMel ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4961 ,N'Chagak ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4962 ,N'Guaäna' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4963 ,N'Harikaju ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4964 ,N'Iksi ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4965 ,N'Ila ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4966 ,N'Inijok ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4967 ,N'Janaha' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4968 ,N'Kiin ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4969 ,N'Kostora ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4970 ,N'Kunura ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4971 ,N'Lathi ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4972 ,N'Naij' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4973 ,N'Neli' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4974 ,N'Savolinju ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4975 ,N'Tiali' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4980 ,N'Valeninnen ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4981 ,N'Akka ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4982 ,N'Beri' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4983 ,N'BeriMel ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4984 ,N'Chagak ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4985 ,N'Guaäna' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4986 ,N'Harikaju ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4987 ,N'Iksi ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4988 ,N'Ila ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4989 ,N'Inijok ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4990 ,N'Janaha' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4991 ,N'Kiin ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4992 ,N'Kostora ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4993 ,N'Kunura ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4994 ,N'Lathi ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (4995 ,N'Naij' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5009 ,N'Tugidag ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5010 ,N'Valla' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5013 ,N'Wauda ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5014 ,N'Wren ' ,N'Nivesische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5015 ,N'Agnitha' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5016 ,N'Arika ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5017 ,N'Besgra' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5018 ,N'Bilkis' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5019 ,N'Bisminka' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5020 ,N'Bite' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5021 ,N'Bitescha' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5022 ,N'Dalkeshja' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5023 ,N'Gerjuscha ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5024 ,N'Hanija' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5025 ,N'Hashandra' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5026 ,N'Hirjuscha ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5027 ,N'Imjaschala' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5028 ,N'Immja ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5029 ,N'Jalna ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5030 ,N'Karenka ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5031 ,N'Kitinkaja ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5032 ,N'Koljuscha ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5033 ,N'Koschka ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5034 ,N'Lorsija' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5035 ,N'Maruscha ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5036 ,N'Merischja' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5037 ,N'Mokaschka ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5038 ,N'Obanuschka ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5039 ,N'Poljita ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5040 ,N'Rasescha' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5041 ,N'Titjanka ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5042 ,N'Wladidere ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5043 ,N'Wladiseveria ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5044 ,N'Yilbakis' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5045 ,N'Ahani' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5046 ,N'Alriksej ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5047 ,N'Dengizik ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5057 ,N'Ellak ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5058 ,N'Ernak ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5059 ,N'Etzel ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5060 ,N'Freoj' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5061 ,N'Hamakil' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5062 ,N'Kwaßimir ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5063 ,N'Larik ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5064 ,N'Lorsij' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5065 ,N'Mischajil' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5066 ,N'Mokoschko ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5067 ,N'Mundschuk ' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5068 ,N'Sildrojan' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5069 ,N'Sildroyan' ,N'Norbardische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5070 ,N'Abrinken' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5071 ,N'Aljeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5072 ,N'Arlin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5073 ,N'Arrastin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5074 ,N'Atranzig' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5075 ,N'Bagoltin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5076 ,N'Bartineff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5077 ,N'Barvedis' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5078 ,N'Bilenzig' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5079 ,N'Bolscheff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5080 ,N'Burtinen ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5081 ,N'Butanjeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5082 ,N'Chadjeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5083 ,N'Choprutin ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5084 ,N'Daginen ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5085 ,N'Dagoneff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5086 ,N'Dallentin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5087 ,N'Daprusek' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5088 ,N'Dimrinen' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5089 ,N'Dogeljeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5090 ,N'Dracul ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5091 ,N'Dukatajeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5092 ,N'Elin ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5093 ,N'Emaneff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5094 ,N'Erginen ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5095 ,N'Etajeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5096 ,N'Eugoltin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5097 ,N'Fagjeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5098 ,N'Fantinen' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5099 ,N'Ferjeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5100 ,N'Filajeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5101 ,N'Firnin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5102 ,N'Fogil' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5103 ,N'Fogujeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5104 ,N'Frantischeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5105 ,N'Fruginen' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5106 ,N'Gamajeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5107 ,N'Garkinen' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5108 ,N'Gerjeleff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5109 ,N'Gertainig' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5110 ,N'Gorening' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5111 ,N'Hardering' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5112 ,N'Helajeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5113 ,N'Horminem' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5114 ,N'Hurlemaneff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5115 ,N'Ibrajeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5116 ,N'Ijineff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5117 ,N'Imonin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5118 ,N'Itolojeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5119 ,N'Janeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5120 ,N'Janig' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5121 ,N'Jarusek ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5122 ,N'Jataneff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5123 ,N'Jeninen ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5124 ,N'Jikajeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5125 ,N'Jonkjeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5126 ,N'Jurgavist ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5127 ,N'Kereling' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5128 ,N'Koranzig ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5129 ,N'Kowalejeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5130 ,N'Kwaßtschuk ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5131 ,N'Kwaßzek ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5132 ,N'Lenejeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5133 ,N'Linerajeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5134 ,N'Loranin ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5135 ,N'Lugoltin ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5136 ,N'Mandragjeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5137 ,N'Marginen' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5138 ,N'Meskinnskoff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5139 ,N'Mogoljeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5140 ,N'Nogil' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5141 ,N'Nurkajeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5142 ,N'Olscheff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5143 ,N'Otaninen' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5144 ,N'Persanzig ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5145 ,N'Porgajeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5146 ,N'Reschgin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5147 ,N'Schlemilskij ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5148 ,N'Schurawij' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5149 ,N'Sewerin' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5150 ,N'Sewertschik ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5151 ,N'Sievening ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5152 ,N'Surjeloff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5153 ,N'Tschepesch ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5154 ,N'Tsirkevist ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5155 ,N'Tureljeff ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5156 ,N'Ugradin ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5157 ,N'Watruschkin ' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5158 ,N'Wodjadeff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5159 ,N'Wodkaroff' ,N'Norbardische Namen (Sippenname)' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5160 ,N'Barl' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5161 ,N'Bluurz ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5162 ,N'Buup ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5163 ,N'Buurp ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5164 ,N'Chruuk' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5165 ,N'Gnatz' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5166 ,N'Gnupp' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5167 ,N'Goglmogl ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5168 ,N'Golluum ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5169 ,N'Golubtsch ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5170 ,N'Gorrwarrn' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5172 ,N'Gragh' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5173 ,N'Groggi ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5174 ,N'Grollfang ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5175 ,N'Groom' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5176 ,N'Grootz' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5177 ,N'Grul' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5178 ,N'Guurk ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5179 ,N'Heiki ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5180 ,N'Jaahikör ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5181 ,N'Kurgan ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5182 ,N'Norf' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5183 ,N'Örf' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5184 ,N'Paabertz' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5185 ,N'Priidul' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5186 ,N'Reelik S' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5187 ,N'Röff' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5188 ,N'Rulpep' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5189 ,N'Sniigs' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5190 ,N'Suviini' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5191 ,N'Tschak' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5192 ,N'Ulrik ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5193 ,N'Url ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5194 ,N'Vurl' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5195 ,N'Wutz ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5196 ,N'Ziplim' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5197 ,N'Boorsta ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5198 ,N'Bunugga ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5199 ,N'Bupuua ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5200 ,N'Bupuuscha' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5201 ,N'Furka' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5204 ,N'Griibna' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5205 ,N'Haarriika ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5206 ,N'Jacuula' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5207 ,N'Kääbus ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5210 ,N'Kasuukas ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5211 ,N'Kergatai' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5212 ,N'Khischa' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5213 ,N'Knuudze ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5214 ,N'Kunga ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5215 ,N'Kungutzka ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5216 ,N'Leelos' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5217 ,N'Lepuua ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5218 ,N'Maalis' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5219 ,N'Mantka ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5220 ,N'Mikdai' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5221 ,N'Orvazz' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5222 ,N'Porsa' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5225 ,N'Riiba ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5226 ,N'Rukuuka' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5227 ,N'Russuula' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5228 ,N'Ruulka' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5229 ,N'Sakuuska' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5232 ,N'Sieva ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5233 ,N'Siisikai ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5234 ,N'Siiskikai' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5235 ,N'Suncuua ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5236 ,N'Suula ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5237 ,N'Suuscrofa ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5238 ,N'Suviintan' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5239 ,N'Triinuun ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5240 ,N'Upuschanna ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5241 ,N'Urkununna ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5242 ,N'Ushka ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5243 ,N'Varmiias ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5244 ,N'Wjassuula ' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5245 ,N'Yaakscha' ,N'Goblinische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5249 ,N'Boronje ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5250 ,N'Dragodane ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5251 ,N'Haraike ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5252 ,N'Jovanka ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5257 ,N'Permine ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5258 ,N'Phexuschka ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5259 ,N'Praioschka ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5260 ,N'Rondrewka ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5261 ,N'Slobodna ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5262 ,N'Stine ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5263 ,N'Travera ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5264 ,N'Travilena ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5265 ,N'Alderich ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5266 ,N'Alrijew ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5267 ,N'Alriksej ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5268 ,N'Anshag ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5269 ,N'Arlrik ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5270 ,N'Baumdragoslav ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5271 ,N'Festo ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5272 ,N'Laranko ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5273 ,N'Perainislaus ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5274 ,N'Perainiwan ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5275 ,N'Praioslaw ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5276 ,N'Travigor ' ,N'Bornländische Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5277 ,N'Brutalinskij ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5278 ,N'Festumske ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5279 ,N'Glumske ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5280 ,N'Gumplew ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5281 ,N'Gurkij' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5282 ,N'Jammerspecht ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5283 ,N'Kapustoj ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5284 ,N'Opskurjeff' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5285 ,N'Skurilske' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5286 ,N'Spacken ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5287 ,N'Wraschummske ' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5288 ,N'Würzen' ,N'Bornländische Namen' ,N'Nachname' ,NULL ,NULL ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5289 ,N'Äarl ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5290 ,N'Adelgunde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5291 ,N'Alrike ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5292 ,N'Amseltraud ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5293 ,N'Arnegund ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5294 ,N'Bärtha ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5295 ,N'Bärtild ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5296 ,N'Bibernell ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5297 ,N'Birsel' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5298 ,N'Bugga ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5299 ,N'Clothild ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5300 ,N'Dorntrud ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5301 ,N'Dülga ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5302 ,N'Dythlind ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5303 ,N'Elftraud ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5304 ,N'Erlwidda ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5305 ,N'Ermentrud ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5306 ,N'Eschwidde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5307 ,N'Espe ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5308 ,N'Faduhenne ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5309 ,N'Fastrade ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5310 ,N'Feengunde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5311 ,N'Feenholde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5312 ,N'Firre ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5313 ,N'Firunwid ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5314 ,N'Freulinde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5315 ,N'Frodegard ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5316 ,N'Frohgemuthe' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5317 ,N'Gedrut ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5318 ,N'Gelda ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5319 ,N'Gerfruw ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5320 ,N'Gerswind ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5321 ,N'Götterliep ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5322 ,N'Griniguld ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5323 ,N'Gundel' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5324 ,N'Gwiduhenna ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5325 ,N'Haarika ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5326 ,N'Hadumaid ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5327 ,N'Hadwig ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5328 ,N'Hartfrau ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5329 ,N'Heerfrau ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5330 ,N'Hehrfrauwe ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5331 ,N'Heidelinde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5334 ,N'Henny' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5335 ,N'Herdwig ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5336 ,N'Herrad ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5337 ,N'Hild ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5338 ,N'Hildelind ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5339 ,N'Hildigund ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5340 ,N'Holdwiep ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5341 ,N'Ifirgund ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5342 ,N'Ilfwudd ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5346 ,N'Ilmengart ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5347 ,N'Immengard ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5348 ,N'Ingolinde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5349 ,N'Ingunde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5350 ,N'Irmenrella ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5351 ,N'Irmingund ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5352 ,N'Isira ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5353 ,N'Jarlind(e) ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5354 ,N'Knechthild ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5355 ,N'Knorrholde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5356 ,N'Leuefrau ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5357 ,N'Leuintreue ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5358 ,N'Luitpercht ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5359 ,N'Luzelin ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5360 ,N'Madelgard ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5361 ,N'Matis' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5362 ,N'Mirnhild(a) ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5363 ,N'Muntliep ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5364 ,N'Odila ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5365 ,N'Osme ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5366 ,N'Peraindis ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5367 ,N'Perainetreue ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5368 ,N'Perchtrade' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5369 ,N'Perchtrudis ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5370 ,N'Permine ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5371 ,N'Praiowine ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5372 ,N'Radegunde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5373 ,N'Rechhild ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5374 ,N'Richild ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5375 ,N'Rondrada ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5376 ,N'Rondralieb ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5377 ,N'Rondrharika ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5378 ,N'Roßwid ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5379 ,N'Rotrud ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5380 ,N'Ruodhaid ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5381 ,N'Selinde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5382 ,N'Sigisburg ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5383 ,N'Stine ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5384 ,N'Sumunelda ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5385 ,N'Tenxwind ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5386 ,N'Thargrin' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5387 ,N'Torbenia ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5388 ,N'Travegunde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5389 ,N'Undra ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5390 ,N'Waidgund ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5391 ,N'Waldrada ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5392 ,N'Waliburia ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5393 ,N'Walla' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5394 ,N'Waltrude ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5395 ,N'Widumaid ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5396 ,N'Wigdis ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5397 ,N'Wilimai ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5398 ,N'Wittefrau ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5399 ,N'Wolfgard ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5400 ,N'Yalagunde ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'w' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5401 ,N'Aarwulf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5402 ,N'Ailrik ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5403 ,N'Aldifreid ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5404 ,N'Arlan ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5405 ,N'Arve ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5406 ,N'Avon ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5407 ,N'Barl' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5408 ,N'Bärnhelm ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5409 ,N'Bäromar ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5410 ,N'Beusel ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5411 ,N'Borkfried ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5412 ,N'Brinne ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5413 ,N'Brinulf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5414 ,N'Brun ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5415 ,N'Bunsenplauter' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5416 ,N'Childewich ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5417 ,N'Cloduar ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5418 ,N'Dingel ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5419 ,N'Dornwill ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5420 ,N'Dyderich ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5421 ,N'Einhard ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5422 ,N'Eitel ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5423 ,N'Ermanerich ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5424 ,N'Etzel ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5425 ,N'Eulrich ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5426 ,N'Falk ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5429 ,N'Feyenholdt ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5431 ,N'Firung ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5432 ,N'Frieder ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5433 ,N'Fuchstreu ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5434 ,N'Fulrad ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5435 ,N'Geisebrecht ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5436 ,N'Geiserich ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5437 ,N'Genja ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5438 ,N'Gerbald' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5439 ,N'Gernwald ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5440 ,N'Gilm ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5441 ,N'Gisbert ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5442 ,N'Giselher ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5443 ,N'Giselwulf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5444 ,N'Gorge ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5445 ,N'Götterfried ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5446 ,N'Götterhold ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5447 ,N'Grimmwulf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5448 ,N'Gunthar ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5449 ,N'Hadumar ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5450 ,N'Hagen ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5451 ,N'Harger ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5452 ,N'Hartuwal ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5453 ,N'Helmbrecht' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5454 ,N'Hlutar ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5455 ,N'Hogg ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5456 ,N'Holder ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5457 ,N'Hopfenfried ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5458 ,N'Ingram ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5459 ,N'Isegrein ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5460 ,N'Jann ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5461 ,N'Jargold ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5462 ,N'Jarl ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5463 ,N'Jarlak ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5464 ,N'Jarlan' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5465 ,N'Josold ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5466 ,N'Kalping ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5467 ,N'Kunrad ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5468 ,N'Landerich ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5469 ,N'Leoderich ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5470 ,N'Leuegrimm ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5471 ,N'Leuintreu ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5472 ,N'Linnart ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5473 ,N'Liudolf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5474 ,N'Liutwalt ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5475 ,N'Luitpoldt ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5476 ,N'Luitprandt ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5477 ,N'Luppe ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5478 ,N'Marbert ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5479 ,N'Meinloh ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5480 ,N'Merowech ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5481 ,N'Nalle' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5482 ,N'Naul ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5483 ,N'Neidhardt ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5484 ,N'Nille ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5485 ,N'Nolle ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5486 ,N'Olein ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5487 ,N'Perainepreis ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5488 ,N'Perainfried ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5489 ,N'Perainumund ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5490 ,N'Phexdan' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5491 ,N'Praiowein ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5492 ,N'Radbod ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5493 ,N'Radulf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5494 ,N'Rainald ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5495 ,N'Ralmir ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5496 ,N'Richolf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5497 ,N'Rondrallrik ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5500 ,N'Rondrymir ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5501 ,N'Rude(gar) ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5502 ,N'Ruodlieb ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5503 ,N'Sigismer ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5504 ,N'Sigiswild ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5505 ,N'Streitward ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5506 ,N'Tankred ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5507 ,N'Thordenin ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5508 ,N'Tirold' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5509 ,N'Trasamund ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5510 ,N'Trautmann ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5511 ,N'Travihold ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5512 ,N'Uffe ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5513 ,N'Ugdalf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5514 ,N'Ulfing' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5515 ,N'Uribert ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5516 ,N'Vulcomar ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5517 ,N'Wahlafried ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5518 ,N'Waidhardt ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5519 ,N'Walbrord ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5520 ,N'Waldbert ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5521 ,N'Waldhold ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5522 ,N'Wallfried' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5523 ,N'Widufred ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5524 ,N'Willehalm ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5525 ,N'Wolfhard ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5526 ,N'Wulf ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
INSERT INTO [Name] (  [NameID],  [Name],  [Herkunft],  [Art],  [Bedeutung],  [Geschlecht],  [KeineVorsilbe],  [KeineNachsilbe]) 
 VALUES (5527 ,N'Wunnemar ' ,N'Weidener Namen' ,N'Vorname' ,NULL ,N'm' ,0 ,0)
GO
SET IDENTITY_INSERT [Name] OFF
GO
ALTER TABLE [Name] ALTER COLUMN [NameID] IDENTITY (5528,1)
GO


/* Rüstung */

INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000076' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,4 ,4 ,0 ,0 ,1 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000077' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,0 ,1 ,60 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000078' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,1 ,0 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000079' ,NULL ,1 ,N'Z' ,0 ,0 ,0 ,0 ,2 ,2 ,0 ,0 ,0 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000080' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,3 ,3 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000081' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,3 ,3 ,0 ,0 ,0 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000082' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,1 ,0 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000083' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,3 ,3 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000084' ,NULL ,0 ,N'Z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000085' ,NULL ,2 ,NULL ,0 ,4 ,4 ,4 ,0 ,0 ,0 ,0 ,0 ,5 ,3 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000086' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000087' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000088' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,1 ,0 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000089' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,3 ,3 ,1 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000090' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,3 ,3 ,1 ,0 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000091' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000092' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,3 ,3 ,1 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000093' ,NULL ,2 ,NULL ,0 ,4 ,4 ,4 ,0 ,0 ,0 ,0 ,2 ,4 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000094' ,NULL ,1 ,NULL ,0 ,4 ,4 ,4 ,0 ,0 ,0 ,0 ,2 ,4 ,3 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000095' ,NULL ,0 ,N'Z' ,0 ,5 ,5 ,5 ,2 ,2 ,2 ,2 ,4 ,3 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000096' ,NULL ,0 ,N'Z' ,0 ,5 ,5 ,5 ,3 ,3 ,4 ,4 ,4 ,6 ,4 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000097' ,NULL ,0 ,N'Z' ,0 ,2 ,0 ,2 ,0 ,0 ,0 ,0 ,1 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000098' ,NULL ,2 ,N'Z' ,0 ,2 ,0 ,2 ,0 ,0 ,0 ,0 ,1 ,2 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000099' ,NULL ,0 ,N'Z' ,0 ,3 ,0 ,3 ,0 ,0 ,0 ,0 ,1 ,3 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000100' ,NULL ,0 ,N'Z' ,0 ,3 ,0 ,3 ,0 ,0 ,0 ,0 ,1 ,3 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000101' ,NULL ,0 ,N'Z' ,0 ,2 ,0 ,2 ,0 ,0 ,0 ,0 ,1 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000102' ,NULL ,0 ,N'Z' ,0 ,3 ,0 ,3 ,0 ,0 ,0 ,0 ,1 ,3 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000103' ,NULL ,0 ,N'Z' ,0 ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000104' ,NULL ,0 ,N'Z' ,3 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000105' ,NULL ,0 ,NULL ,0 ,1 ,1 ,1 ,0 ,0 ,1 ,1 ,1 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000106' ,NULL ,0 ,NULL ,0 ,3 ,3 ,3 ,0 ,0 ,0 ,0 ,2 ,3 ,3 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000107' ,NULL ,0 ,NULL ,0 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000108' ,NULL ,0 ,N'Z' ,3 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000109' ,NULL ,0 ,NULL ,0 ,3 ,3 ,3 ,1 ,1 ,2 ,2 ,2 ,5 ,5 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000110' ,NULL ,0 ,N'Z' ,0 ,1 ,2 ,0 ,1 ,1 ,1 ,1 ,1 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000111' ,NULL ,0 ,N'Z' ,5 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000112' ,NULL ,0 ,N'Z' ,5 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000113' ,NULL ,0 ,N'Z' ,5 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000114' ,NULL ,0 ,N'Z' ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000115' ,NULL ,0 ,N'Z' ,3 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000116' ,NULL ,0 ,N'Z' ,0 ,4 ,4 ,4 ,3 ,3 ,2 ,2 ,3 ,4 ,3 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000117' ,NULL ,0 ,N'Z' ,0 ,4 ,4 ,4 ,0 ,0 ,0 ,0 ,2 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000118' ,NULL ,0 ,NULL ,0 ,3 ,0 ,3 ,0 ,0 ,0 ,0 ,1 ,3 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000119' ,NULL ,1 ,NULL ,0 ,5 ,5 ,5 ,0 ,0 ,3 ,3 ,4 ,4 ,3 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000120' ,NULL ,1 ,N'Z' ,3 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000121' ,NULL ,0 ,N'Z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000122' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,2 ,0 ,0 ,2 ,2 ,1 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000123' ,NULL ,0 ,N'Z' ,0 ,2 ,2 ,2 ,0 ,0 ,1 ,1 ,1 ,2 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000124' ,NULL ,0 ,N'Z' ,5 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000125' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,3 ,3 ,1 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000126' ,NULL ,1 ,N'Z' ,4 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000127' ,NULL ,1 ,N'Z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000128' ,NULL ,0 ,N'Z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000129' ,NULL ,0 ,N'Z' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000130' ,NULL ,0 ,N'Z' ,0 ,4 ,4 ,4 ,0 ,0 ,1 ,1 ,3 ,4 ,3 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000131' ,NULL ,0 ,N'Z' ,0 ,2 ,2 ,2 ,1 ,1 ,1 ,1 ,2 ,2 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000132' ,NULL ,0 ,N'Z' ,0 ,0 ,0 ,0 ,3 ,3 ,0 ,0 ,0 ,1 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000133' ,NULL ,0 ,NULL ,0 ,1 ,2 ,0 ,1 ,1 ,1 ,1 ,1 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000134' ,NULL ,1 ,NULL ,4 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000135' ,NULL ,0 ,NULL ,0 ,2 ,2 ,2 ,0 ,0 ,0 ,0 ,1 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000136' ,NULL ,0 ,NULL ,0 ,1 ,2 ,1 ,0 ,1 ,1 ,1 ,1 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000137' ,NULL ,0 ,NULL ,0 ,2 ,2 ,2 ,0 ,0 ,0 ,0 ,1 ,2 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000138' ,NULL ,0 ,N'Z' ,0 ,1 ,2 ,1 ,0 ,1 ,1 ,1 ,1 ,1 ,0 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000139' ,NULL ,1 ,NULL ,0 ,3 ,3 ,3 ,0 ,0 ,0 ,0 ,2 ,3 ,2 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000140' ,NULL ,1 ,N'Z' ,3 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,1 ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000141' ,NULL ,1 ,N'Z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,NULL ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000144' ,N'Kette/Schuppe' ,1 ,N'Z' ,0 ,0 ,0 ,0 ,0 ,0 ,4 ,4 ,0.8 ,0.4 ,NULL ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000145' ,N'Kette/Schuppe' ,1 ,N'Z' ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,0.1 ,0.05 ,NULL ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000146' ,N'Kette/Schuppe' ,1 ,N'Z' ,3 ,1 ,1 ,0 ,0 ,0 ,0 ,0 ,0.7 ,0.35 ,1 ,0)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000147' ,N'Kette/Schuppe' ,1 ,N'Z' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,1 ,0)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000148' ,N'Kette/Schuppe' ,1 ,N'Z' ,4 ,1 ,1 ,0 ,0 ,0 ,0 ,0 ,0.8 ,0.4 ,1 ,0)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000149' ,N'Kette/Schuppe' ,2 ,NULL ,0 ,4 ,4 ,4 ,2 ,2 ,1 ,1 ,2.8 ,0.4 ,3 ,2)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000150' ,N'Kette/Schuppe' ,2 ,NULL ,0 ,4 ,4 ,4 ,3 ,3 ,2 ,2 ,3.1 ,0.55 ,4 ,3)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000151' ,N'Kette/Schuppe' ,2 ,NULL ,0 ,4 ,4 ,4 ,3 ,3 ,3 ,3 ,3.3 ,0.65 ,5 ,4)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000152' ,N'Kette/Schuppe' ,2 ,NULL ,0 ,4 ,4 ,4 ,0 ,0 ,0 ,0 ,2.4 ,0.2 ,2 ,1)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000153' ,N'Kette/Schuppe' ,2 ,NULL ,0 ,4 ,4 ,4 ,3 ,3 ,1 ,1 ,2.9 ,0.95 ,4 ,3)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000154' ,N'Kette/Schuppe' ,2 ,NULL ,0 ,5 ,5 ,5 ,3 ,3 ,2 ,2 ,3.7 ,1.35 ,5 ,4)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000156' ,N'Kette/Schuppe' ,2 ,N'Z' ,2 ,1 ,1 ,0 ,0 ,0 ,0 ,0 ,0.6 ,0.15 ,NULL ,NULL)
GO
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [gRS],  [gBE],  [RS],  [BE]) 
 VALUES ('00000000-0000-0000-0004-000000000157' ,N'Kette/Schuppe' ,2 ,N'Z' ,2 ,2 ,2 ,0 ,1 ,1 ,0 ,0 ,1.1 ,0.275 ,1 ,1)
GO


/* Schild */

INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000021' ,NULL ,N'S' ,-2 ,5 ,-2 ,2)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000022' ,NULL ,N'SP' ,0 ,2 ,0 ,-2)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000023' ,NULL ,N'S' ,-1 ,4 ,0 ,6)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000024' ,NULL ,N'S' ,-2 ,5 ,-2 ,2)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000025' ,NULL ,N'S' ,-1 ,3 ,-1 ,2)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000026' ,NULL ,N'S' ,-1 ,3 ,-1 ,5)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000027' ,NULL ,N'S' ,-2 ,5 ,-2 ,5)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000028' ,NULL ,N'S' ,-1 ,3 ,0 ,5)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000029' ,NULL ,N'S' ,-5 ,7 ,-3 ,1)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000030' ,NULL ,N'S' ,0 ,0 ,0 ,-1)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000031' ,NULL ,N'SP' ,0 ,0 ,-2 ,-1)
GO
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0003-000000000032' ,NULL ,N'S' ,0 ,1 ,0 ,4)
GO


/* Sonderfertigkeit */

SET IDENTITY_INSERT [Sonderfertigkeit] ON
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1112 ,N'Anathema' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 171')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1113 ,N'Angstlinderung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 180')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1114 ,N'Auge des Händlers' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 184')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1115 ,N'Auge des Mondes' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 177')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1116 ,N'Aura der Form' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 184')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1117 ,N'Bann der Schatten' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 178')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1118 ,N'Bannfluch über Untote' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 177')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1119 ,N'Begehen der Heiligen Wasser' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1120 ,N'Beute erhalten' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 191')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1121 ,N'Bild für die Ewigkeit' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 184')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1122 ,N'Bindung des Tieres' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 195')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1123 ,N'Blendstrahl' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 178')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1124 ,N'Blick des Magiers' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 178')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1125 ,N'Blick in die Erinnerung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 177')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1126 ,N'Blitzschlag' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 186')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1127 ,N'Buchprüfung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 185')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1128 ,N'Chitinhaut' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 182')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1129 ,N'Dreifacher Saatsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 187, 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1130 ,N'Dunkelheit' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 177')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1131 ,N'Ehrenhafter Zweikampf' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 195')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1132 ,N'Eidsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 169')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1133 ,N'Entzug der Göttergaben' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 171')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1134 ,N'Erheben des Untoten' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 177')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1135 ,N'Ermutigung des Kämpfers' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 182')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1136 ,N'Erneuerung des Geborstenen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 185')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1137 ,N'Erneuerung des Verlorenen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1138 ,N'Ewiger Wächter' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 195,196')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1139 ,N'Exkommunikation' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 171')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1140 ,N'Exorzismus' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 171')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1141 ,N'Fest der Freude' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 189')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1142 ,N'Feuersegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 170')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1143 ,N'Flagge des Friedens' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 180')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1144 ,N'Freier Weg' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 192')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1145 ,N'Freundschaftsgefühle' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 180')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1146 ,N'Gabe an die Sterne' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 193')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1147 ,N'Gebet der verborgenen Halle' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 181')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1148 ,N'Geburtssegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 171, 172')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1149 ,N'Geisterblick' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 177')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1150 ,N'Geisterreise' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 177')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1151 ,N'Gemeinschaft der treuen Getährten' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 180')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1152 ,N'Gesang der Delphine' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1153 ,N'Gesegnete Ruhe' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 180')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1154 ,N'Gesegneter Fang' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1155 ,N'Gesichtsverhüllung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 192,193')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1156 ,N'Gewittergrollen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 186')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1157 ,N'Giftbann' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1158 ,N'Gleichklangdes Geistes' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 172')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1159 ,N'Glückssegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 170')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1160 ,N'Glücksspielsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 189')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1161 ,N'Goldener Blick' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 185')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1162 ,N'Göttliche Inspiration' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 172')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1163 ,N'Göttliche Rüstung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 172')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1164 ,N'Göttliche Verständigung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 172')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1165 ,N'Göttliche Verständigung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 172')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1166 ,N'Göttliches Licht' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 179')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1167 ,N'Göttliches Verständnis' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 185')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1168 ,N'Göttliches Zeichen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 172')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1169 ,N'Grabsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 170')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1170 ,N'Große Weihe der Sinnlichkeit' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 189')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1171 ,N'Großer Weihesegen der Waffen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 182,183')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1172 ,N'Gunst der Grazien' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 189')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1173 ,N'Harmonie der Melodie' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 180')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1174 ,N'Harmoniesegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 170')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1175 ,N'Haustrieden' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 180, 181')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1176 ,N'Heiliger Befehl' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 173')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1177 ,N'Heilungssegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 170')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1178 ,N'Herbeirufung der Erdkraft' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1179 ,N'Hexagon des Vielleibigen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 183')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1180 ,N'Hornissenangriff' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 183')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1181 ,N'Hornissenwacht' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 183')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1182 ,N'Hornissenwirbel' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 183')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1183 ,N'Icanarias Gaukelspiel' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 189')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1184 ,N'Indoktrination' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 173')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1185 ,N'Initiation' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 173')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1186 ,N'Jagdglück' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 191')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1187 ,N'Jagdsicht' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 191')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1188 ,N'Kälbersegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 196')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1189 ,N'Kämpfersegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 170')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1190 ,N'Konsekration' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 173')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1191 ,N'Krankheitserkennung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1192 ,N'Leichenschau' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 178')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1193 ,N'Lohn der Unverzagten' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1194 ,N'Mannscha!tssegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 183,184')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1195 ,N'Markierung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 193')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1196 ,N'Nebelkontrolle' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 186')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1197 ,N'Nebelleib' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 186')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1198 ,N'Objektsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 173')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1199 ,N'Objektweihe' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 174')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1200 ,N'Objektweihe' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 174')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1201 ,N'Ordination' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 174')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1202 ,N'Prophezeiung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 174')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1203 ,N'Quellsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1204 ,N'Rauschsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 190')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1205 ,N'Regenbogenbrücke' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 186')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1206 ,N'Regenruf' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 186')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1207 ,N'Reisesegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 174')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1208 ,N'Richtungssegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 170')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1209 ,N'Ruf der Tiere' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 196')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1210 ,N'Ruf der Winde' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 187')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1211 ,N'Schattengestalt' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 193')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1212 ,N'Schattengestalt' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 193')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1213 ,N'Schattenrüstung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 193')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1214 ,N'Schlangenstab' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 197')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1215 ,N'Schneesturm / E:issturm' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 191')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1216 ,N'Schutz der Natur' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 196')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1217 ,N'Schutz vor Hellseherei' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 193, 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1218 ,N'Schutzsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 175')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1219 ,N'Seelenfrieden' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 181')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1220 ,N'Segensreicher Neuanfang' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 185')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1221 ,N'Sichere wanderung im Schnee' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 191')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1222 ,N'Sichere Zuflucht' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 181')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1223 ,N'Sicht auf die magische Welt' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 179')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1224 ,N'Speisesegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 175')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1225 ,N'Speisung der hungernden Seelen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 175')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1226 ,N'sprechende Symbole' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 185')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1227 ,N'Sternenglanz' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1228 ,N'Sühneopter' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 175')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1229 ,N'Tanz der Schöpfung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 190')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1230 ,N'Tarnung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1231 ,N'Tiergestalt' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 175')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1232 ,N'Traumsicht' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 190')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1233 ,N'Treuesegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 182')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1234 ,N'Über die Wolken' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 187')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1235 ,N'Unverstellter Blick' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 179')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1236 ,N'Verständnis der Sprachen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 176')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1237 ,N'Vertrauter des Gebirges' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 196')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1238 ,N'Vertreibung der Pestilenz' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1239 ,N'Visionssuche' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 176')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1240 ,N'Warnung der Götter' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 176')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1241 ,N'Wasserreinigung' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1242 ,N'Wassersuche' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 194')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1243 ,N'Weidesegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 196')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1244 ,N'Weihe der letzten Ruhestatt' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 178')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1245 ,N'Weisheitssegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 176')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1246 ,N'Wildwechsel' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 191')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1247 ,N'Wille zur Wahrheit' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 179')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1248 ,N'Windeseile' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 187')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1249 ,N'Winterschlaf' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 191')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1250 ,N'Wogen macht' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 195')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1251 ,N'Wort der Disziplin' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 184')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1252 ,N'Wundersame Fruchtbarkeit' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 196')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1253 ,N'Wundersames Wachstum' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1254 ,N'Wundsegen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 188')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1255 ,N'Zauberschutz' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 179')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1256 ,N'Zauberspiegel' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 179')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1257 ,N'Zerschmettern' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 197')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1258 ,N'Zerschmetternder Bannstrahl' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 179')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1259 ,N'Zuflucht finden' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 192')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1260 ,N'Zuflucht vor den Elementen' ,0 ,N'Liturgie' ,N'Myranor' ,NULL ,N'MyG 197')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1261 ,N'Ankhatep' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 82')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1262 ,N'Aves' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 82')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1263 ,N'Bishdariel' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 82')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1264 ,N'Brazoragh' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 82')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1265 ,N'Bylmaresh' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 82')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1266 ,N'Chrysir' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 82')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1267 ,N'Efferd' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 82')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1268 ,N'Firun' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1269 ,N'Hesinde' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1270 ,N'Himmelswölfe' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1271 ,N'Ifirn' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1272 ,N'Ingerimm' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1273 ,N'Kamaluq' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1274 ,N'Kor' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1275 ,N'Levthan ' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1276 ,N'Marbo' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1277 ,N'Mokoscha' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1278 ,N'Namenloser' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1279 ,N'Nandus' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1280 ,N'Numinor' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1281 ,N'Ojo''Sombri' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1282 ,N'Peraine' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1283 ,N'Phex' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1284 ,N'Praios' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 84')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1285 ,N'Rahja' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1286 ,N'Rondra' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1287 ,N'Satuaria' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1288 ,N'Shinxir' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1289 ,N'Simia' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1290 ,N'Swafnir' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1291 ,N'Tairach' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1292 ,N'Travia' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1293 ,N'Tsa' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1294 ,N'Ucuri' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'OiC 86')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1295 ,N'Traum der Seele' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 266 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1296 ,N'Visars Vergessen' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 267 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1297 ,N'Visars Vergessen (V)' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 267 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1298 ,N'Taya des Träumenden' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 267 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1299 ,N'Nekemas Rausch' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 267 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1300 ,N'Fluch des Geistes' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 267 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1301 ,N'Reise zu den Geistern' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 267, 268/ OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1302 ,N'Tabu' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 268 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1303 ,N'Großes Tabu' ,0 ,N'Liturgie' ,N'Dunkle Zeiten' ,NULL ,N'WdG 268 / OiC 58')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1304 ,N'Hauch des Elements' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1305 ,N'Aufmerksamer Wächter' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1306 ,N'Geistheilung' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1307 ,N'Stimme des Felsens' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1308 ,N'Tabuzone' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1309 ,N'Farben des Krieges' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1310 ,N'Macht der Elemente ' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1311 ,N'Alles in deinem Mund wird zu (Element)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 131')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1312 ,N'Analyse des Elements' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 114')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1313 ,N'Angst vor (Umschreibung einer Schlechten Eigenschaft) sei in deinem Herzen!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 131')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1314 ,N'Astral-/ Lebensraub' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 114')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1315 ,N'Astral-/ Lebenstransfer' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 114')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1316 ,N'Auge des Speeres' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 123')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1317 ,N'Bannfokus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 114')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1318 ,N'Bannklinge' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 111')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1319 ,N'Beflügelung des Spiels' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 134')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1320 ,N'Behausung des Ahnen' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 129')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1321 ,N'Bilderspiel' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 134')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1322 ,N'Bindung des Siegels' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 120')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1323 ,N'Bindung des Speeres' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 123')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1324 ,N'Bindung des Stabes' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 114')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1325 ,N'Bindungdes Instrumentes' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 133')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1326 ,N'Blendung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 114')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1327 ,N'Bündnis der Speere' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 123, 124')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1328 ,N'Das (Licht der Sonne, die Stimme des Freundes, .. .)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 131')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1329 ,N'Das große Fest' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 134')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1330 ,N'Das große Weinlied' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 135')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1331 ,N'Das kleine Weinlied' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 134')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1332 ,N'Das Spiel der Sinne' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 134')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1333 ,N'Dein Weg sei mit Unglück gepflastert!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 131')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1334 ,N'Dienstfokus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 114')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1335 ,N'Dinge aufspüren' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 126')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1336 ,N'Doppeltes Maß' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 115')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1337 ,N'Elementarklinge' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 115')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1338 ,N'Elementarwahrnehmung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 115')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1339 ,N'Erster unter Gleichen' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 126, 127')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1340 ,N'Fixierung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1341 ,N'Fluch der Schlange' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 135')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1342 ,N'Form der Schlange' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 135')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1343 ,N'Frischer Geist' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1344 ,N'Funkenregen' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 115, 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1345 ,N'Geballte Kraft' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 133')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1346 ,N'Gefährte in neuer Gestalt' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 127')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1347 ,N'Gehorche der Stimme der Magie!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 131')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1348 ,N'Gehorche der Weisungder Geister!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 131')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1349 ,N'Geistesband' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1350 ,N'Großer Exorzismus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 130')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1351 ,N'Hammer des Magus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1352 ,N'Hässlich sollst Du sein!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1353 ,N'Kein (Mensch/Tier/ Amaun/ . .) mehr wird deine Gegenwart ertragen!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1354 ,N'Keine Ruhe wirst du finden!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1355 ,N'Kleiner Exorzismus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 130')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1356 ,N'Kontrolltetisch' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 129')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1357 ,N'Kosrentetisch' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 129')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1358 ,N'Kraft des Speeres' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 124')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1359 ,N'Kraftfokus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1360 ,N'Kristallkraft bündeln' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 135')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1361 ,N'Lebender Lauberspeicher' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 135')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1362 ,N'Leuchtfeuer' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1363 ,N'Licht des Speeres' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 124')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1364 ,N'Lichtzauber' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 116')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1365 ,N'Liederspeicher' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 133')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1366 ,N'Magisches Siegel' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 120')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1367 ,N'Meister finden' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 127')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1368 ,N'Mitverwandlung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1369 ,N'Notruf' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1370 ,N'Pein beuge dich!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1371 ,N'port' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 111')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1372 ,N'Reinigung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1373 ,N'Resistenz' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1374 ,N'Satyaras Segen' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 135')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1375 ,N'Schild vor' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1376 ,N'Schlaf rauben' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 127')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1377 ,N'Schlage Wurzeln!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1378 ,N'Schöner Klang' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 133')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1379 ,N'Schutz des Speeres' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 123')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1380 ,N'Schweige!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1381 ,N'Sei dem Tode geweiht!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1382 ,N'Sei unfruchtbar, Frevler!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 132')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1383 ,N'Seil des Adepten' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1384 ,N'Seil des Magus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1385 ,N'Sensorspeicher' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 117, 118')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1386 ,N'Siegeletikett' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 120')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1387 ,N'Siegelschutz' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 120')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1388 ,N'Spontanfokus' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 118')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1389 ,N'Stabexplosion' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 118')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1390 ,N'Stabzaubererweiterung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119, 120')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1391 ,N'Stimmungssinn' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 127')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1392 ,N'Strafe des S peeres' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 124')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1393 ,N'Tarnung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 127')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1394 ,N'Temporäres Siegel' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 118')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1395 ,N'Tierhaut' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 118')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1396 ,N'Tiersinne' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 127')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1397 ,N'Todesberührung' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 118, 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1398 ,N'übernahme der Besessenheit' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 130')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1399 ,N'Ungesehener Beobachter' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 127, 128')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1400 ,N'Verschlüsselung des Siegels' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 121')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1401 ,N'Wachsame Augen' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 128')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1402 ,N'Welle des (Elements)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1403 ,N'Wesenspeicher ( Dämonen)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1404 ,N'Wesenspeicher ( Stellare)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1405 ,N'Wesenspeicher (Elemente)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1406 ,N'Wesenspeicher (Naturwesen)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1407 ,N'Wesenspeicher (Totenwesen)' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1408 ,N'Zauberklänge tragen weit!' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 133')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1409 ,N'Zauberparade' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1410 ,N'Zauberspeicher' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 119')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1411 ,N'Zwiegespräch' ,0 ,N'Ritual' ,N'Myranor' ,NULL ,N'MyMa 128')
GO
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitID],  [Name],  [HatWert],  [Typ],  [Setting],  [Vorraussetzungen],  [Literatur]) 
 VALUES (1412 ,N'Trollruf' ,0 ,N'Petromantie' ,NULL ,NULL ,N'EG 155')
GO
SET IDENTITY_INSERT [Sonderfertigkeit] OFF
GO
ALTER TABLE [Sonderfertigkeit] ALTER COLUMN [SonderfertigkeitID] IDENTITY (1413,1)
GO

/* VorNachteil */

SET IDENTITY_INSERT [VorNachteil] ON
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (388 ,N'Astraler Block' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (389 ,N'Barbarische Sitten' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (390 ,N'Behäbig' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (391 ,N'Blutdurst' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (392 ,N'Brünstigkeit' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (393 ,N'Flügellahm/ Flugunfähig' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (394 ,N'Flugangst' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (395 ,N'Fühllos' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (396 ,N'Leben im Jetzt' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (397 ,N'Schäumende Wut ' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (398 ,N'Treulosigkeit' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (399 ,N'Unfähigkeit für (Instruktion)' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (400 ,N'Unfähigkeit für (Quelle)' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (401 ,N'Verlorene Zwillingsseele ' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (402 ,N'Invertiertes Fey' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (403 ,N'Körpereigenes Gift ' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (404 ,N'Schlangenleib ' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (405 ,N'Blasse Haut ' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (406 ,N'Fäule ' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (407 ,N'Trollwut' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (408 ,N'Verhornung ' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (409 ,N'Blutlinienpakt' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (410 ,N'Schlagensprössling (wissentlich)' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (411 ,N'Schlagensprössling (intuitiv)' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Rakshazar')
GO
INSERT INTO [VorNachteil] (  [VorNachteilID],  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting]) 
 VALUES (412 ,N'Dschinngeboren' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Aventurien')
GO
SET IDENTITY_INSERT [VorNachteil] OFF
GO
ALTER TABLE [VorNachteil] ALTER COLUMN [VorNachteilID] IDENTITY (413,1)
GO


/* Waffe */

INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000151' ,6 ,1 ,5 ,0 ,14 ,2 ,-1 ,-1 ,-2 ,2 ,100 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000152' ,6 ,1 ,2 ,0 ,12 ,4 ,0 ,0 ,-1 ,1 ,40 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000153' ,6 ,1 ,4 ,0 ,10 ,4 ,0 ,0 ,0 ,2 ,120 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000154' ,6 ,1 ,4 ,0 ,12 ,4 ,0 ,0 ,0 ,1 ,90 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000155' ,6 ,1 ,2 ,0 ,12 ,6 ,0 ,-1 ,-3 ,1 ,20 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000156' ,6 ,2 ,2 ,0 ,14 ,4 ,-2 ,-1 ,-2 ,6 ,300 ,N'SP' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000157' ,6 ,1 ,6 ,0 ,13 ,3 ,-1 ,-1 ,-3 ,2 ,140 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000158' ,6 ,1 ,1 ,0 ,12 ,6 ,-2 ,-2 ,-3 ,7 ,20 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000159' ,6 ,3 ,2 ,0 ,14 ,2 ,-3 ,0 ,-2 ,3 ,220 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000160' ,6 ,1 ,3 ,0 ,11 ,4 ,-1 ,-1 ,-2 ,5 ,50 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000161' ,6 ,1 ,6 ,0 ,13 ,3 ,0 ,0 ,-2 ,4 ,240 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000162' ,6 ,1 ,1 ,0 ,10 ,3 ,0 ,-1 ,-2 ,0 ,NULL ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000163' ,6 ,1 ,1 ,0 ,10 ,3 ,0 ,-1 ,-2 ,0 ,NULL ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000164' ,6 ,1 ,2 ,0 ,12 ,4 ,0 ,0 ,-1 ,3 ,50 ,N'HH' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000165' ,6 ,1 ,4 ,0 ,11 ,4 ,0 ,0 ,0 ,1 ,85 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000166' ,6 ,1 ,4 ,0 ,13 ,5 ,0 ,0 ,-1 ,2 ,100 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000167' ,6 ,2 ,8 ,0 ,15 ,2 ,-1 ,-1 ,-2 ,4 ,250 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000168' ,6 ,2 ,2 ,0 ,13 ,4 ,0 ,0 ,-1 ,3 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000169' ,6 ,1 ,1 ,0 ,12 ,5 ,0 ,0 ,-1 ,2 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000170' ,6 ,2 ,2 ,0 ,15 ,1 ,-2 ,-1 ,-3 ,3 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000171' ,6 ,1 ,5 ,0 ,12 ,2 ,-1 ,0 ,-2 ,5 ,200 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000172' ,6 ,1 ,5 ,0 ,14 ,4 ,0 ,0 ,0 ,3 ,140 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000173' ,6 ,3 ,3 ,0 ,17 ,1 ,-3 ,-2 ,-4 ,3 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000174' ,6 ,1 ,2 ,0 ,13 ,5 ,-1 ,0 ,0 ,5 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000175' ,6 ,1 ,4 ,0 ,13 ,3 ,0 ,0 ,-1 ,5 ,140 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000176' ,6 ,1 ,2 ,0 ,12 ,5 ,0 ,0 ,1 ,1 ,50 ,N'HH' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000177' ,6 ,1 ,3 ,0 ,12 ,3 ,-2 ,-2 ,-3 ,6 ,150 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000178' ,6 ,1 ,4 ,0 ,15 ,4 ,-1 ,0 ,-1 ,3 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000179' ,6 ,2 ,2 ,0 ,12 ,4 ,-2 ,-1 ,-1 ,4 ,350 ,N'SP' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000180' ,6 ,1 ,0 ,0 ,13 ,3 ,-2 ,-2 ,-3 ,8 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000181' ,6 ,1 ,4 ,0 ,11 ,5 ,0 ,0 ,-1 ,1 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000182' ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,0 ,-1 ,1 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000183' ,6 ,0 ,0 ,0 ,NULL ,NULL ,-2 ,-4 ,-3 ,4 ,2 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000184' ,6 ,1 ,3 ,0 ,13 ,3 ,0 ,0 ,-1 ,3 ,100 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000185' ,6 ,1 ,2 ,0 ,12 ,3 ,0 ,0 ,-1 ,2 ,50 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000186' ,6 ,1 ,2 ,0 ,12 ,5 ,0 ,0 ,0 ,3 ,50 ,N'HH' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000187' ,6 ,2 ,6 ,0 ,10 ,2 ,-1 ,-1 ,-4 ,3 ,180 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000188' ,6 ,1 ,2 ,0 ,12 ,4 ,0 ,0 ,0 ,4 ,40 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000189' ,6 ,1 ,3 ,0 ,13 ,3 ,-1 ,0 ,-2 ,3 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000190' ,6 ,2 ,3 ,0 ,13 ,4 ,-2 ,-1 ,-2 ,4 ,200 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000191' ,6 ,2 ,0 ,0 ,12 ,2 ,-2 ,1 ,-4 ,5 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000192' ,6 ,1 ,3 ,0 ,12 ,5 ,0 ,-1 ,-3 ,5 ,150 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000193' ,6 ,1 ,3 ,0 ,14 ,2 ,-1 ,-1 ,-2 ,3 ,100 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000194' ,6 ,1 ,2 ,0 ,12 ,5 ,-1 ,0 ,-2 ,3 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000195' ,6 ,1 ,6 ,0 ,12 ,4 ,-1 ,0 ,-1 ,3 ,200 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000196' ,6 ,1 ,1 ,0 ,12 ,4 ,1 ,0 ,0 ,5 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000197' ,6 ,1 ,4 ,0 ,12 ,4 ,-1 ,0 ,-2 ,4 ,180 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000198' ,6 ,1 ,2 ,0 ,12 ,5 ,-1 ,0 ,-1 ,3 ,35 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000199' ,6 ,1 ,2 ,0 ,13 ,5 ,0 ,0 ,0 ,3 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000200' ,6 ,1 ,2 ,0 ,12 ,5 ,0 ,-1 ,-2 ,4 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000201' ,6 ,1 ,5 ,0 ,14 ,4 ,0 ,0 ,0 ,3 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000202' ,6 ,1 ,2 ,0 ,11 ,3 ,0 ,0 ,-2 ,3 ,80 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000203' ,6 ,1 ,4 ,0 ,11 ,4 ,1 ,0 ,0 ,1 ,85 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000204' ,6 ,2 ,4 ,0 ,12 ,3 ,-1 ,0 ,-1 ,3 ,200 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000205' ,6 ,1 ,-1 ,0 ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000206' ,6 ,1 ,3 ,0 ,12 ,4 ,0 ,0 ,-1 ,6 ,120 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000207' ,6 ,1 ,1 ,0 ,11 ,4 ,0 ,0 ,-2 ,6 ,80 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000208' ,6 ,1 ,4 ,0 ,13 ,2 ,-1 ,0 ,-1 ,2 ,90 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000209' ,6 ,1 ,6 ,0 ,12 ,2 ,-1 ,-1 ,-2 ,5 ,150 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000210' ,6 ,1 ,2 ,0 ,11 ,4 ,0 ,0 ,-1 ,1 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000211' ,6 ,3 ,2 ,0 ,15 ,1 ,-2 ,-1 ,-4 ,3 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000212' ,6 ,2 ,3 ,0 ,13 ,3 ,-2 ,0 ,-3 ,3 ,220 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000213' ,6 ,1 ,4 ,0 ,12 ,4 ,1 ,0 ,0 ,2 ,80 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000214' ,6 ,1 ,5 ,0 ,14 ,2 ,-1 ,0 ,-1 ,3 ,100 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000215' ,6 ,1 ,2 ,0 ,11 ,4 ,1 ,0 ,0 ,1 ,50 ,N'HH' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000216' ,6 ,1 ,2 ,0 ,12 ,4 ,0 ,0 ,-2 ,3 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000217' ,6 ,1 ,0 ,0 ,12 ,6 ,-2 ,-2 ,-3 ,4 ,25 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000218' ,6 ,1 ,2 ,0 ,12 ,6 ,0 ,-1 ,-3 ,1 ,20 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000219' ,6 ,4 ,4 ,0 ,22 ,1 ,-3 ,-2 ,-4 ,2 ,300 ,N'SP' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000220' ,6 ,1 ,2 ,0 ,12 ,4 ,1 ,0 ,0 ,6 ,120 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000221' ,6 ,1 ,2 ,0 ,12 ,4 ,1 ,0 ,0 ,6 ,120 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000222' ,6 ,1 ,3 ,0 ,NULL ,NULL ,-1 ,0 ,-2 ,2 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000223' ,6 ,1 ,2 ,0 ,12 ,3 ,0 ,0 ,-1 ,2 ,50 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000224' ,6 ,1 ,3 ,0 ,13 ,4 ,-1 ,-1 ,-1 ,5 ,120 ,N'SP' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000225' ,6 ,1 ,3 ,0 ,12 ,4 ,0 ,-1 ,-3 ,1 ,20 ,N'H' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000226' ,6 ,1 ,5 ,0 ,11 ,3 ,1 ,0 ,0 ,2 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000227' ,6 ,1 ,0 ,0 ,14 ,5 ,0 ,0 ,NULL ,4 ,250 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000228' ,6 ,1 ,4 ,0 ,12 ,4 ,0 ,0 ,-1 ,2 ,90 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000229' ,6 ,1 ,1 ,0 ,12 ,5 ,0 ,0 ,-1 ,3 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000230' ,6 ,2 ,1 ,0 ,13 ,3 ,-1 ,0 ,-2 ,2 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000231' ,6 ,2 ,4 ,0 ,13 ,3 ,-1 ,0 ,-2 ,3 ,180 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000232' ,6 ,1 ,2 ,0 ,12 ,4 ,0 ,0 ,-1 ,4 ,35 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000233' ,6 ,2 ,2 ,0 ,12 ,3 ,0 ,0 ,0 ,1 ,200 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000234' ,6 ,2 ,2 ,0 ,12 ,4 ,-2 ,-1 ,-1 ,4 ,350 ,N'P' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000235' ,6 ,1 ,6 ,0 ,13 ,3 ,-1 ,-1 ,-2 ,4 ,185 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000236' ,6 ,1 ,2 ,0 ,NULL ,NULL ,1 ,0 ,0 ,3 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000237' ,6 ,1 ,3 ,0 ,12 ,4 ,1 ,0 ,0 ,2 ,90 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000238' ,6 ,1 ,4 ,0 ,10 ,4 ,0 ,0 ,0 ,3 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000239' ,6 ,1 ,2 ,1 ,10 ,3 ,0 ,-1 ,-2 ,0 ,NULL ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000240' ,6 ,1 ,1 ,0 ,12 ,5 ,-1 ,0 ,-1 ,3 ,40 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000241' ,6 ,1 ,1 ,0 ,NULL ,NULL ,0 ,0 ,-2 ,6 ,80 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000242' ,6 ,1 ,3 ,0 ,14 ,3 ,-2 ,-1 ,-2 ,2 ,50 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000243' ,6 ,1 ,3 ,0 ,11 ,4 ,1 ,0 ,0 ,2 ,60 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000244' ,6 ,1 ,2 ,0 ,11 ,4 ,0 ,0 ,0 ,0 ,20 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000245' ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,0 ,-1 ,1 ,85 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000246' ,6 ,1 ,6 ,0 ,13 ,4 ,-2 ,-1 ,-1 ,3 ,200 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000247' ,6 ,1 ,5 ,0 ,13 ,4 ,-3 ,-1 ,-2 ,4 ,200 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000248' ,6 ,1 ,5 ,0 ,13 ,4 ,-3 ,-1 ,-2 ,5 ,200 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000249' ,6 ,1 ,3 ,0 ,13 ,4 ,-2 ,-2 ,-4 ,7 ,160 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000250' ,6 ,1 ,3 ,0 ,13 ,3 ,0 ,0 ,-1 ,1 ,70 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000251' ,6 ,2 ,3 ,0 ,14 ,3 ,-1 ,-1 ,-2 ,4 ,100 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000252' ,6 ,1 ,2 ,0 ,16 ,5 ,-2 ,-1 ,NULL ,3 ,300 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000253' ,6 ,1 ,2 ,0 ,12 ,3 ,-2 ,-1 ,-1 ,3 ,300 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000254' ,6 ,1 ,2 ,0 ,12 ,5 ,-2 ,-2 ,-2 ,6 ,50 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000255' ,6 ,1 ,1 ,0 ,11 ,4 ,0 ,0 ,-1 ,5 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000256' ,6 ,1 ,4 ,0 ,15 ,1 ,-1 ,-1 ,-3 ,2 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000257' ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,0 ,0 ,1 ,80 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000258' ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,0 ,0 ,1 ,95 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000259' ,6 ,1 ,5 ,0 ,12 ,4 ,-1 ,0 ,-2 ,5 ,190 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000260' ,6 ,2 ,2 ,0 ,11 ,4 ,0 ,0 ,-1 ,3 ,250 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000261' ,6 ,1 ,6 ,0 ,13 ,2 ,-3 ,-2 ,-4 ,5 ,100 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000262' ,6 ,2 ,3 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000263' ,6 ,1 ,3 ,0 ,11 ,4 ,-1 ,-1 ,-2 ,6 ,50 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000264' ,6 ,1 ,0 ,0 ,12 ,6 ,-2 ,-2 ,-3 ,6 ,25 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000265' ,6 ,1 ,4 ,0 ,12 ,4 ,-1 ,0 ,-2 ,6 ,180 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000267' ,6 ,1 ,4 ,0 ,11 ,3 ,0 ,0 ,-1 ,1 ,75 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000268' ,6 ,1 ,0 ,0 ,11 ,5 ,-1 ,-1 ,-1 ,8 ,40 ,N'HN' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000269' ,6 ,1 ,4 ,0 ,13 ,3 ,-1 ,-1 ,-2 ,5 ,180 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000270' ,6 ,1 ,4 ,0 ,12 ,4 ,0 ,0 ,-1 ,4 ,200 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000271' ,6 ,1 ,2 ,0 ,12 ,4 ,1 ,0 ,0 ,3 ,150 ,NULL ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000272' ,6 ,1 ,2 ,0 ,12 ,5 ,0 ,0 ,0 ,4 ,35 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000273' ,6 ,1 ,1 ,0 ,12 ,3 ,0 ,0 ,-1 ,4 ,50 ,N'HN' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000274' ,6 ,1 ,3 ,0 ,11 ,3 ,0 ,-1 ,-2 ,4 ,30 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000275' ,6 ,1 ,1 ,0 ,12 ,4 ,1 ,0 ,0 ,4 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000276' ,6 ,1 ,2 ,0 ,12 ,5 ,0 ,0 ,1 ,1 ,40 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000277' ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,0 ,0 ,3 ,90 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000278' ,6 ,1 ,4 ,0 ,11 ,4 ,0 ,0 ,0 ,1 ,95 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000279' ,6 ,1 ,2 ,0 ,12 ,5 ,-2 ,0 ,-1 ,0 ,45 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000280' ,6 ,1 ,5 ,0 ,13 ,4 ,0 ,0 ,-1 ,3 ,250 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000281' ,6 ,1 ,0 ,0 ,12 ,5 ,0 ,0 ,0 ,0 ,NULL ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000282' ,6 ,2 ,4 ,0 ,13 ,2 ,-2 ,0 ,-2 ,3 ,140 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000283' ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,0 ,0 ,3 ,90 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000284' ,6 ,2 ,4 ,0 ,13 ,2 ,-2 ,0 ,-2 ,3 ,140 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000285' ,6 ,1 ,4 ,0 ,11 ,4 ,1 ,0 ,0 ,2 ,100 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000286' ,6 ,1 ,5 ,0 ,13 ,3 ,0 ,0 ,-1 ,1 ,90 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000287' ,6 ,1 ,0 ,0 ,12 ,3 ,0 ,-1 ,-1 ,1 ,70 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000288' ,6 ,1 ,5 ,0 ,14 ,4 ,0 ,0 ,0 ,3 ,150 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000289' ,6 ,1 ,6 ,0 ,13 ,4 ,-2 ,-1 ,-1 ,3 ,200 ,N'NS' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000290' ,6 ,1 ,3 ,0 ,14 ,2 ,-1 ,-1 ,-2 ,3 ,100 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000291' ,6 ,1 ,4 ,0 ,15 ,1 ,-1 ,-1 ,-3 ,2 ,110 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000292' ,6 ,1 ,4 ,0 ,11 ,4 ,1 ,0 ,0 ,2 ,85 ,NULL ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0001-000000000293' ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,-1 ,-2 ,3 ,5 ,N'N' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000101' ,6 ,1 ,1 ,0 ,12 ,5 ,-1 ,-1 ,-2 ,2 ,25 ,N'H' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000102' ,6 ,1 ,2 ,0 ,12 ,5 ,0 ,0 ,-1 ,1 ,40 ,N'H' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000104' ,6 ,1 ,2 ,0 ,13 ,3 ,0 ,0 ,-1 ,2 ,70 ,N'N' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000106' ,6 ,1 ,3 ,0 ,10 ,4 ,0 ,0 ,-1 ,2 ,50 ,N'HN' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000107' ,6 ,1 ,-1 ,0 ,12 ,6 ,-1 ,-2 ,-3 ,2 ,20 ,N'H' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000115' ,6 ,2 ,4 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000116' ,6 ,2 ,4 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000117' ,6 ,2 ,5 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000118' ,6 ,2 ,5 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000119' ,6 ,2 ,5 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000120' ,6 ,2 ,5 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000121' ,6 ,2 ,5 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000122' ,6 ,2 ,5 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000123' ,6 ,2 ,6 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000124' ,6 ,2 ,6 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000125' ,6 ,2 ,6 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000126' ,6 ,2 ,6 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000127' ,6 ,2 ,6 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000128' ,6 ,2 ,6 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000129' ,6 ,2 ,6 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000130' ,6 ,2 ,4 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000131' ,6 ,2 ,4 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000132' ,6 ,2 ,4 ,0 ,12 ,3 ,-1 ,-1 ,-2 ,3 ,140 ,N'NS' ,1)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0002-000000000134' ,6 ,1 ,5 ,0 ,12 ,4 ,-1 ,0 ,-2 ,2 ,190 ,N'S' ,0)
GO
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0003-000000000031' ,6 ,1 ,3 ,0 ,10 ,5 ,-1 ,0 ,0 ,-2 ,25 ,N'H' ,0)
GO


/* Waffe_Talent */

INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000151' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000152' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000153' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000154' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000155' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000156' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000157' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000158' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000159' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000159' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000159' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000160' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000161' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000161' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000162' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000163' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000164' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000164' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000165' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000166' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000166' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000167' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000167' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000168' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000169' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000170' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000171' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000172' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000172' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000173' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000174' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000175' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000176' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000176' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000177' ,N'Zweihandflegel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000178' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000178' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000179' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000179' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000180' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000181' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000182' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000183' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000184' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000185' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000186' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000186' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000187' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000187' ,N'Zweihandflegel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000188' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000189' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000189' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000190' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000190' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000191' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000192' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000193' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000194' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000195' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000196' ,N'Stäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000197' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000198' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000199' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000200' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000201' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000201' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000202' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000203' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000203' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000204' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000204' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000205' ,N'Peitsche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000206' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000207' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000208' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000209' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000209' ,N'Zweihandflegel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000210' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000210' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000211' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000211' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000212' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000212' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000213' ,N'Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000213' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000214' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000214' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000215' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000215' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000216' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000216' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000217' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000218' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000219' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000220' ,N'Stäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000221' ,N'Stäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000222' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000223' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000224' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000225' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000226' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000226' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000227' ,N'Peitsche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000228' ,N'Fechtwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000228' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000229' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000230' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000230' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000231' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000231' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000231' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000232' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000233' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000233' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000234' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000234' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000235' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000235' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000236' ,N'Stäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000237' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000237' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000238' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000239' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000240' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000240' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000241' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000242' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000243' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000243' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000244' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000245' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000246' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000246' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000247' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000247' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000248' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000248' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000248' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000249' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000250' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000250' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000251' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000252' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000253' ,N'Peitsche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000254' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000255' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000256' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000257' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000258' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000258' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000259' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000260' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000260' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000261' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000262' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000262' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000263' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000264' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000265' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000267' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000268' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000269' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000270' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000271' ,N'Stäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000272' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000273' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000273' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000274' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000275' ,N'Stäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000276' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000277' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000278' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000279' ,N'Dolche')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000280' ,N'Infanteriewaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000280' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000281' ,N'Raufen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000282' ,N'Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000283' ,N'Säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000284' ,N'Zweihandschwerter/-säbel')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000285' ,N'Säbel ')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000285' ,N'Schwerter')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000286' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000287' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000288' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000288' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000289' ,N'Bastardstäbe')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000289' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000290' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000291' ,N'Kettenwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000292' ,N'Speere')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0001-000000000293' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000115' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000115' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000116' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000116' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000117' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000117' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000118' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000118' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000119' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000119' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000120' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000120' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000121' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000121' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000122' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000122' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000123' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000123' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000124' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000124' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000125' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000125' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000126' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000126' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000127' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000127' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000128' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000128' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000129' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000129' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000130' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000130' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000131' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000131' ,N'Zweihand-Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000132' ,N'Hiebwaffen')
GO
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [Talentname]) 
 VALUES ('00000000-0000-0000-0002-000000000132' ,N'Zweihand-Hiebwaffen')
GO


/* Zauber */

SET IDENTITY_INSERT [Zauber] ON
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (347 ,N'Glacoflumen Fluss aus Eis' ,N'IN' ,N'KO' ,N'GE' ,N'E' ,N'Sch 3, Elf 2, Dru 1, Mag 1' ,N'Elementar (Eis), Umwelt' ,N'EG 102' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (348 ,N'Aquaqerus Wasserfluch' ,N'MU' ,N'IN' ,N'KK' ,N'D' ,N'Dru 1, Mag 1' ,N'Elementar (Wasser), Schaden' ,N'EG 102' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (349 ,N'Aeropulvis Sanfter Fall' ,N'KL' ,N'IN' ,N'GE' ,N'C' ,N'Elf 3, Mag 2, Dru 1, Hex 1' ,N'Bewegung, Elementar (Luft)' ,N'EG 102, 103' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (350 ,N'Fesselranken' ,N'KL' ,N'IN' ,N'KK' ,N'C' ,N'Dru 3, Hex 2, Mag 1, Elf 1' ,N'Elementar (Humus), Umwelt' ,N'EG 103' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (351 ,N'Feuermähne Flammenhuf' ,N'IN' ,N'CH' ,N'KO' ,N'E' ,N'Elf 1' ,N'Beschwörung, Elementar (Feuer)' ,N'EG 103' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (352 ,N'Ignufugo Feuerbann' ,N'MU' ,N'KO' ,N'FF' ,N'C' ,N'Sch 4, Srl 4,  Mag 2, Dru 1,Geo 1' ,N'Elementar (Feuer), Umwelt' ,N'EG 103, 104' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (353 ,N'Ignimorpho Feuerform' ,N'MU' ,N'FF' ,N'KO' ,N'D' ,N'Elf 3, Geo 1, Dru 1, Mag 1(Elf)' ,N'Elementar (Feuer), Objekt' ,N'EG 104' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (354 ,N'Igniplano Flächenbrand' ,N'MU' ,N'IN' ,N'KO' ,N'F' ,N'Mag 1' ,N'Elementar (Feuer), Schaden, Umwelt' ,N'EG 104' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (355 ,N'Kraft des Humus' ,N'IN' ,N'CH' ,N'KO' ,N'D' ,N'Geo 4, Dru 3, Hex 3, Ach 2,Elf 2, Mag 2(Dru)' ,N'Elementar (Humus), Heilung' ,N'EG 104, 105' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (356 ,N'Stimme des Windes' ,N'MU' ,N'IN' ,N'IN' ,N'D' ,N'Elf 4, Geo 3, Dru 3, Mag 2(Dru), Hex 1(Geo)' ,N'Form, Elementar (Luft)' ,N'EG 105' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (357 ,N'Sumpfstrudel' ,N'MU' ,N'IN' ,N'KK' ,N'D' ,N'Geo 3, Dru 2, Mag 2, Elf 1' ,N'Elementar (Humus), Umwelt' ,N'EG 105, 106' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (358 ,N'Wand aus (Element)' ,N'MU' ,N'KL' ,N'CH' ,N'D' ,N'Geo 4, Dru 3, Mag 3, Elf 1 (allgemein, WAND AUS FLAMMEN Dru, Mag je 4, GLETSCHERWAND Dru 2, Geo 1)' ,N'Elementar (verschieden)' ,N'EG 106' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (359 ,N'Warmes Gefriere' ,N'MU' ,N'KL' ,N'KK' ,N'C' ,N'Mag 3' ,N'Objekt, Elementar (Eis)' ,N'EG 106, 107' ,N'Aventurien')
GO
INSERT INTO [Zauber] (  [ZauberID],  [Name],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Komplex],  [Repräsentationen],  [Merkmale],  [Literatur],  [Setting]) 
 VALUES (360 ,N'Windgeflüster' ,N'KL' ,N'IN' ,N'IN' ,N'B' ,N'Dru 1, Elf 1, Mag 1' ,N'Verständigung, Elementar (Luft)' ,N'EG 107' ,N'Aventurien')
GO
SET IDENTITY_INSERT [Zauber] OFF
GO
ALTER TABLE [Zauber] ALTER COLUMN [ZauberID] IDENTITY (361,1)
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien' WHERE [ZauberID]=1
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien' WHERE [ZauberID]=2
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=3
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=4
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=5
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=6
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=7
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=8
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=9
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=10
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=11
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=12
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=13
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=14
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=15
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=16
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=17
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=18
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=19
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=20
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=21
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=22
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=23
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=24
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=25
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=26
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=27
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=28
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=29
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=30
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=31
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=32
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=33
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=34
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=35
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=36
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=37
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=38
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=39
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=40
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=41
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=346
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=42
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=43
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=44
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=45
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=46
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=47
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=48
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=49
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=50
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=51
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=52
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=53
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=54
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=55
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=56
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=57
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=58
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=59
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=60
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=61
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=62
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=63
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=64
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=65
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=66
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=67
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=68
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=69
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=70
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=71
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=72
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=73
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=74
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=75
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=76
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=77
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=78
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=79
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=80
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=81
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=82
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=83
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=84
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=85
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=86
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=87
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=88
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=89
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=90
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=91
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=92
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=93
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=94
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=95
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=96
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=97
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=98
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=99
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=100
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=101
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=102
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=103
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=104
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=105
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=106
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=107
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=108
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=109
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=110
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=111
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=112
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=113
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=114
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=115
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=116
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=117
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=118
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=119
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=120
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=121
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=122
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=123
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=124
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=125
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=126
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=127
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=128
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=129
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=130
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=131
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=132
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=133
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=134
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=135
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=136
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=137
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=138
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=139
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=140
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=141
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=142
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=143
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=144
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=145
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=146
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=147
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=148
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=149
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=150
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=151
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=152
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=153
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=154
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=155
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=156
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=157
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=158
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=159
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=160
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=161
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=162
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=163
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=164
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=165
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=166
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=167
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=168
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=169
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=170
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=171
GO
UPDATE [Zauber] SET [Repräsentationen] = N'Sch 7, Geo 5, Dru 2(Geo)' ,[Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=172
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=173
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=174
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=175
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=176
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=177
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=178
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=179
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=180
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=181
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=182
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=183
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=184
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=185
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=186
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=187
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=188
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=189
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=190
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=191
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=192
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=193
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=194
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=195
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=196
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=197
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=198
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=199
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=200
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=201
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=202
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=203
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=204
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=205
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=206
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=207
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=208
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=209
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=210
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=211
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=212
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=213
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=214
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=215
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=216
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=217
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=218
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=219
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=220
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=221
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=222
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=223
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=224
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=225
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=226
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=227
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=228
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=229
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=230
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=231
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=232
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=233
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=234
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=235
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=236
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=237
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=238
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=239
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=240
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=241
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=242
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=243
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=244
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=245
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=246
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=247
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=248
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=249
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=250
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=251
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=252
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=253
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=254
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=255
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=256
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=257
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=258
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=259
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=260
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=261
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=262
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=263
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=264
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=265
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=266
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=267
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=268
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=269
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=270
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=271
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=272
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=273
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=274
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=275
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=276
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=277
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=278
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=279
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=280
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=281
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=282
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=283
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=284
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=285
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=286
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=287
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=288
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=289
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=290
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=291
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=292
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=293
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=294
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=295
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=296
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=297
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=298
GO
UPDATE [Zauber] SET [Repräsentationen] = N'Mag 3, Elf 2, Hex 2, Geo 2, Dru 2(Mag), Dru 2(Geo), Dru 2(Hex), Ach 1' ,[Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=299
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=300
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=301
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=302
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=303
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=304
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=305
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=306
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=307
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=308
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=332
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=333
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=334
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=335
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=336
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=337
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=338
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=339
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=340
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=341
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=342
GO
UPDATE [Zauber] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [ZauberID]=343
GO

Update [Ausrüstung] set Setting = 'Aventurien' WHERE Setting is null
GO
Update [Ausrüstung] set Literatur = Replace(Literatur,' , ',', ') WHERE Literatur like '% , %'
GO