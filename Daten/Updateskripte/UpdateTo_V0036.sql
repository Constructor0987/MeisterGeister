-- Strukturpunkte bei Rüstungen und Vorraussetzungen für Sonderfertigkeiten
ALTER TABLE [Held_Rüstung] ADD [Strukturpunkte] int NULL  
GO
ALTER TABLE [Sonderfertigkeit] ADD [Vorraussetzungen] nvarchar(500) NULL 
GO
ALTER TABLE [Sonderfertigkeit] ADD [Literatur] nvarchar(300) NULL