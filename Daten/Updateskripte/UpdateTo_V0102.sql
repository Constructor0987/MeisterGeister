--Daten-Update: Bleichmohn fehlt im Handelsgut
 --Handelsgut
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003645' ,N'Bleichmohn' ,NULL ,NULL ,N'Pflanzen/Kräuter' ,N'Nutzpflanze, Droge' ,N'Schmerzmittel' ,N'ZBA 252');
Go
 --Handelsgut_Setting
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003645' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
GO

 --Daten-Update: Myranor-Pflanzen im Handelsgut
 --Handelsgut
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003646',N'Alzazeerenbaum',N'Pflanzen/Kräuter',N'Heilpflanze',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003647',N'Alzazeerenbaum (Blattsud/ Stärkungselixier)',N'Pflanzen/Kräuter',N'Heilpflanze',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Bemerkung], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003648',N'Aurinde (auch Sonnenkraut)',N'Portion',N'Pflanzen/Kräuter',N'Heilpflanze',N'für Amaunir zur Berkämpfung der Kahle',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003649',N'Blaburri-Perlen',N'Pflanzen/Kräuter',N'Heilpflanze',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003650',N'Caranuss-Strauch',N'Portion',N'Pflanzen/Kräuter',N'Droge',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003651',N'Cuina-Blüte (wild)',N'Uncia bzw. Portion',N'Pflanzen/Kräuter',N'Droge',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003652',N'Cuina-Blüte (kultiviert)',N'Uncia bzw. Portion',N'Pflanzen/Kräuter',N'Droge',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003653',N'Feuerposaune',N'Dosis',N'Pflanzen/Kräuter',N'Droge',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003654',N'Mantigora',N'Wurzel',N'Pflanzen/Kräuter',N'Giftpflanze/ Nutzpflanze',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003655',N'Remembalia',N'für eine kleine Pflanze',N'Pflanzen/Kräuter',N'übernatürliche Pflanze',N'Myranor 262');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [Kategorie], [Tags], [Bemerkung], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003656',N'Roter Bambus',N'Pflanzen/Kräuter',N'Nutzpflanze',N'Probe auf Holzbearbeitung nach Meisterentscheid',N'Myranor 262,263');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003657',N'Schwesterlein',N'Pflanzen/Kräuter',N'übernatürliche Pflanze',N'Myranor 263');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003658',N'Szatate',N'pro Saft eines Stachels (oder als entsprechend präparierter Blasrohrpfeil)',N'Pflanzen/Kräuter',N'Giftpflanze',N'Myranor 263');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003659',N'Tarnblatt',N'Pflanzen/Kräuter',N'Giftpflanze',N'ZBA 268');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [Kategorie], [Tags], [Bemerkung], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003660',N'Traumpilz',N'Pflanzen/Kräuter',N'Giftpflnaze, Gefährliche Pflanze',N'vom leichten Rauschmittel bis zum tödlichen Atemgift oder stark suchterzeugenden Droge',N'Myranor 263');
GO
INSERT INTO [Handelsgut] ( [HandelsgutGUID], [Name], [ME], [Kategorie], [Tags], [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003661',N'Warakwurz',N'für ein Antidot bzw. pro Portion',N'Pflanzen/Kräuter',N'Nutzpflanze',N'Myranor 263');
GO





--Handelsgut_Setting
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003646',N'00000000-0000-0000-5e77-000000000003',N'12');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003647',N'00000000-0000-0000-5e77-000000000003',N'100');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003648',N'00000000-0000-0000-5e77-000000000003',N'12');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003649',N'00000000-0000-0000-5e77-000000000003',N'irrelevant, da nicht handelbar');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003650',N'00000000-0000-0000-5e77-000000000003',N'reif: 5, unreif mit Kalk: 3');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003651',N'00000000-0000-0000-5e77-000000000003',N'Gewürz: 1,6, Droge: 6');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003652',N'00000000-0000-0000-5e77-000000000003',N'Gewürz: 1,6, Droge: 6');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003653',N'00000000-0000-0000-5e77-000000000003',N'50');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003654',N'00000000-0000-0000-5e77-000000000003',N'12');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003655',N'00000000-0000-0000-5e77-000000000003',N'1');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003656',N'00000000-0000-0000-5e77-000000000003',N'0,4');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003657',N'00000000-0000-0000-5e77-000000000003',N'250+');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003658',N'00000000-0000-0000-5e77-000000000003',N'80');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID]) VALUES (N'00000000-0000-0000-002a-000000003659',N'00000000-0000-0000-5e77-000000000003');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003660',N'00000000-0000-0000-5e77-000000000003',N'8-100');
GO
INSERT INTO [Handelsgut_Setting] ( [HandelsgutGUID], [SettingGUID], [Preis]) VALUES (N'00000000-0000-0000-002a-000000003661',N'00000000-0000-0000-5e77-000000000003',N'0,2, 60');
GO


--Update Pflanze
 

-- Übergeordnete Gebiet-Definition
CREATE TABLE [Gebiet] (
	[GebietGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Name] nvarchar(500) NOT NULL,
	CONSTRAINT [PK_Gebiet] PRIMARY KEY ([GebietGUID])
)
GO

