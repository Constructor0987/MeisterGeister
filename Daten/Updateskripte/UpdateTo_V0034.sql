-- Tabellen für Waffen und Rüstungen, Inventar
CREATE TABLE [Fernkampfwaffe] (
	[FernkampfwaffeId] int NOT NULL IDENTITY, 
	[Name] nvarchar(200) NOT NULL, 
	[Preis] float NOT NULL DEFAULT 0, 
	[Munitionspreis] float, 
	[Gewicht] int NOT NULL DEFAULT 0, 
	[Improvisiert] bit NOT NULL DEFAULT 0, 
	[TPWürfel] int, 
	[TPWürfelAnzahl] int, 
	[TPBonus] int, 
	[AusdauerSchaden] bit NOT NULL DEFAULT 0, 
	[TPKKSchwelle] int, 
	[TPKKSchritt] int, 
	[Verwundend] bit NOT NULL DEFAULT 0, 
	[RWSehrNah] int, 
	[RWNah] int, 
	[RWMittel] int, 
	[RWWeit] int, 
	[RWSehrWeit] int, 
	[TPSehrNah] int, 
	[TPNah] int, 
	[TPMittel] int, 
	[TPWeit] int, 
	[TPSehrWeit] int, 
	[Laden] int, 
	[Verbreitung] nvarchar(300), 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(100), 
	[Bemerkung] ntext,
	CONSTRAINT [PK_Fernkampfwaffe] PRIMARY KEY ([FernkampfwaffeId])
)
GO
CREATE TABLE [Rüstung] (
	[RüstungId] int NOT NULL IDENTITY, 
	[Name] nvarchar(200) NOT NULL, 
	[Preis] float NOT NULL DEFAULT 0, 
	[Gewicht] int NOT NULL DEFAULT 0, 
	[Gruppe] nvarchar(100), 
	[Verarbeitung] int DEFAULT 0, 
	[Art] nvarchar(1), 
	[Kopf] int, 
	[Brust] int, 
	[Rücken] int, 
	[Bauch] int, 
	[LArm] int, 
	[RArm] int, 
	[LBein] int, 
	[RBein] int, 
	[gRS] int, 
	[gBE] int, 
	[RS] int, 
	[BE] int, 
	[Verbreitung] nvarchar(300), 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(100), 
	[Bemerkung] ntext,
	CONSTRAINT [PK_Rüstung] PRIMARY KEY ([RüstungId])
)
GO

CREATE TABLE [Schild] (
	[SchildId] int NOT NULL IDENTITY, 
	[Name] nvarchar(200) NOT NULL, 
	[Preis] float NOT NULL DEFAULT 0, 
	[Gewicht] int NOT NULL DEFAULT 0, 
	[Größe] nvarchar(10), 
	[Typ] nvarchar(2) NOT NULL DEFAULT 'S', 
	[WMAT] int NOT NULL DEFAULT 0, 
	[WMPA] int NOT NULL DEFAULT 0, 
	[INI] int NOT NULL DEFAULT 0, 
	[BF] int NOT NULL DEFAULT 0, 
	[Verbreitung] nvarchar(300), 
	[Literatur] nvarchar(300), 
	[Setting] nvarchar(100), 
	[Bemerkung] ntext,
	CONSTRAINT [PK_Schild] PRIMARY KEY ([SchildId])
)
GO

