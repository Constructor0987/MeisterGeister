-- Sprachen und Schriften überarbeitet
--Angramm -> Angram (Schrift)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000101', '00000000-0000-0000-007a-000000000102') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000102';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000101' WHERE TalentGUID='00000000-0000-0000-007a-000000000102';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000102';

--Angramm -> Angram
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000261', '00000000-0000-0000-007a-000000000262') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000262';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000261' WHERE TalentGUID='00000000-0000-0000-007a-000000000262';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000262';

-- Lesen/Schreiben (Alt-Vesayitisch) -> Lesen/Schreiben (Vesayitische Wort- und Silbenzeichen)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000151', '00000000-0000-0000-007a-000000000096') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000096';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000151' WHERE TalentGUID='00000000-0000-0000-007a-000000000096';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000096';

--Aureliani -> Alt-Imperial
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000251', '00000000-0000-0000-007a-000000000268') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000268';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000251' WHERE TalentGUID='00000000-0000-0000-007a-000000000268';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000268';

--Lesen/Schreiben (Alt-Imperiale Buchstaben) -> Lesen/Schreiben (Imperiale Zeichen/Altgüldenländisch/Alt-Imperiale Buchstaben)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000124', '00000000-0000-0000-007a-000000000092') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000092';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000124' WHERE TalentGUID='00000000-0000-0000-007a-000000000092';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000092';

--Lesen/Schreiben (Hjaldingsche Runenzeichen) -> Lesen/Schreiben (Hjaldingsche Runen)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000121', '00000000-0000-0000-007a-000000000122') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000122';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000121' WHERE TalentGUID='00000000-0000-0000-007a-000000000122';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000122';

-- Lesen/Schreiben (Vesayo) -> Lesen/Schreiben (Vesayo-Silbenzeichen)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000152', '00000000-0000-0000-007a-000000000153') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000153';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000152' WHERE TalentGUID='00000000-0000-0000-007a-000000000153';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000153';

-- Sprachen Kennen (Ur-Tulamydia) -> Sprachen Kennen (Ur-Tulamidya)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000343', '00000000-0000-0000-007a-000000000344') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000344';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000343' WHERE TalentGUID='00000000-0000-0000-007a-000000000344';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000344';

--Sprachen Kennen (Ashdaria) -> Sprachen Kennen (Asdharia)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000264', '00000000-0000-0000-007a-000000000266') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000266';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000264' WHERE TalentGUID='00000000-0000-0000-007a-000000000266';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000266';

-- Lesen/Schreiben (Gemein-Vesayitisch) -> Lesen/Schreiben (Vesayo-Silbenzeichen)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000152', '00000000-0000-0000-007a-000000000116') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000116';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000152' WHERE TalentGUID='00000000-0000-0000-007a-000000000116';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000116';

-- Lesen/Schreiben (Ur-Vesayitisch) -> Lesen/Schreiben (Vesayitische Wort- und Silbenzeichen)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000151', '00000000-0000-0000-007a-000000000148') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000148';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000151' WHERE TalentGUID='00000000-0000-0000-007a-000000000148';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000148';

-- Lesen/Schreiben (Ur-Tulamydia) -> Lesen/Schreiben (Ur-Tulamidya)
DELETE FROM Held_Talent WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Talent WHERE TalentGUID in ('00000000-0000-0000-007a-000000000146', '00000000-0000-0000-007a-000000000147') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND TalentGUID='00000000-0000-0000-007a-000000000147';
UPDATE Held_Talent SET TalentGUID='00000000-0000-0000-007a-000000000146' WHERE TalentGUID='00000000-0000-0000-007a-000000000147';
DELETE FROM Talent WHERE TalentGUID='00000000-0000-0000-007a-000000000147';

--gossenstil -> bornländisch
DELETE FROM Held_Sonderfertigkeit WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Sonderfertigkeit WHERE SonderfertigkeitGUID in ('00000000-0000-0000-005f-000000000066', '00000000-0000-0000-005f-000000000924') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND SonderfertigkeitGUID='00000000-0000-0000-005f-000000000924';
UPDATE Held_Sonderfertigkeit SET SonderfertigkeitGUID='00000000-0000-0000-005f-000000000066' WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000924';
DELETE FROM Sonderfertigkeit WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000924';

--legionärsstil -> mercanario
DELETE FROM Held_Sonderfertigkeit WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Sonderfertigkeit WHERE SonderfertigkeitGUID in ('00000000-0000-0000-005f-000000000070', '00000000-0000-0000-005f-000000000925') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND SonderfertigkeitGUID='00000000-0000-0000-005f-000000000925';
UPDATE Held_Sonderfertigkeit SET SonderfertigkeitGUID='00000000-0000-0000-005f-000000000070' WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000925';
DELETE FROM Sonderfertigkeit WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000925';

--cyclopeisches ringen -> unauer schule
DELETE FROM Held_Sonderfertigkeit WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Sonderfertigkeit WHERE SonderfertigkeitGUID in ('00000000-0000-0000-005f-000000000071', '00000000-0000-0000-005f-000000000926') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND SonderfertigkeitGUID='00000000-0000-0000-005f-000000000926';
UPDATE Held_Sonderfertigkeit SET SonderfertigkeitGUID='00000000-0000-0000-005f-000000000071' WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000926';
DELETE FROM Sonderfertigkeit WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000926';

--echsenzwinger -> unauer schule
DELETE FROM Held_Sonderfertigkeit WHERE HeldGUID in 
(SELECT HeldGUID FROM Held_Sonderfertigkeit WHERE SonderfertigkeitGUID in ('00000000-0000-0000-005f-000000000071', '00000000-0000-0000-005f-000000000927') GROUP BY HeldGUID HAVING COUNT(*) > 1)
AND SonderfertigkeitGUID='00000000-0000-0000-005f-000000000927';
UPDATE Held_Sonderfertigkeit SET SonderfertigkeitGUID='00000000-0000-0000-005f-000000000071' WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000927';
DELETE FROM Sonderfertigkeit WHERE SonderfertigkeitGUID='00000000-0000-0000-005f-000000000927';



/* Kultur */