-- Übergeordnete Landschaft-Definition
CREATE TABLE [Landschaft] (
	[LandschaftGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(500) NOT NULL, 
	[Kundig] nvarchar(100),
	CONSTRAINT [PK_Landschaft] PRIMARY KEY ([LandschaftGUID])
)
GO	

-- Pflanzen-Datenbank	
CREATE TABLE [Pflanze] (
	[PflanzeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[HandelsgutGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Name] nvarchar(500) NOT NULL, 
	[Kategorie] nvarchar(254), 
	[Bestimmung] smallint NOT NULL, 
	[Bestimmung2] smallint, 
	[Bestimmungsausnahme] nvarchar(100), 
	[AusnahmeVon] smallint, 
	[AusnahmeBis] smallint, 
	CONSTRAINT [PK_Pflanze] PRIMARY KEY ([PflanzeGUID]),
	CONSTRAINT fk_Pflanze_Handelsgut FOREIGN KEY ([HandelsgutGUID])
		REFERENCES [Handelsgut] ([HandelsgutGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE [Pflanze_Ernte] (
	[ID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[PflanzeGUID] uniqueidentifier NOT NULL, 
	[Von] smallint NOT NULL, 
	[Bis] smallint NOT NULL, 
	[Grundmenge] nvarchar(100) NOT NULL, 
	[Pflanzenteil] nvarchar(100) NOT NULL, 
	[Haltbarkeit] nvarchar(200), 
	[HaltbarkeitEinheit] nvarchar(200), 
	[Gewicht] int, 
	[Bemerkung] nvarchar(500), 
	CONSTRAINT [PK_Pflanze_Ernte] PRIMARY KEY ([ID]),
	CONSTRAINT fk_Pflanze_Ernte_PflanzeGUID FOREIGN KEY ([PflanzeGUID])
		REFERENCES [Pflanze] ([PflanzeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE [Pflanze_Gebiet] (
	[ID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[PflanzeGUID] uniqueidentifier NOT NULL, 
	[GebietGUID] uniqueidentifier NOT NULL, 
	CONSTRAINT [PK_Pflanze_Gebiet] PRIMARY KEY ([ID]),
	CONSTRAINT fk_Pflanze_Gebiet_PflanzeGUID FOREIGN KEY ([PflanzeGUID])
		REFERENCES [Pflanze] ([PflanzeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_Pflanze_Gebiet_GebietGUID FOREIGN KEY ([GebietGUID])
		REFERENCES [Gebiet] ([GebietGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE [Pflanze_Verbreitung] (
	[ID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[PflanzeGUID] uniqueidentifier NOT NULL, 
	[LandschaftGUID] uniqueidentifier NOT NULL, 
	[Verbreitung] smallint NOT NULL, 
	CONSTRAINT [PK_Pflanze_Verbreitung] PRIMARY KEY ([ID]), 
	CONSTRAINT fk_Pflanze_Verbreitung_PflanzeGUID FOREIGN KEY ([PflanzeGUID])
		REFERENCES [Pflanze] ([PflanzeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_Pflanze_Verbreitung_LandschaftGUID FOREIGN KEY ([LandschaftGUID])
		REFERENCES [Landschaft] ([LandschaftGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

-- Gebiet
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000001',N'ganz Aventurien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000002',N'Albernia und Nostria');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000003',N'alle Wüsten, Eisgebiete und Hochgebirge Aventuriens');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000004',N'Altoum, Souram und Nikkali');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000005',N'Amhalassih, Mhanadistan, Gorien, Aranien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000006',N'Aranien, Mhanadistan, Balash, Thalusien, Szintotal, alanfanische Plantagen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000007',N'aventurische Westküste zwischen Salza und Mengbilla');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000008',N'aventurische Westküste zwischen Thorwal und Brabak, auch am Yslisee und im Gebiet um Vallusa');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000009',N'Bornland, nördliches Tobrien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000010',N'Ehrenes Schwert');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000011',N'ganz Aventurien nördlich der Linie Grangor–Elburum');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000012',N'ganz Aventurien nördlich des Yaquir');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000013',N'ganz Aventurien nördlich einer Linie Drôl—Thalusa');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000014',N'ganz Aventurien nördlich einer Linie Havena—Perricum');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000015',N'ganz Aventurien nördlich einer Linie Neetha-Khunchom');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000016',N'ganz Aventurien südlich der Gelben Sichel');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000017',N'ganz Aventurien südlich der Linie Havena–Perricum');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000018',N'ganz Aventurien südlich der Linie Havena—Perricum ');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000019',N'ganz Aventurien südlich des Blauen Sees');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000020',N'ganz Aventurien südlich einer Linie Olport-Festum');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000021',N'ganz Aventurien südlich einer Linie Thorwal-Festum');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000022',N'ganz Aventurien südlich von Festum');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000023',N'ganz Aventurien südlich von Gerasim');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000024',N'ganz Aventurien südlich von Havena, am häufigsten an den westlichen Abhängen des Regengebirges');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000025',N'ganz Aventurien südlich von Riva');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000026',N'ganz Aventurien zwischen Gerasim und Mengbilla');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000027',N'ganz Aventurien zwischen Riva und Al´Anfa');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000028',N'ganz Aventurien zwischen Riva und Mengbilla');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000029',N'ganz Mittelaventurien zwischen Trallop und Punin');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000030',N'ganz Nordaventurien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000031',N'ganz Südaventurien südlich der Linie Neetha—Thalusa');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000032',N'Garetien, Darpatien, Warunk, Beilunk');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000033',N'keine natürlichen Vorkommen bekannt');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000034',N'Khômwüste und ihre Randgebiete, auch Gorien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000035',N'Küste um die Elburische Halbinsel');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000036',N'Liebliches Feld, Almada, Aranien, Südaventurien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000037',N'Maraskan');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000038',N'Maraskan und aranische Küste');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000039',N'Maraskan, seit etwa 995 BF auch in der Nähe Khunchoms');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000040',N'Maraskankette, aranisches Hochland, Grüne Ebene');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000041',N'Mhanadistan, Thalusien, Unauer Berge, Regengebirge');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000042',N'Mittelaventurien von Garetien bis zu den Echsensümpfen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000043',N'Mittelaventurien zwischen Lowangen und Selem');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000044',N'Mittelaventurien zwischen Salamandersteine und Ongalo-Bergen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000045',N'Moghulat Oron');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000046',N'Neunaugensee, Einsiedlersee und andere, von der Sphärenkraft durchdrungene Gewässer');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000047',N'Nord- und Mittelaventurien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000048',N'Nord- und Mittelaventurien, Liebliches Feld, Aranien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000049',N'Nordaventurien bis zu einer Linie Nostria–Vallusa');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000050',N'Nordaventurien nördlich der Linie Thorwal—Festum, besonders in der Gegend um Bjaldorn');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000051',N'Nordaventurien nördlich einer Linie Nostria–Vallusa');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000052',N'nördlich der Khôm');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000053',N'Nordostaventurien von der Letta bis zur Tobimora');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000054',N'Nostria, Andergast, Albernia und Nordmarken');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000055',N'nur auf Maraskan und am Golf von Perricum');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000056',N'Orkland');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000057',N'Orkland und Finsterkamm');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000058',N'Orkland, Gjalsker Öden, Thorwal');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000059',N'Orkland, Svelltsümpfe, Totenmoor');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000060',N'Orkland, Svellttal, Andergast');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000061',N'Ostaventurien, vornehmlich Aranien und Mhandistan');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000062',N'östliche Khôm, Gorien, Mhanadistan');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000063',N'Pailos (eine der Zyklopeninseln)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000064',N'Randgebiete der Khôm und der Gorischen Wüste, Höhen des Raschtulswalls und des Khoramgebirges');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000065',N'Raschtulswall');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000066',N'Schwarz-Tobrien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000067',N'Schwarz-Tobrien, vor allem Xeraanien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000068',N'Steineichenwald, Finsterkamm, Koschberge und Eisenwald');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000069',N'Süd- und Mittelaventurien');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000070',N'Südaventurien ab der Linie Neetha–Thalusa');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000071',N'Südaventurien südlich von Mengbilla');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000072',N'Südaventurien, einzelne Vorkommen weiter nördlich');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000073',N'Südlich der Eternen, Echsensümpfe, Südmaraskan und umgebende Inseln');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000074',N'südlich des Loch Harodröl');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000075',N'südliche Randgebiete der Khôm, Shadif, Szintotal');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000076',N'südliches Maraskan und die umliegenden Inseln');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000077',N'Südwestaventurien südlich von Mengbilla, Waldinseln');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000078',N'ursprünglich Schwarztobrien (Verbreitung in einem Bereich zwischen Donnerbach und Rashdul möglich)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000079',N'ursprünglich Schwarztobrien (Verbreitung in einem Bereich zwischen Riva und Neetha möglich)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000080',N'ursprünglich Schwarztobrien (Verbreitung in ganz Aventurien möglich)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000081',N'Wälder um die Salamandersteine');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000082',N'Wälder und Regengebirge, Mysobgebiet, Waldinseln');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000083',N'Waldinseln von Ost-Altoum bis Ibonka');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000084',N'Warunkei');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000085',N'Westküste Aventuriens');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000086',N'westlich des Regengebirges ab Höhe Mengbilla, südöstlich des Regengebirges zwischen Brabak und Mirham');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000087',N'westliches Regengebirge (Dschungel)');
GO

--Myranor Gebiete
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000088',N'ganz Myranor');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000089',N'Aschenwald');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000090',N'auf Feldern und Wiesen, auf Arhtax, wild in den Randgebieten nördlich des Meeres der Schwimmenden Inseln Era´Sumu');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000091',N'Bergwälder der Wplkenkömme (Waldrand und Lichtung)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000092',N'Binnengewässer zwischen Orismani-Fällen und Valantischem Meer (nicht Grüner Orismani)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000093',N'Buschland, Savannen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000094',N'Dschungel der Tropen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000095',N'Dschungel von Tharpura und Makshapuram');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000096',N'Entlang der tharpurischen Thalassionküste (nur wild; ein abgeerntetes Gebiet muss vier Jahre lang regenerieren)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000097',N'Era´Sumu');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000098',N'feuchte Tropenzonen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000099',N'ganz Myranor an Orten magischer Kraftkonzentration, nicht zu trockenen, temperiert');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000100',N'Gemäßigte Breiten des Nordens bis zum Großen Orismani');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000101',N'gemäßigte nördliche Breiten');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000102',N'Große Steppe westliche des Orismani');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000103',N'im südlichen Imperium nur kultiviert');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000104',N'In gemäßigtem Klima');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000105',N'in Tharpura als Kulturpflanze');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000106',N'Meer der Schwimmenden Inseln, tropischer Teil des Thalassions');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000107',N'Mittlere und nördliche Breiten');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000108',N'Narkramar');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000109',N'Narkramar, Brillantsteine, Tharamans Rippen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000110',N'Narkramar, Wassernähe ');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000111',N'Nebelwald südlich des Meeres der Schwimmenden Inseln');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000112',N'noch vom Sonnenlicht erreichte Flachwasser der tropischen Meere');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000113',N'nördlich einer Linie Balan Cantara-Daranel bis zur winterlichen Eisgrenze (trockene Höhenlagen, Steppe, Wüstenrandgebiete)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000114',N'nördliche Wälder, im Osten bis an den Rand Valantias und Mayenios');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000115',N'nördliche Wüsten');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000116',N'nur als Kulturpflanze in geschützter Umgebung');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000117',N'selten wild in Nebelwäldern und höher gelegenen Dschungelgebieten');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000118',N'Süden der nördlichen Wälder bis nach Cantera und vom Großem Orismani bis zum Thalassion');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000119',N'südlich von Ochobenius bis nach Valantia');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000120',N'Südliches Imperium, auf imperialen Feldern');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000121',N'Tropen, Subtropen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000122',N'Tropen, Subtropen (Gebirge)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000123',N'Tropen, Subtropen (Savanne)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000124',N'Tropische Wälder im Süden des Meeres der Schwimmenden Inseln');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000125',N'Tundren und Gebirge des Nordens, Letztere bis nach Karonius');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000126',N'Tundren, Gebirge und am Rande des Eises im Norden');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000127',N'u. a. Mayenios, Gyldraland');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000128',N'überall in entsprechend feuchten Gebieten');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000129',N'Ufergebiete des Meeres der Schwimmenden Inseln');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000130',N'Wald von Amaunath');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000131',N'Wälder des nördlichen Myranor bis zur großen Steppe, Karonius, Centralis und Cranarenius');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000132',N'Wälder nördlich von Centralis und der großen Steppe');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000133',N'Wüsten nördlich der Dschungel');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000134',N'Xarxaron');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000135',N'Zivilisation (bes. Imperium)');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000136',N'zwischen den Polarregionen');
GO
INSERT INTO [Gebiet] ([GebietGUID],[Name]) VALUES (N'00000000-0000-0000-00f9-000000000137',N'zwischen Frostweste, den Bergen von Skiresis und Nitharus-Bergen');
GO


