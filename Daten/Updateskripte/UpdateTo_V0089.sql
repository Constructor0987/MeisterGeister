--Daten-Update: Uthuria
--Setting
INSERT INTO [Setting] (  [SettingGUID],  [Name],  [Aktiv]) VALUES ('00000000-0000-0000-5e77-000000000005' ,N'Uthuria' ,0);

--Talent
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000409' ,N'Sprachen Kennen (Xoxota)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Uthuria' ,N'U2 94');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000410' ,N'Lesen/Schreiben (Xo''Artal-Glyphen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Uthuria' ,N'U2 94');

 --VorNachteil
 INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Geweiht [Xo''Artal-Stadtpantheon]' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Uthuria' ,'00000000-0000-0000-f024-000000000528' ,N'U2 102');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Speisegebote (Xo''Artal-Blutmagier)' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Uthuria' ,'00000000-0000-0000-f024-000000000529' ,N'U2 102');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Wahrer Name (Xo''Artal-Magier)' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Uthuria' ,'00000000-0000-0000-f024-000000000530' ,N'U2 102');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Blutsteingebunden' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Uthuria' ,'00000000-0000-0000-f024-000000000531' ,N'U2 102');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Prinzipientreue [Xo''Artal]' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Uthuria' ,'00000000-0000-0000-f024-000000000532' ,N'U2 102');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Moralkodex [Xo''Artal]' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Uthuria' ,'00000000-0000-0000-f024-000000000533' ,N'U2 102');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Spätweihe (Xo''Artal-Pantheon)' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Uthuria' ,'00000000-0000-0000-f024-000000000534' ,N'U2 103');

 --Sonderfertigkeit
 INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001608' ,N'Liturgie: Blutopfer der Tlamachil' ,0 ,N'Klerikal (Liturgie)' ,N'U2 106' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001609' ,N'Liturgie: Parinteotls Blutsaat' ,0 ,N'Klerikal (Liturgie)' ,N'U2 106' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001610' ,N'Liturgie: Spiegel der Taten' ,0 ,N'Klerikal (Liturgie)' ,N'U2 106' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001611' ,N'Liturgie: Des Goldenen Ehrerbietung' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001612' ,N'Liturgie: Ohr der Eule' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001613' ,N'Liturgie: Fluch der Scholle' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001614' ,N'Liturgie: Blut des Vergessens' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001615' ,N'Liturgie: Haut des Feindes' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001616' ,N'Liturgie: Geisterwächter' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107,108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001617' ,N'Liturgie: Caljixiuhs Blutsaat' ,0 ,N'Klerikal (Liturgie)' ,N'U2 106' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001618' ,N'Apport (Uthuria)' ,0 ,N'Magisch (Ritual)' ,N'U2 109' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001619' ,N'Blutanalyse' ,0 ,N'Magisch (Ritual)' ,N'U2 109' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001620' ,N'Blutopfer der Tetlachi' ,0 ,N'Magisch (Ritual)' ,N'U2 109, 110' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001621' ,N'Auge der Sonne' ,0 ,N'Magisch (Ritual)' ,N'U2 110' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001622' ,N'Blutsteingespür' ,0 ,N'Magisch (Ritual)' ,N'U2 110' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001623' ,N'Kleine Formung des Blutsteins' ,0 ,N'Magisch (Ritual)' ,N'U2 110' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001624' ,N'Schrei nach Macht' ,0 ,N'Magisch (Ritual)' ,N'U2 110' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001625' ,N'Bad im Blute' ,0 ,N'Magisch (Ritual)' ,N'U2 110' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001626' ,N'Blutraub' ,0 ,N'Magisch (Ritual)' ,N'U2 110' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001627' ,N'Haut des Erdblutes' ,0 ,N'Magisch (Ritual)' ,N'U2 110, 111' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001628' ,N'Große Formung des Blutsteins' ,0 ,N'Magisch (Ritual)' ,N'U2 111' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001629' ,N'Iteration' ,0 ,N'Magisch (Ritual)' ,N'U2 111' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001630' ,N'Kraft des Opfertieres' ,0 ,N'Magisch (Ritual)' ,N'U2 111' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001631' ,N'Blutbanner' ,0 ,N'Magisch (Ritual)' ,N'U2 111' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001632' ,N'Erdblutwächter' ,0 ,N'Magisch (Ritual)' ,N'U2 112' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001633' ,N'Fülle des Blutsteins' ,0 ,N'Magisch (Ritual)' ,N'U2 112' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001634' ,N'Waffenloses Manöver (Coatls List)' ,0 ,N'Kampf' ,N'U2 95' ,N'Ringen 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001635' ,N'Waffenloses Manöver (Jaguarklaue)' ,0 ,N'Kampf' ,N'U2 95' ,N'Raufen 10, Ringen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001636' ,N'Liturgie: Augen des Tieres' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001637' ,N'Liturgie: Augen der Nacht' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001638' ,N'Liturgie: Augen des Waldes' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001639' ,N'Liturgie: Auge der Schlange' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001640' ,N'Liturgie: Unantastbarkeit des Richters' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001641' ,N'Liturgie: Ascheblut' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001642' ,N'Liturgie: Nepillomes Umhang' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001643' ,N'Liturgie: Ewiger Geisterwächter' ,0 ,N'Klerikal (Liturgie)' ,N'U2 107,108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001644' ,N'Liturgie: Objektweihe (Blutgoldintarsien)' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001645' ,N'Liturgie: Blut der Sonne' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001646' ,N'Liturgie: Ordination (Blutsteinbindung)' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001647' ,N'Liturgie: Aufnahme des Götterbruders' ,0 ,N'Klerikal (Liturgie)' ,N'U2 108, 109' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001648' ,N'Liturgie: Blutsteinweihe' ,0 ,N'Klerikal (Liturgie)' ,N'U2 109' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001649' ,N'Liturgie: Verbannung des Götterbruders' ,0 ,N'Klerikal (Liturgie)' ,N'U2 109' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001650' ,N'Liturgie: Gott der Götter (VII)' ,0 ,N'Klerikal (Liturgie)' ,N'LL 171 / U3 125' ,NULL);

 INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000000066' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000000067' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000000068' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001464' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001465' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001466' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001484' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001485' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001495' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001501' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001512' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001513' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001541' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001542' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001548' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001553' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001554' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001555' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001556' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001560' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001561' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001565' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001566' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001567' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001568' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001571' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001583' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001585' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001591' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001603' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001608' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001609' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001610' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001611' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001612' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001613' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001614' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001615' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001616' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001617' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001618' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001619' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001620' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001621' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001622' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001623' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001624' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001625' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001626' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001627' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001628' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001629' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001630' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001631' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001632' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001633' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001634' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001635' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001636' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001637' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001638' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001639' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001640' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001641' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001642' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001643' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001644' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001645' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001646' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001647' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001648' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001649' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001650' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);

 --Ausrüstung
 INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000342' ,N'Macana' ,80 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000343' ,N'Macana (scharf,spitz)' ,80 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000344' ,N'Macana (Holz)' ,80 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000345' ,N'Macana (Stein)' ,80 ,N'U1 121' ,N'Gegen Plattenrüstungen sinken die TP nochmals um 1 und ein Bruchfaktortest ist nötig. Bricht die Waffe, dringen Splitter in den Gegner ein (1W6-1 TP) und die Wundbrandgefahr ist um +2 erhöht.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000346' ,N'Patu (Holz)' ,40 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000347' ,N'Kotiate (Holz)' ,80 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000348' ,N'Patu (Stein)' ,40 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000349' ,N'Kotiate (Stein)' ,80 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000350' ,N'Patu (Jade)' ,40 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000351' ,N'Kotiate (Jade)' ,80 ,N'U1 121' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000352' ,N'Patu (Knochen)' ,40 ,N'U1 121' ,N'Gegen Plattenrüstungen sinken die TP nochmals um 1 und ein Bruchfaktortest ist nötig. Bricht die Waffe, dringen Splitter in den Gegner ein (1W6-1 TP) und die Wundbrandgefahr ist um +2 erhöht.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000353' ,N'Kotiate (Knochen)' ,80 ,N'U1 121' ,N'Gegen Plattenrüstungen sinken die TP nochmals um 1 und ein Bruchfaktortest ist nötig. Bricht die Waffe, dringen Splitter in den Gegner ein (1W6-1 TP) und die Wundbrandgefahr ist um +2 erhöht.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000354' ,N'Raz’Thon' ,15 ,N'U1 121' ,N'Die Raz’Thon (sprich: Rass-Ton) kann mit dem waffenlosen Manöver Schwanzschlag eingesetzt werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000355' ,N'Utharspfote' ,40 ,N'U1 122' ,N'Die Waffe erzeugt im waffenlosen Kampf echte TP und kann mit den waffenlosen Manövern Doppelschlag, Gerade, Handkante und Schwinger eingesetzt werden. ' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000356' ,N'Stachelfaust' ,60 ,N'U1 122' ,N'Die Waffe erzeugt im waffenlosen Kampf echte TP und kann mit den waffenlosen Manövern Doppelschlag, Gerade, Handkante und Schwinger eingesetzt werden. Die Stachelfaust kann auch mit Versteckter Klinge geführt werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000357' ,N'Tonfas' ,20 ,N'U1 122' ,N'Die Waffe erzeugt im waffenlosen Kampf echte TP und kann mit den waffenlosen Manövern Doppelschlag, Gerade, Handkante und Schwinger eingesetzt werden. ' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000358' ,N'Schadd' ,80 ,N'U1 122' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000359' ,N'Avesschnabel' ,75 ,N'U1 122' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000360' ,N'Swafnirfinne' ,60 ,N'U1 122' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000361' ,N'Stachelkeule' ,100 ,N'U1 123' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000362' ,N'Große Stachelkeule' ,130 ,N'U1 123' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000363' ,N'Tumak' ,60 ,N'U1 123' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000364' ,N'Tumak-Dej' ,110 ,N'U1 123' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000365' ,N'Karmulka' ,90 ,N'U1 124' ,N'Die Waffe kann nur erbeutet oder als Besonderer Besitz eines Owangi-Kriegers zu Spielbeginn gewählt werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000366' ,N'Tepoztopilli' ,75 ,N'U2 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000367' ,N'Macuahuitl,Obsidian' ,140 ,N'U2 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000368' ,N'Macuahuitl, Stahl' ,140 ,N'U2 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000369' ,N'Macuahuitl, Macuahuitl' ,250 ,N'U2 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000370' ,N'Sichelschwert' ,70 ,N'U2 125' ,N'Das Sichelschwert kann einhändig oder zweihändig geführt werden. Bei zweihändiger Führung erhöhen sich die TP um 2 Punkte.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000371' ,N'Armbandwurfmesser' ,8 ,N'U1 123' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000372' ,N'Sichelwurfbeil' ,50 ,N'U2 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000373' ,N'Owangi-Holzschild' ,130 ,N'U1 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000374' ,N'Ishilangu, rund' ,80 ,N'U1 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000375' ,N'Ishilangu, oval' ,140 ,N'U1 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000376' ,N'Chimalli, Pflanzenfasern' ,130 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000377' ,N'Chimalli, Pflanzenfasern einrollbar' ,110 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000378' ,N'Mahuizzoh Chimalli' ,180 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000379' ,N'Hartholzpanzer (Uthuria)' ,220 ,N'U1 124' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000380' ,N'Hartholzhelm' ,160 ,N'U1 124' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000381' ,N'Armschienen, Hartholz ' ,45 ,N'U1 124' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000382' ,N'Beinschienen,Hartholz ' ,80 ,N'U1 124' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000383' ,N'Pflanzenfaserrüstung' ,110 ,N'U1 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000384' ,N'Krokodilpanzer, Leder' ,100 ,N'U1 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000385' ,N'Krokodilpanzer, Knochen ' ,130 ,N'U1 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000386' ,N'Armschienen, Krokodilleder ' ,35 ,N'U1 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000387' ,N'Beinschienen, Krokodilleder ' ,50 ,N'U1 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000388' ,N'Rochenlederrüstung' ,70 ,N'U1 125' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000389' ,N'Hartholzharnisch (Uthuria)' ,500 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000390' ,N'SchwererHartholzharnisch' ,800 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000391' ,N'Hartholzhelm' ,160 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000392' ,N'Schwerer Hartholzhelm ' ,250 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000393' ,N'Sonnenpanzer' ,180 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000394' ,N'Kriegsmaske ' ,60 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000395' ,N'Guereni-Fellumhang' ,200 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000396' ,N'Xamantis-Chitinpanzer' ,120 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000397' ,N'Xamantis-Armschienen ' ,30 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000398' ,N'Xamantis-Beinschienen ' ,50 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000399' ,N'Kipakaba-Lederbekleidung' ,90 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000400' ,N'Raubpflanzen-Ichcahuipilli' ,120 ,N'U2 126' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000401' ,N'Taiaha (Paddelspeer)' ,90 ,N'U1 122' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000402' ,N'Taiaha (Paddelspeer/ Paddelende)' ,90 ,N'U1 122' ,NULL ,NULL ,NULL);