UPDATE [Kultur] SET [Literatur] = N'WdH 55 / OiC 18' ,[Setting] = N'Aventurien, Dunkle Zeiten' WHERE [KulturGUID]='00000000-0000-0000-0000-000000000080';
UPDATE [Kultur] SET [Literatur] = N'WdH 59 / OiC 18' ,[Setting] = N'Aventurien, Dunkle Zeiten' WHERE [KulturGUID]='00000000-0000-0000-0000-000000000084';
UPDATE [Kultur] SET [Literatur] = N'WdH 60 / OiC 18' WHERE [KulturGUID]='00000000-0000-0000-0000-000000000085';
UPDATE [Kultur] SET [Name] = N'Tulamidische Kulturen' ,[Variante] = N'Tulamidische Kulturen' WHERE [KulturGUID]='00000000-0000-0000-0000-000000000280';

/* Sonderfertigkeit */

INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001302' ,N'Kulturkunde (Amaunir)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001303' ,N'Kulturkunde (Ashariel)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001304' ,N'Kulturkunde (Ban''Bargui)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001305' ,N'Kulturkunde (Bansumiter)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001306' ,N'Kulturkunde (Baramunen)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001308' ,N'Kulturkunde (Imperium)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001309' ,N'Kulturkunde (Kentori)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001310' ,N'Kulturkunde (Kynokephalen)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001311' ,N'Kulturkunde (Leonir)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001312' ,N'Kulturkunde (Loualil)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001313' ,N'Kulturkunde (Lutraa)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001314' ,N'Kulturkunde (Lyncil)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001315' ,N'Kulturkunde (Minotauren)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001316' ,N'Kulturkunde (Neristu)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001317' ,N'Kulturkunde (Padir)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001318' ,N'Kulturkunde (Ravesaran)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001319' ,N'Kulturkunde (Risso)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001320' ,N'Kulturkunde (Satyare)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001321' ,N'Kulturkunde (Shingwa)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001322' ,N'Kulturkunde (Shinoqi)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001323' ,N'Kulturkunde (Tighir)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001324' ,N'Kulturkunde (Wolfalben' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001325' ,N'Kulturkunde (Zwerge)' ,0 ,N'Allgemein' ,N'WnM 142' ,N'KL 10, IN 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001326' ,N'Sachkundig' ,0 ,N'Allgemein' ,N'WnM 144' ,N'passendes Talent 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001327' ,N'Siminiagefälliges Wissen' ,0 ,N'Allgemein' ,N'WnM 144' ,N'KL 12, IN 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001328' ,N'Wundheiler' ,0 ,N'Allgemein' ,N'WnM 144' ,N'Anatomie 10, Heilkunde Wunden 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001329' ,N'Balancierter Sprung' ,0 ,N'Kampf' ,N'WnM 144' ,N'GE 13, Vorteil Balanceschwanz, SFKampfreflexe');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001330' ,N'Mehrfachangriff' ,0 ,N'Kampf' ,N'WnM 148' ,N'SF Mehrhändiger Kampf');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001331' ,N'Mehrhändiger Kampf I' ,0 ,N'Kampf' ,N'WnM 148' ,N'GE 15, SF Linkhand');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001332' ,N'Mehrhändiger Kampf II' ,0 ,N'Kampf' ,N'WnM 148' ,N'Vorteil: Zusätzliches Armpaar');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001333' ,N'Mehrhändiger Kampf III' ,0 ,N'Kampf' ,N'WnM 148' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001334' ,N'Nertar''Cha I' ,0 ,N'Kampf' ,N'WnM 148' ,N'IN 12, KO 12 oder KK 12/IN 15');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001335' ,N'Nertar''Cha II' ,0 ,N'Kampf' ,N'WnM 148' ,N'Nertar''Cha I, Vorteil Nertaryumi');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001336' ,N'Scharfschütze (Bela)' ,0 ,N'Kampf' ,N'WnM 149' ,N'Bela 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001337' ,N'Scharfschütze (Feuerwaffen)' ,0 ,N'Kampf' ,N'WnM 149' ,N'Feuerwaffen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001338' ,N'Waffenlose Kampftechnik (Amaunischer Stil)' ,0 ,N'Kampf' ,N'WnM 151, 185' ,N'Ringen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001339' ,N'Waffenlose Kampftechnik (Erasumischer Klosterstil)' ,0 ,N'Kampf' ,N'WnM 151, 185' ,N'Raufen 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001340' ,N'Waffenlose Kampftechnik (Leonischer Stil)' ,0 ,N'Kampf' ,N'WnM 151, 185' ,N'KK 13, Raufen 7, Ringen 7 ');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001341' ,N'Waffenlose Kampftechnik (Nertaryu)' ,0 ,N'Kampf' ,N'WnM 151, 186' ,N'IN 12, GE 12, KO12, Raufen 10 | Ringen 10, Zusätzliches Armpaar');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001342' ,N'Waffenlose Kampftechnik (Thalassischer Stil)' ,0 ,N'Kampf' ,N'WnM 151, 186' ,N'Ringen 7, SF Unterwasserkampf');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001343' ,N'Waffenloses Manöver (Anspringen)' ,0 ,N'Kampf' ,N'WnM 151' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001344' ,N'Waffenloses Manöver (Umstoßen)' ,0 ,N'Kampf' ,N'WnM 151' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001345' ,N'Archonbeschwörung' ,0 ,N'Magisch' ,N'WnM 152' ,N'Passender Beschwörungszauber 12, passende SF Geniusbeschwörung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001346' ,N'Auswerfen' ,0 ,N'Magisch' ,N'WnM 153' ,N'MU 15, lann nur bei internen Wesenbeschwörungen durchgeführt werden, die mit mindestens ZfW 7 beherrscht werden');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001347' ,N'Automatische Mitverwandlung' ,0 ,N'Magisch' ,N'WnM 153' ,N'Leiteigenschaft 16, (SF Instruktion: Verwandlung, SF Instruktion: Transformation) | V Wolfskind');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001348' ,N'Bannung' ,0 ,N'Magisch' ,N'WnM 153' ,N'MU 13, Magiekunde 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001349' ,N'Beschwörerzirkel' ,0 ,N'Magisch' ,N'WnM 153' ,N'MU 12, CH 13');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001350' ,N'Beschwörungsroutine' ,0 ,N'Magisch' ,N'WnM 153' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001351' ,N'Blutgeist' ,0 ,N'Magisch' ,N'WnM 153' ,N'SF Blutmagie, eine Tiergeisterbeschwörung 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001352' ,N'Engramm' ,0 ,N'Magisch' ,N'WnM 154' ,N'MU 15, KL 15, CH 15, Ritualkenntnis (Optimatik) 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001353' ,N'Fernzauberei II' ,0 ,N'Magisch' ,N'WnM 154' ,N'IN 12, SF Spontanzauberei III (Reichweite) /IN 15, SF Fernzauberei');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001354' ,N'Formelentwicklung I' ,0 ,N'Magisch' ,N'WnM 154' ,N'Leiteigenschaft 13, Magiekunde 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001355' ,N'Formelentwicklung II' ,0 ,N'Magisch' ,N'WnM 154' ,N'Leiteigenschaft 13, Magiekunde 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001356' ,N'Formelentwicklung III' ,0 ,N'Magisch' ,N'WnM 154' ,N'Leiteigenschaft 15, Magiekunde 10, Rechnen oder Malen/ Zeichnen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001357' ,N'Formelentwicklung IV' ,0 ,N'Magisch' ,N'WnM 154' ,N'Leiteigenschaft 15, Magiekunde 10, Rechnen oder Malen/ Zeichnen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001358' ,N'Formelentwicklung V' ,0 ,N'Magisch' ,N'WnM 154' ,N'Leiteigenschaft 15, Magiekunde 10, Rechnen oder Malen/ Zeichnen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001359' ,N'Formelentwicklung VI' ,0 ,N'Magisch' ,N'WnM 154' ,N'Leiteigenschaft 17, Magiekunde 15, Rechnen oder Malen/ Zeichnen 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001360' ,N'Formelentwicklung VII' ,0 ,N'Magisch' ,N'WnM 154' ,N'Leiteigenschaft 17, Magiekunde 15, Rechnen oder Malen/ Zeichnen 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001361' ,N'Fremdopferung' ,0 ,N'Magisch' ,N'WnM 155' ,N'SF Blutmagie, SF Verbotene Pforten, Heilunde Wunden 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001362' ,N'Geniusbeschwörung' ,0 ,N'Magisch' ,N'WnM 155' ,N'Passende SF Spontanzauber (Invokation oder Inspiration)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001363' ,N'Heilmagie' ,0 ,N'Magisch' ,N'WnM 155' ,N'Leiteigenschaft 15, Heilkunde Wunden 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001364' ,N'Höhere Bindung (Elementar)' ,0 ,N'Magisch' ,N'WnM 155' ,N'Leiteigenschaft 15, Wesensbindung 11, Ritualkenntnis 11');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001365' ,N'Höhere Bindung (Stellar)' ,0 ,N'Magisch' ,N'WnM 155' ,N'Leiteigenschaft 15, Wesensbindung 11, Ritualkenntnis 11');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001366' ,N'Höhere Bindung (Dämon)' ,0 ,N'Magisch' ,N'WnM 155' ,N'Leiteigenschaft 15, Wesensbindung 11, Ritualkenntnis 11');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001367' ,N'Höhere Bindung (Totenwesen)' ,0 ,N'Magisch' ,N'WnM 155' ,N'Leiteigenschaft 15, Wesensbindung 11, Ritualkenntnis 11');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001368' ,N'Höhere Bindung (Naturwesen)' ,0 ,N'Magisch' ,N'WnM 155' ,N'Leiteigenschaft 15, Wesensbindung 11, Ritualkenntnis 11');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001369' ,N'Illusionszauberer' ,0 ,N'Magisch' ,N'WnM 155' ,N'CH 16, IN 14');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001370' ,N'Instruktionsspezialisierung' ,0 ,N'Magisch' ,N'WnM 156' ,N'Zugehörige Instruktions-SF, eine passende Zauberspezialisierung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001371' ,N'Kombinationszauberei' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 15, ein Essenzbeschwörungszauber 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001372' ,N'Kostendeckung' ,0 ,N'Magisch' ,N'WnM 156' ,N'MU 14, CH 16, IN 14');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001373' ,N'Magischer Experte (Antimagier)' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 16, passende Quellenkenntnis, Essenzbeschwörung der Quelle 14, Zauberspezialisierung auf mindestens 2 der Dienste, alle 3 zugehörigen Instruktions-SF');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001374' ,N'Magischer Experte (Baumeister)' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 16, passende Quellenkenntnis, Essenzbeschwörung der Quelle 14, Zauberspezialisierung auf mindestens 2 der Dienste, alle 3 zugehörigen Instruktions-SF');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001375' ,N'Magischer Experte (Chaotiker)' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 16, passende Quellenkenntnis, Essenzbeschwörung der Quelle 14, Zauberspezialisierung auf mindestens 2 der Dienste, alle 3 zugehörigen Instruktions-SF');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001376' ,N'Magischer Experte (Heiler)' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 16, passende Quellenkenntnis, Essenzbeschwörung der Quelle 14, Zauberspezialisierung auf mindestens 2 der Dienste, alle 3 zugehörigen Instruktions-SF');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001377' ,N'Magischer Experte (Hellseher)' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 16, passende Quellenkenntnis, Essenzbeschwörung der Quelle 14, Zauberspezialisierung auf mindestens 2 der Dienste, alle 3 zugehörigen Instruktions-SF');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001378' ,N'Magischer Experte (Kampfzauberer)' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 16, passende Quellenkenntnis, Essenzbeschwörung der Quelle 14, Zauberspezialisierung auf mindestens 2 der Dienste, alle 3 zugehörigen Instruktions-SF');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001379' ,N'Magischer Experte (Telekinetiker)' ,0 ,N'Magisch' ,N'WnM 156' ,N'Leiteigenschaft 16, passende Quellenkenntnis, Essenzbeschwörung der Quelle 14, Zauberspezialisierung auf mindestens 2 der Dienste, alle 3 zugehörigen Instruktions-SF');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001380' ,N'Magische Verbindung' ,0 ,N'Magisch' ,N'WnM 156' ,N'V Tierempathie, Ritualkenntnis (Satudur) 0');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001381' ,N'Massenbeschwörung' ,1 ,N'Magisch' ,N'WnM 157' ,N'Eine Wesensbeschwörung 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001382' ,N'Permanentzauberer' ,0 ,N'Magisch' ,N'WnM 157' ,N'CH 15, SF Spontanzauberei (Wirkungsdauer)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001383' ,N'Schutzzeichen' ,1 ,N'Magisch' ,N'WnM 158' ,N'KL 12, IN 12, FF 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001384' ,N'Shindramatha-Ritual (Lebender Astralspeicher)' ,0 ,N'Magisch (Ritual)' ,N'WnM 158 / WnM 158' ,N'Ritualkenntnis 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001385' ,N'Späte Ausbildung I' ,0 ,N'Magisch' ,N'WnM 158' ,N'CH 15, MU 13, KL 13, IN 13, eine Repräsentation außer der natürlichen ');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001386' ,N'Späte Ausbildung II' ,0 ,N'Magisch' ,N'WnM 158' ,N'CH 16, MU 14, KL 14, IN 14');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001387' ,N'Triopta: Bindung' ,0 ,N'Magisch (Ritual)' ,N'WnM 158 / MyMa 121' ,N'Ritualkenntnis (Optimatik) 3');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001388' ,N'Traumgänger II' ,0 ,N'Magisch' ,N'WnM 159' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001389' ,N'Triopta: Astralspeicher I' ,0 ,N'Magisch (Ritual)' ,N'WnM 159 / MyMa 121' ,N'Ritualkenntnis (Optimatik) 11, SF Tritopta: Bindung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001390' ,N'Triopta: Astralspeicher II' ,0 ,N'Magisch (Ritual)' ,N'WnM 159 / MyMa 122' ,N'Ritualkenntnis (Optimatik) 12, SF Triopta: Zauberspeicher I');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001391' ,N'Triopta: Astralspeicher III' ,0 ,N'Magisch (Ritual)' ,N'WnM 159 / MyMa 122' ,N'Ritualkenntnis (Optimatik) 15, SF Triopta: Zauberspeicher II');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001392' ,N'Triopta: Zauberspeicher I' ,0 ,N'Magisch (Ritual)' ,N'WnM 159 / MyMa 121' ,N'Ritualkenntnis (Optimatik) 9, SF Triopta: Astralspeicher I');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001393' ,N'Triopta: Zauberspeicher II' ,0 ,N'Magisch (Ritual)' ,N'WnM 159 / MyMa 122' ,N'Ritualkenntnis (Optimatik) 12, SF Triopta: Astralspeicher II');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001394' ,N'Waffenlose Kampftechnik (Myrmidonenstil)' ,0 ,N'Kampf' ,N'WnM 151, 186' ,N'Raufen 7, Ringen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001395' ,N'Waffenlose Kampftechnik (Catzol)' ,0 ,N'Kampf' ,N'Rakshazar 24' ,N'GE 13, Raufen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001396' ,N'Waffenlose Kampftechnik (Hakra-Chutram)' ,0 ,N'Kampf' ,N'Rakshazar 24' ,N'Raufen 10, Ringen 5');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001397' ,N'Waffenlose Kampftechnik (Pajgarok)' ,0 ,N'Kampf' ,N'Rakshazar 25' ,N'Raufen 10, Ringen 5');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001398' ,N'Waffenlose Kampftechnik (Rhak-Ringen)' ,0 ,N'Kampf' ,N'Rakshazar 25' ,N'Ringen 7, Raufen 5');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001399' ,N'Waffenlose Kampftechnik (Sirista-Schlangenstil)' ,0 ,N'Kampf' ,N'Rakshazar 26' ,N'Ringen 10, Raufen 3');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001400' ,N'Waffenlose Kampftechnik (Srhj)' ,0 ,N'Kampf' ,N'Rakshazar 26' ,N'Raufen 9');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001401' ,N'Göttliche Anrufung I' ,0 ,N'Klerikal' ,N'OiC 44' ,N'V Sacredos | (SF Spähtweihe, Liturgiekenntnis 0)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001402' ,N'Spontanfetisch' ,0 ,N'Magisch' ,N'MyMa 129' ,N'Ritualkenntnis (animistische Tradition) 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001403' ,N'Göttliche Anrufung IV' ,0 ,N'Klerikal' ,N'OiC 44' ,N'V Sacredos | (SF Spähtweihe, Liturgiekenntnis 0)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001404' ,N'Göttliche Anrufung II' ,0 ,N'Klerikal' ,N'OiC 44' ,N'V Sacredos | (SF Spähtweihe, Liturgiekenntnis 0)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001405' ,N'Zwangskontrolle' ,0 ,N'Magisch' ,N'WnM 160' ,N'MU 15, CH 15');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001406' ,N'Göttliche Anrufung III' ,0 ,N'Klerikal' ,N'OiC 44' ,N'V Sacredos | (SF Spähtweihe, Liturgiekenntnis 0)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001407' ,N'Vielfalt der Geister' ,0 ,N'Magisch' ,N'WnM 160' ,N'IN 14, CH 14, Ritualkenntnis (animistische Tradition) 8');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001408' ,N'Stabzauber: Entwässerung' ,0 ,N'Magisch (Ritual)' ,N'WnM 158' ,N'Ritualkenntnis (Optimatik) 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001409' ,N'Großbeschwörung' ,0 ,N'Magisch' ,N'MyMa 152' ,N'ein Beschwörungszauber mit ZfW 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001410' ,N'Stabzauber: Halbes Maß' ,0 ,N'Magisch (Ritual)' ,N'WnM 158' ,N'Ritualkenntnis (Optimatik) 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001411' ,N'Verstärkungsfetisch' ,0 ,N'Magisch' ,N'MyMa 129' ,N'Ritualkenntnis (animistische Tradition) 5');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001412' ,N'Stabzauber: Stab der Bohrung' ,0 ,N'Magisch (Ritual)' ,N'WnM 159' ,N'Ritualkenntnis (Optimatik) 9');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001413' ,N'Göttliche Anrufung V' ,0 ,N'Klerikal' ,N'OiC 44' ,N'V Sacredos | (SF Spähtweihe, Liturgiekenntnis 0)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001414' ,N'Instruktion: Alpdruck' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Traumbesuch');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001415' ,N'Instruktion: Alptraumwelt' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Traumgänger II');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001416' ,N'Instruktion: Analyse' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Wahrnehmung der Quelle');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001417' ,N'Instruktion: Antimagie' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001418' ,N'Instruktion: Bann der Quelle' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Antimagie');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001419' ,N'Instruktion: Beseelung durch Quelle' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001420' ,N'Instruktion: Dämonische Manifestation' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001421' ,N'Instruktion: Elementare Manifestation' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Elementare Reinheit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001422' ,N'Instruktion: Elementare Reinheit' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001423' ,N'Instruktion: Explosion' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Schadenszauber');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001424' ,N'Instruktion: Fixierung' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001425' ,N'Instruktion: Geistillusion' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Verhüllung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001426' ,N'Instruktion: Heilung/Wiederherstellung' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001427' ,N'Instruktion: Hellsicht' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001428' ,N'Instruktion: Illusion' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001429' ,N'Instruktion: Infektion' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001430' ,N'Instruktion: Kontrolle über Element' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001431' ,N'Instruktion: Kontrolle über Gefühle' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001432' ,N'Instruktion: Kontrolle über (Wesen)' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001433' ,N'Instruktion: Metamagie' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Antimagie, SF Instruktion: Transfer');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001434' ,N'Instruktion: Objektbewegung' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001435' ,N'Instruktion: Regeneration' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Heilung, SF Heilmagie');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001436' ,N'Instruktion: Reinigung' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001437' ,N'Instruktion: Schadenszauber' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001438' ,N'Instruktion: Schutz vor Quelle' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001439' ,N'Instruktion: Traumbesuch' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001440' ,N'Instruktion: Transfer' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001441' ,N'Instruktion: Transformation' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Verwandlung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001442' ,N'Instruktion: Transport durch Element' ,0 ,N'Magisch' ,N'MyMa 152' ,N'SF Instruktion: Über ein Element gehen');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001443' ,N'Instruktion: Über Element gehen' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001444' ,N'Instruktion: Verhüllung' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001445' ,N'Instruktion: Verwandlung' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001446' ,N'Instruktion: Wahnsinn' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001447' ,N'Instruktion: Wahrnehmung der Quelle' ,0 ,N'Magisch' ,N'MyMa 152' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001448' ,N'Bogenzauber: Bogenweihe' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 3');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001449' ,N'Bogenzauber: Lichtbringer' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 4');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001450' ,N'Bogenzauber: Betäuber' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 5');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001451' ,N'Bogenzauber: Sechsfachtreffer' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 6');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001452' ,N'Bogenzauber: Seilknüpfer' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001453' ,N'Bogenzauber: Lebensräuber' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 8');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001454' ,N'Bogenzauber: Pfeilruf' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001455' ,N'Bogenzauber: Zaubertöter' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 11');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001456' ,N'Bogenzauber: Wegweiser' ,0 ,N'Magisch (Ritual)' ,N'WnM 153' ,N'Ritualkenntnis (Wolfalben) 13');
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 275, 276 / WnM 140' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000001';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000002';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 277 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000006';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 277 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000007';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 277 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000008';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 277, 73, Errata / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000009';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 277, 59 / WdS 73 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000010';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 277 / WdS 73 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000011';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 277 / WdS 73 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000012';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 73 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000013';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 60, 73 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000014';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 95 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000017';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 61, 73 / WnM 144' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000018';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 67, 73 / WnM 145' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000019';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 73 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000020';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 73 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000021';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 95 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000023';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 278 / WdS 61, 68, 74 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000024';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 62, 74 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000026';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 68, 74 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000027';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 68, 74 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000028';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000029';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 62, 74 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000030';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 72, 74 / WnM 146' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000031';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 63, 74 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000032';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 74 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000033';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 74 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000034';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 74 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000035';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 63, 75 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000037';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 63, 75 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000038';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 279 / WdS 68, 75 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000039';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 101 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000040';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 75 / WnM 147' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000041';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 61, 68, 75 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000042';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 61, 68, 75 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000043';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280, 70, 71, 75 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000045';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280, 70, 71, 75 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000046';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 101 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000047';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 76 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000048';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 63, 70, 76 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000051';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 63, 70, 71, 76 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000052';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 63, 70, 71, 76 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000053';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000054';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000055';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 76, 95 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000056';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 76 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000057';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 64, 76 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000058';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 65, 77 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000059';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 65, 77 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000060';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 101 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000061';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 65, 77 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000062';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 77 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000063';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 71, 77 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000064';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 77, 190 / WnM 150' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000065';
UPDATE [Sonderfertigkeit] SET [Name] = N'Waffenlose Kampftechnik (Bornländisch/Gossenstil)' ,[Literatur] = N'WdH 282 / WdS 89 / Rakshazar 24 / OiC 44 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000066';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 89 / Rakshazar 24 / WnM 151 / OiC 44' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000067';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 89 / Rakshazar 24 / WnM 151 / OiC 44' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000068';
UPDATE [Sonderfertigkeit] SET [Name] = N'Waffenlose Kampftechnik (Mercenario/Legionärsstil/Söldnerstil)' ,[Literatur] = N'WdH 282 / WdS 90 / OiC 44 / WnM 185' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000070';
UPDATE [Sonderfertigkeit] SET [Name] = N'Waffenlose Kampftechnik (Unauer Schule/Cyclopeisches Ringen/Echsenzwinger)' ,[Literatur] = N'WdH 282 / WdS 90 / OiC 44' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000071';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 283 / WdS 77 / WnM151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000072';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 283 / WdS 71,77 / WnM 152' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000073';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 283 / WdS 65, 77 / WnM 152' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000074';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 275 / WnM 140' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000076';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000077';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000078';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000079';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000080';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000081';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000082';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000083';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000084';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000085';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 276 / WnM 141' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000086';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000129';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000130';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000131';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000132';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000134';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000135';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000136';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 281 / WdS 95 / WnM 149' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000137';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000138';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000139';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000140';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000141';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000143';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000144';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000145';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 280 / WdS 95 / WnM 148' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000146';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 90 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000147';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 90 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000148';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 90 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000149';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000150';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000151';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000152';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000153';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000154';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000155';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000156';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000157';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000158';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000159';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000160';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000163';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 91 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000164';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000165';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000166';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000167';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000168';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000169';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000170';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000171';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000172';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000173';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 92 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000174';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 93 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000175';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 282 / WdS 93 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000176';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 106 / MyMa 111 / WnM 151' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000177';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 284, 10 / WnM 152' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000178';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 31 / WnM 152' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000179';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 31 / WnM 152' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000180';
UPDATE [Sonderfertigkeit] SET [Name] = N'Fernzauberei I' ,[Literatur] = N'WdZ 24 / WnM 154' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000244';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 10 / WnM 155' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000252';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 16 / WnM 156' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000282';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 16 / WnM 156' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000283';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 76 / WnM 156' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000284';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 76 / WnM 156' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000285';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 55 / WnM 156' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000309';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 55 / WnM 156' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000310';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 10 / WnM 156' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000312';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 10 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000362';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 10 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000363';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 111 / MyMa 117 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000426';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 109 / MyMa 115 / WnM 158' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000427';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 110 / MyMa 116 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000428';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 110 / MyMa 116 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000429';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 111 / MyMa 119 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000431';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 76 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000436';
UPDATE [Sonderfertigkeit] SET [Name] = N'Traumgänger I' ,[Literatur] = N'WdH 291 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000438';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 33 / WnM 160' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000444';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 247 / WnM 160' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000483';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001206';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 114 / WnM 158' ,[Voraussetzungen] = N'SF Instruktion: Analyse' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001207';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001208';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 114 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 13' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001209';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 114 / WnM 158' ,[Voraussetzungen] = N'SF Instruktion: Transfer' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001210';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 123 / WnM 158' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001211';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 114 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 11' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001212';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 111 / WnM 153' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001213';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 134 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001214';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 129 / WnM 153' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001215';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 134 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001216';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 120 / WnM 158' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001217';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 114 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 9' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001220';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001222';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 135 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001223';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 135 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001224';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 134 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001225';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 134 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001226';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 114 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 11' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001228';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 115 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 11' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001230';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 115 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 3, SF Instruktion: Wahrnehmung' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001231';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 116 / WnM 158' ,[Voraussetzungen] = N'RK7, SF Instruktion: Fixierung' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001233';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 135 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis 5' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001234';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 135 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis 3' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001235';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 116 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 7' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001236';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 115, 116 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 5' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001237';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001240';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001241';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 116 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 9' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001242';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001244';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001245';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001246';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 135 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis 7' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001251';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 135 / WnM 158' ,[Voraussetzungen] = N'Ritualkenntnis 11' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001252';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 116 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 9' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001253';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 116 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 5' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001255';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 120 / WnM 158' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001257';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 117 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 5' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001259';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 117 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 9, SF Instruktion: Reinigung' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001260';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001261';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 117 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 13' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001263';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 135 / WnM 157' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001264';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 117 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 12, SF Instruktion: Schutz vor Quelle' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001265';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001267';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001270';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001271';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 131 / WnM 159' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001272';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 117 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 13' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001273';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 117, 118 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 12' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001274';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 120 / WnM 158' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001275';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 120 / WnM 158' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001276';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 118 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 11' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001277';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 118 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 13, SF Instruktion: Schadenszauber' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001278';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119, 120 / WnM 159' ,[Voraussetzungen] = N'SF Stabzauber: Bindung des Stabes, CH 15, Ritualkenntnis (Optimatik) 12' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001279';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 118 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 7' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001283';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 118 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 14' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001284';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 118, 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 15' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001286';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 121 / WnM 158' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001289';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 14' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001291';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 14' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001292';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 14' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001293';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 14' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001294';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 14' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001295';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 14' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001296';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'MyMa 119 / WnM 159' ,[Voraussetzungen] = N'Ritualkenntnis (Optimatik) 5' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001298';


