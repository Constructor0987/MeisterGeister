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
	[Name] nvarchar(500) NOT NULL, 
	[Kategorie] nvarchar(254), 
	[Tags] nvarchar(1000), 
	[Bemerkung] ntext, 
	[Bestimmung] smallint NOT NULL, 
	[Bestimmung2] smallint, 
	[Bestimmungsausnahme] nvarchar(100), 
	[AusnahmeVon] smallint, 
	[AusnahmeBis] smallint, 
	[Literatur] nvarchar(500),
	CONSTRAINT [PK_Pflanze] PRIMARY KEY ([PflanzeGUID])
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

CREATE TABLE [Pflanze_Setting] (
	[ID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[PflanzeGUID] uniqueidentifier NOT NULL, 
	[SettingGUID] uniqueidentifier NOT NULL, 
	[Preis] nvarchar(20), 
	[Name] nvarchar(100),
	CONSTRAINT [PK_Pflanze_Setting] PRIMARY KEY ([ID]), 
	CONSTRAINT fk_Pflanze_Setting_PflanzeGUID FOREIGN KEY ([PflanzeGUID])
		REFERENCES [Pflanze] ([PflanzeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_Pflanze_Setting_SettingGUID FOREIGN KEY ([SettingGUID])
		REFERENCES [Setting] ([SettingGUID])
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

--Pflanze
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'Alraune',N'Kräuter',N'Nutzpflanze',N'Grundlage für Elixiere',9,N'ZBA 227 / WdA 206');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000002',N'Alveranie',N'Kräuter',N'Übernatürliche Pflanze',N'** kann nicht verkauft werden, da alle Rituale voraussetzen, dass die Blätter selbst gepflückt wurden',-5,N'ZBA 228');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'Arganstrauch',N'Kräuter',N'Heilpflanze',N'fördert Heilung',2,N'ZBA 228');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000004',N'Atan-Kiefer',N'Kräuter',N'Heilpflanze, Fieber',N'Grundlage für fiebersenkendes Mittel',6,N'ZBA 229');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'Atmon',N'Kräuter',N'Nutzpflanze',N'Aufputschmittel',5,N'ZBA 229');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'Axorda-Baum',N'Kräuter',N'Heilpflanze',N'Grundlage für ein Mittel gegen Zorgan-Pocken',2,N'ZBA 229');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000007',N'Basilamine',N'Kräuter',N'Gefährliche Pflanzen',15,5,N'Blütezeit im Ingerimm',11,11,N'ZBA 230');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000008',N'Belmart',N'Kräuter',N'Heilpflanze, Krankheit',N'Gegengift, stoppt Krankheiten',6,N'ZBA 230');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000009',N'Blutblatt',N'Kräuter',N'Nutzpflanze',N'zeigt Magie an',2,N'ZBA 231');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Bestimmungsausnahme],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000010',N'Boronie',N'Kräuter',N'Übernatürliche Pflanze',N'erleichtert Boron-Liturgien',-2,N'im Süden jederzeit, in Mittelaventurien zwischen Tsa und Boron',8,N'ZBA 231');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'Boronschlinge',N'Kräuter',N'Gefährliche Pflanze',15,N'ZBA 231, 232');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'Braunschlinge',N'Kräuter',N'Gefährliche Pflanze / Nutzpflanze',6,N'WdA 193 / K&K 109');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'Bunter Mohn',N'Kräuter',N'Nutzpflanze',-5,N'ZBA 252, 253');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'Carlog',N'Kräuter',N'Nutzpflanze',N'verleiht Dämmerungssicht',5,N'ZBA 232');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'Cheria-Kaktus',N'Kräuter',N'Giftpflanze',2,N'ZBA 232, 233');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'Chonchinis',N'Kräuter',N'Heilpflanze',N'Heilmittel bei Brand- und Ätzwunden,',6,N'ZBA 233');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000017',N'Dergolasch',N'Kräuter',N'Nutzpflanze',8,4,N'bei Kulturkunde (Zwerge)',1,13,N'WdA 193');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'Disdychonda',N'Kräuter',N'Gefährliche Pflanze',5,N'ZBA 234');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'Donf',N'Kräuter',N'Heilpflanze, Fieber, Lager, Wildnisleben',N'fiebersenkend',6,N'ZBA 234 / Myranische Mysterien 124');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'Dornrose',N'Kräuter',N'Übernatürliche Pflanze',N'mehr für besondere Edelrosen',3,N'ZBA 235');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'Efeuer',N'Kräuter',N'Gefährliche Pflanzen',2,N'ZBA 235');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'Egelschreck',N'Kräuter',N'Nutzpflanze',N'Grundlage für Wundheilung und Parasitenabwehr',6,N'ZBA 236');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'Eitriger Krötenschemel',N'Kräuter',N'Giftpflanze',2,N'Efferd bis Boron, in südlicheren Gebieten auch ganzjährig',N'ZBA 236');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000024',N'Eitriger Krötenschemel',N'Kräuter',N'Giftpflanze',2,N'in südlicheren Gebieten ganzjährig',N'ZBA 236');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000025',N'Felsenmilch',N'Kräuter',N'Nutzpflanze',4,N'WdA 193 / K&K 108');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000026',N'Feuermoos und Efferdmoos',N'Kräuter',N'Gefährliche Pflanzen',15,N'ZBA 237');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'Feuerschlick',N'Kräuter',N'Nutzpflanze / Heilpflanze',6,-5,N'wenn leuchtend',2,3,N'ZBA 237');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'Finage',N'Kräuter',N'Heilpflanze',N'reduziert Eigenschaftsabzüge, Grundlage für Heilmittel',5,N'NOK',N'ZBA 238 / Myranische Mysterien 124');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'Grauer Lotus',N'Kräuter',N'Übernatürliche Pflanze',8,N'ZBA 247');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'Grauer Mohn (Samenkapsel)',N'Kräuter',N'Giftpflanze / Nutzpflanze',1,N'NOK?',N'ZBA 253');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000031',N'Grauer Mohn (getrockneten Blüten)',N'Kräuter',N'Giftpflanze / Nutzpflanze',1,N'NOK?',N'ZBA 253');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'grüne Schleimschlange',N'Kräuter',N'Gefährliche Pflanzen',2,N'ZBA 238');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'Gulmond',N'Kräuter',N'Nutzpflanze',N'erhöht Konstitution',6,N'NOK',N'ZBA 238, 239');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'grüner Schleimpilz',N'Kräuter',N'Nutzpflanze / Giftpflanze',6,N'WdA 193, 194 / K&K 108');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'Hiradwurz',N'Kräuter',N'Nutzpflanze / Heilpflanze',8,5,N'+8 (Trockenzeit), +5 (Regenzeit)',1,13,N'ZBA 239');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'Hollbeere',N'Kräuter',N'Giftpflanze / Heilpflanze',N'**am Fundort häufig und wertlos, andernorts unbekannt und wertlos',2,N'ZBA 240');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'Höllenkraut',N'Kräuter',N'Nutzpflanze / Giftpflanze',8,N'ZBA 240');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000038',N'Horusche',N'Kräuter',N'Nutzpflanze / Giftpflanze',N'verleiht Stärke',7,N'ZBA 240, 241');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000039',N'Iribaarslilie',N'Kräuter',N'Übernatürliche Pflanze',12,N'ZBA 241, 242');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'Jagdgras',N'Kräuter',N'Gefährliche Pflanze',15,N'ZBA 242, 242');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'Joruga',N'Kräuter',N'Heilpflanze',N'Grundlage für Krankheitsbekämpfung',7,N'ZBA 243');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000042',N'Kairan',N'Kräuter',N'Nutzpflanze',6,N'ZBA 243');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'Kajubo',N'Kräuter',N'Nutzpflanze',N'ermöglicht, ohne Atemluft zu überleben',2,N'ZBA 244');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'Khôm- oder Mhanadiknolle',N'Kräuter',N'Nutzpflanze',N'**in der Not unbezahlbar, ansonsten nahezu wertlos, enthält Wasser',5,12,N'+5 (mit Blättern), +12 (ohne Blätter)',1,13,N'ZBA 244');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'Klippenzahn',N'Kräuter',N'Heilpflanze',N'beschleunigt Wundheilung',8,N'ZBA 245');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'Kukuka',N'Kräuter',N'Nutzpflanze / Giftpflanze',N'erhöht Ausdauer',10,N'ZBA 245');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'Libellengras',N'Kräuter',N'Nutzpflanze',5,N'WdA 194');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'Libellengras',N'Kräuter',N'Nutzpflanze',5,N'WdA 194');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000049',N'Lichtnebler',N'Kräuter',N'Gefährliche Pflanzen',10,4,N'+10 (+4 nach Freisetzung der Sporen)',1,13,N'K&K 107, 108');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000050',N'Lichtnebler',N'Kräuter',N'Gefährliche Pflanzen',10,4,N'+10 (+4 nach Freisetzung der Sporen)',1,13,N'K&K 107, 108');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'Lotos',N'Kräuter',N'Nutzpflanze',N'je nach Farbe, ausreichend für 2 bis 5 Stein Bausch, Leinen oder "Halbseide"',5,9,N'+5 (Lotos allgemein) bzw. +9 (einzelne Arten)',1,13,N'ZBA 246');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'Lulanie',N'Kräuter',N'Heilpflanze',N'Beruhigungsmittel',5,N'ZBA 247, 248');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'Madablüte',N'Kräuter',N'Übernatürliche Pflanze',5,15,N'+5 (+15, wenn nicht blühend)',1,13,N'ZBA 248');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000054',N'Malmomis',N'Kräuter',N'Übernatürliche Pflanze',10,N'ZBA 248, 249');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'Menchal - Kaktus',N'Kräuter',N'Nutzpflanze',N'Preis für einen frisch entwurzelten, ungeöffneten Kaktus, 5 D für eine abgefüllte Anwendung Saft, hilft gegen viele Gifte',2,N'ZBA 249');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'Merach-Strauch',N'Kräuter',N'Nutzpflanze / Giftpflanze',2,N'ZBA 250');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'Messergras',N'Kräuter',N'Nutzpflanze / Gefährliche Pflanze',6,N'ZBA 251');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'Mibelrohr',N'Kräuter',N'Nutzpflanze',N'Grundlage für Aufputschmittel',10,N'ZBA 251');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'Mibelrohr',N'Kräuter',N'Nutzpflanze',N'Grundlage für Aufputschmittel',10,N'ZBA 251');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'Mirbelstein',N'Kräuter',N'Nutzpflanze',N'Grundlage für ein Mittel gegen Ungeziefer und Elfen',8,N'ZBA 251');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'Mirhamer Seidenliane',N'Kräuter',N'Nutzpflanze',2,N'ZBA 251, 252');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000062',N'Mohn',N'Kräuter',N'Nutzpflanze / Droge',5,N'ZBA 252');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000063',N'Morgendornstrauch',N'Kräuter',N'Gefährliche Pflanzen',13,N'ZBA 254, 255');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'Naftanstaude',N'Kräuter',N'Giftpflanze',1,N'ZBA 255');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000065',N'Naftanstaude',N'Kräuter',N'Giftpflanze',1,N'ZBA 255');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'Neckerkraut',N'Kräuter',N'Nutzpflanze',N'gegen Kerkersieche',2,N'ZBA 255');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'Olginwurz',N'Kräuter',N'Heilpflanze, Gegengift',N'Gegengift',10,N'ZBA 256, 257');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'Orazal',N'Kräuter',N'Nutzpflanze',2,N'ZBA 257');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'Orkland',N'Kräuter',N'Gefährliche Pflanze',2,N'ZBA 258');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'Pestsporenpilz',N'Kräuter',N'Giftpflanze / Nutzpflanze',6,N'ZBA 258');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000071',N'Pestsporenpilz',N'Kräuter',N'Giftpflanze / Nutzpflanze',6,N'ZBA 258');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'Phosphorpilz',N'Kräuter',N'Nutzpflanze',-3,10,N'-3 wenn er leuchtet, +10 für nicht leuchtendes Geflecht',1,13,N'ZBA 258, 259');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'Purpurner Lotos',N'Kräuter',N'Giftpflanze',9,N'(einzelne Arten)',N'ZBA 246');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'Purpur Mohn',N'Kräuter',N'Übernatürliche Pflanze',3,N'ZBA 253');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000075',N'Quasselwurz',N'Kräuter',N'Nutzpflanze',12,N'ZBA 259');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'Quinya',N'Kräuter',N'Nutzpflanze',N'erhöht Körperkraft',6,N'SRD 259, 260');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'Rahjalieb',N'Kräuter',N'Nutzpflanze',N'empfängnisverhütend',5,N'ZBA 260');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'Rattenpilz',N'Kräuter',N'Übernatürliche Pflanzen',N'**je nachdem, wie viel Verkäufer und Käufer von der Dosis und Wirkung des Pilzes wissen und wie sehr sie ihn benötigen.',7,N'ZBA 260, 261');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000079',N'Rauschgurke',N'Kräuter',N'Nutzpflanze',N'macht stärker und dümmer',3,N'ZBA 261');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'Rote Pfeilblüte',N'Kräuter',N'Heilpflanze, Regeneration',N'spendet Lebensenergie',7,N'ZBA 261, 262');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'Roter Drachenschlund',N'Kräuter',N'Heilpflanze',N'verhindert Ausbruch von Lykanthropie',10,3,N'zur Blütezeit',11,12,N'ZBA 262');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000082',N'Schwarmschwamm',N'Kräuter',N'Übernatürliche Pflanze',N'**bis zu 13 Dämonenkronen für einen Samenkörper (als Paraphernalium), für den Schwamm selbst gibt es in der Regel keine Käufer.',3,N'ZBA 265');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000083',N'Schwarzer Lotos',N'Kräuter',N'Giftpflanze',6,N'ZBA 246');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'Schwarzer Mohn',N'Kräuter',N'Nutzpflanze / Droge',5,N'ZBA 253, 254');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'Seelenhauch',N'Kräuter',N'Übernatürliche Pflanze, Giftpflanze',3,N'WdA 195');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'Shurinstrauch',N'Kräuter',N'Giftpflanze',2,N'ZBA 267');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'Steinrinde',N'Kräuter',N'Nutzpflanze',12,N'K&K 107 / WdA 195');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'Talaschin',N'Kräuter',N'Nutzpflanze',5,N'ZBA 268');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'Tarnblatt',N'Kräuter',N'Giftpflanze',8,N'ZBA 268');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'Tarnele',N'Kräuter',N'Heilpflanze, Wundverbände, Regeneration',N'schmerzlindernd und heilungsfördernd',2,N'ZBA 268, 269');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'Thonnys',N'Kräuter',N'Nutzpflanze',N'erlaubt Astrale Meditation',12,5,N'+12, während der Blüte +5',N'ZBA 269');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'Tiger Mohn',N'Kräuter',N'Nutzpflanze / Droge',10,N'ZBA 254');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'Traschbart',N'Kräuter',N'Heilpflanze, Fieber',N'fiebersenkend',6,N'ZBA 269, 270');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000094',N'Trichterwurzel',N'Kräuter',N'Gefährliche Pflanzen',11,N'ZBA 270');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000095',N'Tuur-Amash-Kelch',N'Kräuter',N'Übernatürliche Pflanze',N'** Bislang gibt es noch keine Käufer für diese Pflanze.',1,N'ZBA 270, 271');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000096',N'Ulmenwürger',N'Kräuter',N'Heilpflanze, Regeneration',N'verbessert Regeneration und verhindert Wundfieber',2,N'ZBA 271');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'Vierblättrige Einbeere',N'Kräuter',N'Heilpflanze, Regeneration, Blutung, Lager, Wildnisleben',N'stoppt Blutungen und gibt Lebensenergie zurück',5,N'ZBA 271');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'Vragieswurzel',N'Kräuter',N'Giftpflanze',6,N'ZBA 272');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000099',N'Vragieswurzel',N'Kräuter',N'Giftpflanze',6,N'ZBA 272');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000100',N'Waldwebe',N'Kräuter',N'Gefährliche Pflanzen',9,N'ZBA 272');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'Wandermoos',N'Kräuter',N'Nutzpflanze',14,5,N'+14 (+5 im mobilen Zustand)',1,13,N'K&K 107 / WdA 196');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'Wasserrausch (Blüten)',N'Kräuter',N'Nutzpflanze',N'Rahjaikum, euphorisierend',1,N'ZBA 273');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'Wasserrausch (Frucht)',N'Kräuter',N'Nutzpflanze',N'Rahjaikum, euphorisierend',1,N'ZBA 273');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'Weißer Lotos',N'Kräuter',N'Giftpflanze',10,11,1,N'ZBA 247');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[AusnahmeVon],[AusnahmeBis],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'Weißgelber Lotos',N'Kräuter',N'Nutzpflanze',5,10,N'+5 (Lotos allgemein) bzw. +10, jedoch nicht von Weißem Lotos zu unterscheiden.',11,1,N'ZBA 247');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmung2],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'Winselgras',N'Kräuter',N'Nutzpflanze',12,-2,N'+12 (tagsüber), -2 (nachts)',N'ZBA 273');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Bestimmungsausnahme],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'Wirselkraut',N'Kräuter',N'Heilpflanze, Regeneration, Blutung, Lager, Wildnisleben',5,N'NOK?',N'ZBA 273, 274 / Myranische Mysterien 124');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'Würgedattel',N'Kräuter',N'Gefährliche Pflanze',5,N'ZBA 274');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'Zithabar',N'Kräuter',N'Nutzpflanze',N'Wird meist in einer Wasserpfeife geraucht',5,N'ZBA 275');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'Zunderschwamm',N'Kräuter',N'Nutzpflanze, Lager, Wildnisleben',N'ermöglicht Feuermachen',2,N'SRD 275, 276 / H&K 159');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'Zwölfblatt',N'Kräuter',N'Heilpflanze, Vorbeugung gegen Krankheiten',N'schützt vor Ansteckung',5,N'ZBA 276');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'Yaganstrauch',N'Kräuter',N'Nutzpflanze',N'Preis pro Nuss, erhöht Ausdauer',6,N'ZBA 274, 275');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'Nothilf',N'Kräuter',N'Heilmittel',N'Preis pro Portion, heilt Brandwunden',6,N'ZBA 256');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000114',N'Nothilf',N'Kräuter',N'Heilmittel',N'Preis pro Portion, heilt Brandwunden',6,N'ZBA 256');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'Satuariensbusch',N'Kräuter',N'Heilpflanze / Übernatürliche Pflanze',N'Preis für eine Handvoll Blätter, Blüten oder Früchte, schützt vor Wundfieber',-2,N'ZBA 263');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'Tigermohn',N'Kräuter',N'Nutzpflanze / Droge',N'Preis pro Samenkapsel, Beruhigungsmittel',10,N'ZBA 254');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'Schlangenzünglein',N'Kräuter',N'Nutzpflanze',N'Preis pro Saft einer Pflanze, zeigt Magie an',3,N'ZBA 263, 264');
GO
INSERT INTO [Pflanze] ([PflanzeGUID],[Name],[Kategorie],[Tags],[Bemerkung],[Bestimmung],[Literatur]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'Bleichmohn',N'Kräuter',N'Nutzpflanze / Droge',N'Schmerzmittel',5,N'ZBA 252');
GO

