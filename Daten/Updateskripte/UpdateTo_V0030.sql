-- Waren-Tabelle umbenennen
DROP TABLE Ware;
CREATE TABLE [Handelsgut] (
  [HandelsgutId] int NOT NULL  IDENTITY (1,1)
, [Name] nvarchar(500) NULL
, [Preis] nvarchar(100) NULL DEFAULT 0
, [Gewicht] float NULL
, [ME] nvarchar(100) NULL
, [Kategorie] nvarchar(500) NULL
, [Tags] nvarchar(1000) NULL
, [Bemerkung] ntext NULL
, [Literatur] nvarchar(500) NULL
);
ALTER TABLE [Handelsgut] ADD CONSTRAINT [PK_Handelsgut] PRIMARY KEY ([HandelsgutId]);