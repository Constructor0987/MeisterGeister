--Liturgien aus Liber Liturgium, Playlist-Hotkeys

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

