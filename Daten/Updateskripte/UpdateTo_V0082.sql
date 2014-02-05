--Ergänzungen der Literaturtabelle

--Strukturerweiterung der Literatur-Tabelle
ALTER TABLE [Literatur] ADD [Größe] float NULL;
ALTER TABLE [Literatur] ADD [GrößeKomprimiert] float NULL;
ALTER TABLE [Literatur] ADD [UrlPdf] nvarchar(500) NULL;
ALTER TABLE [Literatur] ADD [UrlPrint] nvarchar(500) NULL;

--Seitenoffset von Literaturangaben korrigieren
UPDATE [Literatur] SET [Seitenoffset] = 1 WHERE [LiteraturGUID] != '00000000-0000-0000-0011-000000000008' AND [LiteraturGUID] != '00000000-0000-0000-0011-000000000016' AND [LiteraturGUID] != '00000000-0000-0000-0011-000000000023';

--Neue Literatur
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000037' ,N'Hallen arkaner Macht' ,N'HaM' ,NULL ,1, 'http://www.ulisses-ebooks.de/product/109888/', 'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/32226/hallen-arkaner-macht-q6', 32.93, 29.99);
 INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000038' ,N'Horte magischen Wissens' ,N'HmW' ,NULL ,1, 'http://www.ulisses-ebooks.de/product/109889/', 'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/44363/horte-magischen-wissens-q7', 26.54, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000039' ,N'Grüne Hölle 1: Porto Velvenya' ,N'U1' ,NULL ,1, 'http://www.ulisses-ebooks.de/product/110760/', 'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/abenteuer/36779/gruene-hoelle-1-porto-velvenya-u1', 25.11, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000040' ,N'Grüne Hölle 2: Der Fluch des Blutsteins' ,N'U2' ,NULL ,1, 'http://www.ulisses-ebooks.de/product/115887/', 'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/abenteuer/41369/gruene-hoelle-2-der-fluch-des-blutsteins-u2', 17.51, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000041' ,N'Grüne Hölle 3: Der Gott der Xo''Artal' ,N'U3' ,NULL ,1, 'http://www.ulisses-ebooks.de/product/123550', 'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/abenteuer/37510/gruene-hoelle-3-der-gott-der-xoartal-u3', 30.22, NULL);
 INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000042' ,N'Wege der Alchimie - Errata' ,N'WdA Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/396/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000043' ,N'Wege der Zauberei - Errata' ,N'WdZ Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/466/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000044' ,N'Wege der Götter - Errata' ,N'WdG Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/465/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000045' ,N'Wege des Schwerts - Errata' ,N'WdS Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/469/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000046' ,N'Liber Cantiones Deluxe - Errata' ,N'LCD Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/464/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000047' ,N'Wege der Helden - Errata' ,N'WdH Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/467/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000048' ,N'Zoo-Botanica Aventurica - Errata' ,N'ZBA Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/487/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000049' ,N'Aventurisches Arsenal - Errata' ,N'AA Errata' ,NULL ,0, 'http://www.ulisses-spiele.de/download/482/', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000050' ,N'Myranisches Arsenal - Errata' ,N'MyAr Errata' ,NULL ,0, 'http://www.uhrwerk-verlag.de/downl/Myranor/MyArs_Errata_15102012.pdf', NULL, NULL, NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset], [UrlPdf], [UrlPrint], [Größe], [GrößeKomprimiert]) 
 VALUES ('00000000-0000-0000-0011-000000000051' ,N'Wege nach Myranor - Errata' ,N'WnM Errata' ,NULL ,0, 'http://www.uhrwerk-verlag.de/downl/Myranor/WnM%20Errata_15102012.pdf', NULL, NULL, NULL);

 --Update für Daten der Größe und Url
