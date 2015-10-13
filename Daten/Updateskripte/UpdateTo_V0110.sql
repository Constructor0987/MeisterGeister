-- Icon Verwaltung bei Audio Hot-Buttons
ALTER TABLE [GegnerBase_Audio_Playlist] ADD [Icon] nvarchar(254) NULL DEFAULT '/DSA%20MeisterGeister;component/Images/Icons/General/speaker.png'
GO
ALTER TABLE [Held_Audio_Playlist] ADD [Icon] nvarchar(254) NULL DEFAULT '/DSA%20MeisterGeister;component/Images/Icons/General/speaker.png'
GO

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

--Korrektur Talent
UPDATE [Talent] SET [eBE] = NULL WHERE [TalentGUID] = '00000000-0000-0000-007a-000000000191';

--Literatur Updates
INSERT INTO [Literatur] ([LiteraturGUID],[Name],[Abkürzung],[Seitenoffset],[UrlPdf],[UrlPrint],[Regelsystem],[Setting]) VALUES (N'00000000-0000-0000-0011-000000000076',N'Wege nach Tharun',N'WnT',1,N'http://www.ulisses-ebooks.de/product/148848',N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-tharun/quellenbuecher/62352/wege-nach-tharun?c=1292',0,N'Aventurien');
INSERT INTO [Literatur] ([LiteraturGUID],[Name],[Abkürzung],[Seitenoffset],[UrlPdf],[UrlPrint],[Regelsystem],[Setting]) VALUES (N'00000000-0000-0000-0011-000000000077',N'Granden, Gaukler und Gelehrte',N'GGG',1,N'http://www.ulisses-ebooks.de/product/118982',N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/36472/granden-gaukler-und-gelehrte-meisterpersonen-suedaventuriens',0,N'Aventurien');
INSERT INTO [Literatur] ([LiteraturGUID],[Name],[Abkürzung],[Seitenoffset],[UrlPdf],[UrlPrint],[Regelsystem],[Setting]) VALUES (N'00000000-0000-0000-0011-000000000078',N'Krieger, Krämer und Kultisten',N'KKK',1,N'http://www.ulisses-ebooks.de/product/99163',N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/39000/krieger-kraemer-und-kultisten-meisterpersonen-d.mittelreiches',0,N'Aventurien');
INSERT INTO [Literatur] ([LiteraturGUID],[Name],[Abkürzung],[Seitenoffset],[UrlPdf],[UrlPrint],[Regelsystem],[Setting]) VALUES (N'00000000-0000-0000-0011-000000000079',N'Söldner, Skalden und Steppenelfen',N'SSS',1,N'http://www.ulisses-ebooks.de/product/133572',N'http://www.f-shop.de/wuerfel-und-zubehoer/das-schwarze-auge-aventurien/zubehoer/62169/soeldner-skalden-steppenelfen-meisterpersonen-nordaventur',0,N'Aventurien');

--Anpassungen für DSA5

/* Literatur */
ALTER TABLE [Literatur] DROP COLUMN [Regelsystem];
ALTER TABLE [Literatur] ADD [Regelsystem] nvarchar(50) NULL;
UPDATE [Literatur] SET [Regelsystem] = 'DSA 4.1';
INSERT INTO [Literatur] ([LiteraturGUID],[Name],[Abkürzung],[Seitenoffset],[Größe],[UrlPdf],[UrlPrint],[Regelsystem],[Setting]) VALUES (N'00000000-0000-0000-0011-000000000080',N'DSA5 Regelwerk',N'GRW5',2,NULL,N'http://www.ulisses-ebooks.de/product/151474',N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/regelwerke/62704/dsa5-regelwerk-hardcover','DSA 5',N'Aventurien');
UPDATE [Literatur] SET [Regelsystem] = 'DSA 2' WHERE [LiteraturGUID] = '00000000-0000-0000-0011-000000000067';
UPDATE [Literatur] SET [Regelsystem] = 'DSA 3' WHERE [LiteraturGUID] = '00000000-0000-0000-0011-000000000070' OR [LiteraturGUID] = '00000000-0000-0000-0011-000000000056' OR [LiteraturGUID] = '00000000-0000-0000-0011-000000000014' OR [LiteraturGUID] = '00000000-0000-0000-0011-000000000058' OR [LiteraturGUID] = '00000000-0000-0000-0011-000000000020';
UPDATE [Literatur] SET [Regelsystem] = 'DSA 4' WHERE [LiteraturGUID] = '00000000-0000-0000-0011-000000000009';

/* Held */
ALTER TABLE [Held] ADD [Regelsystem] nvarchar(50) NULL;
UPDATE [Held] SET [Regelsystem] = 'DSA 4.1';

/* Talent */
ALTER TABLE [Talent] ADD [Regelsystem] nvarchar(50) NULL;
UPDATE [Talent] SET [Regelsystem] = 'DSA 4.1' WHERE [TalentGUID] <> '00000000-0000-0000-0000-000000000000';
DROP INDEX [Talent].[UQ_Talent];

INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000461',N'Fliegen',2,N'MU',N'IN',N'GE',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 188',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000462',N'Gaukeleien',2,N'MU',N'CH',N'FF',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 188',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000463',N'Klettern',2,N'MU',N'GE',N'KK',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 189',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000464',N'Körperbeherrschung',2,N'GE',N'GE',N'KO',N'Basis',N'BE',N'D',N'Aventurien',N'GRW5 189',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000465',N'Kraftakt',2,N'KO',N'KK',N'KK',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 189',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000466',N'Reiten',2,N'CH',N'GE',N'KK',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 190',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000467',N'Schwimmen',2,N'GE',N'KO',N'KK',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 190',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000468',N'Selbstbeherrschung',2,N'MU',N'MU',N'KO',N'Basis',N'D',N'Aventurien',N'GRW5 190',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000469',N'Singen',2,N'KL',N'CH',N'KO',N'Basis',N'(BE)',N'A',N'Aventurien',N'GRW5 191',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000470',N'Sinnesschärfe',2,N'KL',N'IN',N'IN',N'Basis',N'(BE)',N'D',N'Aventurien',N'GRW5 191',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000471',N'Tanzen',2,N'KL',N'CH',N'GE',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 192',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000472',N'Taschendiebstahl',2,N'MU',N'FF',N'GE',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 192',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000473',N'Verbergen',2,N'MU',N'IN',N'GE',N'Basis',N'BE',N'C',N'Aventurien',N'GRW5 193',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000474',N'Zechen',2,N'KL',N'KO',N'KK',N'Basis',N'A',N'Aventurien',N'GRW5 193',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000475',N'Bekehren & Überzeugen',3,N'MU',N'KL',N'CH',N'Basis',N'B',N'Aventurien',N'GRW5 194',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000476',N'Betören',3,N'MU',N'CH',N'CH',N'Basis',N'(BE)',N'B',N'Aventurien',N'GRW5 195',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000477',N'Einschüchtern',3,N'MU',N'IN',N'CH',N'Basis',N'B',N'Aventurien',N'GRW5 195',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000478',N'Etikette',3,N'KL',N'IN',N'CH',N'Basis',N'(BE)',N'B',N'Aventurien',N'GRW5 196',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000479',N'Gassenwissen',3,N'KL',N'IN',N'CH',N'Basis',N'(BE)',N'C',N'Aventurien',N'GRW5 196',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000480',N'Mennschenkenntnis',3,N'KL',N'IN',N'CH',N'Basis',N'C',N'Aventurien',N'GRW5 197',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000481',N'Überreden',3,N'MU',N'IN',N'CH',N'Basis',N'C',N'Aventurien',N'GRW5 197',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000482',N'Verkleiden',3,N'IN',N'CH',N'GE',N'Basis',N'(BE)',N'B',N'Aventurien',N'GRW5 198',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000483',N'Willenskraft',3,N'MU',N'IN',N'CH',N'Basis',N'D',N'Aventurien',N'GRW5 198',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000484',N'Fährtensuchen',4,N'MU',N'IN',N'GE',N'Basis',N'BE',N'C',N'Aventurien',N'GRW5 198',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000485',N'Fesseln',4,N'KL',N'FF',N'KK',N'Basis',N'(BE)',N'A',N'Aventurien',N'GRW5 199',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000486',N'Fischen & Angeln',4,N'FF',N'GE',N'KO',N'Basis',N'(BE)',N'A',N'Aventurien',N'GRW5 199',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000487',N'Orientierung',4,N'KL',N'IN',N'IN',N'Basis',N'B',N'Aventurien',N'GRW5 200',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000488',N'Pflanzenkunde',4,N'KL',N'FF',N'KO',N'Basis',N'(BE)',N'C',N'Aventurien',N'GRW5 200',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000489',N'Tierkunde',4,N'MU',N'MU',N'CH',N'Basis',N'BE',N'C',N'Aventurien',N'GRW5 200',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000490',N'Wildnisleben',4,N'MU',N'GE',N'KO',N'Basis',N'BE',N'C',N'Aventurien',N'GRW5 201',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000491',N'Brett- & Glücksspiel',5,N'KL',N'KL',N'IN',N'Basis',N'A',N'Aventurien',N'GRW5 201',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000492',N'Geographie',5,N'KL',N'KL',N'IN',N'Basis',N'B',N'Aventurien',N'GRW5 202',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000493',N'Geschichtswissen',5,N'KL',N'KL',N'IN',N'Basis',N'B',N'Aventurien',N'GRW5 202',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000494',N'Götter & Kulte',5,N'KL',N'KL',N'IN',N'Basis',N'B',N'Aventurien',N'GRW5 203',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000495',N'Kriegskunst',5,N'MU',N'KL',N'IN',N'Basis',N'B',N'Aventurien',N'GRW5 203',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000496',N'Magiekunde',5,N'KL',N'KL',N'IN',N'Basis',N'C',N'Aventurien',N'GRW5 204',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000497',N'Mechanik',5,N'KL',N'KL',N'FF',N'Basis',N'B',N'Aventurien',N'GRW5 204',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000498',N'Rechnen',5,N'KL',N'KL',N'IN',N'Basis',N'A',N'Aventurien',N'GRW5 204',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000499',N'Rechtskunde',5,N'KL',N'KL',N'IN',N'Basis',N'A',N'Aventurien',N'GRW5 205',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000500',N'Sagen & Legenden',5,N'KL',N'KL',N'IN',N'Basis',N'B',N'Aventurien',N'GRW5 205',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000501',N'Sphärenkunde',5,N'KL',N'KL',N'IN',N'Basis',N'B',N'Aventurien',N'GRW5 206',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000502',N'Sternkunde',5,N'KL',N'KL',N'IN',N'Basis',N'A',N'Aventurien',N'GRW5 206',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000503',N'Alchimie',6,N'MU',N'KL',N'FF',N'Basis',N'BE',N'C',N'Aventurien',N'GRW5 206',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000504',N'Boote & Schiffe',6,N'FF',N'GE',N'KK',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 207',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000505',N'Fahrzeuge',6,N'CH',N'FF',N'KO',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 207',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000506',N'Handel',6,N'KL',N'IN',N'CH',N'Basis',N'B',N'Aventurien',N'GRW5 208',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000507',N'Heilkunde Gift',6,N'MU',N'KL',N'IN',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 208',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000508',N'Heiklunde Krankheiten',6,N'MU',N'IN',N'KO',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 208',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000509',N'Heiklunde Seele',6,N'IN',N'CH',N'KO',N'Basis',N'B',N'Aventurien',N'GRW5 209',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000510',N'Heilkunde Wunden',6,N'KL',N'FF',N'FF',N'Basis',N'BE',N'D',N'Aventurien',N'GRW5 209',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000511',N'Holzbearbeitung',6,N'FF',N'GE',N'KK',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 210',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000512',N'Lebensmittelbearbeitung',6,N'IN',N'FF',N'FF',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 210',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000513',N'Lederbearbeitung',6,N'FF',N'GE',N'KO',N'Basis',N'BE',N'B',N'Aventurien',N'GRW5 210',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000514',N'Malen & Zeichnen',6,N'IN',N'FF',N'FF',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 211',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000515',N'Metallbearbeitung',6,N'FF',N'KO',N'KK',N'Basis',N'BE',N'C',N'Aventurien',N'GRW5 211',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000516',N'Musizieren',6,N'CH',N'FF',N'KO',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 212',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000517',N'Schlösserknacken',6,N'IN',N'FF',N'FF',N'Basis',N'BE',N'C',N'Aventurien',N'GRW5 212',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000518',N'Steinbearbeiteung',6,N'FF',N'FF',N'KK',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 212',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Talenttyp],[eBE],[Steigerung],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000519',N'Stoffbearbeitung',6,N'KL',N'FF',N'FF',N'Basis',N'BE',N'A',N'Aventurien',N'GRW5 213',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000520',N'Armbrüste',1,N'FF',N'Basis',N'B',N'Fernkampf',N'Aventurien',N'GRW5 245',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000521',N'Bögen',1,N'FF',N'Basis',N'C',N'Fernkampf',N'Aventurien',N'GRW5 245',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000522',N'Dolche',1,N'GE',N'Basis',N'B',N'Nahkampf',N'Aventurien',N'GRW5 235',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000523',N'Fechtwaffen',1,N'GE',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 235',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000524',N'Hiebwaffen',1,N'KK',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 235',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000525',N'Kettenwaffen',1,N'KK',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 235',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000526',N'Lanzen',1,N'KK',N'Basis',N'B',N'Nahkampf',N'Aventurien',N'GRW5 235',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000527',N'Raufen',1,N'GE',N'KK',N'Basis',N'B',N'Nahkampf',N'Aventurien',N'GRW5 235',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000528',N'Schilde',1,N'KO',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 236',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000529',N'Schwerter',1,N'GE',N'KK',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 236',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Eigenschaft2],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000530',N'Stangenwaffen',1,N'GE',N'KK',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 236',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000531',N'Wurfwaffen',1,N'FF',N'Basis',N'B',N'Fernkampf',N'Aventurien',N'GRW5 245',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000532',N'Zweihandhiebwaffen',1,N'KK',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 236',N'DSA 5');
INSERT INTO [Talent] ([TalentGUID],[Talentname],[TalentgruppeID],[Eigenschaft1],[Talenttyp],[Steigerung],[Untergruppe],[Setting],[Literatur],[Regelsystem]) VALUES (N'00000000-0000-0000-007a-000000000533',N'Zweihandschwerter',1,N'KK',N'Basis',N'C',N'Nahkampf',N'Aventurien',N'GRW5 236',N'DSA 5');
