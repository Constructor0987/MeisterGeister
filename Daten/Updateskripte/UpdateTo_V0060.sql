/* Daten-Fehler */
UPDATE Held_Ausrüstung SET AusrüstungGUID='00000000-0000-0000-0001-000000000026' WHERE AusrüstungGUID='00000000-0000-0000-0002-000000000010'
GO
UPDATE Fernkampfwaffe SET FernkampfwaffeGUID='00000000-0000-0000-0001-000000000026' WHERE FernkampfwaffeGUID='00000000-0000-0000-0002-000000000010'
GO
DELETE FROM Ausrüstung WHERE AusrüstungGUID='00000000-0000-0000-0002-000000000010'
GO

/* Strukturänderungen */
ALTER TABLE VorNachteil add [Literatur] nvarchar(500) NULL
GO
ALTER TABLE [Sonderfertigkeit] ADD [Voraussetzungen] ntext NULL
GO
UPDATE [Sonderfertigkeit] SET [Voraussetzungen] = [Vorraussetzungen]
GO
ALTER TABLE [Sonderfertigkeit] DROP COLUMN [Vorraussetzungen]
GO

/* Setting auslagern */
CREATE TABLE Setting (
	[SettingGUID] uniqueidentifier NOT NULL DEFAULT newid(),
	[Name] nvarchar(100) NOT NULL,
	CONSTRAINT [PK_Setting] PRIMARY KEY ([SettingGUID])
)
GO
INSERT INTO Setting (SettingGUID, Name) VALUES ('00000000-0000-0000-5e77-000000000001','Aventurien')
GO
INSERT INTO Setting (SettingGUID, Name) VALUES ('00000000-0000-0000-5e77-000000000002','Dunkle Zeiten')
GO
INSERT INTO Setting (SettingGUID, Name) VALUES ('00000000-0000-0000-5e77-000000000003','Myranor')
GO
INSERT INTO Setting (SettingGUID, Name) VALUES ('00000000-0000-0000-5e77-000000000004','Rakshazar')
GO

