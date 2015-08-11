--Pflanzen-Tabellen

CREATE TABLE [Gebiet] (
  [GebietGUID] uniqueidentifier DEFAULT newid() NOT NULL
, [Name] nvarchar(500) NOT NULL
)
GO

CREATE TABLE [Held_Pflanze] (
  [ID] uniqueidentifier DEFAULT newid() NOT NULL
, [HeldGUID] uniqueidentifier NOT NULL
, [PflanzeGUID] uniqueidentifier NOT NULL
, [Bekannt] bit DEFAULT 0 NOT NULL
)
GO

CREATE TABLE [Landschaft] (
  [LandschaftGUID] uniqueidentifier DEFAULT newid() NOT NULL
, [Name] nvarchar(500) NOT NULL
, [Kundig] nvarchar(100) NULL
)
GO

CREATE TABLE [Pflanze] (
  [PflanzeGUID] uniqueidentifier DEFAULT newid() NOT NULL
, [Bestimmung] smallint NOT NULL
, [AusnahmeVon] float NULL
, [AusnahmeBis] float NULL
, [Name] nvarchar(500) NULL
, [Kategorie] nvarchar(500) NULL
, [Bemerkung] ntext NULL
, [Literatur] nvarchar(500) NULL
, [BestimmungAusname2] smallint NULL
, [Ausnahme2Grund] nvarchar(100) NULL
, [Ausnahme2Von] float NULL
, [Ausnahme2Bis] float NULL
, [AusnameGrund] nvarchar(100) NULL
, [BestimmungAusnahme] smallint NULL
)
GO

CREATE TABLE [Pflanze_Ernte] (
  [ID] uniqueidentifier DEFAULT newid() NOT NULL
, [PflanzeGUID] uniqueidentifier NOT NULL
, [Von] float NOT NULL
, [Bis] float NOT NULL
, [Grundmenge] nvarchar(100) NOT NULL
, [Pflanzenteil] nvarchar(100) NOT NULL
, [Haltbarkeit] nvarchar(200) NULL
, [HaltbarkeitEinheit] nvarchar(200) NULL
, [Gewicht] real NULL
, [Bemerkung] nvarchar(500) NULL
, [HandelsgutGUID] uniqueidentifier NULL
)
GO

CREATE TABLE [Pflanze_Gebiet] (
  [PflanzeGUID] uniqueidentifier NOT NULL
, [GebietGUID] uniqueidentifier NOT NULL
)
GO

CREATE TABLE [Pflanze_Verbreitung] (
  [PflanzeGUID] uniqueidentifier NOT NULL
, [LandschaftGUID] uniqueidentifier NOT NULL
, [Verbreitung] smallint DEFAULT 0 NOT NULL
)
GO

ALTER TABLE [Gebiet] ADD CONSTRAINT [PK_Gebiet] PRIMARY KEY ([GebietGUID])
GO

ALTER TABLE [Held_Pflanze] ADD CONSTRAINT [PK_Held_Pflanze] PRIMARY KEY ([ID])
GO

ALTER TABLE [Landschaft] ADD CONSTRAINT [PK_Landschaft] PRIMARY KEY ([LandschaftGUID])
GO

ALTER TABLE [Pflanze] ADD CONSTRAINT [PK_Pflanze] PRIMARY KEY ([PflanzeGUID])
GO

ALTER TABLE [Pflanze_Ernte] ADD CONSTRAINT [PK_Pflanze_Ernte] PRIMARY KEY ([ID])
GO

ALTER TABLE [Pflanze_Gebiet] ADD CONSTRAINT [PK_Pflanze_Gebiet] PRIMARY KEY ([PflanzeGUID],[GebietGUID])
GO

ALTER TABLE [Pflanze_Verbreitung] ADD CONSTRAINT [pk_Pflanze_Verbreitung] PRIMARY KEY ([PflanzeGUID],[LandschaftGUID])
GO

ALTER TABLE [Held_Pflanze] ADD CONSTRAINT [fk_Held_Pflanze_HeldGUID] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [Held_Pflanze] ADD CONSTRAINT [fk_Held_Pflanze_PflanzeGUID] FOREIGN KEY ([PflanzeGUID]) REFERENCES [Pflanze]([PflanzeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [Pflanze_Ernte] ADD CONSTRAINT [fk_Pflanze_Ernte_Handelsgut] FOREIGN KEY ([HandelsgutGUID]) REFERENCES [Handelsgut]([HandelsgutGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [Pflanze_Ernte] ADD CONSTRAINT [fk_Pflanze_Ernte_PflanzeGUID] FOREIGN KEY ([PflanzeGUID]) REFERENCES [Pflanze]([PflanzeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [Pflanze_Gebiet] ADD CONSTRAINT [fk_Pflanze_Gebiet_GebietGUID] FOREIGN KEY ([GebietGUID]) REFERENCES [Gebiet]([GebietGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [Pflanze_Gebiet] ADD CONSTRAINT [fk_Pflanze_Gebiet_PflanzeGUID] FOREIGN KEY ([PflanzeGUID]) REFERENCES [Pflanze]([PflanzeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [Pflanze_Verbreitung] ADD CONSTRAINT [fk_Pflanze_Verbreitung_LandschaftGUID] FOREIGN KEY ([LandschaftGUID]) REFERENCES [Landschaft]([LandschaftGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [Pflanze_Verbreitung] ADD CONSTRAINT [fk_Pflanze_Verbreitung_PflanzeGUID] FOREIGN KEY ([PflanzeGUID]) REFERENCES [Pflanze]([PflanzeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

--Pflanzen Daten

/* Gebiet */

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000001' ,N'ganz Aventurien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000002' ,N'Albernia')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000003' ,N'Balash')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000004' ,N'Altoum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000005' ,N'Amhalassih')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000006' ,N'alanfanische Plantagen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000007' ,N'aventurische Westküste zwischen Salza und Mengbilla')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000008' ,N'aventurische Westküste zwischen Thorwal und Brabak')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000009' ,N'Bornland')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000010' ,N'Ehernes Schwert')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000011' ,N'ganz Aventurien nördlich der Linie Grangor-Elburum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000012' ,N'ganz Aventurien nördlich des Yaquir')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000013' ,N'ganz Aventurien nördlich einer Linie Drôl-Thalusa')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000014' ,N'ganz Aventurien nördlich einer Linie Havena-Perricum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000015' ,N'ganz Aventurien nördlich einer Linie Neetha-Khunchom')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000016' ,N'ganz Aventurien südlich der Gelben Sichel')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000017' ,N'ganz Aventurien südlich der Linie Havena-Perricum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000018' ,N'nördliches Tobrien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000019' ,N'ganz Aventurien südlich des Blauen Sees')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000020' ,N'ganz Aventurien südlich einer Linie Olport-Festum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000021' ,N'ganz Aventurien südlich einer Linie Thorwal-Festum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000022' ,N'ganz Aventurien südlich von Festum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000023' ,N'ganz Aventurien südlich von Gerasim')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000024' ,N'westliche Abhänge des Regengebirges')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000025' ,N'ganz Aventurien südlich von Riva')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000026' ,N'ganz Aventurien zwischen Gerasim und Mengbilla')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000027' ,N'ganz Aventurien zwischen Riva und Al''Anfa')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000028' ,N'ganz Aventurien zwischen Riva und Mengbilla')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000029' ,N'ganz Mittelaventurien zwischen Trallop und Punin')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000030' ,N'ganz Nordaventurien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000031' ,N'im Gebiet um Vallusa')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000032' ,N'Garetien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000033' ,N'keine natürlichen Vorkommen bekannt')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000034' ,N'Randgebiete der Khôm')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000035' ,N'Küste um die Elburische Halbinsel')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000036' ,N'Almada')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000037' ,N'Maraskan')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000038' ,N'aranische Küste')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000039' ,N'um Khunchom')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000040' ,N'Maraskankette')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000041' ,N'Unauer Berge')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000042' ,N'Mittelaventurien von Garetien bis zu den Echsensümpfen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000043' ,N'Mittelaventurien zwischen Lowangen und Selem')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000044' ,N'Mittelaventurien zwischen Salamandersteine und Ongalo-Bergen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000045' ,N'Moghulat Oron')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000046' ,N'Neunaugensee')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000047' ,N'Mittelaventurien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000048' ,N'Liebliches Feld')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000049' ,N'aranisches Hochland')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000050' ,N'Nordaventurien nördlich der Linie Thorwal-Festum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000051' ,N'Nordaventurien nördlich einer Linie Nostria-Vallusa')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000052' ,N'nördlich der Khôm')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000053' ,N'Nordostaventurien von der Letta bis zur Tobimora')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000054' ,N'Nordmarken')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000055' ,N'am Golf von Perricum')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000056' ,N'Orkland')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000057' ,N'Finsterkamm')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000058' ,N'Gjalsker Öden')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000059' ,N'Svelltsümpfe')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000060' ,N'Svellttal')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000061' ,N'Ostaventurien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000062' ,N'östliche Khôm')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000063' ,N'Pailos')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000064' ,N'Randgebiete der Gorischen Wüste')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000065' ,N'Raschtulswall')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000066' ,N'Schwarz-Tobrien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000067' ,N'Xeraanien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000068' ,N'Steineichenwald')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000069' ,N'Südaventurien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000070' ,N'Südaventurien ab der Linie Neetha-Thalusa')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000071' ,N'Südaventurien südlich von Mengbilla')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000073' ,N'südlich der Eternen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000074' ,N'südlich des Loch Harodrôl')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000075' ,N'südliche Randgebiete der Khôm')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000076' ,N'südliches Maraskan und die umliegenden Inseln')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000077' ,N'Südwestaventurien südlich von Mengbilla')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000078' ,N'zwischen Donnerbach und Rashdul')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000079' ,N'zwischen Riva und Neetha')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000080' ,N'Grüne Ebene')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000081' ,N'Wälder um die Salamandersteine')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000082' ,N'Mysobgebiet')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000083' ,N'Waldinseln von Ost-Altoum bis Ibonka')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000084' ,N'Warunkei')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000085' ,N'Westküste Aventuriens')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000086' ,N'südöstlich des Regengebirges zwischen Brabak und Mirham')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000087' ,N'westliches Regengebirge')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000138' ,N'Westküste Aventuriens südlich von Drôl')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000139' ,N'Zyklopeninseln')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000140' ,N'Mhanadistan')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000141' ,N'Perlenmeer zwischen Selem und Charypso')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000145' ,N'Nostria')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000146' ,N'Souram')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000147' ,N'Nikkali')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000148' ,N'Gorien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000149' ,N'Aranien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000150' ,N'Thalusien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000151' ,N'Szintotal')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000152' ,N'Andergast')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000153' ,N'Koschberge')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000154' ,N'Eisenwald')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000155' ,N'Thorwal')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000156' ,N'Echsensümpfe')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000157' ,N'am Yslisee')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000158' ,N'Regengebirge')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000159' ,N'Wälder um das Regengebirge')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000160' ,N'Waldinseln')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000161' ,N'Khôm')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000162' ,N'Höhen des Raschtulswalls')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000163' ,N'Höhen des Khoramgebirges')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000164' ,N'Shadif')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000165' ,N'westlich des Regengebirges ab Höhe Mengbilla')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000166' ,N'Darpatien')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000167' ,N'Warunk')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000168' ,N'Beilunk')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000169' ,N'Totenmoor')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000170' ,N'Einsiedlersee')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000000171' ,N'von der Sphärenkraft durchdrungene Gewässer')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030001' ,N'ganz Myranor')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030002' ,N'Tharamans Rippen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030003' ,N'Louranath')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030004' ,N'Karonius')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030005' ,N'Centralis')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030006' ,N'Cranarenius')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030007' ,N'Baan-Bashur')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030008' ,N'Berge von Xarxaron')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030009' ,N'Cedaria-Berge')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030010' ,N'Gyldraland')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030011' ,N'vom Großem Orismani bis zum Thalassion')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030012' ,N'Makshapuram')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030013' ,N'Randgebiete nördlich des Meeres der Schwimmenden Inseln')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030089' ,N'Aschenwald')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030090' ,N'Arhtax')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030091' ,N'Wolkenkämme')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030092' ,N'Binnengewässer zwischen Orismani-Fällen und Valantischem Meer (nicht Grüner Orismani)')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030095' ,N'Tharpura')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030096' ,N'Tharpurische Thalassionküste')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030097' ,N'Era´Sumu')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030100' ,N'gemäßigte nördliche Breiten bis zum Großen Orismani')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030101' ,N'gemäßigte nördliche Breiten')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030102' ,N'Große Steppe westlich des Orismani')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030103' ,N'Südliches Imperium')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030104' ,N'Gemäßigtes Klima')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030105' ,N'Tharpura')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030106' ,N'tropischer Teil des Thalassions')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030107' ,N'Mittlere und nördliche Breiten')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030108' ,N'Narkramar')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030109' ,N'Brillantsteine')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030111' ,N'Nebelwald')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030113' ,N'nördlich einer Linie Balan Cantara-Daranel bis zur winterlichen Eisgrenze')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030114' ,N'nördliche Wälder, im Osten bis an den Rand Valantias und Mayenios')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030115' ,N'im Norden')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030117' ,N'hoch gelegene Dschungelgebiete')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030118' ,N'Süden der nördlichen Wälder bis nach Cantera')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030119' ,N'südlich von Ochobenius bis nach Valantia')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030121' ,N'Subtropen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030124' ,N'Alamar')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030125' ,N'Gebirge des Nordens bis nach Karonius')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030127' ,N'Mayenios')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030129' ,N'Ufergebiete des Meeres der Schwimmenden Inseln')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030130' ,N'Wald von Amaunath')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030131' ,N'Wälder des nördlichen Myranor bis zur großen Steppe')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030134' ,N'Xarxaron')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030136' ,N'zwischen den Polarregionen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030137' ,N'zwischen Frostweste, den Bergen von Skiresis und Nitharus-Bergen')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030142' ,N'Meer der Schwimmenden Inseln')
GO

INSERT INTO [Gebiet] (  [GebietGUID],  [Name]) 
 VALUES ('00000000-0000-0000-00f9-000000030143' ,N'Tropen')
GO


/* Handelsgut */

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003645' ,N'Bleichmohn' ,NULL ,N'Samenkapsel' ,N'Pflanzen/Kräuter' ,N'Nutzpflanze, Droge' ,N'Schmerzmittel' ,N'ZBA 252' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003646' ,N'Alazeerenbaumblätter' ,NULL ,N'Portion' ,N'Pflanzen/Kräuter' ,N'Heilpflanze' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003647' ,N'Alazeerenbaum-Blattabsud' ,NULL ,NULL ,N'Pflanzen/Kräuter' ,N'Heilpflanze' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003648' ,N'Aurinde/Sonnenkraut' ,NULL ,N'Portion' ,N'Pflanzen/Kräuter' ,N'Heilpflanze' ,N'für Amaunir zur Berkämpfung der Kahle' ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003649' ,N'Balburri-Perlen' ,NULL ,N'Portion' ,N'Pflanzen/Kräuter' ,N'Heilpflanze' ,N'Preis ist irrelevant, da nicht handelbar' ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003650' ,N'Caranuss (reif)' ,NULL ,N'Portion' ,N'Pflanzen/Kräuter' ,N'Droge' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003651' ,N'Cuina-Droge' ,NULL ,N'Portion' ,N'Pflanzen/Kräuter' ,N'Droge' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003653' ,N'Feuerposaune' ,NULL ,N'Portion' ,N'Pflanzen/Kräuter' ,N'Droge' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003654' ,N'Mantigora' ,NULL ,N'Wurzel' ,N'Pflanzen/Kräuter' ,N'Giftpflanze/ Nutzpflanze' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003655' ,N'Remembalia' ,NULL ,N'kleine Pflanze' ,N'Pflanzen/Kräuter' ,N'übernatürliche Pflanze' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003656' ,N'Roter Bambus' ,NULL ,N'Stange' ,N'Pflanzen/Kräuter' ,N'Nutzpflanze' ,N'Probe auf Holzbearbeitung nach Meisterentscheid' ,N'Myranor 262f' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003657' ,N'Schwesterlein' ,NULL ,N'Pflanze' ,N'Pflanzen/Kräuter' ,N'übernatürliche Pflanze' ,NULL ,N'Myranor 263' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003658' ,N'Szata''te-Saft' ,NULL ,N'Saft eines Stachels' ,N'Pflanzen/Kräuter' ,N'Giftpflanze' ,NULL ,N'Myranor 263' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003659' ,N'Tarnblatt' ,NULL ,N'Portion' ,N'Nutzpflanze' ,NULL ,N'Reicht für eine Gesichtsmaske.' ,N'Myranor 263' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003660' ,N'Traumpilz' ,NULL ,N'Portion' ,N'Pflanzen/Kräuter' ,N'Giftpflanze, Gefährliche Pflanze' ,N'je nach Verarbeitung: vom leichten Rauschmittel bis zum tödlichen Atemgift oder stark suchterzeugenden Droge' ,N'Myranor 263' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003661' ,N'Warakwurz' ,1 ,N'Wurzel' ,N'Pflanzen/Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor 263' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003662' ,N'Brajankelch (Blüte)' ,NULL ,NULL ,N'Pflanzen/Kräuter' ,N'Nutzpflanze' ,N'Weiterverarbeitung zu Kleidung möglich.' ,N'Uhrwerk3 34' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003663' ,N'Ilmenblatt (Blätter und Blüten)' ,0.1 ,N'1/10 Unzen' ,N'Pflanzen/Kräuter' ,N'Nutzpflanze / Droge' ,NULL ,N'ZBA 241' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003664' ,N'Menchalsaft' ,NULL ,N'halbes Maß' ,N'Pflanzen/Kräuter' ,N'Nutzpflanze' ,NULL ,N'ZBA 249' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003665' ,N'Nothilfblatt' ,NULL ,N'Blätter' ,N'Pflanzen/Kräuter' ,N'Heilpflanze' ,N'heilt Brandwunden' ,N'ZBA 256' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003666' ,N'Sansaro' ,20 ,N'Pflanze' ,N'Pflanzen/Kräuter' ,N'Nutzpflanze' ,N'Mittel gegen Kerkersieche' ,N'ZBA 262f' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003667' ,N'Satuariensbusch-Saft' ,NULL ,N'Flux' ,N'Pflanzen/Kräuter' ,N'Heilpflanze / Übernatürliche Pflanze' ,NULL ,N'ZBA 263' ,3)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003668' ,N'Yaganöl' ,NULL ,N'Flux' ,N'Pflanzen/Kräuter' ,N'Nutzpflanze' ,NULL ,N'ZBA 275' ,2)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003669' ,N'Feuerposaunenabsud' ,NULL ,N'Dosis' ,N'Pflanzen/Kräuter' ,N'Droge' ,NULL ,N'Myranor 262' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003670' ,N'Sandklauenholz (hart)' ,NULL ,N'Stein' ,N'Pflanze' ,N'Wildpflanze/Nutzpflanze' ,N'Bei Waffen und Rüstungen aus dem Holz steigt der Preis x4, aber nicht unter 10 Au.' ,N'Uhrwerk4 40, 41, 42' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003671' ,N'Tyrannenbaumflaum' ,1 ,N'Unze' ,N'Pflanze' ,N'Übernatürliche Pflanze' ,NULL ,N'Uhrwerk4 44f' ,NULL)
GO

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003672' ,N'Waldkönigflaum' ,1 ,N'Unze' ,N'Pflanze' ,N'Übernatürliche Pflanze' ,NULL ,N'Uhrwerk4 44f' ,NULL)
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'Der Preis ist Verhandlungssache, nur bei freundlich gesinnten Hexen erhältlich.' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000479'
GO

UPDATE [Handelsgut] SET [Gewicht] = 0.008 ,[Literatur] = N'WdA 190 / WdA-Preisliste 1 / Myranor 281' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000387'
GO

UPDATE [Handelsgut] SET [Name] = N'Satuariensbusch (Holz)' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000696'
GO

UPDATE [Handelsgut] SET [Name] = N'Alveranienblatt' ,[Bemerkung] = N'kann nicht verkauft werden, da alle Rituale voraussetzen, dass die Blätter selbst gepflückt wurden' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000947'
GO

UPDATE [Handelsgut] SET [Name] = N'Arganstrauchwurzel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000948'
GO

UPDATE [Handelsgut] SET [Name] = N'Atan-Kiefer-Rinde' ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000949'
GO

UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000951'
GO

UPDATE [Handelsgut] SET [Name] = N'Blutblattzweig' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000954'
GO

UPDATE [Handelsgut] SET [Name] = N'Boronienblüte' ,[ME] = N'Blüte' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000955'
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'Gewürz' ,[Literatur] = N'ZBA 252f, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000958'
GO

UPDATE [Handelsgut] SET [Name] = N'Carlogblüte' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000959'
GO

