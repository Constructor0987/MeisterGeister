-- Repräsensationen umbenennen
UPDATE Held_Zauber SET Repräsentation = 'Mag' WHERE Repräsentation = 'Gildenmagisch';
UPDATE Held_Zauber SET Repräsentation = 'Elf' WHERE Repräsentation = 'Elfisch';
UPDATE Held_Zauber SET Repräsentation = 'Dru' WHERE Repräsentation = 'Druidisch';
UPDATE Held_Zauber SET Repräsentation = 'Hex' WHERE Repräsentation = 'Druidisch';
UPDATE Held_Zauber SET Repräsentation = 'Geo' WHERE Repräsentation = 'Geodisch';
UPDATE Held_Zauber SET Repräsentation = 'Sch' WHERE Repräsentation = 'Schelmisch';
UPDATE Held_Zauber SET Repräsentation = 'Srl' WHERE Repräsentation = 'Scharlatanisch';
UPDATE Held_Zauber SET Repräsentation = 'Bor' WHERE Repräsentation = 'Borbaradianisch';
UPDATE Held_Zauber SET Repräsentation = 'Ach' WHERE Repräsentation = 'Achaz-Kristallomantisch';
UPDATE Held_Zauber SET Repräsentation = 'Dil' WHERE Repräsentation = 'Magiedilletant';

-- Sonderfertigkeit-Tabelle: Setting
ALTER TABLE Sonderfertigkeit ADD Setting nvarchar(100);

-- Zauberzeichen-Namen korrigieren
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Bann- und Schutzkreis gegen Chimären' WHERE Name = 'Zauberzeichen: Bannkreis gegen Chimären';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Schutzkreis gegen Traumgänger' WHERE Name = 'Zauberzeichen: Bannkreis gegen Traumgänger';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Kraftquellenspeisung' WHERE Name = 'Zauberzeichen: Kraftquellenspeisung';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Magiewiderstand' WHERE Name = 'Zauberzeichen: Magiewiderstand';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Potenzierung' WHERE Name = 'Zauberzeichen: Potenzierung';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Tarnung' WHERE Name = 'Zauberzeichen: Tarnung';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Verkleinerung'  WHERE Name = 'Zauberzeichen: Verkleinerung';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Zielbeschränkung'  WHERE Name = 'Zauberzeichen: Zielbeschränkung';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zeichen des versperrten Blicks'  WHERE Name = 'Zauberzeichen: Zeichen des versperrten Blick';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Satinavs Siegel'  WHERE Name = 'Zauberzeichen: Satinavs Siegel';
UPDATE Sonderfertigkeit SET Name = 'Zauberzeichen: Zusatzzeichen Schutzsiegel'  WHERE Name = 'Zauberzeichen: Schutzsiegel';