--Pflanze_Ernte
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000001',1,13,N'1',N'Pflanze',N'W3+2 Tage');
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
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000012',1,13,N'vier Farnblätter und zwei je 3 W6 Schritt lange Ranken (je nach Alter der Pflanze)',N'ganze Pflanze',N'bei trockener Lagerung 1 W6 Monate für die Rankenseile');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000013',4,4,N'1',N'geschlossene Samenkapsel',N'W6+9 Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000014',10,3,N'W6',N'Blüten mit je einem Stempel',N'2W6+18 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000015',1,13,N'1W6 halbe Stein Kaktusfleisch und pro Stein 3W6+8 Stacheln',N'ganze Pflanze',N'Fruchtfleisch W6 Tage, Stacheln bzw. deren Gift 3W6 Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000016',8,5,N'W20',N'Blätter',N'W6+22 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000017',2,11,N'1W6',N'Pilzehüte',N'2W6 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000018',1,13,N'2 Blätter',N'ganze Pflanze',N'W3+7 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000019',1,13,N'1',N'Stängel',N'W6+6 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000020',1,13,N'1',N'Strauch mit 1W6 Blüten',N'W6+7 Tage (Blüte) bzw. Wochen (Dornen)',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000021',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000022',2,6,N'2W20 Blätter',N'ganze Pflanze',N'W3+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000023',3,5,N'2W6 Pilzhäute pro Fundort',N'ganze Pflanze',N'W6+1 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000024',1,13,N'2W6 Pilzhäute pro Fundort',N'ganze Pflanze',N'W6+1 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000025',1,13,N'1 Schank',N'ganze Pflanze',N'2W6 Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000026',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000027',1,13,N'W6 Stein',N'der Algen',N'22 Stunden',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000028',1,13,N'ein Baum mit W20 Trieben',N'ganze Pflanze',N'W3+3 Monate, versetzt mit Orazal doppelt so lang');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000029',9,5,N'W6+1',N'Blüten',N'W6 Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000030',1,13,N'eine (geschloss.) Samenkapsel / eine Blüte',N'Kapsel/Blüte',N'W6+18 Monate (Samenkapsel trocken aufbewahrt) / W6+12 Wochen (getrocknete Blätter licht- und luftdicht aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000031',1,13,N'eine (geschloss.) Samenkapsel / eine Blüte',N'Kapsel/Blüte',N'W6+18 Monate (Samenkapsel trocken aufbewahrt) / W6+12 Wochen (getrocknete Blätter licht- und luftdicht aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000032',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000033',1,13,N'2W6',N'Blätter',N'W6+22 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000034',1,13,N'1W20 Unzen',N'ganze Pflanze',N'1W6 Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000035',1,13,N'1',N'Wurzel',N'W3+6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000036',2,3,N'2W6 Sträucher mit jeweils 2W6+5 Beeren und 2W6+3 Blättern der untersten Zweige',N'ganze Pflanze',N'W6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000037',1,13,N'1W20 halbe Stein',N'der Ranken',N'W6+6 Monate (abgeschlagene Ranken)',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000038',1,13,N'W6 erntereife Schoten pro Pflanze, W3 Kerne pro Schote',N'ganze Pflanze',N'in der Schote 2W6+2 Tage, einzeln W6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000039',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000040',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000041',12,5,N'1',N'Wurzel',N'W3+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000042',1,13,N'1',N'Halm',N'W3+3 Wochen im eigenen Wasser');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000043',9,4,N'2W6',N'Knospen, von denen Haipu immer höchstens die Hälfte abpflücken, damit der Strauch geschont wird. ',N'W6+10 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000044',1,13,N'1',N'Wurzel mit bis zu W6 Maß klarem Wasser',N'Wasser in der ungeöffneten Knolle etwa 1 Monat');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000045',10,2,N'2W6',N'Stängel',N'W3+2 Tage, maximal bis zum nächsten Vollmond');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000046',1,13,N'1W3 x 20',N'Blätter',N'1W6+1 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000047',2,3,N'1',N'Frucht',N'1 Woche');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000048',11,12,N'1',N'Frucht',N'1 Woche');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000049',3,6,N'1 Skrupel',N'Sporen',N'1W6 Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000050',10,1,N'1 Skrupel',N'Sporen',N'1W6 Tage',1);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000051',11,1,N'2W6+1',N'Blüten pro Fundort',N'W3+6 Monate (getrocknete Blätter, licht- und luftdicht aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000052',12,2,N'1',N'Blüte',N'W3+5 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000053',1,13,N'1',N'Blüte',N'im vollen Mondlicht unbegrenzt, vergeht bei Monduntergang');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000054',2,2,N'1',N'Blüte',N'W3 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000055',1,13,N'ein Kaktus mit W6 halben Maß Menchalsaft; bei 1 auf W20: mit W6 Blüten',N'ganze Pflanze',N'W3+5 Wochen (entwurzelter Kaktus), W6+8 Wochen (Menchalsaft)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000056',3,4,N'2W20 reife Früchte',N'pro Strauch',N'W6+7 Tage',N'Giftstufe 1');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000057',1,13,N'1',N'ganze Pflanze',N'Abgeschnittenes Gras hält sich – einigermaßen trocken gelagert – wie Stroh mehrere Jahre lang.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000058',11,3,N'1',N'ganze Pflanze',N'2W6 Kolben pro Fundort');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000059',8,6,N'1',N'ganze Pflanze',N'2W6 Kolben pro Fundort');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000060',10,5,N'1',N'Wurzelknolle',N'ausgegraben W3+2 Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000061',8,5,N'1',N'Ranke (mit 2 bis 3 Knoten)',N'sowohl als Seil als auch als unverarbeitete Liane bei sachgemäßer Lagerung praktisch unbegrenzt');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000062',2,2,N'W6',N'geschlossene Samenkapseln',N'2W6+18 Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000063',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000064',1,13,N'1',N'Staude (ganze Pflanze)',N'Die abgeerntete, komplette Pflanze hält sich 1W+2 Tage lang, der ausgepresste Saft 1W+2 Wochen (in einem luftdicht geschlossenen Metall-, Glas- oder Keramikgefäß).');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000065',11,2,N'1',N'Staude (ganze Pflanze)',N'Die abgeerntete, komplette Pflanze hält sich 1W+2 Tage lang, der ausgepresste Saft 1W+2 Wochen (in einem luftdicht geschlossenen Metall-, Glas- oder Keramikgefäß).');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000066',1,13,N'1W20+ 5',N'Blätter',N'W3+3 Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000067',1,13,N'W3',N'Moosballen',N'2W6+12 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000068',1,13,N'W6',N'verholzte Stängel',N'mehrere Jahre');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000069',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000070',3,5,N'1',N'Pilzhaut',N'W6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000071',10,12,N'1',N'Pilzhaut',N'W6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000072',1,13,N'W6',N'Stein der leuchtende Geflechtstücke (für die direkte Anwendung, sonst trockenes Geflecht)',N'W6 SR (Leuchtkraft)',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000073',11,1,N'W6+1',N'Blüten pro Fundort',N'W6+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000074',12,12,N'1',N'(geschlossene) Samenkapsel',N'13 Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000075',9,4,N'1',N'Wurzel',N'W3+3 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000076',11,4,N'W3+2',N'Beeren',N'1W6+12 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000077',9,7,N'2W6',N'Blätter',N'3 Tage',N'wirkt nicht auf Achaz');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000078',2,11,N'1',N'Pilz',N'W6+28 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000079',8,5,N'3W6',N'reife Rauschgurken pro Baum',N'W3+1 Wochen');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000080',10,12,N'W6',N'Blüten',N'W6 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000081',1,13,N'W6',N'Blätter',N'W3+6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000082',1,13,N'1 Schwamm, W2 Samenkörper',N'ganze Pflanze',N'W3 Tage (Samenkörper W6 Monate)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000083',11,1,N'W6',N'Blüten pro Fundort',N'W6+2 Tage (oder bis zum nächsten Windhauch)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000084',3,5,N'2 Blätter, 1 Samenkapsel',N'ganze Pflanze',N'2W6+9 Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000085',11,1,N'1',N'Blüte',N'in Wasser aufbewahrt 1W3 +3 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000086',11,2,N'W20',N'Knollen (Fruchtkörper)',N'W6+2 Tage (Knolle), W3+1 Tage (Blätter)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000087',1,13,N'W6',N'Stein',N'W6 Tage',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000088',1,13,N'W6',N'Flechten',N'1 Stunde (Wirkdauer)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000089',1,13,N'1',N'ganze Pflanze',N'1W6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000090',11,4,N'1',N'ganze Pflanze',N'W6+18 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000091',10,4,N'W6+2',N'Blätter',N'2W6+36 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000092',4,4,N'1',N'geschlossene Samenkapsel',N'W6+9 Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000093',1,13,N'W6',N'Flechten',N'bei feuchter Lagerung 1W6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000094',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000095',1,13,N'W6+3',N'Kelche, für eine Tuur-Amash-Beere müssen 13 TaP* übrig bleiben.',N'W6 Tage / W6 Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000096',3,4,N'W20',N'Blüten',N'3W6+22 Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Bemerkung]) VALUES (N'00000000-0000-0000-00ff-000000000097',9,6,N'1',N'Strauch mit 1W6 Beeren',N'W3+2 Tage',N'Werden an einem Tag 2 Beeren eingenommen, besteht bei 1—2 auf W20 die Möglichkeit einer Sucht (Krankheit Stufe 10), die mit jeder weiteren Beere um 5 % steigt. Ein Süchtiger, der nicht einmal in der Woche Vierblatt zu sich nehmen kann, verliert 1W6 LeP. Vierblatt hat für ihn keine heilsame Wirkung mehr.');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000098',3,4,N'1',N'Wurzel (außerhalb von Plantagen)',N'W+7 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000099',3,5,N'1',N'Wurzel (außerhalb von Plantagen)',N'W+7 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000100',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000101',1,13,N'1',N'Moosball',N'W20 Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000102',12,4,N'2W20 Blüten, 1 Frucht (Um eine Frucht zu finden, muss eine Probe mit 12 TaP* gelingen.)',N'ganze Pflanze',N'Blüten 3W+22 Stunden, Früchte unverarbeitet W6+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000103',12,4,N'2W20 Blüten, 1 Frucht (Um eine Frucht zu finden, muss eine Probe mit 12 TaP* gelingen.)',N'ganze Pflanze',N'Blüten 3W+22 Stunden, Früchte unverarbeitet W6+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000104',11,1,N'W6+1',N'Blüten pro Fundort',N'W6+2 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000105',11,1,N'W3',N'Blüten',N'W3+6 Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000106',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000107',9,5,N'W6+2',N'Blätter',N'2W6+18 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil]) VALUES (N'00000000-0000-0000-00ff-000000000108',1,13,N'1',N'ganze Pflanze');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000109',10,1,N'3W20',N'Blätter',N'1 Tag');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit],[Gewicht]) VALUES (N'00000000-0000-0000-00ff-000000000110',1,13,N'W6',N'Pilze',N'W6+2 Monate',40);
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000111',6,1,N'12',N'Stängel pro Pflanze',N'W6+9 Stunden');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000112',5,5,N'W6',N'Nüsse',N'W6+7 Monate (Nuss oder Öl)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000113',1,1,N'W20+2 Blüten, 2W20 +10 Blätter',N'ganze Pflanze',N'W3+6 Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000114',10,10,N'W20+2 Blüten, 2W20 +10 Blätter',N'ganze Pflanze',N'W3+6 Monate');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000115',9,4,N'2W20 Blätter, W20 Blüten, W20 Früchte, W3 Flux Saft',N'ganze Pflanze',N'Saft W3+6 Tage (in einem geschlossenen, abgedunkelten Gefäß)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000116',4,4,N'1',N'geschlossene Samenkapsel',N'W6+9 Monate (trocken aufbewahrt)');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000117',1,13,N'1',N'Saft einer Pflanze',N'W3+6 Tage');
GO
INSERT INTO [Pflanze_Ernte] ([PflanzeGUID],[Von],[Bis],[Grundmenge],[Pflanzenteil],[Haltbarkeit]) VALUES (N'00000000-0000-0000-00ff-000000000118',2,2,N'W6',N'geschlossene Samenkapseln',N'2W6+18 Monate (trocken aufbewahrt)');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'00000000-0000-0000-00f9-000000000079');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000024',N'00000000-0000-0000-00f9-000000000025');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000031',N'00000000-0000-0000-00f9-000000000001');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'00000000-0000-0000-00f9-000000000078');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000065',N'00000000-0000-0000-00f9-000000000025');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000071',N'00000000-0000-0000-00f9-000000000051');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000099',N'00000000-0000-0000-00f9-000000000024');
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
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000114',N'00000000-0000-0000-00f9-000000000081');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-00f9-000000000016');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00f9-000000000048');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00f9-000000000055');
GO
INSERT INTO [Pflanze_Gebiet] ([PflanzeGUID],[GebietGUID]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'00000000-0000-0000-00f9-000000000001');
GO


