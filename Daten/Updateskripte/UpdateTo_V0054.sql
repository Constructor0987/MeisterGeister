-- Zauber: Literatur-Spalte geändert
ALTER TABLE [Zauber] DROP COLUMN [Quelle]
GO
ALTER TABLE [Zauber] ADD [Literatur] nvarchar(300)
GO

-- Zauber: Setting-Spalte hinzugefügt
ALTER TABLE [Zauber] ADD [Setting] nvarchar(100)
GO

-- Talent: Literatur-Spalte geändert
ALTER TABLE [Talent] ADD [Literatur] nvarchar(300)
GO
UPDATE [Talent] SET [Literatur] = [Quelle]
GO
ALTER TABLE [Talent] DROP COLUMN [Quelle]
GO