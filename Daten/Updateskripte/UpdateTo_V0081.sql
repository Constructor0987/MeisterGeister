--Einstellungen und Regeln in neue Tabellenstruktur überführen
sp_rename 'Einstellungen', 'Einstellungen_old'
GO
CREATE TABLE [Einstellung] (
    [Kontext]  nvarchar(100) Not Null,
    [Kategorie] nvarchar(100) Null,
    [Name]  nvarchar(100) Not Null,
    [Typ] nvarchar(100) Not Null,
    [Wert] ntext NULL,
    [Beschreibung] ntext Null,    
    CONSTRAINT [PK_Einstellung] PRIMARY KEY ([Name])
)
GO
----Boolean
--FrageNeueKampfrundeAbstellen
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Kampf',NULL,'FrageNeueKampfrundeAbstellen','Boolean', Cast(Cast(WertBool as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'FrageNeueKampfrundeAbstellen'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Kampf',NULL,'FrageNeueKampfrundeAbstellen','Boolean', '0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'FrageNeueKampfrundeAbstellen')
GO
--Jingle
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein',NULL,'JingleAbstellen','Boolean', Cast(Cast(WertBool as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'JingleAbstellen'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein',NULL,'JingleAbstellen','Boolean', '0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'JingleAbstellen')
GO
--WuerfelSound
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Proben',NULL,'WuerfelSoundAbspielen','Boolean', Cast(Cast(WertBool as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'WuerfelSoundAbspielen'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Proben',NULL,'WuerfelSoundAbspielen','Boolean', '0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'WuerfelSoundAbspielen')
GO
--Audio direkt
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Audioplayer',NULL,'AudioDirektAbspielen','Boolean', Cast(Cast(WertBool as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'AudioDirektAbspielen'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Audioplayer',NULL,'AudioDirektAbspielen','Boolean', '0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'AudioDirektAbspielen')
GO
--IsReadOnly
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein','Versteckt','IsReadOnly','Boolean', Cast(Cast(WertBool as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'IsReadOnly'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein','Versteckt','IsReadOnly','Boolean', '0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'IsReadOnly')
GO
----Integer
--Fading
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Audioplayer',NULL,'Fading','Integer', Cast(Cast(WertInt as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'Fading'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Audioplayer',NULL,'Fading','Integer', '600' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'Fading')
GO
--SelectedTab
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein','Versteckt','SelectedTab','Integer', Cast(Cast(WertInt as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'SelectedTab'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein','Versteckt','SelectedTab','Integer', '0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'SelectedTab')
GO
--SelectedHeldenTab
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Helden','Versteckt','SelectedHeldenTab','Integer', Cast(Cast(WertInt as nvarchar) as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'SelectedHeldenTab'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Helden','Versteckt','SelectedHeldenTab','Integer', '0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'SelectedHeldenTab')
GO
--StartTabs
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein','Versteckt','StartTabs','String', Cast(WertString as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'StartTabs'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Allgemein','Versteckt','StartTabs','String', '' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'StartTabs')
GO
--KalenderExpandedSections
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Kalender','Versteckt','KalenderExpandedSections','String', Cast(WertString as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'KalenderExpandedSections'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Kalender','Versteckt','KalenderExpandedSections','String', '111111' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'KalenderExpandedSections')
GO
--ProbenAnzeigeModus
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Proben','Versteckt','ProbenAnzeigeModus','String', Cast(WertString as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'ProbenAnzeigeModus'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Proben','Versteckt','ProbenAnzeigeModus','String', 'Zeile' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'ProbenAnzeigeModus')
GO
--DatumAktuell
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Kalender','Versteckt','DatumAktuell','String', Cast(WertString as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'DatumAktuell'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Kalender','Versteckt','DatumAktuell','String', '1|0|993|0' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'DatumAktuell')
GO
--UmrechnerExpandedSections
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Umrechner','Versteckt','UmrechnerExpandedSections','String', Cast(WertString as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'UmrechnerExpandedSections'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Umrechner','Versteckt','UmrechnerExpandedSections','String', '111111' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'UmrechnerExpandedSections')
GO
--GegnerViewExpandedSections
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Gegner','Versteckt','GegnerViewExpandedSections','String', Cast(WertString as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'GegnerViewExpandedSections'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Gegner','Versteckt','GegnerViewExpandedSections','String', '11' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'GegnerViewExpandedSections')
GO
--Einstellungen_Allgemein
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Gegner','Versteckt','GegnerDetailViewExpandedSections','String', Cast(WertString as ntext),cast(NULL as ntext) FROM Einstellungen_old WHERE Name = 'GegnerDetailViewExpandedSections'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
SELECT 'Gegner','Versteckt','GegnerDetailViewExpandedSections','String', '110' ,cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name = 'GegnerDetailViewExpandedSections')
GO