--Pflanze_Setting
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'00000000-0000-0000-5e77-000000000001',N'8');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000002',N'00000000-0000-0000-5e77-000000000001',N'**');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-5e77-000000000001',N'7');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000004',N'00000000-0000-0000-5e77-000000000001',N'12');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-5e77-000000000001',N'20');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'00000000-0000-0000-5e77-000000000001',N'150');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000007',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000008',N'00000000-0000-0000-5e77-000000000001',N'9');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000009',N'00000000-0000-0000-5e77-000000000001',N'7');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000010',N'00000000-0000-0000-5e77-000000000001',N'**');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'00000000-0000-0000-5e77-000000000001',N'2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-5e77-000000000001',N'0,05-0,1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'00000000-0000-0000-5e77-000000000001',N'1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-5e77-000000000001',N'2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000017',N'00000000-0000-0000-5e77-000000000001',N'1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'00000000-0000-0000-5e77-000000000001',N'30');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-5e77-000000000001',N'3');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-5e77-000000000001',N'0,05');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-5e77-000000000001',N'0,8');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000024',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000025',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000026',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'00000000-0000-0000-5e77-000000000001',N'0,1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'00000000-0000-0000-5e77-000000000001',N'6');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'00000000-0000-0000-5e77-000000000001',N'80');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000031',N'00000000-0000-0000-5e77-000000000001',N'150');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-5e77-000000000001',N'8');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'00000000-0000-0000-5e77-000000000001',N'8');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'00000000-0000-0000-5e77-000000000001',N'**');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000038',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000039',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-5e77-000000000001',N'8');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000042',N'00000000-0000-0000-5e77-000000000001',N'15');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'00000000-0000-0000-5e77-000000000001',N'2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'00000000-0000-0000-5e77-000000000001',N'**');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'00000000-0000-0000-5e77-000000000001',N'0,5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'00000000-0000-0000-5e77-000000000001',N'50');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000049',N'00000000-0000-0000-5e77-000000000001',N'20');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000050',N'00000000-0000-0000-5e77-000000000001',N'20');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'00000000-0000-0000-5e77-000000000001',N'1-5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'00000000-0000-0000-5e77-000000000001',N'12');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000054',N'00000000-0000-0000-5e77-000000000001',N'300');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'00000000-0000-0000-5e77-000000000001',N'180');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-5e77-000000000001',N'0,1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-5e77-000000000001',N'1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-5e77-000000000001',N'60');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000062',N'00000000-0000-0000-5e77-000000000001',N'70');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000063',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'00000000-0000-0000-5e77-000000000001',N'80');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000065',N'00000000-0000-0000-5e77-000000000001',N'80');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-5e77-000000000001',N'0,5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-5e77-000000000001',N'30');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'00000000-0000-0000-5e77-000000000001',N'12');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-5e77-000000000001',N'2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000071',N'00000000-0000-0000-5e77-000000000001',N'2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'00000000-0000-0000-5e77-000000000001',N'0,2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'00000000-0000-0000-5e77-000000000001',N'6');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-5e77-000000000001',N'50');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000075',N'00000000-0000-0000-5e77-000000000001',N'3');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-5e77-000000000001',N'1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-5e77-000000000001',N'1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-5e77-000000000001',N'**');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000079',N'00000000-0000-0000-5e77-000000000001',N'0,5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'00000000-0000-0000-5e77-000000000001',N'1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'00000000-0000-0000-5e77-000000000001',N'200');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000082',N'00000000-0000-0000-5e77-000000000001',N'**');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000083',N'00000000-0000-0000-5e77-000000000001',N'100');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-5e77-000000000001',N'20');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-5e77-000000000001',N'80');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'00000000-0000-0000-5e77-000000000001',N'5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-5e77-000000000001',N'0,12');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-5e77-000000000001',N'0,2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'00000000-0000-0000-5e77-000000000001',N'12');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'00000000-0000-0000-5e77-000000000001',N'0,1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000094',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000095',N'00000000-0000-0000-5e77-000000000001',N'**');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000096',N'00000000-0000-0000-5e77-000000000001',N'6');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-5e77-000000000001',N'2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000099',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000100',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'00000000-0000-0000-5e77-000000000001',N'20');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'00000000-0000-0000-5e77-000000000001',N'150');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-5e77-000000000001',N'12');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'00000000-0000-0000-5e77-000000000001',N'50-100');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'00000000-0000-0000-5e77-000000000001',N'');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-5e77-000000000001',N'0,2');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'00000000-0000-0000-5e77-000000000001',N'0,5');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-5e77-000000000001',N'9');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'00000000-0000-0000-5e77-000000000001',N'30');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-5e77-000000000001',N'300');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000114',N'00000000-0000-0000-5e77-000000000001',N'300');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-5e77-000000000001',N'0,1');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-5e77-000000000001',N'10');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-5e77-000000000001',N'20');
GO
INSERT INTO [Pflanze_Setting] ([PflanzeGUID],[SettingGUID],[Preis]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'00000000-0000-0000-5e77-000000000001',N'70');
GO

