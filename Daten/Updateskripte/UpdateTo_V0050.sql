-- Tabellen für Abenteuer-Tool
CREATE TABLE [Abenteuer] (
[AbenteuerGUID] uniqueidentifier NOT NULL DEFAULT newid(),
[Name] nvarchar(200) NOT NULL,
CONSTRAINT [PK_Abenteuer] PRIMARY KEY ([AbenteuerGUID])
)
GO

CREATE TABLE [Abenteuer_Szene] (
[SzeneGUID] uniqueidentifier NOT NULL DEFAULT newid(),
[AbenteuerGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
[Name] nvarchar(200) NOT NULL,
CONSTRAINT [PK_Abenteuer_Szene] PRIMARY KEY ([SzeneGUID]),
CONSTRAINT fk_AbenteuerSzene_Abenteuer FOREIGN KEY ([AbenteuerGUID])
		REFERENCES Abenteuer ([AbenteuerGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE [Abenteuer_Ereignis] (
[EreignisGUID] uniqueidentifier NOT NULL DEFAULT newid(),
[SzeneGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
[Name] nvarchar(200) NOT NULL,
CONSTRAINT [PK_Abenteuer_Ereignis] PRIMARY KEY ([EreignisGUID]),
CONSTRAINT fk_AbenteuerEreignis_Szene FOREIGN KEY ([SzeneGUID])
		REFERENCES Abenteuer_Szene ([SzeneGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE [Abenteuer_Verweis] (
[VerweisGUID] uniqueidentifier NOT NULL DEFAULT newid(),
[VonSzeneGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
[NachSzeneGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
[VonEreignisGUID] uniqueidentifier,
[NachEreignisGUID] uniqueidentifier,
CONSTRAINT [PK_Abenteuer_Verweis] PRIMARY KEY ([VerweisGUID]),
CONSTRAINT fk_AbenteuerVerweis_VonSzene FOREIGN KEY ([VonSzeneGUID])
		REFERENCES Abenteuer_Szene ([SzeneGUID]),
CONSTRAINT fk_AbenteuerVerweis_NachSzene FOREIGN KEY ([NachSzeneGUID])
		REFERENCES Abenteuer_Szene ([SzeneGUID]),
CONSTRAINT fk_AbenteuerVerweis_VonEreignis FOREIGN KEY ([VonEreignisGUID])
		REFERENCES Abenteuer_Ereignis ([EreignisGUID]),
CONSTRAINT fk_AbenteuerVerweis_NachEreignis FOREIGN KEY ([NachEreignisGUID])
		REFERENCES Abenteuer_Ereignis ([EreignisGUID])
)
GO