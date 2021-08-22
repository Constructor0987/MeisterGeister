-- Bilderpfade für alle Gegenständer ---
ALTER TABLE Handelsgut ADD "Pfad" nvarchar(500);
ALTER TABLE Waffe ADD "Pfad" nvarchar(500);
ALTER TABLE Fernkampfwaffe ADD "Pfad" nvarchar(500);
ALTER TABLE Schild ADD "Pfad" nvarchar(500);
ALTER TABLE Rüstung ADD "Pfad" nvarchar(500);

