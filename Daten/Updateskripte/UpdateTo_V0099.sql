
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
