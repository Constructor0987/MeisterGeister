--Held_Ausrüstung nur noch einmal JE ausrüstung.
ALTER TABLE Held_Ausrüstung DROP CONSTRAINT PK_Held_Ausrüstung;
ALTER TABLE Held_Ausrüstung ADD Id bigint NOT NULL IDENTITY;
ALTER TABLE Held_Ausrüstung ADD CONSTRAINT PK_Held_Ausrüstung PRIMARY KEY (Id);
-- Held_Ausrüstung mit Anzahl > 1
--#WHILE;
SELECT COUNT(*) FROM Held_Ausrüstung WHERE Anzahl > 1 and Anzahl is not null;
--#DO;
INSERT INTO Held_Ausrüstung (HeldGUID, AusrüstungGUID, Angelegt, TalentGUID, BF, TrageortGUID)
SELECT 
		[HeldGUID],
		[AusrüstungGUID],
		Angelegt, TalentGUID, BF, TrageortGUID
	FROM [Held_Ausrüstung] where Anzahl > 1 and Anzahl is not null;
UPDATE Held_Ausrüstung SET Anzahl=Anzahl-1 WHERE  Anzahl > 1 and Anzahl is not null;
--#END;
ALTER TABLE Held_Ausrüstung DROP COLUMN Anzahl;

--TODO Waffenset

--TODO MT: Datenanpassungen Literatur

--TODO MT: Anpassungen für DSA5