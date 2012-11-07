-- Notizen
CREATE TABLE Notizen (NotizID int IDENTITY (1, 1) NOT NULL PRIMARY KEY, Name nvarchar(100), Text ntext, NotToDelete bit DEFAULT 0);
INSERT INTO Notizen (Name, NotToDelete) VALUES ('Allgemein', 1);
INSERT INTO Notizen (Name, NotToDelete) VALUES ('Erfahrungen', 1);
INSERT INTO Notizen (Name) VALUES ('Zitate');
INSERT INTO Einstellungen (Name, WertInt) VALUES ('NotizFontSize', 12);