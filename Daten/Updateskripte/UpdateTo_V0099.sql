
-- Modifkator-Tabellen hinzufügen
CREATE TABLE [Modifikator] (
	[ModifikatorGUID] uniqueidentifier NOT NULL DEFAULT newid() PRIMARY KEY, 
	[Name] nvarchar(100) NOT NULL, 
	[Typ] nvarchar(200) NOT NULL,
	[Beschreibung] ntext,
	[Json] ntext,
	[Literatur] nvarchar(300)
)
GO
CREATE UNIQUE INDEX [UQ_Modifikator]
	ON [Modifikator] (
	[Name]
)
GO
CREATE TABLE [Held_Modifikator] (
	[Id] bigint NOT NULL IDENTITY PRIMARY KEY, 
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[ModifikatorGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Wert1] int NULL, 
	[Wert2] int NULL, 
	[Wert3] int NULL,
	[Erstellt] datetime NOT NULL DEFAULT GETDATE(),
	FOREIGN KEY ([HeldGUID])
		REFERENCES [Held] ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	FOREIGN KEY ([ModifikatorGUID])
		REFERENCES [Modifikator] ([ModifikatorGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
CREATE TABLE [Gegner_Modifikator] (
	[Id] bigint NOT NULL IDENTITY PRIMARY KEY, 
	[GegnerGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[ModifikatorGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Wert1] int NULL, 
	[Wert2] int NULL, 
	[Wert3] int NULL,
	[Erstellt] datetime NOT NULL DEFAULT GETDATE(),
	FOREIGN KEY ([GegnerGUID])
		REFERENCES [gegner] ([GegnerGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	FOREIGN KEY ([ModifikatorGUID])
		REFERENCES [Modifikator] ([ModifikatorGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
--TODO MT: Anpassungen an Literatur-Tabellen-- Fehler in Vorteil korrigieren
UPDATE [VorNachteil] SET [Setting] = 'Aventurien, Myranor', [Literatur] = 'WdH 258 / WnM 120' WHERE [Name] LIKE 'Wesen der Nacht%';

--Anpassungen an Literatur-Tabellen
ALTER TABLE [Literatur] ADD [Regelsystem] int NOT NULL DEFAULT 0;
ALTER TABLE [Literatur] ADD [Nummer] nvarchar(50) NULL;
ALTER TABLE [Literatur] ADD [Art] nvarchar(150) NULL;
ALTER TABLE [Literatur] ADD [Reihe] nvarchar(150) NULL;
ALTER TABLE [Literatur] ADD [Setting] nvarchar(150) NULL DEFAULT 'Aventurien';
ALTER TABLE [Literatur] ADD [Box] nvarchar(150) NULL;

--TODO MT: Datenanpassungen Literatur


--TODO MT: Anpassungen für DSA5