--Landschaft
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000001',N'überall');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000002',N'Hausgärten');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000003',N'Eisgebiete',N'Eiskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000004',N'Eiswüste',N'Eiskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000005',N'Feuchte Grasländer');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000006',N'Feuchte Höhlen',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000007',N'Feuchte Waldgebiete',N'Waldkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000008',N'Feuchte Wiesen');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000009',N'Fließende Gewässer');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000010',N'Flussauen');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000011',N'Flussläufe');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000012',N'Flussufer');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000013',N'Gebirge',N'Gebirgskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000014',N'Gebirgshänge',N'Gebirgskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000015',N'Gebirgshöhen',N'Gebirgskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000016',N'Gebirgspflanze',N'Gebirgskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000017',N'Gezeitenzone');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000018',N'Grasland');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000019',N'Hochland');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000020',N'Höhlen',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000021',N'Höhlen ab 100 Schritt Tiefe',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000022',N'Höhlen ab 1000 Schritt tiefe',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000023',N'Höhlen ab 300 Schritt Tiefe',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000024',N'Höhlen ab 50 Schritt Tiefe',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000025',N'Höhlen ab 75 Schritt Tiefe',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000026',N'Höhleneingänge und Ruinen',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000027',N'in der Nähe astraler Quellen');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000028',N'Kulturland');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000029',N'Küste');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000030',N'Brackwassersümpfe',N'Sumpfkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000031',N'Küstensümpfe',N'Sumpfkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000032',N'lichte Laubwälder',N'Waldkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000033',N'Maraskanische Wälder',N'Waldkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000034',N'Maraskankette');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000035',N'Moor');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000036',N'nur auf Lichtungen');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000037',N'Randgebiete der Wüsten',N'Wüstenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000038',N'Regenwald',N'Dschungelkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000039',N'Ruinenstadt Palakar');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000040',N'schattige Gebiete im Hochland');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000041',N'Seeufer');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000042',N'Stätten der Namenlosen Macht');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000043',N'Steppen',N'Steppenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000044',N'Strand');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000045',N'südliche Sümpfe',N'Sumpfkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000046',N'Sumpf',N'Sumpfkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000047',N'Sumpfgebiete',N'Sumpfkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000048',N'sumpfige Uferstreifen',N'Sumpfkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000049',N'Teiche');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000050',N'Trockene Höhlen',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000051',N'Trockene Höhlen und Gänge',N'Höhlenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000052',N'Wald',N'Waldkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000053',N'Waldgebiete',N'Waldkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000054',N'Waldrand',N'Waldkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000055',N'Wiesen');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000056',N'Wüste',N'Wüstenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000057',N'Wüstenähnliches Hochland',N'Wüstenkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000058',N'Wüstenrandgebiete',N'Wüstenkundig');
GO
--Landschaft Myranor
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000059',N'offenes Meer',N'Meereskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000060',N'oberirdische Hitzequellen');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000061',N'Meer',N'Meereskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000062',N'Tundra');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000063',N'Dschungel',N'Dschungelkundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000064',N'Orismanis');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000065',N'Louranath');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000066',N'Gebirgslagen bis zur Schneegrenze',N'Gebirgskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000067',N'Buschlan');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000068',N'vor Regen geschützte Hitzequellen');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000069',N'Hochgebirge',N'Gebirgskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name],[Kundig]) VALUES (N'00000000-0000-0000-00fe-000000000070',N'baumlose Teile von Hochgebirgen',N'Gebirgskundig');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000071',N'sonstige Binnengewässer');
GO
INSERT INTO [Landschaft] ([LandschaftGUID],[Name]) VALUES (N'00000000-0000-0000-00fe-000000000072',N'Domänen');
GO

--Pflanze
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'00000000-0000-0000-002a-000000000946',N'Alraune',N'Pflanzen/Kräuter',9);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000002',N'00000000-0000-0000-002a-000000000947',N'Alveranie',N'Pflanzen/Kräuter',-5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-002a-000000000948',N'Arganstrauch',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000004',N'00000000-0000-0000-002a-000000000949',N'Atan-Kiefer',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-002a-000000000950',N'Atmon',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'00000000-0000-0000-002a-000000000951',N'Axorda-Baum',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000007',N'00000000-0000-0000-002a-000000000952',N'Basilamine',N'Pflanzen/Kräuter',15,5,N'Blütezeit im Ingerimm',11,11);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000008',N'00000000-0000-0000-002a-000000000953',N'Belmart',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000009',N'00000000-0000-0000-002a-000000000954',N'Blutblatt',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000010',N'00000000-0000-0000-002a-000000000955',N'Boronie',N'Pflanzen/Kräuter',-2,N'im Süden jederzeit, in Mittelaventurien zwischen Tsa und Boron',8);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-002a-000000000956',N'Boronschlinge',N'Pflanzen/Kräuter',15);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-002a-000000000958',N'Bunter Mohn',N'Pflanzen/Kräuter',-5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-002a-000000000959',N'Carlog',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'00000000-0000-0000-002a-000000000960',N'Cheria-Kaktus',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-002a-000000000961',N'Chonchinis',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000017',N'00000000-0000-0000-002a-000000000962',N'Dergolasch',N'Pflanzen/Kräuter',8,4,N'bei Kulturkunde (Zwerge)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'00000000-0000-0000-002a-000000000963',N'Disdychonda',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-002a-000000000964',N'Donf',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-002a-000000000965',N'Dornrose',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'00000000-0000-0000-002a-000000000966',N'Efeuer',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-002a-000000000967',N'Egelschreck',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-002a-000000000968',N'Eitriger Krötenschemel',N'Pflanzen/Kräuter',2,N'Efferd bis Boron, in südlicheren Gebieten auch ganzjährig');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000025',N'00000000-0000-0000-002a-000000000969',N'Felsenmilch',N'Pflanzen/Kräuter',4);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000026',N'00000000-0000-0000-002a-000000000970',N'Feuermoos und Efferdmoos',N'Pflanzen/Kräuter',15);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'00000000-0000-0000-002a-000000000971',N'Feuerschlick',N'Pflanzen/Kräuter',6,-5,N'wenn leuchtend',2,3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'00000000-0000-0000-002a-000000000972',N'Finage',N'Pflanzen/Kräuter',5,N'NOK');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-002a-000000000973',N'Grauer Lotus',N'Pflanzen/Kräuter',8);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'00000000-0000-0000-002a-000000000974',N'Grauer Mohn (Samenkapsel)',N'Pflanzen/Kräuter',1,N'NOK?');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'00000000-0000-0000-002a-000000000976',N'grüne Schleimschlange',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-002a-000000000977',N'Gulmond',N'Pflanzen/Kräuter',6,N'NOK');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'00000000-0000-0000-002a-000000000978',N'grüner Schleimpilz',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'00000000-0000-0000-002a-000000000979',N'Hiradwurz',N'Pflanzen/Kräuter',8,5,N'+8 (Trockenzeit), +5 (Regenzeit)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'00000000-0000-0000-002a-000000000980',N'Hollbeere',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'00000000-0000-0000-002a-000000000981',N'Höllenkraut',N'Pflanzen/Kräuter',8);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000038',N'00000000-0000-0000-002a-000000000982',N'Horusche',N'Pflanzen/Kräuter',7);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000039',N'00000000-0000-0000-002a-000000000983',N'Iribaarslilie',N'Pflanzen/Kräuter',12);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-002a-000000000984',N'Jagdgras',N'Pflanzen/Kräuter',15);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-002a-000000000985',N'Joruga',N'Pflanzen/Kräuter',7);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000042',N'00000000-0000-0000-002a-000000000986',N'Kairan',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'00000000-0000-0000-002a-000000000987',N'Kajubo',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'00000000-0000-0000-002a-000000000988',N'Khôm- oder Mhanadiknolle',N'Pflanzen/Kräuter',5,12,N'+5 (mit Blättern), +12 (ohne Blätter)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'00000000-0000-0000-002a-000000000989',N'Klippenzahn',N'Pflanzen/Kräuter',8);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'00000000-0000-0000-002a-000000000990',N'Kukuka',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-002a-000000000991',N'Libellengras',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000049',N'00000000-0000-0000-002a-000000000992',N'Lichtnebler',N'Pflanzen/Kräuter',10,4,N'+10 (+4 nach Freisetzung der Sporen)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000050',N'00000000-0000-0000-002a-000000000992',N'Lichtnebler',N'Pflanzen/Kräuter',10,4,N'+10 (+4 nach Freisetzung der Sporen)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'00000000-0000-0000-002a-000000000993',N'Lotos',N'Pflanzen/Kräuter',5,9,N'+5 (Lotos allgemein) bzw. +9 (einzelne Arten)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'00000000-0000-0000-002a-000000000994',N'Lulanie',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'00000000-0000-0000-002a-000000000995',N'Madablüte',N'Pflanzen/Kräuter',5,15,N'+5 (+15, wenn nicht blühend)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000054',N'00000000-0000-0000-002a-000000000996',N'Malmomis',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'00000000-0000-0000-002a-000000000997',N'Menchal - Kaktus',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-002a-000000000998',N'Merach-Strauch',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-002a-000000000999',N'Messergras',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-002a-000000001000',N'Mibelrohr',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-002a-000000001000',N'Mibelrohr',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-002a-000000001001',N'Mirbelstein',N'Pflanzen/Kräuter',8);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-002a-000000001002',N'Mirhamer Seidenliane',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000062',N'00000000-0000-0000-002a-000000001003',N'Mohn',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000063',N'00000000-0000-0000-002a-000000001004',N'Morgendornstrauch',N'Pflanzen/Kräuter',13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-002a-000000001005',N'Naftanstaude',N'Pflanzen/Kräuter',1);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-002a-000000001006',N'Neckerkraut',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-002a-000000001007',N'Olginwurz',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'00000000-0000-0000-002a-000000001008',N'Orazal',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-002a-000000001009',N'Orkland',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-002a-000000001010',N'Pestsporenpilz',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'00000000-0000-0000-002a-000000001011',N'Phosphorpilz',N'Pflanzen/Kräuter',-3,10,N'-3 wenn er leuchtet, +10 für nicht leuchtendes Geflecht',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'00000000-0000-0000-002a-000000001012',N'Purpurner Lotos',N'Pflanzen/Kräuter',9,N'(einzelne Arten)');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-002a-000000001013',N'Purpur Mohn',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000075',N'00000000-0000-0000-002a-000000001014',N'Quasselwurz',N'Pflanzen/Kräuter',12);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-002a-000000001015',N'Quinya',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-002a-000000001016',N'Rahjalieb',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-002a-000000001017',N'Rattenpilz',N'Pflanzen/Kräuter',7);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000079',N'00000000-0000-0000-002a-000000001018',N'Rauschgurke',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'00000000-0000-0000-002a-000000001019',N'Rote Pfeilblüte',N'Pflanzen/Kräuter',7);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'00000000-0000-0000-002a-000000001020',N'Roter Drachenschlund',N'Pflanzen/Kräuter',10,3,N'zur Blütezeit',11,12);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000082',N'00000000-0000-0000-002a-000000001021',N'Schwarmschwamm',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000083',N'00000000-0000-0000-002a-000000001022',N'Schwarzer Lotos',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'00000000-0000-0000-002a-000000001023',N'Schwarzer Mohn',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-002a-000000001024',N'Seelenhauch',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-002a-000000001025',N'Shurinstrauch',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'00000000-0000-0000-002a-000000001026',N'Steinrinde',N'Pflanzen/Kräuter',12);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-002a-000000001027',N'Talaschin',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'00000000-0000-0000-002a-000000001028',N'Tarnblatt',N'Pflanzen/Kräuter',8);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-002a-000000001029',N'Tarnele',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'00000000-0000-0000-002a-000000001030',N'Thonnys',N'Pflanzen/Kräuter',12,5,N'+12, während der Blüte +5');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-002a-000000001031',N'Tiger Mohn',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'00000000-0000-0000-002a-000000001032',N'Traschbart',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000094',N'00000000-0000-0000-002a-000000001033',N'Trichterwurzel',N'Pflanzen/Kräuter',11);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000095',N'00000000-0000-0000-002a-000000001034',N'Tuur-Amash-Kelch',N'Pflanzen/Kräuter',1);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000096',N'00000000-0000-0000-002a-000000001035',N'Ulmenwürger',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-002a-000000001036',N'Vierblättrige Einbeere',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-002a-000000001037',N'Vragieswurzel',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000100',N'00000000-0000-0000-002a-000000001038',N'Waldwebe',N'Pflanzen/Kräuter',9);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'00000000-0000-0000-002a-000000001039',N'Wandermoos',N'Pflanzen/Kräuter',14,5,N'+14 (+5 im mobilen Zustand)',1,13);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'00000000-0000-0000-002a-000000001040',N'Wasserrausch (Blüten)',N'Pflanzen/Kräuter',1);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'00000000-0000-0000-002a-000000001041',N'Wasserrausch (Frucht)',N'Pflanzen/Kräuter',1);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-002a-000000001042',N'Weißer Lotos',N'Pflanzen/Kräuter',10,11,1);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'00000000-0000-0000-002a-000000001043',N'Weißgelber Lotos',N'Pflanzen/Kräuter',5,10,N'+5 (Lotos allgemein) bzw. +10, jedoch nicht von Weißem Lotos zu unterscheiden.',11,1);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'00000000-0000-0000-002a-000000001044',N'Winselgras',N'Pflanzen/Kräuter',12,-2,N'+12 (tagsüber), -2 (nachts)');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'00000000-0000-0000-002a-000000001045',N'Wirselkraut',N'Pflanzen/Kräuter',5,N'NOK?');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'00000000-0000-0000-002a-000000001046',N'Würgedattel',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-002a-000000001047',N'Zithabar',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'00000000-0000-0000-002a-000000001048',N'Zunderschwamm',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-002a-000000001049',N'Zwölfblatt',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'00000000-0000-0000-002a-000000003355',N'Yaganstrauch',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-002a-000000003356',N'Nothilf',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-002a-000000003357',N'Satuariensbusch',N'Pflanzen/Kräuter',-2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-002a-000000003358',N'Tigermohn',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-002a-000000003359',N'Schlangenzünglein',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'00000000-0000-0000-002a-000000003481',N'Bleichmohn',N'Pflanzen/Kräuter',5);
GO

