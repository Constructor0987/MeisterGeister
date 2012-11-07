-- Zauber Dämonenbann
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Agrimoth)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Amazeroth)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Asfaloth)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Belhalhar)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Belkelel)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Belzhorash)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Blakharaz)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Charyptoroth)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Lolgramoth)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Nagrach)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Tasfarelel)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale],[Quelle]) VALUES (N'Dämonenbann (Thargunitoth)',N'MU',N'CH',N'KO',N'C',N'Mag 2',N'Antimagie, Dämonisch',null);

-- Sonderfertigkeit Wert-Spalte vergrößern
ALTER TABLE Held_Sonderfertigkeit ALTER COLUMN Wert nvarchar(1200);