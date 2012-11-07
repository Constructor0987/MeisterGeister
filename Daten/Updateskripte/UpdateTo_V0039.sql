-- Rasse und Kultur neu
CREATE TABLE [Rasse] (
	[RasseId] bigint NOT NULL IDENTITY, 
	[Name] nvarchar(50) NOT NULL, 
	[Variante] nvarchar(50) NOT NULL, 
	[GP] int NOT NULL DEFAULT 0, 
	[Größe] int NOT NULL DEFAULT 0, 
	[GrößeMod] nvarchar(10) DEFAULT '2W20', 
	[Gewicht] int NOT NULL DEFAULT 0, 
	[MUMod] int NOT NULL DEFAULT 0, 
	[KLMod] int NOT NULL DEFAULT 0, 
	[INMod] int NOT NULL DEFAULT 0, 
	[CHMod] int NOT NULL DEFAULT 0, 
	[FFMod] int NOT NULL DEFAULT 0, 
	[GEMod] int NOT NULL DEFAULT 0, 
	[KOMod] int NOT NULL DEFAULT 0, 
	[KKMod] int NOT NULL DEFAULT 0, 
	[LEMod] int NOT NULL DEFAULT 0, 
	[AUMod] int NOT NULL DEFAULT 0, 
	[AEMod] int NOT NULL DEFAULT 0, 
	[MRMod] int NOT NULL DEFAULT 0, 
	[INIMod] int NOT NULL DEFAULT 0, 
	[Literatur] nvarchar(300),
	CONSTRAINT [PK_Rasse] PRIMARY KEY ([RasseId])
)
GO
CREATE UNIQUE INDEX [Rasse_Unique] ON [Rasse] (
	[Name], 
	[Variante]
)
GO

CREATE TABLE [Kultur] (
	[KulturId] bigint NOT NULL IDENTITY, 
	[Name] nvarchar(200) NOT NULL, 
	[Variante] nvarchar(200) NOT NULL, 
	[GP] int NOT NULL DEFAULT 0, 
	[SOmin] int, 
	[SOmax] int, 
	[Voraussetzungen] nvarchar(300), 
	[MUMod] int NOT NULL DEFAULT 0, 
	[KLMod] int NOT NULL DEFAULT 0, 
	[INMod] int NOT NULL DEFAULT 0, 
	[CHMod] int NOT NULL DEFAULT 0, 
	[FFMod] int NOT NULL DEFAULT 0, 
	[GEMod] int NOT NULL DEFAULT 0, 
	[KOMod] int NOT NULL DEFAULT 0, 
	[KKMod] int NOT NULL DEFAULT 0, 
	[LEMod] int NOT NULL DEFAULT 0, 
	[AUMod] int NOT NULL DEFAULT 0, 
	[MRMod] int NOT NULL DEFAULT 0, 
	[Literatur] nvarchar(300),
	CONSTRAINT [PK_Kultur] PRIMARY KEY ([KulturId])
)
GO
CREATE UNIQUE INDEX [Kultur_Unique] ON [Kultur] (
	[Name], 
	[Variante]
)
GO

ALTER TABLE [Held] DROP COLUMN [RS]
GO