--Pflanzen Myranor
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000119',N'00000000-0000-0000-002a-000000003482',N'Aschenbusch',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000120',N'00000000-0000-0000-002a-000000003483',N'Atermatea Funginus/Baramuns Liebling',N'Pflanzen/Kräuter',4,N'(nur Ende Raia und Anfang Chrysir, sonst unmöglich)');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-002a-000000003484',N'Batuunuur/Blutgewürz',N'Pflanzen/Kräuter',4);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-002a-000000003485',N'Blutpilz',N'Pflanzen/Kräuter',3,N'Fruchtkörper');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-002a-000000003486',N'Brajankelch (Nektar oder Fruchtsaft)',N'Pflanzen/Kräuter',0,N'während Blüte im Brajan');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000128',N'00000000-0000-0000-002a-000000003487',N'Brajankelch  (Frucht)',N'Pflanzen/Kräuter',0,N'während Blüte im Brajan');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000129',N'00000000-0000-0000-002a-000000003488',N'Brajanlieb/Kompasskraut ',N'Pflanzen/Kräuter',10);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000130',N'00000000-0000-0000-002a-000000003489',N'Chrysirhaube',N'Pflanzen/Kräuter',7);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000131',N'00000000-0000-0000-002a-000000003490',N'Echobaum (Leuchtstoff)',N'Pflanzen/Kräuter',4);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000132',N'00000000-0000-0000-002a-000000003491',N'Echobaum (Pulver)',N'Pflanzen/Kräuter',4);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000134',N'00000000-0000-0000-002a-000000003492',N'Goldhaut ',N'Pflanzen/Kräuter',2,N'während Blüte (spätes Zatura-Oktal bis Mitte Shinxir)');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000135',N'00000000-0000-0000-002a-000000003493',N'Incendium Herba (Samen)',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000136',N'00000000-0000-0000-002a-000000003494',N'Incendium Herba (Pulver)',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000138',N'00000000-0000-0000-002a-000000003495',N'Laufkraut ',N'Pflanzen/Kräuter',2,N'während Blütezeit im Shinxir');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000139',N'00000000-0000-0000-002a-000000003496',N'Lebensmoos ',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'00000000-0000-0000-002a-000000003497',N'Loruoor/Lebenskoralle ',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000142',N'00000000-0000-0000-002a-000000003498',N'Nachtaugen ',N'Pflanzen/Kräuter',0,N'während Blüte nachts');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'00000000-0000-0000-002a-000000003499',N'Sandklaue',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000145',N'00000000-0000-0000-002a-000000003500',N'Satyarenbaum ',N'Pflanzen/Kräuter',3,N'während Blüte und Ernte');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000146',N'00000000-0000-0000-002a-000000003501',N'Schneemoos ',N'Pflanzen/Kräuter',6);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-002a-000000003502',N'Tanzbusch',N'Pflanzen/Kräuter',6,N'ausserhalb der Ernte und Blüte');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000150',N'00000000-0000-0000-002a-000000003503',N'Tyrannenbaum',N'Pflanzen/Kräuter',2,N'während Blüte');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000152',N'00000000-0000-0000-002a-000000003504',N'Waldkönig',N'Pflanzen/Kräuter',2,N'während Blüte');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000154',N'00000000-0000-0000-002a-000000003505',N'Witwenmacher',N'Pflanzen/Kräuter',2,N'während Blüte');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000156',N'00000000-0000-0000-002a-000000003506',N'Wolfsblatt',N'Pflanzen/Kräuter',4,N'während der Blüte im Brajan');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000157',N'00000000-0000-0000-002a-000000003507',N'Atholisbaum',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000158',N'00000000-0000-0000-002a-000000003508',N'Auijaja-Baum',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000159',N'00000000-0000-0000-002a-000000003509',N'Azulie /Neristu-Rose',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000160',N'00000000-0000-0000-002a-000000003510',N'Belyabels Schleier',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000161',N'00000000-0000-0000-002a-000000003511',N'Boinurre',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000162',N'00000000-0000-0000-002a-000000003512',N'Cassava',N'Pflanzen/Kräuter',-5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000163',N'00000000-0000-0000-002a-000000003513',N'Claqua-Rose',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000164',N'00000000-0000-0000-002a-000000003514',N'Geistblüten',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000165',N'00000000-0000-0000-002a-000000003515',N'Ghorlenklaue ',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000166',N'00000000-0000-0000-002a-000000003516',N'Gyldaraswacht ',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000167',N'00000000-0000-0000-002a-000000003517',N'Himmelszeder',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000168',N'00000000-0000-0000-002a-000000003518',N'Isnea',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000169',N'00000000-0000-0000-002a-000000003519',N'Jungfernhüter',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000170',N'00000000-0000-0000-002a-000000003520',N'Maulbaum',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000171',N'00000000-0000-0000-002a-000000003521',N'Neretonie',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000172',N'00000000-0000-0000-002a-000000003522',N'Pardiriske',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000173',N'00000000-0000-0000-002a-000000003523',N'Pfeilsamenbaum ',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000174',N'00000000-0000-0000-002a-000000003524',N'Phrenophoren-Katkus ',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000175',N'00000000-0000-0000-002a-000000003525',N'Ramara-Diestel ',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000176',N'00000000-0000-0000-002a-000000003526',N'Spährentod',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000177',N'00000000-0000-0000-002a-000000003527',N'Sternapfelbaum',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000178',N'00000000-0000-0000-002a-000000003528',N'Tarnaille (-n) /Taschenblüte',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000179',N'00000000-0000-0000-002a-000000003529',N'Therkalbaum /Krüppelzeder',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000180',N'00000000-0000-0000-002a-000000003530',N'Tilians-Gruß',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000181',N'00000000-0000-0000-002a-000000003531',N'Wächter-Efeu',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000182',N'00000000-0000-0000-002a-000000003532',N'Wächterhecke',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000183',N'00000000-0000-0000-002a-000000003533',N'Alzazeerenbaum',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000184',N'00000000-0000-0000-002a-000000003534',N'Alzazeerenbaum (Blattsud/ Stärkungselixier)',N'Pflanzen/Kräuter',0);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000185',N'00000000-0000-0000-002a-000000003535',N'Aurinde (auch Sonnenkraut)',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000186',N'00000000-0000-0000-002a-000000003536',N'Blaburri-Perlen',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000187',N'00000000-0000-0000-002a-000000003537',N'Caranuss-Strauch',N'Pflanzen/Kräuter',2);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000188',N'00000000-0000-0000-002a-000000003538',N'Cuina-Blüte (wild)',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000189',N'00000000-0000-0000-002a-000000003539',N'Cuina-Blüte (kultiviert)',N'Pflanzen/Kräuter',-3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000190',N'00000000-0000-0000-002a-000000003540',N'Feuerposaune',N'Pflanzen/Kräuter',4);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000191',N'00000000-0000-0000-002a-000000003541',N'Mantigora',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000192',N'00000000-0000-0000-002a-000000003542',N'Remembalia',N'Pflanzen/Kräuter',3);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000194',N'00000000-0000-0000-002a-000000003543',N'Roter Bambus',N'Pflanzen/Kräuter',2,N'während der Blüte');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000195',N'00000000-0000-0000-002a-000000003544',N'Schwesterlein',N'Pflanzen/Kräuter',8);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000196',N'00000000-0000-0000-002a-000000003545',N'Szatate',N'Pflanzen/Kräuter',4);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000197',N'00000000-0000-0000-002a-000000003546',N'Tarnblatt',N'Pflanzen/Kräuter',5);
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung],[Bestimmungsausnahme]) VALUES (N'00000000-0000-0000-00ff-000000000199',N'00000000-0000-0000-002a-000000003547',N'Traumpilz',N'Pflanzen/Kräuter',3,N'zum Erkennen eines entsprechenden Pilzwaldes');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[HandelsgutGUID],[Name],[Kategorie],[Bestimmung]) VALUES (N'00000000-0000-0000-00ff-000000000200',N'00000000-0000-0000-002a-000000003548',N'Warakwurz',N'Pflanzen/Kräuter',6);
GO



