-- Audio Player Shuffle & Repeat Funktion speichern
ALTER TABLE [Audio_Playlist] ADD "Shuffle" bit NOT NULL DEFAULT 1;
ALTER TABLE [Audio_Playlist] ADD "Repeat" bit NOT NULL DEFAULT 1;
-- Audio Player Theme Einstellung "Nur Gräusche des Themes zusätzlich abspielen" speichern
ALTER TABLE [Audio_Theme] ADD "NurGeräusche" bit NOT NULL DEFAULT 0;

-- Rassen-Tabelle um ein Feld für die Operation (+,*,=) bei der Gewichtsberechnung ergänzt
ALTER TABLE [Rasse] ADD GewichtOperator nvarchar(1) NOT NULL DEFAULT '+';

-- Ausrüstungstabelle um ein Feld Basisausrüstung erweitern
ALTER TABLE [Ausrüstung] ADD BasisAusrüstung nvarchar(200) NULL;
UPDATE [Ausrüstung] SET BasisAusrüstung = Name;
CREATE INDEX I_Ausrüstung_BasisAusrüstung on Ausrüstung (BasisAusrüstung);
CREATE INDEX I_Ausrüstung_Name on Ausrüstung (Name);

-- _Setting-Tabellen um Name erweitert
ALTER TABLE [Zauber_Setting] ADD Name nvarchar(200) NULL;
ALTER TABLE [Sonderfertigkeit_Setting] ADD Name nvarchar(200) NULL;
ALTER TABLE [Ausrüstung_Setting] ADD Name nvarchar(200) NULL;
ALTER TABLE [Handelsgut_Setting] ADD Name nvarchar(200) NULL;
ALTER TABLE [Zauberzeichen_Setting] ADD Name nvarchar(200) NULL;

--Anpassungen an Zaubern

CREATE INDEX I_Zauber_Name on Zauber (Name);
ALTER TABLE [Zauber] ADD Magieresistenz bit NOT NULL DEFAULT 0;

DROP TABLE  [Zauber_Variante];
CREATE TABLE [Zauber_Variante] (
	[ZauberGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Name] nvarchar(200) NOT NULL, 
	[Erschwernis] int NOT NULL DEFAULT 0,
	[MinZfW] int NOT NULL DEFAULT 0,
	[SpoMod] bit NOT NULL DEFAULT 0, 
	[MehrfachAnwendbar] bit NOT NULL DEFAULT 0,
	[Eigenschaft1] nvarchar(2), 
	[Eigenschaft2] nvarchar(2), 
	[Eigenschaft3] nvarchar(2), 
	[Merkmale] nvarchar(300), 
	[Zauberdauer] nvarchar(300), 
	[Wirkungsdauer] nvarchar(300), 
	[Reichweite] nvarchar(300), 
	[Wirkungsradius] nvarchar(300), 
	[Zielobjekt] nvarchar(300), 
	[Kosten] nvarchar(300), 
	[Magieresistenz] bit,
	[Tags] ntext, 
	[Bemerkung] ntext,
	CONSTRAINT [PK_Zauber_Variante] PRIMARY KEY ([ZauberGUID], [Name]), 
	FOREIGN KEY ([ZauberGUID])
		REFERENCES [Zauber] ([ZauberGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
);
