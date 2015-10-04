-- Landschaften aufgeräumt und gruppiert

CREATE TABLE [Landschaftsgruppe] (
  [LandschaftsgruppeID] nvarchar(1) NOT NULL
, [Name] nvarchar(254) NOT NULL
);
CREATE TABLE [Landschaftsgruppe_Landschaft] (
  [LandschaftsgruppeID] nvarchar(1) DEFAULT 'A' NOT NULL
, [LandschaftGUID] uniqueidentifier NOT NULL
);
ALTER TABLE [Landschaftsgruppe] ADD CONSTRAINT [PK_Landschaftsgruppe] PRIMARY KEY ([LandschaftsgruppeID]);
ALTER TABLE [Landschaftsgruppe_Landschaft] ADD CONSTRAINT [PK_Landschaftsgruppe_Landschaft] PRIMARY KEY ([LandschaftsgruppeID],[LandschaftGUID]);
ALTER TABLE [Landschaftsgruppe_Landschaft] ADD CONSTRAINT [FK_Landschaftsgruppe_Landschaft_Landschaftsgruppe] FOREIGN KEY ([LandschaftsgruppeID]) REFERENCES [Landschaftsgruppe]([LandschaftsgruppeID]) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE [Landschaftsgruppe_Landschaft] ADD CONSTRAINT [FK_Landschaftsgruppe_Landschaft_Landschaft] FOREIGN KEY ([LandschaftGUID]) REFERENCES [Landschaft]([LandschaftGUID]) ON DELETE CASCADE ON UPDATE CASCADE;

/* Gebiet */

INSERT INTO [Gebiet] (  [GebietGUID],  [Name],  [Left],  [Top],  [Right],  [Bot]) 
 VALUES ('00000000-0000-0000-00f9-000000000024' ,N'Palakar' ,-3.97073518205224 ,23.7559918391783 ,-3.91868622893972 ,23.7029723881684);

 /* Polygon */

INSERT INTO [Polygon] (  [PolygonGUID],  [Name],  [Left],  [Top],  [Right],  [Bot],  [Data]) 
 VALUES ('00000000-0000-0000-0095-000000000267' ,N'Palakar' ,-3.97073518205224 ,23.7559918391783 ,-3.91868622893972 ,23.7029723881684 ,N'(-3.92787270850656, 23.7029723881684), (-3.91868622893972, 23.7409481021009), (-3.95715724047008, 23.7559918391783), (-3.97073518205224, 23.7203175875256),');


/* Gebiet_Polygon */

INSERT INTO [Gebiet_Polygon] (  [GebietGUID],  [PolygonGUID]) 
 VALUES ('00000000-0000-0000-00f9-000000000024' ,'00000000-0000-0000-0095-000000000267');

/* Landschaft */

UPDATE [Landschaft] SET [Kundig] = N'Dschungelkundig' WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000033';
UPDATE [Landschaft] SET [Kundig] = N'Gebirgskundig' WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000034';
UPDATE [Landschaft] SET [Name] = N'Wüstenrandgebiete' WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000037';
UPDATE [Landschaft] SET [Kundig] = N'Steppenkundig' WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000062';
UPDATE [Landschaft] SET [Kundig] = N'Steppenkundig' WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000067';
UPDATE [Landschaft] SET [Kundig] = N'Steppenkundig' WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000079';
DELETE FROM [Landschaft] WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000016';
DELETE FROM [Landschaft] WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000036';
DELETE FROM [Landschaft] WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000039';
DELETE FROM [Landschaft] WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000047';
DELETE FROM [Landschaft] WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000053';
DELETE FROM [Landschaft] WHERE [LandschaftGUID]='00000000-0000-0000-00fe-000000000058';


/* Landschaftsgruppe */

INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'A' ,N'Eis');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'B' ,N'Wüste, Wüstenrand');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'C' ,N'Gebirge');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'D' ,N'Hochland');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'E' ,N'Steppe, Buschland');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'F' ,N'Grasland, Wiesen');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'G' ,N'Fluss- und Seeufer, Teiche, Seen');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'H' ,N'Küste, Strand');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'I' ,N'Flussauen');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'J' ,N'Sumpf, Moor');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'K' ,N'Regenwald');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'L' ,N'Wald');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'M' ,N'Waldrand');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'N' ,N'Höhlen, Ruinen');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'O' ,N'Meer');
INSERT INTO [Landschaftsgruppe] (  [LandschaftsgruppeID],  [Name]) 
 VALUES (N'P' ,N'Sonstiges');