--Pflanze_Ernte
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000001',1,13,N'1',N'Pflanze',N'W3+2',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000002',1,12,N'12',N'einzelne Blätter, jeweils in der Farbe des Monats',N'12',N'Stunden (einzelne Blätter)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000003',1,13,N'1',N'Wurzel',N'2W6 + 18',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000004',1,13,N'W20 Stein Rinde, bei kompletten Abschälen verdreifacht sich der Wert',N'',N'W6+12',N'Tage',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000005',10,10,N'W6',N'Büschel der ganzen Pflanze',N'W6+6 ',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000006',1,13,N'1',N'Baum',N'W6 +2',N'Wochen',2800);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000007',1,13,N'W20+10',N'Schoten');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000008',10,5,N'2W20',N'Blätter',N'2W6+18',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000009',1,13,N'W20+2',N'Zweige pro 10 AsP der Quelle',N'nahezu unbegrenzt in der Nähe einer magischen Quelle (jede Woche W20: fällt eine 20, geht das Blutblatt ein), sonst jedoch W3 SR');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000010',5,13,N'5',N'Blüten, möglichst kurz vor dem Verblühen',N'5',N'Stunden nach dem Pflücken ist der Blütenduft verflogen.',N'Im Herrschaftsbereich des Al´Anfaner Boron-Kultes hat die Kirche ein Monopol auf den Besitz der Pflanze, Zuwiderhandlungen werden strengstens bestraft. Unverarbeitete Blüten werden nicht verkauft, sind aber womöglich – ähnlich wie bei der Alveranie für jeden außer dem Pflücker wertlos.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000011',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000011',1,13,N'vier Farnblätter und zwei je 3 W6 Schritt lange Ranken (je nach Alter der Pflanze)',N'ganze Pflanze',N'bei trockener Lagerung 1 W6 Monate für die Rankenseile');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000013',4,4,N'1',N'geschlossene Samenkapsel',N'W6+9',N'Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000014',10,3,N'W6',N'Blüten mit je einem Stempel',N'2W6+18',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000015',1,13,N'1W6 halbe Stein Kaktusfleisch und pro Stein 3W6+8 Stacheln',N'ganze Pflanze',N'Fruchtfleisch W6 Tage, Stacheln bzw. deren Gift 3W6 Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000016',8,5,N'W20',N'Blätter',N'W6+22',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000017',2,11,N'1W6',N'Pilzehüte',N'2W6',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000018',1,13,N'2 Blätter',N'ganze Pflanze',N'W3+7',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000019',1,13,N'1',N'Stängel',N'W6+6',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000020',1,13,N'1',N'Strauch mit 1W6 Blüten',N'W6+7 Tage (Blüte) bzw. Wochen (Dornen)',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000021',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000022',2,6,N'2W20 Blätter',N'ganze Pflanze',N'W3+2',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000023',3,5,N'2W6 Pilzhäute pro Fundort',N'ganze Pflanze',N'W6+1',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000023',1,13,N'2W6 Pilzhäute pro Fundort',N'ganze Pflanze',N'W6+1',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000025',1,13,N'1 Schank',N'ganze Pflanze',N'2W6',N'Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000026',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000027',1,13,N'W6 Stein',N'der Algen',N'22',N'Stunden',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000028',1,13,N'ein Baum mit W20 Trieben',N'ganze Pflanze',N'W3+3',N'Monate, versetzt mit Orazal doppelt so lang');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000029',9,5,N'W6+1',N'Blüten',N'W6',N'Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000030',1,13,N'eine (geschloss.) Samenkapsel / eine Blüte',N'Kapsel/Blüte',N'W6+18 Monate (Samenkapsel trocken aufbewahrt) / W6+12 Wochen (getrocknete Blätter licht- und luftdicht aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000030',1,13,N'eine (geschloss.) Samenkapsel / eine Blüte',N'Kapsel/Blüte',N'W6+18 Monate (Samenkapsel trocken aufbewahrt) / W6+12 Wochen (getrocknete Blätter licht- und luftdicht aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000032',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000033',1,13,N'2W6',N'Blätter',N'W6+22',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000034',1,13,N'1W20 Unzen',N'ganze Pflanze',N'1W6',N'Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000035',1,13,N'1',N'Wurzel',N'W3+6',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000036',2,3,N'2W6 Sträucher mit jeweils 2W6+5 Beeren und 2W6+3 Blättern der untersten Zweige',N'ganze Pflanze',N'W6',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000037',1,13,N'1W20 halbe Stein',N'der Ranken',N'W6+6',N'Monate (abgeschlagene Ranken)',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000038',1,13,N'W6 erntereife Schoten pro Pflanze, W3 Kerne pro Schote',N'ganze Pflanze',N'in der Schote 2W6+2 Tage, einzeln W6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000039',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000040',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000041',12,5,N'1',N'Wurzel',N'W3+2',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000042',1,13,N'1',N'Halm',N'W3+3',N'Wochen im eigenen Wasser');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000043',9,4,N'2W6',N'Knospen, von denen Haipu immer höchstens die Hälfte abpflücken, damit der Strauch geschont wird. ',N'W6+10',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000044',1,13,N'1',N'Wurzel mit bis zu W6 Maß klarem Wasser',N'Wasser in der ungeöffneten Knolle etwa 1 Monat');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000045',10,2,N'2W6',N'Stängel',N'W3+2',N'Tage, maximal bis zum nächsten Vollmond');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000046',1,13,N'1W3 x 20',N'Blätter',N'1W6+1',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000047',2,3,N'1',N'Frucht',N'1',N'Woche');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000047',11,12,N'1',N'Frucht',N'1',N'Woche');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000049',3,6,N'1 Skrupel',N'Sporen',N'1W6',N'Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000050',10,1,N'1 Skrupel',N'Sporen',N'1W6',N'Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000051',11,1,N'2W6+1',N'Blüten pro Fundort',N'W3+6 Monate (getrocknete Blätter, licht- und luftdicht aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000052',12,2,N'1',N'Blüte',N'W3+5',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000053',1,13,N'1',N'Blüte',N'im vollen Mondlicht unbegrenzt, vergeht bei Monduntergang');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000054',2,2,N'1',N'Blüte',N'W3',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000055',1,13,N'ein Kaktus mit W6 halben Maß Menchalsaft; bei 1 auf W20: mit W6 Blüten',N'ganze Pflanze',N'W3+5 Wochen (entwurzelter Kaktus), W6+8 Wochen (Menchalsaft)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000056',3,4,N'2W20 reife Früchte',N'pro Strauch',N'W6+7',N'Tage',N'Giftstufe 1');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000057',1,13,N'1',N'ganze Pflanze',N'Abgeschnittenes Gras hält sich – einigermaßen trocken gelagert – wie Stroh mehrere Jahre lang.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000058',11,3,N'1',N'ganze Pflanze',N'2W6 Kolben pro Fundort');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000059',8,6,N'1',N'ganze Pflanze',N'2W6 Kolben pro Fundort');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000060',10,5,N'1',N'Wurzelknolle',N'ausgegraben W3+2',N'Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000061',8,5,N'1',N'Ranke (mit 2 bis 3 Knoten)',N'sowohl als Seil als auch als unverarbeitete Liane bei sachgemäßer Lagerung praktisch unbegrenzt');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000062',2,2,N'W6',N'geschlossene Samenkapseln',N'2W6+18',N'Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000063',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000064',1,13,N'1',N'Staude (ganze Pflanze)',N'Die abgeerntete, komplette Pflanze hält sich 1W+2 Tage lang, der ausgepresste Saft 1W+2 Wochen (in einem luftdicht geschlossenen Metall-, Glas- oder Keramikgefäß).');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000064',11,2,N'1',N'Staude (ganze Pflanze)',N'Die abgeerntete, komplette Pflanze hält sich 1W+2 Tage lang, der ausgepresste Saft 1W+2 Wochen (in einem luftdicht geschlossenen Metall-, Glas- oder Keramikgefäß).');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000066',1,13,N'1W20+ 5',N'Blätter',N'W3+3',N'Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000067',1,13,N'W3',N'Moosballen',N'2W6+12',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000068',1,13,N'W6',N'verholzte Stängel',N'mehrere Jahre');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000069',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000070',3,5,N'1',N'Pilzhaut',N'W6',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000070',10,12,N'1',N'Pilzhaut',N'W6',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000072',1,13,N'W6',N'Stein der leuchtende Geflechtstücke (für die direkte Anwendung, sonst trockenes Geflecht)',N'W6',N'SR (Leuchtkraft)',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000073',11,1,N'W6+1',N'Blüten pro Fundort',N'W6+2',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000074',12,12,N'1',N'(geschlossene) Samenkapsel',N'13',N'Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000075',9,4,N'1',N'Wurzel',N'W3+3',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000076',11,4,N'W3+2',N'Beeren',N'1W6+12',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000077',9,7,N'2W6',N'Blätter',N'3',N'Tage',N'wirkt nicht auf Achaz');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000078',2,11,N'1',N'Pilz',N'W6+28',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000079',8,5,N'3W6',N'reife Rauschgurken pro Baum',N'W3+1',N'Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000080',10,12,N'W6',N'Blüten',N'W6',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000081',1,13,N'W6',N'Blätter',N'W3+6',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000082',1,13,N'1 Schwamm, W2 Samenkörper',N'ganze Pflanze',N'W3 Tage (Samenkörper W6 Monate)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000083',11,1,N'W6',N'Blüten pro Fundort',N'W6+2',N'Tage (oder bis zum nächsten Windhauch)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000084',3,5,N'2 Blätter, 1 Samenkapsel',N'ganze Pflanze',N'2W6+9',N'Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000085',11,1,N'1',N'Blüte',N'in Wasser aufbewahrt 1W3 +3',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000086',11,2,N'W20',N'Knollen (Fruchtkörper)',N'W6+2 Tage (Knolle), W3+1 Tage (Blätter)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000087',1,13,N'W6',N'Stein',N'W6',N'Tage',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000088',1,13,N'W6',N'Flechten',N'1',N'Stunde (Wirkdauer)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000089',1,13,N'1',N'ganze Pflanze',N'1W6',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000090',11,4,N'1',N'ganze Pflanze',N'W6+18',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000091',10,4,N'W6+2',N'Blätter',N'2W6+36',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000092',4,4,N'1',N'geschlossene Samenkapsel',N'W6+9',N'Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000093',1,13,N'W6',N'Flechten',N'bei feuchter Lagerung 1W6',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000094',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000095',1,13,N'W6+3',N'Kelche, für eine Tuur-Amash-Beere müssen 13 TaP* übrig bleiben.',N'W6 Tage / W6 Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000096',3,4,N'W20',N'Blüten',N'3W6+22',N'Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000097',9,6,N'1',N'Strauch mit 1W6 Beeren',N'W3+2',N'Tage',N'Werden an einem Tag 2 Beeren eingenommen, besteht bei 1—2 auf W20 die Möglichkeit einer Sucht (Krankheit Stufe 10), die mit jeder weiteren Beere um 5 % steigt. Ein Süchtiger, der nicht einmal in der Woche Vierblatt zu sich nehmen kann, verliert 1W6 LeP. Vierblatt hat für ihn keine heilsame Wirkung mehr.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000098',3,4,N'1',N'Wurzel (außerhalb von Plantagen)',N'W+7',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000098',3,5,N'1',N'Wurzel (außerhalb von Plantagen)',N'W+7',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000100',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000101',1,13,N'1',N'Moosball',N'W20',N'Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000102',12,4,N'2W20 Blüten, 1 Frucht (Um eine Frucht zu finden, muss eine Probe mit 12 TaP* gelingen.)',N'ganze Pflanze',N'Blüten 3W+22 Stunden, Früchte unverarbeitet W6+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000103',12,4,N'2W20 Blüten, 1 Frucht (Um eine Frucht zu finden, muss eine Probe mit 12 TaP* gelingen.)',N'ganze Pflanze',N'Blüten 3W+22 Stunden, Früchte unverarbeitet W6+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000104',11,1,N'W6+1',N'Blüten pro Fundort',N'W6+2',N'Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000105',11,1,N'W3',N'Blüten',N'W3+6',N'Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000106',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000107',9,5,N'W6+2',N'Blätter',N'2W6+18',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000108',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000109',10,1,N'3W20',N'Blätter',N'1',N'Tag');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000110',1,13,N'W6',N'Pilze',N'W6+2',N'Monate',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000111',6,1,N'12',N'Stängel pro Pflanze',N'W6+9',N'Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000112',5,5,N'W6',N'Nüsse',N'W6+7',N'Monate (Nuss oder Öl)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000113',1,1,N'W20+2 Blüten, 2W20 +10 Blätter',N'ganze Pflanze',N'W3+6',N'Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000113',10,10,N'W20+2 Blüten, 2W20 +10 Blätter',N'ganze Pflanze',N'W3+6',N'Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000115',9,4,N'2W20 Blätter, W20 Blüten, W20 Früchte, W3 Flux Saft',N'ganze Pflanze',N'Saft W3+6 Tage (in einem geschlossenen, abgedunkelten Gefäß)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000116',4,4,N'1',N'geschlossene Samenkapsel',N'W6+9 Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000117',1,13,N'1',N'Saft einer Pflanze',N'W3+6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000118',2,2,N'W6',N'geschlossene Samenkapseln',N'2W6+18 Monate (trocken aufbewahrt)');
GO


