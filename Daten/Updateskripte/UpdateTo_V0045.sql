-- Ausrüstungstabellen geändert. Vererbungsschema angewandt.
CREATE TABLE [Ausrüstung] (
	[AusrüstungGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(200) NOT NULL, 
	[Preis] float NOT NULL DEFAULT 0, 
	[Gewicht] int NOT NULL DEFAULT 0, 
	[Verbreitung] nvarchar(300), 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(100), 
	[Bemerkung] ntext,
	CONSTRAINT [PK_Ausrüstung] PRIMARY KEY ([AusrüstungGUID])
)
GO

INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000001','Achfawar',300,100,'(2) ACH','Aventurisches Arsenal 93',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000002','Amazonensäbel',180,75,'(12) WEI, GAR, ALB, HOR, ALM, ARA, ORO','Aventurisches Arsenal 38',NULL,'Vom Pferderücken aus gegen den Fußlämpfer richtet ein Amazonensäbel zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000003','Andergaster',350,220,'(6) RIV, WEI, GAR, AND, ALB, ALM, GAL, HOR','Aventurisches Arsenal 32',NULL,'Der Schlag eines Andergasters kann mit Fechtwaffen und Dolchen nocht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000004','Anderthalbhänder',250,100,'(7) WEI, GAR, ALB, ALM, HOR','Aventurisches Arsenal 25',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000005','Arbach',120,100,'(10) ORK,  SVE (nur bei ORKS)','Aventurisches Arsenal 92',NULL,'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000006','Baccanaq',180,80,'(4) ALA, BRA, CHA','Aventurisches Arsenal 83',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000007','Barbarenschwert',200,100,'(6) THO (nur Gjalskerland), FIR, GAR (nur Trollzacken)','Aventurisches Arsenal 74',NULL,'Vom Pferderücken aus gegen Fußkämpfer, richtet das Barbarenschwert zwei zusätzliche TP an. Erfordert mindest KK von 15 für einhändige Führung mit dem Talent Schwerter')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000008','Barbarensteitaxt',150,250,'(6) FIR, THO, GAR (nur Trollzacken)','Aventurisches Arsenal 75',NULL,'Erfordert mindest KK von 15')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000009','Basiliskenzunge',70,25,'(6) ALM, HOR, ZYK, KHU, THA, MEN, BR, ALA','Aventurisches Arsenal 67',NULL,'Mit einer Basiliskenzunge  können Schläge von Kettenwaffen (mit der Außnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000010','Bastardschwert',200,120,'(8) GLO, BOR, ORK, THO, AND, WEI, ALB','Aventurisches Arsenal 25',NULL,'Erfordert mindest KK von 15 für einhändige Führung mit dem Talent Schwerter')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000011','Beil',20,70,'(18) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 11',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000012','Beil (Stein)',15,80,'(18)ORK, EHE, ALA, BRA, WAL, CHA','Aventurisches Arsenal 11',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000015','Boronssichel',400,160,'(5) ALM, ARA, ORO, ART, MHA','Aventurisches Arsenal 33',NULL,'Der Schlag einer Boronsichel kann mit Fechtwaffen und Dolchen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000016','Brabakbengel',100,120,'(7) GLO, THO, ORK, AND, ALB, GAL RHA, XER, MEN, BRA, ALA, THA','Aventurisches Arsenal 26',NULL,'Mit dem zentralen Dorn kann mit dem Talent Dolche auch gestochen werden: 1W6 TP, AT um 3 erschwert, eigene nächste PA ebenfalls um 3 erschwert.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000017','Breitschwert',120,80,'(15) SVE, THO, ORK, ZYK, MEN, BRA, ALA','Aventurisches Arsenal 26',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000018','Byakka',90,130,'(9) ORK, SVE, AND (nur bei Orks)','Aventurisches Arsenal 92',NULL,'Ein Stich wird auf das Talent Dolche (AT erschwert um 3, eigene nächste PA ebenfalls erschwert um 3) abgelegt')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000019','Degen',150,40,'(6) ALB, GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 60',NULL,'Mit dem Degen können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000020','Dolch',20,20,'(18) alle Regionen außer FIR, EHE, WAL','Aventurisches Arsenal 11',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000021','Doppelkhunchomer',250,150,'(5) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 78, ERRATA 4',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000022','Drachentöter',0,400,'(2) ZWE','Aventurisches Arsenal 86',NULL,'Wird immer von Spießgespann (siehe Sonderfertigkeit) geführt.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000023','Drachenzahn',120,40,'(12) ZWE','Aventurisches Arsenal 86',NULL,'Mit einem Drachenzahn  können Schläge von Kettenwaffen (mit der Außnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000024','Dreizack',50,90,'(5) THO, ALB, HOR, MEN, BRA, ALA, THA, KHU, GAR (perricum), ARA, ORO, MAR, XER, BOR','Aventurisches Arsenal 18',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000025','Dreschflegel',15,100,'(18) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 12',NULL,'Schläge eines Dreschflegels können mit Dolchen oder Fechtwaffen nicht pariert werden, ein Dreschflegel ignoriert den Verteidigungsbonus von Schilden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000026','Dschadra',120,80,'(10) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 78',NULL,'Die hier angegebenen Werte gelten für die Verwendung im Nahkampf.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000027','Eberfänger',60,40,'(12) THO, SVE, RIV, BOR, AND, WEI, TOB, GAL, GAR, ALB, ZWE','Aventurisches Arsenal 19',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000028','Echsische Axt',0,90,'(2) ACH','Aventurisches Arsenal 94, ERRATA 5',NULL,'Ein Stich wird auf das Talent Speere (AT erschwert um 3) abgelegt ein erfolgreicher Einsatz des Dorns richtet 1W64 TP an.Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000029','Echsisches Szepter',0,120,'nur bei Achaz-Schamanen','Aventurisches Arsenal 55',NULL,' unverkäuflich')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000030','Efferdbart',80,90,'(4) THO, ALB, HOR, GAR (Perricum), ARA, BOR','Aventurisches Arsenal 54, Errata 3',NULL,' oder nach Geweihtem')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000031','Entermesser',50,70,'(12) GLO, RIV, SVE, THO, ALB, HOR, ZYK, MEN, BRA, ALA, CHA, THA, KHU, ORO, ARA, GAR, XER, BOR','Aventurisches Arsenal 12',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000032','Fackel',0.5,30,'nach Bedarf','Aventurisches Arsenal 17',NULL,'Richtet der Schlag SP an, kommen noch 1W-1 SP Feuerschaden hinzu! Fällt eine 6, so erlischt die Fackel.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000034','Felsspalter',300,150,'(5) ZWE','Aventurisches Arsenal  87, ERRATA 4',NULL,'beachte die Regelung im Arsenal, Ein (zwergischer) Waffenmeister (Felsspalter) hat gegen alle Gegner der Größenklassen groß oder größer eine Attacke-Erleichterung von 2 Punkten, egal welches (mit dem Felsspalter erlaubte und von ihm bereits erlernte) Angriffsmanöver er durchführt. Er darf Finten und Defensiven Kampfstil nutzen. Der Hammerschlag ist für ihn um 2 Punkte erleichtert (gegen große Wesen also sogar um 4 Punkte). Um diese SF zu erlernen, muss der zwergische Kämpfer eine GE und eine KK von jeweils 17 aufweisen')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000035','Fleischerbeil',20,60,'(12) alle Regionen ausser FIR, NIV, ELF, EHE, WAL','Aventurisches Arsenal 13',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000036','Florett',180,30,'(3) ALM, HOR','Aventurisches Arsenal 61',NULL,'Mit dem Florett können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000037','Geißel',15,30,'(6) alle Regionen ausser FIR, NIV, ELF, EHE, THO, ZWE, WAL','Aventurisches Arsenal 13',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000038','Glefe',45,120,'(10) BOR, WEI, GAR, AND, ALB, ALM, TOB, GAL, XER, RHA, HOR, ZYK, ARA','Aventurisches Arsenal 33',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000039','Großer Sklaventod',350,160,'(4) ALA, BRA, CHA','Aventurisches Arsenal 84, ERRATA 4',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000040','Gruufhai',120,180,'(8) ORK, SVE, AND (nur bei Orks)','Aventurisches Arsenal 93',NULL,'Schläge eines Gruufhai können mit Dolchen und Fechtwaffen nicht pariert werden. Kann ab KK 18 Einhändig mit Hiebwaffen geführt werden (TP/KK dann 15/3).')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000042','Hakenspieß',70,120,'(12) WIE, GAR, AND, ALB, ALM, TOB, XER, GAL, RHA, HOR, ARA','Aventurisches Arsenal 33',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000043','Haumesser',40,90,'(12) alle Regionen ausser FIR, NIV, ELF, EHE, WAL','Aventurisches Arsenal 13',NULL,'Für Haumesser gilt nicht der TP-Bonus für Reiter')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000044','Hellebarde',75,150,'(10) GLO, BOR, WEI, GAR, AND, ALB, ALM, TOB, GAL, RHA, XER, HOR, ARA','Aventurisches Arsenal 34',NULL,'Schläge einer Hellebarde können mit Dolchen und Fechtwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000045','Holzfälleraxt',80,160,'(12) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 13',NULL,'Schläge mit der Holzfälleraxt können von Dolchen und Fechtwaffen nicht pariert werden')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000046','Holzspeer',10,60,'(16) alle Regionen ausser FIR, KHO','Aventurisches Arsenal 19',NULL,'Ein Steinspitze richtet gegen nur mit Stoff, dünnem Leder oder Fell gerüstete Gegner einen TP mehr an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000047','Jagdmesser',50,15,'(14) alle Regionen ausser EHE und WAL','Aventurisches Arsenal 19',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000048','Jagdmesser (elfische Version)',0,15,'(6) FIR, ELF','Aventurisches Arsenal 90',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000049','Jagdspieß',80,80,'(8) alle außer EHE, ZWE','Aventurisches Arsenal 20',NULL,'Kann ab KK 16 einhändig geführt werden (TP/KK dann 13/5).')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000050','Jagdspieß (elfische Variante)',80,75,'(16) ELF','Aventurisches Arsenal 20, 90, ERRATA 1, 4',NULL,'beachte Regelung im Arsenal (S. 91). Kann ab KK 16 einhändig geführt werden (TP/KK dann 13/5).')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000051','Kampfstab',40,80,'(8) alle Regionen ausser FIR, ZWE, ACH, WAL','Aventurisches Arsenal 62',NULL,'Das Entwaffnen aus der AT ist um 2 Punkte erleichtert.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000052','Kettenstab',120,100,'(6) ARA, ORO, MAR, SKR, KHU, THA, SHI','Aventurisches Arsenal 65',NULL,'beachte Regelung im Arsenal. Erfordert mindestens GE 15. Das Entwaffnen aus der AT ist um 3 Punkte erleichtert.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000053','Kettenstab (Drei-Glieder-Stab)',180,100,'(2) ORO, MAR, SKR, KHU','Aventurisches Arsenal 65',NULL,'beachte Regelung im Arsenal, Der Kettenstab/Drei-Glieder-Stab ignoriert den PA Bonus von Schilden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000054','Keule',15,100,'(18) alle','Aventurisches Arsenal 66',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000055','Khunchomer',130,90,'(12) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 79',NULL,'Vom Pferderücken aus gegen Fußkämpfer, richtet ein Khunchomer zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000056','Knochenkeule, groß',0,220,'je nach Schamane','Aventurisches Arsenal 55',NULL,'Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000057','Knochenkeule, klein',0,80,'je nach Schamane, unverkäuflich','Aventurisches Arsenal 54',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000058','Knochenkeule, mittel',0,110,'je nach Schamane unverkäuflich','Aventurisches Arsenal 54',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000059','Knüppel',1,60,'(18) alle','Aventurisches Arsenal 14',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000060','Korspieß',200,140,'(4) ALM, HOR, ORO, MHA, GOR, KHU, MAR, THA, ALA','Aventurisches Arsenal 55',NULL,'Schläge mit einem Korspieß können mit Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000061','Kriegsfächer',250,50,'(2) ARA, ORO','Aventurisches Arsenal 80',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000062','Kriegsflegel',50,120,'(6) BOR, WEI, GAR, AND, ALB, ALM, TOB, GAL, RHA, XER, HOR, ARA','Aventurisches Arsenal 34',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000063','Kriegshammer',120,180,'(6) alle Regionen ausser NIV, ELF, EHE, WAL','Aventurisches Arsenal 34',NULL,'Schläge eines Kriegshammers können mit Dolchen und Fechtwaffen nicht pariert werden. Kann ab KK18 einhändig geführt werden mit dem Talent Hiebwaffen (TP/KK dann 15/3),  die TP/KK betragen dann 15/3')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000064','Kriegslanze',120,150,'(8) BOR, WEI, GAR, AND, ALB, ALM, HOR, GAL, XER, RHA, ARA','Aventurisches Arsenal 38',NULL,'Die hier angegebenen Werte gelten für die Zweckfremde Verwendung als improvisierter Speer.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000065','Kurzschwert',80,40,'(16) alle Regionen ausser FIR, NIV, EHE, WAL','Aventurisches Arsenal 27',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000066','Kusliker Säbel',160,70,'(6) ALB, HAO, ZYK, MEN, BA, ALA','Aventurisches Arsenal 62',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000067','Langdolch',45,30,'(14) alle ausser FIR, EHE, ORK, WAL','Aventurisches Arsenal 66',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000068','Langschwert',180,80,'(14) alle Regionen ausser FIR, NIV, EHE, WAL','Aventurisches Arsenal 27',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000069','Lindwurmschläger',120,95,'(12) ZWE','Aventurisches Arsenal 88',NULL,'beachte die Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000071','Magierdegen',150,25,'nur in Orten mit Magierakademien, wird nur an lizensierte Gildenmagier verkauft.','Aventurisches Arsenal 56',NULL,'Mit dem Magierdegen können Schläge von Kettenwaffen, Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwertern nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000072','Magierrapier',200,35,'nur in Orten mit Magierakademien, wird nur an lizensierte Gildenmagier verkauft.','Aventurisches Arsenal 56',NULL,'Mit dem Magierdegen können Schläge von Kettenwaffen, Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwertern nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000073','Magierstab (kurz)',0,70,'je nach Magier, unverkäuflich','Aventurisches Arsenal 56',NULL,'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000074','Magierstab (sehr kurz)',0,60,'je nach Magier, unverkäuflich','Aventurisches Arsenal 57',NULL,'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000075','Magierstab als Stab',0,90,'je nach Magier, unverkäuflich','Aventurisches Arsenal 56',NULL,'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000076','Magierstab m. Kristallk.',0,150,'je nach Magier, unverkäuflich','Aventurisches Arsenal 56, ERRATA 3',NULL,'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000077','Mengbilar',200,20,'(2) GAL, XER, RHA, ORO, MAR, SHI, MEN, BRA, ALA','Aventurisches Arsenal 68',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000078','Menschenfänger',200,200,'(2) WEI, BOR, TOB, GAR, GAL, XER, RHA, ALM, HOR, THA, ALA','Aventurisches Arsenal 67',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000079','Messer',10,10,'(18) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 14',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000080','Messer (Steinklinge)',5,12,'(12) FIR, EHE, ORK, WAL, ALA, BRA, CHA','Aventurisches Arsenal 14',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000081','Meucheldolch',80,15,'sehr selten und üblicherweise nicht frei verkäuflich (außer GLO, XER, GAL, RHA, MAR, MEN und ALA)','Aventurisches Arsenal 67',NULL,'beachte Regelung im Arsenal / BF, Gewicht und Länge können abweichen')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000082','Molokdeschnaja',90,100,'(6) Bor, NIV, RIV, SVE','Aventurisches Arsenal 75',NULL,'Vom Pferderücken aus gegen Fußkämpfer, richtet die Molokdeschnaja  zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000083','Morgenstern',100,140,'(8) GLO, THO, ORK, WEI, GAL, XER, RHA, AND, ALB, GAR, ALA','Aventurisches Arsenal 27',NULL,'Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist).')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000084','Nachtwind',500,70,'(4) MAR, SHI, KHU, THA','Aventurisches Arsenal 82',NULL,'beachte Regelung im Arsenal. Erfordert mindestens GE 16 für einhändige Führung mit dem Talent Schwerter.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000085','Neethaner Langaxt',160,160,'(4) HOR, MEN, BRA','Aventurisches Arsenal 35',NULL,'Schläge einer Neethaner Langaxt können mit Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000086','Neunschwänzige',60,80,'(4) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 15',NULL,'Bei einer AT/2 wird ein TP mehr angerichtet.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000087','Ochsenherde',250,300,'(4) GLO, THO, ORK, WEI, TOB, GAL, XER, RHA, AND, ALB, GAR, ALA','Aventurisches Arsenal 28',NULL,'Erfordert mindest KK 16. Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist). Eine 19 zählt bei dieser Waffe auch als Patzer.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000088','Ogerfänger',150,35,'(8) THO, SVE, BOR, AND, WEI, TOB, GAL, GAR, ALB','Aventurisches Arsenal 20',NULL,'Klinge bleibt in der Wunde stecken und verursacht pro SR 1W6 SP. Die Entfernung erzeugt 1W-1 bei gelungener Heilkunde Probe, sonst 2W6 SP')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000089','Ogerschelle',180,240,'(6) GLO, THO, ORK, WEI, TOB, GAL, XER, RHA, AND, ALB, GAR, ALA','Aventurisches Arsenal 28',NULL,'Erfordert mindest KK 15. Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist).')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000090','Orchidee',180,35,'(4) ORO, MENBRA, ALA, CHA','Aventurisches Arsenal 69',NULL,'beachte Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Veteranenhand als unbewaffnet.Ein Kämpfer mit einer Orchidee besitzt allerdings einen Zonen-RS von 1, ein Kämpfer mit einer Veteranenhand einen Zonen-RS von 2.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000091','Orknase',75,110,'(14) THO, SVE, bei thorwalischen Söldner-Ottaskin','Aventurisches Arsenal 76, ERRATA 4',NULL,'beachte Regeleung im Arsenal, Kann ab KK 14 einhändig geführt werden (TP/KK dann 13/3). Schläge der Waffe können von Dolchen und Fechtwaffen nicht')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000092','Pailos',300,180,'(2) ZYK','Aventurisches Arsenal 85',NULL,'beachte Regelung im Arsenal, Mit der Stoßklinge kann mit dem Talent Speere auch gestochen werden: 1W7 TP, AT um 5 erschwert, eigene nächste PA ebenfalls um 5 erschwert')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000094','Panzerstecher',120,80,'(4) ALM, HOR','Aventurisches Arsenal 28',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000095','Partisane',80,150,'(8) WEI, BOR, TOB, AND, ALB, GAR, XER, RHA, ALM, HOR, THA, ALA','Aventurisches Arsenal 35',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000096','Peitsche',25,60,'(10) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 15',NULL,'In der DK Nahkampf ein AT-Malus von 8, im DK Handgemenge nicht zu verwenden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000097','Pike',50,180,'(12) BOR, GAR','Aventurisches Arsenal 36',NULL,' GAL, XER, RHA, ALM, HOR, ARA')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000098','Rabenschnabel',130,90,'(8) ALM, ARA, MHA, GOR, KHU, KHO, HOR, ART, MEN, THA, ALA','Aventurisches Arsenal 39, 57',NULL,'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet ein Rabenschnabel zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000099','Rapier',120,45,'(6) ALB, GAR, ALM, HOR, ZYK, MEN, ALA','Aventurisches Arsenal 63',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000100','Rapier, Raufdegen (Almada)',120,45,'(6) ALB, GAR, ALM, HOR, ZYK, MEN, ALA','Aventurisches Arsenal 63',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000101','Reißer (echsische gezahnte Axt)',0,110,'(1) ACH','Aventurisches Arsenal 94, ERRATA 5',NULL,'Ein Stich wird auf das Talent Speere (AT erschwert um 4) abgelegt ein erfolgreicher Einsatz des Dorns richtet 1W65 TP an. Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000102','Richtschwert',0,200,'unverkäufliche Einzelstücke an allen Orten mit einer entsprechenden Gerichtsbarkeit','Aventurisches Arsenal 57',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000103','Robbentöter',200,70,'(4) FIR','Aventurisches Arsenal 91, ERRATA 5',NULL,'Vom Pferderücken aus gegen Fußkämpfer, richtet ein Robbentöter zwei zusätzliche TP an. Die Rückseite der Klinge ist meist bewusst stumpf gehalten, um bei Genickschlägen das Fell nicht zu verletzen. Die Manöver Stumpfer Schlag und Betäubungsschlag sind jeweils um 2 Punkte erleichtert. Bei der Waffenmeister-SF muss es heißen: »Ein Waffenmeister (Säbel) kann auch den Robbentöter einsetzen, ...')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000104','Rondrakamm',0,130,'je nach Rondrageweihtem','Aventurisches Arsenal 58',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000105','Runaskraja',120,70,'nur in Thorwal und Olport wird in Thorwal nur an lizensierte Gildenmagier verkauft','Aventurisches Arsenal 58',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000106','Säbel',100,60,'(10) GAR, ALB, LM, HOR, MEN, BRA, GAL, XER, RHA, ARA, ORO, ALA','Aventurisches Arsenal 39',NULL,'Vom PFerderücken aus gegen einen Fußkämpfer eingesetzt, richtet ein Säbel zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000107','Sägefischschwert',0,60,'nur bei Tocamujac-Schamanen','Aventurisches Arsenal 55',NULL,' unverkäuflich')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000108','Scheibendolch',0,40,'(10) WEI, AND, TOB, GAL, GAR, XER, RHA, ALB, ZWE, ALM, HOR','Aventurisches Arsenal 30, ERRATA 2',NULL,'siehe Reglung in der ERRATA')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000109','Schlagring',25,20,'(12) alle Regionen ausser FIR, ELF, EHE, ZWE, WAL','Aventurisches Arsenal 68',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000110','Schmiedehammer',0,150,'je nach Geweihtem','Aventurisches Arsenal 58',NULL,'Der Schmiedehammer ist eine geweihte Waffe und daher in der Lage gegen profane Waffen unempfindliche oder weniger empfindliche Wesen zu verletzen.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000111','Schnitter',120,90,'(10) ARA, ORO, MAR, SHI, KHU, MHA, THA, ACH, ALA, BRA, CHA','Aventurisches Arsenal 36',NULL,'Kann ab GE 16 einhändig geführt werden (TP 1W3, TP/KK 15/5, WM 0/-1)')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000112','Schwerer Dolch',40,30,'(16) alle ausser EHE, WAL','Aventurisches Arsenal 69',NULL,'Mit dem Schweren Dolch  können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000113','Sense',30,100,'(16) alle Regionen ausser FIR, NIV, GLO, EHE, ELF, ORK, KHO, ACH, WAL, CHA','Aventurisches Arsenal 16',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000114','Sichel',25,30,'(12) alle Regionen außer FIR, EHE, WAL','Aventurisches Arsenal 16',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000115','Sklaventod',250,80,'(10) ALA, BRA, THA, CHA','Aventurisches Arsenal 84',NULL,'Vom Pferderücken aus gegen Fußkämpfer, richtet ein Sklaventod zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000116','Skraja',50,90,'(12) THO, SVE, bei thorwalischen Söldner-Ottaskin','Aventurisches Arsenal 76',NULL,'Ein Stich mit dem Dorn der Skraja (AT auf das Talent Dolche, erschwert um 3) richtet 1W63 TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000117','Sonnenszepter',0,90,'je nach Praios-Geweihtem','Aventurisches Arsenal 59',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000118','Speer',30,80,'(18) alle Regionen','Aventurisches Arsenal 21, ERRATA 1',NULL,'Kann ab KK 15 einhändig geführt werden (TP/KK dann 13/5). Die Manöver Gegenhalten und Gezielter Stich sind für ihn um 2 Punkte erleichtert.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000119','Spitzhacke',30,200,'(8) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 16',NULL,'Einhändig ab KK 18, dann TP/KK 14/3')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000120','Stockdegen',180,35,'(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 64',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000122','Stoßspeer',100,150,'(10) FIR, GLO, NIV, RIV, SVE, THO, ORK, WIE, BOR, TOB, AND, ALB, GAR, GAL, XER, RHA, ALM, HOR','Aventurisches Arsenal 22',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000123','Streitaxt',50,120,'(12) alle Regionen ausser ELF, EHE, WAL','Aventurisches Arsenal 30',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000124','Streitkolben',50,120,'(12) alle Regionen ausser FIR, NIV, ELF, EHE, SVE, ACH, WAL','Aventurisches Arsenal 30',NULL,'Vom Pferderücken aus gegen den Fußkämpfer eingesetzt, richtet der Streitkolben zwei zusätzliche TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000125','Stuhlbein',0,40,'nach Bedarf','Aventurisches Arsenal 17',NULL,'siehe Regeln zu improvisierten Waffen')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000126','Sturmsense',40,120,'(12) BOR, WEI, GAR, AND, ALB, ALM, TOB, HOR, ARA','Aventurisches Arsenal 36',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000127','Turnierlanze',50,120,'(8) BOR, WEI, GAR, AND, ALB, ALM, HOR, GAL, XER, RHA, ARA','Aventurisches Arsenal 39',NULL,'Die hier angegebenen Werte gelten für die Zweckentfremdung als improvisierter Speer. Ihre Werte im Reiterkampf siehe WdS.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000128','Turnierschwert',80,60,'(10) BOR, WEI, AND, ALB, GAR, ALM, ARA, HOR','Aventurisches Arsenal 31',NULL,'Es existieren auch hölzerne, ähnlich ausgewuchtete Übrungsvarianten (1W1 TP (A), BF 5).')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000129','Tuzakmesser',400,100,'(4) BOR (nur Festum), ARA, ORO, MAR, SHI, KHU, THA','Aventurisches Arsenal 82',NULL,'beachte Regelung im Arsenal. Mindest GE 15, ansonsten pro Punkt unter 15 TP, INI und PA -1.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000130','Veteranenhand',250,70,'(4) ALB, HOR, ALM, GAR, GAL, XER, BOR (nur Festum), ORO, MEN, BRA, ALA, CHA','Aventurisches Arsenal 69',NULL,'beachte Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Veteranenhand als unbewaffnet.Ein Kämpfer mit einer Orchidee besitzt allerdings einen Zonen-RS von 1, ein Kämpfer mit einer Veteranenhand einen Zonen-RS von 2.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000131','Vorschlaghammer',30,250,'(8) alle Regionen ausser EHE, WAL','Aventurisches Arsenal 16',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000132','Vulkanglasdolch',0,30,'je nach Druide','Aventurisches Arsenal 59',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000133','Waqqif',60,35,'(10) KHO, MHA, ARA, KHU, GOR, THA, ART','Aventurisches Arsenal 80',NULL,'Mit dem Waqqif können die Schläge von Kettenwaffen (mit der Ausnahme vonGeißel und Neunschänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwertern oder -säbeln nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000134','Warunker Hammer',150,150,'(4) WEI, GAR, ALM, TOB, GAL, RHA, XER','Aventurisches Arsenal 37',NULL,'Der Schlag eines Warunker Hammers  kann mit Fechtwaffen und Dolchen nicht pariert werden. Mit der Stoßklinge kann mit dem Talent Speere auch gestochen werden: 1W5 TP, AT um 3 erschwert')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000135','Wolfsmesser',250,50,'(4) ELF','Aventurisches Arsenal 91, ERRATA 5',NULL,'Vom Pferderücken aus gegen Fußkämpfer, richtet ein Wolfsmesser zwei zusätzliche TP an. Im Sinne der Parade-Einschränkungen von Fechtwaffen gilt das Wolfsmesser als Schwert.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000141','Wurmspieß',120,120,'(9) ZWE','Aventurisches Arsenal 89',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000142','Zweihänder',250,160,'(9) BOR, NIV, WEI, AND, GAR, ALB, ALM, GAL, RHA, XER, HOR','Aventurisches Arsenal 37',NULL,'Der Schlag eines Zweihänders kann mit Fechtwaffen und Dolchen nicht pariert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000143','Zweililien',200,80,'(6) ALB, HOR, ZWE, MHA','Aventurisches Arsenal 64',NULL,'beachte Regelung im Arsenal. Erleichtert das entwaffnen aus der AT um 4 Punkte (Errata WdS S.4)')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000144','Zwergenschlägel',150,120,'(8) ZWE','Aventurisches Arsenal 89',NULL,'Mit dem Zwergenschlägel können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht parriert werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000145','Zwergenskraja',100,80,'(8) ZWE','Aventurisches Arsenal 89, ERRATA 4',NULL,'Ein Stich mit dem Dorn der Zwergenskraja (AT auf das Talent Dolche, erschwert um 4) richtet 1W63 TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000146','Drachenklaue',350,200,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,'Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000147','Drachenklaue mit langer Klinge',390,200,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,'Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden. Mit langer Klinge auch mit dem Schwinger.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000148','Drachenklaue mit Klingenfänger',390,200,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,'Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden. Das Manöver Entwaffnen aus der Parade ist um 2 erleichtert.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000149','Drachenklaue mit Klingenbrecher',410,200,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,'Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden. Die Manöver Waffe zerbrechen und Entwaffnen aus der Parade sind um 2 erleichtert.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000150','Orchidee mit verstärktem Handschuh',200,35,'(4) ORO, MENBRA, ALA, CHA','Aventurisches Arsenal 69',NULL,'beachte Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Veteranenhand als unbewaffnet.Ein Kämpfer mit einer Orchidee besitzt allerdings einen Zonen-RS von 1, ein Kämpfer mit einer Veteranenhand einen Zonen-RS von 2.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000001','Arbalette',600,200,'(4) ZWE, HOR, ALM','Aventurisches Arsenal 41','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000002','Arbalone',800,480,'(2) HOR','Aventurisches Arsenal 43','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000003','Balestra',500,150,'(3) HOR, ZWE','Aventurisches Arsenal 41','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000004','Balestrina',450,60,'(5) HOR, ZWE, MEN, ALM','Aventurisches Arsenal 41','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000005','Balläster',200,120,'(8) ZWE, BOR, GAR, ALM, HOR, KHU, ALA','Aventurisches Arsenal 18','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000006','Blasrohr',40,15,'(8) MEN, WAL, BRA, CHA','Aventurisches Arsenal 83','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000007','Borndorn',40,30,'(6) BOR, ALB, GAR, XER, ALM, HOR, ARA, ORO, MEN, BRA, ALA, CHA','Aventurisches Arsenal 72',NULL,'Es gelten die üblichen Parade-Einschränkungen für Dolche')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000008','Diskus',25,30,'(6) MAR, SKR, SHI, KHU, THA, sehr selten auch bei Exilanten in Festum oder auch Al''Anfa','Aventurisches Arsenal 81','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000010','Dschadra',120,80,'(10) KHO, MHA, ARA, ORO, KHU, GOR, THA, ART','Aventurisches Arsenal 79','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000012','Eisenwalder',400,100,'(6) ZWE, ALM, HOR','Aventurisches Arsenal 87','Aventurien','Füllen des Magazins dauert 20 Aktionen, das Magazin fasst 10 Schuss')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000013','Elfenbogen',0,25,'(8) ELF','Aventurisches Arsenal 90','Aventurien','unverkäuflich')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000014','Fledermaus',10,20,'(8) bei den Brillantzwergen  (10) in KHO, ART, MHA','Aventurisches Arsenal 88','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000015','Gandrasch-Armbrust M1024 (doppelt gespannt)',600,280,'(2) ZWE, GAR','Aventurisches Arsenal 42','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000016','Gandrasch-Armbrust M1024',600,280,'(2) ZWE, GAR','Aventurisches Arsenal 42','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000017','Granatapfel',0,40,'(1) HOR, MEN','Aventurisches Arsenal 44','Aventurien','unverkäuflich')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000019','Jagddiskus',30,35,'(6) MAR, SKR, SHI','Aventurisches Arsenal 81','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000021','Kampfdiskus',60,50,'(4) MAR, SKR, SHI sehr selten auch bei Exilanten in Festum oder auch Al''Anfa','Aventurisches Arsenal 81, Errata 4','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an. Der Kampfdiskus setzt eine KK von 14 zum korrekten Einsatz vorraus, ansonsten gilt er als improvisierte Wurfwaffe')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000022','Kettenkugel',150,250,NULL,'Wege des Schwertes 130, Errata','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000023','Kompositbogen',80,25,'(16) KHO, MHA, ARA, ORO, ART, KHU, GOR, THA','Aventurisches Arsenal 79','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000024','Krähenfüße, 10 Stück',20,10,NULL,'Katakomben und Kavernen 125','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000025','Kriegsbogen',100,45,'(4) WEI, AND, GAR, TOB, GAL','Aventurisches Arsenal 44','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000026','Kurzbogen',45,20,'(16) alle Regionen ausser FIR, NIV, GLO, EHE','Aventurisches Arsenal 20','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000027','Langbogen',60,30,'(16) AND, ALB, WEI, TOB','Aventurisches Arsenal 45','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000028','Lasso',12,40,'(8) RIV, SVE, ORK, BOR, WEI, ARA, ORO, KHO','Aventurisches Arsenal 14','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000029','Leichte Armbrust',180,150,'(10) alle ausser FIR, EHE, ELF, NIV, ORK, ACH, WAL','Aventurisches Arsenal 45','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000030','Orkischer Reiterbogen',120,40,'(8) Ork','Aventurisches Arsenal 93','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000031','Schleuder',15,10,'(6) WEI, BOR, TOB (bei den jeweils ansässigen Goblins), ZYK','Aventurisches Arsenal 21, Errata 1','Aventurien','Der Munitionspreis ist für spezielle Bleikugeln, die 1TP mehr anrichten und die Fernkampf-Probe um 1 Punkt erleichtern.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000032','Schneidezahn',60,50,'(10) THO, SVE bei thowalschen Söldner-Ottaskin','Aventurisches Arsenal 76','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000033','Schweres Wurfnetz',60,200,'(6) GLO, NIV, SVE, THO, XER, ARA, MHA, GOR, ALA, BRA','Aventurisches Arsenal 22','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000035','Speerschleuder',25,30,'(10) NIV, GLO, RIV, ORK, ARA, MHA, GOR, KHO, ART, ACH','Aventurisches Arsenal 22','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000036','Stabschleuder',15,40,'(16) alle ausser FIR, NIV, GLO, EHE','Aventurisches Arsenal 45','Aventurien','Der Munitionspreis ist für spezielle Bleikugeln, die 1TP mehr anrichten.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000037','Stein, Flasche',0,10,'alle Regionen ausser FIR, NIV, GLO, EHE','Aventurisches Arsenal 17','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000038','Windenarmbrust',350,200,'(7) GAL, GAR, ALB, ZWE, ALM, HOR, ARA, KHU, MEN, ALA','Aventurisches Arsenal 46','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000039','Wurfbeil',35,60,'(8) FIT, GLO, NIV, SVE, THO, ORK, AND, WIE, BOR, TOB','Aventurisches Arsenal 77','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000040','Wurfdolch',30,20,'(7) ALB, GAR, GAL, XER, ALM, HOR, ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, BRA, ALA, CHA','Aventurisches Arsenal 71',NULL,'Es gelten nicht nur die üblichen Parade-Einschränkungen für Dolche, sondern zudem ist das Wurfmesser noch eine imrovisierte Waffe für den Nahkampf')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000041','Wurfkeule',18,35,'(12) NIV, deutlich seltener auch den Sippen RIV und BOR','Aventurisches Arsenal 77',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000042','Wurfmesser',15,10,'(8) ALB, GAR, GAR, XER, ALM, HOR, ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, BRA, ALA, CHA','Aventurisches Arsenal 71',NULL,'Es gelten nicht nur die üblichen Parade-Einschränkungen für Dolche.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000043','Wurfnetz',35,80,'(6) GLO, NIV, SVE, THO, XER, ARA, MHA, GOR, ALA, BRA','Aventurisches Arsenal 22','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000044','Wurfscheibe, -ring, -stern',35,10,'(6) ARA, ORO, MHA, GOR, KHU, KHO, MAR, SHI, THA, MEN, ALA','Aventurisches Arsenal 72','Aventurien',NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0002-000000000045','Wurfspeer',30,80,'(13) ALM, ARA, ORO, MHA, GOR, KHU, THA, HOR, ART, ALA','Aventurisches Arsenal 21, 23','Aventurien','bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000001','Einfacher Holzschild',40,140,'(14) GLO, BOR, AND, GAR, TOB, GAL, XER, RHA, ALB, ALM, HOR, ZYK, MEN, MAR, WEI, ZWE','Aventurisches Arsenal 96, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000002','Verstärkter Holzschild',50,160,'(14) GLO, BOR, AND, GAR, TOB, GAL, XER, RHA, ALB, ALM, HOR, ZYK, MEN, MAR, WEI, ZWE','Aventurisches Arsenal 96, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000003','Lederschild',30,80,'(12) FIR, GLO, NIV, ORK, KHO, MHA, ARA, ORO, ART, WAL, CHA, WEI','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000004','Mattenschild',60,100,'(9) ACH, SKR, ORK, CHA ','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000005','Großer Lederschild',75,120,'(12) FIR, GLO, NIV, ORK, KHO, MHA, ARA, ORO, ART, WAL, CHA','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000006','Thorwalerschild',60,180,'(12) THO, SVE, RIV oder bei Thorwaler Solnder-Ottas','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000007','Großschild (Reiterschild)',100,200,'(14) THO, BOR, AND, GAR, TOB, GAL, XER, RHA, ALB, ALM, HOR, WEI','Aventurisches Arsenal 96, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000008','Turmschild',120,280,'(14) GAR, ALM, HOR, ALA, ZWE','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000009','Buckler',40,40,'(10) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 95',NULL,'Kann mit dem waffenlosen Manöver Gerade oder zum Angriff mit dem Schild verwendet werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000010','Großer (Vollmetall-) Buckler',60,60,'(10) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 95',NULL,'Kann mit dem waffenlosen Manöver Gerade oder zum Angriff mit dem Schild verwendet werden.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000011','Panzerarm',140,220,'(4) ALB, HOR, ALM, ALA','Aventurisches Arsenal 97, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000013','Bock',80,120,'(4) THO, AND, ALB, GAR, GAL, XER, HOR, ALM, MEN, MHA, GOR, KHU, THA, ALA, WEI','Aventurisches Arsenal 95, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000014','Hakendolch',90,50,'(3) ORO, MHA, GOR, KHU, THA, MAR, SHI','Aventurisches Arsenal 61',NULL,'beachte Regelung im Arsenal, Mit dem Hakendolch können auch die Angriffe von Kettenwaffen nicht pariert werden.(Errata WdS S.4)')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000015','Kriegsfächer',250,50,'(2) ARA, ORO','Aventurisches Arsenal 80',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000016','Linkhand',90,30,'(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 63',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000018','Linkhand mit Klingenfänger',105,35,'(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 63',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000019','Linkhand mit Klingenbrecher',115,40,'(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 63',NULL,'beachte Regelung im Arsenal')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000020','Hummerschere',0,70,NULL,'Aventurisches Arsenal 69',NULL,'Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Hummerschere als unbewaffnet.Ein Kämpfer mit einer Hummerschere hat einen Zonen-RS von 2.Die Werte für Gewicht und BF sind geraten.')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000001','Amazonenrüstung',0,320,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,'unverkäuflich')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000002','Anaurak',0,200,NULL,'Aventurisches Arsenal 100',NULL,'Preis variabel')
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000003','Armschienen, Bronze',25,60,'(4) ORK, GAR, ARA, ORO, ALM, HOR, ZYK, MEN, BRA, ALA','Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000004','Armschienen, Leder',15,40,NULL,'Aventurisches Arsenal 102',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000005','Armschienen, Stahl',35,60,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000006','Baburiner Hut',60,120,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000007','Bart/Halsberge',45,40,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000008','Beinschienen, Bronze',35,120,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000009','Beinschienen, Leder',25,40,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000010','Beinschienen, Stahl',50,120,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000011','Beintaschen/Schürze',90,80,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000012','Ringmantel (Brabakmantel)',180,360,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000013','Brigantina',350,240,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000014','Bronzeharnisch',250,240,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000015','Brustplatte, Leder',50,80,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000016','Brustplatte, Stahl',50,80,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000017','Brustschalen',25,20,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000018','Dicke Kleidung',0,120,NULL,'Aventurisches Arsenal 100, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000019','Drachenhelm',80,120,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000020','Eisenmantel',500,240,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000021','Fellumhang/Fuhrmannsmantel',0,120,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000022','Fünflagenharnisch',600,280,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000023','Gambeson/Wattierter Waffenrock',40,120,NULL,'Aventurisches Arsenal 101, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000024','Garether Platte',750,560,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000025','Gestechrüstung',2500,1200,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000026','Gladiatorenschulter',180,160,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000027','Hohe Stiefel',0,80,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000028','Horasischer Reiterharnisch',1000,680,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000029','Iryanrüstung',125,140,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000030','Kettenbeinlinge, Paar',200,320,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000031','Kettenhandschuhe, Paar',100,60,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000032','Kettenhaube',80,140,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000033','Kettenzeug',350,480,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000034','Kettenhaube, mit Gesichtsschutz',100,160,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000035','Kettenhemd, 1/2 Arm',150,260,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000036','Kettenhemd, lang',180,400,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000037','Kettenkragen',60,100,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000038','Kettenmantel',500,480,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000039','Kettenweste',100,200,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000040','Krötenhaut',60,160,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000041','Kürass',110,160,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000042','Kusliker Lamellar',500,300,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000043','Lederharnisch',80,180,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000044','Lederhelm',20,60,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000045','Lederhelm, verstärkt',30,70,NULL,'Aventurisches Arsenal 102',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000046','Lederzeug',40,80,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000047','Lederhose',0,80,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000048','Lederweste/Pelzweste',0,80,NULL,'Aventurisches Arsenal 100',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000049','Leichte Platte',250,300,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000050','Löwenmähne',100,200,NULL,'Aventurisches Arsenal 104',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000051','Mammutonpanzer',1500,240,NULL,'Aventurisches Arsenal 103, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000052','Maraskanischer Hartholzharnisch',1200,280,NULL,'Aventurisches Arsenal 103, Wege des Schwerts 135',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000053','Mattenrücken',65,140,NULL,'Aventurisches Arsenal 101',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000054','Morion',75,160,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000055','Panzerbein',150,240,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000056','Panzerhandschuhe, Paar',120,60,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000057','Panzerschuh',120,40,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000058','Plattenarme',200,120,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000059','Plattenschultern',150,120,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000060','Plattenzeug',500,380,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000061','Ringelpanzer',550,280,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000062','Schaller',60,160,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000063','Schaller (mit Bart)',100,200,NULL,'Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000064','Schuppenpanzer',1000,480,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000065','Schuppenpanzer, lang',1200,720,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000066','Spiegelpanzer',1000,400,NULL,'Aventurisches Arsenal 104, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000067','Stechhelm/Visierhelm',100,160,NULL,'Aventurisches Arsenal 108',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000068','Streifenschurz',40,120,NULL,'Aventurisches Arsenal 102, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000069','Sturmhaube',70,140,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000070','Tellerhelm',30,60,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000071','Topfhelm',80,180,NULL,'Aventurisches Arsenal 108, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000072','Tuchrüstung',50,100,NULL,'Aventurisches Arsenal 101, Wege des Schwerts 136',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000073','Unterzeug mit Kettenteilen',80,160,NULL,'Aventurisches Arsenal 101',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000074','Wattierte Kappe',5,20,NULL,'Aventurisches Arsenal 101',NULL,NULL)
GO
INSERT INTO [Ausrüstung] ([AusrüstungGUID],[Name],[Preis],[Gewicht],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0004-000000000075','Wattiertes Unterzeug/Wattierte Unterkleidung',25,100,NULL,'Aventurisches Arsenal 101, Wege des Schwerts 136',NULL,NULL)
GO

--fix für den Update-Crash 1.3 -> 1.4
DELETE FROM [Waffe]
		WHERE WaffeGUID in ('00000000-0000-0000-0001-000000000000', '00000000-0000-0000-0000-000000000146', '00000000-0000-0000-0000-000000000147', '00000000-0000-0000-0000-000000000148', '00000000-0000-0000-0000-000000000149', '00000000-0000-0000-0000-000000000150')
GO

ALTER TABLE [Schild] ADD CONSTRAINT [fk_Schild_Ausrüstung] FOREIGN KEY ([SchildGUID]) REFERENCES [Ausrüstung]([AusrüstungGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Waffe] ADD CONSTRAINT [fk_Waffe_Ausrüstung] FOREIGN KEY ([WaffeGUID]) REFERENCES [Ausrüstung]([AusrüstungGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Fernkampfwaffe] ADD CONSTRAINT [fk_Fernkampfwaffe_Ausrüstung] FOREIGN KEY ([FernkampfwaffeGUID]) REFERENCES [Ausrüstung]([AusrüstungGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Rüstung] ADD CONSTRAINT [fk_Rüstung_Ausrüstung] FOREIGN KEY ([RüstungGUID]) REFERENCES [Ausrüstung]([AusrüstungGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

CREATE TABLE [Held_Ausrüstung] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Angelegt] bit NOT NULL DEFAULT 0, 
	[Ort] nvarchar(50) NOT NULL DEFAULT 'Rucksack', 
	[AusrüstungGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	[Talentname] nvarchar(100), 
	[Anzahl] int, 
	[BF] int NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Held_Ausrüstung] PRIMARY KEY ([HeldGUID], [Ort], [AusrüstungGUID]), 
	CONSTRAINT fk_HeldAusrüstung_Held FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_HeldAusrüstung_Talent FOREIGN KEY ([Talentname])
		REFERENCES Talent ([Talentname])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_HeldAusrüstung_Ausrüstung FOREIGN KEY ([AusrüstungGUID])
		REFERENCES Ausrüstung ([AusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO


ALTER TABLE [Schild] DROP column [Name]
GO
ALTER TABLE [Schild] DROP column [Preis]
GO
ALTER TABLE [Schild] DROP column [Gewicht]
GO
ALTER TABLE [Schild] DROP column [Verbreitung]
GO
ALTER TABLE [Schild] DROP column [Literatur]
GO
ALTER TABLE [Schild] DROP column [Bemerkung]
GO

ALTER TABLE [Waffe] DROP column [Name]
GO
ALTER TABLE [Waffe] DROP column [Preis]
GO
ALTER TABLE [Waffe] DROP column [Gewicht]
GO
ALTER TABLE [Waffe] DROP column [Verbreitung]
GO
ALTER TABLE [Waffe] DROP column [Literatur]
GO
ALTER TABLE [Waffe] DROP column [Bemerkung]
GO

ALTER TABLE [Fernkampfwaffe] DROP column [Name]
GO
ALTER TABLE [Fernkampfwaffe] DROP column [Preis]
GO
ALTER TABLE [Fernkampfwaffe] DROP column [Gewicht]
GO
ALTER TABLE [Fernkampfwaffe] DROP column [Verbreitung]
GO
ALTER TABLE [Fernkampfwaffe] DROP column [Literatur]
GO
ALTER TABLE [Fernkampfwaffe] DROP column [Bemerkung]
GO

ALTER TABLE [Rüstung]  DROP column [Name]
GO
ALTER TABLE [Rüstung]  DROP column [Preis]
GO
ALTER TABLE [Rüstung]  DROP column [Gewicht]
GO
ALTER TABLE [Rüstung]  DROP column [Verbreitung]
GO
ALTER TABLE [Rüstung]  DROP column [Literatur]
GO
ALTER TABLE [Rüstung]  DROP column [Bemerkung]
GO

ALTER TABLE [Fernkampfwaffe] DROP column [Setting]
GO
ALTER TABLE [Rüstung]  DROP column [Setting]
GO
ALTER TABLE [Schild] DROP column [Setting]
GO