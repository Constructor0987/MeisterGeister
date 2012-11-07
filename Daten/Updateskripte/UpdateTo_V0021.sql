-- Bestiarium Spalten vergrößern
ALTER TABLE Bestiarium ALTER COLUMN Besonderheiten nvarchar(1000);
ALTER TABLE Bestiarium ALTER COLUMN Sonderfertigkeiten nvarchar(1000);

-- Sozialstatus
ALTER TABLE Held ADD COLUMN SO int;