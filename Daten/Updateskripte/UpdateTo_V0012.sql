-- Einstellungen: SelectedTab, ExpandedSections, Standort
INSERT INTO Einstellungen (Name) VALUES ('SelectedTab');
INSERT INTO Einstellungen (Name, WertString) VALUES ('KalenderExpandedSections', '111111');
INSERT INTO Einstellungen (Name, WertString) VALUES ('UmrechnerExpandedSections', '110011');
INSERT INTO Einstellungen (Name, WertText) VALUES ('Standort', 'Gareth#3.735098459067687#29.79180235685203');

-- Ortsmarken Tabelle
CREATE TABLE Landmarke (NameID int IDENTITY (1, 1) NOT NULL PRIMARY KEY, Name nvarchar(350), Art nvarchar(150), Longitude nvarchar(150), Latitude nvarchar(150), KmlLink nvarchar(350));