--Standort
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Allgemein','Versteckt','Standort','String', CASE WHEN WertText is NOT NULL and LEN(cast(WertText as nvarchar)) > 0 THEN WertText ELSE cast(WertString as ntext) END,cast(NULL as ntext) FROM Einstellungen_old WHERE Name='Standort'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Allgemein','Versteckt','Standort','String','Gareth#29.79180235685203#3.735098459067687',cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name='Standort')
GO
--SelectedHeld
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Helden','Versteckt','SelectedHeld','String', CASE WHEN WertText is NOT NULL and LEN(cast(WertText as nvarchar)) > 0 THEN WertText ELSE cast(WertString as ntext) END,cast(NULL as ntext) FROM Einstellungen_old WHERE Name='SelectedHeld'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Helden','Versteckt','SelectedHeld','String',cast(NULL as ntext),cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name='SelectedHeld')
GO
--ProbenFavoriten
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Proben','Versteckt','ProbenFavoriten','String', CASE WHEN WertText is NOT NULL and LEN(cast(WertText as nvarchar)) > 0 THEN WertText ELSE cast(WertString as ntext) END,cast(NULL as ntext) FROM Einstellungen_old WHERE Name='ProbenFavoriten'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Proben','Versteckt','ProbenFavoriten','String',cast(NULL as ntext),cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name='ProbenFavoriten')
GO
--PdfReaderCommand
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Almanach',NULL,'PdfReaderCommand','String', CASE WHEN WertText is NOT NULL and LEN(cast(WertText as nvarchar)) > 0 THEN WertText ELSE cast(WertString as ntext) END,cast(NULL as ntext) FROM Einstellungen_old WHERE Name='PdfReaderCommand'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Almanach',NULL,'PdfReaderCommand','String',cast(NULL as ntext),cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name='PdfReaderCommand')
GO
--PdfReaderArguments
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Almanach',NULL,'PdfReaderArguments','String', CASE WHEN WertText is NOT NULL and LEN(cast(WertText as nvarchar)) > 0 THEN WertText ELSE cast(WertString as ntext) END,cast(NULL as ntext) FROM Einstellungen_old WHERE Name='PdfReaderArguments'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Almanach',NULL,'PdfReaderArguments','String',cast(NULL as ntext),cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name='PdfReaderArguments')
GO
--KampfRecentColors
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Kampf','Versteckt','KampfRecentColors','String', CASE WHEN WertText is NOT NULL and LEN(cast(WertText as nvarchar)) > 0 THEN WertText ELSE cast(WertString as ntext) END,cast(NULL as ntext) FROM Einstellungen_old WHERE Name='KampfRecentColors'
GO
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung]) 
SELECT 'Kampf','Versteckt','KampfRecentColors','String',cast(NULL as ntext),cast(NULL as ntext) WHERE not EXISTS (SELECT EinstellungGUID from Einstellung WHERE Name='KampfRecentColors')
GO

--Regeln
INSERT INTO [Einstellung] ([Kontext],[Kategorie],[Name],[Typ],[Wert],[Beschreibung])
select 'Kampf' as Kontext, Typ as Kategorie, Name, 'Boolean' as Typ, Cast(Cast(Anwenden as nvarchar) as ntext) as Wert, Beschreibung from Regeln Where Beschreibung like 'Kampf:%'
union all
select 'Proben' as Kontext, Typ as Kategorie, Name, 'Boolean' as Typ, Cast(Cast(Anwenden as nvarchar) as ntext) as Wert, Beschreibung from Regeln Where Beschreibung like 'Eigenschafts-Proben:%'
GO

DROP Table Einstellungen_old
GO
DROP Table Regeln
GO