--Pflanze_Ernte Myranor
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000119',1,8,N'k.A.',N'',N'Das Gift hält drei Oktale');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000120',6,7,N'k.A.',N'',N'trocken ein gutes halbes Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000121',1,8,N'k.A.',N'',N'mindestens leicht befeuchtet zwei Oktale');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000121',1,8,N'k.A.',N'',N'mindestens leicht befeuchtet zwei Oktale');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000121',2,7,N'k.A.',N'',N'Der Pilz stirb ohne Nahrung innerhalb weniger Nonen ab, wenn er nicht Winterruhe (durchgehend unter 0 C°) hält.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000124',2,7,N'k.A.',N'',N'Der Pilz stirb ohne Nahrung innerhalb weniger Nonen ab, wenn er nicht Winterruhe (durchgehend unter 0 C°) hält.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000124',1,8,N'k.A.',N'',N'Nektar nur magisch länger haltbar, Blüten einige Nonen, wenn nicht mit Alkohol vorsichtig behandelt, Frucht ein Oktal, Fruchtsaft wie Nektar');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000126',1,8,N'k.A.',N'',N'Nektar nur magisch länger haltbar, Blüten einige Nonen, wenn nicht mit Alkohol vorsichtig behandelt, Frucht ein Oktal, Fruchtsaft wie Nektar',N'Verbreitung jeweils auf Baumästen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000126',1,8,N'k.A.',N'',N'Nektar nur magisch länger haltbar, Blüten einige Nonen, wenn nicht mit Alkohol vorsichtig behandelt, Frucht ein Oktal, Fruchtsaft wie Nektar',N'Verbreitung jeweils auf Baumästen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000128',1,8,N'k.A.',N'',N'Nektar nur magisch länger haltbar, Blüten einige Nonen, wenn nicht mit Alkohol vorsichtig behandelt, Frucht ein Oktal, Fruchtsaft wie Nektar',N'Verbreitung jeweils auf Baumästen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000129',1,8,N'k.A.',N'',N'trocken bis zu zwei Jahre');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000130',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000131',1,8,N'',N'',N'1',N'Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000132',1,8,N'',N'',N'1',N'Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000132',1,8,N'',N'',N'trocken gelagert, aber regelmäßig geschüttelt hält der Saft ein halbes Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000134',1,8,N'',N'',N'trocken gelagert, aber regelmäßig geschüttelt hält der Saft ein halbes Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000135',1,8,N'k.A.',N'',N'trocken nahezu unbegrenzt');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000136',1,8,N'k.A.',N'',N'trocken nahezu unbegrenzt');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000136',4,4,N'k.A.',N'',N'trocken vier Nonen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000138',4,4,N'k.A.',N'',N'trocken vier Nonen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000139',1,8,N'k.A.',N'',N'1',N'Tag');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000140',1,8,N'k.A.',N'',N'mindestens leicht feucht gelagert 1 Oktal');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000140',4,5,N'',N'',N'eine halbe None');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000142',4,5,N'',N'',N'eine halbe None');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000143',1,8,N'k.A.',N'',N'dauerhaft');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000143',1,8,N'k.A.',N'',N'ein halbes Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000145',1,8,N'k.A.',N'',N'ein halbes Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000146',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000146',1,8,N'k.A.',N'',N'1',N'Okal');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[HaltbarkeitEinheit]) VALUES (N'00000000-0000-0000-00ff-000000000148',1,8,N'k.A.',N'',N'1',N'Okal');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000148',1,5,N'k.A.',N'',N'Blüten eingelegt halbes Jahr, Flaum ebenso lange');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000150',1,5,N'k.A.',N'',N'Blüten eingelegt halbes Jahr, Flaum ebenso lange');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000150',1,5,N'k.A.',N'',N'Blüten eingelegt halbes Jahr, Flaum ebenso lange');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000152',1,5,N'k.A.',N'',N'Blüten eingelegt halbes Jahr, Flaum ebenso lange');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000152',1,8,N'k.A.',N'',N'mindestens leicht feucht ein Oktal');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000154',1,8,N'k.A.',N'',N'mindestens leicht feucht ein Oktal');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000154',6,6,N'6 Samen pro Portion',N'',N'trocken gelagert ein gutes Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000156',6,6,N'6 Samen pro Portion',N'',N'trocken gelagert ein gutes Jahr');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000157',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000158',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000159',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000160',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000161',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000162',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000163',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000164',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000165',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000166',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000167',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000168',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000169',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000170',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000171',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000172',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000173',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000174',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000175',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000176',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000177',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000178',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000179',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000180',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000181',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000182',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000183',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000184',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000185',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000186',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000187',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000188',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000189',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000190',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000191',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000192',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000192',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000194',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000195',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000196',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000197',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000197',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000199',1,8,N'k.A.',N'',N'k.A.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000200',1,8,N'k.A.',N'',N'k.A.');
GO


