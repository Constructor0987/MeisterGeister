-- Nicht mehr benötigte Einstellungen löschen
DELETE FROM Einstellungen WHERE Name = 'NotizFontSize';

-- BugFix Sonderfertigkeit
UPDATE Sonderfertigkeit SET Name = 'Liturgie: Handwerkssegen' WHERE Name = 'Liturgie: Handwerksegen';
UPDATE Sonderfertigkeit SET Name = 'Reaktivierungsgespür (Runenkunde)' WHERE Name = 'Reaktivierungsgespür (Rundenkunde)';
UPDATE Sonderfertigkeit SET Name = 'Spontanzeichen (Runenkunde)' WHERE Name = 'Spontanzeichen (Rundenkunde)';