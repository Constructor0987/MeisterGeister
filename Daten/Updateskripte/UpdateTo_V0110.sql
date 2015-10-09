-- Datenverbesserungen bei Pflanzen

/* Landschaft */

INSERT INTO [Landschaft] (  [LandschaftGUID],  [Name],  [Kundig]) 
 VALUES ('00000000-0000-0000-00fe-000000000080' ,N'überall außer Eis, Gebirge und Wüste' ,NULL);

/* Landschaftsgruppe */

UPDATE [Landschaftsgruppe] SET [Name] = N'Flüsse, Teiche, Seen' WHERE [LandschaftsgruppeID]=N'G';

/* Landschaftsgruppe_Landschaft */

INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'D' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'E' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'F' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'G' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'H' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'I' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'J' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'K' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'L' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'M' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000006');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'N' ,'00000000-0000-0000-00fe-000000000080');
INSERT INTO [Landschaftsgruppe_Landschaft] (  [LandschaftsgruppeID],  [LandschaftGUID]) 
 VALUES (N'P' ,'00000000-0000-0000-00fe-000000000068');
DELETE FROM [Landschaftsgruppe_Landschaft] WHERE [LandschaftsgruppeID]=N'N' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000068';
DELETE FROM [Landschaftsgruppe_Landschaft] WHERE [LandschaftsgruppeID]=N'P' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000006';

/* Pflanze_Verbreitung */

INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000078' ,'00000000-0000-0000-00fe-000000000080' ,16);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00fe-000000000056' ,0);
INSERT INTO [Pflanze_Verbreitung] (  [PflanzeGUID],  [LandschaftGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-00ff-000000000085' ,'00000000-0000-0000-00fe-000000000080' ,16);
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000054' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000013';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000078' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000001';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000078' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000004';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000078' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000013';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000078' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000056';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000085' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000001';
DELETE FROM [Pflanze_Verbreitung] WHERE [PflanzeGUID]='00000000-0000-0000-00ff-000000000085' AND [LandschaftGUID]='00000000-0000-0000-00fe-000000000003';

--Geländekundig
Update Landschaft SET Kundig = 'Maraskankundig' Where LandschaftGUID in ('00000000-0000-0000-00fe-000000000033', '00000000-0000-0000-00fe-000000000034');
Update Landschaft SET Kundig = 'Steppenkundig' Where LandschaftGUID in ('00000000-0000-0000-00fe-000000000018', '00000000-0000-0000-00fe-000000000008');

--TODO Waffenset

--TODO MT: Datenanpassungen Literatur

--TODO MT: Anpassungen für DSA5
ALTER TABLE [Literatur] DROP COLUMN [Regelsystem];
ALTER TABLE [Literatur] ADD [Regelsystem] nvarchar(50) NULL;

ALTER TABLE [Held] ADD [Regelsystem] nvarchar(50) NULL;
UPDATE [Held] SET [Regelsystem] = 'DSA 4.1';

ALTER TABLE [Talent] ADD [Regelsystem] nvarchar(50) NULL;
UPDATE [Talent] SET [Regelsystem] = 'DSA 4.1';