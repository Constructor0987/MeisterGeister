-- eventuell auftretender Daten-Fehler in Ausrüstung-Tabelle korrigieren
DELETE FROM Ausrüstung WHERE AusrüstungGUID = '00000000-0000-0000-0000-000000000000';

-- Sonderfertigkeiten für Streitwagen eingefügt
INSERT INTO [Sonderfertigkeit] ([Name],[HatWert],[Typ],[Setting],[Vorraussetzungen],[Literatur]) VALUES ('Kriegsreiterei (Streitwagen)',0,'Kampf',NULL,NULL,NULL);
INSERT INTO [Sonderfertigkeit] ([Name],[HatWert],[Typ],[Setting],[Vorraussetzungen],[Literatur]) VALUES ('Reiterkampf (Streitwagen)',0,'Kampf',NULL,NULL,NULL);

-- Göttergeschenke
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Greif', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Schwert', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Delphin', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Gans', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Rabe', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Schlange', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Eisbär', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Eidechse', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Fuchs', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Storch', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Hammer/Amboss', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Stute', 1, 0, 'Vorteile', 'Aventurien');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Göttergeschenk: Sternenleere', 1, 0, 'Vorteile', 'Aventurien');