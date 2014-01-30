--TODO: Siehe unten!!!

--Strukturerweiterung der Literatur-Tabelle
ALTER TABLE [Literatur] ADD [Größe] float NULL;
ALTER TABLE [Literatur] ADD [GrößeKomprimiert] float NULL;
ALTER TABLE [Literatur] ADD [UrlPdf] nvarchar(500) NULL;
ALTER TABLE [Literatur] ADD [UrlPrint] nvarchar(500) NULL;

--Seitenoffset von Literaturangaben korrigieren
UPDATE [Literatur] SET [Seitenoffset] = 1 WHERE [LiteraturGUID] != '00000000-0000-0000-0011-000000000008' AND [LiteraturGUID] != '00000000-0000-0000-0011-000000000016' AND [LiteraturGUID] != '00000000-0000-0000-0011-000000000023';

--TODO: Update für Daten der Größe und Url
--auf Datenaufbereitung warten...

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