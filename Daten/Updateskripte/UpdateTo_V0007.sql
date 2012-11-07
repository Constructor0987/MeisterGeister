-- Erweiterung: Held-Tabelle
ALTER TABLE Held ADD BildLink nvarchar(500), Rasse nvarchar(500), Kultur nvarchar(500), Profession nvarchar(500), Notizen ntext;

-- Einstellungen: Tabs
ALTER TABLE Einstellungen ADD WertText ntext;
INSERT INTO Einstellungen (Name, WertText) VALUES ('StartTabs', 'Proben#Kampf#Helden#Notizen#Kalender#NSCs#Umrechner#Würfel');