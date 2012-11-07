-- Datum
INSERT INTO Einstellungen (Name) VALUES ('DatumAktuell');
INSERT INTO Einstellungen (Name, WertBool) VALUES ('FrageNeueKampfrundeAbstellen', 0);

-- Myranor Talente
INSERT INTO Talent (Talentname, TalentgruppeID, Talenttyp, Quelle, eBE, Steigerung) VALUES ('Bastardstäbe', 1, 'Spezial', 'Myranor 223', 'BE-2', 'D');
INSERT INTO Talent (Talentname, TalentgruppeID, Eigenschaft1, Eigenschaft2, Eigenschaft3, Talenttyp, Quelle, eBE, Spezialisierungen, Steigerung) VALUES ('Freies Fliegen', 2, 'MU', 'GE', 'KK', 'Basis/Spezial', 'Myranor 211', 'BEx2', 'eigene Flügel, technomantische Hilfsmittel, Magie (nach Art)', 'D');
INSERT INTO Talent (Talentname, TalentgruppeID, Eigenschaft1, Eigenschaft2, Eigenschaft3, Talenttyp, Quelle, Spezialisierungen, Steigerung) VALUES ('Fluggeräte Steuern', 6, 'MU', 'IN', 'FF', 'Spezial', 'Myranor 212', 'Ballon, fliegender Teppich, Gleiter, Insektopter', 'B');

-- Erweiterung: Held-Tabelle
ALTER TABLE Held ADD AktiveHeldengruppe bit DEFAULT 1;
UPDATE Held SET AktiveHeldengruppe = 1;
ALTER TABLE Held ADD Proben bit DEFAULT 1;
UPDATE Held SET Proben = 1;