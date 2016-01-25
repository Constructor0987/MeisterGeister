CREATE Table Fortbewegung (
	ID int NOT NULL IDENTITY (4,1) PRIMARY KEY,
	Name nvarchar(50) NOT NULL
)
GO

CREATE TABLE Fortbewegung_Modifikation (
	ID int NOT NULL IDENTITY(37,1) PRIMARY KEY,
	Fortbewegung int NOT NULL,
	Wegtyp int NOT NULL,
	Multiplikator float NOT NULL
)
GO

ALTER TABLE Fortbewegung_Modifikation ADD CONSTRAINT fk_Fortbewegung_Modifikation_Fortbewegung FOREIGN KEY (Fortbewegung) REFERENCES Fortbewegung (ID) ON UPDATE CASCADE ON DELETE CASCADE
GO

ALTER TABLE Fortbewegung_Modifikation ADD CONSTRAINT fk_Fortbewegung_Modifikation_Wegtyp FOREIGN KEY (Wegtyp) REFERENCES Wegtyp (ID) ON UPDATE CASCADE ON DELETE CASCADE
GO

SET IDENTITY_INSERT Fortbewegung ON
GO

INSERT INTO Fortbewegung (ID, Name) VALUES (1, 'Fußmarsch')
GO
INSERT INTO Fortbewegung (ID, Name) VALUES (2, 'Wanderritt')
GO
INSERT INTO Fortbewegung (ID, Name) VALUES (3, 'Reisekutsche')
GO

SET IDENTITY_INSERT Fortbewegung OFF
GO

SET IDENTITY_INSERT Fortbewegung_Modifikation ON
GO

INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (1, 1, 1, 1.1)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (2, 1, 2, 1)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (3, 1, 3, 0.8)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (4, 1, 4, 0.53)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (5, 1, 5, 0.2)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (6, 1, 6, 0.5)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (7, 1, 7, 0.5)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (8, 1, 8, 1)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (9, 1, 9, 1)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (10, 1, 10, 0.7)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (11, 1, 11, 0.3)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (12, 1, 12, 0.77)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (13, 2, 1, 1.3)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (14, 2, 2, 1.17)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (15, 2, 3, 0.93)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (16, 2, 4, 0.5)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (17, 2, 5, 0.1)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (18, 2, 6, 0.6)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (19, 2, 7, 0)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (20, 2, 8, 0)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (21, 2, 9, 1)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (22, 2, 10, 0.77)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (23, 2, 11, 0.27)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (24, 2, 12, 0)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (25, 3, 1, 2)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (26, 3, 2, 1.67)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (27, 3, 3, 1.33)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (28, 3, 4, 0.2)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (29, 3, 5, 0)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (30, 3, 6, 0)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (31, 3, 7, 0)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (32, 3, 8, 0)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (33, 3, 9, 1)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (34, 3, 10, 0.6)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (35, 3, 11, 0.33)
GO
INSERT INTO Fortbewegung_Modifikation (ID, Fortbewegung, Wegtyp, Multiplikator) VALUES (36, 3, 12, 0)
GO

SET IDENTITY_INSERT Fortbewegung_Modifikation OFF
GO