--Pflanze_Gebiet
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'00000000-0000-0000-00f9-000000000052');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000002',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-00f9-000000000082');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000004',N'00000000-0000-0000-00f9-000000000010');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00f9-000000000064');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'00000000-0000-0000-00f9-000000000037');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000007',N'00000000-0000-0000-00f9-000000000056');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000008',N'00000000-0000-0000-00f9-000000000011');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000009',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000010',N'00000000-0000-0000-00f9-000000000072');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00f9-000000000069');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-00f9-000000000048');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00f9-000000000008');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'00000000-0000-0000-00f9-000000000075');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-00f9-000000000042');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000017',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'00000000-0000-0000-00f9-000000000073');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-00f9-000000000023');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-00f9-000000000045');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'00000000-0000-0000-00f9-000000000047');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00f9-000000000043');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00f9-000000000025');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000025',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000026',N'00000000-0000-0000-00f9-000000000047');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'00000000-0000-0000-00f9-000000000035');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'00000000-0000-0000-00f9-000000000070');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-00f9-000000000084');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'00000000-0000-0000-00f9-000000000059');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-00f9-000000000049');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'00000000-0000-0000-00f9-000000000021');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'00000000-0000-0000-00f9-000000000005');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'00000000-0000-0000-00f9-000000000002');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'00000000-0000-0000-00f9-000000000077');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000038',N'00000000-0000-0000-00f9-000000000076');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000039',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00f9-000000000040');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-00f9-000000000054');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000042',N'00000000-0000-0000-00f9-000000000046');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'00000000-0000-0000-00f9-000000000004');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'00000000-0000-0000-00f9-000000000062');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'00000000-0000-0000-00f9-000000000058');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'00000000-0000-0000-00f9-000000000087');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00f9-000000000078');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000049',N'00000000-0000-0000-00f9-000000000015');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000050',N'00000000-0000-0000-00f9-000000000015');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'00000000-0000-0000-00f9-000000000017');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'00000000-0000-0000-00f9-000000000029');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'00000000-0000-0000-00f9-000000000030');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000054',N'00000000-0000-0000-00f9-000000000033');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'00000000-0000-0000-00f9-000000000034');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-00f9-000000000041');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-00f9-000000000028');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-00f9-000000000007');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-00f9-000000000007');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-00f9-000000000032');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-00f9-000000000086');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000062',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000063',N'00000000-0000-0000-00f9-000000000053');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-00f9-000000000025');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-00f9-000000000085');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-00f9-000000000065');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'00000000-0000-0000-00f9-000000000071');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-00f9-000000000060');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00f9-000000000051');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'00000000-0000-0000-00f9-000000000057');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'00000000-0000-0000-00f9-000000000017');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00f9-000000000036');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000075',N'00000000-0000-0000-00f9-000000000044');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-00f9-000000000074');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00f9-000000000022');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000079',N'00000000-0000-0000-00f9-000000000039');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'00000000-0000-0000-00f9-000000000031');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'00000000-0000-0000-00f9-000000000013');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000082',N'00000000-0000-0000-00f9-000000000067');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000083',N'00000000-0000-0000-00f9-000000000017');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'00000000-0000-0000-00f9-000000000063');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00f9-000000000080');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-00f9-000000000027');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-00f9-000000000003');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'00000000-0000-0000-00f9-000000000037');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00f9-000000000026');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'00000000-0000-0000-00f9-000000000050');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-00f9-000000000048');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'00000000-0000-0000-00f9-000000000068');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000094',N'00000000-0000-0000-00f9-000000000037');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000095',N'00000000-0000-0000-00f9-000000000066');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000096',N'00000000-0000-0000-00f9-000000000014');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00f9-000000000012');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00f9-000000000024');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000100',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'00000000-0000-0000-00f9-000000000020');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'00000000-0000-0000-00f9-000000000009');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'00000000-0000-0000-00f9-000000000009');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-00f9-000000000017');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'00000000-0000-0000-00f9-000000000018');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'00000000-0000-0000-00f9-000000000061');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'00000000-0000-0000-00f9-000000000083');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00f9-000000000006');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'00000000-0000-0000-00f9-000000000019');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00f9-000000000025');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'00000000-0000-0000-00f9-000000000038');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-00f9-000000000081');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-00f9-000000000016');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00f9-000000000048');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00f9-000000000055');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'00000000-0000-0000-00f9-000000000001');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000119',N'00000000-0000-0000-00f9-000000000089');
GO

