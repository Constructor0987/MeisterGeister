--Liturgien aus Liber Liturgium, Playlist-Hotkeys, Rechtschreibkorrektur für Ausrüstung

--Playlist-Hotkeys

ALTER TABLE [Audio_Playlist] ADD [Key] nvarchar(30) NULL;
ALTER TABLE [Audio_Playlist] ADD [Modifiers] nvarchar(25) NULL;

--Liturgien:

INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset]) 
 VALUES ('00000000-0000-0000-0011-000000000036' ,N'Liber Liturgium' ,N'LL' ,NULL ,0);

/* Sonderfertigkeit */

INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001537' ,N'Liturgie: Auge Xeledons, Xeledons helles Licht' ,0 ,N'Klerikal (Liturgie)' ,N'LL 103' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001538' ,N'Liturgie: Birkenzweig ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 111' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001539' ,N'Liturgie: Conagas Ruf' ,0 ,N'Klerikal (Liturgie)' ,N'LL 123' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001540' ,N'Liturgie: Der über das Schlachtfeld schreitet ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 126' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001541' ,N'Liturgie: Des Einen bezaubernder Sphärenklang' ,0 ,N'Klerikal (Liturgie)' ,N'LL 127' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001542' ,N'Liturgie: Die goldene Hand' ,0 ,N'Klerikal (Liturgie)' ,N'LL 128' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001543' ,N'Liturgie: Eisherz' ,0 ,N'Klerikal (Liturgie)' ,N'LL 209' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001544' ,N'Liturgie: Eiskerker' ,0 ,N'Klerikal (Liturgie)' ,N'LL 148' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001545' ,N'Liturgie: Erzieherische Maßnahme der Heiligen Yalsicena ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 140' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001546' ,N'Liturgie: Ewige Jugend' ,0 ,N'Klerikal (Liturgie)' ,N'LL 142' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001547' ,N'Liturgie: Firuns Zorn ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 148' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001548' ,N'Liturgie: Fluch wider die Ungläubigen' ,0 ,N'Klerikal (Liturgie)' ,N'LL 150' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001549' ,N'Liturgie: Gebet des kristallklaren Blicks (III)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 156' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001550' ,N'Liturgie: Gebet des kristallklaren Blicks (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 156' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001551' ,N'Liturgie: Golgaris Zwielicht ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 170' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001552' ,N'Liturgie: Gott der Götter' ,0 ,N'Klerikal (Liturgie)' ,N'LL 171' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001553' ,N'Liturgie: Gott der Götter (II)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 171' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001554' ,N'Liturgie: Gott der Götter (III)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 171' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001555' ,N'Liturgie: Gott der Götter (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 171' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001556' ,N'Liturgie: Gott der Götter (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 171' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001557' ,N'Liturgie: Göttliche Strafe (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 165' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001558' ,N'Liturgie: Göttliche Strafe (V)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 165' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001559' ,N'Liturgie: Göttliche Strafe (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 165' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001560' ,N'Liturgie: Herbeirufung der Diener des Herrn' ,0 ,N'Klerikal (Liturgie)' ,N'LL 185' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001561' ,N'Liturgie: Herbeirufung der Heerscharen des Liturgie: Rattenkindes' ,0 ,N'Klerikal (Liturgie)' ,N'LL 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001562' ,N'Liturgie: Initiation (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 256 / LL 193' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001563' ,N'Liturgie: Lidaris Herz ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 209' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001564' ,N'Liturgie: Mikailspfeil ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 213' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001565' ,N'Liturgie: Namenlose Kälte' ,0 ,N'Klerikal (Liturgie)' ,N'LL 215' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001566' ,N'Liturgie: Namenlose Raserei' ,0 ,N'Klerikal (Liturgie)' ,N'LL 216' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001567' ,N'Liturgie: Namenloser Zweifel, Namenlose Erleuchtung' ,0 ,N'Klerikal (Liturgie)' ,N'LL 217' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001568' ,N'Liturgie: Namenloses Vergessen' ,0 ,N'Klerikal (Liturgie)' ,N'LL 218' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001569' ,N'Liturgie: Nemekaths Geisterblick (III)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 267 / LL 219' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001570' ,N'Liturgie: Objektsegen (V)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 257 / LL 223' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001571' ,N'Liturgie: Pech und Schwefel' ,0 ,N'Klerikal (Liturgie)' ,N'LL 227' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001572' ,N'Liturgie: Phexens Verteidigung ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 235' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001573' ,N'Liturgie: Rahjalinas Farbenspiel' ,0 ,N'Klerikal (Liturgie)' ,N'LL 242' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001574' ,N'Liturgie: Rahjas Begehren' ,0 ,N'Klerikal (Liturgie)' ,N'LL 244' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001575' ,N'Liturgie: Reiches Land  (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 251' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001576' ,N'Liturgie: Reiches Land (V)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 251' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001577' ,N'Liturgie: Reiches Land (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 251' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001578' ,N'Liturgie: Revolution der Gedanken ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 254' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001579' ,N'Liturgie: Ritus der Schlachthilfe' ,0 ,N'Klerikal (Liturgie)' ,N'LL 255' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001580' ,N'Liturgie: Ruf der Ferne  ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 257' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001581' ,N'Liturgie: Ruf zum Bund wider die Mächte der Finsternis' ,0 ,N'Klerikal (Liturgie)' ,N'LL 260' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001582' ,N'Liturgie: Runjensweisung ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 262' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001583' ,N'Liturgie: Schleichende Fäulnis' ,0 ,N'Klerikal (Liturgie)' ,N'LL 266' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001584' ,N'Liturgie: Schnell wie eine Eidechse ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 268' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001585' ,N'Liturgie: Schwindende Zauberkraft' ,0 ,N'Klerikal (Liturgie)' ,N'LL 271' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001586' ,N'Liturgie: Sechs Leben des Mungos ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 272' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001587' ,N'Liturgie: Sechs Leben des Mungos (III)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 272' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001588' ,N'Liturgie: Seelenbannung' ,0 ,N'Klerikal (Liturgie)' ,N'LL 273' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001589' ,N'Liturgie: Seelengefährte ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 274' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001590' ,N'Liturgie: Seelenrudel' ,0 ,N'Klerikal (Liturgie)' ,N'LL 274' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001591' ,N'Liturgie: Seelenschatten' ,0 ,N'Klerikal (Liturgie)' ,N'LL 276' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001592' ,N'Liturgie: Seelenschatten (V)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 276' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001593' ,N'Liturgie: Siegel Borons (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 268 / LL 290' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001594' ,N'Liturgie: Siegel Borons (V)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 268 / LL 290' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001595' ,N'Liturgie: Sippenfluch (V)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 289 / LL 291' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001596' ,N'Liturgie: Sippenfluch (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 289 / LL 291' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001597' ,N'Liturgie: Teilung der Wasser' ,0 ,N'Klerikal (Liturgie)' ,N'LL 303' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001598' ,N'Liturgie: Teilung der Wasser (III)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 303' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001599' ,N'Liturgie: Teilung der Wasser (V)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 303' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001600' ,N'Liturgie: Vertrauter der Flamme (III)' ,0 ,N'Klerikal (Liturgie)' ,N'WdG 279 / LL 324' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001601' ,N'Liturgie: Wachsamkeit der Gänse ' ,0 ,N'Klerikal (Liturgie)' ,N'LL 328' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001602' ,N'Liturgie: Wachsamkeit der Gänse (III)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 328' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001603' ,N'Liturgie: Waffenfluch' ,0 ,N'Klerikal (Liturgie)' ,N'LL 329' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001604' ,N'Liturgie: Wegzehrung der Heiligen Selma' ,0 ,N'Klerikal (Liturgie)' ,N'LL 333' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001605' ,N'Liturgie: Wundersames Teilen des Martyriums' ,0 ,N'Klerikal (Liturgie)' ,N'LL 341' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001606' ,N'Liturgie: Wundersames Teilen des Martyriums (V)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 341' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001607' ,N'Liturgie: Wundersames Teilen des Martyriums (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 341' ,NULL);
UPDATE [Sonderfertigkeit] SET [Name] = N'Liturgie: Achmad''ayan ankhrellah al''nurach Shaitanim' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000558';
UPDATE [Sonderfertigkeit] SET [Name] = N'Liturgie: Große Tabus' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000681';
UPDATE [Sonderfertigkeit] SET [Name] = N'Liturgie: Kräutersegen des Heiligen Nemekath' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000726';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 266 / LL 112 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001191';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 267 / LL 120 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001192';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 267 / LL 120 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001193';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 267 / LL 200 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001194';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 267 / LL 248 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001195';
UPDATE [Sonderfertigkeit] SET [Name] = N'Liturgie: Schutz des Geleges' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000802';
UPDATE [Sonderfertigkeit] SET [Name] = N'Liturgie: Segnung des Heims' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000822';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 267, 268 / LL 220 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001197';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 268 / LL 290 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001198';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdG 268 / LL 290 / OiC 58' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000842';
UPDATE [Sonderfertigkeit] SET [Name] = N'Liturgie: Vertrauter des Felsens' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000872';


/* Sonderfertigkeit_Setting */

INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001537' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001538' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001539' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001540' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001541' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001542' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001543' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001544' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001545' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001546' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001547' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001548' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001549' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001550' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001551' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001552' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001553' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001554' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001555' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001556' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001557' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001558' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001559' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001560' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001561' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001562' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001563' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001564' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001565' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001566' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001567' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001568' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001569' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001570' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001571' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001572' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001573' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001574' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001575' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001576' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001577' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001578' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001579' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001580' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001581' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001582' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001583' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001584' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001585' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001586' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001587' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001588' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001589' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001590' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001591' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001592' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001593' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001594' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001595' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001596' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001597' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001598' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001599' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001600' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001601' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001602' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001603' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001604' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001605' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001606' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001607' ,'00000000-0000-0000-5e77-000000000001' ,NULL);


--Rechtschreibkorrektur für Ausrüstung

UPDATE [Ausrüstung] SET [Bemerkung] = N'Für jeweils 1 SR erhält man Druckluft für 5 Schüsse und erwirbt 1 Punkt Erschöpfung. Luftpumpe notwenig. Weitere Regelung siehe Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000099';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000101';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000102';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000104';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Genaue Wirkung der Substanzen hängt vom Inhalt ab. Nach dem Wurf wird auf W20 geworfen ob dieser beschädigt wurde. Bei einer 20 ist dieser beschädigt und kann erst wieder gefüllt eingesetzt werden, wenn dieser repariert wurde.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000105';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000106';
UPDATE [Ausrüstung] SET [Bemerkung] = N'AT +10. Die TP gelten als Erschwernis auf einen Entfesseln Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000109';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Wird der Angriff mit einem Schild abgewehrt, bleibt dieser stecken und verdoppelt so den AT-Malus des Schildes, während der PA-Bonus um 1 sinkt. Um den Speer aus dem Schild zu ziehen, benötigt der Schildträger 3 Aktionen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000113';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Zuschläge steigen um 3 für sehr große Ziele. Patzer bei 19 oder 20.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000114';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Vom Pferderücken aus gegen Fußkämpfer eingesetzt richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000115';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000116';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000117';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000118';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000119';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000120';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000121';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000122';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000123';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000124';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000125';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000126';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000127';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000128';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000129';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe benötigt keine min. KK mehr. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000130';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000001';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer richtet ein Amazonensäbel zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000002';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Schlag eines Andergasters kann mit Fechtwaffen und Dolchen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000003';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer richtet die Waffe zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000005';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000006';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer, richtet das Barbarenschwert zwei zusätzliche TP an. Erfordert mindestens KK 15 für die einhändige Führung mit dem Talent Schwerter.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000007';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Erfordert mindestens KK 15.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000008';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit einer Basiliskenzunge können Schläge von Kettenwaffen (mit der Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000009';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Erfordert mindestens KK 15 für die einhändige Führung mit dem Talent Schwerter.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000010';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Das Beil ist nur in Myranor als Wurfwaffe zu verwenden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000011';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ein Stich wird auf das Talent Dolche (AT erschwert um 3, eigene nächste PA ebenfalls erschwert um 3) abgelegt.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000018';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit dem Degen können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000019';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000021';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Wird immer von einem Spießgespann (siehe Sonderfertigkeit) geführt.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000022';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. Patzer bei 19-20. Krontrollwurf um 2 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000151';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel können nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000152';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Die Manöver Stumpfer Schlag und Betäubungsschlag sind um 2 Punkte erleichtert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000153';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000154';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000156';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Waffe 2 zusätzliche TP an. Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000157';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit einem Drachenzahn können Schläge von Kettenwaffen (mit der Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000023';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ein Stich wird auf das Talent Speere (AT erschwert um 3) abgelegt ein erfolgreicher Einsatz des Dorns richtet 1W6+4 TP an.Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000028';
UPDATE [Ausrüstung] SET [Bemerkung] = N'unverkäuflich' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000029';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000030';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Richtet der Schlag SP an, kommen noch 1W-1 SP Feuerschaden hinzu. Fällt eine 6, so erlischt die Fackel.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000032';
UPDATE [Ausrüstung] SET [Literatur] = N'AA 87, Errata 4' ,[Bemerkung] = N'Beachte die Regelung im Arsenal. Ein (zwergischer) Waffenmeister (Felsspalter) hat gegen alle Gegner der Größenklassen groß oder größer eine Attacke-Erleichterung von 2 Punkten, egal welches (mit dem Felsspalter erlaubte und von ihm bereits erlernte) Angriffsmanöver er durchführt. Er darf Finten und Defensiven Kampfstil nutzen. Der Hammerschlag ist für ihn um 2 Punkte erleichtert (gegen große Wesen also sogar um 4 Punkte). Um diese SF zu erlernen, muss der zwergische Kämpfer eine GE und eine KK von jeweils 17 aufweisen' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000034';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit dem Florett können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000036';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000039';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge eines Gruufhai können mit Dolchen und Fechtwaffen nicht pariert werden. Kann ab KK 18 einhändig mit dem Talent Hiebwaffen geführt werden (TP/KK dann 15/3).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000040';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Für Haumesser gilt nicht der TP-Bonus für Reiter.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000043';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge einer Hellebarde können mit Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000044';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge mit der Holzfälleraxt können von Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000045';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel können nciht pariert werden. Das Ziehen eines Balisong erfordert eine Aktion, das Öffnen eine weitere (bei gelungener FF-Probe nur eine freie Aktion).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000158';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann von Dolchen und Fechtwaffen nicht pariert werden. Der Satz bezüglich der einhändigen Führung entfällt.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000159';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000161';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel können nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000164';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Reittier aus gegen Fußkämpfer richtet die Waffe 2 TP mehr an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000165';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Reittier aus gegen Fußkämpfer richtet die Waffe 2 TP mehr an. Kann von Dolchen und Fechtwaffen nicht pariert werden. Weitere Regelung siehe Arsenal. Errata: 1 Bei einhändiger Führung mit dem Talent Hiebwaffen ändert sich lediglich die TP/KK, hierfür ist außerdem eine KK von 15 erforderlich.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000168';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. Patzer bei 19-20. Krontrollwurf um 2 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000170';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal (S. 91). Kann ab KK 16 einhändig geführt werden (TP/KK dann 13/5).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000050';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Erfordert mindestens GE 15. Das Entwaffnen aus der AT ist um 3 Punkte erleichtert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000052';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte Regelung im Arsenal. Der Kettenstab/Drei-Glieder-Stab ignoriert den PA Bonus von Schilden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000053';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000061';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge eines Kriegshammers können mit Dolchen und Fechtwaffen nicht pariert werden. Kann ab KK 18 einhändig mit dem Talent Hiebwaffen geführt werden (TP/KK dann 15/3).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000063';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die hier angegebenen Werte gelten für die zweckfremde Verwendung als improvisierter Speer.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000064';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000172';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. Patzer bei 19-20. Krontrollwurf um 2 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000173';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000178';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000067';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000069';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit dem Magierrapier können Schläge von Kettenwaffen, Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwertern nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000072';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000077';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000078';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. BF, Gewicht und Länge können abweichen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000081';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer, richtet die Molokdeschnaja zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000082';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Die einhändige Führung mit dem Talent Schwerter erfordert mindestens GE 16.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000084';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Das Opfer kann ohne weiteres auf Abstand gehalten werden. Die GE des Opfers ist dann um 8 vermindert. Will das Opfer sich mit Gewalt befreien erleidet es (je nach Bösartigkeit der Konstruktion) 1W4 bis 2W4 TP und automatisch je eine Wunde pro KO/2 SP in der Zone Kopf. Im Verlauf von 30-FF Aktionen kann man sich jedoch ohne Probe langsam selbst befreien ohne Schaden zu nehmen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000183';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000186';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000187';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel können nicht pariert werden. Optional gilt: WM 0/-1 über Wasser und WM 1/0 unter Wasser.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000188';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Im Falle einer einhändigen Führung betragen die TP 1W6+3 und der WM beläuft sich auf 0/-5.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000197';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000198';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Erfordert mindestens KK 16. Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist). Eine 19 zählt bei dieser Waffe auch als Patzer.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000087';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Klinge bleibt in der Wunde stecken und verursacht pro SR 1W6 SP. Die Entfernung erzeugt 1W-1 bei gelungener Heilkunde Probe, sonst 2W6 SP.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000088';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Erfordert mindestens KK 15. Die PA des Gegners ist um 2 Punkte erschwert (sofern eine PA überhaupt möglich ist).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000089';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Orchidee als unbewaffnet. Ein Kämpfer mit einer Orchidee besitzt einen Zonen-RS von 1.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000090';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regeleung im Arsenal. Kann ab KK 14 einhändig geführt werden (TP/KK dann 13/3). Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000091';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Mit der Stoßklinge kann mit dem Talent Speere auch gestochen werden: 1W6+7 TP, AT um 5 erschwert, eigene nächste PA ebenfalls um 5 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000092';
UPDATE [Ausrüstung] SET [Bemerkung] = N'In der DK N hat die Peitsche einen AT-Malus von 8. In der DK H ist sie nicht zu verwenden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000096';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000097';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet ein Rabenschnabel zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000098';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000100';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ein Stich wird auf das Talent Speere (AT erschwert um 4) abgelegt ein erfolgreicher Einsatz des Dorns richtet 1W6+5 TP an. Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000101';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet ein Säbel zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000106';
UPDATE [Ausrüstung] SET [Bemerkung] = N'unverkäuflich' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000107';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe die Regelung in den Errata.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000108';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren. Man kann den RS des Gegners ignorieren. Der sich aus dem RS des Gegners ergebende Probenzuschlag kann nicht ignoriert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000199';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000109';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit dem Schweren Dolch können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000112';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ein Stich mit dem Dorn der Skraja (AT auf das Talent Dolche, erschwert um 3) richtet 1W6+3 TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000116';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann ab KK 15 einhändig geführt werden (TP/KK dann 13/5). Die Manöver Gegenhalten und Gezielter Stich sind um 2 Punkte erleichtert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000118';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann ab KK 18 einhändig geführt werden (TP/KK dann 14/3).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000119';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet der Streitkolben zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000124';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe die Regeln zu improvisierten Waffen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000125';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die hier angegebenen Werte gelten für die Zweckentfremdung als improvisierten Speer. Für die Werte im Reiterkampf siehe WdS.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000127';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Es existieren auch hölzerne, ähnlich ausgewuchtete Übungsvarianten (1W1 TP (A), BF 5).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000128';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Richtet automatisch eine Wunde an sobald er mehr als KO/2-2 erzeugt. Weitere Regelung siehe Arsenal. Die nach dem ersten Satz der Besonderheiten aufgeführten Punkte werden gestrichen. Ab einer KK von 23 kann die Waffe jedoch auch einhändig mit dem Talent Hiebwaffen und einem WM von 23/2 verwendet werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000219';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Entwaffnen aus der AT ist um 2 Punkte erleichtert. Weitere Regelung siehe Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000220';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Erfordert mindestens GE 15, ansonsten gilt für jeden Punkt unter 15: TP, INI und PA -1.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000129';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Veteranenhand als unbewaffnet. Ein Kämpfer mit einer Veteranenhand besitzt einen Zonen-RS von 2.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000130';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000132';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit dem Waqqif können die Schläge von Kettenwaffen (mit der Ausnahme von Geißel und Neunschänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwertern oder -säbeln nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000133';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Schläge eines Warunker Hammers können mit Fechtwaffen und Dolchen nicht pariert werden. Mit der Stoßklinge kann mit dem Talent Speere auch gestochen werden: 1W5 TP, AT um 3 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000134';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Erleichtert das Entwaffnen aus der AT um 4 Punkte (Errata WdS S.4).' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000143';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit dem Zwergenschlägel können Schläge von Kettenwaffen, Zweihandflegeln und Zweihand-Hiebwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000144';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ein Stich mit dem Dorn der Zwergenskraja (AT auf das Talent Dolche, erschwert um 4) richtet 1W6+3 TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000145';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Eine Drachenklaue kann mit dem Manöver Gerade eingesetzt werden. Die Manöver Waffe Zerbrechen und Entwaffnen aus der Parade sind um 2 erleichtert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000149';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Orchidee als unbewaffnet. Ein Kämpfer mit einer Orchidee besitzt einen Zonen-RS von 1.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000150';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000222';
UPDATE [Ausrüstung] SET [Bemerkung] = N'TP/KK bei einhändiger Führung beträgt 14/5.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000224';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer richtet die Waffe 2 zusätzliche TP an. Gezielter Stich und Todesstoß sind um 1 erleichtert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000226';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer richtet die Waffe 2 zusätzliche TP an. Weitere Regelung siehe Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000228';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Schläge können mit Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000229';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Es gelten die üblichen Parade-Einschränkungen für Dolche.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000007';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000008';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000019';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an. Der Kampfdiskus setzt eine KK von 14 zum korrekten Einsatz vorraus, ansonsten gilt er als improvisierte Wurfwaffe' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000021';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Munitionspreis in Myranor ist nur 2 Perkunos.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000026';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die TP gelten als Erschwernis auf einen Entfesseln-Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000028';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Munitionspreis ist für spezielle Bleikugeln, die 1 TP mehr anrichten und die Fernkampf-Probe um 1 Punkt erleichtern.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000031';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.  Nicht-Risso erleiden einen Malus von -1/-1.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000232';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann von Dolchen und Fechtwaffen nicht pariert werden. Weitere Regelung siehe Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000233';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000236';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Sagaris zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000238';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Schläge können mit Dolchen und Fechtwaffen nicht pariert werden. Rüstungen schützen in normalem Umfang.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000240';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000242';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an. Nicht der Tod von Links wird dank der Waffenmeisterschaft erleichtert, sondern der Mehrfachangriff.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000243';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Erzeugt echte TP.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000244';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Munitionspreis ist für spezielle Bleikugeln, die 1 TP mehr anrichten.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000036';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000039';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Es gelten nicht nur die üblichen Parade-Einschränkungen für Dolche, sondern zudem ist der Wurfdolch noch eine improvisierte Waffe für den Nahkampf.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000040';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Es gelten nicht nur die üblichen Parade-Einschränkungen für Dolche, sondern zudem ist das Wurfmesser noch eine improvisierte Waffe für den Nahkampf.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000042';
UPDATE [Ausrüstung] SET [Bemerkung] = N'AT +8. Die TP gelten als Erschwernis auf einen Entfesseln-Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000043';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei TP/KK 13/3 richtet die Fernkampfwaffe +1 TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000045';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000245';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Schläge können mit Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000250';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Schläge können mit Dolchen und Fechtwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000251';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Damit die Giftwirkung gilt muss mindestens 1 SP gemacht werden' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000255';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000257';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000258';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000260';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an. Die angegebenen Werte gelten für die Verwendung als Zweihandwaffe, der unter den Besonderheiten aufgeführte Bonus entfällt dafür. Die TP/KK bei einhändiger Führung beträgt 13/4, zudem ist dafür eine KK von 15 erforderlich und das Talent Hiebwaffen kommt zur Anwendung.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000262';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Mit dem Hakendolch können auch die Angriffe von Kettenwaffen nicht pariert werden (Errata WdS S.4).' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000014';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000015';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000016';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000018';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000019';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Hummerschere als unbewaffnet. Ein Kämpfer mit einer Hummerschere hat einen Zonen-RS von 2. Die Werte für Gewicht und BF sind geraten.' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000020';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Das Ziel eines Wurfbeils muss wenigstens 5 Schritt entfernt sein. Richtet eine Attacke dank einer Metallrüstung keinen Schaden
an, muss ein Bruchtest durchgeführt werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000263';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Auch bei einhändiger Führung ist die Distanzklasse S.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000265';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000270';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000272';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000273';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Im waffenlosen Kampf werden echte TP erzeugt.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000274';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt richtet die Waffe 2 zusätzliche TP an. Giftregelung siehe Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000278';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann keine Schläge von Kettenwaffen, Zweihandhiebwaffen, Zweihandschwerter und -säbel parieren.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000279';
UPDATE [Ausrüstung] SET [Bemerkung] = N'' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000280';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Im Kampf werden echte TP erzeugt.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000281';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Entspricht dem Großer Sklaventod. Beachte die Regelung im Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000282';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Entspricht dem Sklaventod. Vom Pferderücken aus gegen Fußkämpfer richtet die Waffe zwei zusätzliche TP an. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000283';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Standardmunition sind Kettenstecher.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000046';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die TP gelten als Erschwernis auf einen Entfesseln-Wurf, den das Ziel ablegen muss, bei Misslingen steigt die Folgeprobe um 1. Die Waffe richtet keine echten TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000050';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000051';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000052';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000053';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*mehr. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000054';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000055';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Entspricht dem Amazonensäbel. Vom Pferderücken aus gegen Fußkämpfer richtet die Waffe zwei zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000285';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Entspricht dem Brabakbengel. Mit dem zentralen Dorn kann mit dem Talent Dolche auch gestochen werden: 1W6 TP, AT um 3 erschwert, eigene nächste PA ebenfalls um 3 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000286';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Entspricht dem Sonnenszepter.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000287';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000290';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ignoriert den PA Bonus, den Schilde bieten. Kann von Dolchen und Fechtwaffen nicht pariert werden. Bei allen anderen Waffen ist die PA um 2 erschwert. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000291';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer richtet das Sichelschwert zwei zusätzliche TP an. Alle Manöver zum Entwaffnen sind mit dem Sichelschwert um 2 Punkte erleichtert. Umreißen ist mit dieser Waffe erlaubt.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000292';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000056';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000057';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei einer KK von 16 oder weniger gelten alle Ziele als eine Klasse weiter entfernt und die TP sinken um 1.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000097';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Es können 2 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000058';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000059';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000060';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000061';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000062';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000063';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Es können 3 Schüsse abgegeben werden, ohne dazwischen nachzuladen. Forschung: Grobschmied (Waffenschmied) 10/ Vierteljahr/ 30 TaP*. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000064';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000065';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Forschung für den gebohrten Lauf: Grobschmied (Waffenschmied) 12/ Vierteljahr/ 90TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000066';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000067';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Die Waffe hat keine KK-Voraussetzungen mehr. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000068';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Waffe setzt KK 15 vorraus, darunter gelten die Ziele als eine Kategorie kleiner.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000069';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Nicht vom Reittier aus einsetzbar. Im Nahkampf kann die Waffe mit dem Talent Speere geführt werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000073';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei einer KK von 13 oder weniger verdoppelt sich die Ladezeit.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000076';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei einer KK von 13 oder weniger verdoppelt sich die Ladezeit.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000077';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei einer KK von 12 oder weniger verdoppelt sich die Ladezeit.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000081';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Regelung siehe Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000084';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die Standardmunition sind gehärtete Pfeile. Bei anderen Pfeilen enstehen die folgenden Nachteile: Patzer bei 18-20. Bei Bestätigung zu dem 2W6 Wurf zusätzlich 2. Bei dem Ergebnis "Kameraden getroffen" trifft der Schütze bei einem ungeraden Wurf immer sich selbst und zwar an dem Arm, der den Bogen hält.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000087';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Preis für diese Geschosse entspricht angefertigten Schleuderbleien, die 1 TP mehr anrichten und die FK Probe um 1 erleichtern. Passende Steine lassen sich jedoch (fast) überall finden. Beim gezielten Schuss sind alle Zuschläge verdoppelt.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000089';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Wenn die TP die KK des Opfer übersteigen, wird dies niedergeworfen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000090';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Wenn die TP die KK des Opfer übersteigen, wird dies niedergeworfen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000091';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Regelung siehe Arsenal.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000092';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Damit das Gift des Pfeils wirkt, muss mindestens 1 SP verursacht werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000093';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*.Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000131';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelwerk im Myranischen Arsenal.Die Waffe benötigt keine min. KK mehr. Forschung für den gezogenen Lauf: Grobschmied (Waffenschmied) 15/ Vierteljahr/120TaP*. Forschung für den Armbrustschaft: Bogenbau (Armbrust) oder (Bela) 10/Vierteljahr/ 15 TaP*. Vom Pferderücken aus gegen einen Fußkämpfer eingesetzt, richtet die Waffe 2 zusätzliche TP an.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000132';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Wunde bei KO/2-2. Jeder Punkt unter KK 15 erhöht die Ladezeit um 5 Aktionen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000133';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe Regelung Dunkle Zeiten.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000134';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei Verwendung von 2 Tartaschen gilt INI 1.' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000032';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT und PA, bis die Waffe wieder gerade gebogen wird. Aus Stahl hat die Waffe einen BF von 1; verbiegen würde diese nicht.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000165';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT und PA, bis die Waffe wieder gerade gebogen wird. Aus Stahl hat die Waffe einen BF von 4; verbiegen würde diese nicht.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000166';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT und PA, bis die Waffe wieder gerade gebogen wird. Schläge von Kettenwaffen (Ausnahme Geißel und Neunschwänzige), Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwerter oder -säbel können nicht pariert werden. Ab TaW 10+ kann die Aktion in Reaktion umgewandelt werden ohne dass der Malus von +4 entsteht.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000167';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT und PA, bis die Waffe wieder gerade gebogen wird. Schläge von Kettenwaffen (Ausnahme Geißel und Neunschwänzige), Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwerter oder -säbel können nicht pariert werden. Ab TaW 10+ kann die Aktion in Reaktion umgewandelt werden ohne dass der Malus von +4 entsteht.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000168';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT und PA, bis die Waffe wieder gerade gebogen wird. Mit Drachenhörnern alleine können Schläge von Zweihandflegeln und Zweihandhiebwaffen nicht pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000169';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann ab KK 15 einhändig geführt werden (TP 1W+5, TP/KK 13/5, WM 0/-5).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000251';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann einhändig geführt werden (TP 1W+3, TP/KK 13/5, WM 0/-6).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000252';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann einhändig geführt werden (TP 1W+3, TP/KK 13/5, WM 0/-6).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000253';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann einhändig geführt werden (TP 1W+2, TP/KK 13/5, WM -1/-6).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000254';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT/PA bis die Waffe wieder gerade gebogen wird. Mit dieser Waffe können Schläge von Kettenwaffen (mit der Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht pariert werden. Mit der Waffe ist kein Todesstoß möglich dafür aber einen Wuchtschlage.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000175';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT/PA bis die Waffe wieder gerade gebogen wird. Mit dieser Waffe können Schläge von Kettenwaffen (mit der Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen und Zweihandschwerter oder -säbeln nicht pariert werden. Mit der Waffe ist kein Todesstoß möglich dafür aber einen Wuchtschlage.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000176';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Um die Waffe spannen zu können, muss man auf festem Untergrund stehen. Man benötigt hierzu eine KK von 14. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000312';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann ab KK 14 einhändig geführt werden (TP/KK 12/4).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000270';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann ab KK 14 einhändig geführt werden (TP/KK 12/4).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000272';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Schlag dieser Waffe kann mit Fechtwaffen und Dolchen nicht pariert werden. Gegen metallene Rüstungen richtet die große Schwert-Keule nur 2W+2 TP an. Bei jedem Treffer gegen eine Plattenrüstung muss ein BF-Test durchgeführt werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000275';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Schlag dieser Waffe kann mit Fechtwaffen und Dolchen nicht pariert werden. Kann ab KK 18 einhändig geführt werden (TP/KK 15/3).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000277';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Schlag dieser Waffe kann mit Fechtwaffen und Dolchen nicht pariert werden. Kann ab KK 18 einhändig geführt werden (TP/KK 15/3).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000278';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Gegen metallene Rüstung richtet die Wolfszahnkeule nur 1W+2 TP an. Gegen Plattenrüstungen muss nach jedem erfolgreichen Treffer einen BF-Test durchgeführt werden. Schläge dieser Waffe können nicht mit Dolchen und Fechtwaffen pariert werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000280';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann mit dem waffenlosen Manövern Tritt, Hoher Tritt und Sprungtritt eingesetzt werden. Die DK entspricht dabei jeweils der DK des Manövers. Der Angriff mit einer Messerklaue gilt als unbewaffneter Angriff.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000288';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann mit dem waffenlosen Manövern Tritt, Hoher Tritt und Sprungtritt eingesetzt werden. Die DK entspricht dabei jeweils der DK des Manövers. Der Angriff mit einer Messerklaue gilt als unbewaffneter Angriff.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000289';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe die Regelung im Buch der Klingen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000291';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe die Regelung im Buch der Klingen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000292';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann mit dem waffenlosen Manöver Schwanzschlag eingesetzt werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000293';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann mit dem waffenlosen Manöver Schwanzschlag eingesetzt werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000294';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann mit dem waffenlosen Manöver Schwanzschlag eingesetzt werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000295';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Alle FK-Proben sind um 3 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000306';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Richtet ein Treffer mindestens 1 SP an kommt die Giftwirkung beim Opfer zum Tragen. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000307';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Um die Waffe spannen zu können, muss man auf festem Untergrund stehen. Ein Brokthar benötigt hierzu eine KK von 16, ein Mensch eine KK von 17. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000311';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kann mit dem waffenlosen Manöver Schwanzschlag eingesetzt werden.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000296';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge dieser Waffe können mit Dolchen und Fechtwaffen nicht pariert werden. Die Waffe kann ab KK 20 einhändig mit dem Talent Hiebwaffen geführt werden. Die TP/KK betragen dann 16/2, die DK sinkt auf N.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000189';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge dieser Waffe können mit Dolchen und Fechtwaffen nicht pariert werden. Die Waffe kann ab KK 20 einhändig mit dem Talent Hiebwaffen geführt werden. Die TP/KK betragen dann 16/2, die DK sinkt auf N.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000190';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT/PA bis die Waffe wieder gerade gebogen wird.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000195';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT/PA bis die Waffe wieder gerade gebogen wird.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000196';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Jede Erhöhung des BF führt zu einem Malus von -1 auf AT/PA bis die Waffe wieder gerade gebogen wird. Alle FK-Proben sind mit der Wurfklinge um +2 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000225';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe die Regelung im Buch der Klingen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000233';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Siehe die Regelung im Buch der Klingen.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000262';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Gegen metallene Rüstungen richtet die Waffe nur 1W+3 TP an. Gegen Plattenrüstungen muss nach jedem erfolgreichen Treffer ein Bruchfaktortest durchgeführt werden. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000264';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Gegen metallene Rüstungen richtet die Waffe nur 1W+3 TP an. Gegen Plattenrüstungen muss nach jedem erfolgreichen Treffer ein Bruchfaktortest durchgeführt werden. ' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000265';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Alle Fernkampfproben mit dieser Waffe sind um 3 erschwert. Um die Armbrust spannen zu können, ist eine KK von 14 nötig. Der angegebene Schaden gilt für Geschosse aus Stein, Geschosse aus Metall richten 1 TP mehr an. Sind die Geschosse mit Stacheln versehen erhöht sich ihr Schaden abermals um 1 TP, allerdings erschweren sie die FK-Probe zusätzlich um 1.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000313';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Für den Sklavenfänger ist eine Mindest-KK von 15 erforderlich. Darunter gelten alle Ziele als um eine Klasse weiter entfernt als sie in Wirklichkeit sind. Außerdem sinken die TP um 2. Der Bogen kann nur von berittenen Schützen benutzt werden. Für Nicht-Brokthar ist die Verwendung des Sklavenjägers je nach Anatomie um 1 bis 3 Punkte erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000314';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Alle FK-Proben sind um 3 erschwert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000316';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die TP geben beim normalen Einsatz die Erschwernis auf die Entfesseln-Probe an, um sich zu befreien. Die Probe darf jede KR wiederholt werden; jede misslungene Probe erschwert alle folgenden Proben um 1 Punkt. Im verhedderten Zustand richtet der Greifer echte TP an. Die TP+ gelten nur für den verhedderten Zustand. Das Verheddern braucht 1 Aktion, das Entwirren 4 Aktionen und eine gelungene FF-Probe.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000317';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Die angegebenen TP gelten für Steine. Speziell hergestellte Schleuderbleie richten 1 TP mehr an. Reichweiten für andere Geschosse sind: 10/20/35/50/80.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000318';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Zu den 4W Schaden kommt noch 1W6 KR lang ein Folgeschaden von je 1W6. Patzergefahr bei 19 oder 20. Ein nicht abgewendeter Patzer bedeutet dass der Schütze sich mit voller Auswirkung selbst in Brand gesetzt hat. Bei einem abgewendeten Patzer hat die Waffe Feuer gefangen aber der Schütze konnte sie rechtzeitig wegwerfen (Sekundäreffekte je nach Situation).' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000320';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Mit der Hand an der sich der Kalazz befindet, kann eine Waffe oder ein Schild geführt werden. Allerdings kann immer nur eins von beidem benutzt werden, der Kalazz oder der in der Hand gehaltene Gegenstand. Der Wechsel zwischen beidem erfordert eine Aktion Position.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000324';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Gegen metallene Rüstungen richtet die Waffe nur 1W+1 TP an. Gegen Plattenrüstungen muss nach jedem erfolgreichen Treffer ein BF-Test durchgeführt werden. Die Waffe kann mit den waffenlosen Manövern Gerade oder Schwinger oder zum Angriff mit dem Schild eingesetzt werden. Der Mur’grakz kann als Parierwaffe gleichzeitig mit einer beliebigen Einhandwaffe geführt werden. Mit der Waffe können Schläge von Zweihand-Hiebwaffen nicht abgefangen werden. Das Manöver Entwaffnen aus der PA ist um 2 erleichtert.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000325';