UPDATE [Handelsgut] SET [Name] = N'Chonchinisblatt' ,[ME] = N'Blätter' ,[Verpackungseinheit] = 5 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000961'
GO

UPDATE [Handelsgut] SET [Name] = N'Donfstängel' ,[ME] = N'Stängel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000964'
GO

UPDATE [Handelsgut] SET [Name] = N'Dornrosenblüten' ,[Gewicht] = 0.5 ,[ME] = N'Blüte' ,[Verpackungseinheit] = 80 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000965'
GO

UPDATE [Handelsgut] SET [Name] = N'Egelschreckblätter' ,[ME] = N'Blätter' ,[Verpackungseinheit] = 5 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000967'
GO

UPDATE [Handelsgut] SET [Name] = N'Eitriger Krötenschemel (Pilzhaut)' ,[Literatur] = N'ZBA 236, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000968'
GO

UPDATE [Handelsgut] SET [Name] = N'Feuermoos und Efferdmoos (Saft)' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000970'
GO

UPDATE [Handelsgut] SET [Tags] = N'Nutzpflanze / Heilpflanze, Alchimistische Stoffe' ,[Literatur] = N'ZBA 237 /SRD 119' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000971'
GO

UPDATE [Handelsgut] SET [ME] = N'Triebe' ,[Verpackungseinheit] = 7 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000972'
GO

UPDATE [Handelsgut] SET [Tags] = N'Giftpflanze / Nutzpflanze, Einnahmegift' ,[Literatur] = N'ZBA 253, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000974'
GO

UPDATE [Handelsgut] SET [Name] = N'Grauer Mohn (getrocknete Blüten)' ,[ME] = N'Blüten' ,[Tags] = N'Giftpflanze / Nutzpflanze, Einnahmegift' ,[Verpackungseinheit] = 3 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000975'
GO

UPDATE [Handelsgut] SET [ME] = N'Blätter' ,[Verpackungseinheit] = 8 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000977'
GO

UPDATE [Handelsgut] SET [ME] = N'Wurzel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000979'
GO

UPDATE [Handelsgut] SET [Name] = N'Hollbeerblätter' ,[ME] = N'Blätter' ,[Bemerkung] = N'am Fundort häufig und wertlos, andernorts unbekannt und wertlos' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000980'
GO

UPDATE [Handelsgut] SET [Gewicht] = 20 ,[ME] = N'halbe Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000981'
GO

UPDATE [Handelsgut] SET [Name] = N'Horuschenkern' ,[Tags] = N'Nutzpflanze / Giftpflanze, Einnahmegift' ,[Literatur] = N'ZBA 240f' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000982'
GO

UPDATE [Handelsgut] SET [ME] = N'Wurzel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000985'
GO

UPDATE [Handelsgut] SET [Name] = N'Kairanhalm' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000986'
GO

UPDATE [Handelsgut] SET [Name] = N'Kajuboknospe' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000987'
GO

UPDATE [Handelsgut] SET [Name] = N'Klippenzahnstängel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000989'
GO

UPDATE [Handelsgut] SET [Name] = N'Kukukablatt' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000990'
GO

UPDATE [Handelsgut] SET [Name] = N'Libellengrasfrucht' ,[ME] = N'Frucht' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000991'
GO

UPDATE [Handelsgut] SET [Name] = N'Lichtneblersporen' ,[ME] = N'Skrupel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000992'
GO

UPDATE [Handelsgut] SET [Name] = N'Färberlotos' ,[ME] = N'Blüten' ,[Verpackungseinheit] = 5 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000993'
GO

UPDATE [Handelsgut] SET [Name] = N'Lulanieblüte' ,[ME] = N'Blüten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000994'
GO

UPDATE [Handelsgut] SET [Name] = N'Malomisblüte' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000996'
GO

UPDATE [Handelsgut] SET [Name] = N'Menchal-Kaktus' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000997'
GO

UPDATE [Handelsgut] SET [Tags] = N'Giftpflanze, Kontaktgift' ,[Literatur] = N'ZBA 255, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001005'
GO

UPDATE [Handelsgut] SET [Name] = N'Neckerkrautblatt' ,[ME] = N'Blätter' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001006'
GO

UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein Liane' ,[Verpackungseinheit] = 10 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001008'
GO

UPDATE [Handelsgut] SET [Name] = N'Orkland-Bovist' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001009'
GO

UPDATE [Handelsgut] SET [Tags] = N'Nutzpflanze, Beleuchtung' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001011'
GO

UPDATE [Handelsgut] SET [Name] = N'Purpurmohn' ,[Tags] = N'Übernatürliche Pflanze, Einnahmegift' ,[Bemerkung] = N'hochgradig suchterzeugend, Trugbilder und Glaubensverfall' ,[Literatur] = N'ZBA 253, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001013'
GO

UPDATE [Handelsgut] SET [Name] = N'Quinja-Beeren' ,[Literatur] = N'ZBA 259, 260' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001015'
GO

UPDATE [Handelsgut] SET [Name] = N'Rahjalieb-Blätter' ,[ME] = N'Blätter' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001016'
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'Preis je nachdem, wie viel Verkäufer und Käufer von der Dosis und Wirkung des Pilzes wissen und wie sehr sie ihn benötigen.' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001017'
GO

UPDATE [Handelsgut] SET [Name] = N'Roter Drachenschlund-Blätter' ,[ME] = N'Blätter' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001020'
GO

UPDATE [Handelsgut] SET [ME] = N'Stängel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001067'
GO

UPDATE [Handelsgut] SET [ME] = N'Anwendungen' ,[Verpackungseinheit] = 5 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001071'
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'Kraft- und ausdauersteigernd' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001074'
GO

UPDATE [Handelsgut] SET [Name] = N'Ilmenblatt (Harz)' ,[Gewicht] = 0.1 ,[ME] = N'1/10 Unzen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001078'
GO

UPDATE [Handelsgut] SET [Name] = N'Ilmenblatt (getrocknet und gehäckselt)' ,[Gewicht] = 0.1 ,[ME] = N'1/10 Unzen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001079'
GO

UPDATE [Handelsgut] SET [Name] = N'Libellengrasfrucht (getrocknet)' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001085'
GO

UPDATE [Handelsgut] SET [Kategorie] = N'Gifte' ,[Tags] = N'Lebensmittel, Einnahmegift' ,[Bemerkung] = N'Giftstufe 1. zuckersüß, aber tödlich in Verbindung mit Alkohol' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001093'
GO

UPDATE [Handelsgut] SET [ME] = N'Unze' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001097'
GO

UPDATE [Handelsgut] SET [ME] = N'Beeren' ,[Verpackungseinheit] = 3 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001102'
GO

UPDATE [Handelsgut] SET [Name] = N'SchwarmschwammSamenkörper' ,[ME] = N'Samenkörper' ,[Tags] = N'Übernatürliche Pflanze, Einnahmegift' ,[Bemerkung] = N'Bis zu 13 Dämonenkronen für einen Samenkörper (als Paraphernalium).' ,[Literatur] = N'ZBA 265, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001021'
GO

UPDATE [Handelsgut] SET [Literatur] = N'ZBA 253f' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001023'
GO

UPDATE [Handelsgut] SET [Name] = N'Seelenhauchblüte' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001024'
GO

UPDATE [Handelsgut] SET [Name] = N'Shurinknolle' ,[Tags] = N'Giftpflanze, Einnahmegift' ,[Literatur] = N'ZBA 267, 287' ,[Verpackungseinheit] = 3 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001025'
GO

UPDATE [Handelsgut] SET [ME] = N'Flechten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001027'
GO

UPDATE [Handelsgut] SET [Tags] = N'Giftpflanze, Kontaktgift, Einnahmegift' ,[Literatur] = N'ZBA 268, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001028'
GO

UPDATE [Handelsgut] SET [Name] = N'Thonnysblatt' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001030'
GO

UPDATE [Handelsgut] SET [Name] = N'Tigermohn' ,[Bemerkung] = N'Preis pro Samenkapsel, Beruhigungsmittel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001031'
GO

UPDATE [Handelsgut] SET [Tags] = N'Übernatürliche Pflanze, Einnahmegift' ,[Bemerkung] = NULL ,[Literatur] = N'ZBA 270, 271, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001034'
GO

UPDATE [Handelsgut] SET [Name] = N'Ulmenwürgerblüten' ,[ME] = N'Blüten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001035'
GO

UPDATE [Handelsgut] SET [ME] = N'Beeren' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001036'
GO

UPDATE [Handelsgut] SET [Tags] = N'Giftpflanze, Einnahmegift' ,[Literatur] = N'ZBA 272, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001037'
GO

UPDATE [Handelsgut] SET [ME] = N'Blüten' ,[Tags] = N'Nutzpflanze, Atemgift' ,[Literatur] = N'ZBA 273, 287' ,[Verpackungseinheit] = 10 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001040'
GO

UPDATE [Handelsgut] SET [Tags] = N'Nutzpflanze, Einnahmegift' ,[Literatur] = N'ZBA 273, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001041'
GO

UPDATE [Handelsgut] SET [Name] = N'Wirselkrautblatt' ,[ME] = N'Blätter' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001045'
GO

UPDATE [Handelsgut] SET [Name] = N'Zithabarblatt' ,[ME] = N'Blätter' ,[Verpackungseinheit] = 3 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001047'
GO

UPDATE [Handelsgut] SET [Literatur] = N'ZBA 275f / SRD 275, 276 / H&K 159' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001048'
GO

UPDATE [Handelsgut] SET [ME] = N'Stängel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001049'
GO

UPDATE [Handelsgut] SET [ME] = N'Unze' ,[Tags] = N'Konservierungsmittel, Alchimistische Stoffe' ,[Bemerkung] = NULL ,[Literatur] = N'ZBA 257 / WdA 206 / SRD 121' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001111'
GO

UPDATE [Handelsgut] SET [Tags] = N'Rauschmittel' ,[Literatur] = N'H&K 142 / ZBA 241' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001521'
GO

UPDATE [Handelsgut] SET [Tags] = N'maraskanisch, Gewürz, Delikatesse, Heilmittel' ,[Bemerkung] = N'Haltbarkeit je nach Behandlung bis zu 5 Jahren' ,[Literatur] = N'ZBA 261' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001593'
GO

UPDATE [Handelsgut] SET [Name] = N'Yagan-Nuss-Kern' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001824'
GO

UPDATE [Handelsgut] SET [Name] = N'Färberlotos (gelb, blau, rosa und rot färben)' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002065'
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'Preis variabel, , beständige Farben für Stoffe' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002084'
GO

UPDATE [Handelsgut] SET [Name] = N'Alazeerenbaum-Stärkungselixier' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002506'
GO

UPDATE [Handelsgut] SET [Name] = N'Caranus (unreif mit Kalk)' ,[Kategorie] = N'Pflanzen/Kräuter' ,[Tags] = N'Droge' ,[Bemerkung] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002509'
GO

UPDATE [Handelsgut] SET [Name] = N'Cuina-Blüte' ,[ME] = N'Stück' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002510'
GO

UPDATE [Handelsgut] SET [Name] = N'Szata''te-Giftpfeil' ,[ME] = N'präparierter Blasrohrpfeil' ,[Kategorie] = N'Gift' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002516'
GO

UPDATE [Handelsgut] SET [Name] = N'Warakwurz-Antidot' ,[ME] = N'Portion' ,[Kategorie] = N'Heilmittel' ,[Tags] = N'Gegengift' ,[Bemerkung] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002519'
GO

UPDATE [Handelsgut] SET [ME] = N'Uncia' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002606'
GO

UPDATE [Handelsgut] SET [ME] = N'Uncia' ,[Literatur] = N'Myranor 263, 283' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002754'
GO

UPDATE [Handelsgut] SET [Name] = N'Yagan-Nuss' ,[ME] = N'Nuss' ,[Bemerkung] = N'erhöht Ausdauer. Eine Nuss enthält 2 Flux Yaganöl.' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003355'
GO

UPDATE [Handelsgut] SET [Name] = N'Nothilfblüte' ,[ME] = N'Blüten' ,[Tags] = N'Heilpflanze' ,[Bemerkung] = N'heilt Brandwunden' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003356'
GO

UPDATE [Handelsgut] SET [Name] = N'Satuariensbusch (Blätter, Blüten und Früchte)' ,[Bemerkung] = N'schützt vor Wundfieber' ,[Verpackungseinheit] = 10 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003357'
GO

UPDATE [Handelsgut] SET [Name] = N'Schlangenzünglein-Saft' ,[Bemerkung] = N'Saft einer Pflanze, zeigt Magie an' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003359'
GO

UPDATE [Handelsgut] SET [Tags] = N'Giftpflanze / Heilpflanze, Einnahmegift' ,[Literatur] = N'ZBA 240, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003425'
GO

UPDATE [Handelsgut] SET [Name] = N'Horuschenschote' ,[ME] = N'Schote' ,[Kategorie] = N'Pflanzen/Kräuter' ,[Tags] = N'Nutzpflanze / Giftpflanze, Einnahmegift' ,[Literatur] = N'ZBA 240f, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003426'
GO

UPDATE [Handelsgut] SET [Name] = N'Grauer Lotos' ,[ME] = N'Blüte' ,[Tags] = N'Atemgift, Übernatürliche Pflanze' ,[Literatur] = N'ZBA 246f, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003432'
GO

UPDATE [Handelsgut] SET [Name] = N'Cheria-Kaktusfleisch' ,[Gewicht] = 1 ,[ME] = N'Unze' ,[Literatur] = N'ZBA 232f, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003408'
GO

UPDATE [Handelsgut] SET [Name] = N'Cheria-Kaktusstacheln' ,[ME] = N'Stacheln' ,[Literatur] = N'ZBA 232f, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003409'
GO

UPDATE [Handelsgut] SET [Name] = N'Disdychondablatt' ,[ME] = N'Blatt' ,[Bemerkung] = N'giftige Nesselblätter' ,[Literatur] = N'ZBA 233f, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003410'
GO

UPDATE [Handelsgut] SET [Name] = N'Schwarzer Lotos' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003433'
GO

UPDATE [Handelsgut] SET [Name] = N'Boronsschlingenpollen' ,[Bemerkung] = N'Substitut im Schlafgift' ,[Literatur] = N'ZBA 231f, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003405'
GO

UPDATE [Handelsgut] SET [Name] = N'Purpurner Lotos' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003434'
GO

UPDATE [Handelsgut] SET [Name] = N'Weißer Lotos' ,[Literatur] = N'ZBA 247, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003435'
GO

UPDATE [Handelsgut] SET [ME] = N'Sporen' ,[Bemerkung] = N'' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003449'
GO

UPDATE [Handelsgut] SET [ME] = N'Blüten' ,[Bemerkung] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003456'
GO

UPDATE [Handelsgut] SET [ME] = N'Pilze' ,[Kategorie] = N'Giftpflanze' ,[Bemerkung] = NULL ,[Literatur] = N'ZBA 264, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003457'
GO

UPDATE [Handelsgut] SET [ME] = N'Traube' ,[Kategorie] = N'Übernatürliche Pflanze' ,[Bemerkung] = N'Eine Traube hat 7W6 Beeren. In zwölfgöttlichen Landen steht die Hinrichtung auf den Besitz.' ,[Literatur] = N'ZBA 266, 287' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003460'
GO

UPDATE [Handelsgut] SET [Name] = N'Witwenmacherextrakt' ,[ME] = N'Portion' ,[Kategorie] = N'Pflanzen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003549'
GO

UPDATE [Handelsgut] SET [Name] = N'Wolfsblattsamen' ,[ME] = N'Samen' ,[Bemerkung] = N'Preis gilt für denn Winter und Frühjahr; im Spätsommer mehr. 6 Samen sind eine Portion.' ,[Verpackungseinheit] = 6 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003550'
GO

UPDATE [Handelsgut] SET [ME] = N'Portion' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003555'
GO

UPDATE [Handelsgut] SET [ME] = N'Portion Sporen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003529'
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'nur magisch länger haltbar' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003530'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003532'
GO

UPDATE [Handelsgut] SET [Name] = N'Goldhautsaft' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003536'
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'Ein Samen ergibt eine Portion Pulver.' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003537'
GO

UPDATE [Handelsgut] SET [ME] = N'Portion' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003538'
GO

UPDATE [Handelsgut] SET [Name] = N'Laufkraut-Blütenblätter' ,[ME] = N'Portion' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003539'
GO

UPDATE [Handelsgut] SET [Name] = N'Lebensmoostriebe' ,[ME] = N'Triebe' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003540'
GO

UPDATE [Handelsgut] SET [ME] = N'Blüten' ,[Verpackungseinheit] = 12 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003542'
GO

UPDATE [Handelsgut] SET [Name] = N'Sandklauenholz (weich)' ,[Kategorie] = N'Pflanze' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003543'
GO

UPDATE [Handelsgut] SET [Name] = N'Satyarenbaumsud ' ,[ME] = N'Portion' ,[Kategorie] = N'Pflanzen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003544'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Kategorie] = N'Pflanzen' ,[Bemerkung] = N'reicht für eine Mahlzeit' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003545'
GO

UPDATE [Handelsgut] SET [Name] = N'Tanzbuschgift' ,[Kategorie] = N'Gift' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003546'
GO

UPDATE [Handelsgut] SET [Name] = N'Tyrannenbaumblüte' ,[Gewicht] = 1 ,[Kategorie] = N'Pflanze' ,[Tags] = N'Übernatürliche Pflanze' ,[Bemerkung] = NULL ,[Literatur] = N'Uhrwerk4 44f' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003547'
GO

UPDATE [Handelsgut] SET [Name] = N'Waldkönigblüte' ,[Gewicht] = 1 ,[Kategorie] = N'Pflanze' ,[Bemerkung] = NULL ,[Literatur] = N'Uhrwerk4 44f' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003548'
GO

UPDATE [Handelsgut] SET [Name] = N'Azulie/Neristu-Rose' ,[ME] = N'Pflanze' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003580'
GO

UPDATE [Handelsgut] SET [Name] = N'Belyabels Schleier-Gift' ,[Kategorie] = N'Gift' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003581'
GO

UPDATE [Handelsgut] SET [Name] = N'Cassava-Knolle' ,[Kategorie] = N'Nutzpflanze' ,[Literatur] = N'Myranor 260' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003583'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Literatur] = N'Myranor 264' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003584'
GO

UPDATE [Handelsgut] SET [Name] = N'Geistblüte' ,[ME] = N'Pflanze' ,[Tags] = N'Übernatürliche Pflanze' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003585'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003586'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Literatur] = N'Myranor 264' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003587'
GO

UPDATE [Handelsgut] SET [Name] = N'Himmelszederholz' ,[ME] = N'Stein' ,[Kategorie] = N'Pflanze' ,[Literatur] = N'Myranor 260' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003588'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Literatur] = N'Myranor 264' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003589'
GO

UPDATE [Handelsgut] SET [Name] = N'Maulbaumholz' ,[ME] = N'Stein' ,[Kategorie] = N'Pflanze' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003591'
GO

UPDATE [Handelsgut] SET [Name] = N'Neretonienholz' ,[ME] = N'Stein' ,[Kategorie] = N'Pflanze' ,[Literatur] = N'Myranor 260' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003592'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Literatur] = N'Myranor 264' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003593'
GO

UPDATE [Handelsgut] SET [Name] = N'Pfeilsamen ' ,[ME] = N'Samen' ,[Kategorie] = N'Gift' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003594'
GO

UPDATE [Handelsgut] SET [Name] = N'Phrenophoren-Kaktus-Knolle ' ,[ME] = N'Knolle' ,[Kategorie] = N'Pflanzen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003595'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Literatur] = N'Myranor 264' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003596'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003597'
GO

UPDATE [Handelsgut] SET [Name] = N'Sternapfel' ,[Kategorie] = N'Pflanze' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003598'
GO

UPDATE [Handelsgut] SET [Name] = N'Tarnai' ,[ME] = N'Portion' ,[Kategorie] = N'Pflanzen/Kräuter' ,[Bemerkung] = N'Samen der Tarnaille. Geist klärendes Niespulver.' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003599'
GO

UPDATE [Handelsgut] SET [Name] = N'Therkalholz' ,[ME] = N'Stein' ,[Kategorie] = N'Pflanze' ,[Literatur] = N'Myranor 260' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003600'
GO

UPDATE [Handelsgut] SET [ME] = N'Pflanze' ,[Kategorie] = N'Pflanzen' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003601'
GO

UPDATE [Handelsgut] SET [Kategorie] = N'Pflanzen' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003602'
GO

UPDATE [Handelsgut] SET [Kategorie] = N'Pflanzen' ,[Literatur] = N'Myranor 263' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003603'
GO

UPDATE [Handelsgut] SET [Name] = N'Aschenbusch-Gift' ,[Kategorie] = N'Gift' ,[Tags] = N'Giftpflanze, Kontaktgift' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003526'
GO

