-- Mehrere Sonderfertigkeiten mit gleichem Namen möglich machen
ALTER TABLE Held_Sonderfertigkeit DROP CONSTRAINT PK_Held_Sonderfertigkeit;
ALTER TABLE Held_Sonderfertigkeit ADD Id bigint NOT NULL IDENTITY;
ALTER TABLE Held_Sonderfertigkeit ADD CONSTRAINT PK_Held_Sonderfertigkeit PRIMARY KEY (Id);
CREATE UNIQUE INDEX UQ_Held_Sonderfertigkeit ON Held_Sonderfertigkeit (
	HeldGUID, SonderfertigkeitGUID, Wert
);
-- Sonderfertigkeiten mit mehreren Werten splitten
--#WHILE;
SELECT COUNT(*) FROM Held_Sonderfertigkeit WHERE Wert like '%,%';
--#DO;
INSERT INTO Held_Sonderfertigkeit (HeldGUID, SonderfertigkeitGUID, Wert)
SELECT 
		[HeldGUID],
		[SonderfertigkeitGUID],
		RTRIM(LTRIM(substring(Wert, 1, CHARINDEX(',',Wert,0)-1))) as Wert1
	FROM [Held_Sonderfertigkeit] where Wert like '%,%';
UPDATE Held_Sonderfertigkeit SET Wert=RTRIM(LTRIM(substring(Wert, CHARINDEX(',',Wert,0)+1, LEN(Wert))))
WHERE Wert like '%,%';
--#END;