UPDATE [Ausrüstung] SET [Bemerkung] = N'Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000056';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000057';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000058';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000073';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000074';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000075';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Der Magierstab gilt als magische Waffe und ist damit in der Lage, gegen profane Waffen unempfindliche oder weniger empfindliche Wesenheiten zu verletzen. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000076';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Beachte die Regelung im Arsenal. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000132';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000117';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Ein Stich wird auf das Talent Speere (AT erschwert um 4) abgelegt ein erfolgreicher Einsatz des Dorns richtet 1W6+5 TP an. Schläge der Waffe können von Dolchen und Fechtwaffen nicht pariert werden. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000101';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000102';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Vom Pferderücken aus gegen Fußkämpfer, richtet ein Robbentöter zwei zusätzliche TP an. Die Rückseite der Klinge ist meist bewusst stumpf gehalten, um bei Genickschlägen das Fell nicht zu verletzen. Die Manöver Stumpfer Schlag und Betäubungsschlag sind jeweils um 2 Punkte erleichtert. Bei der Waffenmeister-SF muss es heißen: »Ein Waffenmeister (Säbel) kann auch den Robbentöter einsetzen, ... ' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000103';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000107';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben. Siehe die Regelung in den Errata.' WHERE [AusrüstungGUID]='00000000-0000-0000-0001-000000000108';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000013';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0002-000000000017';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Kälterüstungsschutz: 3, BE: 0, Preis je nach Material und Qualität' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000021';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Preis je nach Material und Qualität' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000027';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Preis je nach Material und Qualität' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000047';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Pelzweste: Kälterüstungsschutz 3, BE:1, Preis je nach Material und Qualität' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000048';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Bei Paraden gegen Waffen gilt ein Kämpfer mit einer Hummerschere als unbewaffnet. Ein Kämpfer mit einer Hummerschere hat einen Zonen-RS von 2. Die Werte für Gewicht und BF sind geraten. Diese Waffe kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0003-000000000020';
UPDATE [Ausrüstung] SET [Bemerkung] = N'Diese Rüstung kann man käuflich nicht erwerben.' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000001';