/* Literatur */
UPDATE [Literatur] SET [Größe] = 25.61 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93362' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/31604/wege-der-helden' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000001';
UPDATE [Literatur] SET [Größe] = 20.71 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93366' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/31554/wege-des-schwerts' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000002';
UPDATE [Literatur] SET [Größe] = 33.27 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93367' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/40079/wege-der-zauberei' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000003';
UPDATE [Literatur] SET [Größe] = 27.23 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93368' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/31605/wege-der-goetter' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000004';
UPDATE [Literatur] SET [Größe] = 40.13 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/102823' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/36197/wege-des-entdeckers' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000005';
UPDATE [Literatur] SET [Größe] = 6.55 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93373' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/44674/liber-cantiones' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000006';
UPDATE [Literatur] SET [Größe] = 22.29 ,[GrößeKomprimiert] = 17.52 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93374' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/41398/wege-der-alchimie?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000007';
UPDATE [Literatur] SET [UrlPdf] = N'http://www.ulisses-spiele.de/download/396/' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000008';
UPDATE [Literatur] SET [Größe] = 15.55 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93360' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000009';
UPDATE [Literatur] SET [Größe] = 18.6 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93361/' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/33320/aventurisches-arsenal-r1' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000010';
UPDATE [Literatur] SET [Größe] = 23.07 ,[GrößeKomprimiert] = 22.52 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/96422' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/32761/von-toten-und-untoten?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000011';
UPDATE [Literatur] SET [Größe] = 74.34 ,[GrößeKomprimiert] = 39.08 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/109886' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/30859/tractatus-contra-daemonis?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000012';
UPDATE [Literatur] SET [Größe] = 54.24 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/105549' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/37789/elementare-gewalten?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000013';
UPDATE [Literatur] SET [Größe] = 23.05 ,[GrößeKomprimiert] = 18.76 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/108863' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/32670/die-dunklen-zeiten-imperien-in-truemmern' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000015';
UPDATE [Literatur] SET [UrlPdf] = N'http://www.ulisses-spiele.de/download/502/' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000016';
UPDATE [Literatur] SET [Größe] = 22.67 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/93364' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/44664/zoo-botanica-aventurica-r3?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000017';
UPDATE [Literatur] SET [Größe] = 6.83 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/109885' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/44890/handelsherr-u-kiepenkerl-q4?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000018';
UPDATE [Literatur] SET [Größe] = 21.96 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/109956' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/43951/efferds-wogen-q2?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000019';
UPDATE [Literatur] SET [Größe] = 29.36 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/109954' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/44720/katakomben-und-kavernen-q5?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000021';
UPDATE [Literatur] SET [UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/36562/geographia-aventurica-g0?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000022';
UPDATE [Literatur] SET [Größe] = 12.63 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/123083' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/abenteuer/37674/esche-u-kork-ab.149?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000024';
UPDATE [Literatur] SET [UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-myranor/regelwerke/32787/wege-nach-myranor' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000025';
UPDATE [Literatur] SET [UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-myranor/regelwerke/36096/myranisches-arsenal' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000027';
UPDATE [Literatur] SET [UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-myranor/quellenbuecher/32194/myranische-goetter?c=1287' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000029';
UPDATE [Literatur] SET [Name] = N'Codex Monstrorum' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000031';
UPDATE [Literatur] SET [UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/32670/die-dunklen-zeiten-imperien-in-truemmern' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000033';
UPDATE [Literatur] SET [UrlPdf] = N'http://rakshazar.de/downloads/Rakshazar_Buch_der_Helden_HQ.pdf' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/limitiertsondereditionen/41182/rakshazar-buch-der-helden' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000034';
UPDATE [Literatur] SET [UrlPdf] = N'http://rakshazar.de/downloads/Buch%20der%20Klingen%20Beta3.pdf' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000035';
UPDATE [Literatur] SET [Größe] = 6.66 ,[UrlPdf] = N'http://www.ulisses-ebooks.de/product/115560' ,[UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/34160/liber-liturgium?c=1278' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000036';
