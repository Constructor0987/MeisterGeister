-- Kultur_Name Leerzeichenfehler
Update [Kultur_Name] SET [Herkunft]=Replace([Herkunft], ' ', ' ')
GO

-- WaffenGUIDs tabellenübergreifend eindeutig. Datenqualität verbessert. Improvisiert bit für Waffe eingefügt.
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000146','Drachenklaue',6,1,2,0,12,3,-1,0,0,0,200,20,350,'H','Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden.','(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000147','Drachenklaue mit langer Klinge',6,1,3,0,12,3,-1,0,0,1,200,30,390,'H','Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden. Mit langer Klinge auch mit dem Schwinger.','(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000148','Drachenklaue mit Klingenfänger',6,1,2,0,12,3,-1,0,0,0,200,20,390,'H','Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden. Das Manöver Entwaffnen aus der Parade ist um 2 erleichtert.','(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000149','Drachenklaue mit Klingenbrecher',6,1,2,0,12,3,-1,0,0,0,200,20,410,'H','Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden. Die Manöver Waffe zerbrechen und Entwaffnen aus der Parade sind um 2 erleichtert.','(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0000-000000000150','Orchidee mit verstärktem Handschuh',6,1,2,0,12,4,0,-1,-2,2,35,0,200,'H','beachte Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Veteranenhand als unbewaffnet.Ein Kämpfer mit einer Orchidee besitzt allerdings einen Zonen-RS von 1, ein Kämpfer mit einer Veteranenhand einen Zonen-RS von 2.','(4) ORO, MENBRA, ALA, CHA','Aventurisches Arsenal 69')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0003-000000000020','Hummerschere',6,1,2,0,12,4,-1,0,-1,4,70,0,0,'H','Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Hummerschere als unbewaffnet.Ein Kämpfer mit einer Hummerschere hat einen Zonen-RS von 2.Die Werte für Gewicht und BF sind geraten.','','Aventurisches Arsenal 69')
GO
Update Waffe set WaffeGUID=Replace(WaffeGUID,'00000000-0000-0000-0000-', '00000000-0000-0000-0001-')
GO
Update Fernkampfwaffe set FernkampfwaffeGUID=Replace(FernkampfwaffeGUID,'00000000-0000-0000-0000-', '00000000-0000-0000-0002-')
GO
Update Schild set SchildGUID=Replace(SchildGUID,'00000000-0000-0000-0000-', '00000000-0000-0000-0003-')
GO
Update Rüstung set RüstungGUID=Replace(RüstungGUID,'00000000-0000-0000-0000-', '00000000-0000-0000-0004-')
GO
--panzerarm
Update Waffe set WaffeGUID='00000000-0000-0000-0003-000000000011' Where WaffeGUID='00000000-0000-0000-0001-000000000093'
GO
--linkhand
Update Waffe set WaffeGUID='00000000-0000-0000-0003-000000000016' Where WaffeGUID='00000000-0000-0000-0001-000000000070'
GO
--Hakendolch
Update Waffe set WaffeGUID='00000000-0000-0000-0003-000000000014' Where WaffeGUID='00000000-0000-0000-0001-000000000041'
GO
--Langdolch
Update Schild set SchildGUID='00000000-0000-0000-0001-000000000067' where SchildGUID='00000000-0000-0000-0003-000000000017'
GO
--Bock
Update Waffe set WaffeGUID='00000000-0000-0000-0003-000000000013' Where WaffeGUID='00000000-0000-0000-0001-000000000013'
GO
--Drachenklaue
Update Schild set SchildGUID='00000000-0000-0000-0001-000000000146' where SchildGUID='00000000-0000-0000-0003-000000000012'
GO

INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0003-000000000018','Linkhand mit Klingenfänger',6,1,1,0,12,5,0,0,1,0,35,30,105,'H','beachte Regelung im Arsenal','(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 63')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0003-000000000019','Linkhand mit Klingenbrecher',6,1,1,0,12,5,0,0,1,0,40,30,115,'H','beachte Regelung im Arsenal','(4) GAR, ALM, HOR, MEN, ALA','Aventurisches Arsenal 63')
GO
Update Schild set Preis=105, WMPA=2, BF=0, Verbreitung='(2) GAR, ALM, HOR, MEN, ALA', Literatur='Aventurisches Arsenal 62' Where SchildGUID='00000000-0000-0000-0003-000000000018'
GO
Update Schild set Preis=115, WMPA=2, BF=0, Verbreitung='(2) GAR, ALM, HOR, MEN, ALA', Literatur='Aventurisches Arsenal 62' Where SchildGUID='00000000-0000-0000-0003-000000000019'
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000149','Drachenklaue mit Klingenbrecher',410,200,'klein','SP',-2,1,0,0,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000148','Drachenklaue mit Klingenfänger',390,200,'klein','SP',-2,1,0,0,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000147','Drachenklaue mit langer Klinge',390,200,'klein','SP',-2,1,0,0,'(2) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 98, ERRATA 5',NULL,NULL)
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0003-000000000009','Buckler',6,1,0,1,11,4,-1,-2,-1,0,40,10,40,'H','Kann mit dem waffenlosen Manöver Gerade oder zum Angriff mit dem Schild verwendet werden.','(10) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 95')
GO
INSERT INTO [Waffe] ([WaffeGUID],[Name],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[INI],[WMAT],[WMPA],[BF],[Gewicht],[Länge],[Preis],[DK],[Bemerkung],[Verbreitung],[Literatur]) VALUES ('00000000-0000-0000-0003-000000000010','Großer (Vollmetall-) Buckler',6,1,1,1,11,4,-1,-2,0,-2,60,10,60,'H','Kann mit dem waffenlosen Manöver Gerade oder zum Angriff mit dem Schild verwendet werden.','(10) ALB, GAR, HOR, ALM, ALA','Aventurisches Arsenal 95')
GO

INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0003-000000000009','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0003-000000000010','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0001-000000000146','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0001-000000000147','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0001-000000000148','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0001-000000000149','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0001-000000000150','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0003-000000000020','Raufen')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0003-000000000018','Dolche')
GO
INSERT INTO [Waffe_Talent] ([WaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0003-000000000019','Dolche')
GO
INSERT INTO [Schild] ([SchildGUID],[Name],[Preis],[Gewicht],[Größe],[Typ],[WMAT],[WMPA],[INI],[BF],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0003-000000000020','Hummerschere',0,70,'klein','SP',-2,1,0,4,'','Aventurisches Arsenal 69',NULL,'Die Werte für Gewicht und BF sind geraten.')
GO
--

--Borndorn
Update Waffe set WaffeGUID='00000000-0000-0000-0002-000000000007', Name='Borndorn' Where WaffeGUID='00000000-0000-0000-0001-000000000014'
GO
--Dolch
Update Fernkampfwaffe set FernkampfwaffeGUID='00000000-0000-0000-0001-000000000020' Where FernkampfwaffeGUID='00000000-0000-0000-0002-000000000009'
GO
--Efferdbart
Update Fernkampfwaffe set FernkampfwaffeGUID='00000000-0000-0000-0001-000000000030' Where FernkampfwaffeGUID='00000000-0000-0000-0002-000000000011'
GO
--Holzspeer
Update Fernkampfwaffe set FernkampfwaffeGUID='00000000-0000-0000-0001-000000000046' Where FernkampfwaffeGUID='00000000-0000-0000-0002-000000000018'
GO
--Jagdspieß
Update Fernkampfwaffe set FernkampfwaffeGUID='00000000-0000-0000-0001-000000000049', Gewicht=80, Verbreitung='(8) alle außer EHE, ZWE', Literatur='Aventurisches Arsenal 20' Where FernkampfwaffeGUID='00000000-0000-0000-0002-000000000020'
GO
INSERT INTO [Fernkampfwaffe] ([FernkampfwaffeGUID],[Name],[Preis],[Munitionspreis],[Munitionsgewicht],[Munitionsart],[Gewicht],[Improvisiert],[TPWürfel],[TPWürfelAnzahl],[TPBonus],[AusdauerSchaden],[TPKKSchwelle],[TPKKSchritt],[Verwundend],[RWSehrNah],[RWNah],[RWMittel],[RWWeit],[RWSehrWeit],[TPSehrNah],[TPNah],[TPMittel],[TPWeit],[TPSehrWeit],[Laden],[Verbreitung],[Literatur],[Setting],[Bemerkung]) VALUES ('00000000-0000-0000-0001-000000000050','Jagdspieß (elfische Variante)',80,NULL,NULL,NULL,75,0,6,1,4,0,12,3,0,5,10,15,20,30,0,0,-1,-2,-2,NULL,'(16) ELF','Aventurisches Arsenal 20, Errata 1, Errata 4','Aventurien',NULL)
GO
INSERT INTO [Fernkampfwaffe_Talent] ([FernkampfwaffeGUID],[Talentname]) VALUES ('00000000-0000-0000-0001-000000000050','Wurfspeere')
GO
--Speer
Update Fernkampfwaffe set FernkampfwaffeGUID='00000000-0000-0000-0001-000000000118' Where FernkampfwaffeGUID='00000000-0000-0000-0002-000000000034'
GO
--Wurfbeil
Update Waffe set WaffeGUID='00000000-0000-0000-0002-000000000039', Name='Wurfbeil' Where WaffeGUID='00000000-0000-0000-0001-000000000136'
GO
--Wurfdolch
Update Waffe set WaffeGUID='00000000-0000-0000-0002-000000000040', Name='Wurfdolch' Where WaffeGUID='00000000-0000-0000-0001-000000000137'
GO
--Wurfkeule
Update Waffe set WaffeGUID='00000000-0000-0000-0002-000000000041', Name='Wurfkeule' Where WaffeGUID='00000000-0000-0000-0001-000000000138'
GO
--Wurfmesser
Update Waffe set WaffeGUID='00000000-0000-0000-0002-000000000042', Name='Wurfmesser' Where WaffeGUID='00000000-0000-0000-0001-000000000139'
GO
--Wurfspeer
Update Waffe set WaffeGUID='00000000-0000-0000-0002-000000000045', Name='Wurfspeer' Where WaffeGUID='00000000-0000-0000-0001-000000000140'
GO

Alter Table Waffe add column Improvisiert bit not null default 0
GO
Update Waffe set Improvisiert=1 Where WaffeGUID in ('00000000-0000-0000-0001-000000000011','00000000-0000-0000-0001-000000000012','00000000-0000-0000-0001-000000000032','00000000-0000-0000-0001-000000000035','00000000-0000-0000-0001-000000000043','00000000-0000-0000-0001-000000000114','00000000-0000-0000-0001-000000000125','00000000-0000-0000-0002-000000000039','00000000-0000-0000-0002-000000000041','00000000-0000-0000-0001-000000000113','00000000-0000-0000-0001-000000000024','00000000-0000-0000-0001-000000000064','00000000-0000-0000-0001-000000000127','00000000-0000-0000-0002-000000000045','00000000-0000-0000-0001-000000000025','00000000-0000-0000-0001-000000000045','00000000-0000-0000-0001-000000000119','00000000-0000-0000-0001-000000000131','00000000-0000-0000-0001-000000000102','00000000-0000-0000-0001-000000000079','00000000-0000-0000-0001-000000000080','00000000-0000-0000-0001-000000000132','00000000-0000-0000-0002-000000000040','00000000-0000-0000-0002-000000000042')
GO