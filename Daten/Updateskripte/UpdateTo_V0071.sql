-- Zauber Korrekturen
UPDATE Zauber SET Name = 'Dämonenbann (Belshirash)' WHERE Name = 'Dämonenbann (Nagrach)';

-- Korrektur von Umbruch-Fehler in Rüstungen
UPDATE Rüstung SET Gruppe = 'Exotische Materialien' WHERE Gruppe = 'Exotische Materialien
'
GO
UPDATE Rüstung SET Gruppe = 'Kleidung' WHERE Gruppe = 'Kleidung
'
GO
UPDATE Rüstung SET Gruppe = 'Lederrüstungen' WHERE Gruppe = 'Lederrüstungen
'
GO
UPDATE Rüstung SET Gruppe = 'Plattenrüstungen' WHERE Gruppe = 'Plattenrüstungen
'
GO
UPDATE Rüstung SET Gruppe = 'Hervorragende Kette' WHERE Gruppe = 'Hervorragende Kette
'
GO
UPDATE Rüstung SET Gruppe = 'Kette' WHERE Gruppe = 'Kette
'
GO
UPDATE Rüstung SET Gruppe = 'Schuppe' WHERE Gruppe = 'Schuppe
'
GO
UPDATE Rüstung SET Gruppe = 'Tuchrüstungen' WHERE Gruppe = 'Tuchrüstungen
'
GO