UPDATE [Handelsgut] SET [Bemerkung] = N'Preis außerhalb der Erntezeit 1 bis 2 Ag, wobei Baramunen auch deutlich mehr zahlen oder sie mit Gewalt an sich bringen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003527'
GO

UPDATE [Handelsgut] SET [Gewicht] = 2 ,[Bemerkung] = N'Preis: 5 Ag unter Wasser, an Land ab 8 Ag pro Portion' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003528'
GO

UPDATE [Handelsgut] SET [Name] = N'Atholisrinde' ,[Kategorie] = N'Pflanzen' ,[Literatur] = N'Myranor 260' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003578'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000036'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000075'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000359'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000952'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000956'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000960'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000963'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000966'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000973'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000976'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000998'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001003'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001033'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001038'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001044'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001046'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001485'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001598'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001669'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001683'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002423'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002505'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002507'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002508'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002511'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002512'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002513'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002514'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002515'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002517'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002518'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003358'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003416'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003439'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003441'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003442'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003443'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003458'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003463'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003468'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003473'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003474'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003475'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003533'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003582'
GO

DELETE FROM [Handelsgut] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003579'
GO



/* Handelsgut_Setting */

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000011' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000063' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000387' ,'00000000-0000-0000-5e77-000000000003' ,N'0,36' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000514' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000516' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000958' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000974' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000975' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000993' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000996' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001011' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001012' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001013' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001022' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001023' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001031' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001042' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001043' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001053' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001057' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001059' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001067' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001076' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001088' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001094' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001097' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001101' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001102' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001103' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001107' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001108' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001111' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001119' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001123' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001603' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001613' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000002065' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000002084' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003356' ,'00000000-0000-0000-5e77-000000000002' ,N'30' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003431' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003432' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003433' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003434' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003435' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003645' ,'00000000-0000-0000-5e77-000000000001' ,N'70' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003645' ,'00000000-0000-0000-5e77-000000000002' ,N'70' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003645' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003646' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003647' ,'00000000-0000-0000-5e77-000000000003' ,N'12' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003648' ,'00000000-0000-0000-5e77-000000000003' ,N'12' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003649' ,'00000000-0000-0000-5e77-000000000003' ,N'irrelevant, da nicht handelbar' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003650' ,'00000000-0000-0000-5e77-000000000003' ,N'5' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003651' ,'00000000-0000-0000-5e77-000000000003' ,N'6' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003653' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003654' ,'00000000-0000-0000-5e77-000000000003' ,N'12' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003655' ,'00000000-0000-0000-5e77-000000000003' ,N'1' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003656' ,'00000000-0000-0000-5e77-000000000003' ,N'0,4' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003657' ,'00000000-0000-0000-5e77-000000000003' ,N'250+' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003658' ,'00000000-0000-0000-5e77-000000000003' ,N'80' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003659' ,'00000000-0000-0000-5e77-000000000003' ,N'20-50' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003660' ,'00000000-0000-0000-5e77-000000000003' ,N'8-100' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003661' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003663' ,'00000000-0000-0000-5e77-000000000001' ,N'2' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003663' ,'00000000-0000-0000-5e77-000000000002' ,N'2' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003664' ,'00000000-0000-0000-5e77-000000000001' ,N'50' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003664' ,'00000000-0000-0000-5e77-000000000002' ,N'50' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003665' ,'00000000-0000-0000-5e77-000000000001' ,N'13,33' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003665' ,'00000000-0000-0000-5e77-000000000002' ,N'13,33' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003666' ,'00000000-0000-0000-5e77-000000000001' ,N'0,5' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003666' ,'00000000-0000-0000-5e77-000000000002' ,N'0,5' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003666' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003667' ,'00000000-0000-0000-5e77-000000000001' ,N'0,167' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003667' ,'00000000-0000-0000-5e77-000000000002' ,N'0,167' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003668' ,'00000000-0000-0000-5e77-000000000001' ,N'15' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003668' ,'00000000-0000-0000-5e77-000000000002' ,N'15' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003669' ,'00000000-0000-0000-5e77-000000000003' ,N'50' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003670' ,'00000000-0000-0000-5e77-000000000003' ,N'50' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003671' ,'00000000-0000-0000-5e77-000000000003' ,N'10' ,NULL)
GO

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003672' ,'00000000-0000-0000-5e77-000000000003' ,N'10' ,NULL)
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000479' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'2,14' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000951' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,8' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000961' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,000625' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000965' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,16' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000967' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,857' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000972' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'5' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000975' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000977' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'5' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000981' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,2-1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000993' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'1,2' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001008' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001017' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'16' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001071' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001093' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'13,33' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001102' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001021' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'26,667' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001025' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'10' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001028' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,4' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001029' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'4' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001036' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'2' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001037' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001040' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,0667' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001047' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'bis zu 100' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001593' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'3' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002509' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'60' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002519' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'2,14' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000951' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,8' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000961' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,000625' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000965' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,16' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000967' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,857' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000972' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,857' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000972' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'5' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000975' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000977' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'5' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000981' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,2-1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000993' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'1,2' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001008' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001017' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001021' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'26,667' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001025' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'10' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001028' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,4' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001029' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'4' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001036' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'2' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001037' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001040' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,0667' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001047' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'16' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001071' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'16' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001071' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001093' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'13,33' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001102' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'30' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003356' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,01' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003357' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003408' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'30' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003410' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'10' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003426' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'5' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003432' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'15' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003456' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'5' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003457' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'10' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003460' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'5-8+' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003528' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,1667' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003542' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,8333' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003550' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003578' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003580' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003581' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = N'0,07' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003583' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003584' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003585' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003586' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003587' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003588' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003589' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003590' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003591' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003592' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003593' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003594' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003595' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003596' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003597' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003598' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003599' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003600' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003601' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003602' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

UPDATE [Handelsgut_Setting] SET [Preis] = NULL WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003603' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000036' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000075' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000359' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000952' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000956' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000960' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000963' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000966' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000973' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000976' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000998' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001003' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001033' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001038' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001044' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001046' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001485' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001598' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001669' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001683' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002423' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002505' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002507' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002508' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002511' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002512' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002513' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002514' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002515' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002517' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002518' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000036' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000075' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000359' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000952' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000956' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000960' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000963' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000966' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000973' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000976' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000998' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001003' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001033' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001038' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001044' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001046' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001485' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001598' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001669' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001683' AND [SettingGUID]='00000000-0000-0000-5e77-000000000002'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003358' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003416' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003439' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003441' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003442' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003443' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003458' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003463' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003468' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003473' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003473' AND [SettingGUID]='00000000-0000-0000-5e77-000000000005'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003474' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003475' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000359' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003533' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003579' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO

DELETE FROM [Handelsgut_Setting] WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003582' AND [SettingGUID]='00000000-0000-0000-5e77-000000000003'
GO



/* Landschaft */

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000001' ,N'überall' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000002' ,N'Hausgärten' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000003' ,N'Eisgebiete' ,N'Eiskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000004' ,N'Eiswüste' ,N'Eiskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000005' ,N'Feuchte Grasländer' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000006' ,N'Feuchte Höhlen' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000007' ,N'Feuchte Waldgebiete' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000008' ,N'Feuchte Wiesen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000009' ,N'Fließende Gewässer' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000010' ,N'Flussauen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000011' ,N'Flussläufe' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000012' ,N'Flussufer' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000013' ,N'Gebirge' ,N'Gebirgskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000014' ,N'Gebirgshänge' ,N'Gebirgskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000015' ,N'Gebirgshöhen' ,N'Gebirgskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000016' ,N'Gebirgspflanze' ,N'Gebirgskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000017' ,N'Gezeitenzone' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000018' ,N'Grasland' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000019' ,N'Hochland' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000020' ,N'Höhlen' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000021' ,N'Höhlen ab 100 Schritt Tiefe' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000022' ,N'Höhlen ab 1000 Schritt tiefe' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000023' ,N'Höhlen ab 300 Schritt Tiefe' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000024' ,N'Höhlen ab 50 Schritt Tiefe' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000025' ,N'Höhlen ab 75 Schritt Tiefe' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000026' ,N'Höhleneingänge und Ruinen' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000027' ,N'in der Nähe astraler Quellen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000028' ,N'Kulturland' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000029' ,N'Küste' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000030' ,N'Brackwassersümpfe' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000031' ,N'Küstensümpfe' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000032' ,N'lichte Laubwälder' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000033' ,N'Maraskanische Wälder' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000034' ,N'Maraskankette' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000035' ,N'Moor' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000036' ,N'nur auf Lichtungen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000037' ,N'Randgebiete der Wüsten' ,N'Wüstenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000038' ,N'Regenwald' ,N'Dschungelkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000039' ,N'Ruinenstadt Palakar' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000040' ,N'schattige Gebiete im Hochland' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000041' ,N'Seeufer' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000042' ,N'Stätten der Namenlosen Macht' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000043' ,N'Steppen' ,N'Steppenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000044' ,N'Strand' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000045' ,N'südliche Sümpfe' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000046' ,N'Sumpf' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000047' ,N'Sumpfgebiete' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000048' ,N'sumpfige Uferstreifen' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000049' ,N'Teiche' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000050' ,N'Trockene Höhlen' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000051' ,N'Trockene Höhlen und Gänge' ,N'Höhlenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000052' ,N'Wald' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000053' ,N'Waldgebiete' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000054' ,N'Waldrand' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000055' ,N'Wiesen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000056' ,N'Wüste' ,N'Wüstenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000057' ,N'Wüstenähnliches Hochland' ,N'Wüstenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000058' ,N'Wüstenrandgebiete' ,N'Wüstenkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000059' ,N'offenes Meer' ,N'Meereskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000060' ,N'oberirdische Hitzequellen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000061' ,N'Meer' ,N'Meereskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000062' ,N'Tundra' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000063' ,N'Dschungel' ,N'Dschungelkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000064' ,N'Orismanis' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000066' ,N'Gebirgslagen bis zur Schneegrenze' ,N'Gebirgskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000067' ,N'Buschland' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000068' ,N'vor Regen geschützte Hitzequellen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000069' ,N'Hochgebirge' ,N'Gebirgskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000070' ,N'baumlose Teile von Hochgebirgen' ,N'Gebirgskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000071' ,N'sonstige Binnengewässer' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000072' ,N'Domänen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000073' ,N'Küstengewässer' ,N'Meereskundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000074' ,N'Waldlichtungen' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000075' ,N'Hochmoore' ,N'Sumpfkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000076' ,N'Auwälder' ,N'Waldkundig')
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000077' ,N'Hügelland' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000078' ,N'Seen' ,NULL)
GO

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000079' ,N'Savanne' ,NULL)
GO