-- DDZ Sonderfertigkeiten
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Alhanien)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Bosparanisches Reich)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Bosparanische Nordprovinzen)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Bosparanische Südprovinzen)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Cyclopea)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Diamantenes Sultanat)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Emirat Al''Mada)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Haranija)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Hjaldinger)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Kemi)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Koschzwerge)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Krakonier)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Serrach)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Sylla)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Wudu)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kulturkunde (Wüstenstämme)', 0, 'Allgemein', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Repräsentation (Alhanisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Repräsentation (Druidisch-Geodisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Repräsentation (Grolmisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Repräsentation (Güldenländisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Repräsentation (Kophtanisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Repräsentation (Mudramulisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Repräsentation (Satuarisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Ritualkenntnis (Güldenländisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Ritualkenntnis (Alhanisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Ritualkenntnis (Kophtanisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Ritualkenntnis (Mudramulisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Ritualkenntnis (Satuarisch)', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Ritualkenntnis (Tapasuul)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Spätweihe Dunkle Zeiten', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Waffenlose Kampftechnik (Gossenstil)', 0, 'Kampf', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Waffenlose Kampftechnik (Legionärsstil)', 0, 'Kampf', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Waffenlose Kampftechnik (Cyclopeisches Ringen)', 0, 'Kampf', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Waffenlose Kampftechnik (Echsenzwinger)', 0, 'Kampf', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Beseelung rufen (I)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Essenz kanalisieren (I)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Macht binden (I)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliches Prinzip stärken (I)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Schutz erflehen (I)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Willen erzwingen (I)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Beseelung rufen (II)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Essenz kanalisieren (II)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Macht binden (II)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliches Prinzip stärken (II)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Schutz erflehen (II)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Willen erzwingen (II)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Beseelung rufen (III)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Essenz kanalisieren (III)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Macht binden (III)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliches Prinzip stärken (III)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Schutz erflehen (III)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Willen erzwingen (III)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Beseelung rufen (IV)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Essenz kanalisieren (IV)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Macht binden (IV)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliches Prinzip stärken (IV)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Schutz erflehen (IV)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Willen erzwingen (IV)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Beseelung rufen (V)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Essenz kanalisieren (V)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliche Macht binden (V)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttliches Prinzip stärken (V)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Schutz erflehen (V)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Göttlichen Willen erzwingen (V)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Grolmenbund', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Karmale Einstimmung', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Karmale Kraftquelle', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Seele der Gemeinschaft', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Liturgiekenntnis (Dunkle Zeiten)', 0, 'Klerikal', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Stabzauber: Astralschild', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Stabzauber: Langer Arm', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Stabzauber: Schutz gegen Untote', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kugelzauber: Geschenk des Ssad''Navv', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kugelzauber: Siegel der sechafachen Ehre', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kugelzauber: Untrügliche Blick des Falken', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kugelzauber: Zauberspeicher', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Schalenzauber: Satinavs Bannung', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Schlangenszepter: Bindung', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Schlangenszepter: Ruf der fliegenden Schlange', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Bindung', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Fliegenleib', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Golemdiener', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Herrscher der Djinnim', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Herrscher der Ifriitim', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Kraft der Kophtanim', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Schutz der Ahnen', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Stimme der Macht', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Vermächtnis der Kophtanim', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Szepter: Waffe des Geistes', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Tapasuul: Blut für Visar', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Tapasuul: Ein Herz für Visar', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Tapasuul: Gestalt des Tapam', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Tapasuul: Kerker des Satuul', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Tapasuul: Kraft des Tapam', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Astrales Zeichen', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Aura des Friedens', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Blut der Sippe', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Gebet der Fürsorge', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Hilfe des Pendels', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Licht der Hoffnung', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Pendel der Hellsicht', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Seelengespür', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Stein der Weisen', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Traumgespinste', 0, 'Magisch', 'Dunkle Zeiten');
INSERT INTO Sonderfertigkeit (Name, HatWert, Typ, Setting) VALUES ('Kristallpendel: Weihe des Pendels', 0, 'Magisch', 'Dunkle Zeiten');

