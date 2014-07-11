--Daten-Update: Uthuria / Myranor
--Setting
INSERT INTO [Setting] (  [SettingGUID],  [Name],  [Aktiv]) VALUES ('00000000-0000-0000-5e77-000000000005' ,N'Uthuria' ,0);

--Talente
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000409' ,N'Sprachen Kennen (Xoxota)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Uthuria' ,N'U2 94');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000410' ,N'Lesen/Schreiben (Xo''Artal-Glyphen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Uthuria' ,N'U2 94');

 --VorNachteile
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
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Blind' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000535' ,N'WnM 124');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unvermögend für Beschwörungsdisziplin (Invokation)' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000536' ,N'WnM 137');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unvermögend für Beschwörungsdisziplin (Inspiration) ' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000537' ,N'WnM 137');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unvermögend für Beschwörungsdisziplin (Instruktion)' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000538' ,N'WnM 137');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unvermögend für Beschwörungsdisziplin (Influktion)' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000539' ,N'WnM 137');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Greifschwanz' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Myranor' ,'920f77e0-09d0-4d04-807a-5e19dfce8752' ,N'WnM 121');

 --Sonderfertigkeiten
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