/* Sonderfertigkeit_Setting */

INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000001' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000002' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000006' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000007' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000008' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000009' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000010' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000011' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000012' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000013' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000014' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000017' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000018' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000019' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000020' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000021' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000023' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000024' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000026' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000027' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000028' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000029' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000030' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000031' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000032' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000033' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000034' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000035' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000036' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000037' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000038' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000039' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000040' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000041' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000042' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000043' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000045' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000046' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000047' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000048' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000049' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000050' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000051' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000052' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000066' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000066' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000066' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000067' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000067' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000068' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000068' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000070' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000070' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000071' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000072' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000075' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000076' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000077' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000078' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000080' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000081' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000082' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000083' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000084' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000085' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000086' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000129' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000130' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000131' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000132' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000134' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000135' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000136' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000137' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000138' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000139' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000140' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000141' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000143' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000144' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000145' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000146' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000147' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000148' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000149' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000150' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000151' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000152' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000153' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000154' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000155' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000156' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000157' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000158' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000159' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000160' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000163' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000164' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000165' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000166' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000169' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000170' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000171' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000172' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000173' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000174' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000175' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000176' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000178' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000179' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000180' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000244' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000282' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000309' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000310' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000312' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000362' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000363' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000438' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000444' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000000902' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001302' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001303' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001304' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001305' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001306' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001308' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001309' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001310' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001311' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001312' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001313' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001314' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001315' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001316' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001317' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001318' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001319' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001320' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001321' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001322' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001323' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001324' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001325' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001326' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001327' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001328' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001329' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001330' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001331' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001332' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001333' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001334' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001335' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001336' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001337' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001338' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001339' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001340' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001341' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001342' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001343' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001344' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001345' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001346' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001347' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001348' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001349' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001350' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001351' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001352' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001353' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001354' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001355' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001356' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001357' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001358' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001359' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001360' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001361' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001362' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001363' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001364' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001365' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001366' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001367' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001368' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001369' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001370' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001371' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001372' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001373' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001374' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001375' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001376' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001377' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001378' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001379' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001380' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001381' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001382' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001383' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001384' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001385' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001386' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001387' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001388' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001389' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001390' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001391' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001392' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001393' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001394' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001395' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001396' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001397' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001398' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001399' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001400' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001401' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001402' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001403' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001404' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001405' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001406' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001407' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001408' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001409' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001410' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001411' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001412' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001413' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001414' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001415' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001416' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001417' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001418' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001419' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001420' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001421' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001422' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001423' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001424' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001425' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001426' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001427' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001428' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001429' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001430' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001431' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001432' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001433' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001434' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001435' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001436' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001437' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001438' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001439' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001440' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001441' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001442' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001443' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001444' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001445' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001446' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001447' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001448' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001449' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001450' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001451' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001452' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001453' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001454' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001455' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001456' ,'00000000-0000-0000-5e77-000000000003' ,NULL);


