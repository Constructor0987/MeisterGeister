-- GUID Umstellung
--Skript zur Umstellung auf GUIDs
--09.02.2012 18:31:50 Jonas Tampier

ALTER TABLE [Held] ADD [HeldGUID] uniqueidentifier NOT NULL  ROWGUIDCOL default newid()
GO

CREATE TABLE [Held_Sonderfertigkeit2] (
  [HeldGUID] uniqueidentifier NOT NULL default '00000000-0000-0000-0000-000000000000'
, [SonderfertigkeitID] int NOT NULL
, [Wert] nvarchar(1200) NULL
)
GO
INSERT INTO [Held_Sonderfertigkeit2] ( [HeldGUID], [SonderfertigkeitID], [Wert] ) SELECT [Held].[HeldGUID], T.[SonderfertigkeitID], T.[Wert] FROM [Held], [Held_Sonderfertigkeit] as T where [Held].[HeldID]=T.[HeldID]
GO
DROP TABLE [Held_Sonderfertigkeit]
GO
--macht einen fehler, führt aber alles korrekt aus
sp_rename 'Held_Sonderfertigkeit2', 'Held_Sonderfertigkeit'
GO
ALTER TABLE [Held_Sonderfertigkeit] ADD CONSTRAINT [PK_Held_Sonderfertigkeit] PRIMARY KEY ([HeldGUID],[SonderfertigkeitID])
GO


CREATE TABLE [Held_Talent2] (
  [Talentname] nvarchar(100) NOT NULL
, [TaW] int NULL DEFAULT 0
, [HeldGUID] uniqueidentifier NOT NULL default '00000000-0000-0000-0000-000000000000'
, [Bemerkung] nvarchar(300) NULL
, [ZuteilungAT] int NULL
, [ZuteilungPA] int NULL
)
GO
INSERT INTO [Held_Talent2] ( [HeldGUID], [Talentname], [TaW], [Bemerkung], [ZuteilungAT], [ZuteilungPA] ) SELECT [Held].[HeldGUID], T.[Talentname], T.[TaW], T.[Bemerkung], T.[ZuteilungAT], T.[ZuteilungPA] FROM [Held], [Held_Talent] as T where [Held].[HeldID]=T.[HeldID]
GO
DROP TABLE [Held_Talent]
GO
--macht einen fehler, führt aber alles korrekt aus
sp_rename 'Held_Talent2', 'Held_Talent'
GO
ALTER TABLE [Held_Talent] ADD CONSTRAINT [PK__Held_Talent__00000000000000B1] PRIMARY KEY ([Talentname],[HeldGUID])
GO


CREATE TABLE [Held_VorNachteil2] (
  [HeldGUID] uniqueidentifier NOT NULL default '00000000-0000-0000-0000-000000000000'
, [VorNachteilID] int NOT NULL
, [Wert] nvarchar(255) NULL
)
GO
INSERT INTO [Held_VorNachteil2] ( [HeldGUID], [VorNachteilID], [Wert] ) SELECT [Held].[HeldGUID], T.[VorNachteilID], T.[Wert] FROM [Held], [Held_VorNachteil] as T where [Held].[HeldID]=T.[HeldID]
GO
DROP TABLE [Held_VorNachteil]
GO
--macht einen fehler, führt aber alles korrekt aus
sp_rename 'Held_VorNachteil2', 'Held_VorNachteil'
GO
ALTER TABLE [Held_VorNachteil] ADD CONSTRAINT [PK_Held_VorNachteil] PRIMARY KEY ([HeldGUID],[VorNachteilID])
GO


CREATE TABLE [Held_Waffe2] (
  [ID] int NOT NULL  IDENTITY (1,1)
, [HeldGUID] uniqueidentifier NOT NULL default '00000000-0000-0000-0000-000000000000'
, [WaffeId] int NOT NULL
, [Talentname] nvarchar(100) NULL
)
GO
INSERT INTO [Held_Waffe2] ( [HeldGUID], [WaffeID], [Talentname] ) SELECT [Held].[HeldGUID], T.[WaffeID], T.[Talentname] FROM [Held], [Held_Waffe] as T where [Held].[HeldID]=T.[HeldID]
GO
DROP TABLE [Held_Waffe]
GO
--macht einen fehler, führt aber alles korrekt aus
sp_rename 'Held_Waffe2', 'Held_Waffe'
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [PK_Held_Waffe] PRIMARY KEY ([ID])
GO


CREATE TABLE [Held_Zauber2] (
  [HeldGUID] uniqueidentifier NOT NULL default '00000000-0000-0000-0000-000000000000'
, [ZauberID] int NOT NULL
, [ZfW] int NULL DEFAULT 0
, [Repräsentation] nvarchar(50) NOT NULL DEFAULT 'Mag'
, [Bemerkung] nvarchar(300) NULL
)
GO
INSERT INTO [Held_Zauber2] ( [HeldGUID], [ZauberID], [ZfW], [Repräsentation], [Bemerkung] ) SELECT [Held].[HeldGUID], T.[ZauberID], T.[ZfW], T.[Repräsentation], T.[Bemerkung] FROM [Held], [Held_Zauber] as T where [Held].[HeldID]=T.[HeldID]
GO
DROP TABLE [Held_Zauber]
GO
--macht einen fehler, führt aber alles korrekt aus
sp_rename 'Held_Zauber2', 'Held_Zauber'
GO
ALTER TABLE [Held_Zauber] ADD CONSTRAINT [PK__Held_Zauber__00000000000002F5] PRIMARY KEY ([HeldGUID],[ZauberID],[Repräsentation])
GO

-- Held umstellen
ALTER TABLE [Held] DROP CONSTRAINT [PK__Held__00000000000001DD]
GO
ALTER TABLE [Held] DROP CONSTRAINT [UQ__Held__00000000000001E2]
GO
ALTER TABLE [Held] DROP COLUMN [HeldID]
GO
ALTER TABLE [Held] ADD CONSTRAINT [PK__Held__00000000000001DD] PRIMARY KEY ([HeldGUID]);
GO


--Foreign keys
ALTER TABLE [Held_Sonderfertigkeit] ADD CONSTRAINT [Held_Sonderfertigkeit_HeldFK] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Sonderfertigkeit] ADD CONSTRAINT [Held_Sonderfertigkeit_SonderfertigkeitFK] FOREIGN KEY ([SonderfertigkeitID]) REFERENCES [Sonderfertigkeit]([SonderfertigkeitID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Talent] ADD CONSTRAINT [Held_FK] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Talent] ADD CONSTRAINT [Talent_FK] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_VorNachteil] ADD CONSTRAINT [Held_VorNachteil_HeldFK] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_VorNachteil] ADD CONSTRAINT [Held_VorNachteil_VorNachteilFK] FOREIGN KEY ([VorNachteilID]) REFERENCES [VorNachteil]([VorNachteilID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Waffe] FOREIGN KEY ([WaffeId]) REFERENCES [Waffe]([WaffeID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Zauber] ADD CONSTRAINT [Held_Zauber_HeldFK] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Held_Zauber] ADD CONSTRAINT [Zauber_FK] FOREIGN KEY ([ZauberID]) REFERENCES [Zauber]([ZauberID]) ON DELETE CASCADE ON UPDATE CASCADE;