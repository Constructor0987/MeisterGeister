-- Zauberzeichen-Tabelle neu
CREATE TABLE [Zauberzeichen] (
	[ZauberzeichenGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(100) NOT NULL, 
	[Typ] nvarchar(50) NOT NULL, 
	[SonderfertigkeitID] int NOT NULL, 
	[Lernkosten] int NOT NULL DEFAULT 0, 
	[Komplexität] int NOT NULL DEFAULT 0, 
	[Merkmal] nvarchar(300), 
	[ReichweitenDivisor] float NOT NULL DEFAULT 0, 
	[Bemerkung] ntext, 
	[Verbreitung] nvarchar(300), 
	[Komponenten] ntext, 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(100) DEFAULT 'Aventurien',
	CONSTRAINT [PK_Zauberzeichen] PRIMARY KEY ([ZauberzeichenGUID]), 
	CONSTRAINT [FK_Zauberzeichen_Sonderfertigkeit] FOREIGN KEY ([SonderfertigkeitID])
		REFERENCES Sonderfertigkeit ([SonderfertigkeitID])
)
GO

-- Zauberzeichen Daten
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000001',N'Auge der ewigen Wacht',N'Arkanoglyphe',208,150,9,N'Dämonisch (Thargunitoth), Temporal',0.5,null,N'Mag 2, Ach 1',N'Glyphen Bannung, Geist, Bewegung Objektsigille Körper Aroqa-Rune Thargunitoth',N'WdA 144',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000002',N'Auge des Basilisken',N'Arkanoglyphe',209,150,9,N'Elementar (Erz), Form',1,null,N'Mag 2, Ach 1',N'Glyphen Wahrnehmung, Wandlung, Verstärkung, Erz, Ortssigille',N'WdA 144',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000003',N'Auge des Mondes',N'Arkanoglyphe',536,125,5,N'Hellsicht, Illusion, Kraft',4,null,N'Wdm 4, Ach 2, Mag 2',N'Glyphen Wahrnehmung, Macht, Phantasmagorie, Ortssigille',N'WdA 144',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000004',N'Fallensiegel',N'Arkanoglyphe',537,100,3,N'Elementar (Einzelelement), Schaden',2,null,N'Ach 3, Mag 3, Zib 3, Alc 1, Wdm 1',N'Glyphen Schutz,Schaden, Weg, Elementsigille, Orts- oder Gegenstandssigille',N'WdA 144',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000005',N'Fanal der Herrschaft',N'Arkanoglyphe',206,100,7,N'Eigenschaften, Einfluss',1,null,N'Ach 2, Mag1',N'Glyphen Macht, Geist, Verstärkung, Namenssigille Nutznießer, Orts- oder Objektssigille',N'WdA 144',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000006',N'Fixierungszeichen',N'Arkanoglyphe',538,100,6,N'Objekt',0,null,N'Ach 3, Alc 2, Wdm 2, Zib 2',N'Glyphen Bewegung, Schutz, Objektsigille',N'WdA 145',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000007',N'Gezücht des Meister',N'Arkanoglyphe',539,150,8,N'Hellsicht, Herrschaft, Temporal',2,null,N'Ach 2, Mag 2',N'Glyphen Geist, Körper, Schutz, Wesenssigille, Zauberersigille, Ortssigille',N'WdA 145',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000008',N'Glyphe der elementaren Attraktion',N'Arkanoglyphe',200,75,4,N'Elementar nach Wahl, Umwelt',1,null,N'Ach 6, Alc 5, Mag 5, Utu 5, Wdm 5',N'Glyphen Ziel, Verstärkung, Elementsymbol nach Wahl, Ortssigille oder Gegenstandssigille',N'WdA 145',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000009',N'Glyphe der elementaren Bannung',N'Arkanoglyphe',202,50,5,N'Antimagie, Elementar nach Wahl',4,null,N'Alc 6, Mag 4, Ach 4, Utu 3, Zib 3',N'Glyphen Wandlung, Schutz, Elementsymbol nach Wahl, Objekt- oder Ortssigille',N'WdA 146',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000010',N'Glyphe des verfluchten Goldes',N'Arkanoglyphe',207,125,8,N'Objekt, Eigenschaften, Schaden',3,null,N'Ach 4, Zib 4, Utu 3, Mag 2',N'Glyphen Ursprung, Schutz, Bewegung, Orts- oder Objektsigille für Aufbewahrungsort',N'WdA 146',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000011',N'Hermetisches Siegel',N'Arkanoglyphe',199,50,3,N'Objekt, Temporal',2,null,N'Zib 6, Utu 5, Ach 4, Alc 4, Wdm 4, Mag 2',N'Glyphen Schutz, Wandlung, Orts- oder Objektsigille',N'WdA 146',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000012',N'Hypnotisches Zeichen',N'Arkanoglyphe',540,150,8,N'Einfluss',1,null,N'Ach 4, Mag 3, Wdm 3, Zib 3',N'Glyphen Wahrnehmung, Geist, Ursprung, Ortssigille',N'WdA 146',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000013',N'Leuchtendes Zeichen',N'Arkanoglyphe',196,25,1,N'Umwelt',0,null,N'Alc 6, Mag 6, Ach 3, Wdm 3, Zib 3',N'Glyphen Ursprung, Phantasmagorie, Farbsigille nach Wahl',N'WdA 147',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000014',N'Markierung des Todes',N'Arkanoglyphe',205,100,7,N'Objekt, Telekinese',0,null,N'Mag 2',N'Glyphen Wahrnehmung, Bewegung, Ziel, Objektssigille',N'WdA 147',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000015',N'Siegel der Seelenruhe',N'Arkanoglyphe',198,25,3,N'Einfluss',2,null,N'Zib 5, Ach 4, Mag 4',N'Glyphen Bannung, Wahrnehmung, Ortssigille',N'WdA 147',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000016',N'Siegel der Stille',N'Arkanoglyphe',541,50,2,N'Umwelt',1,null,N'Wdm 5, Zib 5, Ach 3, Alc 3, Mag 3, Utu 3',N'Glyphen Wahrnehmung, Verminderung, Ortssigille',N'WdA 147',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000017',N'Siegel der zweiten Haut',N'Arkanoglyphe',542,50,3,N'Kraft, Objekt',0,null,N'Ach 5, Wdm 4, Mag 3',N'Glyphe Verstärkung, Resistenz, Objektsigille',N'WdA 148',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000018',N'Sigille der Schatten',N'Arkanoglyphe',543,50,2,N'Umwelt',2,null,N'Ach 4, Alc 3, Mag 3, Wdm 3',N'Glyphe Bannung, Ortssigille',N'WdA 148',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000019',N'Sigille des unsichtbaren Trägers',N'Arkanoglyphe',544,125,4,N'Elementar (Luft)',0,null,N'Alc 5, Mag 4, Utu 4, Wdm 4, Ach 2',N'Glyphe Körper, Luft, Objektsigille',N'WdA 148',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000020',N'Sigille des unsichtbaren Weges',N'Arkanoglyphe',545,100,4,N'Elementar (Luft), Umwelt',1,null,N'Mag 3, Zib 3',N'Glyphe Luft, 2 Sigillen (Ort der Glyphe & Zielort)',N'WdA 149',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000021',N'Singendes Zeichen',N'Arkanoglyphe',197,25,2,N'Illusion',2,null,N'Ach 5, Mag 5, Alc 3',N'Glyphe Schutz, Phantasmagorie, Orts- und Objektssigille',N'WdA 149',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000022',N'Ungesehenes Zeichen',N'Arkanoglyphe',201,50,5,N'Einfluss, Illusion',2,null,N'Zib 5, Wdm 4, Mag 3, Ach 3',N'Glyphen Phantasmagorie, Schutz, Wahrnehmung, Orts- oder Objektsigille Ziel',N'WdA 149',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000023',N'Verderben des Magiers',N'Arkanoglyphe',546,125,8,N'Kraft, Schaden, Verständigung',1,null,N'Mag 2, Zib 2',N'Glyphen Bannung, Macht, Ortssigille',N'WdA 149',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000024',N'Verständigungszeichen',N'Arkanoglyphe',547,50,5,N'Verständigung',0.001,N'RkW in Meilen',N'Zib 5, Wdm 2',N'Glyphen Wahrnehmung, Verständigung, Sigille Empfänger, Orts- oder Objektssigille Zeichenträger',N'WdA 149',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000025',N'Wimmelndes Zeichen',N'Arkanoglyphe',548,100,5,N'Dämonisch (Mishkara), Herbeirufung',0.01,N'RkW * 100 Schritt Reichweite, RkW/2 Schritt Konzentrationsradius',N'Ach 3, Wdm 2, Zib 2',N'Glyphen Herbeirufung, Sigille der Mishkara, Sigille für Spinnen oder Insekten, Ortssigille',N'WdA 150',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000026',N'Wunschglyphe',N'Arkanoglyphe',549,75,6,N'Einfluss, Hellsicht, Illusion',1,null,N'Ach 3, Zib 2, Mag 1',N'Glyphen Phantasmagorie, Wahrnehmung, Objektssigille',N'WdA 150',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000027',N'Zähne des Feuers',N'Arkanoglyphe',203,75,6,N'Objekt, Schaden',2,null,N'Ach 4, Mag 3, Alc 2',N'Glyphen Bannung, Weg, Ziel, Objektsigille für den Untergrund',N'WdA 150',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000028',N'Zeichen der Zauberschmiede',N'Arkanoglyphe',204,75,6,N'Objekt, Schaden',0,null,N'Mag 4, Ach 1, Sch 1, Zib 1',N'Glyphen Bannung, Macht, Verstärkung, Objektsigille, Denominator (Wahrer Name der gewünschten Wesenheit)',N'WdA 150',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000029',N'Zeichen des Handwerks',N'Arkanoglyphe',550,125,5,N'Objekt',0,null,N'Alc 5, Utu 5, Wdm 5, Mag 2',N'Glyphe Verstärkung, Objektsigille',N'WdA 151',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000030',N'Zeichen des Stillstands',N'Arkanoglyphe',551,125,6,N'Elementar (Eis), Umwelt',1,null,N'Ach 4, Mag 3',N'Glyphen Bewegung, Verminderung, Eis, Ortssigille',N'WdA 151',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000031',N'Zeichen des versperrten Blicks',N'Arkanoglyphe',552,75,7,N'Antimagie, Hellsicht',2,null,N'Mag 2, Zib 1',N'Glyphen Wahrnehmung, Resistenz, Geist, Ortssigille',N'WdA 151',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000032',N'Zeichen gegen Magie',N'Arkanoglyphe',553,100,4,N'Metamagie, Antimagie, Objekt',0,null,N'Ach 3, Mag 3,',N'Glyphen Macht, Wandlung, Resistenz, Objektsigille',N'WdA 151',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000033',N'Zusatzzeichen Kraftquellenspeisung',N'Zusatzzeichen',554,150,5,N'Temporal, Metamagie, Kraft',0,null,N'Ach 1, Mag 1, Zib 1',N'Glyphen Macht, Wandlung, Schutz, Sigille für Kraftquelle',N'WdA 152',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000034',N'Zusatzzeichen Magiewiderstand',N'Zusatzzeichen',555,0,2,N'Metamagie, Antimagie, Objekt',0,N'Ist in 553 Zeichen gegen Magie inbegriffen',N'unbekannt',N'Glyphen Macht, Wandlung, Resistenz, ',N'WdA 152',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000035',N'Zusatzzeichen Potenzierung',N'Zusatzzeichen',556,50,2,N'Kraft, Metamagie',0.5,N'+2 je Stufe der Potenzierung',N'Mag 4, Alc 3, Ach 1, Zib 1',N'Glyphe Verstärkung',N'WdA 152',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000036',N'Zusatzzeichen Satinavs Siegel',N'Zusatzzeichen',210,0,1,N'Metamagie, Temporal',0,N'+1 je Zeitkategorie',N'SF Zauberzeichen',N'Sternsymbol mit 13 Zacken, zusätzliche Symbole',N'WdA 153',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000037',N'Zusatzzeichen Schutzsiegel',N'Zusatzzeichen',211,125,3,N'Elementar (Wahl), Schaden',0,null,N'Ach 5, Alc 4, Mag 4, Utu 4, Wdm 4, Zib 4',N'Glyphen Schutz, Weg, Elementsymbol nach Wahl',N'WdA 153',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000038',N'Zusatzzeichen Tarnung',N'Zusatzzeichen',557,50,2,N'Illusion',0,null,N'Zib 4, Alc 2, Mag 2, Utu 2',N'Glyphen Wahrnehmung, Schutz',N'WdA 153',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000039',N'Zusatzzeichen Verkleinerung',N'Zusatzzeichen',558,50,1,N'Metamagie',0,N'+1 je Verkleinerung',N'Mag 6, Zib 5, Alc 5, Ach 4',N'Glyphe Wandlung',N'WdA 153',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000040',N'Zusatzzeichen Zielbeschränkung',N'Zusatzzeichen',559,75,2,N'Hellsicht',0,null,N'Zib 5, Mag 3',N'Glyphen Ziel, Wahrnehmung, Schutz, Sigille für Personengruppe',N'WdA 154',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000041',N'Bannkreis gegen Chimären',N'Bannkreis',195,75,7,N'Antimagie, Dämonisch (Asfaloth)',1,null,N'Mag 3, Ach 2, Wdm 1',N'Heptagramm, Asfaloths Dämonensigel, Sigillen der Ursprungsgeschöpfe',N'WdA 156',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000042',N'Schutzkreis gegen Chimären',N'Schutzkreis',195,75,7,N'Antimagie, Dämonisch (Asfaloth)',2,null,N'Mag 3, Ach 2, Wdm 2',N'Heptagramm, Sigillen der Ursprungsgeschöpfe, Zeichen der Tsa',N'WdA 156',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000043',N'Bannkreis gegen Elementare',N'Bannkreis',190,75,7,N'Antimagie, Elementar',1,null,N'Ach 5, Alc 4, Mag 3, Utu 2, Wdm 2',N'Hexagramm, Zeichen der Elemente',N'WdA 156',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000044',N'Schutzkreis gegen Elementare',N'Schutzkreis',190,75,7,N'Antimagie, Elementar',2,null,N'Ach 5, Alc 4, Mag 3, Utu 2, Wdm 3',N'Hexagramm, Siegel der Elemente, passende Zeichen in Ur-Tulamidya und Elementsymbole',N'WdA 156',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000045',N'Bannkreis gegen Geisterwesen',N'Bannkreis',188,25,5,N'Antimagie, Geisterwesen',1,null,N'Mag 6, Utu 6, Wdm 6, Ach 5, Zib 4',N'Pentagramm, Zeichen des vollen Madamals in Uthars Pforte',N'WdA 157',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000046',N'Schutzkreis gegen Geisterwesen',N'Schutzkreis',188,25,5,N'Antimagie, Geisterwesen',2,null,N'Mag 6, Utu 6, Wdm 6, Ach 5, Zib 5',N'Pentagramm, Zeichen des Praios',N'WdA 157',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000047',N'Bannkreis gegen niedere Dämonen',N'Bannkreis',189,50,6,N'Antimagie, Dämonisch',1,null,N'Mag 7, Alc 5, Wdm 4, Zib 3, Utu 2',N'Pentagramm, Ligaturen aus Siegeln der Erzdämonen oder siebenstrahligen Dämonenkrone',N'WdA 157',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000048',N'Schutzkreis gegen niedere Dämonen',N'Schutzkreis',189,50,6,N'Antimagie, Dämonisch',2,null,N'Mag 7, Alc 5, Wdm 4, Zib 3, Utu 3',N'Pentagramm, Schutzzeichen, Zeichen des dämonischen Tagesherrschers, Zeichen einzelner Götter',N'WdA 157',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000049',N'Bannkreis gegen gehörnte Dämonen',N'Bannkreis',191,75,8,N'Antimagie, Dämonisch',1,null,N'Mag 5, Alc 2, Wdm 2',N'Heptagramm, Ligaturen aus Siegeln der Erzdämonen oder siebenstrahligen Dämonenkrone, sorgfältiger , detaillierter',N'WdA 157',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000050',N'Schutzkreis gegen Gehörnte Dämonen',N'Schutzkreis',191,75,8,N'Antimagie, Dämonisch',2,null,N'Mag 5, Alc 2, Wdm 3',N'Pentagramm, Schutzzeichen, Zeichen des dämonischen Tagesherrschers, Zeichen einzelner Götter',N'WdA 157',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000051',N'Schutzkreis gegen Reptilien',N'Schutzkreis',194,25,4,N'Antimagie, Herbeirufung',2,null,N'Mag 3, Alc 2, Utu 2',N'Sigille des Bastrabun und Sulman al''Nassori, archaische Chrmk-Zeichen',N'WdA 158',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000052',N'Schutzkreis gegen Traumgänger',N'Schutzkreis',192,50,6,N'Antimagie, Verständigung',2,null,N'Zib 6, Mag 2',N'Zeichen des Boron und der Marbo, Symbole diverser Wesenheiten, die in Träume eindringen können, Zeichen in Ur-Tulamidya',N'WdA 158',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000053',N'Schutzkreis gegen Ungeziefer',N'Schutzkreis',193,25,3,N'Antimagie, Herbeirufung',2,null,N'Ach 6, Utu 5, Wdm 5, Mag 4, Alc 3',N'Sigille der Mishkara, Glyphe der Wandlung, Zeichen in Ur-Tulamidya',N'WdA 158',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000054',N'Schutzkreis gegen Untote',N'Schutzkreis',535,50,7,N'Antimagie, Dämonisch (Thargunitoth)',2,null,N'Mag 2, Wdm 1',N'Zhayad-Ligaturen versch. Untoter Wesenheiten, Aroqa-Rune',N'WdA 159',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000055',N'Alfenbannrune',N'Rune',525,200,8,N'Antimagie, Einfluss',1,N'RkP* Schritt',N'2',N'rotes Triskal in grünem Drachen, darum weiße Wale',N'WdA 161',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000056',N'Bärenrune',N'Rune',526,100,5,N'Eigenschaften / Objekt',0,null,N'3',N'grüner Pottwal in gelber Sonnenscheibe, umgeben von grünen Walknoten',N'WdA 161',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000057',N'Blutrune',N'Zusatzrune',403,125,3,N'Kraft, Schaden, Verständigung',0,null,N'2',N'grünes Triskal, bestehend aus einem weißen Walknoten, der roten Wellen und die Namensrune des Nutznießers einfasst',N'WdA 161',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000058',N'Drachenrune',N'Rune',401,150,8,N'Einfluss',0,null,N'4',N'verschlungenes Band aus weißen Walen und violeten Meerestieren fasst eine rote geflügelte Seeschlange ein',N'WdA 162',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000059',N'Entgifterrune',N'Rune',527,75,4,N'Eigenschaften / Objekt',0,null,N'3',N'grüne Ranken in weißen Walknoten',N'WdA 162',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000060',N'Felsenrune',N'Rune',397,75,6,N'Elementar (Erz), Eigenschaften / Objekt',0,null,N'6',N'verwobene Elementarzeichen von Stein und Fels, mehrfach wiederholt, im Innern grüner Pottwal und gelbe Sonnenscheibe',N'WdA 162',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000061',N'Finsterrune',N'Rune',395,50,5,N'Umwelt, Verständigung',0,null,N'3',N'rote Sonnenscheibe mit gewundenen Strahlen, umgeben von einem Kranz aus weißen Mondsicheln',N'WdA 162',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000062',N'Friedensrune',N'Rune',393,50,5,N'Einfluss',1,null,N'4',N'grüne Mondsichel, umschlungen von blauem Bändergeflecht',N'WdA 163',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000063',N'Furchtrune',N'Rune',402,150,9,N'Einfluss',0,null,N'5',N'Walknoten mit weißen Drachenhals und roter Mondsichel',N'WdA 163',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000064',N'Geisterbannrune',N'Rune',528,200,8,N'Antimagie, Geisterwesen',1,N'RkP* Schritt',N'3',N'rote Mondsichel, umgeben von orangen Geistern und weißen Walen',N'WdA 163',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000065',N'Lebensrune',N'Rune',529,175,8,N'Temporal, Eigenschaften / Objekt',0,null,N'1',N'grünes Triskal in orangener Sonnenscheibe, umgeben von weißen Walknoten',N'WdA 163',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000066',N'Nebelrune',N'Rune',530,50,5,N'Umwelt, Eigenschaften (belebtes Zielobjekt)',1,null,N'4',N'rote Mondsichel in weißer Sonnenscheibe',N'WdA 163',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000067',N'Orkanstimmenrune',N'Rune',531,100,6,N'Eigenschaften / Objekt',0.02,N'50 * RkP* Schritt',N'2',N'grüne Mondsichel, umgeben von weißen Walknoten',N'WdA 164',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000068',N'Ottarune',N'Rune',394,0,5,N'Verständigung, Eigenschaften / Objekt',0,N'Ist in Ottagaldr enthalten',N'3',N'Namensrune von Schiff, Ottajasko, evtl. Zielperson eingeflochten in ornamentale Symbole',N'WdA 164',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000069',N'Pfeilrune',N'Rune',399,100,7,N'Illusion',0,null,N'3',N'Walknoten, eingeschlungen sind Triskal und grüne Pfeilsymbole',N'WdA 164',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000070',N'Rauschrune',N'Rune',392,50,4,N'Eigenschaften / Objekt',0,null,N'4',N'grüner Walknoten, verflochten mit gelben Ranken',N'WdA 165',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000071',N'Salzwasserrune',N'Rune',532,100,4,N'Eigenschaften / Objekt',0,null,N'4',N'grüner Walknoten mit gelben Wellenbändern',N'WdA 165',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000072',N'Schicksalsrune',N'Rune',398,100,7,N'Eigenschaften / Objekt',0,null,N'4',N'in einem komplexen Walknoten sind ein grüner Pottwal und eine orangene Mondsichel eingewoben',N'WdA 165',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000073',N'Waberlohenrune',N'Rune',533,75,4,N'Elementar (Feuer), Eigenschaften / Objekt',0,null,N'4',N'gelber Pottwal in blauer Mondsichel, umgeben von Flammenornamentik',N'WdA 165',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000074',N'Waffenrune',N'Rune',400,125,8,N'Schaden, Eigenschaften / Objekt',0,null,N'1',N'rotes Triskal mit blaugrünem Drachen und der Namensrune der gewünschten Wesenheit, in ausufernder Ornamentik verbunden',N'WdA 165',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000075',N'Wogensturmrune',N'Rune',396,75,6,N'Elementar (Wasser), Umwelt',0,null,N'5',N'gelbe Sonnenscheibe mit gewundenen Strahlen, dazu die weiße Mondsichel, umschlungen von hellblauen Ornamentbändern für Wind und Wellen',N'WdA 166',N'Aventurien')
GO
INSERT INTO [Zauberzeichen] ([ZauberzeichenGUID],[Name],[Typ],[SonderfertigkeitID],[Lernkosten],[Komplexität],[Merkmal],[ReichweitenDivisor],[Bemerkung],[Verbreitung],[Komponenten],[Literatur],[Setting]) VALUES ('00000000-0000-0000-000a-000000000076',N'Zukunftsrune',N'Rune',534,175,6,N'Hellsicht',0,null,N'3',N'gelbe Mondsichel in weißer Sonne',N'WdA 166',N'Aventurien')
GO

-- Datentransfer zu Held_Ausrüstung. Löschen der anderen Tabellen.
INSERT INTO [Held_Ausrüstung]
		([HeldGUID]
		,[Angelegt]
		,[Ort]
		,[AusrüstungGUID]
		,[Talentname]
		,[Anzahl]
		,[BF])
SELECT [HeldGUID]
		,[Angelegt]
		,[Ort]
		,[WaffeGUID]
		,[Talentname]
		,[Anzahl]
		,[BF]
	FROM [Held_Waffe]
	where UPPER(HeldGUID) + UPPER(WaffeGUID) not in (Select UPPER(HeldGUID) + UPPER(AusrüstungGUID) from Held_Ausrüstung)
GO

INSERT INTO [Held_Ausrüstung]
		([HeldGUID]
		,[Angelegt]
		,[Ort]
		,[AusrüstungGUID]
		,[Talentname]
		,[Anzahl]
		,[BF])
SELECT [HeldGUID]
		,[Angelegt]
		,[Ort]
		,[SchildGUID]
		,NULL
		,[Anzahl]
		,[BF]
	FROM [Held_Schild]
	where UPPER(HeldGUID) + UPPER(SchildGUID) not in (Select UPPER(HeldGUID) + UPPER(AusrüstungGUID) from Held_Ausrüstung)
GO

INSERT INTO [Held_Ausrüstung]
		([HeldGUID]
		,[Angelegt]
		,[Ort]
		,[AusrüstungGUID]
		,[Talentname]
		,[Anzahl]
		,[BF])
SELECT [HeldGUID]
		,[Angelegt]
		,[Ort]
		,[RüstungGUID]
		,NULL
		,[Anzahl]
		,Case when [Strukturpunkte] is NULL THEN 0 ELSE [Strukturpunkte] END
	FROM [Held_Rüstung]
	where UPPER(HeldGUID) + UPPER(RüstungGUID) not in (Select UPPER(HeldGUID) + UPPER(AusrüstungGUID) from Held_Ausrüstung)
GO

INSERT INTO [Held_Ausrüstung]
		([HeldGUID]
		,[Angelegt]
		,[Ort]
		,[AusrüstungGUID]
		,[Talentname]
		,[Anzahl]
		,[BF])
SELECT [HeldGUID]
		,[Angelegt]
		,[Ort]
		,[FernkampfwaffeGUID]
		,[Talentname]
		,[Anzahl]
		,0
	FROM [Held_Fernkampfwaffe]
	where UPPER(HeldGUID) + UPPER(FernkampfwaffeGUID) not in (Select UPPER(HeldGUID) + UPPER(AusrüstungGUID) from Held_Ausrüstung)
GO

DROP TABLE [Held_Fernkampfwaffe]
GO
DROP TABLE [Held_Rüstung]
GO
DROP TABLE [Held_Schild]
GO
DROP TABLE [Held_Waffe]
GO