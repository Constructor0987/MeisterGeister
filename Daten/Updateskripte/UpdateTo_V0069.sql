-- Gegner_Angriff DK mehr Zeichen
ALTER TABLE GegnerBase_Angriff ALTER COLUMN DK nvarchar(5)
GO
-- Gegner_Angriff um PA ergänzen
ALTER TABLE GegnerBase_Angriff ADD PA int NOT NULL DEFAULT 0
GO
-- Globale Kampfwerte bei Gegnern
ALTER TABLE GegnerBase ADD AT int NOT NULL DEFAULT 0
GO
ALTER TABLE Gegner ADD AT int NOT NULL DEFAULT 0
GO
ALTER TABLE GegnerBase ADD FK int NOT NULL DEFAULT 0
GO
ALTER TABLE Gegner ADD FK int NOT NULL DEFAULT 0
GO