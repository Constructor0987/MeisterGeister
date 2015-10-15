-- Modifikatoren für abgeleitete Werte auftrennen
ALTER TABLE [Held] ADD [LE_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [LE_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [AU_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [AU_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [AE_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [AE_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [AE_pAsP] int DEFAULT 0;
ALTER TABLE [Held] ADD [KE_ModGen] int DEFAULT 0;
ALTER TABLE [Held] ADD [KE_ModZukauf] int DEFAULT 0;
ALTER TABLE [Held] ADD [KE_pKaP] int DEFAULT 0;

-- TODO: Mod-Werte auf neue Felder verteilen
-- [LE_ModGen] = Wert nach Rasse setzen und [LE_Mod] reduzieren
-- [LE_Mod] modifizieren um VN: Hohe Lebenskraft (+1 LeP pro Stufe; max. 6), Niedrige Lebenskraft (-1 LeP pro Stufe; max. 6)

-- [AU_ModGen] = Wert nach Rasse setzen und [AU_Mod] reduzieren
-- [AU_Mod] modifizieren um VN: Ausdauernd (+2 AuP pro Stufe; max. 3 Stufen, also 6 AuP), Kurzatmig (-2 AuP pro Stufe; max. 3 Stufen, also 6 AuP)

-- [AE_Mod] modifizieren um VN: Vollzauberer (+12 AsP), Halbzauberer (+6 AsP), Viertelzauberer (-6 AsP), Zauberhaar (+7 AsP), Astralmacht (+1 AsP pro Stufe; max. 6), Niedrige Astralkraft (-1 AsP pro Stufe; max. 6)
-- [KE_Mod] modifizieren um Vorteil Geweiht [zwölfgöttliche Kirche/H'Ranga/Angrosch/Gravesh/Xo'Artal-Stadtpantheon]; SF Spätweihe Alveranische Gottheit/Spätweihe Namenloser/Spätweihe (Xo'Artal-Pantheon)/Spätweihe (Xo'Artal-Pantheon) (+24 KaP), 
--				Geweiht [nicht-alveranische Gottheit]; SF Spätweihe Nichtalveranische Gottheit/Kontakt zum Großen Geist (+12 KaP), Vorteil Sacerdos I bis IV; SF Spätweihe Dunkle Zeiten I bis III (+6 je Stufe)

-- DSA5 Anpassungen
ALTER TABLE [Held] ADD [LeiteigenschaftMagisch] nvarchar(2);
-- TODO: Wert aus vorhandenen SF setzen
ALTER TABLE [Held] ADD [LeiteigenschaftKlerikal] nvarchar(2);

--TODO Waffenset

--TODO MT: Datenanpassungen Literatur