/* Pflanze */

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000001' ,9 ,NULL ,NULL ,N'Alraune' ,N'Nutzpflanze' ,N'Grundlage für Elixiere' ,N'ZBA 227 / WdA 206' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000002' ,-5 ,NULL ,NULL ,N'Alveranie' ,N'Übernatürliche Pflanze' ,N'Kann nicht verkauft werden, da alle Rituale voraussetzen, dass die Blätter selbst gepflückt wurden' ,N'ZBA 228' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000003' ,2 ,NULL ,NULL ,N'Arganstrauch' ,N'Heilpflanze' ,N'fördert Heilung' ,N'ZBA 228' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000004' ,6 ,NULL ,NULL ,N'Atan-Kiefer' ,N'Heilpflanze' ,N'Grundlage für fiebersenkendes Mittel' ,N'ZBA 229' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,5 ,NULL ,NULL ,N'Atmon' ,N'Nutzpflanze' ,N'Aufputschmittel' ,N'ZBA 229' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000006' ,2 ,NULL ,NULL ,N'Axorda-Baum' ,N'Heilpflanze' ,N'Grundlage für ein Mittel gegen Zorgan-Pocken' ,N'ZBA 229' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000007' ,15 ,11 ,11 ,N'Basilamine' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 230' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,5)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000008' ,6 ,NULL ,NULL ,N'Belmart' ,N'Heilpflanze' ,N'Gegengift, stoppt Krankheiten' ,N'ZBA 230' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000009' ,2 ,NULL ,NULL ,N'Blutblatt' ,N'Nutzpflanze' ,N'zeigt Magie an' ,N'ZBA 231' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000010' ,-2 ,NULL ,NULL ,N'Boronie' ,N'Übernatürliche Pflanze' ,N'Erleichtert Boron-Liturgien. Ernte im Süden jederzeit, in Mittelaventurien zwischen Tsa und Boron.' ,N'ZBA 231' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000011' ,15 ,NULL ,NULL ,N'Boronsschlinge' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 231, 232' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,-5 ,NULL ,NULL ,N'Bunter Mohn' ,N'Nutzpflanze' ,NULL ,N'ZBA 252, 253' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,5 ,NULL ,NULL ,N'Carlog' ,N'Nutzpflanze' ,N'verleiht Dämmerungssicht' ,N'ZBA 232' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,2 ,NULL ,NULL ,N'Cheria-Kaktus' ,N'Giftpflanze' ,NULL ,N'ZBA 232, 233' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000016' ,6 ,NULL ,NULL ,N'Chonchinis' ,N'Heilpflanze' ,N'Heilmittel bei Brand- und Ätzwunden' ,N'ZBA 233' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000017' ,8 ,NULL ,NULL ,N'Dergolasch' ,N'Nutzpflanze' ,NULL ,N'WdA 193' ,NULL ,NULL ,NULL ,NULL ,N'Kulturkunde (Zwerge)' ,4)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000018' ,5 ,NULL ,NULL ,N'Disdychonda' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 234' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000019' ,6 ,NULL ,NULL ,N'Donf' ,N'Heilpflanze' ,N'fiebersenkend' ,N'ZBA 234 / MyMy 124' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000020' ,3 ,NULL ,NULL ,N'Dornrose' ,N'Übernatürliche Pflanze' ,N'mehr für besondere Edelrosen' ,N'ZBA 235' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000021' ,2 ,NULL ,NULL ,N'Efeuer' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 235' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000022' ,6 ,NULL ,NULL ,N'Egelschreck' ,N'Nutzpflanze' ,N'Grundlage für Wundheilung und Parasitenabwehr' ,N'ZBA 236' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000023' ,2 ,NULL ,NULL ,N'Eitriger Krötenschemel' ,N'Giftpflanze' ,N'Ernte Efferd bis Boron, in südlicheren Gebieten auch ganzjährig' ,N'ZBA 236' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000025' ,4 ,NULL ,NULL ,N'Felsenmilch' ,N'Nutzpflanze' ,NULL ,N'WdA 193 / K&K 108' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000026' ,15 ,NULL ,NULL ,N'Feuermoos und Efferdmoos' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 237' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000027' ,6 ,2 ,3 ,N'Feuerschlick' ,N'Nutzpflanze / Heilpflanze' ,N'Feuerschlick leuchtet während der Vollmondnächte im Rondra und Efferd.' ,N'ZBA 237 / SRD 119' ,NULL ,NULL ,NULL ,NULL ,N'bei Vollmond' ,-5)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000028' ,5 ,NULL ,NULL ,N'Finage' ,N'Heilpflanze' ,N'reduziert Eigenschaftsabzüge, Grundlage für Heilmittel' ,N'ZBA 238 / MyMy 124' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000029' ,8 ,NULL ,NULL ,N'Grauer Lotus' ,N'Übernatürliche Pflanze' ,NULL ,N'ZBA 247' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000030' ,1 ,NULL ,NULL ,N'Grauer Mohn' ,N'Giftpflanze / Nutzpflanze' ,NULL ,N'ZBA 253' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000032' ,2 ,NULL ,NULL ,N'grüne Schleimschlange' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 238' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000033' ,6 ,NULL ,NULL ,N'Gulmond' ,N'Nutzpflanze' ,N'erhöht Konstitution' ,N'ZBA 238, 239' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000034' ,6 ,NULL ,NULL ,N'grüner Schleimpilz' ,N'Nutzpflanze / Giftpflanze' ,NULL ,N'WdA 193, 194 / K&K 108' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000035' ,8 ,3 ,4 ,N'Hiradwurz' ,N'Nutzpflanze / Heilpflanze' ,N'Regenzeit ist neben Efferd und Travia auch in Tsa und Phex' ,N'ZBA 239' ,5 ,N'Regenzeit' ,8 ,9 ,N'Regenzeit' ,5)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000036' ,2 ,NULL ,NULL ,N'Hollbeere' ,N'Giftpflanze / Heilpflanze' ,N'Am Fundort häufig und wertlos, andernorts unbekannt und wertlos' ,N'ZBA 240' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000037' ,8 ,NULL ,NULL ,N'Höllenkraut' ,N'Nutzpflanze / Giftpflanze' ,NULL ,N'ZBA 240' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000038' ,7 ,NULL ,NULL ,N'Horusche' ,N'Nutzpflanze / Giftpflanze' ,N'verleiht Stärke' ,N'ZBA 240, 241' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000039' ,12 ,NULL ,NULL ,N'Iribaarslilie' ,N'Übernatürliche Pflanze' ,NULL ,N'ZBA 241, 242' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,15 ,NULL ,NULL ,N'Jagdgras' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 242, 242' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,7 ,NULL ,NULL ,N'Joruga' ,N'Heilpflanze' ,N'Grundlage für Krankheitsbekämpfung' ,N'ZBA 243' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000042' ,6 ,NULL ,NULL ,N'Kairan' ,N'Nutzpflanze' ,NULL ,N'ZBA 243' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000043' ,2 ,NULL ,NULL ,N'Kajubo' ,N'Nutzpflanze' ,N'ermöglicht, ohne Atemluft zu überleben' ,N'ZBA 244' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000044' ,12 ,1 ,3 ,N'Khôm- oder Mhanadiknolle' ,N'Nutzpflanze' ,N'in der Not unbezahlbar, ansonsten nahezu wertlos' ,N'ZBA 244' ,NULL ,NULL ,NULL ,NULL ,N'mit Blättern' ,5)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000045' ,8 ,NULL ,NULL ,N'Klippenzahn' ,N'Heilpflanze' ,N'beschleunigt Wundheilung' ,N'ZBA 245' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000046' ,10 ,NULL ,NULL ,N'Kukuka' ,N'Nutzpflanze / Giftpflanze' ,N'erhöht Ausdauer' ,N'ZBA 245' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000047' ,5 ,NULL ,NULL ,N'Libellengras' ,N'Nutzpflanze' ,NULL ,N'WdA 194' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000049' ,10 ,NULL ,NULL ,N'Lichtnebler' ,N'Gefährliche Pflanze' ,N'Die Sporen werden bei Kontakt mit Helligkeit freigesetzt.' ,N'K&K 107, 108' ,NULL ,NULL ,NULL ,NULL ,N'nach Freisetzung der Sporen' ,4)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000051' ,5 ,NULL ,NULL ,N'Färberlotos' ,N'Nutzpflanze' ,N'Je nach Farbe, ausreichend für 2 bis 5 Stein Bausch, Leinen oder ''Halbseide''' ,N'ZBA 246' ,NULL ,NULL ,NULL ,NULL ,N'zur Bestimmung der Art' ,9)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000052' ,5 ,NULL ,NULL ,N'Lulanie' ,N'Heilpflanze' ,N'Beruhigungsmittel' ,N'ZBA 247, 248' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000053' ,15 ,NULL ,NULL ,N'Madablüte' ,N'Übernatürliche Pflanze' ,NULL ,N'ZBA 248' ,NULL ,NULL ,NULL ,NULL ,N'bei Vollmond' ,5)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000054' ,10 ,NULL ,NULL ,N'Malomis' ,N'Übernatürliche Pflanze' ,NULL ,N'ZBA 248, 249' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000055' ,2 ,NULL ,NULL ,N'Menchal-Kaktus' ,N'Nutzpflanze' ,N'Preis für einen frisch entwurzelten, ungeöffneten Kaktus, 5 D für eine abgefüllte Anwendung Saft' ,N'ZBA 249' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,2 ,NULL ,NULL ,N'Merach-Strauch' ,N'Nutzpflanze / Giftpflanze' ,NULL ,N'ZBA 250' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000057' ,6 ,NULL ,NULL ,N'Messergras' ,N'Nutzpflanze / Gefährliche Pflanze' ,NULL ,N'ZBA 251' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000058' ,10 ,NULL ,NULL ,N'Mibelrohr' ,N'Nutzpflanze' ,N'Grundlage für Aufputschmittel' ,N'ZBA 251' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,8 ,NULL ,NULL ,N'Mirbelstein' ,N'Nutzpflanze' ,N'Grundlage für ein Mittel gegen Ungeziefer und Elfen' ,N'ZBA 251' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000061' ,2 ,NULL ,NULL ,N'Mirhamer Seidenliane' ,N'Nutzpflanze' ,NULL ,N'ZBA 251, 252' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000063' ,13 ,NULL ,NULL ,N'Morgendornstrauch' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 254, 255' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000064' ,1 ,NULL ,NULL ,N'Naftanstaude' ,N'Giftpflanze' ,NULL ,N'ZBA 255' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000066' ,2 ,NULL ,NULL ,N'Neckerkraut' ,N'Nutzpflanze' ,N'gegen Kerkersieche' ,N'ZBA 255' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000067' ,10 ,NULL ,NULL ,N'Olginwurz' ,N'Heilpflanze' ,N'Gegengift' ,N'ZBA 256, 257' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000068' ,2 ,NULL ,NULL ,N'Orazal' ,N'Nutzpflanze' ,NULL ,N'ZBA 257' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000069' ,2 ,NULL ,NULL ,N'Orkland-Bovist' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 258' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000070' ,6 ,NULL ,NULL ,N'Pestsporenpilz' ,N'Giftpflanze / Nutzpflanze' ,NULL ,N'ZBA 258' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000072' ,10 ,1 ,13 ,N'Phosphorpilz' ,N'Nutzpflanze' ,NULL ,N'ZBA 258, 259' ,NULL ,NULL ,NULL ,NULL ,N'bei Feuchtigkeit' ,-3)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000073' ,7 ,NULL ,NULL ,N'Purpurner Lotos' ,N'Giftpflanze' ,NULL ,N'ZBA 246' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,3 ,NULL ,NULL ,N'Purpurmohn' ,N'Übernatürliche Pflanze' ,NULL ,N'ZBA 253' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000075' ,12 ,NULL ,NULL ,N'Quasselwurz' ,N'Nutzpflanze' ,NULL ,N'ZBA 259' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000076' ,6 ,NULL ,NULL ,N'Quinja' ,N'Nutzpflanze' ,N'erhöht Körperkraft' ,N'ZBA 259, 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000077' ,5 ,NULL ,NULL ,N'Rahjalieb' ,N'Nutzpflanze' ,N'empfängnisverhütend' ,N'ZBA 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,7 ,NULL ,NULL ,N'Rattenpilz' ,N'Übernatürliche Pflanze' ,N'Preis je nachdem, wie viel Verkäufer und Käufer von der Dosis und Wirkung des Pilzes wissen und wie sehr sie ihn benötigen.' ,N'ZBA 260, 261' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000079' ,3 ,NULL ,NULL ,N'Rauschgurke' ,N'Nutzpflanze' ,N'macht stärker und dümmer' ,N'ZBA 261' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000080' ,7 ,NULL ,NULL ,N'Rote Pfeilblüte' ,N'Heilpflanze' ,N'spendet Lebensenergie' ,N'ZBA 261, 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000081' ,10 ,11 ,12 ,N'Roter Drachenschlund' ,N'Heilpflanze' ,N'verhindert Ausbruch von Lykanthropie' ,N'ZBA 262' ,NULL ,NULL ,NULL ,NULL ,N'zur Blütezeit' ,3)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000082' ,3 ,NULL ,NULL ,N'Schwarmschwamm' ,N'Übernatürliche Pflanze' ,N'Preis bis zu 13 Dämonenkronen für einen Samenkörper (als Paraphernalium), für den Schwamm selbst gibt es in der Regel keine Käufer.' ,N'ZBA 265' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000083' ,6 ,NULL ,NULL ,N'Schwarzer Lotos' ,N'Giftpflanze' ,NULL ,N'ZBA 246' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000084' ,5 ,NULL ,NULL ,N'Schwarzer Mohn' ,N'Nutzpflanze / Droge' ,NULL ,N'ZBA 253, 254' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,3 ,NULL ,NULL ,N'Seelenhauch' ,N'Übernatürliche Pflanze / Giftpflanze' ,NULL ,N'WdA 195' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000086' ,2 ,NULL ,NULL ,N'Shurinstrauch' ,N'Giftpflanze' ,NULL ,N'ZBA 267' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000087' ,12 ,NULL ,NULL ,N'Steinrinde' ,N'Nutzpflanze' ,NULL ,N'K&K 107 / WdA 195' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000088' ,5 ,NULL ,NULL ,N'Talaschin' ,N'Nutzpflanze' ,NULL ,N'ZBA 268' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000089' ,8 ,NULL ,NULL ,N'Tarnblatt' ,N'Giftpflanze' ,NULL ,N'ZBA 268' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000090' ,2 ,NULL ,NULL ,N'Tarnele' ,N'Heilpflanze' ,N'schmerzlindernd und heilungsfördernd' ,N'ZBA 268, 269' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000091' ,12 ,12.2 ,12.7 ,N'Thonnys' ,N'Nutzpflanze' ,N'erlaubt Astrale Meditation. Blüht nur zwei Wochen im Rahja.' ,N'ZBA 269' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,5)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,10 ,NULL ,NULL ,N'Tigermohn' ,N'Nutzpflanze / Droge' ,N'Preis pro Samenkapsel, Beruhigungsmittel' ,N'ZBA 254' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,6 ,NULL ,NULL ,N'Traschbart' ,N'Heilpflanze' ,N'fiebersenkend' ,N'ZBA 269, 270' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000094' ,11 ,NULL ,NULL ,N'Trichterwurzel' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 270' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000095' ,1 ,NULL ,NULL ,N'Tuur-Amash-Kelch' ,N'Übernatürliche Pflanze' ,N'Um einen Tuur-Amash-Kelch rechtzeitig zu
entdecken, muss eine Sinnenschärfe-Probe +7 gelingen.' ,N'ZBA 270, 271' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000096' ,2 ,NULL ,NULL ,N'Ulmenwürger' ,N'Heilpflanze' ,N'verbessert Regeneration und verhindert Wundfieber' ,N'ZBA 271' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000097' ,5 ,NULL ,NULL ,N'Vierblättrige Einbeere' ,N'Heilpflanze' ,N'stoppt Blutungen und gibt Lebensenergie zurück' ,N'ZBA 271' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000098' ,6 ,NULL ,NULL ,N'Vragieswurzel' ,N'Giftpflanze' ,NULL ,N'ZBA 272' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000100' ,9 ,NULL ,NULL ,N'Waldwebe' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 272' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000101' ,14 ,NULL ,NULL ,N'Wandermoos' ,N'Nutzpflanze' ,N'Bei ungünstigen bedingungen wird das Moos mobil und sucht sich eine neue Wasserquelle.' ,N'K&K 107 / WdA 196' ,NULL ,NULL ,NULL ,NULL ,N'bei Trockenheit' ,5)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000102' ,1 ,NULL ,NULL ,N'Wasserrausch' ,N'Nutzpflanze' ,N'Rahjaikum, euphorisierend' ,N'ZBA 273' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000104' ,5 ,NULL ,NULL ,N'Weißer Lotos' ,N'Giftpflanze' ,NULL ,N'ZBA 247' ,NULL ,NULL ,NULL ,NULL ,N'zur Bestimmung der Art' ,10)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000105' ,5 ,NULL ,NULL ,N'Weißgelber Lotos' ,N'Nutzpflanze' ,NULL ,N'ZBA 247' ,NULL ,NULL ,NULL ,NULL ,N'zur Bestimmung der Art' ,10)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000106' ,12 ,NULL ,NULL ,N'Winselgras' ,N'Nutzpflanze' ,NULL ,N'ZBA 273' ,NULL ,NULL ,NULL ,NULL ,N'Nachts' ,-2)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000107' ,5 ,NULL ,NULL ,N'Wirselkraut' ,N'Heilpflanze' ,NULL ,N'ZBA 273, 274 / MyMy 124' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000108' ,5 ,NULL ,NULL ,N'Würgedattel' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 274' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,5 ,NULL ,NULL ,N'Zithabar' ,N'Nutzpflanze' ,N'Wird meist in einer Wasserpfeife geraucht' ,N'ZBA 275' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000110' ,2 ,NULL ,NULL ,N'Zunderschwamm' ,N'Nutzpflanze' ,N'ermöglicht Feuermachen' ,N'ZBA 275f / SRD 275, 276 / H&K 159' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000111' ,5 ,NULL ,NULL ,N'Zwölfblatt' ,N'Heilpflanze' ,N'schützt vor Ansteckung' ,N'ZBA 276' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000112' ,6 ,NULL ,NULL ,N'Yaganstrauch' ,N'Nutzpflanze' ,N'Preis pro Nuss, erhöht Ausdauer' ,N'ZBA 274, 275' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000113' ,6 ,NULL ,NULL ,N'Nothilf' ,N'Heilmittel' ,N'Preis pro Portion, heilt Brandwunden' ,N'ZBA 256' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000115' ,-2 ,NULL ,NULL ,N'Satuariensbusch' ,N'Heilpflanze / Übernatürliche Pflanze' ,N'Preis für eine Handvoll Blätter, Blüten oder Früchte, schützt vor Wundfieber' ,N'ZBA 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000117' ,3 ,NULL ,NULL ,N'Schlangenzünglein' ,N'Nutzpflanze' ,N'Preis pro Saft einer Pflanze, zeigt Magie an' ,N'ZBA 263, 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000118' ,5 ,NULL ,NULL ,N'Bleichmohn' ,N'Nutzpflanze / Droge' ,N'Schmerzmittel' ,N'ZBA 252' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000119' ,0 ,NULL ,NULL ,N'Aschenbusch' ,N'Giftpflanze' ,NULL ,N'Uhrwerk3 29, 30, 31' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000120' ,99 ,3.5 ,4.5 ,N'Atermatea Funginus/Baramuns Liebling' ,N'Giftpflanze / Nutzpflanze' ,N'8 Ob pro Portion, Preis außerhalb der Erntezeit 1 bis 2 Ag, wobei Baramunen auch deutlich mehr zahlen oder sie mit Gewalt an sich bringen' ,N'Uhrwerk3 31, 32' ,NULL ,NULL ,NULL ,NULL ,N'Fruchtkörper' ,4)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000121' ,4 ,NULL ,NULL ,N'Batuunuur/Blutgewürz' ,N'Parasit / Nutzpflanze' ,NULL ,N'Uhrwerk3 32, 33' ,NULL ,NULL ,NULL ,NULL ,N'auf schwarzen Meereswesen' ,7)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000124' ,6 ,NULL ,NULL ,N'Blutpilz' ,N'Giftpflanze / Gefährliche Pflanze' ,N'Der Pilz ruht in den tiefen Wintermonaten Ende Chrysir bis Anfang Siminia.' ,N'Uhrwerk3 33, 34' ,NULL ,NULL ,NULL ,NULL ,N'Fruchtkörper' ,3)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000126' ,4 ,1 ,2.5 ,N'Brajankelch' ,N'Nutzpflanze' ,N'gedeiht auf hohen Bäumen' ,N'Uhrwerk3 34' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,0)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000129' ,10 ,NULL ,NULL ,N'Brajanlieb/Kompasskraut ' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk3 34, 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000130' ,7 ,NULL ,NULL ,N'Chrysirhaube' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk3 35' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000131' ,4 ,NULL ,NULL ,N'Echobaum' ,N'Übernatürliche Pflanze' ,NULL ,N'Uhrwerk3 35 ,36' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000134' ,7 ,11.5 ,12.5 ,N'Goldhaut ' ,N'Nutzpflanze' ,N'Blütezeit (spätes Zatura-Oktal bis Mitte Shinxir)' ,N'Uhrwerk3 36' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,2)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000135' ,2 ,NULL ,NULL ,N'Incendium Herba' ,N'Übernatürliche Pflanze' ,NULL ,N'Uhrwerk4 37' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000138' ,8 ,11.7 ,13 ,N'Laufkraut ' ,N'Nutzpflanze' ,N'Blütezeit im Shinxir. Preis außerhalb der Erntezeit das Dreifache.' ,N'Uhrwerk4 37, 38' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,2)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000139' ,6 ,NULL ,NULL ,N'Lebensmoos ' ,N'Heilpflanze' ,NULL ,N'Uhrwerk4 38,39' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000140' ,2 ,NULL ,NULL ,N'Loruoor/Lebenskoralle ' ,N'Heilpflanze / Giftpflanze' ,NULL ,N'Uhrwerk4 39' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000142' ,6 ,12.6 ,1 ,N'Nachtaugen ' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk4 40' ,NULL ,NULL ,NULL ,NULL ,N'Nachts' ,0)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000143' ,0 ,NULL ,NULL ,N'Sandklaue' ,N'Wildpflanze / Nutzpflanze' ,N'Bei Waffen und Rüstungen aus dem Holz steigt der Preis x4, aber nicht unter 10 Au.' ,N'Uhrwerk4 40, 41, 42' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000145' ,6 ,10 ,11.5 ,N'Satyarenbaum ' ,N'Nutzpflanze' ,N'während Blüte (Zatura) und Ernte (Raia)' ,N'Uhrwerk4 42' ,3 ,N'Erntezeit' ,2.5 ,3 ,N'Blütezeit' ,3)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000146' ,6 ,NULL ,NULL ,N'Schneemoos ' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk4 42' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000148' ,6 ,8 ,10 ,N'Tanzbusch' ,N'Giftpflanze' ,NULL ,N'Uhrwerk4 43, 44' ,4 ,N'Erntezeit' ,NULL ,NULL ,N'Blütezeit' ,4)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000150' ,6 ,7 ,13 ,N'Tyrannenbaum' ,N'Übernatürliche Pflanze' ,NULL ,N'Uhrwerk4 44f' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,2)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000152' ,6 ,7 ,13 ,N'Waldkönig' ,N'Übernatürliche Pflanze' ,NULL ,N'Uhrwerk4 44f' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,2)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000154' ,6 ,8.5 ,9 ,N'Witwenmacher' ,N'Heilpflanze' ,NULL ,N'Uhrwerk4 45' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,2)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000156' ,6 ,1 ,2.5 ,N'Wolfsblatt' ,N'Wildpflanze / Nutzpflanze' ,N'Preis gilt für denn Winter und Frühjahr; im Spätsommer mehr' ,N'Uhrwerk4 46' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,4)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000157' ,0 ,NULL ,NULL ,N'Atholisbaum' ,N'Wildpflanze' ,NULL ,N'Myranor 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000158' ,0 ,NULL ,NULL ,N'Auijaja-Baum' ,N'Gefährliche Pflanze' ,NULL ,N'Myranor 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000159' ,0 ,NULL ,NULL ,N'Azulie/Neristu-Rose' ,N'Droge' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000160' ,0 ,NULL ,NULL ,N'Belyabels Schleier' ,N'Parasit' ,NULL ,N'HuW 91' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000161' ,0 ,NULL ,NULL ,N'Boinure' ,N'Gefährliche Pflanze' ,NULL ,N'Myranor 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000162' ,-5 ,NULL ,NULL ,N'Cassava' ,N'Nutzpflanze / Giftpflanze' ,NULL ,N'Myranor 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000163' ,0 ,NULL ,NULL ,N'Claqua-Rose' ,N'Nutzpflanze' ,NULL ,N'Myranor 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000164' ,0 ,NULL ,NULL ,N'Geistblüte' ,N'Übernatürliche Pflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000165' ,0 ,NULL ,NULL ,N'Ghorlenklaue ' ,N'Nutzpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000166' ,0 ,NULL ,NULL ,N'Gyldaraswacht ' ,N'Nutzpflanze' ,NULL ,N'Myranor 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,0 ,NULL ,NULL ,N'Himmelszeder' ,N'Nutzpflanze' ,NULL ,N'Myranor 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000168' ,0 ,NULL ,NULL ,N'Isnea' ,N'Nutzpflanze' ,NULL ,N'Myranor 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000169' ,0 ,NULL ,NULL ,N'Jungfernhüter' ,N'Nutzpflanze' ,NULL ,N'Myranor 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000170' ,0 ,NULL ,NULL ,N'Maulbaum' ,N'Gefährliche Pflanze' ,NULL ,N'CM 86 / MyMo 124' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000171' ,0 ,NULL ,NULL ,N'Neretonie' ,N'Nutzpflanze' ,NULL ,N'Myranor 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000172' ,0 ,NULL ,NULL ,N'Pardiriske' ,N'Nutzpflanze' ,NULL ,N'Myranor 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000173' ,0 ,NULL ,NULL ,N'Pfeilsamenbaum ' ,N'Gefährliche Pflanze' ,NULL ,N'HuW 90' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000174' ,0 ,NULL ,NULL ,N'Phrenophoren-Kaktus ' ,N'Nutzpflanze' ,NULL ,N'CM 69' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000175' ,0 ,NULL ,NULL ,N'Ramara-Diestel ' ,N'Nutzpflanze' ,NULL ,N'Myranor 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000176' ,0 ,NULL ,NULL ,N'Spährentod' ,N'Giftpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000177' ,0 ,NULL ,NULL ,N'Sternapfelbaum' ,N'Heilpflanze' ,NULL ,N'CM 121' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000178' ,0 ,NULL ,NULL ,N'Tarnaille/Taschenblümchen' ,N'Heilpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000179' ,0 ,NULL ,NULL ,N'Therkalbaum/Krüppelzeder' ,N'Nutzpflanze' ,NULL ,N'Myranor 260' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000180' ,0 ,NULL ,NULL ,N'Tilians-Gruß' ,N'Giftpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000181' ,0 ,NULL ,NULL ,N'Wächter-Efeu' ,N'Nutzpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000182' ,0 ,NULL ,NULL ,N'Wächterhecke' ,N'Nutzpflanze ' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000183' ,3 ,NULL ,NULL ,N'Alzazeerenbaum' ,N'Heilpflanze' ,NULL ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000185' ,3 ,NULL ,NULL ,N'Aurinde/Sonnenkraut' ,N'Heilpflanze' ,N'für Amaunir zur Berkämpfung der Kahle' ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000186' ,5 ,NULL ,NULL ,N'Balburri-Perlen' ,N'Heilpflanze' ,NULL ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000187' ,2 ,NULL ,NULL ,N'Caranuss-Strauch' ,N'Droge' ,NULL ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000188' ,3 ,NULL ,NULL ,N'Cuina-Blüte' ,N'Droge' ,NULL ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,N'kultiviert' ,-3)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000190' ,4 ,NULL ,NULL ,N'Feuerposaune' ,N'Droge' ,NULL ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000191' ,3 ,NULL ,NULL ,N'Mantigora' ,N'Giftpflanze / Nutzpflanze' ,NULL ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000192' ,3 ,NULL ,NULL ,N'Remembalia' ,N'Übernatürliche Pflanze' ,NULL ,N'Myranor 262' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000194' ,8 ,NULL ,NULL ,N'Roter Bambus' ,N'Nutzpflanze' ,N'Der Bambus blüht alle Dutzend Jahre einmal. Probe auf Holzbearbeitung nach Meisterentscheid' ,N'Myranor 262,263' ,NULL ,NULL ,NULL ,NULL ,N'Blütezeit' ,2)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000195' ,8 ,NULL ,NULL ,N'Schwesterlein' ,N'Übernatürliche Pflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000196' ,4 ,NULL ,NULL ,N'Szata''te' ,N'Giftpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000197' ,5 ,NULL ,NULL ,N'Tarnblatt' ,N'Nutzpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000199' ,3 ,NULL ,NULL ,N'Traumpilz' ,N'Giftpflanze / Gefährliche Pflanze' ,N'vom leichten Rauschmittel bis zum tödlichen Atemgift oder stark suchterzeugenden Droge' ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,N'zum Erkennen des korrekten Zeitpunkts den Wald zu betreten' ,8)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000200' ,6 ,NULL ,NULL ,N'Warakwurz' ,N'Nutzpflanze' ,NULL ,N'Myranor 263' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000201' ,2 ,NULL ,NULL ,N'Ilmenblatt' ,N'Nutzpflanze / Droge' ,NULL ,N'ZBA 241' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000202' ,12 ,NULL ,NULL ,N'Iribarslilie' ,N'Übernatürliche Pflanze' ,NULL ,N'ZBA 242' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000203' ,12 ,NULL ,NULL ,N'Sansaro' ,N'Nutzpflanze' ,N'Verbreitung: Küstengewässer (häufig, im Winter selten),
Offenes Meer (selten, nicht im Winter)' ,N'ZBA 262f' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000204' ,6 ,NULL ,NULL ,N'Schleichender Tod' ,N'Nutzpflanze / Droge' ,NULL ,N'ZBA 264' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000205' ,3 ,NULL ,NULL ,N'Schleimiger Sumpfknöterich' ,N'Giftpflanze' ,NULL ,N'ZBA 264f' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000206' ,12 ,NULL ,NULL ,N'Schlinggras' ,N'Gefährliche Pflanze' ,NULL ,N'ZBA 265' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000207' ,2 ,NULL ,NULL ,N'Schwarzer Wein' ,N'Übernatürliche Pflanze' ,NULL ,N'ZBA 266f' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000208' ,8 ,NULL ,NULL ,N'Suunurguur/Blutschlinge' ,N'Gefährliche Pflanze' ,NULL ,N'Uhrwerk4 42f' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO

INSERT INTO [Pflanze] (  [PflanzeGUID],  [Bestimmung],  [AusnahmeVon],  [AusnahmeBis],  [Name],  [Kategorie],  [Bemerkung],  [Literatur],  [BestimmungAusname2],  [Ausnahme2Grund],  [Ausnahme2Von],  [Ausnahme2Bis],  [AusnameGrund],  [BestimmungAusnahme]) 
 VALUES ('00000000-0000-0000-00ff-000000000209' ,0 ,NULL ,NULL ,N'Zaturaklaue' ,N'Gefährliche Pflanze' ,NULL ,N'Uhrwerk4 46f' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL)
GO



/* Pflanze_Ernte */

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('0029e5f9-3261-4e73-9988-dfa6ba022582' ,'00000000-0000-0000-00ff-000000000081' ,1 ,13 ,N'W6' ,N'Blätter' ,N'W3+6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001020')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('00d2f758-64c5-44a9-aa17-15ae4bde9759' ,'00000000-0000-0000-00ff-000000000201' ,4 ,4 ,N'W20' ,N'Blätter und Blüten' ,N'2W+24' ,N'Monate' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003663')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('00eced48-027a-4a43-9371-c8004a8c4558' ,'00000000-0000-0000-00ff-000000000084' ,5 ,5 ,N'1' ,N'Samenkapsel' ,N'2W6+9' ,N'Monate' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000001023')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('0160c4a2-6d72-4182-8e65-6fd6df86c971' ,'00000000-0000-0000-00ff-000000000016' ,8 ,5 ,N'W20' ,N'Blätter' ,N'W6+24' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000961')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('0421f475-4f2d-44f7-965a-539fab7f9526' ,'00000000-0000-0000-00ff-000000000036' ,2 ,3 ,N'2W6*(2W6+5)' ,N'Beeren' ,N'W6' ,N'Tage' ,NULL ,N'2W6 Sträucher mit der jeweiligen Anzahl Beeren und Blätter.' ,'00000000-0000-0000-002a-000000003425')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('06e45651-a1b6-4eee-8e36-48a10c6622ce' ,'00000000-0000-0000-00ff-000000000075' ,9 ,4 ,N'1' ,N'Wurzel' ,N'W3+3' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001014')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('091e1d1d-038e-4257-9505-38e1fdfc623e' ,'00000000-0000-0000-00ff-000000000045' ,10 ,2 ,N'2W6' ,N'Stängel' ,N'W3+2' ,N'Tage' ,NULL ,N'Maximal bis zum nächsten Vollmond haltbar.' ,'00000000-0000-0000-002a-000000000989')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('0970cf17-232e-47c0-a06e-bfddc75991be' ,'00000000-0000-0000-00ff-000000000115' ,11 ,12 ,N'W20' ,N'Blüten' ,N'W6+9' ,N'Monate' ,NULL ,N'getrocknet als Tee (Verarbeitung +2)' ,'00000000-0000-0000-002a-000000003357')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('0eac5e5d-41b5-4789-b99a-76a5a711dfdf' ,'00000000-0000-0000-00ff-000000000187' ,1 ,13 ,N'1' ,N'Portion' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003650')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('0f940cf6-f860-4921-98af-a24287e557ff' ,'00000000-0000-0000-00ff-000000000079' ,8 ,5 ,N'3W6' ,N'Rauschgurken' ,N'W3+1' ,N'Wochen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001018')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('0fd723a2-2a2c-4055-a028-87383f01179e' ,'00000000-0000-0000-00ff-000000000160' ,1 ,13 ,N'1' ,N'Gift' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003581')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('102f114a-9000-4dc7-856f-04af1d3ac4d8' ,'00000000-0000-0000-00ff-000000000187' ,1 ,13 ,N'1' ,N'Portion' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000002509')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('1238d7e4-7047-4d26-864c-b496f7265a6e' ,'00000000-0000-0000-00ff-000000000084' ,3 ,5 ,N'2' ,N'Blätter' ,N'2W6+9' ,N'Monate' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000001023')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('14f4ce71-f948-4166-8c8c-43fa09059340' ,'00000000-0000-0000-00ff-000000000196' ,1 ,13 ,N'1' ,N'Stachel' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003658')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('15c99a9b-76fc-4f4f-9bc3-fed93517f95c' ,'00000000-0000-0000-00ff-000000000066' ,1 ,13 ,N'1W20+5' ,N'Blätter' ,N'W3+3' ,N'Wochen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001006')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('161e2cf6-86d3-4ec9-876f-568b36d379f8' ,'00000000-0000-0000-00ff-000000000005' ,10 ,10 ,N'W6' ,N'Büschel' ,N'W6+6 ' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000950')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('18608ae5-37d7-4bc5-b9fa-0400827f2f2a' ,'00000000-0000-0000-00ff-000000000029' ,9 ,5 ,N'W6+1' ,N'Blüten' ,N'W6' ,N'Wochen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003432')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('18dcabea-44ef-4d9f-b443-8985515d3412' ,'00000000-0000-0000-00ff-000000000020' ,1 ,13 ,N'1W6' ,N'Blüten' ,N'W6+7' ,N'Tage' ,0.5 ,N'Die Zweige können als Hiebwaffe verwendet werden. Die Dornen haben eine Haltbarkeit von W6+7 Wochen.' ,'00000000-0000-0000-002a-000000000965')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('1c1259b2-d83f-42fa-a498-dc7946e76e7f' ,'00000000-0000-0000-00ff-000000000173' ,1 ,13 ,N'3W6' ,N'Samen' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003594')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('1d7bfe6a-0bce-4274-b9a7-002abb151afc' ,'00000000-0000-0000-00ff-000000000072' ,1 ,13 ,N'W6' ,N'Stein der leuchtende Geflechtstücke (für die direkte Anwendung, sonst trockenes Geflecht)' ,N'W6' ,N'SR' ,40 ,N'Die Haltbarkeit betrifft die Leuchtkraft.' ,'00000000-0000-0000-002a-000000001011')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('1e7f276c-b3d6-4985-a395-5a88c03f5003' ,'00000000-0000-0000-00ff-000000000138' ,11.6 ,12.2 ,N'1' ,N'Portion Blütenblätter' ,N'4' ,N'Nonen' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000003539')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('1f4c31c7-6dfc-4b0b-b2b8-fb8c3f7fb15a' ,'00000000-0000-0000-00ff-000000000172' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003593')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('1fe00c29-8fa2-43f0-8a03-fa32c6512170' ,'00000000-0000-0000-00ff-000000000001' ,1 ,13 ,N'1' ,N'Pflanze' ,N'W3+2' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000946')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('2251603a-84c8-473f-ac23-5de7a6306d73' ,'00000000-0000-0000-00ff-000000000064' ,11 ,2 ,N'1' ,N'Staude' ,N'1W6+4' ,N'Tage' ,NULL ,N'Im Norden' ,'00000000-0000-0000-002a-000000001005')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('23b48ef7-3b80-41aa-b0f9-d8ef506aa63d' ,'00000000-0000-0000-00ff-000000000121' ,4.001 ,4.033 ,N'1' ,N'Portion Sporen' ,N'2' ,N'Oktale' ,NULL ,N'leicht feucht aufbewahren' ,'00000000-0000-0000-002a-000000003528')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('25dafbcd-6b13-4ec2-80aa-976c1a7d7171' ,'00000000-0000-0000-00ff-000000000019' ,1 ,13 ,N'1' ,N'Stängel' ,N'W6+6' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000964')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('294fecdf-68da-4b6a-8997-0a758b66e654' ,'00000000-0000-0000-00ff-000000000207' ,1 ,13 ,N'1' ,N'Traube' ,N'W6+3' ,N'Tage' ,NULL ,N'Die Ernte ist nur möglich, wenn 7 TaP* übrig bleiben oder für ausreichend Opfer gesorgt wurde.' ,'00000000-0000-0000-002a-000000003460')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('29c95f9d-ee52-4fc4-b3ea-e35375925cca' ,'00000000-0000-0000-00ff-000000000170' ,1 ,13 ,N'0' ,N'Holz' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003591')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('2a421ba2-70a0-497a-b9cc-2e7bc6c4cbf4' ,'00000000-0000-0000-00ff-000000000051' ,11 ,1 ,N'2W6+1' ,N'Blüten pro Fundort' ,N'W3+6' ,N'Monate' ,NULL ,N'trocknen und licht- und luftdicht aufbewahren' ,'00000000-0000-0000-002a-000000000993')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('2c7fe397-349d-4a13-bbca-34396ec7ccb1' ,'00000000-0000-0000-00ff-000000000102' ,12 ,3 ,N'2W20' ,N'Blüten' ,N'3W+24' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001040')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('2cbc904c-e7d0-4023-b79b-205aba127ce4' ,'00000000-0000-0000-00ff-000000000073' ,11 ,1 ,N'W6+1' ,N'Blüten pro Fundort' ,N'W6+2' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001012')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('300f50fe-fe5e-41f7-9fa1-b2abb9774a67' ,'00000000-0000-0000-00ff-000000000082' ,3 ,3 ,N'W2' ,N'Samenkörper' ,N'W6' ,N'Monate' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001021')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('319bcaf8-b6e8-4eee-8db4-8bff94f51e14' ,'00000000-0000-0000-00ff-000000000054' ,2 ,2 ,N'1' ,N'Blüte' ,N'W3' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000996')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('326e081f-db03-4e87-a21b-cdcf9705773c' ,'00000000-0000-0000-00ff-000000000162' ,1 ,13 ,N'1' ,N'Knolle' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003583')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('33af268f-a7e1-4b6b-91da-f0fffcef3308' ,'00000000-0000-0000-00ff-000000000017' ,2 ,11 ,N'1W6' ,N'Pilzhüte' ,N'2W6' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000962')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('37a49663-f12f-4120-ad09-9cd27a1f649c' ,'00000000-0000-0000-00ff-000000000150' ,7 ,8.5 ,N'1' ,N'Blüten' ,N'0,5' ,N'Jahre' ,1 ,N'eingelegt aufbewahren' ,'00000000-0000-0000-002a-000000003547')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('38b29264-5acb-4f4b-b9c4-77607eda7ba4' ,'00000000-0000-0000-00ff-000000000126' ,3.5 ,4.1 ,N'1' ,N'Frucht' ,N'1' ,N'Oktal' ,NULL ,N'Fruchtsaft nur magisch länger haltbar' ,'00000000-0000-0000-002a-000000003531')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('3a288e4e-0be2-44b7-a90e-f678d750a039' ,'00000000-0000-0000-00ff-000000000087' ,1 ,13 ,N'W6' ,N'Stein' ,N'W6' ,N'Tage' ,40 ,NULL ,'00000000-0000-0000-002a-000000001026')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('3aa5c65c-0a78-479e-afe9-d56e65f40b4f' ,'00000000-0000-0000-00ff-000000000008' ,10 ,5 ,N'4W20' ,N'Blätter' ,N'2W6+18' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000953')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('3f0f178e-1402-470c-80bb-2843e5118a1b' ,'00000000-0000-0000-00ff-000000000135' ,1 ,13 ,N'4W6' ,N'Samen' ,NULL ,N'unbegrenzt' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000003537')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('41a6e7cc-bbfe-4eff-8ae6-224ddf38cb73' ,'00000000-0000-0000-00ff-000000000150' ,1 ,2.5 ,N'1' ,N'Flaum' ,N'0,5' ,N'Jahre' ,1 ,NULL ,'00000000-0000-0000-002a-000000003671')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('42e9352b-cce3-420e-8b62-ad1fc8323459' ,'00000000-0000-0000-00ff-000000000175' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003596')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('454877e9-12b2-4edc-bf65-218fae565820' ,'00000000-0000-0000-00ff-000000000056' ,3 ,4 ,N'2W20' ,N'Früchte' ,N'W6+7' ,N'Tage' ,NULL ,N'Giftstufe 1' ,'00000000-0000-0000-002a-000000001093')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('461a2fb0-6511-40e9-a2b3-4c4c99c5c5d3' ,'00000000-0000-0000-00ff-000000000035' ,1 ,13 ,N'1' ,N'Wurzel' ,N'W3+6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000979')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('463dc968-f9aa-4959-bc78-4e8b44a7890a' ,'00000000-0000-0000-00ff-000000000068' ,1 ,13 ,N'W6' ,N'verholzte Stängel' ,N'mehrere' ,N'Jahre' ,40 ,NULL ,'00000000-0000-0000-002a-000000001008')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('4806aa0a-0165-4e49-84eb-5922168dcd34' ,'00000000-0000-0000-00ff-000000000083' ,11 ,1 ,N'W6' ,N'Blüten' ,N'W6+2' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001022')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('48228a05-aaaa-466a-a4f7-cf0927ca3b20' ,'00000000-0000-0000-00ff-000000000033' ,10 ,6 ,N'2W6' ,N'Blätter' ,N'W6+24' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000977')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('4c3d1caf-13a6-40d8-98f8-bd9be0b0a663' ,'00000000-0000-0000-00ff-000000000165' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003586')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('4e09e7c5-98fa-437e-adda-2c6d4190862d' ,'00000000-0000-0000-00ff-000000000058' ,8 ,6 ,N'2W6' ,N'Kolben' ,N'W6' ,N'Tage' ,NULL ,N'Im Süden' ,'00000000-0000-0000-002a-000000001000')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('51492954-a0ab-4e53-80ae-a9625b2ff019' ,'00000000-0000-0000-00ff-000000000195' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003657')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('52a25860-088e-4d01-bb1d-96805efcc90d' ,'00000000-0000-0000-00ff-000000000004' ,1 ,13 ,N'W20' ,N'Stein Rinde' ,N'W6+12' ,N'Tage' ,40 ,N'Bei komplettem Abschälen verdreifacht sich die Erntemenge.' ,'00000000-0000-0000-002a-000000000949')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('53684bbc-ceed-40ec-a8d9-a0afa3b92eda' ,'00000000-0000-0000-00ff-000000000022' ,2 ,6 ,N'2W20' ,N'Blätter' ,N'W3+2' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000967')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5547fab8-c139-4fce-9c95-a398337f922f' ,'00000000-0000-0000-00ff-000000000145' ,10 ,11.5 ,N'1' ,N'Portion Rindensud' ,N'0,5' ,N'Jahre' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003544')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('57e76e49-5c9b-4eef-beba-d9145b599865' ,'00000000-0000-0000-00ff-000000000030' ,1 ,13 ,N'1' ,N'Blüte' ,N'W6+12' ,N'Wochen' ,NULL ,N'getrocknete Blätter licht- und luftdicht aufbewahrt' ,'00000000-0000-0000-002a-000000000975')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('587e537e-33ea-4796-b07f-aed177e5cb50' ,'00000000-0000-0000-00ff-000000000098' ,3 ,4 ,N'1' ,N'Wurzel' ,N'W+7' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001037')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('595c3735-7d25-418b-a12a-0c26554f5724' ,'00000000-0000-0000-00ff-000000000174' ,1 ,13 ,N'1' ,N'Knolle' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003595')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('59f2629b-7ac9-4ce5-89c2-3797ced03f0f' ,'00000000-0000-0000-00ff-000000000131' ,10 ,11.5 ,N'1' ,N'Blüten' ,N'1' ,N'Jahr' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003534')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('59f6e6d0-4bce-4dc4-84c3-8679f50518aa' ,'00000000-0000-0000-00ff-000000000188' ,1 ,13 ,N'1' ,N'Blüte' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000002510')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5b08ac06-2a85-4008-81dc-8d6b4bcbf58a' ,'00000000-0000-0000-00ff-000000000183' ,1 ,13 ,N'1' ,N'Portion Blätter' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003646')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5b66c6d6-e15e-4feb-9c9c-f77a2e2fd5d8' ,'00000000-0000-0000-00ff-000000000159' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003580')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5bafda89-d69e-4fbc-979f-23446763fa84' ,'00000000-0000-0000-00ff-000000000107' ,9 ,5 ,N'W6+4' ,N'Blätter' ,N'2W6+18' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001045')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5c1ab3d5-0b03-4a66-a876-6b781f2ede12' ,'00000000-0000-0000-00ff-000000000003' ,1 ,13 ,N'1' ,N'Wurzel' ,N'2W6+18' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000948')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5cdb213f-785b-45d4-9001-3354e3b50ace' ,'00000000-0000-0000-00ff-000000000070' ,3 ,5 ,N'1' ,N'Pilzhaut' ,N'W6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001010')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5d08c9a6-2165-46b3-99d6-72e96dfb9803' ,'00000000-0000-0000-00ff-000000000013' ,4 ,4 ,N'1' ,N'Samenkapsel' ,N'W6+9' ,N'Monate' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000000958')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5d82bf0c-571e-4bd0-87be-93a1dcacb4ec' ,'00000000-0000-0000-00ff-000000000014' ,10 ,10 ,N'W6' ,N'Blüten' ,N'2W6+18' ,N'Stunden' ,NULL ,N'Jede Blüte hat einen Stempel.' ,'00000000-0000-0000-002a-000000000959')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5eb02c1c-2021-4cc2-be4b-6afd1e4c648d' ,'00000000-0000-0000-00ff-000000000101' ,1 ,13 ,N'1' ,N'Moosball' ,N'W20' ,N'Monate' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001039')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5f501291-2a0f-43d4-84c2-b4e160d950cf' ,'00000000-0000-0000-00ff-000000000180' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003601')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5fdd81c4-efce-43bf-a6c7-13bd2f34a044' ,'00000000-0000-0000-00ff-000000000026' ,1 ,13 ,N'1' ,N'Saft' ,NULL ,NULL ,NULL ,N'Haltbarkeit ist je nach Menge einige Stunden bis Tage.' ,'00000000-0000-0000-002a-000000000970')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('5ff08ef1-403b-4bb7-88b0-ffdcfde98df3' ,'00000000-0000-0000-00ff-000000000058' ,11 ,3 ,N'2W6' ,N'Kolben' ,N'W6' ,N'Tage' ,NULL ,N'Im Norden' ,'00000000-0000-0000-002a-000000001000')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('62463a30-0844-4796-9c42-d15b0867d8da' ,'00000000-0000-0000-00ff-000000000055' ,1 ,13 ,N'1' ,N'ganze Pflanze' ,N'W3+5' ,N'Wochen' ,NULL ,N'Ein Kaktus mit enthält W6 halben Maß Menchalsaft. Der Saft ist W6+8 Wochen haltbar. Bei 1 auf W20 trägt der Kaktus gerade W6 Blüten.' ,'00000000-0000-0000-002a-000000000997')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('626d31ae-e884-4748-af4e-9fdf7ac15538' ,'00000000-0000-0000-00ff-000000000145' ,2.5 ,3 ,N'1' ,N'Portion Rindensud' ,N'0,5' ,N'Jahre' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003544')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('643c4542-a351-49e7-8cdc-941bf459b0cc' ,'00000000-0000-0000-00ff-000000000126' ,2 ,2.5 ,N'1' ,N'Nektar' ,NULL ,NULL ,NULL ,N'nur magisch länger haltbar' ,'00000000-0000-0000-002a-000000003530')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('64d3a7c0-88b7-4c6b-97dd-529e7045252f' ,'00000000-0000-0000-00ff-000000000077' ,9 ,7 ,N'2W6' ,N'Blätter' ,N'3' ,N'Tage' ,NULL ,N'wirkt nicht auf Achaz' ,'00000000-0000-0000-002a-000000001016')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('6676e6ce-2e2d-4df3-9d9a-6ca1410dc1d2' ,'00000000-0000-0000-00ff-000000000115' ,11 ,1 ,N'2W20' ,N'Blätter' ,N'W6+9' ,N'Monate' ,NULL ,N'getrocknet als Tee (Verarbeitung +2)' ,'00000000-0000-0000-002a-000000003357')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('68fa7f09-4998-44ed-b4cf-6c717a8a4349' ,'00000000-0000-0000-00ff-000000000197' ,1 ,13 ,N'1' ,N'Portion Blätter' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003659')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('69f13500-b875-42e6-a12b-037b602ae00b' ,'00000000-0000-0000-00ff-000000000038' ,1 ,13 ,N'W6' ,N'Schoten' ,N'2W6+2' ,N'Tage' ,NULL ,N'Jede Schote enthält W3 Kerne, die einzeln W6 Tage haltbar sind.' ,'00000000-0000-0000-002a-000000003426')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('6b0cf997-bf92-4d43-a9cc-f595bcc39570' ,'00000000-0000-0000-00ff-000000000204' ,11 ,12 ,N'W6' ,N'Blüten' ,N'W6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003456')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('6c8806f1-a8ee-4a1b-a327-0fca450ff01b' ,'00000000-0000-0000-00ff-000000000102' ,2 ,4 ,N'1' ,N'Frucht' ,N'W6+2' ,N'Tage' ,NULL ,N'Um eine Frucht zu finden, muss eine Probe mit 12 TaP* gelingen.' ,'00000000-0000-0000-002a-000000001041')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('6eec81f6-c2fe-4369-8b7c-5b2401592477' ,'00000000-0000-0000-00ff-000000000053' ,1 ,13 ,N'1' ,N'Blüte' ,N'0' ,N'Tage' ,NULL ,N'Nur in vollem Mondlicht erntbar. Vergeht geplückt bei Monduntergang.' ,'00000000-0000-0000-002a-000000000995')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('6fb7f9ca-6c4c-4e5d-9310-ef40ef56197f' ,'00000000-0000-0000-00ff-000000000143' ,5.5 ,12.5 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'In Koromanthia.' ,'00000000-0000-0000-002a-000000003670')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('71ee1965-b5d9-4c07-bd30-23bb9a02dd68' ,'00000000-0000-0000-00ff-000000000002' ,1 ,12 ,N'12' ,N'Blätter' ,N'12' ,N'Stunden' ,NULL ,N'Die Blätter sind in der Farbe des Monats.' ,'00000000-0000-0000-002a-000000000947')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('72419eb7-ee50-4122-9864-a287da76afb5' ,'00000000-0000-0000-00ff-000000000057' ,1 ,13 ,N'1' ,N'ganze Pflanze' ,N'mehrere' ,N'Jahre' ,NULL ,N'trocken lagern' ,'00000000-0000-0000-002a-000000000999')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('72da00f9-457e-4922-b343-0d6dc05af118' ,'00000000-0000-0000-00ff-000000000074' ,12.5 ,12 ,N'1' ,N'Samenkapsel' ,N'13' ,N'Monate' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000001013')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('741e280c-e896-4f49-a36d-5764e6cd6694' ,'00000000-0000-0000-00ff-000000000190' ,1 ,13 ,N'1' ,N'Portion Pilz' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003653')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('75f1ddda-f12c-49f0-9367-9db052fe60f0' ,'00000000-0000-0000-00ff-000000000010' ,5 ,13 ,N'5' ,N'Blüten' ,N'5' ,N'Stunden' ,NULL ,N'Nach Ende der Haltbarkeit ist der Bütenduft verflogen. Im Herrschaftsbereich des Al´Anfaner Boron-Kultes hat die Kirche ein Monopol auf den Besitz der Pflanze, Zuwiderhandlungen werden strengstens bestraft. Unverarbeitete Blüten werden nicht verkauft, sind aber womöglich – ähnlich wie bei der Alveranie für jeden außer dem Pflücker wertlos.' ,'00000000-0000-0000-002a-000000000955')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('7638b3c4-351f-4c30-887e-bf594013da8b' ,'00000000-0000-0000-00ff-000000000090' ,11 ,4 ,N'1' ,N'ganze Pflanze' ,N'W6+18' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001029')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('76a85e05-ac5b-4299-8733-e8b84d4485cd' ,'00000000-0000-0000-00ff-000000000182' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003603')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('77033da6-c6dd-44b4-be9e-f03e3c876702' ,'00000000-0000-0000-00ff-000000000113' ,10 ,10 ,N'2W20+10' ,N'Blätter' ,N'W3+6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003665')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('77df0acd-1243-4482-8be2-720c3ef96a8d' ,'00000000-0000-0000-00ff-000000000096' ,3 ,4 ,N'W20' ,N'Blüten' ,N'3W6+24' ,N'Monate' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001035')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('7d0f963b-122a-41c3-97a9-548d43793052' ,'00000000-0000-0000-00ff-000000000148' ,1 ,13 ,N'1' ,N'Gift' ,N'1' ,N'Oktal' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003546')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('7da7f5d9-9ceb-46db-9171-e5d36475b2c9' ,'00000000-0000-0000-00ff-000000000018' ,1 ,13 ,N'4' ,N'Blätter' ,N'W3+7' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003410')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('7def6eaa-ebf4-4b00-9287-31b33bbde361' ,'00000000-0000-0000-00ff-000000000121' ,10.001 ,10.033 ,N'1' ,N'Portion Sporen' ,N'2' ,N'Oktale' ,NULL ,N'leicht feucht aufbewahren' ,'00000000-0000-0000-002a-000000003528')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('7dfc190d-867d-45ad-b6e1-a68b37bd8b84' ,'00000000-0000-0000-00ff-000000000030' ,2 ,2 ,N'1' ,N'Samenkapsel' ,N'W6+18' ,N'Monate' ,NULL ,N'Samenkapsel trocken aufbewahrt' ,'00000000-0000-0000-002a-000000000974')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('7f2294a2-00ca-4b03-b862-820ffe9d52b1' ,'00000000-0000-0000-00ff-000000000140' ,1 ,13 ,N'1' ,N'Portion' ,N'1' ,N'Oktal' ,NULL ,N'leicht feucht aufbewahren' ,'00000000-0000-0000-002a-000000003541')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('809102fa-3ccd-4d1a-bff3-56f478a114cb' ,'00000000-0000-0000-00ff-000000000046' ,1 ,13 ,N'1W3*20' ,N'Blätter' ,N'1W6+1' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000990')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('830f5e80-a772-43ec-a7b9-797eb5fbefac' ,'00000000-0000-0000-00ff-000000000126' ,2 ,2.5 ,N'1' ,N'Spann Blütenblätter' ,N'einige' ,N'Nonen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003662')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('866967d8-5a35-42e6-93ce-46c5c9655a07' ,'00000000-0000-0000-00ff-000000000064' ,1 ,13 ,N'1' ,N'Staude' ,N'1W6+4' ,N'Tage' ,NULL ,N'Im Süden' ,'00000000-0000-0000-002a-000000001005')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('88a005ec-6d9d-4082-a969-8b44b862ac22' ,'00000000-0000-0000-00ff-000000000118' ,2 ,2 ,N'W6' ,N'Samenkapseln' ,N'2W6+18' ,N'Monate' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000003645')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8ad73e50-edde-4ab7-9d65-89d7b6c207e3' ,'00000000-0000-0000-00ff-000000000009' ,1 ,13 ,N'W20+2' ,N'Zweige pro 10 AsP der Quelle' ,N'W3' ,N'Spielrunden' ,NULL ,N'Die Haltbarkeit ist nahezu unbegrenzt in der Nähe einer magischen Quelle (jede Woche W20: fällt eine 20, geht das Blutblatt ein).' ,'00000000-0000-0000-002a-000000000954')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8b7fa35f-b3db-41e9-b2f0-88bcc9449385' ,'00000000-0000-0000-00ff-000000000199' ,1 ,13 ,N'1' ,N'Portion Sporen' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003660')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8b994437-b37d-43bd-9e29-1585204e19d1' ,'00000000-0000-0000-00ff-000000000088' ,1 ,13 ,N'W6' ,N'Flechten' ,N'1' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001027')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8c14c639-5b96-44c2-9a94-a8ba8e4a4223' ,'00000000-0000-0000-00ff-000000000076' ,11 ,4 ,N'W3+2' ,N'Beeren' ,N'1W6+12' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001015')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8d58ae57-4133-4d66-95b2-f8c984e6fc0f' ,'00000000-0000-0000-00ff-000000000142' ,12.6 ,1 ,N'' ,N'Blüten' ,N'0,5' ,N'Nonen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003542')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8e94e76b-1f72-4f9b-8108-94716a5221a5' ,'00000000-0000-0000-00ff-000000000061' ,8 ,5 ,N'1' ,N'Ranke' ,NULL ,N'unbegrenzt' ,NULL ,N'Ein Ranke hat 2 bis 3 Knoten. Bei sachgemäßer Lagerung praktisch unbegrenzt haltbar.' ,'00000000-0000-0000-002a-000000001002')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8f3bca8e-84be-4180-98ba-3da8b9d1a967' ,'00000000-0000-0000-00ff-000000000119' ,1 ,13 ,N'1' ,N'Portionen Rinde' ,N'3' ,N'Oktale' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003526')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8faf539c-49d7-4ea3-a75b-eecff3ad0440' ,'00000000-0000-0000-00ff-000000000034' ,1 ,13 ,N'1W20' ,N'Unzen' ,N'1W6' ,N'Tage' ,1 ,NULL ,'00000000-0000-0000-002a-000000000978')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('8fd442f7-7f66-4b41-b533-b81550d4a692' ,'00000000-0000-0000-00ff-000000000143' ,7 ,9 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'Nach Regen im Winter in Rhacornos.' ,'00000000-0000-0000-002a-000000003543')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('916e2bf2-f6fc-41e7-8296-9805734d55d2' ,'00000000-0000-0000-00ff-000000000143' ,4 ,6 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'In Rhacornos.' ,'00000000-0000-0000-002a-000000003670')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('91753435-b0ce-4959-a0d3-abc1e6134429' ,'00000000-0000-0000-00ff-000000000014' ,3 ,3 ,N'W6' ,N'Blüten' ,N'2W6+18' ,N'Stunden' ,NULL ,N'Jede Blüte hat einen Stempel.' ,'00000000-0000-0000-002a-000000000959')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('929d24f4-98dd-4233-bd86-66c8f5dc8871' ,'00000000-0000-0000-00ff-000000000044' ,1 ,13 ,N'1' ,N'Wurzel' ,N'1' ,N'Monat' ,NULL ,N'Die Wurzel enthält bis zu W6 Maß klares Wasser.' ,'00000000-0000-0000-002a-000000000988')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('9510af46-196e-4853-8605-937579d7d37f' ,'00000000-0000-0000-00ff-000000000177' ,11.5 ,5.5 ,N'1' ,N'Apfel' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003598')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('9620154b-d5ce-487e-8977-f950d2119e81' ,'00000000-0000-0000-00ff-000000000037' ,1 ,13 ,N'1W20' ,N'halbe Stein der Ranken' ,N'W6+6' ,N'Monate' ,20 ,NULL ,'00000000-0000-0000-002a-000000000981')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('97282c54-d191-4ba9-b10d-f0f6fd1163f4' ,'00000000-0000-0000-00ff-000000000143' ,12.5 ,5.5 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'Zur Regenzeit in Koromanthia.' ,'00000000-0000-0000-002a-000000003543')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('99382400-8cb3-4433-8df7-317bd4315886' ,'00000000-0000-0000-00ff-000000000203' ,1 ,13 ,N'1' ,N'Pflanze' ,N'W6+24' ,N'Stunden' ,20 ,NULL ,'00000000-0000-0000-002a-000000003666')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('9a48ea4b-d22a-4413-91cf-f16601a0e8c1' ,'00000000-0000-0000-00ff-000000000067' ,1 ,13 ,N'W3' ,N'Moosballen' ,N'2W6+12' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001007')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('9b47b357-1ecd-4a9e-8805-7f038dbd6914' ,'00000000-0000-0000-00ff-000000000092' ,4 ,4 ,N'1' ,N'Samenkapsel' ,N'W6+9' ,N'Monate' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000001031')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('9d419774-7a69-4901-a850-b2067a9b89c6' ,'00000000-0000-0000-00ff-000000000093' ,1 ,13 ,N'W6' ,N'Flechten' ,N'1W6' ,N'Tage' ,NULL ,N'feucht aufbewahren' ,'00000000-0000-0000-002a-000000001032')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('9fc4b79d-6ad1-4053-8bd4-fbf31fdcf83f' ,'00000000-0000-0000-00ff-000000000091' ,10 ,4 ,N'W6+2' ,N'Blätter' ,N'2W6+36' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001030')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('9fe461c5-dc88-440a-9abe-65f712d64d23' ,'00000000-0000-0000-00ff-000000000115' ,9 ,1 ,N'W3' ,N'Flux Saft' ,N'W3+6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003667')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('a0eb485d-1ddd-4e66-97d3-1edd1875a662' ,'00000000-0000-0000-00ff-000000000176' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003597')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('a2ea20fd-9887-4ce0-8bca-dfcfc505c8b8' ,'00000000-0000-0000-00ff-000000000143' ,10 ,13 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'In Rhacornos.' ,'00000000-0000-0000-002a-000000003670')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('a64cfa0b-4cc0-4e01-bf59-9a17a9e605c4' ,'00000000-0000-0000-00ff-000000000105' ,11 ,1 ,N'W3' ,N'Blüten' ,N'W3+6' ,N'Monate' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001043')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('aa64f6e7-b13a-458e-9511-778d1a5b3755' ,'00000000-0000-0000-00ff-000000000143' ,11.6 ,5.5 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'Zur Regenzeit in Mayenios.' ,'00000000-0000-0000-002a-000000003543')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('abdd8ffc-4459-442f-8633-80063db8b529' ,'00000000-0000-0000-00ff-000000000200' ,1 ,13 ,N'1' ,N'Wurzel' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003661')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ac0a4de8-a3de-4356-9bca-7baa3f238d02' ,'00000000-0000-0000-00ff-000000000143' ,5.5 ,11.6 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'In Mayenios.' ,'00000000-0000-0000-002a-000000003670')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ad435a6c-ad67-48f6-84b2-841b691e0afd' ,'00000000-0000-0000-00ff-000000000113' ,1 ,1 ,N'W20+2' ,N'Blüten' ,N'W3+6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003356')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('aef0aafb-4b62-4b5e-9338-b87ab608b500' ,'00000000-0000-0000-00ff-000000000028' ,5 ,7 ,N'5' ,N'Bast' ,N'W3+4' ,N'Monate' ,NULL ,N'Der Bast muss noch mit einer Probe +3 weiterverarbeitet werden.' ,'00000000-0000-0000-002a-000000001071')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('b30ef409-ee25-4a2b-aa1e-c64048aa10c5' ,'00000000-0000-0000-00ff-000000000120' ,6 ,7 ,N'W2' ,N'Portionen' ,N'0,5' ,N'Jahre' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000003527')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('b33df555-3903-42e0-83cf-138af79dee16' ,'00000000-0000-0000-00ff-000000000143' ,1 ,3 ,N'2W6' ,N'Stein Holz' ,NULL ,N'unbegrenzt' ,NULL ,N'Nach Regen im Sommer in Rhacornos.' ,'00000000-0000-0000-002a-000000003543')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('b342690c-72b9-42ce-8998-56f3b432de8b' ,'00000000-0000-0000-00ff-000000000154' ,1 ,13 ,N'1' ,N'Wurzelextrakt' ,N'1' ,N'Oktal' ,NULL ,N'leicht feucht aufbewahren' ,'00000000-0000-0000-002a-000000003549')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('b46d9b3d-96f3-4f55-a986-40e406893ecc' ,'00000000-0000-0000-00ff-000000000085' ,11 ,1 ,N'1' ,N'Blüte' ,N'1W3+3' ,N'Tage' ,NULL ,N'In Wasser aufbewahren' ,'00000000-0000-0000-002a-000000001024')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('b4f20ec5-6d5d-474d-9bb0-7a2a9053e510' ,'00000000-0000-0000-00ff-000000000036' ,2 ,3 ,N'2W6*(2W6+3)' ,N'Blätter' ,N'W6' ,N'Tage' ,NULL ,N'2W6 Sträucher mit der jeweiligen Anzahl Beeren und Blätter.' ,'00000000-0000-0000-002a-000000000980')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('b5f428ea-43e0-4bbb-9fe5-f1295ffc4898' ,'00000000-0000-0000-00ff-000000000205' ,1 ,4 ,N'2W6' ,N'Pilz' ,N'W6+24' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003457')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ba3893f0-50d3-4392-acae-bffe2ce90c8a' ,'00000000-0000-0000-00ff-000000000185' ,1 ,13 ,N'1' ,N'Portion' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003648')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ba8291e0-362a-49d3-9497-caea0586af93' ,'00000000-0000-0000-00ff-000000000152' ,1 ,2.5 ,N'1' ,N'Flaum' ,N'0,5' ,N'Jahre' ,1 ,NULL ,'00000000-0000-0000-002a-000000003672')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('bb2b3fa6-38e5-4be4-93e8-9790d9fe0473' ,'00000000-0000-0000-00ff-000000000110' ,1 ,13 ,N'W6' ,N'Pilze' ,N'W6+2' ,N'Monate' ,40 ,NULL ,'00000000-0000-0000-002a-000000001048')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('be403aae-ede0-4347-b9ff-81172e108666' ,'00000000-0000-0000-00ff-000000000097' ,9 ,6 ,N'1W6' ,N'Beeren' ,N'W3+2' ,N'Tage' ,NULL ,N'Werden an einem Tag 2 Beeren eingenommen, besteht bei 1-2 auf W20 die Möglichkeit einer Sucht (Krankheit Stufe 10), die mit jeder weiteren Beere um 5 % steigt. Ein Süchtiger, der nicht einmal in der Woche Vierblatt zu sich nehmen kann, verliert 1W6 LeP. Vierblatt hat für ihn keine heilsame Wirkung mehr.' ,'00000000-0000-0000-002a-000000001036')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('bf00c8d6-1d14-4b1c-881f-96f3413634c3' ,'00000000-0000-0000-00ff-000000000028' ,10 ,10 ,N'W20' ,N'Triebe' ,N'W3+3' ,N'Monate' ,NULL ,N'Versetzt mit Orazal ist die Haltbarkeit doppelt so lang.' ,'00000000-0000-0000-002a-000000000972')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c1cb7b27-e6c1-4a27-a89f-b93b513df731' ,'00000000-0000-0000-00ff-000000000078' ,2 ,11 ,N'1' ,N'Pilz' ,N'W6+48' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001017')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c30f4653-2732-4627-830b-ee3cb70877c3' ,'00000000-0000-0000-00ff-000000000117' ,1 ,13 ,N'1' ,N'Saft einer Pflanze' ,N'W3+6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003359')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c3470b65-b541-40e1-9955-8c9765132471' ,'00000000-0000-0000-00ff-000000000042' ,1 ,13 ,N'1' ,N'Halm' ,N'W3+3' ,N'Wochen' ,NULL ,N'Im eigenen Wasser aufbewahren.' ,'00000000-0000-0000-002a-000000000986')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c3a7e3f8-3a04-4876-9f8b-36a517ce1b65' ,'00000000-0000-0000-00ff-000000000047' ,2 ,3 ,N'1' ,N'Frucht' ,N'1' ,N'Woche' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000991')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c460887b-c939-4949-ab6a-bd746e632443' ,'00000000-0000-0000-00ff-000000000041' ,12 ,5 ,N'1' ,N'Wurzel' ,N'W3+2' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000985')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c5d5b448-c493-437c-ac32-291f1c1ca01c' ,'00000000-0000-0000-00ff-000000000080' ,10 ,12 ,N'W6' ,N'Blüten' ,N'W6' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001019')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c6e4fffd-25fc-4c33-ae5b-84f60e5202b1' ,'00000000-0000-0000-00ff-000000000152' ,7 ,8.5 ,N'1' ,N'Blüten' ,N'0,5' ,N'Jahre' ,1 ,N'eingelegt aufbewahren' ,'00000000-0000-0000-002a-000000003548')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('c76e7a90-5696-4b3c-9cdd-f8b22cfa74ae' ,'00000000-0000-0000-00ff-000000000043' ,9 ,4 ,N'2W6' ,N'Knospen' ,N'W6+10' ,N'Tage' ,NULL ,N'Die Haipu pflücken immer höchstens die Hälfte der Knospen ab, damit der Strauch geschont wird.' ,'00000000-0000-0000-002a-000000000987')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('cc8d31da-9e9c-4038-a44f-44be7fb8746a' ,'00000000-0000-0000-00ff-000000000163' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003584')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ccffbf92-355c-4ac9-a38c-05a74c0b691a' ,'00000000-0000-0000-00ff-000000000086' ,11 ,2 ,N'W20' ,N'Knollen' ,N'W6+2' ,N'Tage' ,NULL ,N'Die Blätter sind ebenfalls verwertbar und halten sich W3+1 Tage.' ,'00000000-0000-0000-002a-000000001025')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('cdf7d97c-643b-4609-8db8-1eb930ce6ab8' ,'00000000-0000-0000-00ff-000000000015' ,1 ,13 ,N'1W3*(3W6+8)' ,N'Stacheln' ,N'3W6' ,N'Tage' ,NULL ,N'pro Stein Kaktusfleisch 3W6+8 Stacheln' ,'00000000-0000-0000-002a-000000003409')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('cea9c12e-c4c9-4f7b-9464-0dee62dde528' ,'00000000-0000-0000-00ff-000000000124' ,10 ,6 ,N'1' ,N'Portion Sporen' ,N'wenige' ,N'Nonen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003529')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ceddbd48-edd5-4a17-a61d-bc8b1ae40000' ,'00000000-0000-0000-00ff-000000000111' ,6 ,1 ,N'12' ,N'Stängel' ,N'W6+9' ,N'Stunden' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001049')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d115115f-c932-4294-a043-18b880dab4ae' ,'00000000-0000-0000-00ff-000000000047' ,11 ,12 ,N'1' ,N'Frucht' ,N'1' ,N'Woche' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000991')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d35d9630-feae-4e1f-9337-76bc414e1be4' ,'00000000-0000-0000-00ff-000000000049' ,3 ,6 ,N'1' ,N'Sporen' ,N'1W6' ,N'Tage' ,0.1 ,NULL ,'00000000-0000-0000-002a-000000000992')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d39b99ce-bc09-4543-8a9a-6f00b3c59c8e' ,'00000000-0000-0000-00ff-000000000025' ,1 ,13 ,N'1' ,N'Schank' ,N'2W6' ,N'Wochen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000969')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d76828f2-80a8-4f91-a2ea-bee45731f348' ,'00000000-0000-0000-00ff-000000000115' ,3 ,4 ,N'W20' ,N'Früchte' ,N'W6+9' ,N'Monate' ,NULL ,N'getrocknet als Tee (Verarbeitung +2)' ,'00000000-0000-0000-002a-000000003357')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d81d3796-be5b-4d16-9423-a01d66a0fbe7' ,'00000000-0000-0000-00ff-000000000186' ,1 ,13 ,N'1' ,N'Portion' ,NULL ,N'Minuten' ,NULL ,N'muss vor Ort verzehrt werden' ,'00000000-0000-0000-002a-000000003649')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d8b24ba9-0347-4125-896a-ef911b09d14e' ,'00000000-0000-0000-00ff-000000000166' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003587')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d91677b9-4898-43b1-9822-9f5c5ca7ce27' ,'00000000-0000-0000-00ff-000000000109' ,10 ,1 ,N'3W20' ,N'Blätter' ,N'1' ,N'Tag' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001047')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('d9d74863-45ba-4066-82cb-1a12e228edf9' ,'00000000-0000-0000-00ff-000000000169' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003590')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('da102da0-b7e5-4fed-a928-8614603ba6f0' ,'00000000-0000-0000-00ff-000000000089' ,1 ,13 ,N'1' ,N'ganze Pflanze' ,N'1W6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001028')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('da702413-374a-4fc8-9d50-978387bea1df' ,'00000000-0000-0000-00ff-000000000070' ,10 ,12 ,N'1' ,N'Pilzhaut' ,N'W6' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001010')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('da81b9f3-88f4-4895-99a5-908cca143790' ,'00000000-0000-0000-00ff-000000000178' ,1 ,13 ,N'1' ,N'Portion' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003599')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('dacdc742-f755-4353-9c22-2837fe3f9d12' ,'00000000-0000-0000-00ff-000000000157' ,1 ,13 ,N'x' ,N'Rinde' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003578')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('dace3e45-aa48-4634-abab-5a7e98804295' ,'00000000-0000-0000-00ff-000000000134' ,12.5 ,11.5 ,N'1' ,N'Saft' ,N'0,5' ,N'Jahre' ,NULL ,N'trocken aufbewahren und regelmäßig schütteln' ,'00000000-0000-0000-002a-000000003536')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('dc963a75-f2cf-4b97-9f13-243ab6ce801f' ,'00000000-0000-0000-00ff-000000000191' ,1 ,13 ,N'1' ,N'Wurzel' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003654')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('dcf31f76-3b6b-41aa-9f8e-64ed9390e3be' ,'00000000-0000-0000-00ff-000000000194' ,1 ,13 ,N'1' ,N'Stange' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003656')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ddc19d23-cedb-494f-b035-cfaaef694f9a' ,'00000000-0000-0000-00ff-000000000168' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003589')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('deb5033b-6a91-49b1-b19e-9a4ea24ca489' ,'00000000-0000-0000-00ff-000000000156' ,2.5 ,3 ,N'6' ,N'Samen' ,N'1' ,N'Jahr' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000003550')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('e175c155-02ab-430a-9134-da8ec7cd2468' ,'00000000-0000-0000-00ff-000000000181' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003602')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('e2c053d8-3f47-477c-bd8d-441077eaa8e0' ,'00000000-0000-0000-00ff-000000000131' ,1 ,13 ,N'1' ,N'Holzpulver' ,N'1' ,N'Jahr' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003535')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('e441e2fc-3e0a-4eb3-bb11-a12d66aee967' ,'00000000-0000-0000-00ff-000000000060' ,10 ,5 ,N'1' ,N'Wurzelknolle' ,N'W3+4' ,N'Wochen' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001001')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('e4a6efe0-a675-45e0-ae7f-41fb6ad9b236' ,'00000000-0000-0000-00ff-000000000006' ,1 ,13 ,N'70' ,N'Stein Rinde' ,N'W6+2' ,N'Wochen' ,2800 ,NULL ,'00000000-0000-0000-002a-000000000951')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('e667a714-cce5-4574-9549-5c3989d3f5a0' ,'00000000-0000-0000-00ff-000000000129' ,1 ,13 ,N'1' ,N'Pflanze' ,N'2' ,N'Jahre' ,NULL ,N'trocken aufbewahren' ,'00000000-0000-0000-002a-000000003532')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('e8a6f141-30e7-48e6-9e5e-684c63509065' ,'00000000-0000-0000-00ff-000000000104' ,11 ,1 ,N'W6+1' ,N'Blüten' ,N'W6+2' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000001042')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('e8f4fd44-9d58-43ba-a848-95ed4af03f7b' ,'00000000-0000-0000-00ff-000000000023' ,1 ,13 ,N'2W6' ,N'Pilzhäute' ,N'W6+1' ,N'Tage' ,NULL ,N'Im Norden ist die Ernte nur von Efferd bis Boron möglich.' ,'00000000-0000-0000-002a-000000000968')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('eacfeb09-0c3c-49bd-bdb8-7a2718af1aa6' ,'00000000-0000-0000-00ff-000000000011' ,1 ,13 ,N'1' ,N'Pollen' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003405')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ebf55f9e-3d90-41eb-b4a8-be3c980e8e73' ,'00000000-0000-0000-00ff-000000000049' ,10 ,1 ,N'1' ,N'Sporen' ,N'1W6' ,N'Tage' ,0.1 ,NULL ,'00000000-0000-0000-002a-000000000992')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('f503d1c1-652e-472f-8d05-0ac44b82537c' ,'00000000-0000-0000-00ff-000000000095' ,1 ,13 ,N'W6+3' ,N'Kelche' ,N'W6' ,N'Tage' ,NULL ,N'Bleiben 13 TaP* übrig, kann die Tuur-Amash-Beere gefunden werden. Diese hält sich W6 Monate.' ,'00000000-0000-0000-002a-000000001034')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('f53a628a-b58e-4644-a040-c607877c3da7' ,'00000000-0000-0000-00ff-000000000192' ,1 ,13 ,N'1' ,N'kleine Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003655')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('f559607c-aded-4780-8bb3-86d0f9bd6f23' ,'00000000-0000-0000-00ff-000000000015' ,1 ,13 ,N'1W6' ,N'halbe Stein Kaktusfleisch' ,N'W6' ,N'Tage' ,20 ,N'pro Stein Kaktusfleisch 3W6+8 Stacheln' ,'00000000-0000-0000-002a-000000003408')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('f622a80d-b98c-4917-a429-3958205870aa' ,'00000000-0000-0000-00ff-000000000052' ,12 ,2 ,N'1' ,N'Blüte' ,N'W3+5' ,N'Tage' ,NULL ,NULL ,'00000000-0000-0000-002a-000000000994')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('f6cadd91-09d5-4a91-8074-45894ec42f7e' ,'00000000-0000-0000-00ff-000000000139' ,1 ,13 ,N'1' ,N'Trieb' ,N'1' ,N'Tag' ,NULL ,N'erfordert bei großen Wildbäumen
jedoch eine Klettern-Probe (-2 bis +6)' ,'00000000-0000-0000-002a-000000003540')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('fac9e7eb-d3bd-4577-a48d-25205f9ac566' ,'00000000-0000-0000-00ff-000000000164' ,1 ,13 ,N'1' ,N'Pflanze' ,NULL ,NULL ,NULL ,NULL ,'00000000-0000-0000-002a-000000003585')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('fb134616-322f-496f-b146-c1f2534b01ba' ,'00000000-0000-0000-00ff-000000000098' ,3 ,5 ,N'1' ,N'Wurzel' ,N'W+7' ,N'Tage' ,NULL ,N'Südaventurien' ,'00000000-0000-0000-002a-000000001037')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('fee5fab6-32c1-4874-b0e4-3bbb6138f240' ,'00000000-0000-0000-00ff-000000000027' ,1 ,13 ,N'W6' ,N'Stein Algen' ,N'24' ,N'Stunden' ,40 ,NULL ,'00000000-0000-0000-002a-000000000971')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('fefb8981-a811-4db2-8d87-c78852ff4703' ,'00000000-0000-0000-00ff-000000000146' ,1 ,13 ,N'1' ,N'Pflanze' ,N'2' ,N'Nonen' ,NULL ,N'recht für eine Mahlzeit' ,'00000000-0000-0000-002a-000000003545')
GO

INSERT INTO [Pflanze_Ernte] (  [ID],  [PflanzeGUID],  [Von],  [Bis],  [Grundmenge],  [Pflanzenteil],  [Haltbarkeit],  [HaltbarkeitEinheit],  [Gewicht],  [Bemerkung],  [HandelsgutGUID]) 
 VALUES ('ff44545b-d0c2-4866-8a3d-f4c588e60cd8' ,'00000000-0000-0000-00ff-000000000112' ,5 ,5 ,N'W6' ,N'Nüsse' ,N'W6+7' ,N'Monate' ,NULL ,NULL ,'00000000-0000-0000-002a-000000003355')
GO



/* Pflanze_Gebiet */

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000001' ,'00000000-0000-0000-00f9-000000000052')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000002' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000003' ,'00000000-0000-0000-00f9-000000000082')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000003' ,'00000000-0000-0000-00f9-000000000159')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000003' ,'00000000-0000-0000-00f9-000000000160')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000004' ,'00000000-0000-0000-00f9-000000000010')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00f9-000000000034')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00f9-000000000064')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00f9-000000000162')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00f9-000000000163')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000006' ,'00000000-0000-0000-00f9-000000000037')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000007' ,'00000000-0000-0000-00f9-000000000056')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000008' ,'00000000-0000-0000-00f9-000000000011')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000009' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000010' ,'00000000-0000-0000-00f9-000000000047')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000010' ,'00000000-0000-0000-00f9-000000000069')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000011' ,'00000000-0000-0000-00f9-000000000047')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000011' ,'00000000-0000-0000-00f9-000000000069')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,'00000000-0000-0000-00f9-000000000030')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,'00000000-0000-0000-00f9-000000000047')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,'00000000-0000-0000-00f9-000000000048')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,'00000000-0000-0000-00f9-000000000149')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,'00000000-0000-0000-00f9-000000000008')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,'00000000-0000-0000-00f9-000000000031')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,'00000000-0000-0000-00f9-000000000157')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,'00000000-0000-0000-00f9-000000000075')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,'00000000-0000-0000-00f9-000000000150')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,'00000000-0000-0000-00f9-000000000151')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,'00000000-0000-0000-00f9-000000000164')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000016' ,'00000000-0000-0000-00f9-000000000042')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000017' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000018' ,'00000000-0000-0000-00f9-000000000073')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000018' ,'00000000-0000-0000-00f9-000000000076')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000018' ,'00000000-0000-0000-00f9-000000000156')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000019' ,'00000000-0000-0000-00f9-000000000023')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000020' ,'00000000-0000-0000-00f9-000000000045')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000021' ,'00000000-0000-0000-00f9-000000000030')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000021' ,'00000000-0000-0000-00f9-000000000047')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000022' ,'00000000-0000-0000-00f9-000000000043')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000023' ,'00000000-0000-0000-00f9-000000000025')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000025' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000026' ,'00000000-0000-0000-00f9-000000000030')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000026' ,'00000000-0000-0000-00f9-000000000047')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000027' ,'00000000-0000-0000-00f9-000000000035')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000028' ,'00000000-0000-0000-00f9-000000000070')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000029' ,'00000000-0000-0000-00f9-000000000084')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000030' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000032' ,'00000000-0000-0000-00f9-000000000056')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000032' ,'00000000-0000-0000-00f9-000000000059')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000032' ,'00000000-0000-0000-00f9-000000000169')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000033' ,'00000000-0000-0000-00f9-000000000051')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000034' ,'00000000-0000-0000-00f9-000000000021')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000035' ,'00000000-0000-0000-00f9-000000000005')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000035' ,'00000000-0000-0000-00f9-000000000140')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000035' ,'00000000-0000-0000-00f9-000000000148')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000035' ,'00000000-0000-0000-00f9-000000000149')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000036' ,'00000000-0000-0000-00f9-000000000002')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000036' ,'00000000-0000-0000-00f9-000000000145')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000037' ,'00000000-0000-0000-00f9-000000000077')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000037' ,'00000000-0000-0000-00f9-000000000160')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000038' ,'00000000-0000-0000-00f9-000000000076')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000039' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,'00000000-0000-0000-00f9-000000000040')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,'00000000-0000-0000-00f9-000000000049')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,'00000000-0000-0000-00f9-000000000080')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,'00000000-0000-0000-00f9-000000000002')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,'00000000-0000-0000-00f9-000000000054')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,'00000000-0000-0000-00f9-000000000145')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,'00000000-0000-0000-00f9-000000000152')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000042' ,'00000000-0000-0000-00f9-000000000046')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000042' ,'00000000-0000-0000-00f9-000000000170')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000042' ,'00000000-0000-0000-00f9-000000000171')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000043' ,'00000000-0000-0000-00f9-000000000004')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000043' ,'00000000-0000-0000-00f9-000000000146')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000043' ,'00000000-0000-0000-00f9-000000000147')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000044' ,'00000000-0000-0000-00f9-000000000062')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000044' ,'00000000-0000-0000-00f9-000000000140')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000044' ,'00000000-0000-0000-00f9-000000000148')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000045' ,'00000000-0000-0000-00f9-000000000056')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000045' ,'00000000-0000-0000-00f9-000000000058')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000045' ,'00000000-0000-0000-00f9-000000000155')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000046' ,'00000000-0000-0000-00f9-000000000087')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000047' ,'00000000-0000-0000-00f9-000000000078')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000049' ,'00000000-0000-0000-00f9-000000000015')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000051' ,'00000000-0000-0000-00f9-000000000017')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000052' ,'00000000-0000-0000-00f9-000000000029')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000053' ,'00000000-0000-0000-00f9-000000000030')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000054' ,'00000000-0000-0000-00f9-000000000033')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000055' ,'00000000-0000-0000-00f9-000000000034')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000055' ,'00000000-0000-0000-00f9-000000000148')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000055' ,'00000000-0000-0000-00f9-000000000161')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,'00000000-0000-0000-00f9-000000000041')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,'00000000-0000-0000-00f9-000000000140')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,'00000000-0000-0000-00f9-000000000150')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,'00000000-0000-0000-00f9-000000000158')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000057' ,'00000000-0000-0000-00f9-000000000028')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000058' ,'00000000-0000-0000-00f9-000000000007')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,'00000000-0000-0000-00f9-000000000032')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,'00000000-0000-0000-00f9-000000000166')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,'00000000-0000-0000-00f9-000000000167')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,'00000000-0000-0000-00f9-000000000168')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000061' ,'00000000-0000-0000-00f9-000000000086')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000061' ,'00000000-0000-0000-00f9-000000000165')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000063' ,'00000000-0000-0000-00f9-000000000053')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000064' ,'00000000-0000-0000-00f9-000000000025')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000066' ,'00000000-0000-0000-00f9-000000000085')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000067' ,'00000000-0000-0000-00f9-000000000065')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000068' ,'00000000-0000-0000-00f9-000000000071')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000069' ,'00000000-0000-0000-00f9-000000000056')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000069' ,'00000000-0000-0000-00f9-000000000060')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000069' ,'00000000-0000-0000-00f9-000000000152')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000070' ,'00000000-0000-0000-00f9-000000000051')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000072' ,'00000000-0000-0000-00f9-000000000056')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000072' ,'00000000-0000-0000-00f9-000000000057')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000073' ,'00000000-0000-0000-00f9-000000000017')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00f9-000000000036')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00f9-000000000048')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00f9-000000000069')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00f9-000000000149')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000075' ,'00000000-0000-0000-00f9-000000000044')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000076' ,'00000000-0000-0000-00f9-000000000074')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000077' ,'00000000-0000-0000-00f9-000000000022')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000079' ,'00000000-0000-0000-00f9-000000000037')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000079' ,'00000000-0000-0000-00f9-000000000039')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000080' ,'00000000-0000-0000-00f9-000000000070')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000081' ,'00000000-0000-0000-00f9-000000000013')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000082' ,'00000000-0000-0000-00f9-000000000066')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000082' ,'00000000-0000-0000-00f9-000000000067')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000083' ,'00000000-0000-0000-00f9-000000000017')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000084' ,'00000000-0000-0000-00f9-000000000063')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000086' ,'00000000-0000-0000-00f9-000000000027')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000087' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000088' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000089' ,'00000000-0000-0000-00f9-000000000037')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000090' ,'00000000-0000-0000-00f9-000000000026')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000091' ,'00000000-0000-0000-00f9-000000000050')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,'00000000-0000-0000-00f9-000000000030')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,'00000000-0000-0000-00f9-000000000047')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,'00000000-0000-0000-00f9-000000000048')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,'00000000-0000-0000-00f9-000000000149')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,'00000000-0000-0000-00f9-000000000057')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,'00000000-0000-0000-00f9-000000000068')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,'00000000-0000-0000-00f9-000000000153')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,'00000000-0000-0000-00f9-000000000154')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000094' ,'00000000-0000-0000-00f9-000000000037')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000095' ,'00000000-0000-0000-00f9-000000000066')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000096' ,'00000000-0000-0000-00f9-000000000014')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000097' ,'00000000-0000-0000-00f9-000000000012')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000098' ,'00000000-0000-0000-00f9-000000000017')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000098' ,'00000000-0000-0000-00f9-000000000024')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000100' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000101' ,'00000000-0000-0000-00f9-000000000020')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000102' ,'00000000-0000-0000-00f9-000000000009')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000102' ,'00000000-0000-0000-00f9-000000000018')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000104' ,'00000000-0000-0000-00f9-000000000017')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000105' ,'00000000-0000-0000-00f9-000000000017')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000106' ,'00000000-0000-0000-00f9-000000000061')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000106' ,'00000000-0000-0000-00f9-000000000140')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000106' ,'00000000-0000-0000-00f9-000000000149')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000107' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000108' ,'00000000-0000-0000-00f9-000000000083')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00f9-000000000003')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00f9-000000000006')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00f9-000000000140')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00f9-000000000149')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00f9-000000000150')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00f9-000000000151')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000110' ,'00000000-0000-0000-00f9-000000000019')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000111' ,'00000000-0000-0000-00f9-000000000025')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000112' ,'00000000-0000-0000-00f9-000000000037')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000112' ,'00000000-0000-0000-00f9-000000000038')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000113' ,'00000000-0000-0000-00f9-000000000081')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000115' ,'00000000-0000-0000-00f9-000000000016')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000117' ,'00000000-0000-0000-00f9-000000000037')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000117' ,'00000000-0000-0000-00f9-000000000055')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000118' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000119' ,'00000000-0000-0000-00f9-000000030089')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000120' ,'00000000-0000-0000-00f9-000000030004')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000120' ,'00000000-0000-0000-00f9-000000030005')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000120' ,'00000000-0000-0000-00f9-000000030006')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000120' ,'00000000-0000-0000-00f9-000000030131')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000121' ,'00000000-0000-0000-00f9-000000030003')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000121' ,'00000000-0000-0000-00f9-000000030106')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000121' ,'00000000-0000-0000-00f9-000000030142')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000124' ,'00000000-0000-0000-00f9-000000030115')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000126' ,'00000000-0000-0000-00f9-000000030121')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000126' ,'00000000-0000-0000-00f9-000000030143')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000129' ,'00000000-0000-0000-00f9-000000030002')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000129' ,'00000000-0000-0000-00f9-000000030108')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000129' ,'00000000-0000-0000-00f9-000000030109')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000130' ,'00000000-0000-0000-00f9-000000030115')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000131' ,'00000000-0000-0000-00f9-000000030100')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000134' ,'00000000-0000-0000-00f9-000000030102')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000135' ,'00000000-0000-0000-00f9-000000030136')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000138' ,'00000000-0000-0000-00f9-000000030137')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000139' ,'00000000-0000-0000-00f9-000000030114')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000140' ,'00000000-0000-0000-00f9-000000030143')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000142' ,'00000000-0000-0000-00f9-000000030101')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000143' ,'00000000-0000-0000-00f9-000000030115')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000145' ,'00000000-0000-0000-00f9-000000030119')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000146' ,'00000000-0000-0000-00f9-000000030115')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000146' ,'00000000-0000-0000-00f9-000000030125')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000148' ,'00000000-0000-0000-00f9-000000030011')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000148' ,'00000000-0000-0000-00f9-000000030118')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000150' ,'00000000-0000-0000-00f9-000000030130')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000152' ,'00000000-0000-0000-00f9-000000030143')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000154' ,'00000000-0000-0000-00f9-000000030092')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000156' ,'00000000-0000-0000-00f9-000000030131')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000157' ,'00000000-0000-0000-00f9-000000030121')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000157' ,'00000000-0000-0000-00f9-000000030143')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000158' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000159' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000160' ,'00000000-0000-0000-00f9-000000030108')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000161' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000162' ,'00000000-0000-0000-00f9-000000030103')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000163' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000164' ,'00000000-0000-0000-00f9-000000030097')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000165' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000166' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,'00000000-0000-0000-00f9-000000030007')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,'00000000-0000-0000-00f9-000000030008')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,'00000000-0000-0000-00f9-000000030009')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,'00000000-0000-0000-00f9-000000030010')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,'00000000-0000-0000-00f9-000000030127')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000168' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000169' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000170' ,'00000000-0000-0000-00f9-000000030130')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000171' ,'00000000-0000-0000-00f9-000000030107')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000172' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000173' ,'00000000-0000-0000-00f9-000000030108')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000174' ,'00000000-0000-0000-00f9-000000030134')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000175' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000176' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000177' ,'00000000-0000-0000-00f9-000000030104')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000178' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000179' ,'00000000-0000-0000-00f9-000000030121')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000179' ,'00000000-0000-0000-00f9-000000030143')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000180' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000181' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000182' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000183' ,'00000000-0000-0000-00f9-000000030111')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000185' ,'00000000-0000-0000-00f9-000000030096')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000186' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000187' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000188' ,'00000000-0000-0000-00f9-000000030111')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000188' ,'00000000-0000-0000-00f9-000000030117')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000190' ,'00000000-0000-0000-00f9-000000030111')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000190' ,'00000000-0000-0000-00f9-000000030124')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000191' ,'00000000-0000-0000-00f9-000000030013')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000191' ,'00000000-0000-0000-00f9-000000030090')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000191' ,'00000000-0000-0000-00f9-000000030097')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000192' ,'00000000-0000-0000-00f9-000000030103')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000194' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000195' ,'00000000-0000-0000-00f9-000000030012')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000195' ,'00000000-0000-0000-00f9-000000030095')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000196' ,'00000000-0000-0000-00f9-000000030091')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000197' ,'00000000-0000-0000-00f9-000000030129')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000199' ,'00000000-0000-0000-00f9-000000030001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000200' ,'00000000-0000-0000-00f9-000000030113')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000201' ,'00000000-0000-0000-00f9-000000000004')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000201' ,'00000000-0000-0000-00f9-000000000138')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000201' ,'00000000-0000-0000-00f9-000000000139')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000202' ,'00000000-0000-0000-00f9-000000000001')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000203' ,'00000000-0000-0000-00f9-000000000141')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000204' ,'00000000-0000-0000-00f9-000000000074')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000205' ,'00000000-0000-0000-00f9-000000000009')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000205' ,'00000000-0000-0000-00f9-000000000018')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000206' ,'00000000-0000-0000-00f9-000000000051')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000207' ,'00000000-0000-0000-00f9-000000000045')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000208' ,'00000000-0000-0000-00f9-000000030142')
GO

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000209' ,'00000000-0000-0000-00f9-000000030143')
GO



/* Pflanze_Verbreitung */

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000001' ,'00000000-0000-0000-00fe-000000000005' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000001' ,'00000000-0000-0000-00fe-000000000053' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000002' ,'00000000-0000-0000-00fe-000000000001' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000003' ,'00000000-0000-0000-00fe-000000000007' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000003' ,'00000000-0000-0000-00fe-000000000038' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000003' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000004' ,'00000000-0000-0000-00fe-000000000015' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00fe-000000000010' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00fe-000000000019' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00fe-000000000043' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000005' ,'00000000-0000-0000-00fe-000000000046' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000006' ,'00000000-0000-0000-00fe-000000000013' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000006' ,'00000000-0000-0000-00fe-000000000038' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000007' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000007' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000008' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000008' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000009' ,'00000000-0000-0000-00fe-000000000027' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000010' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000010' ,'00000000-0000-0000-00fe-000000000055' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000011' ,'00000000-0000-0000-00fe-000000000038' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000011' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,'00000000-0000-0000-00fe-000000000012' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000013' ,'00000000-0000-0000-00fe-000000000055' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,'00000000-0000-0000-00fe-000000000010' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,'00000000-0000-0000-00fe-000000000030' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,'00000000-0000-0000-00fe-000000000031' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000014' ,'00000000-0000-0000-00fe-000000000041' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,'00000000-0000-0000-00fe-000000000056' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,'00000000-0000-0000-00fe-000000000058' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000016' ,'00000000-0000-0000-00fe-000000000019' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000016' ,'00000000-0000-0000-00fe-000000000043' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000016' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000017' ,'00000000-0000-0000-00fe-000000000020' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000018' ,'00000000-0000-0000-00fe-000000000038' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000018' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000019' ,'00000000-0000-0000-00fe-000000000010' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000019' ,'00000000-0000-0000-00fe-000000000041' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000019' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000020' ,'00000000-0000-0000-00fe-000000000018' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000020' ,'00000000-0000-0000-00fe-000000000028' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000020' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000021' ,'00000000-0000-0000-00fe-000000000026' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000021' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000022' ,'00000000-0000-0000-00fe-000000000008' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000022' ,'00000000-0000-0000-00fe-000000000010' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000022' ,'00000000-0000-0000-00fe-000000000046' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000022' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000023' ,'00000000-0000-0000-00fe-000000000010' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000023' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000023' ,'00000000-0000-0000-00fe-000000000053' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000025' ,'00000000-0000-0000-00fe-000000000022' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000026' ,'00000000-0000-0000-00fe-000000000006' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000027' ,'00000000-0000-0000-00fe-000000000017' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000028' ,'00000000-0000-0000-00fe-000000000018' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000028' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000029' ,'00000000-0000-0000-00fe-000000000041' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000029' ,'00000000-0000-0000-00fe-000000000046' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000029' ,'00000000-0000-0000-00fe-000000000049' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000030' ,'00000000-0000-0000-00fe-000000000013' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000032' ,'00000000-0000-0000-00fe-000000000010' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000032' ,'00000000-0000-0000-00fe-000000000046' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000033' ,'00000000-0000-0000-00fe-000000000019' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000033' ,'00000000-0000-0000-00fe-000000000043' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000033' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000034' ,'00000000-0000-0000-00fe-000000000021' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000034' ,'00000000-0000-0000-00fe-000000000024' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000035' ,'00000000-0000-0000-00fe-000000000043' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000036' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000036' ,'00000000-0000-0000-00fe-000000000054' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000037' ,'00000000-0000-0000-00fe-000000000038' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000037' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000037' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000038' ,'00000000-0000-0000-00fe-000000000007' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000039' ,'00000000-0000-0000-00fe-000000000046' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,'00000000-0000-0000-00fe-000000000019' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,'00000000-0000-0000-00fe-000000000033' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,'00000000-0000-0000-00fe-000000000034' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000040' ,'00000000-0000-0000-00fe-000000000043' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,'00000000-0000-0000-00fe-000000000013' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000041' ,'00000000-0000-0000-00fe-000000000055' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000042' ,'00000000-0000-0000-00fe-000000000041' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000043' ,'00000000-0000-0000-00fe-000000000029' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000043' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000044' ,'00000000-0000-0000-00fe-000000000043' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000044' ,'00000000-0000-0000-00fe-000000000056' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000045' ,'00000000-0000-0000-00fe-000000000013' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000045' ,'00000000-0000-0000-00fe-000000000019' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000046' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000047' ,'00000000-0000-0000-00fe-000000000008' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000047' ,'00000000-0000-0000-00fe-000000000010' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000047' ,'00000000-0000-0000-00fe-000000000012' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000047' ,'00000000-0000-0000-00fe-000000000041' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000047' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000049' ,'00000000-0000-0000-00fe-000000000025' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000051' ,'00000000-0000-0000-00fe-000000000041' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000051' ,'00000000-0000-0000-00fe-000000000049' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000052' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000052' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000053' ,'00000000-0000-0000-00fe-000000000013' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000053' ,'00000000-0000-0000-00fe-000000000043' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000054' ,'00000000-0000-0000-00fe-000000000013' ,100)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000055' ,'00000000-0000-0000-00fe-000000000056' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000055' ,'00000000-0000-0000-00fe-000000000057' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,'00000000-0000-0000-00fe-000000000002' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000056' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000057' ,'00000000-0000-0000-00fe-000000000019' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000057' ,'00000000-0000-0000-00fe-000000000037' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000057' ,'00000000-0000-0000-00fe-000000000043' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000058' ,'00000000-0000-0000-00fe-000000000010' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000058' ,'00000000-0000-0000-00fe-000000000041' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000058' ,'00000000-0000-0000-00fe-000000000046' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,'00000000-0000-0000-00fe-000000000018' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,'00000000-0000-0000-00fe-000000000019' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000060' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000061' ,'00000000-0000-0000-00fe-000000000012' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000061' ,'00000000-0000-0000-00fe-000000000014' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000061' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000063' ,'00000000-0000-0000-00fe-000000000035' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000064' ,'00000000-0000-0000-00fe-000000000018' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000064' ,'00000000-0000-0000-00fe-000000000043' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000064' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000066' ,'00000000-0000-0000-00fe-000000000044' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000066' ,'00000000-0000-0000-00fe-000000000046' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000066' ,'00000000-0000-0000-00fe-000000000055' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000067' ,'00000000-0000-0000-00fe-000000000013' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000067' ,'00000000-0000-0000-00fe-000000000019' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000067' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000068' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000068' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000069' ,'00000000-0000-0000-00fe-000000000040' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000069' ,'00000000-0000-0000-00fe-000000000043' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000069' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000070' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000070' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000070' ,'00000000-0000-0000-00fe-000000000055' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000072' ,'00000000-0000-0000-00fe-000000000006' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000072' ,'00000000-0000-0000-00fe-000000000050' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000073' ,'00000000-0000-0000-00fe-000000000041' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000073' ,'00000000-0000-0000-00fe-000000000049' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00fe-000000000011' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00fe-000000000036' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00fe-000000000055' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000075' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000076' ,'00000000-0000-0000-00fe-000000000038' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000076' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000076' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000077' ,'00000000-0000-0000-00fe-000000000038' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000077' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000077' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000077' ,'00000000-0000-0000-00fe-000000000055' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,'00000000-0000-0000-00fe-000000000001' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,'00000000-0000-0000-00fe-000000000004' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,'00000000-0000-0000-00fe-000000000013' ,100)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,'00000000-0000-0000-00fe-000000000042' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,'00000000-0000-0000-00fe-000000000056' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000079' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000079' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000080' ,'00000000-0000-0000-00fe-000000000031' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000080' ,'00000000-0000-0000-00fe-000000000038' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000080' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000081' ,'00000000-0000-0000-00fe-000000000012' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000081' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000082' ,'00000000-0000-0000-00fe-000000000009' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000083' ,'00000000-0000-0000-00fe-000000000048' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000084' ,'00000000-0000-0000-00fe-000000000039' ,1)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00fe-000000000001' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00fe-000000000003' ,100)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00fe-000000000018' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00fe-000000000019' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00fe-000000000043' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000086' ,'00000000-0000-0000-00fe-000000000018' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000086' ,'00000000-0000-0000-00fe-000000000043' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000086' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000087' ,'00000000-0000-0000-00fe-000000000021' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000087' ,'00000000-0000-0000-00fe-000000000023' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000088' ,'00000000-0000-0000-00fe-000000000003' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000088' ,'00000000-0000-0000-00fe-000000000013' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000088' ,'00000000-0000-0000-00fe-000000000056' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000089' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000089' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000090' ,'00000000-0000-0000-00fe-000000000010' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000090' ,'00000000-0000-0000-00fe-000000000019' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000090' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000090' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000090' ,'00000000-0000-0000-00fe-000000000055' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000091' ,'00000000-0000-0000-00fe-000000000043' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000091' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,'00000000-0000-0000-00fe-000000000010' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000092' ,'00000000-0000-0000-00fe-000000000055' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,'00000000-0000-0000-00fe-000000000047' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000094' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000094' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000095' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000095' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000096' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000096' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000097' ,'00000000-0000-0000-00fe-000000000001' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000097' ,'00000000-0000-0000-00fe-000000000018' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000097' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000097' ,'00000000-0000-0000-00fe-000000000054' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000098' ,'00000000-0000-0000-00fe-000000000014' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000098' ,'00000000-0000-0000-00fe-000000000019' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000098' ,'00000000-0000-0000-00fe-000000000038' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000098' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000100' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000101' ,'00000000-0000-0000-00fe-000000000006' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000101' ,'00000000-0000-0000-00fe-000000000051' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000102' ,'00000000-0000-0000-00fe-000000000041' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000102' ,'00000000-0000-0000-00fe-000000000049' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000104' ,'00000000-0000-0000-00fe-000000000041' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000104' ,'00000000-0000-0000-00fe-000000000046' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000104' ,'00000000-0000-0000-00fe-000000000049' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000105' ,'00000000-0000-0000-00fe-000000000041' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000105' ,'00000000-0000-0000-00fe-000000000049' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000106' ,'00000000-0000-0000-00fe-000000000018' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000106' ,'00000000-0000-0000-00fe-000000000043' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000107' ,'00000000-0000-0000-00fe-000000000018' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000107' ,'00000000-0000-0000-00fe-000000000043' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000108' ,'00000000-0000-0000-00fe-000000000038' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000108' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00fe-000000000012' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00fe-000000000052' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000109' ,'00000000-0000-0000-00fe-000000000054' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000110' ,'00000000-0000-0000-00fe-000000000038' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000110' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000111' ,'00000000-0000-0000-00fe-000000000018' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000111' ,'00000000-0000-0000-00fe-000000000019' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000111' ,'00000000-0000-0000-00fe-000000000043' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000111' ,'00000000-0000-0000-00fe-000000000045' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000111' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000112' ,'00000000-0000-0000-00fe-000000000019' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000112' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000113' ,'00000000-0000-0000-00fe-000000000013' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000113' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000115' ,'00000000-0000-0000-00fe-000000000032' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000115' ,'00000000-0000-0000-00fe-000000000054' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000117' ,'00000000-0000-0000-00fe-000000000011' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000117' ,'00000000-0000-0000-00fe-000000000038' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000117' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000118' ,'00000000-0000-0000-00fe-000000000013' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000119' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000120' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000120' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000121' ,'00000000-0000-0000-00fe-000000000035' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000121' ,'00000000-0000-0000-00fe-000000000059' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000124' ,'00000000-0000-0000-00fe-000000000013' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000124' ,'00000000-0000-0000-00fe-000000000053' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000124' ,'00000000-0000-0000-00fe-000000000062' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000126' ,'00000000-0000-0000-00fe-000000000013' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000126' ,'00000000-0000-0000-00fe-000000000038' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000126' ,'00000000-0000-0000-00fe-000000000043' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000126' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000129' ,'00000000-0000-0000-00fe-000000000056' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000129' ,'00000000-0000-0000-00fe-000000000066' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000130' ,'00000000-0000-0000-00fe-000000000056' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000130' ,'00000000-0000-0000-00fe-000000000067' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000131' ,'00000000-0000-0000-00fe-000000000013' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000131' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000134' ,'00000000-0000-0000-00fe-000000000018' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000134' ,'00000000-0000-0000-00fe-000000000043' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000135' ,'00000000-0000-0000-00fe-000000000060' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000135' ,'00000000-0000-0000-00fe-000000000068' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000138' ,'00000000-0000-0000-00fe-000000000043' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000138' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000138' ,'00000000-0000-0000-00fe-000000000069' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000139' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000139' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000139' ,'00000000-0000-0000-00fe-000000000069' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000140' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000140' ,'00000000-0000-0000-00fe-000000000061' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000142' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000142' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000143' ,'00000000-0000-0000-00fe-000000000043' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000143' ,'00000000-0000-0000-00fe-000000000056' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000145' ,'00000000-0000-0000-00fe-000000000018' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000145' ,'00000000-0000-0000-00fe-000000000052' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000145' ,'00000000-0000-0000-00fe-000000000072' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000146' ,'00000000-0000-0000-00fe-000000000062' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000146' ,'00000000-0000-0000-00fe-000000000070' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000148' ,'00000000-0000-0000-00fe-000000000013' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000148' ,'00000000-0000-0000-00fe-000000000018' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000148' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000150' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000150' ,'00000000-0000-0000-00fe-000000000063' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000152' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000152' ,'00000000-0000-0000-00fe-000000000063' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000154' ,'00000000-0000-0000-00fe-000000000064' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000154' ,'00000000-0000-0000-00fe-000000000071' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000156' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000156' ,'00000000-0000-0000-00fe-000000000052' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000157' ,'00000000-0000-0000-00fe-000000000079' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000158' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000159' ,'00000000-0000-0000-00fe-000000000067' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000159' ,'00000000-0000-0000-00fe-000000000079' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000160' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000161' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000162' ,'00000000-0000-0000-00fe-000000000028' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000163' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000164' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000165' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000166' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,'00000000-0000-0000-00fe-000000000013' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000167' ,'00000000-0000-0000-00fe-000000000052' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000168' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000169' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000170' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000171' ,'00000000-0000-0000-00fe-000000000052' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000172' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000173' ,'00000000-0000-0000-00fe-000000000012' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000173' ,'00000000-0000-0000-00fe-000000000029' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000173' ,'00000000-0000-0000-00fe-000000000041' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000173' ,'00000000-0000-0000-00fe-000000000046' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000174' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000175' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000176' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000177' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000178' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000179' ,'00000000-0000-0000-00fe-000000000013' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000180' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000181' ,'00000000-0000-0000-00fe-000000000002' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000181' ,'00000000-0000-0000-00fe-000000000028' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000182' ,'00000000-0000-0000-00fe-000000000002' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000182' ,'00000000-0000-0000-00fe-000000000028' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000183' ,'00000000-0000-0000-00fe-000000000052' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000185' ,'00000000-0000-0000-00fe-000000000029' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000186' ,'00000000-0000-0000-00fe-000000000027' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000187' ,'00000000-0000-0000-00fe-000000000002' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000187' ,'00000000-0000-0000-00fe-000000000028' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000188' ,'00000000-0000-0000-00fe-000000000028' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000188' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000188' ,'00000000-0000-0000-00fe-000000000063' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000190' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000191' ,'00000000-0000-0000-00fe-000000000018' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000191' ,'00000000-0000-0000-00fe-000000000028' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000191' ,'00000000-0000-0000-00fe-000000000055' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000192' ,'00000000-0000-0000-00fe-000000000002' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000194' ,'00000000-0000-0000-00fe-000000000001' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000195' ,'00000000-0000-0000-00fe-000000000063' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000196' ,'00000000-0000-0000-00fe-000000000054' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000196' ,'00000000-0000-0000-00fe-000000000074' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000197' ,'00000000-0000-0000-00fe-000000000029' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000199' ,'00000000-0000-0000-00fe-000000000007' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000200' ,'00000000-0000-0000-00fe-000000000019' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000200' ,'00000000-0000-0000-00fe-000000000037' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000200' ,'00000000-0000-0000-00fe-000000000043' ,0)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000201' ,'00000000-0000-0000-00fe-000000000014' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000201' ,'00000000-0000-0000-00fe-000000000055' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000201' ,'00000000-0000-0000-00fe-000000000074' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000202' ,'00000000-0000-0000-00fe-000000000046' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000203' ,'00000000-0000-0000-00fe-000000000059' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000203' ,'00000000-0000-0000-00fe-000000000073' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000204' ,'00000000-0000-0000-00fe-000000000012' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000204' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000205' ,'00000000-0000-0000-00fe-000000000052' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000205' ,'00000000-0000-0000-00fe-000000000054' ,16)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000206' ,'00000000-0000-0000-00fe-000000000046' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000206' ,'00000000-0000-0000-00fe-000000000075' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000207' ,'00000000-0000-0000-00fe-000000000043' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000207' ,'00000000-0000-0000-00fe-000000000076' ,2)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000207' ,'00000000-0000-0000-00fe-000000000077' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000208' ,'00000000-0000-0000-00fe-000000000011' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000208' ,'00000000-0000-0000-00fe-000000000046' ,4)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000208' ,'00000000-0000-0000-00fe-000000000061' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000208' ,'00000000-0000-0000-00fe-000000000078' ,8)
GO

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000209' ,'00000000-0000-0000-00fe-000000000063' ,4)
GO