CREATE TABLE [Ausrüstung_Setting] (
	[AusrüstungGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[SettingGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Preis] float not null DEFAULT 0, 
	[Verbreitung] nvarchar(300) NULL, 
	CONSTRAINT [PK_Ausrüstung_Setting] PRIMARY KEY ([AusrüstungGUID], [SettingGUID]), 
	CONSTRAINT fk_AusrüstungSetting_Ausrüstung FOREIGN KEY ([AusrüstungGUID])
		REFERENCES Ausrüstung ([AusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_AusrüstungSetting_Setting FOREIGN KEY ([SettingGUID])
		REFERENCES Setting ([SettingGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
INSERT INTO [Ausrüstung_Setting] ([AusrüstungGUID],[SettingGUID],[Preis],[Verbreitung])
SELECT [AusrüstungGUID], '00000000-0000-0000-5e77-000000000001', [Preis], [Verbreitung] FROM Ausrüstung WHERE Setting is null or Setting like '%Aventurien%'
GO
INSERT INTO [Ausrüstung_Setting] ([AusrüstungGUID],[SettingGUID],[Preis],[Verbreitung])
SELECT [AusrüstungGUID], '00000000-0000-0000-5e77-000000000002', [Preis], [Verbreitung] FROM Ausrüstung WHERE Setting like '%Dunkle Zeiten%'
GO

ALTER TABLE Ausrüstung DROP COLUMN Setting
GO
ALTER TABLE Ausrüstung DROP COLUMN Preis
GO
ALTER TABLE Ausrüstung DROP COLUMN Verbreitung
GO

CREATE TABLE [Handelsgut_Setting] (
	[HandelsgutGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[SettingGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Preis] nvarchar(100) NULL, 
	CONSTRAINT [PK_Handelsgut_Setting] PRIMARY KEY ([HandelsgutGUID], [SettingGUID]), 
	CONSTRAINT fk_HandelsgutSetting_Handelsgut FOREIGN KEY ([HandelsgutGUID])
		REFERENCES Handelsgut ([HandelsgutGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_HandelsgutSetting_Setting FOREIGN KEY ([SettingGUID])
		REFERENCES Setting ([SettingGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
INSERT INTO [Handelsgut_Setting] ([HandelsgutGUID],[SettingGUID],[Preis])
SELECT [HandelsgutGUID], '00000000-0000-0000-5e77-000000000001', [Preis] FROM Handelsgut WHERE Setting is null or Setting like '%Aventurien%'
GO
INSERT INTO [Handelsgut_Setting] ([HandelsgutGUID],[SettingGUID],[Preis])
SELECT [HandelsgutGUID], '00000000-0000-0000-5e77-000000000002', [Preis] FROM Handelsgut WHERE Setting like '%Dunkle Zeiten%'
GO
INSERT INTO [Handelsgut_Setting] ([HandelsgutGUID],[SettingGUID],[Preis])
SELECT [HandelsgutGUID], '00000000-0000-0000-5e77-000000000003', [Preis] FROM Handelsgut WHERE Setting like '%Myranor%'
GO
INSERT INTO [Handelsgut_Setting] ([HandelsgutGUID],[SettingGUID],[Preis])
SELECT [HandelsgutGUID], '00000000-0000-0000-5e77-000000000003', [Preis] FROM Handelsgut WHERE Setting like '%Rakshazar%'
GO
ALTER TABLE Handelsgut DROP COLUMN Setting
GO
ALTER TABLE Handelsgut DROP COLUMN Preis
GO

CREATE TABLE [Sonderfertigkeit_Setting] (
	[SonderfertigkeitGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[SettingGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Verbreitung] nvarchar(300) NULL, 
	CONSTRAINT [PK_Sonderfertigkeit_Setting] PRIMARY KEY ([SonderfertigkeitGUID], [SettingGUID]), 
	CONSTRAINT fk_SonderfertigkeitSetting_Sonderfertigkeit FOREIGN KEY ([SonderfertigkeitGUID])
		REFERENCES Sonderfertigkeit ([SonderfertigkeitGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_SonderfertigkeitSetting_Setting FOREIGN KEY ([SettingGUID])
		REFERENCES Setting ([SettingGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
INSERT INTO [Sonderfertigkeit_Setting] ([SonderfertigkeitGUID],[SettingGUID],[Verbreitung])
SELECT [SonderfertigkeitGUID], '00000000-0000-0000-5e77-000000000001', null FROM Sonderfertigkeit WHERE Setting is null or Setting like '%Aventurien%'
GO
INSERT INTO [Sonderfertigkeit_Setting] ([SonderfertigkeitGUID],[SettingGUID],[Verbreitung])
SELECT [SonderfertigkeitGUID], '00000000-0000-0000-5e77-000000000002', null FROM Sonderfertigkeit WHERE Setting like '%Dunkle Zeiten%'
GO
INSERT INTO [Sonderfertigkeit_Setting] ([SonderfertigkeitGUID],[SettingGUID],[Verbreitung])
SELECT [SonderfertigkeitGUID], '00000000-0000-0000-5e77-000000000003', null FROM Sonderfertigkeit WHERE Setting like '%Myranor%'
GO
INSERT INTO [Sonderfertigkeit_Setting] ([SonderfertigkeitGUID],[SettingGUID],[Verbreitung])
SELECT [SonderfertigkeitGUID], '00000000-0000-0000-5e77-000000000003', null FROM Sonderfertigkeit WHERE Setting like '%Rakshazar%'
GO

ALTER TABLE Sonderfertigkeit DROP COLUMN Setting
GO

CREATE TABLE [Zauberzeichen_Setting] (
	[ZauberzeichenGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[SettingGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Verbreitung] nvarchar(300) NULL, 
	CONSTRAINT [PK_Zauberzeichen_Setting] PRIMARY KEY ([ZauberzeichenGUID], [SettingGUID]), 
	CONSTRAINT fk_ZauberzeichenSetting_Zauberzeichen FOREIGN KEY ([ZauberzeichenGUID])
		REFERENCES Zauberzeichen ([ZauberzeichenGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_ZauberzeichenSetting_Setting FOREIGN KEY ([SettingGUID])
		REFERENCES Setting ([SettingGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
INSERT INTO [Zauberzeichen_Setting] ([ZauberzeichenGUID],[SettingGUID],[Verbreitung])
SELECT [ZauberzeichenGUID], '00000000-0000-0000-5e77-000000000001', [Verbreitung] FROM Zauberzeichen WHERE Setting is null or Setting like '%Aventurien%'
GO
INSERT INTO [Zauberzeichen_Setting] ([ZauberzeichenGUID],[SettingGUID],[Verbreitung])
SELECT [ZauberzeichenGUID], '00000000-0000-0000-5e77-000000000002', [Verbreitung] FROM Zauberzeichen WHERE Setting like '%Dunkle Zeiten%'
GO
INSERT INTO [Zauberzeichen_Setting] ([ZauberzeichenGUID],[SettingGUID],[Verbreitung])
SELECT [ZauberzeichenGUID], '00000000-0000-0000-5e77-000000000003', [Verbreitung] FROM Zauberzeichen WHERE Setting like '%Myranor%'
GO
INSERT INTO [Zauberzeichen_Setting] ([ZauberzeichenGUID],[SettingGUID],[Verbreitung])
SELECT [ZauberzeichenGUID], '00000000-0000-0000-5e77-000000000003', [Verbreitung] FROM Zauberzeichen WHERE Setting like '%Rakshazar%'
GO

ALTER TABLE Zauberzeichen DROP COLUMN Setting
GO
ALTER TABLE Zauberzeichen DROP COLUMN Verbreitung
GO

CREATE TABLE [Zauber_Setting] (
	[ZauberGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[SettingGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Repräsentationen] nvarchar(300) NULL, 
	CONSTRAINT [PK_Zauber_Setting] PRIMARY KEY ([ZauberGUID], [SettingGUID]), 
	CONSTRAINT fk_ZauberSetting_Zauber FOREIGN KEY ([ZauberGUID])
		REFERENCES Zauber ([ZauberGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_ZauberSetting_Setting FOREIGN KEY ([SettingGUID])
		REFERENCES Setting ([SettingGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO
UPDATE Zauber SET Setting=Replace(Setting, 'Avenurien', 'Aventurien')
GO
INSERT INTO [Zauber_Setting] ([ZauberGUID],[SettingGUID],[Repräsentationen])
SELECT [ZauberGUID], '00000000-0000-0000-5e77-000000000001', [Repräsentationen] FROM Zauber WHERE Setting is null or Setting like '%Aventurien%'
GO
INSERT INTO [Zauber_Setting] ([ZauberGUID],[SettingGUID],[Repräsentationen])
SELECT [ZauberGUID], '00000000-0000-0000-5e77-000000000002', [Repräsentationen] FROM Zauber WHERE Setting like '%Dunkle Zeiten%'
GO
INSERT INTO [Zauber_Setting] ([ZauberGUID],[SettingGUID],[Repräsentationen])
SELECT [ZauberGUID], '00000000-0000-0000-5e77-000000000003', [Repräsentationen] FROM Zauber WHERE Setting like '%Myranor%'
GO
INSERT INTO [Zauber_Setting] ([ZauberGUID],[SettingGUID],[Repräsentationen])
SELECT [ZauberGUID], '00000000-0000-0000-5e77-000000000003', [Repräsentationen] FROM Zauber WHERE Setting like '%Rakshazar%'
GO

ALTER TABLE Zauber DROP COLUMN Setting
GO
ALTER TABLE Zauber DROP COLUMN [Repräsentationen]
GO