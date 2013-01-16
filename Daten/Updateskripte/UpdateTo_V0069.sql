-- Gegner_Angriff DK mehr Zeichen
ALTER TABLE GegnerBase_Angriff ALTER COLUMN DK nvarchar(5)
GO

-- Gegner_Angriff um PA ergänzen
ALTER TABLE GegnerBase_Angriff ADD PA int NOT NULL DEFAULT 0
GO