/* Landschaftsgruppe_Landschaft */

INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'A' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'A' ,'00000000-0000-0000-00fe-000000000003');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'A' ,'00000000-0000-0000-00fe-000000000004');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'B' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'B' ,'00000000-0000-0000-00fe-000000000037');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'B' ,'00000000-0000-0000-00fe-000000000056');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'B' ,'00000000-0000-0000-00fe-000000000057');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000013');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000014');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000015');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000034');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000066');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000069');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'C' ,'00000000-0000-0000-00fe-000000000070');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000013');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000014');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000015');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000019');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000034');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000040');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000057');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000066');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000069');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000070');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000077');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'E' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'E' ,'00000000-0000-0000-00fe-000000000043');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'E' ,'00000000-0000-0000-00fe-000000000062');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'E' ,'00000000-0000-0000-00fe-000000000067');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'E' ,'00000000-0000-0000-00fe-000000000079');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'F' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'F' ,'00000000-0000-0000-00fe-000000000005');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'F' ,'00000000-0000-0000-00fe-000000000008');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'F' ,'00000000-0000-0000-00fe-000000000018');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'F' ,'00000000-0000-0000-00fe-000000000055');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000009');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000012');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000041');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000048');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000049');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000071');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000078');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000011');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000017');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000029');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000031');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000044');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000073');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'I' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'I' ,'00000000-0000-0000-00fe-000000000009');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'I' ,'00000000-0000-0000-00fe-000000000010');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'I' ,'00000000-0000-0000-00fe-000000000011');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'I' ,'00000000-0000-0000-00fe-000000000012');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'I' ,'00000000-0000-0000-00fe-000000000076');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'J' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'J' ,'00000000-0000-0000-00fe-000000000030');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'J' ,'00000000-0000-0000-00fe-000000000035');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'J' ,'00000000-0000-0000-00fe-000000000045');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'J' ,'00000000-0000-0000-00fe-000000000046');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'J' ,'00000000-0000-0000-00fe-000000000075');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'K' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'K' ,'00000000-0000-0000-00fe-000000000033');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'K' ,'00000000-0000-0000-00fe-000000000038');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'K' ,'00000000-0000-0000-00fe-000000000063');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'L' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'L' ,'00000000-0000-0000-00fe-000000000007');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'L' ,'00000000-0000-0000-00fe-000000000032');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'L' ,'00000000-0000-0000-00fe-000000000052');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'L' ,'00000000-0000-0000-00fe-000000000074');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'L' ,'00000000-0000-0000-00fe-000000000076');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'M' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'M' ,'00000000-0000-0000-00fe-000000000007');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'M' ,'00000000-0000-0000-00fe-000000000032');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'M' ,'00000000-0000-0000-00fe-000000000054');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'M' ,'00000000-0000-0000-00fe-000000000076');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000020');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000021');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000022');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000023');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000024');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000025');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000026');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000050');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000051');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000068');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'O' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'O' ,'00000000-0000-0000-00fe-000000000059');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'O' ,'00000000-0000-0000-00fe-000000000061');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000001');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000002');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000006');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000027');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000028');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000042');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000060');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000064');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000072');

/* Pflanze_Gebiet */

INSERT INTO [Pflanze_Gebiet] (  [PflanzeGUID],  [GebietGUID]) 
 VALUES ('00000000-0000-0000-00ff-000000000084' ,'00000000-0000-0000-00f9-000000000024');
DELETE FROM [Pflanze_Gebiet] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000084' AND [GebietGUID]='00000000-0000-0000-00f9-000000000063';


/* Pflanze_Verbreitung */

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000001' ,'00000000-0000-0000-00fe-000000000052' ,8);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000015' ,'00000000-0000-0000-00fe-000000000037' ,8);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000023' ,'00000000-0000-0000-00fe-000000000052' ,16);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000074' ,'00000000-0000-0000-00fe-000000000074' ,16);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000084' ,'00000000-0000-0000-00fe-000000000001' ,1);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000093' ,'00000000-0000-0000-00fe-000000000046' ,4);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000124' ,'00000000-0000-0000-00fe-000000000052' ,8);
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000001' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000053';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000015' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000058';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000023' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000053';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000074' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000036';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000084' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000039';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000093' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000047';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000124' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000053';
