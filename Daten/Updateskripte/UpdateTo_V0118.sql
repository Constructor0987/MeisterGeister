-- Held_Zauber um Hauszauber ergänzen
ALTER TABLE Held_Zauber add Hauszauber bit DEFAULT 0
GO

--Panzerstecher hat TP/KK 13/3 und nicht 12/3
UPDATE Waffe SET TPKKSchwelle = 13 WHERE WaffeGUID = '00000000-0000-0000-0001-000000000094'
GO
UPDATE Ausrüstung SET Literatur = 'AA 28 / AA Errata 2' WHERE AusrüstungGUID = '00000000-0000-0000-0001-000000000094'
GO
-- Pechfackel war Faktor 10 zu teuer
UPDATE Handelsgut_Setting SET Preis=0.5 WHERE HandelsgutGUID='00000000-0000-0000-002a-000000000358'
GO

-- 'Tenebaro Schattentanz' fehlt bei Zaubern
INSERT INTO Zauber (ZauberGUID, Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Merkmale, Literatur) SELECT '00000000-0000-0000-00ca-000000000387' as ZauberGUID, 'Tenebaro Schattentanz' as Name, Eigenschaft1, Eigenschaft2, Eigenschaft3, Komplex, Merkmale, Literatur + ' / SoG 163' as Literatur from Zauber WHERE ZauberGUID='00000000-0000-0000-00ca-000000000083'
GO

--Iribaarslilie doppelt, Grauer Lotus -> Grauer Lotos 
DELETE FROM Pflanze WHERE PflanzeGUID='00000000-0000-0000-00ff-000000000202'
GO
UPDATE Pflanze SET Name='Grauer Lotos' WHERE PflanzeGUID='00000000-0000-0000-00ff-000000000029'
GO