--Ausrüstung_Setting
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000342' ,'00000000-0000-0000-5e77-000000000005' ,30 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000343' ,'00000000-0000-0000-5e77-000000000005' ,30 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000344' ,'00000000-0000-0000-5e77-000000000005' ,30 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000345' ,'00000000-0000-0000-5e77-000000000005' ,30 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000346' ,'00000000-0000-0000-5e77-000000000005' ,10 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000347' ,'00000000-0000-0000-5e77-000000000005' ,20 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000348' ,'00000000-0000-0000-5e77-000000000005' ,10 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000349' ,'00000000-0000-0000-5e77-000000000005' ,20 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000350' ,'00000000-0000-0000-5e77-000000000005' ,10 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000351' ,'00000000-0000-0000-5e77-000000000005' ,20 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000352' ,'00000000-0000-0000-5e77-000000000005' ,10 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000353' ,'00000000-0000-0000-5e77-000000000005' ,20 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000354' ,'00000000-0000-0000-5e77-000000000005' ,30 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000355' ,'00000000-0000-0000-5e77-000000000005' ,200 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000356' ,'00000000-0000-0000-5e77-000000000005' ,150 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000357' ,'00000000-0000-0000-5e77-000000000005' ,10 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000358' ,'00000000-0000-0000-5e77-000000000005' ,150 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000359' ,'00000000-0000-0000-5e77-000000000005' ,50 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000360' ,'00000000-0000-0000-5e77-000000000005' ,40 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000361' ,'00000000-0000-0000-5e77-000000000005' ,60 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000362' ,'00000000-0000-0000-5e77-000000000005' ,90 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000363' ,'00000000-0000-0000-5e77-000000000005' ,25 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000364' ,'00000000-0000-0000-5e77-000000000005' ,90 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000365' ,'00000000-0000-0000-5e77-000000000005' ,0 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000366' ,'00000000-0000-0000-5e77-000000000005' ,60 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000367' ,'00000000-0000-0000-5e77-000000000005' ,90 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000368' ,'00000000-0000-0000-5e77-000000000005' ,120 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000369' ,'00000000-0000-0000-5e77-000000000005' ,160 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000370' ,'00000000-0000-0000-5e77-000000000005' ,200 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000371' ,'00000000-0000-0000-5e77-000000000005' ,25 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000372' ,'00000000-0000-0000-5e77-000000000005' ,50 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000373' ,'00000000-0000-0000-5e77-000000000005' ,50 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000374' ,'00000000-0000-0000-5e77-000000000005' ,70 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000375' ,'00000000-0000-0000-5e77-000000000005' ,85 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000376' ,'00000000-0000-0000-5e77-000000000005' ,70 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000377' ,'00000000-0000-0000-5e77-000000000005' ,80 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000378' ,'00000000-0000-0000-5e77-000000000005' ,90 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000379' ,'00000000-0000-0000-5e77-000000000005' ,150 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000380' ,'00000000-0000-0000-5e77-000000000005' ,100 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000381' ,'00000000-0000-0000-5e77-000000000005' ,60 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000382' ,'00000000-0000-0000-5e77-000000000005' ,60 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000383' ,'00000000-0000-0000-5e77-000000000005' ,60 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000384' ,'00000000-0000-0000-5e77-000000000005' ,70 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000385' ,'00000000-0000-0000-5e77-000000000005' ,90 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000386' ,'00000000-0000-0000-5e77-000000000005' ,25 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000387' ,'00000000-0000-0000-5e77-000000000005' ,25 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000388' ,'00000000-0000-0000-5e77-000000000005' ,250 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000389' ,'00000000-0000-0000-5e77-000000000005' ,350 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000390' ,'00000000-0000-0000-5e77-000000000005' ,500 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000391' ,'00000000-0000-0000-5e77-000000000005' ,100 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000392' ,'00000000-0000-0000-5e77-000000000005' ,150 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000393' ,'00000000-0000-0000-5e77-000000000005' ,400 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000394' ,'00000000-0000-0000-5e77-000000000005' ,80 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000395' ,'00000000-0000-0000-5e77-000000000005' ,5000 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000396' ,'00000000-0000-0000-5e77-000000000005' ,2000 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000397' ,'00000000-0000-0000-5e77-000000000005' ,600 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000398' ,'00000000-0000-0000-5e77-000000000005' ,600 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000399' ,'00000000-0000-0000-5e77-000000000005' ,1200 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000400' ,'00000000-0000-0000-5e77-000000000005' ,200 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000401' ,'00000000-0000-0000-5e77-000000000005' ,50 ,N'WIST' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000402' ,'00000000-0000-0000-5e77-000000000005' ,50 ,N'WIST' ,NULL);

 --Fernkampfwaffe
 INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000371' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,NULL ,0 ,NULL ,NULL ,0 ,2 ,4 ,6 ,8 ,15 ,1 ,0 ,0 ,0 ,-1 ,NULL);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000372' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,3 ,0 ,NULL ,NULL ,0 ,2 ,5 ,10 ,15 ,25 ,1 ,1 ,0 ,0 ,-1 ,NULL);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000401' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,12 ,3 ,0 ,5 ,10 ,15 ,25 ,40 ,1 ,0 ,0 ,-1 ,-2 ,NULL);

 --Fernkampfwaffe_Talent
 INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000371' ,'00000000-0000-0000-007a-000000000380');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000372' ,'00000000-0000-0000-007a-000000000379');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000372' ,'00000000-0000-0000-007a-000000000380');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000401' ,'00000000-0000-0000-007a-000000000042');

 --Handelsgut
 INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003477' ,N'Borunga' ,NULL ,NULL ,N'Gifte' ,N'Waffengift' ,N'giftige Teile: unreife Samen mit weicher Schale' ,N'U1 126');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003478' ,N'Utharshauch (Mugu-Maguro)' ,NULL ,NULL ,N'Gifte' ,N'Atemgift' ,N'giftige Teile: Ranken' ,N'U1 126');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003479' ,N'Falsches Blut (Calados)' ,NULL ,NULL ,N'Gifte' ,N'Blutgift' ,N'Um das Opfer dem eigenen Willen zu unterwerfen, muss das Blut des Caladirs mit dem Blut des Herstellers oder eines anderen „Blutsherrn" vermischt werden. Dadurch kommt es zwangläufig einer Verdünnung, weshalb das Gift außerhalb des Blutwurmesschwächer, wenngleich immer noch sehr wirkungsvoll ist.' ,N'U1 126');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003480' ,N'Grüne Pause (Natanko)' ,NULL ,NULL ,N'Gifte' ,N'Einnahmegift, Waffengift' ,NULL ,N'U1 126');

 --Handelsgut_Setting
 INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003477' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003478' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003479' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003480' ,'00000000-0000-0000-5e77-000000000005' ,NULL ,NULL);

 --Rüstung
 INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000379' ,NULL ,1 ,NULL ,0 ,5 ,4 ,4 ,0 ,0 ,0 ,0 ,2 ,1 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000380' ,NULL ,1 ,N'z' ,4 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000381' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,2 ,2 ,0 ,0 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000382' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000383' ,NULL ,0 ,NULL ,0 ,2 ,2 ,2 ,2 ,2 ,1 ,1 ,1 ,1 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000384' ,NULL ,0 ,NULL ,0 ,3 ,3 ,2 ,0 ,0 ,0 ,0 ,1 ,1 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000385' ,NULL ,0 ,N'z' ,0 ,4 ,5 ,4 ,0 ,0 ,0 ,0 ,2 ,2 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000386' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000387' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000388' ,NULL ,1 ,NULL ,0 ,2 ,2 ,2 ,1 ,1 ,1 ,1 ,1 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000389' ,NULL ,1 ,NULL ,0 ,5 ,5 ,5 ,3 ,3 ,3 ,3 ,3 ,2 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000390' ,NULL ,0 ,NULL ,0 ,6 ,6 ,6 ,4 ,4 ,4 ,4 ,4 ,4 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000391' ,NULL ,1 ,N'z' ,4 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000392' ,NULL ,0 ,N'z' ,6 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000393' ,NULL ,0 ,NULL ,0 ,6 ,4 ,5 ,0 ,0 ,0 ,0 ,3 ,3 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000394' ,NULL ,0 ,N'z' ,3 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000395' ,NULL ,1 ,NULL ,3 ,2 ,2 ,0 ,2 ,2 ,2 ,2 ,1 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000396' ,NULL ,1 ,NULL ,2 ,5 ,5 ,5 ,0 ,0 ,0 ,0 ,3 ,2 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000397' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,4 ,4 ,0 ,0 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000398' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,0 ,0 ,4 ,4 ,0 ,0 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000399' ,NULL ,1 ,NULL ,0 ,3 ,2 ,2 ,2 ,2 ,2 ,2 ,2 ,1 ,1);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000400' ,NULL ,0 ,NULL ,0 ,3 ,3 ,3 ,3 ,3 ,2 ,2 ,2 ,2 ,1);

 --Schild
 INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000373' ,N'groß' ,N'S' ,-1 ,3 ,-1 ,4);
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000374' ,N'groß' ,N'S' ,-1 ,3 ,0 ,5);
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000375' ,N'sehr groß' ,N'S' ,-1 ,4 ,-1 ,5);
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000376' ,N'groß' ,N'S' ,-1 ,4 ,0 ,2);
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000377' ,N'groß' ,N'S' ,-1 ,4 ,0 ,4);
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000378' ,N'groß' ,N'S' ,-3 ,5 ,-3 ,1);

