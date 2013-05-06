-- Audio-Player TitelKategorie in die Playlists integrieren
ALTER TABLE [Audio_Playlist] ADD [Kategorie] nvarchar(500) NULL
GO

--Unterthemes für Themes im Audioplayer
CREATE TABLE [Audio_Theme_Theme] (
  [Audio_ThemeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Audio_UnterThemeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
)
GO
ALTER TABLE [Audio_Theme_Theme] ADD CONSTRAINT [PK_Audio_Theme_Theme] PRIMARY KEY ([Audio_ThemeGUID],[Audio_UnterThemeGUID])
GO
ALTER TABLE [Audio_Theme_Theme] ADD CONSTRAINT [fk_Audio_Theme_Theme_Theme1] FOREIGN KEY ([Audio_ThemeGUID]) REFERENCES [Audio_Theme]([Audio_ThemeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Audio_Theme_Theme] ADD CONSTRAINT [fk_Audio_Theme_Theme_Theme2] FOREIGN KEY ([Audio_UnterThemeGUID]) REFERENCES [Audio_Theme]([Audio_ThemeGUID])
GO

--Alchimietabellen neu
CREATE TABLE [Alchimierezept] (
	[AlchimierezeptGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(200) NOT NULL,
	[Gruppe] nvarchar(200) NOT NULL,
	[Kategorie] nvarchar(200) NOT NULL,
	[Merkmale] ntext NULL,
	[Brauschwierigkeit] int NOT NULL DEFAULT 0, 
	[Analyseschwierigkeit] int NOT NULL DEFAULT 0, 
	[Beschaffungsschwierigkeit] int NOT NULL DEFAULT 0, 
	[Beschaffungskosten] float NOT NULL DEFAULT 0,
	[Haltbarkeit] nvarchar(30) NULL,
	[Haltbarkeitseinheit] nvarchar(100) NOT NULL,
	[Labor] nvarchar(20) NOT NULL DEFAULT 'archaisches Labor',
	[Zutaten] ntext, 
	[Substitute] ntext,
	[Bemerkung] ntext, 
	[Literatur] nvarchar(300), 
	[Tags] ntext,
	CONSTRAINT [PK_Alchimierezept] PRIMARY KEY ([AlchimierezeptGUID])
)
GO

CREATE TABLE [Alchimierezept_Setting] (
	[AlchimierezeptGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[SettingGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Preis] nvarchar(100) NULL, 
	[Verbreitung] int NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Alchimierezept_Setting] PRIMARY KEY ([AlchimierezeptGUID], [SettingGUID]), 
	CONSTRAINT fk_AlchimierezeptSetting_Alchimierezept FOREIGN KEY ([AlchimierezeptGUID])
		REFERENCES Alchimierezept ([AlchimierezeptGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_AlchimierezeptSetting_Setting FOREIGN KEY ([SettingGUID])
		REFERENCES Setting ([SettingGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

--Zauber um varianten und zauberdetails erweitert
ALTER TABLE [Zauber] ADD Zauberdauer nvarchar(300) NULL
GO
ALTER TABLE [Zauber] ADD Wirkungsdauer nvarchar(300) NULL
GO
ALTER TABLE [Zauber] ADD Reichweite nvarchar(300) NULL
GO
ALTER TABLE [Zauber] ADD Wirkungsradius nvarchar(300) NULL
GO
ALTER TABLE [Zauber] ADD Zielobjekt nvarchar(300) NULL
GO
ALTER TABLE [Zauber] ADD Modifikationen nvarchar(300) NULL
GO
ALTER TABLE [Zauber] ADD Kosten nvarchar(300) NULL
GO
ALTER TABLE [Zauber] ADD Tags ntext NULL
GO
ALTER TABLE [Zauber] ADD Bemerkung ntext NULL
GO

CREATE TABLE [Zauber_Variante] (
  [ZauberGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Name] nvarchar(200) NOT NULL
, [SpoMod] bit NOT NULL DEFAULT 0
, [Eigenschaft1] nvarchar(2) NULL
, [Eigenschaft2] nvarchar(2) NULL
, [Eigenschaft3] nvarchar(2) NULL
, [Merkmale] nvarchar(300) NULL
, [Zauberdauer] nvarchar(300) NULL
, [Wirkungsdauer] nvarchar(300) NULL
, [Reichweite] nvarchar(300) NULL
, [Wirkungsradius] nvarchar(300) NULL
, [Zielobjekt] nvarchar(300) NULL
, [Kosten] nvarchar(300) NULL
, [Tags] ntext NULL
, [Bemerkung] ntext NULL
, CONSTRAINT [PK_Zauber_Variante] PRIMARY KEY ([ZauberGUID], [Name])
, CONSTRAINT fk_ZauberVariante_Zauber FOREIGN KEY ([ZauberGUID])
   REFERENCES Zauber (ZauberGUID)
   ON UPDATE CASCADE
   ON DELETE CASCADE
)
GO