-- DDZ Zauber
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Bienenschwarm', 'IN', 'CH', 'KO', 'E', 'Alh 3', 'Form', 'Ordnung ins Chaos 67');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Entfesselung des Getiers', 'KL', 'CH', 'KO', 'E', 'Ach 3, Kop 3, Sat 2', 'Form', 'Ordnung ins Chaos 67');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Gebieter der Tiefe', 'MU', 'CH', 'KO', 'D', 'Kop 3, Ach 2', 'Form', 'Ordnung ins Chaos 68');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Geschoss der Niederhöllen', 'CH', 'FF', 'KK', 'C', 'Kop 3', 'Dämonisch, Schaden', 'Ordnung ins Chaos 68');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Grolmenseele', 'KL', 'CH', 'FF', 'F', 'Gro 4/2', 'Beschwörung, Geisterwesen, Telekinese', 'Ordnung ins Chaos 69');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Hauch der tiefen Tochter', 'KL', 'CH', 'KK', 'C', 'Kop 2', 'Elementar (Wasser), Einfluss', 'Ordnung ins Chaos 69');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Hexagramma Dschinnenbann', 'MU', 'MU', 'IN', 'C', 'Mud 5, Kop 2', 'Antimagie, Beschwörung, Elementar', 'Ordnung ins Chaos 69');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Hornissenruf', 'MU', 'CH', 'CH', 'C', '', 'Einfluss, Herbeirufung, Limbus', 'Ordnung ins Chaos 69');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Igniflumen Flammenspur', 'MU', 'KL', 'KO', 'D', 'Gül 4', 'Elementar (Feuer), Schaden', 'Ordnung ins Chaos 69');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Igniplano Flächenbrand', 'MU', 'IN', 'KO', 'F', 'Gül 3', 'Elementar (Feuer), Schaden, Umwelt', 'Ordnung ins Chaos 70');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Igniqueris Feuerkragen', 'KL', 'CH', 'KK', 'C', 'Gül 3', 'Elementar (Feuer), Einfluss', 'Ordnung ins Chaos 70');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Leib aus tausend Fliegen', 'IN', 'CH', 'KO', 'E', 'Kop 4', 'Form', 'Ordnung ins Chaos 70');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Schlangenruf', 'MU', 'CH', 'CH', 'C', 'Alh 5', 'Einfluss, Herbeirufung, Limbus', 'Ordnung ins Chaos 71');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Seelenfeuer Lichterloh', 'MU', 'MU', 'KO', 'D', 'Gül 2', 'Elementar (Feuer), Form', 'Ordnung ins Chaos 71');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Sphaerovisio Schreckensbild', 'MU', 'KL', 'FF', 'D', 'Kop 4, Gül 3', 'Dämonisch, Limbus, Verständigung', 'Ordnung ins Chaos 71');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Spinnennetz', 'IN', 'FF', 'KO', 'B', 'Sat 5', 'Eigenschaften', 'Ordnung ins Chaos 72');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Spinnenruf', 'MU', 'CH', 'CH', 'C', '', 'Einfluss, Herbeirufung, Limbus', 'Ordnung ins Chaos 72');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Tanz der Erlösung', 'MU', 'CH', 'CH', 'D', 'Mud 4', 'Antimagie, Einfluss, Herrschaft', 'Ordnung ins Chaos 73');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Tierruf', 'MU', 'CH', 'CH', 'C', 'Sat 6', 'Einfluss, Herbeirufung, Limbus', 'Ordnung ins Chaos 73');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Unsichtbare Glut', 'KL', 'IN', 'FF', 'C', 'Mud 4', 'Objekt', 'Ordnung ins Chaos 73');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Valetudo Lebenskraft', 'IN', 'GE', 'KO', 'C', '', 'Heilung, Kraft', 'Ordnung ins Chaos 73');
INSERT INTO Zauber (Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Repräsentationen, Merkmale, Quelle) VALUES ('Weisheit der Steine', 'MU', 'IN', 'KO', 'D', '', 'Form, Elementar (Erz)', 'Ordnung ins Chaos 73');

-- Sonderfertigkeit Typ-Spalte vergrößern
ALTER TABLE Sonderfertigkeit ALTER COLUMN Typ nvarchar(100);

-- Ware-Tabelle
CREATE TABLE [Ware] (
  [WareId] int NOT NULL  IDENTITY (1,1)
, [Name] nvarchar(500) NULL
, [Preis] nvarchar(100) NULL DEFAULT 0
, [Gewicht] float NULL
, [ME] nvarchar(100) NULL
, [Kategorie] nvarchar(500) NULL
, [Tags] nvarchar(1000) NULL
, [Bemerkung] ntext NULL
, [Literatur] nvarchar(500) NULL
);
ALTER TABLE [Ware] ADD CONSTRAINT [PK_Ware] PRIMARY KEY ([WareId]);