/* Talent */

INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000388' ,N'Sprachen Kennen (Pristidial)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor' ,N'WnM 180');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000389' ,N'Lesen/Schreiben (Imperiale Buchstaben)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'Myranor (HC) 211 / WnM 180');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000390' ,N'Sprachen Kennen (Tesumurrisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 180');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000391' ,N'Sprachen Kennen (Lish''shi/Wolfalbisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 180');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000392' ,N'Lesen/Schreiben (Ban''shi Bilderschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 180');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000393' ,N'Sprachen Kennen (Ur-Vesayitisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 181');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000394' ,N'Sprachen Kennen (Alt-Vesayitisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 181');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000395' ,N'Sprachen Kennen (Gemein-Vesayitisch/Vesayo)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 181');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000396' ,N'Sprachen Kennen (Tighral/Tharr''Orr)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 181');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000397' ,N'Lesen/Schreiben (Khorrzu-Symbole)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 181');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000398' ,N'Lesen/Schreiben (Lyncil-Symbole)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 181');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000399' ,N'Sprachen Kennen (Mholurisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 181');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000400' ,N'Sprachen Kennen (Horngesang)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 182');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000401' ,N'Sprachen Kennen (Alt-Zwergisch/Rhoglossa)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 182');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000402' ,N'Sprachen Kennen (Lutral)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 182');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000403' ,N'Sprachen Kennen (Ruritin)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 182');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000404' ,N'Sprachen Kennen (Norkoshal)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 182');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000405' ,N'Lesen/Schreiben (Alt-Taural)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 182');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000406' ,N'Sprachen Kennen (Myrmidal)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Myranor' ,N'WnM 182');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000407' ,N'Lesen/Schreiben (Alt-Amulashtra)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,N'' ,N'' ,N'' ,N'A' ,N'' ,NULL ,N'Aventurien, Dunkle Zeiten' ,N'WdS 30, 31, 32 / OiC 45');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000408' ,N'Tieremphatie' ,9 ,N'MU' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'F' ,NULL ,NULL ,NULL ,N'WdH 256');
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000089';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000090';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Amaunische Kratzschrift)' ,[Steigerung] = N'A' ,[Literatur] = N'WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000091';
UPDATE [Talent] SET [Steigerung] = N'B' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000093';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000094';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Anneristalya-Bilderschrift)' ,[Steigerung] = N'B' ,[Literatur] = N'WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000095';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Alte Tesumurrische Glyphen)' ,[Steigerung] = N'A' ,[Literatur] = N'WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000097';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000098';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000099';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000100';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Rakshazar' ,[Literatur] = N'WdS 30, 31, 32 / Rakshazar 34 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000101';
UPDATE [Talent] SET [Setting] = N'Aventurien, Myranor' ,[Literatur] = N'WdS 30, 31, 32 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000103';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Asdharia)' ,[Steigerung] = N'A' ,[Setting] = N'Aventurien, Dunkle Zeiten, Rakshazar' ,[Literatur] = N'WdS 30, 31, 32 / Rakshazar 35 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000104';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Bramschoromk/Baramun-Keilschrift)' ,[Steigerung] = N'A' ,[Literatur] = N'WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000105';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000106';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Chuchas/Yash-Hualay-Glyphen/Protozelemja)' ,[Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000107';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000108';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000109';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000110';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000111';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000112';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Eupherban-Code)' ,[Steigerung] = N'A' ,[Literatur] = N'WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000113';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Früh-Imperiale Glyphen)' ,[Steigerung] = N'A' ,[Literatur] = N'WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000114';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000115';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000117';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000118';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000119';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000120';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Myranor' ,[Literatur] = N'WdS 30, 31, 32 / WnM 180 / Myranor (HC) 211 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000121';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000123';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Imperiale Zeichen/Altgüldenländisch/Alt-Imperiale Buchstaben)' ,[Setting] = N'Aventurien, Dunkle Zeiten, Myranor' ,[Literatur] = N'WdS 30, 31, 32 / WnM 180 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000124';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000125';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000126';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Isdira)' ,[Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000127';
UPDATE [Talent] SET [Talentname] = N'Lesen/Schreiben (Kalshinishi/Shingwanische Knotenschrift)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000128';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000129';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000130';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000131';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Myranor' ,[Literatur] = N'WdS 30, 31, 32 / WnM 182 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000132';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000133';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000134';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000135';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000136';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000137';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000138';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000139';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000140';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000141';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000142';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000143';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Rakshazar' ,[Literatur] = N'WdS 30, 32 / Rakshazar 34 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000144';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000145';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Rakshazar' ,[Literatur] = N'WdS 30, 31, 32 / Rakshazar 34 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000146';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000149';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000150';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000151';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000152';
UPDATE [Talent] SET [Steigerung] = N'B' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000155';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000156';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Abishant)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000248';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000249';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000250';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Alt-Imperial/Alt-Güldenländisch/Aureliani)' ,[Steigerung] = N'A' ,[Setting] = N'Aventurien, Dunkle Zeiten, Myranor' ,[Literatur] = N'WdS 30, 31, 32 / Myranor (HC) 211 / WnM 179 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000251';
UPDATE [Talent] SET [Steigerung] = N'B' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000252';
UPDATE [Talent] SET [Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000253';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000254';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000255';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000256';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000257';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000258';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000259';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000260';
UPDATE [Talent] SET [Setting] = N'Aventurien, Rakshazar, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / Rakshazar 30 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000261';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000263';
UPDATE [Talent] SET [Setting] = N'Aventurien, Rakshazar, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / Rakshazar 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000264';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Alamar-Asharielitisch)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000265';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000267';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Sumurrisch/Ur-Bansumitisch)' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000269';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Archäisch/Bashurisch)' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000270';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Boa''goram/Banbarguinisch)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000271';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000272';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Bramscho/Baramunisch)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000273';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000274';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000276';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000277';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000279';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000280';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000282';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000283';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000285';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000286';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Gemein-Amaunal/AhMa)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000287';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 179' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000288';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000289';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000290';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000291';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000292';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000293';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Hiero-Amaunal/AhMaGao)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000294';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 179' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000295';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Hjaldingsch/Saga-Thorwalsch)' ,[Setting] = N'Aventurien, Dunkle Zeiten, Myranor' ,[Literatur] = N'WdS 30, 32 / Myranor (HC) 211 / WnM 179 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000296';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000297';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000298';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000299';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000300';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000301';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000302';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000303';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000304';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Leonal/Khorrzu)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000305';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000306';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Lyncal/Fhi''Ai)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000307';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000308';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000309';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000310';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000311';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000312';
UPDATE [Talent] SET [Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000313';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000314';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000315';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000316';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000317';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000318';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000319';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000320';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000321';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000322';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Padiral/Bhagrach)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 181' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000323';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000324';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Proto-Imperial/Dorinthisch)' ,[Literatur] = N'WnM 179' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000325';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000326';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Ravesaran)' ,[Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000327';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Myranor' ,[Literatur] = N'WdS 30, 32 / Myranor (HC) 212 / WnM 181 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000328';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000329';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000330';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000331';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000332';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000333';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000334';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 182' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000335';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000336';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000337';
UPDATE [Talent] SET [Steigerung] = N'C' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000338';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000339';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000340';
UPDATE [Talent] SET [Steigerung] = N'C' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000341';
UPDATE [Talent] SET [Setting] = N'Aventurien' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000342';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Rakshazar' ,[Literatur] = N'WdS 30, 31, 32 / Rakshazar 31 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000343';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000345';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000346';
UPDATE [Talent] SET [Steigerung] = N'A' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000347';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 212 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000348';
UPDATE [Talent] SET [Steigerung] = N'C' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000350';
UPDATE [Talent] SET [Steigerung] = N'A' ,[Literatur] = N'Myranor (HC) 211 / WnM 180' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000351';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000352';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000353';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000354';
UPDATE [Talent] SET [Talentname] = N'Sprachen Kennen (Z''Lit)' ,[Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000355';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten' ,[Literatur] = N'WdS 30, 31, 32 / OiC 45' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000356';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000092';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000096';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000102';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000116';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000122';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000147';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000148';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000153';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000262';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000266';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000268';
DELETE FROM [Talent] WHERE [TalentGUID]='00000000-0000-0000-007a-000000000344';


/* VorNachteil */

INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Astraler Block II' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000496' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Astraler Block III' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000497' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Beinlos' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000498' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Belyabels Fluch I' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000499' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Belyabels Fluch II' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000500' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Belyabels Fluch III' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000501' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Bittsteller' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000503' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Diktator' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000504' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Erhöhter Nahrungsbedarf I' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000505' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Erhöhter Nahrungsbedarf II' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000506' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Erhöhter Nahrungsbedarf III' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000507' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Faulheit' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor' ,'00000000-0000-0000-f024-000000000508' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Fluch der Finsternis II' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000509' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Fluch der Finsternis III' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000510' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Fürsorglich' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor' ,'00000000-0000-0000-f024-000000000511' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Großer Rumpf' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000512' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Kampflähmung' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000513' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Keine karmale Regeneration' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000514' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Kein räumliches sehen' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000515' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Loyalität' ,0 ,1 ,1 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000516' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Mehrköpfigkeit' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000517' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Schlechte Regeneration II' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000518' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Schlechte Regeneration III' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000519' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Sklavenmentalität' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor' ,'00000000-0000-0000-f024-000000000520' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Spieltrieb' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor' ,'00000000-0000-0000-f024-000000000521' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Tiefenangst' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor' ,'00000000-0000-0000-f024-000000000522' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Ungeduld' ,0 ,1 ,0 ,NULL ,N'Nachteile (Schlechte Eigenschaften)' ,N'Myranor' ,'00000000-0000-0000-f024-000000000523' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Untertan' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000524' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Umvermögen für (Beschwörungsdisziplin)' ,0 ,1 ,1 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000525' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Vierfüßig' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000526' ,NULL);
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Winzwüchsig' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000527' ,NULL);
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000163';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000164';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000165';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000166';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000167';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000168';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000169';
UPDATE [VorNachteil] SET [Name] = N'Astraler Block I' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000170';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000171';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000176';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000177';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000179';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000180';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000181';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000183';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000184';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000185';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000186';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000187';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000188';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000190';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000191';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000192';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000193';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000194';
UPDATE [VorNachteil] SET [Name] = N'Fluch der Finsternis I' ,[Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000195';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000196';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000197';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000198';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000199';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000200';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000201';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000202';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000203';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000204';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000205';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000206';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000207';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000208';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000209';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000211';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000212';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000213';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000214';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000216';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000217';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000218';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000219';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000220';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000222';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000223';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000225';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000226';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000228';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000229';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000230';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000231';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000232';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000233';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000234';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000235';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000236';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000237';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000239';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000240';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000242';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000243';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000244';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000245';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000246';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000247';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000248';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000249';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000250';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000251';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000253';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000254';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000256';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000257';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000258';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000259';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000260';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000261';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000262';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000263';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000264';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000266';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000267';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000268';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000269';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000270';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000271';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000272';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000273';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000274';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000275';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000276';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000277';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000278';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000279';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000280';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000281';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000282';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000283';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000284';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000285';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000286';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000287';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000288';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000289';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000290';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000291';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000292';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000293';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000294';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000295';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000296';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000297';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000298';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000299';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000300';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000301';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000302';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000303';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000304';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000305';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000306';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000307';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000308';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000309';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000310';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000311';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000312';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000313';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000314';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000315';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000316';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000317';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000318';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000319';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000327';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000340';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000341';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000342';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000343';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000344';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000345';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000346';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000347';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000002';
UPDATE [VorNachteil] SET [Typ] = N'Nachteile (Schlechte Eigenschaften)' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000394';