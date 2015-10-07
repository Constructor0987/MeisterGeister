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