-- Pflanze_Gebiet Myranor
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000120',N'00000000-0000-0000-00f9-000000000131');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-00f9-000000000106');
GO
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-00f9-000000000126');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00f9-000000000121');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000128',N'00000000-0000-0000-00f9-000000000121');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000129',N'00000000-0000-0000-00f9-000000000109');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000130',N'00000000-0000-0000-00f9-000000000133');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000131',N'00000000-0000-0000-00f9-000000000100');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000132',N'00000000-0000-0000-00f9-000000000100');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000134',N'00000000-0000-0000-00f9-000000000102');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000135',N'00000000-0000-0000-00f9-000000000136');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000136',N'00000000-0000-0000-00f9-000000000136');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000138',N'00000000-0000-0000-00f9-000000000137');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000139',N'00000000-0000-0000-00f9-000000000114');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'00000000-0000-0000-00f9-000000000112');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000142',N'00000000-0000-0000-00f9-000000000101');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'00000000-0000-0000-00f9-000000000115');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000145',N'00000000-0000-0000-00f9-000000000119');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000146',N'00000000-0000-0000-00f9-000000000125');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-00f9-000000000118');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000150',N'00000000-0000-0000-00f9-000000000130');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000152',N'00000000-0000-0000-00f9-000000000094');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000154',N'00000000-0000-0000-00f9-000000000092');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000156',N'00000000-0000-0000-00f9-000000000132');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000157',N'00000000-0000-0000-00f9-000000000123');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000158',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000159',N'00000000-0000-0000-00f9-000000000093');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000160',N'00000000-0000-0000-00f9-000000000108');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000161',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000162',N'00000000-0000-0000-00f9-000000000120');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000163',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000164',N'00000000-0000-0000-00f9-000000000097');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000165',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000166',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000167',N'00000000-0000-0000-00f9-000000000127');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000168',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000169',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000170',N'00000000-0000-0000-00f9-000000000130');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000171',N'00000000-0000-0000-00f9-000000000107');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000172',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000173',N'00000000-0000-0000-00f9-000000000110');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000174',N'00000000-0000-0000-00f9-000000000134');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000175',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000176',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000177',N'00000000-0000-0000-00f9-000000000104');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000178',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000179',N'00000000-0000-0000-00f9-000000000122');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000180',N'00000000-0000-0000-00f9-000000000088');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000181',N'00000000-0000-0000-00f9-000000000135');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000182',N'00000000-0000-0000-00f9-000000000135');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000183',N'00000000-0000-0000-00f9-000000000111');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000184',N'00000000-0000-0000-00f9-000000000111');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000185',N'00000000-0000-0000-00f9-000000000096');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000186',N'00000000-0000-0000-00f9-000000000099');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000187',N'00000000-0000-0000-00f9-000000000116');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000188',N'00000000-0000-0000-00f9-000000000117');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000189',N'00000000-0000-0000-00f9-000000000105');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000190',N'00000000-0000-0000-00f9-000000000124');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000191',N'00000000-0000-0000-00f9-000000000090');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000192',N'00000000-0000-0000-00f9-000000000103');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000194',N'00000000-0000-0000-00f9-000000000098');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000195',N'00000000-0000-0000-00f9-000000000095');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000196',N'00000000-0000-0000-00f9-000000000091');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000197',N'00000000-0000-0000-00f9-000000000129');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000199',N'00000000-0000-0000-00f9-000000000128');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000200',N'00000000-0000-0000-00f9-000000000113');
GO


--Pflanze_Verbreitung
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'00000000-0000-0000-00fe-000000000053',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'00000000-0000-0000-00fe-000000000005',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000002',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-00fe-000000000007',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000004',N'00000000-0000-0000-00fe-000000000015',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000007',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000007',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000008',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000008',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000009',N'00000000-0000-0000-00fe-000000000027',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000010',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000010',N'00000000-0000-0000-00fe-000000000055',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-00fe-000000000055',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-00fe-000000000012',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000031',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000030',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'00000000-0000-0000-00fe-000000000056',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'00000000-0000-0000-00fe-000000000058',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000017',N'00000000-0000-0000-00fe-000000000020',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-00fe-000000000018',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-00fe-000000000028',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'00000000-0000-0000-00fe-000000000026',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000046',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000008',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000010',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000053',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000053',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000025',N'00000000-0000-0000-00fe-000000000022',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000026',N'00000000-0000-0000-00fe-000000000006',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'00000000-0000-0000-00fe-000000000017',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'00000000-0000-0000-00fe-000000000018',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'00000000-0000-0000-00fe-000000000046',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-00fe-000000000043',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-00fe-000000000019',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'00000000-0000-0000-00fe-000000000024',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'00000000-0000-0000-00fe-000000000021',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'00000000-0000-0000-00fe-000000000054',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'00000000-0000-0000-00fe-000000000038',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000038',N'00000000-0000-0000-00fe-000000000007',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000039',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000034',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000033',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000042',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'00000000-0000-0000-00fe-000000000029',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'00000000-0000-0000-00fe-000000000056',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'00000000-0000-0000-00fe-000000000019',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000012',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000010',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000008',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000012',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000010',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000008',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000049',N'00000000-0000-0000-00fe-000000000025',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000050',N'00000000-0000-0000-00fe-000000000025',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'00000000-0000-0000-00fe-000000000049',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000054',N'00000000-0000-0000-00fe-000000000013',100);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'00000000-0000-0000-00fe-000000000056',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'00000000-0000-0000-00fe-000000000057',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-00fe-000000000002',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-00fe-000000000037',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-00fe-000000000014',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-00fe-000000000012',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000062',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000063',N'00000000-0000-0000-00fe-000000000035',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-00fe-000000000044',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-00fe-000000000040',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'00000000-0000-0000-00fe-000000000006',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'00000000-0000-0000-00fe-000000000050',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000036',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000011',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000075',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-00fe-000000000038',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000055',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000038',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000056',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000004',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000042',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000013',100);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000079',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000079',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'00000000-0000-0000-00fe-000000000031',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'00000000-0000-0000-00fe-000000000012',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000082',N'00000000-0000-0000-00fe-000000000009',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000083',N'00000000-0000-0000-00fe-000000000048',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'00000000-0000-0000-00fe-000000000039',1);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000003',100);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'00000000-0000-0000-00fe-000000000021',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'00000000-0000-0000-00fe-000000000023',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-00fe-000000000003',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-00fe-000000000056',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000055',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000010',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'00000000-0000-0000-00fe-000000000047',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000094',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000094',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000095',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000095',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000096',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000096',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000054',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000014',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000014',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000100',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'00000000-0000-0000-00fe-000000000051',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'00000000-0000-0000-00fe-000000000006',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'00000000-0000-0000-00fe-000000000049',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'00000000-0000-0000-00fe-000000000043',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000012',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000045',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-00fe-000000000054',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-00fe-000000000032',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00fe-000000000011',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000119',N'00000000-0000-0000-00fe-000000000001',0);
GO

--Pflanze_Verbreitung für Myranor
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000120',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000120',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-00fe-000000000059',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-00fe-000000000065',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-00fe-000000000059',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-00fe-000000000065',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-00fe-000000000059',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'00000000-0000-0000-00fe-000000000065',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-00fe-000000000053',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-00fe-000000000062',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-00fe-000000000053',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-00fe-000000000062',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000128',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000128',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000128',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000128',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000129',N'00000000-0000-0000-00fe-000000000056',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000129',N'00000000-0000-0000-00fe-000000000066',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000130',N'00000000-0000-0000-00fe-000000000056',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000130',N'00000000-0000-0000-00fe-000000000067',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000131',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000131',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000132',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000132',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000132',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000132',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000134',N'00000000-0000-0000-00fe-000000000043',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000134',N'00000000-0000-0000-00fe-000000000018',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000135',N'00000000-0000-0000-00fe-000000000060',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000135',N'00000000-0000-0000-00fe-000000000068',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000136',N'00000000-0000-0000-00fe-000000000060',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000136',N'00000000-0000-0000-00fe-000000000068',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000136',N'00000000-0000-0000-00fe-000000000060',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000136',N'00000000-0000-0000-00fe-000000000068',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000138',N'00000000-0000-0000-00fe-000000000043',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000138',N'00000000-0000-0000-00fe-000000000069',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000138',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000139',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000139',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000139',N'00000000-0000-0000-00fe-000000000069',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'00000000-0000-0000-00fe-000000000061',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'00000000-0000-0000-00fe-000000000061',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000142',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000142',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'00000000-0000-0000-00fe-000000000056',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'00000000-0000-0000-00fe-000000000056',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000145',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000145',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000145',N'00000000-0000-0000-00fe-000000000072',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000146',N'00000000-0000-0000-00fe-000000000062',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000146',N'00000000-0000-0000-00fe-000000000070',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000146',N'00000000-0000-0000-00fe-000000000062',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000146',N'00000000-0000-0000-00fe-000000000070',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000150',N'00000000-0000-0000-00fe-000000000063',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000150',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000150',N'00000000-0000-0000-00fe-000000000063',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000150',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000152',N'00000000-0000-0000-00fe-000000000063',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000152',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000152',N'00000000-0000-0000-00fe-000000000063',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000152',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000154',N'00000000-0000-0000-00fe-000000000064',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000154',N'00000000-0000-0000-00fe-000000000071',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000154',N'00000000-0000-0000-00fe-000000000064',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000154',N'00000000-0000-0000-00fe-000000000071',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000156',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000156',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000157',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000158',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000159',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000160',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000161',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000162',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000163',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000164',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000165',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000166',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000167',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000168',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000169',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000170',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000171',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000172',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000173',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000174',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000175',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000176',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000177',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000178',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000179',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000180',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000181',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000182',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000183',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000184',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000185',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000186',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000187',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000188',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000189',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000190',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000191',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000192',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000192',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000194',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000195',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000196',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000197',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000197',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000199',N'00000000-0000-0000-00fe-000000000001',0);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000200',N'00000000-0000-0000-00fe-000000000001',0);
GO


--Held-Pflanzen hinzufügen 
--Hausregel: Damit jeder Held eine bekannte Pflanzenliste erhält
		
CREATE TABLE [Held_Pflanze] (
	[ID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[HeldGUID] uniqueidentifier NOT NULL, 
	[PflanzeGUID] uniqueidentifier NOT NULL, 
	[Bekannt] bit NOT NULL DEFAULT 0, 
	CONSTRAINT [PK_Held_Pflanze] PRIMARY KEY ([ID]),
	CONSTRAINT fk_Held_Pflanze_PflanzeGUID FOREIGN KEY ([PflanzeGUID])
		REFERENCES [Pflanze] ([PflanzeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_Held_Pflanze_HeldGUID FOREIGN KEY ([HeldGUID])
		REFERENCES [Held] ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO