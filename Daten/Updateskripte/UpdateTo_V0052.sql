-- Erstellt neue Tabelle Trageort für Inventar, verbunden mit Held_Ausrüstung 1:1 und setzt neuen PK für Held_Ausrüstung
CREATE TABLE Trageort (
	[TrageortGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Name] nvarchar(100) NOT NULL, 
	[TragkraftFaktor] float NOT NULL DEFAULT 1,
	CONSTRAINT [PK_Trageort] PRIMARY KEY ([TrageortGUID])	
)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000001','Kopf', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000002','Linker Arm', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000003','Rechter Arm', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000004','Rechte Hand', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000005','Linke Hand', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000006','Rücken', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000007','Oberkörper', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000008','Linkes Bein', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000009','Rechtes Bein', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000010','Schwanz', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000011','Rucksack', 1)
GO
INSERT INTO [Trageort] ([TrageortGUID],[Name],[TragkraftFaktor]) 
VALUES ('00000000-0000-0000-001A-000000000012','Tragetier', 0)
GO
ALTER TABLE Held_Ausrüstung 
ADD Column TrageortGUID uniqueidentifier NOT NULL default '00000000-0000-0000-001A-000000000011'
GO
ALTER TABLE Held_Ausrüstung ADD
CONSTRAINT fk_TrageortGUID FOREIGN KEY ([TrageortGUID])
		REFERENCES Trageort ([TrageortGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
GO
Alter TABLE Held_Ausrüstung DROP CONSTRAINT [PK_Held_Ausrüstung]
GO
Alter TABLE Held_Ausrüstung 
ADD CONSTRAINT [PK_Held_Ausrüstung] PRIMARY KEY ([HeldGUID], [TrageortGUID], [AusrüstungGUID])
GO
ALTER TABLE Held_Ausrüstung DROP COLUMN Ort
GO