--Waffe
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000342' ,6 ,1 ,3 ,0 ,11 ,4 ,-1 ,0 ,-2 ,6 ,50 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000343' ,6 ,1 ,2 ,0 ,11 ,4 ,-1 ,0 ,-2 ,8 ,50 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000344' ,6 ,1 ,4 ,0 ,11 ,4 ,-1 ,-1 ,-3 ,4 ,50 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000345' ,6 ,1 ,2 ,0 ,11 ,4 ,-1 ,0 ,-2 ,12 ,50 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000346' ,6 ,1 ,1 ,0 ,11 ,3 ,0 ,0 ,-2 ,5 ,40 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000347' ,6 ,1 ,2 ,0 ,11 ,3 ,0 ,0 ,-2 ,3 ,60 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000348' ,6 ,1 ,2 ,0 ,11 ,3 ,0 ,-1 ,-3 ,3 ,40 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000349' ,6 ,1 ,3 ,0 ,11 ,3 ,0 ,-1 ,-3 ,1 ,60 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000350' ,6 ,1 ,2 ,0 ,11 ,3 ,0 ,-1 ,-3 ,3 ,40 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000351' ,6 ,1 ,3 ,0 ,11 ,3 ,0 ,-1 ,-3 ,1 ,60 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000352' ,6 ,1 ,0 ,0 ,11 ,3 ,0 ,0 ,-2 ,11 ,40 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000353' ,6 ,1 ,1 ,0 ,11 ,3 ,0 ,0 ,-2 ,9 ,60 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000354' ,6 ,1 ,2 ,0 ,11 ,4 ,0 ,0 ,0 ,0 ,20 ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000355' ,6 ,1 ,1 ,0 ,12 ,4 ,1 ,-1 ,-2 ,2 ,NULL ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000356' ,6 ,1 ,2 ,0 ,12 ,3 ,0 ,0 ,-2 ,3 ,20 ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000357' ,6 ,1 ,1 ,0 ,12 ,3 ,0 ,0 ,-1 ,4 ,40 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000358' ,6 ,1 ,5 ,0 ,13 ,4 ,0 ,0 ,0 ,7 ,130 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000359' ,6 ,1 ,3 ,0 ,11 ,3 ,-1 ,0 ,-2 ,5 ,50 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000360' ,6 ,1 ,3 ,0 ,12 ,3 ,0 ,-1 ,-2 ,8 ,50 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000361' ,6 ,1 ,3 ,0 ,11 ,3 ,-1 ,0 ,-2 ,4 ,90 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000362' ,6 ,2 ,2 ,0 ,13 ,2 ,-2 ,-1 ,-3 ,5 ,110 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000363' ,6 ,1 ,3 ,0 ,11 ,4 ,-1 ,-1 ,-2 ,5 ,40 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000364' ,6 ,1 ,4 ,0 ,13 ,3 ,-1 ,0 ,-2 ,4 ,75 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000365' ,6 ,1 ,3 ,0 ,11 ,4 ,0 ,0 ,-1 ,2 ,90 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000366' ,6 ,1 ,6 ,0 ,13 ,3 ,-1 ,0 ,-3 ,7 ,200 ,N'S' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000367' ,6 ,1 ,4 ,0 ,13 ,2 ,-1 ,0 ,-2 ,4 ,80 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000368' ,6 ,1 ,5 ,0 ,13 ,2 ,-1 ,0 ,-1 ,2 ,80 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000369' ,6 ,2 ,1 ,0 ,13 ,2 ,-3 ,-2 ,-4 ,6 ,170 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000370' ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,0 ,-2 ,3 ,90 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000401' ,6 ,1 ,4 ,0 ,12 ,4 ,0 ,0 ,-1 ,5 ,180 ,N'S' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000402' ,6 ,1 ,2 ,0 ,13 ,2 ,0 ,0 ,-1 ,5 ,180 ,N'N' ,0);

 --Waffe_Talent
 INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000342' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000343' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000344' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000345' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000346' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000347' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000348' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000349' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000350' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000351' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000352' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000353' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000354' ,'00000000-0000-0000-007a-000000000203');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000355' ,'00000000-0000-0000-007a-000000000203');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000356' ,'00000000-0000-0000-007a-000000000203');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000357' ,'00000000-0000-0000-007a-000000000203');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000358' ,'00000000-0000-0000-007a-000000000073');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000358' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000358' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000359' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000360' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000361' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000362' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000363' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000364' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000365' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000365' ,'00000000-0000-0000-007a-000000000227');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000365' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000366' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000367' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000367' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000368' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000368' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000369' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000369' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000370' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000370' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000401' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000402' ,'00000000-0000-0000-007a-000000000247');
