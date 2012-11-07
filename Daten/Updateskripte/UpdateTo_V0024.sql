-- Optional-Regeln, Dunkle Zeiten Setting
INSERT INTO Regeln (Name, Typ, Beschreibung) VALUES ('TPKK', 'Optional', 'Kampf: Trefferpunkte und Körperkraft (TP/KK) (WdS 81f)');
INSERT INTO Regeln (Name, Typ, Beschreibung) VALUES ('NiedrigeLE', 'Optional', 'Kampf: Auswirkungen niedriger LE (WdS 57)');
INSERT INTO Regeln (Name, Typ, Beschreibung) VALUES ('NiedrigeAU', 'Optional', 'Kampf: Auswirkungen niedriger AU (WdS 83)');
INSERT INTO Regeln (Name, Typ, Beschreibung) VALUES ('DunkleZeiten', 'Setting', 'Dunkle Zeiten berücksichtigen');

-- Menü-Link Tabelle
CREATE TABLE [MenuLink] (
  [MenuPunkt] nvarchar(300) NOT NULL
, [ProgrammPfad] nvarchar(500) NOT NULL
, [Name] nvarchar(300) NULL
, [Bild] nvarchar(100) NULL
);
ALTER TABLE [MenuLink] ADD CONSTRAINT [PK_MenuLink] PRIMARY KEY ([MenuPunkt],[ProgrammPfad]);

-- Talente-Tabelle: Setting
ALTER TABLE Talent ADD Setting nvarchar(100);

-- DDZ Talente
INSERT INTO Talent (Talentname, TalentgruppeID, Eigenschaft1, Eigenschaft2, Eigenschaft3, Talenttyp, Quelle, Steigerung, Setting) VALUES ('Sprachen Kennen (Wudu)', 7, 'KL', 'IN', 'CH', 'Spezial', 'Ordnung ins Chaos 45', 'A', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Eigenschaft1, Eigenschaft2, Eigenschaft3, Talenttyp, Quelle, Steigerung, Setting) VALUES ('Lesen/Schreiben (Wudu)', 7, 'KL', 'KL', 'FF', 'Spezial', 'Ordnung ins Chaos 45', 'A', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Eigenschaft1, Eigenschaft2, Eigenschaft3, Talenttyp, Quelle, WikiLink, Setting) VALUES ('Liturgiekenntnis (Dunkle Zeiten)', 11, 'MU', 'IN', 'CH', 'Spezial', 'Ordnung ins Chaos', 'Liturgiekenntnis', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Talenttyp, Quelle, WikiLink, Setting) VALUES ('Ritualkenntnis (Güldenländisch)', 10, 'Spezial', 'Ordnung ins Chaos 52', 'Ritualkenntnis (Tradition)', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Talenttyp, Quelle, WikiLink, Setting) VALUES ('Ritualkenntnis (Alhanisch)', 10, 'Spezial', 'Ordnung ins Chaos 52', 'Ritualkenntnis (Tradition)', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Talenttyp, Quelle, WikiLink, Setting) VALUES ('Ritualkenntnis (Kophtanisch)', 10, 'Spezial', 'Ordnung ins Chaos 52', 'Ritualkenntnis (Tradition)', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Talenttyp, Quelle, WikiLink, Setting) VALUES ('Ritualkenntnis (Mudramulisch)', 10, 'Spezial', 'Ordnung ins Chaos 52', 'Ritualkenntnis (Tradition)', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Talenttyp, Quelle, WikiLink, Setting) VALUES ('Ritualkenntnis (Satuarisch)', 10, 'Spezial', 'Ordnung ins Chaos 52', 'Ritualkenntnis (Tradition)', 'Dunkle Zeiten');
INSERT INTO Talent (Talentname, TalentgruppeID, Talenttyp, Quelle, WikiLink, Setting) VALUES ('Ritualkenntnis (Tapasuul)', 10, 'Spezial', 'Ordnung ins Chaos 52', 'Ritualkenntnis (Tradition)', 'Dunkle Zeiten');

-- VorNachteil-Tabelle: Setting
ALTER TABLE VorNachteil ADD Setting nvarchar(100);

-- DDZ VorNachteile
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Adlig (Hoher Amtsadel)', 1, 0, 'Vorteile', 'Dunkle Zeiten');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Begabung für Göttliche Kraft (Essenz kanalisieren)', 1, 0, 'Vorteile', 'Dunkle Zeiten');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Begabung für Göttliche Kraft (Prinzip stärken)', 1, 0, 'Vorteile', 'Dunkle Zeiten');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Begabung für Göttliche Kraft (Willen erzwingen)', 1, 0, 'Vorteile', 'Dunkle Zeiten');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Begabung für Göttliche Kraft (Macht binden)', 1, 0, 'Vorteile', 'Dunkle Zeiten');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Begabung für Göttliche Kraft (Beseelung rufen)', 1, 0, 'Vorteile', 'Dunkle Zeiten');
INSERT INTO VorNachteil (Name, Vorteil, HatWert, Typ, Setting) VALUES ('Begabung für Göttliche Kraft (Schutz erflehen)', 1, 0, 'Vorteile', 'Dunkle Zeiten');
INSERT INTO [VorNachteil] ([Name],[Vorteil],[Nachteil],[HatWert],[WertTyp],[Typ],[Setting]) VALUES (N'Comes',1,0,0,null,N'Vorteile',N'Dunkle Zeiten');
INSERT INTO [VorNachteil] ([Name],[Vorteil],[Nachteil],[HatWert],[WertTyp],[Typ],[Setting]) VALUES (N'Erstgeborener Comes',1,0,0,null,N'Vorteile',N'Dunkle Zeiten');
INSERT INTO [VorNachteil] ([Name],[Vorteil],[Nachteil],[HatWert],[WertTyp],[Typ],[Setting]) VALUES (N'Ererbtes Szepter',1,0,1,N'int',N'Vorteile',N'Dunkle Zeiten');
INSERT INTO [VorNachteil] ([Name],[Vorteil],[Nachteil],[HatWert],[WertTyp],[Typ],[Setting]) VALUES (N'Hohe Karmaenergie',1,0,1,N'int',N'Vorteile',N'Dunkle Zeiten');
INSERT INTO [VorNachteil] ([Name],[Vorteil],[Nachteil],[HatWert],[WertTyp],[Typ],[Setting]) VALUES (N'Sacerdos',1,0,1,N'int',N'Vorteile',N'Dunkle Zeiten');