CREATE TABLE [Fernkampfwaffe_Talent] (
	[FernkampfwaffeId] int NOT NULL, 
	[Talentname] nvarchar(100) NOT NULL,
	CONSTRAINT [PK_Fernkampfwaffe_Talent] PRIMARY KEY ([FernkampfwaffeId], [Talentname]), 
	CONSTRAINT fk_FernkampfwaffeTalent_Talent FOREIGN KEY ([Talentname])
		REFERENCES Talent ([Talentname])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_FernkampfwaffeTalent_Waffe FOREIGN KEY ([FernkampfwaffeId])
		REFERENCES Fernkampfwaffe ([FernkampfwaffeId])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO


DROP TABLE [Held_Waffe];
GO

CREATE TABLE [Held_Waffe] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
,	[Angelegt] bit NOT NULL DEFAULT 0
,	[Ort] nvarchar(50) NOT NULL DEFAULT 'RArm'
, [WaffeId] int NOT NULL
, [Talentname] nvarchar(100) NULL
,	[Anzahl] int NULL
);
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [PK_Held_Waffe] PRIMARY KEY ([HeldGUID], [Ort], [WaffeId] );
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Waffe] FOREIGN KEY ([WaffeId]) REFERENCES [Waffe]([WaffeID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO

CREATE TABLE [Held_Fernkampfwaffe] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
,	[Angelegt] bit NOT NULL DEFAULT 0
,	[Ort] nvarchar(50) NOT NULL DEFAULT 'RArm'
, [FernkampfwaffeId] int NOT NULL
, [Talentname] nvarchar(100) NULL
,	[Anzahl] int NULL
);
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [PK_Held_Fernkampfwaffe] PRIMARY KEY ([HeldGUID], [Ort], [FernkampfwaffeId] );
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [fk_HeldFernkampfwaffe_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [fk_HeldFernkampfwaffe_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Fernkampfwaffe] ADD CONSTRAINT [fk_HeldFernkampfwaffe_Fernkampfwaffe] FOREIGN KEY ([FernkampfwaffeId]) REFERENCES [Fernkampfwaffe]([FernkampfwaffeID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO


CREATE TABLE [Held_Rüstung] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
,	[Angelegt] bit NOT NULL DEFAULT 0
,	[Ort] nvarchar(50) NOT NULL DEFAULT 'RArm'
, [RüstungId] int NOT NULL
,	[Anzahl] int NULL
);
GO
ALTER TABLE [Held_Rüstung] ADD CONSTRAINT [PK_Held_Rüstung] PRIMARY KEY ([HeldGUID], [Ort], [RüstungId] );
GO
ALTER TABLE [Held_Rüstung] ADD CONSTRAINT [fk_HeldRüstung_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Rüstung] ADD CONSTRAINT [fk_HeldRüstung_Rüstung] FOREIGN KEY ([RüstungId]) REFERENCES [Rüstung]([RüstungID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO

CREATE TABLE [Held_Schild] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
,	[Angelegt] bit NOT NULL DEFAULT 0
,	[Ort] nvarchar(50) NOT NULL DEFAULT 'LArm'
, [SchildId] int NOT NULL
,	[Anzahl] int NULL
);
GO
ALTER TABLE [Held_Schild] ADD CONSTRAINT [PK_Held_Schild] PRIMARY KEY ([HeldGUID], [Ort], [SchildId] );
GO
ALTER TABLE [Held_Schild] ADD CONSTRAINT [fk_HeldSchild_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Schild] ADD CONSTRAINT [fk_HeldSchild_Schild] FOREIGN KEY ([SchildId]) REFERENCES [Schild]([SchildID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO


CREATE TABLE [Inventar] (
	[InventarId] int NOT NULL IDENTITY, 
	[Name] nvarchar(500), 
	[Preis] nvarchar(100) DEFAULT 0, 
	[Gewicht] float, 
	[ME] nvarchar(100), 
	[Kategorie] nvarchar(500), 
	[Tags] nvarchar(1000), 
	[Bemerkung] ntext, 
	[Literatur] nvarchar(500),
	[HandelsgutId] int NULL,
	CONSTRAINT [PK_Inventar] PRIMARY KEY ([InventarId])
)
GO

CREATE TABLE [Held_Inventar] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
,	[Angelegt] bit NOT NULL DEFAULT 0
,	[Ort] nvarchar(50) NOT NULL DEFAULT 'Rucksack'
, [InventarId] int NOT NULL
,	[Anzahl] int NULL
);
GO
ALTER TABLE [Held_Inventar] ADD CONSTRAINT [PK_Held_Inventar] PRIMARY KEY ([HeldGUID], [Ort], [InventarId] );
GO
ALTER TABLE [Held_Inventar] ADD CONSTRAINT [fk_HeldInventar_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Inventar] ADD CONSTRAINT [fk_HeldInventar_Inventar] FOREIGN KEY ([InventarId]) REFERENCES [Inventar]([InventarID]) ON DELETE CASCADE ON UPDATE CASCADE;