--Pflanze_Verbreitung
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'00000000-0000-0000-00fe-000000000005',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'00000000-0000-0000-00fe-000000000053',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000002',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-00fe-000000000007',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000004',N'00000000-0000-0000-00fe-000000000015',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'00000000-0000-0000-00fe-000000000038',16);
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
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000012',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-00fe-000000000012',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'00000000-0000-0000-00fe-000000000055',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000030',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000031',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'00000000-0000-0000-00fe-000000000056',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'00000000-0000-0000-00fe-000000000058',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000017',N'00000000-0000-0000-00fe-000000000020',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-00fe-000000000018',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-00fe-000000000028',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'00000000-0000-0000-00fe-000000000026',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000008',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000010',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000046',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'00000000-0000-0000-00fe-000000000053',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000024',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000024',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000024',N'00000000-0000-0000-00fe-000000000053',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000025',N'00000000-0000-0000-00fe-000000000022',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000026',N'00000000-0000-0000-00fe-000000000006',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'00000000-0000-0000-00fe-000000000017',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'00000000-0000-0000-00fe-000000000018',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000031',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'00000000-0000-0000-00fe-000000000010',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'00000000-0000-0000-00fe-000000000046',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-00fe-000000000019',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-00fe-000000000043',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'00000000-0000-0000-00fe-000000000021',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'00000000-0000-0000-00fe-000000000024',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'00000000-0000-0000-00fe-000000000054',2);
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
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000033',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000034',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000042',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'00000000-0000-0000-00fe-000000000029',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'00000000-0000-0000-00fe-000000000056',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'00000000-0000-0000-00fe-000000000019',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000008',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000010',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000012',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'00000000-0000-0000-00fe-000000000008',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'00000000-0000-0000-00fe-000000000010',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'00000000-0000-0000-00fe-000000000012',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000048',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000049',N'00000000-0000-0000-00fe-000000000025',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000050',N'00000000-0000-0000-00fe-000000000025',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'00000000-0000-0000-00fe-000000000049',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'00000000-0000-0000-00fe-000000000013',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000054',N'00000000-0000-0000-00fe-000000000013',100);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'00000000-0000-0000-00fe-000000000056',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'00000000-0000-0000-00fe-000000000057',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-00fe-000000000002',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-00fe-000000000037',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000059',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-00fe-000000000012',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-00fe-000000000014',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'00000000-0000-0000-00fe-000000000038',8);
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
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000065',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000065',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000065',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-00fe-000000000044',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-00fe-000000000040',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000071',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000071',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000071',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'00000000-0000-0000-00fe-000000000006',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'00000000-0000-0000-00fe-000000000050',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000011',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000036',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000075',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-00fe-000000000038',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000038',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'00000000-0000-0000-00fe-000000000055',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000004',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000013',100);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000042',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'00000000-0000-0000-00fe-000000000056',16);
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
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'00000000-0000-0000-00fe-000000000012',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000082',N'00000000-0000-0000-00fe-000000000009',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000083',N'00000000-0000-0000-00fe-000000000048',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'00000000-0000-0000-00fe-000000000039',1);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000003',100);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-00fe-000000000043',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'00000000-0000-0000-00fe-000000000021',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'00000000-0000-0000-00fe-000000000023',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-00fe-000000000003',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-00fe-000000000013',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'00000000-0000-0000-00fe-000000000056',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'00000000-0000-0000-00fe-000000000038',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000010',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'00000000-0000-0000-00fe-000000000055',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'00000000-0000-0000-00fe-000000000043',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'00000000-0000-0000-00fe-000000000047',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'00000000-0000-0000-00fe-000000000052',2);
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
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000001',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'00000000-0000-0000-00fe-000000000054',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000014',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000099',N'00000000-0000-0000-00fe-000000000014',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000099',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000099',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000099',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000100',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'00000000-0000-0000-00fe-000000000006',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'00000000-0000-0000-00fe-000000000051',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000103',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-00fe-000000000041',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-00fe-000000000046',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'00000000-0000-0000-00fe-000000000049',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'00000000-0000-0000-00fe-000000000041',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'00000000-0000-0000-00fe-000000000049',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'00000000-0000-0000-00fe-000000000018',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'00000000-0000-0000-00fe-000000000043',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'00000000-0000-0000-00fe-000000000054',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000012',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000046',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000052',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'00000000-0000-0000-00fe-000000000038',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'00000000-0000-0000-00fe-000000000052',2);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000018',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000019',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000043',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000045',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'00000000-0000-0000-00fe-000000000019',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'00000000-0000-0000-00fe-000000000052',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000114',N'00000000-0000-0000-00fe-000000000013',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000114',N'00000000-0000-0000-00fe-000000000052',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-00fe-000000000032',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'00000000-0000-0000-00fe-000000000054',4);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00fe-000000000010',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00fe-000000000054',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000116',N'00000000-0000-0000-00fe-000000000055',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00fe-000000000011',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00fe-000000000038',16);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'00000000-0000-0000-00fe-000000000046',8);
GO
INSERT INTO [Pflanze_Verbreitung] ([PflanzeGUID],[LandschaftGUID],[Verbreitung]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'00000000-0000-0000-00fe-000000000013',8);
GO

