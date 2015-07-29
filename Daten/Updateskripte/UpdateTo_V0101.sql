-- Korrektur Basardaten: Mehrzahl zu Einzahl
ALTER TABLE [Handelsgut] ADD [Verpackungseinheit] float NULL;
UPDATE [Handelsgut] SET [Gewicht] = 0.1, [Verpackungseinheit] = 10, [Name] = 'Pergament' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001950';
UPDATE [Handelsgut_Setting] SET [Preis] = '1,2' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001950';
UPDATE [Handelsgut] SET [Gewicht] = 0.1, [Verpackungseinheit] = 10, [Name] = 'Gänsekiel/Griffel' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001952';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,4' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001952';
UPDATE [Handelsgut] SET [Gewicht] = 0.1, [Verpackungseinheit] = 10, [Name] = 'Federkiel' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002966';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,1' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002966';
UPDATE [Handelsgut] SET [Gewicht] = 3.333, [Verpackungseinheit] = 6, [Name] = 'Ei' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001431';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,015' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001431';
UPDATE [Handelsgut] SET [Gewicht] = 5, [Verpackungseinheit] = 10, [Name] = 'Kletterhaken' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000000133';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,5' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000000133';
UPDATE [Handelsgut] SET [Gewicht] = 4, [Verpackungseinheit] = 10, [Name] = 'Kletterhaken' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002865';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,2' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002865';
UPDATE [Handelsgut] SET [Gewicht] = 0.333, [Verpackungseinheit] = 3, [Name] = 'Würfel, beschwert' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000000134';
UPDATE [Handelsgut_Setting] SET [Preis] = '1,667' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000000134';
UPDATE [Handelsgut] SET [Gewicht] = 6.667, [Verpackungseinheit] = 3, [Name] = 'Jonglierball' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001303';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,5' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001303';
UPDATE [Handelsgut] SET [Gewicht] = 6.667, [Verpackungseinheit] = 3, [Name] = 'Jonglierball' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002973';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,267' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002973';
UPDATE [Handelsgut] SET [Gewicht] = 15, [Verpackungseinheit] = 3, [Name] = 'Jonglierkeule' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001304';
UPDATE [Handelsgut_Setting] SET [Preis] = '1,333' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001304';
UPDATE [Handelsgut] SET [Gewicht] = 0.4, [Verpackungseinheit] = 5, [Name] = 'Kohlestift' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001953';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,01' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001953';
UPDATE [Handelsgut] SET [Gewicht] = 0.4, [Verpackungseinheit] = 5, [Name] = 'Kohlestift' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002840';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,1' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002840';
UPDATE [Handelsgut] SET [Gewicht] = 1, [Verpackungseinheit] = 10, [Name] = 'Wundverband' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002011';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,1' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002011';
UPDATE [Handelsgut] SET [Gewicht] = 0.333, [Verpackungseinheit] = 3, [Name] = 'Würfel' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002989';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,333' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002989';
UPDATE [Handelsgut] SET [Gewicht] = 0.333, [Verpackungseinheit] = 3, [Name] = 'Würfel, Bein' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001332';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,667' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001332';
UPDATE [Handelsgut] SET [Gewicht] = 60, [Verpackungseinheit] = 2, [Name] = 'Zeltstange' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002016';
UPDATE [Handelsgut_Setting] SET [Preis] = '1,5' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000002016';
UPDATE [Handelsgut] SET [Gewicht] = NULL, [Verpackungseinheit] = 10, [Name] = 'Zigarre' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001831';
UPDATE [Handelsgut_Setting] SET [Preis] = '1' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001831';

-- Setting: Tharun
INSERT INTO [Setting] (  [SettingGUID],  [Name],  [Aktiv]) VALUES ('00000000-0000-0000-5e77-000000000006' ,N'Tharun' ,0);

-- Tharun, Myranor: Sprachen
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000413' ,N'Lesen/Schreiben (Bramschoromk oder Baramun-Keilschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor' ,N'Myranor (HC) 212');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000414' ,N'Lesen/Schreiben (Kalshinish oder Shingwanische Knotenschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor' ,N'Myranor (HC) 212');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000415' ,N'Lesen/Schreiben (Eupherban-Codec)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor' ,N'Myranor (HC) 212');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000416' ,N'Lesen/Schreiben (AhMaGao)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,N'Myranor' ,N'Myranor (HC) 212');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000417' ,N'Sprachen Kennen (Dagathimisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'B' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000418' ,N'Sprachen Kennen (Krakonisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'B' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000434' ,N'Sprachen Kennen (Hashandrisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000435' ,N'Sprachen Kennen (Kuumisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000436' ,N'Sprachen Kennen (Conossi)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000437' ,N'Sprachen Kennen (Lan)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000438' ,N'Sprachen Kennen (Jütung)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000439' ,N'Sprachen Kennen (Nesutisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000440' ,N'Sprachen Kennen (Thuarisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000441' ,N'Sprachen Kennen (Vailisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000442' ,N'Sprachen Kennen (Tharunisch)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000443' ,N'Sprachen Kennen (Verkehrssprache)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000444' ,N'Sprachen Kennen (Heilige Sprache)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000445' ,N'Lesen/Schreiben (Tharunische Zeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000446' ,N'Lesen/Schreiben (Memonhabitische Bilderschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000447' ,N'Lesen/Schreiben (Gesten der Azarai des Ojo''Sombri)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000448' ,N'Lesen/Schreiben (Lokale Geheimschrift)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Tharun' ,N'WnT 41');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000452' ,N'Sprachen Kennen (Iaril)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000454' ,N'Sprachen Kennen (Hippocampir-Zeichensprache)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000455' ,N'Sprachen Kennen (Lied-der-Gemeinschaft)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000456' ,N'Sprachen Kennen (Nequaner-Zeichensprache)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000457' ,N'Sprachen Kennen (Nequaner-Wasserschall-Code)' ,7 ,N'KL' ,N'IN' ,N'CH' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000458' ,N'Lesen/Schreiben (Lamahrische Glyphen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
INSERT INTO [Talent] (  [TalentGUID],  [Talentname],  [TalentgruppeID],  [Eigenschaft1],  [Eigenschaft2],  [Eigenschaft3],  [Talenttyp],  [eBE],  [Spezialisierungen],  [Voraussetzungen],  [Steigerung],  [WikiLink],  [Untergruppe],  [Setting],  [Literatur]) 
 VALUES ('00000000-0000-0000-007a-000000000460' ,N'Lesen/Schreiben (Wasserschallzeichen)' ,7 ,N'KL' ,N'KL' ,N'FF' ,N'Spezial' ,NULL ,NULL ,NULL ,N'A' ,NULL ,NULL ,N'Myranor' ,N'MM 204');
UPDATE [Talent] SET [Literatur] = N'WnM 180 / MM 204' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000388';
UPDATE [Talent] SET [Setting] = N'Aventurien, Dunkle Zeiten, Myranor' ,[Literatur] = N'WdS 30, 31, 32 / WnM 182 / OiC 45 / MM 204 / MM 204' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000132';
UPDATE [Talent] SET [Literatur] = N'Myranor 212 / WnM 182 / MM 204' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000306';
UPDATE [Talent] SET [Literatur] = N'WdS 30, 32 / Myranor 212 / WnM 181 / OiC 45 / MM 204' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000328';
UPDATE [Talent] SET [Literatur] = N'WnM 181 / MM 204' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000399';
UPDATE [Talent] SET [Literatur] = N'WnM 182 / MM 204' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000403';
UPDATE [Talent] SET [Literatur] = N'WnM 182 / MM 204' WHERE [TalentGUID]='00000000-0000-0000-007a-000000000404';

-- Tharun: Ausrüstung
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000403' ,N'Faustmesser' ,15 ,N'WnT 65' ,N'Mit einem Faustmesser können die Schläge von Kettenwaffen Zweihandflegeln, Zweihand- Hiebwaffen und Zweihandschwertern oder –säbeln nicht pariert werden. Das Faustmesser erzielt im waffenlosen Kampf echte TP. Mit dem Faustmesser sind folgende waffenlose Manöver möglich: Gerade und Schwinger.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000404' ,N'Schwere Hand' ,30 ,N'WnT 65' ,N'Mit einer Schweren Hand können die Schläge von Kettenwaffen (mit Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln, Zweihand- Hiebwaffen und Zweihandschwertern oder -säbeln nicht pariert werden. Als Parierwaffe geführt, gilt für die Schwere Hand WM 0/+1.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000405' ,N'Schwere Hand (als Parierwaffe)' ,45 ,N'WnT 65' ,N'Mit einer Schweren Hand können die Schläge von Kettenwaffen (mit Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln, Zweihand- Hiebwaffen und Zweihandschwertern oder -säbeln nicht pariert werden. Als Parierwaffe geführt, gilt für die Schwere Hand WM 0/+1.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000406' ,N'Paradet' ,45 ,N'WnT 65' ,N'Mit dem Paradet können die Schläge von Kettenwaffen (mit Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln und Zweihand-Hiebwaffen nicht pariert werden. Mit dem Paradet können die Manöver Befreiungsschlag und Niederwerfen nicht durchgeführt werden. Es gibt auch keinen TPBonus vom Rücken eines Reittieres. Es ist möglich mit dem Paradet einen Gegner zu entwaffnen, beim Entwaffnen ist die PA um 2 Punkte erleichtert. Es ist auch möglich, eine gegnerische Klingenwaffe zu zerbrechen; die PA hierfür ist um 2 Punkte erleichtert, die KK-Probe um 4 Punkte. Als Parierwaffe geführt, gilt für das Paradet WM −1/+3.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000407' ,N'Kurzer Dorn' ,35 ,N'WnT 67' ,N'Mit einem Kurzen Dorn können die Schläge von Kettenwaffen (mit Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwertern oder -säbeln nicht pariert werden. Als Parierwaffe geführt, gilt für den Kurzen Dorn WM 0/+1.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000408' ,N'Langdorn' ,50 ,N'WnT 67' ,N'Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet der Lange Dorn zwei zusätzliche TP an. Die Paradeeinschränkungen für Fechtwaffen entfallen für den Langdorn.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000409' ,N'Kreuzdorn' ,140 ,N'WnT 67' ,N'Der Schlag eines Kreuzdorns kann mit Fechtwaffen und Dolchen nicht pariert werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000410' ,N'Halbdorn' ,110 ,N'WnT 67, 68' ,N'Ein Halbdorn kann ab KK 15 einhändig (mit dem Talent Schwerter) geführt werden; in diesem Fall gilt TP/KK 13/4, DK N. Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet der Halbe Dorn zusätzlich 2 TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000411' ,N'Ritualdorn' ,20 ,N'WnT 68' ,N'"Mit einem Ritualdorn können die Schläge von Kettenwaffen (mit Ausnahme von Geißel und Neunschwänziger), Zweihandflegeln, Zweihand-Hiebwaffen werden.
und Zweihandschwertern oder -säbeln nicht pariert"' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000412' ,N'Hashandrische Schwinge' ,80 ,N'WnT 68' ,N'Die Hashandrische Schwinge kann ab KK 15 einhändig (mit dem Talent Schwerter) geführt werden. In diesem Fall erhöht sich TP/KK auf 12/4 und es gilt die Distanzklasse N. Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet eine Schwinge zwei zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000413' ,N'Ilshiitische Langschwinge' ,100 ,N'WnT 68' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000414' ,N'Lanianische Kurzschwinge' ,70 ,N'WnT 69' ,N'Vom Rücken eines Reittieres gegen Fußkämpfereingesetzt, richtet die Kurzschwinge zwei zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000415' ,N'Halbschwinge' ,60 ,N'WnT 69' ,N'Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet die Kurzschwinge zwei zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000416' ,N'Thuarische Dunja' ,90 ,N'WnT 69' ,N'Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet die Dunja zwei zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000417' ,N'Große thuarische Dunja' ,130 ,N'WnT 69' ,N'Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet die Große thuarische Dunja zwei zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000418' ,N'Kushra' ,80 ,N'WnT 69, 71' ,N'Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet die Kushra zwei zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000419' ,N'Große Kushra' ,150 ,N'WnT 71' ,N'Ein Angriff mit einer Großen Kushra kann von Dolchen nicht pariert werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000420' ,N'Memonhabitische Sichelschwinge' ,80 ,N'WnT 71' ,N'Mit der Sichelschwinge sind Manöver zum Entwaffnen um 2 Punkte erleichtert und außerdem ist das Manöver Umreißen möglich. Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet die Sichelschwinge zwei zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000421' ,N'Jüitische Doppelschwinge' ,120 ,N'WnT 71' ,N'Ein Waffenmeister (Jüitische Doppelschwinge) erhält einen 1 Punkt höheren INI-Bonus, er kann beim Manöver Niederwerfen die Erschwernis um 4 Punkte, bei Entwaffnen, Hammerschlag und Sturmangriff um je 2 Punkte vermindern. Als Anderthalbhänder geführt kann er den Ausfall ohne Erschwernis initiieren, als Zweihänder die Erschwernis beim Schildspalter um 2 Punkte senken. Für diese Waffenmeisterschaft sind GE 15 und KK 17 erforderlich.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000422' ,N'Conossische Zackenschwinge' ,90 ,N'WnT 71, 73' ,N'"Die Zackenschwinge kann ab KK 15 einhändig (mit dem Talent Schwerter) geführt werden. In diesem
Fall erhöht sich TP/KK auf 13/3 und es gilt die Distanzklasse N. Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet eine Schwinge zwei zusätzliche TP an."' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000423' ,N'Flammar' ,150 ,N'WnT 73' ,N'Der Schlag eines Flammars kann mit Fechtwaffen und Dolchen nicht pariert werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000424' ,N'Spälter' ,240 ,N'WnT 73, 74' ,N'Schläge eines Spälters können mit Dolchen oder Fechtwaffen jedweder Art nicht pariert werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000425' ,N'Richterstab' ,120 ,N'WnT 74' ,N'Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet der Richterstab 2 zusätzliche TP an.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000426' ,N'Kuumischer Richterstab' ,180 ,N'WnT 74' ,N'Ein Angriff mit einem kuumischen Richterstab kann mit Dolchen und Fechtwaffen nicht pariert werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000427' ,N'Langholz' ,150 ,N'WnT 74' ,N'Mit dem Langholz darf (bei Kenntnis der dazugehörigen SF) das Manöver Festnageln eingesetzt werden. Das Langholz kann auch einhändig mit dem Talent Speere eingesetzt werden. Allerdings gilt ein PA-Abzug von 3 Punkten und die Kampfwerte von 1W+4 und TP/KK 13/4.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000428' ,N'Hörner' ,120 ,N'WnT 75' ,N'Mit den Hörnern darf (bei Kenntnis der dazugehörigen SF) das Manöver Festnageln um 2 Punkte erleichtert eingesetzt werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000429' ,N'Reißer' ,200 ,N'WnT 75' ,N'Angriffe mit dem Reißer können mit Dolchen und Fechtwaffen nicht pariert werden. Vom Rücken eines Reittieres gegen Fußkämpfer eingesetzt, richtet ein Reißer zwei zusätzliche TP an. Man kann mit dem Reißer das Manöver Umreißen auch gegen Reiter anwenden, die dem nur mit einer um 8 erschwerten Parade begegnen können (WdS 105).' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000430' ,N'Sturmreiche' ,220 ,N'WnT 75' ,N'Die Werte hier gelten für die Verwendung als Speer im Kampf zu Fuß. Im Reiterkampf wird die Sturmreiche regeltechnisch wie eine Kriegslanze behandelt Die Sturmreiche kann rein theoretisch im Spießgespann eingesetzt werden, allerdings ist dies nicht üblich.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000431' ,N'Drachenreiche' ,500 ,N'WnT 75, 76' ,N'Die Drachenreiche kann nur im Spießgespann geführt werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000432' ,N'Streitbeil' ,110 ,N'WnT 81' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000433' ,N'Doppelaxt' ,150 ,N'WnT 81' ,N'Angriffe mit der Doppelaxt lassen sich nicht mit Dolchen und Fechtwaffen parieren.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000434' ,N'Hornaxt' ,180 ,N'WnT 81' ,N'Angriffe mit dem Hornbeil lassen sich nicht mit Dolchen und Fechtwaffen parieren.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000435' ,N'Drachenkrallen' ,60 ,N'WnT 81' ,N'Drachenkrallen verursachen im Kampf echte Trefferpunkte, keine TP (A). Mit der Hand, die Krallen trägt, kann kein Schild geführt werden. Der Einsatz von Waffen ist aber möglich. Mit den Krallen sind folgende waffenlose Manöver möglich: Doppelschlag, Gerade und Schwinger.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000436' ,N'Fänger' ,60 ,N'WnT 81, 82' ,N'"Der Fänger ist eine reine Angriffswaffe mit der nicht pariert werden kann. Mit dem Fänger ist es möglich, den Gegner zu Entwaffnen oder Umzureißen (dem Gegner ist keine PA erlaubt); außerdem ignoriert er den Paradebonus von Schilden. Der Fänger kann allein oder zusammen mit einer einhändig zu führenden Waffe benutzt werden. Ein Angriff zum Umreißen ist nur möglich, wenn er mit der ersten der beiden Attacken erfolgt. Der Wechsel der ersten AT vom Fänger zur Hauptwaffe und umgekehrt kann in jeder KR erfolgen. Die sonst übliche Einschränkung, dass Umreißen eine zweihändige Waffenführung erfordert, entfällt bei der Verwendung des Fängers, wenn er an Knauf oder Handgelenk befestigt wurde. Es ist möglich, mit einem Fänger ein Opfer zu fesseln in diesem Fall geben die TP die Erschwernis des Entfesseln-
Wurfes des Opfers an, den es nach dem Treffer ablegen muss, um sich wieder zu befreien. Das Opfer darf diese Probe jede Kampfrunde einmal ablegen, bei Misslingen steigt die Schwierigkeit für die folgenden Proben um 1. Um einen solchen Fesseln-Angriff durchzuführen, ist eine AT+8 nötig (weitere Zuschläge erhöhen diese Erschwernis-TP)."' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000437' ,N'Haarringe' ,15 ,N'WnT 82, 83' ,N'siehe Die Kampfkunst der Schwertmeister auf S. 60 Wege nach Tharun).' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000438' ,N'Bluthorn' ,20 ,N'WnT 83' ,N'Bei Paraden gegen einen Bewaffneten gilt ein Kämpfer mit einem Bluthorn als unbewaffnet.  Das Bluthorn wird beim Blutringen üblicherweise mit dem Manöver Versteckte Klinge unter dem Talent Raufen geführt.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000439' ,N'Faustdorn' ,20 ,N'WnT 83' ,N'Mit einem Faustdorn können die Schläge von Kettenwaffen, Zweihandflegeln, Zweihand-Hiebwaffen und Zweihandschwertern oder –säbeln nicht pariert werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000440' ,N'Saipedo' ,80 ,N'WnT 82' ,N'Die Saipedo ist eine reine Angriffswaffe mit der nicht pariert werden kann. Mit der Saipedo ist es möglich, einen Gegner zu Entwaffnen oder Umzureißen (dem Gegner ist keine Parade erlaubt). Außerdem ignoriert der Saipedo den Verteidigungsbonus von Schilden. In der Distanzklasse N erleidet der Träger einer Saipedo einen AT-Malus von 8 Punkten. Im Handgemenge ist der Saipedo nicht zu verwenden. Es ist möglich, mit einer Saipedo ein Opfer zu fesseln, in diesem Fall geben die TP die Erschwernis des Entfesseln-Wurfes des Opfers an, den es nach dem Treffer ablegen muss, um sich wieder zu befreien. Das Opfer darf diese Probe jede Kampfrunde einmal ablegen, bei Misslingen steigt die Schwierigkeit für die folgenden Proben um 1. Um einen solchen Fesseln-Angriff durchzuführen, ist eine AT+8 nötig weitere Zuschläge erhöhen diese Erschwernis-TP).' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000441' ,N'Sichelmesser' ,30 ,N'WnT 83' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000442' ,N'Spieß (als Nahkampfwaffe)' ,80 ,N'WnT 76' ,N'Entsteht beim Angriff mit dem Fangspieß eine Wunde, verhindern die Widerhaken, dass er einfach wieder entfernt werden kann. Solange der Fangspieß im Fleisch des Opfers verbleibt, verursacht er pro KR 1W6 SP (A) und einen Punkt Erschöpfung, wenn sich das Opfer weiterhin bewegt (sprich: kämpft oder flüchtet), ansonsten 1W6 SP pro SR. Die Waffe kann noch im Kampf gewaltsam entfernt werden, dazu ist eine Aktion und eine gelungene KK-Probe erforderlich. Einen in einem anderen Kämpfer steckenden Fangspieß zu ergreifen, erfordert außerdem eine gelungene Raufen-Attacke +3. Die KK-Probe, um ihn durch Reißen am Seil zu entfernen, ist um +5 erschwert und verursacht dabei 2W6+2 SP und eine weitere Wunde. Nach dem Kampf kann er in Ruhe entfernt werden, was eine SR dauert und nach gelungener Probe auf Heilkunde Wunden 1W6−1 SP verursacht, nach misslungener Probe 2W6 SP Die Wirkung mehrerer Fangspieße ist kumulativ.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000443' ,N'Kehrling (als Nahkampfwaffe)' ,50 ,N'WnT 77' ,N'Vom Rücken eines Reittieres als Hiebwaffe gegen Fußkämpfer eingesetzt erhält der Kehrling keine zusätzlichen TP.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000444' ,N'Donnerrohr (als Nahkampfwaffe)' ,200 ,N'WnT 80' ,N'Ein Donnerohr kann auch im Nahkampf als improvisierte Hiebwaffe eingesetzt werden, wenn der Träger keine Zeit hatte, die Waffe zu wechseln. Das Donnerrohr kann ab KK 16 einhändig mit dem Talent Hiebwaffen geführt werden und hat dann TP/KK 14/4.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000445' ,N'Sternchen (als Nahkampfwaffe)' ,10 ,N'WnT 77' ,N' Bei Paraden gegen Waffen gilt ein Sternchenkämpfer als unbewaffnet.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000446' ,N'Spieß' ,80 ,N'WnT 76' ,N'Ein Treffer mit einem Spieß (geworfen oder aus der Schleuder) richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2−2 SP erzeugt. Zudem hat der Treffer die Wirkung eines Angriffs zum Niederwerfen.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000447' ,N'Spieß mit Schleuder' ,40 ,N'WnT 76' ,N'Ein Treffer mit einem Spieß (geworfen oder aus der Schleuder) richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2−2 SP erzeugt. Zudem hat der Treffer die Wirkung eines Angriffs zum Niederwerfen.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000448' ,N'Sternchen' ,10 ,N'WnT 77' ,N'Aus der Hand geworfen können Sternchen als Eisenhagel eingesetzt werden. Das Sternchen kann mit der SF Versteckte Klinge im Nahkampf eingesetzt werden. ' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000449' ,N'Sternchen mit Schleuder' ,10 ,N'WnT 77' ,N'Aus der Hand geworfen können Sternchen als Eisenhagel eingesetzt werden. Das Sternchen kann mit der SF Versteckte Klinge im Nahkampf eingesetzt werden. ' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000450' ,N'Kehrling' ,50 ,N'WnT 77' ,N'Um einen zurückkommenden Kehrling zu fangen, muss dem Fänger eine FF-Probe gelingen. Würfelt er bei dieser Probe eine 20, wird er selbst vom Kehrling getroffen, was 1W6 TP anrichtet.Das Ziel muss mindestens 5 Schritt entfernt sein, die Probe auf Wurfbeil ist um einen Punkt erschwert.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000451' ,N'Rotar' ,60 ,N'WnT 77' ,N'Ein Rotar erfordert eine KK von 14, um korrekt eingesetzt werden zu können, ansonsten gilt er als improvisierte Wurfwaffe. Ein Treffer mit einem Rotar richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2−2 SP erzeugt.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000452' ,N'Kleine Spanne' ,20 ,N'WnT 77' ,N'Ein Treffer mit der Kleinen Spanne richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2−2 SP erzeugt.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000453' ,N'Große Spanne' ,35 ,N'WnT 79' ,N'Ein Treffer mit der Großen Spanne richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2−2 SP erzeugt. Für die Große Spanne ist eine Mindest-KK von 15 erforderlich; darunter gelten alle Ziele als um eine Klasse weiter entfernt, als sie in Wirklichkeit sind; außerdem sinken die TP um 2.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000454' ,N'Mannspanne' ,45 ,N'WnT 79' ,N'Ein Treffer mit der Mannspanne richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2−2 SP erzeugt. Für die Mannspanne ist eine Mindest-KK von 16 erforderlich; darunter gelten alle Ziele als um eine Klasse weiter entfernt, als sie in Wirklichkeit sind; außerdem sinken die TP um 2. Die Mannspanne kann nicht von einem Reiter eingesetzt werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000455' ,N'Fesselsteine' ,20 ,N'WnT 82' ,N'Fesselsteine richten keine echten TP an. Stattdessen ist als TP die Erschwernis auf einen Entfesseln-Wurf genannt, den das Ziel nach einem Treffer ablegen muss. Das Opfer darf diese Probe jede KR ablegen, bei Misslingen steigt die Erschwernis für folgende Proben jedoch um 1.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000456' ,N'Donnerrohr' ,200 ,N'WnT 79' ,N'Aufgrund der Streuwirkung werden bei Schüssen mit dem Donnerrohr alle Ziele als eine Zielgröße größer behandelt. Ein Treffer mit dem Donnerohr gilt automatisch als Angriff zum Niederwerfen, zudem verursacht er automatisch eine Wunde, wenn er beim Opfer mehr als KO/2−2 SP erzeugt. Verursacht ein Treffer mehr als eine Wunde, muss die Trefferzone für jede Wunde einzeln bestimmt werden. Vor der eigentlichen Fernkampfprobe muss der Schütze zum problemlosen Treffen des Zündlochs eine FF-Probe ablegen, bei deren Gelingen die unbeeinflusste FK-Probe folgen kann. Andernfalls ist die FK-Probe um so viele Punkte erschwert, wie bei der FF Probe gefehlt haben. Eine 20 bei der FF-Probe bedeutet, dass erneut das Zündloch mit Zündpulver befüllt werden muss, was sechs Aktionen benötigt. Wenn jemand anderes das Zünden übernimmt, entfällt die FF-Probe. Für das Donnerrohr ist eine Mindest-KK von 15 erforderlich, darunter ist die FK-Probe um einen Punkt für jeden fehlenden KK-Punkt erschwert. Es kommt bei der FKProbe bereits bei einer 19 oder 20 zu einem Patzer, der auf der speziellen Patzertabelle für Feuerwaffen bestimmt wird. Reiter können keine Donnerrohre einsetzen. Ein Donnerohr kann auch im Nahkampf als improvisierte Hiebwaffe eingesetzt werden, wenn der Träger keine Zeit hatte, die Waffe zu wechseln. Das Donnerrohr kann ab KK 16 einhändig mit dem Talent Hiebwaffen geführt werden und hat dann TP/KK 14/4.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000457' ,N'Achteckiger Bronzeschild' ,40 ,N'WnT 86' ,N'Die Verwendung des Achteckigen Bronzeschilds ermöglicht die Verwendung einer Waffe mit der Schildhand. In diesem Fall muss jedoch jede KR entschieden werden, ob der Schild oder die Waffe eingesetzt werden soll. Zweihändig geführte Waffen können mit einem Achteckigen Bronzeschild nicht pariert werden.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000458' ,N'Großes Achteck' ,160 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000459' ,N'Memonhabitischer Lederschild' ,80 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000460' ,N'Jüitischer Lederschild' ,120 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000461' ,N'Laniaischer Reiterschild' ,200 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000462' ,N'Conossischer Rundschild' ,240 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000463' ,N'Rakshasaschild' ,360 ,N'WnT 86' ,N'Ein Rakshasaschild kann nicht von Menschen verwendet werden, die kleiner als zwei Schritt sind und nicht mindestens über eine Körperkraft von 18 verfügen. Er kann seinen Träger bei Deckung um ganze drei Größenklassen verkleinern' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000464' ,N'Spranger' ,80 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000465' ,N'Boqueskinne' ,260 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000466' ,N'Kurze Bronn' ,300 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000467' ,N'Torax' ,320 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000468' ,N'Käfertorax' ,160 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000469' ,N'Große Bronn' ,500 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000470' ,N'Echsenbronn' ,600 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000471' ,N'Eisenrock' ,180 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000472' ,N'Beinlinge ' ,320 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000473' ,N'Arm-Schinter, Metall ' ,60 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000474' ,N'Bein-Schinter, Metall ' ,120 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000475' ,N'Arm-Schinter, Chitin ' ,40 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000476' ,N'Bein-Schinter, Chitin ' ,100 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000477' ,N'Rohren ' ,100 ,N'WnT 86' ,N'Rohren erhöhen die TP (A) beim Waffenlosen Manöver Knie um 2.' ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000478' ,N'Ellen ' ,120 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000479' ,N'Ellen, Chitin ' ,100 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000480' ,N'Handlinge ' ,40 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000481' ,N'Toppet ' ,60 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000482' ,N'Kuppet' ,120 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000483' ,N'Hörnermaske ' ,80 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000484' ,N'Toppet ' ,60 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000485' ,N'Kuppet' ,120 ,N'WnT 86' ,NULL ,NULL ,NULL);
INSERT INTO [Ausrüstung] (  [AusrüstungGUID],  [Name],  [Gewicht],  [Literatur],  [Bemerkung],  [Tags],  [BasisAusrüstung]) 
 VALUES ('00000000-0000-0000-0005-000000000486' ,N'Hörnermaske ' ,80 ,N'WnT 86' ,NULL ,NULL ,NULL);

 -- Tharun: Ausrüstung_Setting
 INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000403' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Kuum' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000404' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000405' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000406' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Ilshi Vailen und Thuara' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000407' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000408' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Lania, Hashandra, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000409' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos, Kuum, Hashandra, Zirraku-Kult' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000410' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'vor allem Jü' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000411' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Azarai' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000412' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Hashandra, Tharun, Kuum' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000413' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Ilshi Vailen, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000414' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Lania, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000415' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Conossos, Lania, Jü, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000416' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Thuara, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000417' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Thuara, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000418' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Thuara, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000419' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Thuara, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000420' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Memonhab, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000421' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Jü, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000422' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Conossos, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000423' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Jü, Thuara, Hashandra' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000424' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos und Kuum, Zirraku-Kult' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000425' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000426' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Kuum' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000427' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000428' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'vor allem Conossos, Zirraku-Kult' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000429' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Kuum, Zirraku-Kult' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000430' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000431' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Thuara' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000432' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000433' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000434' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000435' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000436' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Jü' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000437' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Lania' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000438' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Conossos' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000439' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, selten' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000440' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Ilshi Vailen (nur Rote Kaste)' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000441' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000442' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Kuum' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000443' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Kuum, Memonhab' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000444' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Tharun, Thuara, Conossos, andere Reiche an einzelnen Höfen' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000445' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000446' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Kuum' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000447' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Kuum' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000448' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000449' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000450' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Kuum, Memonhab' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000451' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos und Memonhab' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000452' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Conossos, Lania' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000453' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000454' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, vor allem Ilshi Vailen, Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000455' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche, Brigantai' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000456' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Tharun, Thuara, Conossos, andere Reiche an einzelnen Höfen' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000457' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000458' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Tharun' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000459' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Memonhab' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000460' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Jü' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000461' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Lania' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000462' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'Conossos' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000463' ,'00000000-0000-0000-5e77-000000000006' ,0 ,N'alle Reiche' ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000464' ,'00000000-0000-0000-5e77-000000000006' ,0 ,NULL ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000465' ,'00000000-0000-0000-5e77-000000000006' ,0 ,NULL ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000466' ,'00000000-0000-0000-5e77-000000000006' ,0 ,NULL ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000467' ,'00000000-0000-0000-5e77-000000000006' ,0 ,NULL ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000468' ,'00000000-0000-0000-5e77-000000000006' ,0 ,NULL ,NULL);
INSERT INTO [Ausrüstung_Setting] (  [AusrüstungGUID],  [SettingGUID],  [Preis],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-0005-000000000469' ,'00000000-0000-0000-5e77-000000000006' ,0 ,NULL ,NULL);

 -- Tharun: Fernkampfwaffen
 INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000446' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,4 ,0 ,12 ,3 ,0 ,5 ,10 ,15 ,25 ,40 ,3 ,1 ,0 ,0 ,-1 ,NULL);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000447' ,NULL ,80 ,NULL ,0 ,6 ,1 ,6 ,0 ,12 ,3 ,0 ,5 ,15 ,25 ,35 ,50 ,3 ,1 ,0 ,0 ,-1 ,2);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000448' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,13 ,5 ,0 ,3 ,6 ,10 ,15 ,25 ,1 ,0 ,0 ,0 ,0 ,NULL);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000449' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,5 ,10 ,15 ,25 ,40 ,2 ,1 ,0 ,0 ,-1 ,2);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000450' ,NULL ,NULL ,NULL ,0 ,6 ,2 ,NULL ,0 ,13 ,3 ,0 ,0 ,5 ,15 ,25 ,40 ,NULL ,1 ,1 ,1 ,0 ,NULL);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000451' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,5 ,0 ,13 ,3 ,0 ,5 ,10 ,15 ,25 ,40 ,3 ,1 ,0 ,0 ,-1 ,NULL);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000452' ,NULL ,2 ,NULL ,0 ,6 ,1 ,4 ,0 ,NULL ,NULL ,0 ,5 ,15 ,30 ,50 ,75 ,2 ,1 ,0 ,0 ,-1 ,2);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000453' ,NULL ,2 ,NULL ,0 ,6 ,1 ,5 ,0 ,NULL ,NULL ,0 ,10 ,20 ,35 ,65 ,100 ,3 ,2 ,1 ,0 ,0 ,3);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000454' ,NULL ,4 ,NULL ,0 ,6 ,2 ,4 ,0 ,NULL ,NULL ,0 ,10 ,20 ,40 ,80 ,160 ,3 ,2 ,1 ,0 ,0 ,4);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000455' ,NULL ,NULL ,NULL ,0 ,6 ,1 ,2 ,0 ,NULL ,NULL ,0 ,0 ,5 ,10 ,15 ,25 ,NULL ,0 ,0 ,0 ,-1 ,1);
INSERT INTO [Fernkampfwaffe] (  [FernkampfwaffeGUID],  [Munitionspreis],  [Munitionsgewicht],  [Munitionsart],  [Improvisiert],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [Verwundend],  [RWSehrNah],  [RWNah],  [RWMittel],  [RWWeit],  [RWSehrWeit],  [TPSehrNah],  [TPNah],  [TPMittel],  [TPWeit],  [TPSehrWeit],  [Laden]) 
 VALUES ('00000000-0000-0000-0005-000000000456' ,NULL ,4 ,NULL ,0 ,6 ,3 ,6 ,0 ,NULL ,NULL ,0 ,5 ,10 ,25 ,35 ,50 ,4 ,2 ,0 ,-2 ,-4 ,40);

 -- Tharun: Fernkampfwaffe_Talent
 INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000446' ,'00000000-0000-0000-007a-000000000381');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000447' ,'00000000-0000-0000-007a-000000000232');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000448' ,'00000000-0000-0000-007a-000000000380');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000449' ,'00000000-0000-0000-007a-000000000232');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000450' ,'00000000-0000-0000-007a-000000000379');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000451' ,'00000000-0000-0000-007a-000000000029');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000452' ,'00000000-0000-0000-007a-000000000024');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000453' ,'00000000-0000-0000-007a-000000000024');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000454' ,'00000000-0000-0000-007a-000000000024');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000455' ,'00000000-0000-0000-007a-000000000232');
INSERT INTO [Fernkampfwaffe_Talent] (  [FernkampfwaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000456' ,'00000000-0000-0000-007a-000000000042');

 -- Tharun: Rüstungen
 INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000464' ,NULL ,1 ,NULL ,0 ,2 ,2 ,2 ,1 ,1 ,1 ,1 ,2 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000465' ,NULL ,0 ,NULL ,0 ,3 ,3 ,3 ,3 ,3 ,3 ,3 ,4 ,4 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000466' ,NULL ,1 ,NULL ,0 ,5 ,4 ,4 ,3 ,3 ,0 ,0 ,4 ,3 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000467' ,NULL ,0 ,NULL ,0 ,6 ,5 ,5 ,0 ,0 ,0 ,0 ,4 ,3 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000468' ,NULL ,0 ,NULL ,0 ,3 ,3 ,3 ,0 ,0 ,0 ,0 ,2 ,2 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000469' ,NULL ,1 ,NULL ,0 ,5 ,4 ,4 ,3 ,3 ,4 ,4 ,5 ,4 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000470' ,NULL ,1 ,NULL ,0 ,6 ,5 ,5 ,0 ,0 ,4 ,4 ,5 ,4 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000471' ,NULL ,1 ,N'z' ,0 ,0 ,0 ,2 ,0 ,0 ,3 ,3 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000472' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,0 ,0 ,4 ,4 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000473' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,3 ,3 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000474' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,0 ,0 ,3 ,3 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000475' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,2 ,2 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000476' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000477' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,2 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000478' ,NULL ,0 ,N'z' ,0 ,1 ,1 ,0 ,3 ,3 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000479' ,NULL ,0 ,N'z' ,0 ,1 ,1 ,0 ,2 ,2 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000480' ,NULL ,0 ,N'z' ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000481' ,NULL ,0 ,N'z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000482' ,NULL ,1 ,N'z' ,4 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,2 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000483' ,NULL ,0 ,N'z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000484' ,NULL ,0 ,N'z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000485' ,NULL ,1 ,N'z' ,4 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,2 ,1 ,0);
INSERT INTO [Rüstung] (  [RüstungGUID],  [Gruppe],  [Verarbeitung],  [Art],  [Kopf],  [Brust],  [Rücken],  [Bauch],  [LArm],  [RArm],  [LBein],  [RBein],  [RS],  [BE],  [Steif]) 
 VALUES ('00000000-0000-0000-0005-000000000486' ,NULL ,0 ,N'z' ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0);

 -- Tharun: Schilde
 INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000457' ,N'klein' ,N'SP' ,0 ,1 ,0 ,0);
INSERT INTO [Schild] (  [SchildGUID],  [Größe],  [Typ],  [WMAT],  [WMPA],  [INI],  [BF]) 
 VALUES ('00000000-0000-0000-0005-000000000459' ,N'groß' ,N'S' ,-1 ,3 ,0 ,5);

 -- Tharun: Waffen
 INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000403' ,6 ,1 ,1 ,0 ,12 ,5 ,-2 ,-1 ,-1 ,2 ,20 ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000404' ,6 ,1 ,2 ,0 ,12 ,4 ,0 ,0 ,-1 ,1 ,35 ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000405' ,6 ,1 ,2 ,0 ,11 ,4 ,0 ,0 ,1 ,1 ,50 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000406' ,6 ,1 ,2 ,0 ,11 ,4 ,0 ,0 ,1 ,1 ,50 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000407' ,6 ,1 ,2 ,0 ,12 ,5 ,1 ,0 ,0 ,3 ,35 ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000408' ,6 ,1 ,3 ,0 ,12 ,4 ,1 ,1 ,-1 ,2 ,90 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000409' ,6 ,2 ,3 ,0 ,12 ,2 ,-1 ,1 ,-2 ,2 ,140 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000410' ,6 ,2 ,2 ,0 ,12 ,3 ,0 ,0 ,0 ,2 ,110 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000411' ,6 ,1 ,1 ,0 ,12 ,5 ,0 ,0 ,-1 ,2 ,30 ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000412' ,6 ,1 ,5 ,0 ,11 ,3 ,1 ,0 ,0 ,1 ,1100 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000413' ,6 ,1 ,6 ,0 ,12 ,2 ,0 ,0 ,-1 ,2 ,130 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000414' ,6 ,1 ,4 ,0 ,11 ,4 ,0 ,0 ,1 ,1 ,80 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000415' ,6 ,1 ,3 ,0 ,11 ,5 ,2 ,0 ,0 ,0 ,70 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000416' ,6 ,1 ,5 ,0 ,12 ,3 ,0 ,0 ,0 ,1 ,100 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000417' ,6 ,2 ,4 ,0 ,13 ,2 ,0 ,0 ,-1 ,2 ,135 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000418' ,6 ,1 ,4 ,0 ,12 ,3 ,1 ,0 ,1 ,1 ,100 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000419' ,6 ,2 ,6 ,0 ,13 ,3 ,-2 ,0 ,-3 ,3 ,170 ,N'S' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000420' ,6 ,1 ,4 ,0 ,11 ,4 ,1 ,1 ,0 ,1 ,90 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000421' ,6 ,2 ,3 ,0 ,13 ,2 ,-1 ,0 ,-1 ,2 ,130 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000422' ,6 ,1 ,5 ,0 ,12 ,2 ,1 ,0 ,-1 ,2 ,120 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000423' ,6 ,2 ,4 ,0 ,12 ,3 ,0 ,0 ,-1 ,2 ,160 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000424' ,6 ,4 ,0 ,0 ,16 ,1 ,-2 ,-1 ,-3 ,2 ,140 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000425' ,6 ,1 ,4 ,0 ,11 ,3 ,0 ,0 ,0 ,1 ,80 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000426' ,6 ,2 ,3 ,0 ,14 ,2 ,-2 ,-1 ,-2 ,4 ,170 ,N'S' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000427' ,6 ,1 ,5 ,0 ,12 ,3 ,0 ,0 ,-1 ,4 ,200 ,N'S' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000428' ,6 ,1 ,5 ,0 ,13 ,4 ,0 ,0 ,-2 ,3 ,180 ,N'S' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000429' ,6 ,2 ,5 ,0 ,14 ,3 ,-2 ,-1 ,-3 ,3 ,280 ,N'SP' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000430' ,6 ,1 ,6 ,0 ,14 ,4 ,-2 ,-1 ,-3 ,4 ,400 ,N'P' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000431' ,6 ,4 ,5 ,0 ,22 ,1 ,-4 ,-2 ,-5 ,3 ,500 ,N'P' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000432' ,6 ,1 ,4 ,0 ,13 ,3 ,0 ,0 ,0 ,3 ,80 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000433' ,6 ,2 ,2 ,0 ,14 ,2 ,-1 ,0 ,-2 ,2 ,120 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000434' ,6 ,1 ,6 ,0 ,14 ,2 ,-1 ,-1 ,-2 ,3 ,120 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000435' ,6 ,1 ,2 ,0 ,12 ,4 ,0 ,0 ,-1 ,3 ,NULL ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000436' ,6 ,1 ,1 ,0 ,14 ,5 ,1 ,0 ,NULL ,3 ,250 ,N'NS' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000437' ,6 ,1 ,-2 ,0 ,NULL ,NULL ,-1 ,0 ,0 ,0 ,NULL ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000438' ,6 ,1 ,0 ,0 ,12 ,3 ,-2 ,-1 ,-3 ,5 ,20 ,N'H' ,1);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000439' ,6 ,1 ,2 ,0 ,12 ,5 ,0 ,0 ,-3 ,1 ,25 ,N'H' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000440' ,6 ,1 ,2 ,0 ,14 ,4 ,0 ,0 ,NULL ,3 ,300 ,N'S' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000441' ,6 ,1 ,2 ,0 ,12 ,4 ,-1 ,-1 ,-1 ,4 ,50 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000442' ,6 ,1 ,3 ,0 ,11 ,5 ,-2 ,-1 ,-3 ,4 ,120 ,N'N' ,1);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000443' ,6 ,1 ,2 ,0 ,11 ,4 ,-1 ,0 ,-2 ,3 ,50 ,N'HN' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000444' ,6 ,1 ,5 ,0 ,13 ,3 ,-2 ,-1 ,-3 ,4 ,110 ,N'N' ,0);
INSERT INTO [Waffe] (  [WaffeGUID],  [TPWürfel],  [TPWürfelAnzahl],  [TPBonus],  [AusdauerSchaden],  [TPKKSchwelle],  [TPKKSchritt],  [INI],  [WMAT],  [WMPA],  [BF],  [Länge],  [DK],  [Improvisiert]) 
 VALUES ('00000000-0000-0000-0005-000000000445' ,6 ,1 ,0 ,0 ,12 ,5 ,0 ,-1 ,-2 ,2 ,NULL ,N'H' ,0);

 -- Tharun: Waffe_Talent
 INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000403' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000403' ,'00000000-0000-0000-007a-000000000203');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000404' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000405' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000406' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000406' ,'00000000-0000-0000-007a-000000000227');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000406' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000407' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000407' ,'00000000-0000-0000-007a-000000000038');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000408' ,'00000000-0000-0000-007a-000000000038');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000408' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000409' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000410' ,'00000000-0000-0000-007a-000000000006');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000410' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000411' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000412' ,'00000000-0000-0000-007a-000000000006');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000412' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000413' ,'00000000-0000-0000-007a-000000000006');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000413' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000414' ,'00000000-0000-0000-007a-000000000227');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000414' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000415' ,'00000000-0000-0000-007a-000000000227');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000415' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000416' ,'00000000-0000-0000-007a-000000000227');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000417' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000418' ,'00000000-0000-0000-007a-000000000227');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000419' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000420' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000420' ,'00000000-0000-0000-007a-000000000227');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000421' ,'00000000-0000-0000-007a-000000000006');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000421' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000422' ,'00000000-0000-0000-007a-000000000006');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000422' ,'00000000-0000-0000-007a-000000000237');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000423' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000424' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000424' ,'00000000-0000-0000-007a-000000000386');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000425' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000426' ,'00000000-0000-0000-007a-000000000073');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000426' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000427' ,'00000000-0000-0000-007a-000000000073');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000427' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000428' ,'00000000-0000-0000-007a-000000000073');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000428' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000429' ,'00000000-0000-0000-007a-000000000073');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000429' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000430' ,'00000000-0000-0000-007a-000000000086');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000430' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000431' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000432' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000433' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000434' ,'00000000-0000-0000-007a-000000000384');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000435' ,'00000000-0000-0000-007a-000000000203');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000436' ,'00000000-0000-0000-007a-000000000190');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000437' ,'00000000-0000-0000-007a-000000000203');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000438' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000439' ,'00000000-0000-0000-007a-000000000030');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000440' ,'00000000-0000-0000-007a-000000000190');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000441' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000442' ,'00000000-0000-0000-007a-000000000247');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000443' ,'00000000-0000-0000-007a-000000000070');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000443' ,'00000000-0000-0000-007a-000000000379');
INSERT INTO [Waffe_Talent] (  [WaffeGUID],  [TalentGUID]) 
 VALUES ('00000000-0000-0000-0005-000000000444' ,'00000000-0000-0000-007a-000000000384');

 -- Literatur
 INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000052' ,N'Myranische Monstren' ,N'MyMo' ,N'' ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/149245' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-myranor/regelwerke/41652/myranische-monstren' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000053' ,N'Uhrwerk! Das Magazin Nr. 3' ,N'Uhrwerk3' ,NULL ,0 ,NULL ,NULL ,N'http://uhrwerk-magazin.de/?wpdmdl=428' ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000054' ,N'Uhrwerk! Das Magazin Nr. 4' ,N'Uhrwerk4' ,NULL ,0 ,NULL ,NULL ,N'http://uhrwerk-magazin.de/?wpdmdl=430' ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000055' ,N'Aventurischer Almanach' ,N'AvAlm' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000056' ,N'Das Land an Born und Walsach' ,N'DLBW' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000057' ,N'Land des Schwarzen Bären' ,N'LdsB' ,NULL ,0 ,NULL ,NULL ,NULL ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/42396/das-land-des-schwarzen-baeren-g10' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000058' ,N'Aventurien - Das Lexikon des Schwarzen Auges' ,N'AvLSA' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000059' ,N'Die Kaiserstadt Gareth' ,N'DKG' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000060' ,N'Herz des Reiches' ,N'HdR' ,NULL ,0 ,NULL ,NULL ,NULL ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/30861/herz-des-reiches-g8' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000061' ,N'Verschworene Gemeinschaften' ,N'VG' ,NULL ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/96419/' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/32754/verschworene-gemeinschaften-q8' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000062' ,N'Reich des Horas' ,N'RdH' ,NULL ,0 ,NULL ,NULL ,NULL ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/44896/reich-des-horas-g12' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000063' ,N'Das Reich des Horas' ,N'DRdH' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000064' ,N'In den Dschnungeln Meridianas' ,N'IdDM' ,NULL ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/143161/' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/38178/in-den-dschungeln-meridianas-g1' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000065' ,N'Schattenlande' ,N'Scl' ,NULL ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/121992/' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/44364/schattenlande-g14' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000066' ,N'Geheimnisse der Elfen' ,N'GdE' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000067' ,N'Das Fürstentum Albernia' ,N'DFA' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000068' ,N'Am Großen Fluss' ,N'AGF' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000069' ,N'Die Magische Bibliothek' ,N'DMB' ,NULL ,0 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/148565' ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000070' ,N'Unsterbliche Gier' ,N'A57' ,NULL ,0 ,NULL ,NULL ,NULL ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000071' ,N'Uhrwerk! Das Magazin Nr. 2' ,N'Uhrwerk2' ,N'' ,0 ,NULL ,NULL ,N'http://uhrwerk-magazin.de/?wpdmdl=426' ,NULL ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000072' ,N'Im Herzen der Metropole' ,N'IHdM' ,NULL ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/109213/' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/41112/gareth-kaiserstadt-des-mittelreichs-box?c=1231' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000073' ,N'Goldene Dächer, düstere Gassen' ,N'GDdG' ,NULL ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/109213/' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/41112/gareth-kaiserstadt-des-mittelreichs-box?c=1231' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000074' ,N'Gassenhelden' ,N'Gassenhelden' ,NULL ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/109213/' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/41112/gareth-kaiserstadt-des-mittelreichs-box?c=1231' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
INSERT INTO [Literatur] (  [LiteraturGUID],  [Name],  [Abkürzung],  [Pfad],  [Seitenoffset],  [Größe],  [GrößeKomprimiert],  [UrlPdf],  [UrlPrint],  [Regelsystem],  [Nummer],  [Art],  [Reihe],  [Setting],  [Box]) 
 VALUES ('00000000-0000-0000-0011-000000000075' ,N'Karten und Kopiervorlagen' ,N'GKuK' ,NULL ,1 ,NULL ,NULL ,N'http://www.ulisses-ebooks.de/product/109213/' ,N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/quellenbuecher/41112/gareth-kaiserstadt-des-mittelreichs-box?c=1231' ,0 ,NULL ,NULL ,NULL ,N'Aventurien' ,NULL);
UPDATE [Literatur] SET [UrlPrint] = N'http://www.f-shop.de/rollenspiele/das-schwarze-auge-aventurien/limitiertsondereditionen/59952' WHERE [LiteraturGUID]='00000000-0000-0000-0011-000000000035';

-- Tharun: Sonderfertigkeiten
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001664' ,N'Geländekunde (Algenfelder)' ,0 ,N'Allgemein' ,N'WnT 36 / WdH 276' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001665' ,N'Geländekunde (Meereskundig (Kristallmeer))' ,0 ,N'Allgemein' ,N'WnT 36 / WdH 276' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001666' ,N'Geländekunde (Ödnis)' ,0 ,N'Allgemein' ,N'WnT 36 / WdH 276' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001667' ,N'Meeresorakel' ,0 ,N'Allgemein' ,N'WnT 224, 36' ,N'IN 14, CH 14, TaW Seefahrt 12, Runenfertigkeit (Wasser)10, SF Numinaiwissen, SF Runenorakel');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001668' ,N'Meeressicht' ,0 ,N'Allgemein' ,N'WnT 225, 36' ,N'KL 14, TaW Sinnenschärfe 10, Runenfertigkeit (Wasser) 10, SF Runenmeister (Wasser), SF Numinaiwissen');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001669' ,N'Meeressuche' ,0 ,N'Allgemein' ,N'WnT 224, 36' ,N'KL 14, IN 14, TaW Seefahrt 12, TaW Orientierung 7, Runenfertigkeit (Wasser) 10, SF Numinaiwissen');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001670' ,N'Meereswacht' ,0 ,N'Allgemein' ,N'WnT 224, 225, 36' ,N'IN 15, CH 14, Runenfertigkeit (Wasser)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001671' ,N'Numinaiwissen' ,0 ,N'Allgemein' ,N'WnT 224, 36' ,N'MU 13, IN 13, TaW Seefahrt 9, SF Meereskundig, SF Runenmeditation (Wasser oder Luft)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001672' ,N'Ojo''Sombris Geheimwissen' ,0 ,N'Allgemein' ,N'WnT 37' ,N'KL 12, IN 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001673' ,N'Schleichfahrt' ,0 ,N'Allgemein' ,N'WnT 224, 37' ,N'IN 14, FF 14, TaW Seefahrt 12, Runenfertigkeit (Wasser) 7, SF Numinaiwissen');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001674' ,N'Schnellfahrt' ,0 ,N'Allgemein' ,N'WnT 224, 37' ,N'GE 14, IN 14, TaW Seefahrt 12, Runenfertigkeit (Wasser) 7, SF Numinaiwissen');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001675' ,N'Windfahrt ' ,0 ,N'Allgemein' ,N'WnT 225, 37' ,N'GE 14, IN 14, TaW Seefahrt 12, TaW Segeln 12, SF Runenmeditation (Luft), Runenfertigkeit (Luft) 7, SF Numinaiwissen');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001676' ,N'Meisterliche Schwingenhaltung (Meister der Mutigen Schwinge) (Waffe)' ,0 ,N'Kampf' ,N'WnT 37' ,N'SF Schwingentanz, TaW des Waffentalents 18, Grundversion der entsprechenden SF Schwingenhaltung, SF Kampfgespür');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001677' ,N'Meisterliche Schwingenhaltung (Meister der Hohen Schwinge) (Waffe)' ,0 ,N'Kampf' ,N'WnT 37' ,N'SF Schwingentanz, TaW des Waffentalents 18, Grundversion der entsprechenden SF Schwingenhaltung, SF Kampfgespür');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001678' ,N'Meisterliche Schwingenhaltung (Meister der Tiefen Schwinge) (Waffe)' ,0 ,N'Kampf' ,N'WnT 37' ,N'SF Schwingentanz, TaW des Waffentalents 18, Grundversion der entsprechenden SF Schwingenhaltung, SF Kampfgespür');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001679' ,N'Meisterliche Schwingenhaltung (Meister der Harten Schwinge) (Waffe)' ,0 ,N'Kampf' ,N'WnT 37' ,N'SF Schwingentanz, TaW des Waffentalents 18, Grundversion der entsprechenden SF Schwingenhaltung, SF Kampfgespür');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001680' ,N'Meisterliche Schwingenhaltung (Meister der Harmonischen Schwinge) (Waffe)' ,0 ,N'Kampf' ,N'WnT 37' ,N'SF Schwingentanz, TaW des Waffentalents 18, Grundversion der entsprechenden SF Schwingenhaltung, SF Kampfgespür');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001681' ,N'Schwertmeisterlicher Ausfall' ,0 ,N'Kampf' ,N'WnT 37' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001682' ,N'Schwingentanz (Waffe)' ,0 ,N'Kampf' ,N'WnT 37' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001683' ,N'Schwingenhaltung Mutige Schwinge (Waffe):' ,0 ,N'Kampf' ,N'WnT 37, 52' ,N'SF Schwingentanz, TaW des Waffentalents 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001684' ,N'Schwingenhaltung Hohe Schwinge' ,0 ,N'Kampf' ,N'WnT 37, 52' ,N'SF Schwingentanz, TaW des Waffentalents 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001685' ,N'Schwingenhaltung Tiefe Schwinge' ,0 ,N'Kampf' ,N'WnT 37, 52' ,N'SF Schwingentanz, TaW des Waffentalents 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001686' ,N'Schwingenhaltung Harte Schwinge' ,0 ,N'Kampf' ,N'WnT 37, 52' ,N'SF Schwingentanz, TaW des Waffentalents 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001687' ,N'Schwingenhaltung Harmonische Schwinge' ,0 ,N'Kampf' ,N'WnT 37, 52' ,N'SF Schwingentanz, TaW des Waffentalents 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001688' ,N'Schwingenlauf (Das zehrende Feuer)' ,0 ,N'Kampf' ,N'WnT 37, 53' ,N'Mutige Schwinge, Finte, Wuchtschlag');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001689' ,N'Schwingenlauf (Der zügellose Sturm)' ,0 ,N'Kampf' ,N'WnT 37, 53' ,N'Hohe Schwinge, Finte, Meisterparade');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001690' ,N'Schwingenlauf (Der Sog des Wassers)' ,0 ,N'Kampf' ,N'WnT 37, 53' ,N'Tiefe Schwinge, Finte, Niederwerfen');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001691' ,N'Schwingenlauf (Der stehende Baum)' ,0 ,N'Kampf' ,N'WnT 37, 53' ,N'Harte Schwinge, Kampfgespür, Defensiver Kampfstil');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001692' ,N'Schwingenlauf (Grimmiger Donner)' ,0 ,N'Kampf' ,N'WnT 37, 53' ,N'Gezielter Stich, Wuchtschlag, Hohe Schwinge');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001693' ,N'Schwingenlauf (Rakshasaverteidigung)' ,0 ,N'Kampf' ,N'WnT 37, 53' ,N'Defensiver Kampfstil, Gegenhalten, Harte');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001694' ,N'Waffenloses Manöver (Shin-Zhu (''Ohne Schwert''))' ,0 ,N'Kampf' ,N'WnT 58' ,N'Raufen 10, Ringen 7, Sonderfertigkeiten Schwingentanz, Windmühle');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001695' ,N'Waffenloses Manöver (Conossisches Blutringen)' ,0 ,N'Kampf' ,N'WnT 58, 59' ,N'TaW Ringen 7, Raufen 5');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001696' ,N'Waffenloser Kampfstil (Kuumischer Ringkampf)' ,0 ,N'Kampf' ,N'WnT 59, 60' ,N'TaW Raufen 5, TaW Ringen 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001697' ,N'Waffenloser Kampfstil (Jüitischer Kampftanz)' ,0 ,N'Kampf' ,N'WnT 60' ,N'TaW Raufen 10, TaW Tanzen 8');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001698' ,N'Waffenloser Kampfstil (Memonhabitisches Niederwerfen)' ,0 ,N'Kampf' ,N'WnT 60' ,N'TaW Raufen 7, TaW Ringen 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001699' ,N'Waffenloser Kampfstil (Lanianische Haarringe)' ,0 ,N'Kampf' ,N'WnT 60' ,N'Raufen 10, GE 13');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001700' ,N'Runengefäß' ,0 ,N'Magisch' ,N'WnT 161, 162' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001701' ,N'Runenzauberartefakt' ,0 ,N'Magisch' ,N'WnT 162, 163, 38' ,N'TaW Magiekunde 16, TaW beliebiges Handwerkstalent 10, RfW (Härte) 12, Ritualkenntnis (Runenmagie) 15, SF Runenzauber, SF Runengefäß [beliebige Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001702' ,N'Runenort' ,0 ,N'Magisch' ,N'WnT 114, 38' ,N'SF Runenorakel, SF Runenbesitz, SF Runenmeister [beliebige Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001703' ,N'Meditationsfokus' ,0 ,N'Magisch' ,N'WnT 160' ,N'TaW Magiekunde 10, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001704' ,N'Runenmeditation [Rune]' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001707' ,N'Runenorakel' ,0 ,N'Magisch' ,N'WnT 37' ,N'IN 14, SF Runenbesitz, Runenfertigkeit (beliebige Rune) 3');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001708' ,N'Runensuche' ,0 ,N'Magisch' ,N'WnT 159, 37' ,N'IN 15, SF Runenorakel');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001709' ,N'Zwei Schwingen' ,0 ,N'Kampf' ,N'WnT 50, 37' ,N'GE 16, KK 18, SF Beidhändiger Kampf II oder Mehrhändiger Kampf I');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001710' ,N'Runenzauber' ,0 ,N'Magisch' ,N'WnT 119' ,N'TaW Magiekunde 5, RkW (Runenmagie) 3, Besitz der drei beteiligten Runensteine');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001711' ,N'Kraftbelegung' ,0 ,N'Magisch' ,N'WnT 119' ,N'SF Runenzauber, TaW Magiekunde 13, RkW (Runenmagie) 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001712' ,N'Astralbelegung ' ,0 ,N'Magisch' ,N'WnT 119' ,N'SF Runenzauber, TaW Magiekunde 15, RkW (Runenmagie) 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001713' ,N'Runenzauber lösen ' ,0 ,N'Magisch' ,N'WnT 119' ,N'SF Runenzauber, TaW Magiekunde 15, RkW (Runenmagie) 13');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001714' ,N'Blutopfer' ,0 ,N'Magisch' ,N'WnT 178' ,N'Nachteil Morguaiverfall');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001715' ,N'Blutraub I' ,0 ,N'Magisch' ,N'WnT 178' ,N'MU 13, SF Runenmeditation [Rune], SF Blutopfer, TaW Selbstbeherrschung 10, TaW Anatomie 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001716' ,N'Blutraub II' ,0 ,N'Magisch' ,N'WnT 178' ,N'SF Blutraub [Rune] I, SF Runenmeister [Rune], TaW Selbstbeherrschung 12, TaW Anatomie 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001717' ,N'Blutraub III' ,0 ,N'Magisch' ,N'WnT 179' ,N'SF Blutraub [Rune] II, SF Runenpakt [Rune], TaW Selbstbeherrschung 15, TaW Anatomie 17');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001719' ,N'Blutmeditation' ,0 ,N'Magisch' ,N'WnT 178' ,N'SF Astrale Meditation, SF Blutraub [beliebige Rune] II, SF Verbotene Pforten, TaW Magiekunde 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001720' ,N'Bluttaufe' ,0 ,N'Magisch' ,N'WnT 112, 179' ,N'Erwerb: IN 12, KO 12, SF Blutmagie, SF Runenmeister (Körper), Besitz und Verfügbarkeit eines Körper-Runensteins; Aufstieg (für den Schüler): IN 10 /12, KO 10 / 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001722' ,N'Morguaimeditation' ,0 ,N'Magisch' ,N'WnT 179' ,N'SF Astrale Meditation, SF Blutmagie, SF Blutraub [beliebige Rune] I');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001723' ,N'Andauerndes Pentagramm' ,0 ,N'Magisch' ,N'WnT 174, 38' ,N'SF Pentagramme');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001724' ,N'Beschwörungspentagramm' ,0 ,N'Magisch' ,N'WnT 174, 38' ,N'SF Pentagramme');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001725' ,N'Doppelziel' ,0 ,N'Magisch' ,N'WnT 174, 38' ,N'SF Pentagramme');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001726' ,N'Immerwährendes Pentagramm' ,0 ,N'Magisch' ,N'WnT 175, 38' ,N'SF Andauerndes Pentagramm, SF Wächterpentagramm, Ritualkenntnis (Runenmagie) 21');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001727' ,N'Pentagramme ' ,0 ,N'Magisch' ,N'WnT 175, 38' ,N'CH 15, TaW Magiekunde 17, Leiteigenschaft 16, Repräsentation Runenmagie, Ritualkenntnis (Runenmagie) 15, drei Runenfertigkeiten ab RfW 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001728' ,N'Wächterpentagramm' ,0 ,N'Magisch' ,N'WnT 175, 38' ,N'SF Pentagramme');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001729' ,N'Zauberpentagramm' ,0 ,N'Magisch' ,N'WnT 175, 38' ,N'SF Pentagramme');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001730' ,N'Derisches Objektritual' ,0 ,N'Magisch' ,N'WnT 154, 38' ,N'Kenntnis zur Herstellung und Bindung/ Weihe eines Artefakts, SF Runenmeditation (Härte), Ausführung: Verfügbarkeit eines Härte-Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001731' ,N'Derisches Runenartefakt ' ,0 ,N'Magisch' ,N'WnT 155, 38' ,N'Kenntnis zur Herstellung und Bindung/ Weihe des Artefakts, SF Runenzauber');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001732' ,N'Empathisches Band' ,0 ,N'Magisch' ,N'WnT 208, 38' ,N'CH 13, Ritualkenntnis (Kymanai) 7, SF Runenmeditation');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001733' ,N'Grundfixierung' ,0 ,N'Magisch' ,N'WnT 208, 38' ,N'IN 13, CH 13, Ritualkenntnis (Kymanai) 7, SF Runenmeditation');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001734' ,N'Lösen des Bandes' ,0 ,N'Magisch' ,N'WnT 209, 38' ,N'CH 13, Ritualkenntnis (Kymanai) 7, SF Stärkung des Bandes');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001735' ,N'Überreizung' ,0 ,N'Magisch' ,N'WnT 209, 38' ,N'CH 14, Ritualkenntnis (Kymanai) 13, SF Grundfixierung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001736' ,N'Schwarmkontrolle ' ,0 ,N'Magisch' ,N'WnT 209, 38' ,N'KL 13 CH 14, Ritualkenntnis (Kymanai) 15, SF Grundfixierung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001737' ,N'Stärkung des Bandes' ,0 ,N'Magisch' ,N'WnT 208, 38' ,N'CH 13, Ritualkenntnis (Kymanai) 7, SF Runenmeditation, SF Empathisches Band');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001738' ,N'Temporäre Fixierung' ,0 ,N'Magisch' ,N'WnT 208, 38' ,N'IN 15, CH 15, Ritualkenntnis (Kymanai) 12, SF Grundfixierung, SF Stärkung des Bandes');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001739' ,N'Regeneration Geist-Rune' ,0 ,N'Magisch' ,N'WnT 110, 38' ,N'SF Runenmeditation (Geist)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001740' ,N'Regeneration Runenpakt ' ,0 ,N'Magisch' ,N'WnT 110, 38' ,N'Erwerb: IN 15, Sombrai als Lehrmeister; Erschaffung: Meditationsbund mit Sombrai, Verfügbarkeit eines geeigneten Runensteins mit eigenem Pakt.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001741' ,N'Repräsentation (Runenmagie)' ,0 ,N'Magisch' ,N'WnT 109, 38' ,N'IN 11, TaW Magiekunde 5, Besitz dreier Runensteine (ggf. mit Erwerb dieser SF). Diese SF ist Bestandteil der tharunischen Vorteile Halb- und Vollzauberer.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001742' ,N'Runenbesitz' ,0 ,N'Magisch' ,N'WnT 108, 38' ,N'IN 11, TaW Magiekunde 3, RfW zu verfügbarem Runenstein');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001744' ,N'Ritualkenntnis (Kymanai)' ,0 ,N'Magisch' ,N'WnT 203, 38' ,N'IN 13, CH 13, SF Runenmeditation, TaW Abrichten 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001745' ,N'Ritualkenntnis (Runenmagie)' ,0 ,N'Magisch' ,N'WnT 108, 109, 38' ,N'');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001747' ,N'Runenpakt ' ,0 ,N'Magisch' ,N'WnT 114, 38' ,N'Lernen: TaW Magiekunde 12, Leiteigenschaft 17, RfW [Rune] 15, SF Runenmeister [Rune]; Paktschluss: Besitz sowie Verfügbarkeit eines passenden Runensteins, RkW  mit Sombrai(Runenmagie) 7 oder Meditationsbund');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001748' ,N'Große Meditation (Tharun)' ,0 ,N'Magisch' ,N'WnT 111, 38' ,N'KL 12, IN 12, Ritualkenntnis (Runenmagie), SF Runenmeister (Geist)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001749' ,N'Astraler Aufstieg ' ,0 ,N'Magisch' ,N'WnT 112, 38' ,N'Erwerb: Sombrai sein, Sombrai als Lehrmeister, SF Meditationsbund; Aufstieg (für den Schüler): IN 10 / 12 / 14 (Aufstieg zum Viertelzauberer / Halbzauberer/ Vollzauberer), TaW Magiekunde 2 / 4 / 6, RfW [Sinnesrune] 6 oder RfW [Zustandsrune] 5 oder RfW [andere Rune] 4, Meditationsbund mit Sombrai');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001750' ,N'Astrale Meditation (Tharun)' ,0 ,N'Magisch' ,N'WnT 111, 38' ,N'IN 13');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001751' ,N'Aufladung' ,0 ,N'Magisch' ,N'WnT 121, 38' ,N'MU 12, CH 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001752' ,N'Belegungsroutine' ,0 ,N'Magisch' ,N'WnT 119, 38' ,N'SF Runenzauber, TaW Selbstbeherrschung 10, TaW Magiekunde 12, RkW (Runenmagie) 7, KL 14');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001753' ,N'Gefäß der Steine' ,0 ,N'Magisch' ,N'WnT 111, 38' ,N'SF Runenbesitz, Besitz mindestens eines Runensteins, CH 15, IN 14, Sombrai als Lehrmeister.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001754' ,N'Gestikzaubern' ,0 ,N'Magisch' ,N'WnT 122, 38' ,N'SF Runenzauber, SF Runenmeister (Körper oder Fühlen), Besitz eines Körper- oder Fühlen-Runensteins (zur Nutzung), RfW (Körper oder Fühlen) 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001755' ,N'Kampfzauberer' ,0 ,N'Magisch' ,N'WnT 121, 38' ,N'Benutztes Waffentalent mindestens TaW 12, Leiteigenschaft 13, SF Runenzauber, SF Runenmeditation (Kampf), SF Konzentrationsstärke, Schnellzaubern I');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001756' ,N'Meditationsbund (Ritual)' ,0 ,N'Magisch' ,N'WnT 112, 38' ,N'Erwerb: Sombrai sein, Sombrai als Lehrmeister, CH 14, RkW (Runenmagie) 11, SF Runenmeister (Geist); Ausführung: Besitz und Verfügbarkeit eines Geist-Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001757' ,N'Schnellzaubern I' ,0 ,N'Magisch' ,N'WnT 121, 38' ,N'INI-Basis 7, Leiteigenschaft 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001758' ,N'Schnellzaubern II' ,0 ,N'Magisch' ,N'WnT 121, 38' ,N'INI-Basis 10, Leiteigenschaft 15; für den Erwerb von Schnellzaubern II ist Schnellzaubern I Voraussetzung');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001759' ,N'Stummes Zaubern' ,0 ,N'Magisch' ,N'WnT 122, 38' ,N'SF Runenzauber, SF Runenmeister (Hören), Besitz eines Hören-Runensteins (zur Nutzung), RfW (Hören) 12');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001760' ,N'Runengefäß (Erde)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001761' ,N'Runengefäß (Feuer)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001762' ,N'Runengefäß (Finsternis)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001763' ,N'Runengefäß (Frieden)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001764' ,N'Runengefäß (Fühlen)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001765' ,N'Runengefäß (Geist)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001766' ,N'Runengefäß (Härte)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001767' ,N'Runengefäß (Hören)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001768' ,N'Runengefäß (Kampf)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001769' ,N'Runengefäß (Körper)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001770' ,N'Runengefäß (Leben und Tod)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001771' ,N'Runengefäß (Licht)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001772' ,N'Runengefäß (Luft)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001773' ,N'Runengefäß (Schmecken)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001774' ,N'Runengefäß (Sehen)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001775' ,N'Runengefäß (Wasser)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001776' ,N'Runengefäß (Weichheit)' ,0 ,N'Magisch' ,N'WnT 162, 38' ,N'TaW Magiekunde 14, TaW beliebiges Handwerkstalent 5, Ritualkenntnis (Runenmagie) 12, RfW [Rune] 7, SF Runenmeister (Härte)');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001777' ,N'Runenmeister (Erde)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001778' ,N'Runenmeister (Feuer)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001779' ,N'Runenmeister (Finsternis)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001780' ,N'Runenmeister (Frieden)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001781' ,N'Runenmeister (Fühlen)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001782' ,N'Runenmeister (Geist)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001783' ,N'Runenmeister (Härte)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001784' ,N'Runenmeister (Hören)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001785' ,N'Runenmeister (Kampf)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001786' ,N'Runenmeister (Körper)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001787' ,N'Runenmeister (Leben und Tod)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001788' ,N'Runenmeister (Licht)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001789' ,N'Runenmeister (Luft)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001790' ,N'Runenmeister (Schmecken)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001791' ,N'Runenmeister (Sehen)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001792' ,N'Runenmeister (Wasser)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001793' ,N'Runenmeister (Weichheit)' ,0 ,N'Magisch' ,N'WnT 113, 114, 37' ,N'TaW Magiekunde 7, Leiteigenschaft 15, RfW [Rune] 12, SF Runenmeditation [Rune]');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001794' ,N'Runenmeditation (Erde)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001795' ,N'Runenmeditation (Feuer)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001796' ,N'Runenmeditation (Finsternis)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001797' ,N'Runenmeditation (Frieden)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001798' ,N'Runenmeditation (Fühlen)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001799' ,N'Runenmeditation (Geist)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001800' ,N'Runenmeditation (Härte)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001801' ,N'Runenmeditation (Hören)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001802' ,N'Runenmeditation (Kampf)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001803' ,N'Runenmeditation (Körper)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001804' ,N'Runenmeditation (Leben und Tod)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001805' ,N'Runenmeditation (Licht)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001806' ,N'Runenmeditation (Luft)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001807' ,N'Runenmeditation (schmecken)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001808' ,N'Runenmeditation (Sehen)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001809' ,N'Runenmeditation (Wasser)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001810' ,N'Runenmeditation (Weichheit)' ,0 ,N'Magisch' ,N'WnT 113, 37' ,N'Lernen: TaW Magiekunde 5, Leiteigenschaft 13; Nutzung: Verfügbarkeit eines passenden Runensteins');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001811' ,N'Fokuswirkung (Erde/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001812' ,N'Fokuswirkung (Feuer/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001813' ,N'Fokuswirkung (Finstrernis/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001814' ,N'Fokuswirkung (Frieden/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001815' ,N'Fokuswirkung (Fühlen/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001816' ,N'Fokuswirkung (Geist/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001817' ,N'Fokuswirkung (Härte/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001818' ,N'Fokuswirkung (Hören/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001819' ,N'Fokuswirkung (Kampf/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001820' ,N'Fokuswirkung (Körper/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001821' ,N'Fokuswirkung (Leben und Tod/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001822' ,N'Fokuswirkung (Licht/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001823' ,N'Fokuswirkung (Luft/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001824' ,N'Fokuswirkung (Schmecken/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001825' ,N'Fokuswirkung (Sehen/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001826' ,N'Fokuswirkung (Wasser/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001827' ,N'Fokuswirkung (Weichheit/Fähigkeit)' ,0 ,N'Magisch' ,N'WnT  161, 162, 36' ,N'RfW [Rune] 5, SF Runenmediation [Rune], Besitz der dazugehörigen Fähigkeit');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001828' ,N'Derisches Zauberwirken durch (Erde/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001829' ,N'Derisches Zauberwirken durch (Feuer/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001830' ,N'Derisches Zauberwirken durch (Finsternis/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001831' ,N'Derisches Zauberwirken durch (Frieden/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001832' ,N'Derisches Zauberwirken durch (Fühlen/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001833' ,N'Derisches Zauberwirken durch (Geist/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001834' ,N'Derisches Zauberwirken durch (Härte/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001835' ,N'Derisches Zauberwirken durch (Hören/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001836' ,N'Derisches Zauberwirken durch (Kampf/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001837' ,N'Derisches Zauberwirken durch (Körper/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001838' ,N'Derisches Zauberwirken durch (Leben und Tod/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001839' ,N'Derisches Zauberwirken durch (Licht/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001840' ,N'Derisches Zauberwirken durch (Luft/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001841' ,N'Derisches Zauberwirken durch (Schmecken/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001842' ,N'Derisches Zauberwirken durch (Sehen/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001843' ,N'Derisches Zauberwirken durch (Wasser/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001844' ,N'Derisches Zauberwirken durch (Weichheit/Aventurien)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001845' ,N'Derisches Zauberwirken durch (Erde/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001846' ,N'Derisches Zauberwirken durch (Feuer/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001847' ,N'Derisches Zauberwirken durch (Finsternis/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001848' ,N'Derisches Zauberwirken durch (Frieden/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001849' ,N'Derisches Zauberwirken durch (Fühlen/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001850' ,N'Derisches Zauberwirken durch (Geist/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001851' ,N'Derisches Zauberwirken durch (Härte/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001852' ,N'Derisches Zauberwirken durch (Hören/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001853' ,N'Derisches Zauberwirken durch (Kampf/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001854' ,N'Derisches Zauberwirken durch (Körper/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001855' ,N'Derisches Zauberwirken durch (Leben und Tod/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001856' ,N'Derisches Zauberwirken durch (Licht/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001857' ,N'Derisches Zauberwirken durch (Luft/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001858' ,N'Derisches Zauberwirken durch (Schmecken/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001859' ,N'Derisches Zauberwirken durch (Sehen/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001860' ,N'Derisches Zauberwirken durch (Wasser/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001861' ,N'Derisches Zauberwirken durch (Weichheit/Myranor)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001862' ,N'Derisches Zauberwirken durch (Erde/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001863' ,N'Derisches Zauberwirken durch (Feuer/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001864' ,N'Derisches Zauberwirken durch (Finsternis/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001865' ,N'Derisches Zauberwirken durch (Frieden/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001866' ,N'Derisches Zauberwirken durch (Fühlen/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001867' ,N'Derisches Zauberwirken durch (Geist/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001868' ,N'Derisches Zauberwirken durch (Härte/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001869' ,N'Derisches Zauberwirken durch (Hören/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001870' ,N'Derisches Zauberwirken durch (Kampf/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001871' ,N'Derisches Zauberwirken durch (Körper/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001872' ,N'Derisches Zauberwirken durch (Leben und Tod/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001873' ,N'Derisches Zauberwirken durch (Licht/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001874' ,N'Derisches Zauberwirken durch (Luft/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001875' ,N'Derisches Zauberwirken durch (Schmecken/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001876' ,N'Derisches Zauberwirken durch (Sehen/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001877' ,N'Derisches Zauberwirken durch (Wasser/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001878' ,N'Derisches Zauberwirken durch (Weichheit/Uthuria)' ,0 ,N'Magisch' ,N'WnT 152, 153, 38' ,N'Kenntnis einer derischen Repräsentation oder einer gleichwertigen Zaubertradition (z. B. Durro-Dûn), SF Runenmeditation [Rune], SF Runenbesitz, Besitz eines passenden Runensteins.');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001879' ,N'Liturgie: Ewiges Weiß' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 183' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001880' ,N'Liturgie: Himmelsbote' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 183' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001881' ,N'Liturgie: Erlösende Hingabe' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001882' ,N'Liturgie: Fruchtbarer Leib' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001883' ,N'Liturgie: Heilsame Hingabe' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001884' ,N'Liturgie: Köstliche Speise' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001885' ,N'Liturgie: Fehllose Schwinge' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001886' ,N'Liturgie: Furchtlose Schwinge' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001887' ,N'Liturgie: Schmerzlose Schwinge' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001888' ,N'Liturgie: Kampfesmut' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001889' ,N'Liturgie: Gnadenbrot' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 187' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001890' ,N'Liturgie: Heilende Hand' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 187' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001891' ,N'Liturgie: Wissen des Fremden (I)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001892' ,N'Liturgie: Achtsame Rache' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001893' ,N'Möge Dir die Erkenntnis ewig verwehrt bleiben!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 193' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001894' ,N'Möge jeder Vogel nach dem Licht Deiner Augen jagen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 193' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001895' ,N'Karmaler Fluch: Mögen die Weißen Stunden Dich verbrennen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 193' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001896' ,N'Karmaler Fluch: Möge die Lebenslinie Deines Geschlechts verlöschen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001897' ,N'Karmaler Fluch: Möge Deine Saat verdorren!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001898' ,N'Karmaler Fluch: Möge kein Kampf mehr von Dir begonnen werden!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001899' ,N'Karmaler Fluch: Mögest Du das Feuer fürchten!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001900' ,N'Karmaler Fluch: Möge alles, was unter Nanurtas Obhut gedeiht, Dir nicht zum Wohl gereichen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001901' ,N'Karmaler Fluch: Mögen Dich die Orangen Stunden in die Knie zwingen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001902' ,N'Karmaler Fluch: Möge das Blut auf dich zurückfallen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001903' ,N'Karmaler Fluch: Möge Dein Blut niemals gerinnen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 194' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001904' ,N'Karmaler Fluch: Mögest du in Qualen sterben!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001905' ,N'Karmaler Fluch: Möge das Mal der Gnade Dir verwehrt bleiben!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001906' ,N'Karmaler Fluch: Mögen Deine Wunden keine Linderung erfahren!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001907' ,N'Karmaler Fluch: Möge Dir Deine Haut ein Bußgewand sein!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001908' ,N'Karmaler Fluch: Möge Deine Magie Dich verlassen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001909' ,N'Karmaler Fluch: Möge Dein Wissen in Dir begraben werden!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001910' ,N'Karmaler Fluch: Möge Dir das Geheimnis der Zeichen verwehrt bleiben!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001911' ,N'Karmaler Fluch: Möge die Unendlichkeit Deinen Leib verlassen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001912' ,N'Karmaler Fluch: Möge die Unergründlichkeit Dein Lebensopfer empfangen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 195' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001913' ,N'Karmaler Fluch: Möge das Mal ewiger Nacht Dich zeichnen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 196' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001914' ,N'Karmaler Fluch: Möge die Rache in Dir wachsen, bis sie Dich ganz erfüllt!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 196' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001915' ,N'Karmaler Fluch: Möge der Atem des Arkanai dich entstellen!' ,0 ,N'Klerikal (Fluch)' ,N'WnT 196' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001916' ,N'Liturgie: Lichtschild' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 183' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001917' ,N'Liturgie: Offenbarung der Sonne' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 183' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001918' ,N'Liturgie: Dienstbare Natur' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001919' ,N'Liturgie: Sprießende Natur' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001920' ,N'Liturgie: Flammende Schwinge' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001921' ,N'Liturgie: Ruf des Shinxasa' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185, 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001922' ,N'Liturgie: Blutmal' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001923' ,N'Liturgie: Kraft des Blutes' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 187' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001924' ,N'Liturgie: Gnädiger Schlaf' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 187' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001925' ,N'Liturgie: Lindernde Demut' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001926' ,N'Liturgie: Purpurne Augen' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001927' ,N'Liturgie: Purpurner Schleier' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001928' ,N'Liturgie: Trübsinnige Buße' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001929' ,N'Liturgie: Wissen des Fremden (II)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001930' ,N'Liturgie: Spur des Meeres' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 190' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001931' ,N'Liturgie: Tiefes Meer' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 190' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001932' ,N'Liturgie: Arkanaiaugen' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001933' ,N'Liturgie: Arkanaigriff' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001934' ,N'Liturgie: Arkanaiklaue' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001935' ,N'Liturgie: Herrschaft der Rache (II)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001936' ,N'Liturgie: Ruf des Arkanai (II)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191, 192' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001937' ,N'Liturgie: Augenfessel' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 183' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001938' ,N'Liturgie: Ruf der Nanja' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001939' ,N'Liturgie: Glühende Esse' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001940' ,N'Liturgie: Ruf des Shinxasa (III)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185, 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001941' ,N'Liturgie: Unbesiegbares Feuer' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001942' ,N'Liturgie: Blutmarter' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001943' ,N'Liturgie: Blutsturz' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001944' ,N'Liturgie: Ruf des Rakshasa' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 187' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001945' ,N'Liturgie: Entzug des Fremden' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001946' ,N'Liturgie: Geheimes Verhängnis' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001947' ,N'Liturgie: Spiegel des Fremden' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001948' ,N'Liturgie: Gestalt der Tiefe' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001949' ,N'Liturgie: Griff der Tiefe' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001950' ,N'Liturgie: Kraft des Meeres' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 190' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001951' ,N'Liturgie: Herrschaft der Rache (III)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001952' ,N'Liturgie: Ruf des Arkanai (III)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191, 192' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001953' ,N'Liturgie: Göttliche Bannung (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001954' ,N'Liturgie: Weihe zum Niederen Azarai' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001955' ,N'Liturgie: Allsehendes Auge' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 183' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001956' ,N'Liturgie: Gleißender Strahl' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 183' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001957' ,N'Liturgie: Gelbe Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001958' ,N'Liturgie: Orange Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001959' ,N'Liturgie: Ruf des Shinxasa (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 185, 186' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001960' ,N'Liturgie: Rote Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 187' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001961' ,N'Liturgie: Purpurne Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001962' ,N'Liturgie: Geheimes Wissen' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188, 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001963' ,N'Liturgie: Violette Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001964' ,N'Liturgie: Azurblaue Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001965' ,N'Liturgie: Ruf der Tiefe' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 190' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001966' ,N'Liturgie: Sendling des Unergründlichen' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 190' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001967' ,N'Liturgie: Arkanaigestalt' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001968' ,N'Liturgie: Ruf des Arkanai (IV)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 191, 192' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001969' ,N'Liturgie: Schwarze Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 192' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001970' ,N'Liturgie: Heiliger Ort' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001971' ,N'Liturgie: Weihe des Sindayru' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001972' ,N'Liturgie: Weihe des Nanurta' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001973' ,N'Liturgie: Weihe des Shin-Xirit' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001974' ,N'Liturgie: Weihe des Zirraku' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001975' ,N'Liturgie: Weihe des Pateshi' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001976' ,N'Liturgie: Weihe des Ojo’Sombri' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001977' ,N'Liturgie: Weihe des Numinoru' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001978' ,N'Liturgie: Weihe des Arkan’Zin' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181, 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001979' ,N'Liturgie: Weihe zum Hohen Azarai' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 182' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001980' ,N'Liturgie: Weiße Stunde' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 184' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001981' ,N'Liturgie: Opfergang' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 187' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001982' ,N'Liturgie: Demütigung des Fremden' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 188' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001983' ,N'Liturgie: Zerstörung des Fremden (V)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001984' ,N'Liturgie: Göttliche Bannung (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 181' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001985' ,N'Liturgie: Zerstörung des Fremden (VI)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001986' ,N'Liturgie: Zerstörung des Fremden (VII)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001987' ,N'Liturgie: Zerstörung des Fremden (VIII)' ,0 ,N'Klerikal (Liturgie)' ,N'WnT 189' ,NULL);
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdH 288 / WdZ 33 / WnT 178' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000206';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WdZ 33 / WnM 160 / WnT 111, 38' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000444';
UPDATE [Sonderfertigkeit] SET [Literatur] = N'WnM 155 / WnT 179' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000001361';

-- Tharun: Soderfertigkeit_Setting
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000000206' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000000444' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001361' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001664' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001665' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001666' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001667' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001668' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001669' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001670' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001671' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001672' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001673' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001674' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001675' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001676' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001677' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001678' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001679' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001680' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001681' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001682' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001683' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001684' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001685' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001686' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001687' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001688' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001689' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001690' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001691' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001692' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001693' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001694' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001695' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001696' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001697' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001698' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001699' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001700' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001701' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001702' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001703' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001704' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001707' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001708' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001709' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001710' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001711' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001712' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001713' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001714' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001715' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001716' ,'00000000-0000-0000-5e77-000000000006' ,N'2' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001717' ,'00000000-0000-0000-5e77-000000000006' ,N'2' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001719' ,'00000000-0000-0000-5e77-000000000006' ,N'2' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001720' ,'00000000-0000-0000-5e77-000000000006' ,N'2' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001722' ,'00000000-0000-0000-5e77-000000000006' ,N'2 ' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001723' ,'00000000-0000-0000-5e77-000000000006' ,N'2 ' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001724' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001725' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001726' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001727' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001728' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001729' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001730' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001731' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001732' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001733' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001734' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001735' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001736' ,'00000000-0000-0000-5e77-000000000006' ,N'2' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001737' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001738' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001739' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001740' ,'00000000-0000-0000-5e77-000000000006' ,N'2' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001741' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001742' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001744' ,'00000000-0000-0000-5e77-000000000006' ,N'4; die Ritualkenntnis gehört keiner Repräsentation an' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001745' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001747' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001748' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001749' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001750' ,'00000000-0000-0000-5e77-000000000006' ,N'5; diese Praxis ist zwar verboten, drängt sich einem Runenherrn aber förmlich auf' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001751' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001752' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001753' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001754' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001755' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001756' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001757' ,'00000000-0000-0000-5e77-000000000006' ,N'4' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001758' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001759' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001760' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001761' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001762' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001763' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001764' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001765' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001766' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001767' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001768' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001769' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001770' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001771' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001772' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001773' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001774' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001775' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001776' ,'00000000-0000-0000-5e77-000000000006' ,N'3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001777' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001778' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001779' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001780' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001781' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001782' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001783' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001784' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001785' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001786' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001787' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001788' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001789' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001790' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001791' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001792' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001793' ,'00000000-0000-0000-5e77-000000000006' ,N'5' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001794' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001795' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001796' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001797' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001798' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001799' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001800' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001801' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001802' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001803' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001804' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001805' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001806' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001807' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001808' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001809' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001810' ,'00000000-0000-0000-5e77-000000000006' ,N'6' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001811' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001812' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001813' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001814' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001815' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001816' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001817' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001818' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001819' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001820' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001821' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001822' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001823' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001824' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001825' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001826' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001827' ,'00000000-0000-0000-5e77-000000000006' ,N'5-3' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001828' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001829' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001830' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001831' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001832' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001833' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001834' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001835' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001836' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001837' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001838' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001839' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001840' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001841' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001842' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001843' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001844' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001845' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001846' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001847' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001848' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001849' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001850' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001851' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001852' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001853' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001854' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001855' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001856' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001857' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001858' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001859' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001860' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001861' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001862' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001863' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001864' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001865' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001866' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001867' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001868' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001869' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001870' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001871' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001872' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001873' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001874' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001875' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001876' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001877' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001878' ,'00000000-0000-0000-5e77-000000000006' ,N'1' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001879' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001880' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001881' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001882' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001883' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001884' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001885' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001886' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001887' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001888' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001889' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001890' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001891' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001892' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001893' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001894' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001895' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001896' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001897' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001898' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001899' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001900' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001901' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001902' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001903' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001904' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001905' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001906' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001907' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001908' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001909' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001910' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001911' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001912' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001913' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001914' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001915' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001916' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001917' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001918' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001919' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001920' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001921' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001922' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001923' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001924' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001925' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001926' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001927' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001928' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001929' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001930' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001931' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001932' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001933' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001934' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001935' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001936' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001937' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001938' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001939' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001940' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001941' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001942' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001943' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001944' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001945' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001946' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001947' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001948' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001949' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001950' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001951' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001952' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001953' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001954' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001955' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001956' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001957' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001958' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001959' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001960' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001961' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001962' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001963' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001964' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001965' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001966' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001967' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001968' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001969' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001970' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001971' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001972' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001973' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001974' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001975' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001976' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001977' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001978' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001979' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001980' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001981' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001982' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001983' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001984' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001985' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001986' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung],  [Name]) 
 VALUES ('00000000-0000-0000-005f-000000001987' ,'00000000-0000-0000-5e77-000000000006' ,NULL ,NULL);

 -- Tharun, Myranor: VorNachteile
 INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Azarai' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000534' ,N'WnT 34');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Blendsicht' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000541' ,N'WnT 34');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Gueraiausbildung' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000542' ,N'WnT 34');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Mitglied einer Herrscherfamilie' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000544' ,N'WnT 35');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Nanjafreund' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000545' ,N'WnT 35');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Reittierempathie' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000546' ,N'WnT 35');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Zusätzliche Runensteine' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000547' ,N'WnT 35');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Abschaum' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000548' ,N'WnT 35');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Morguaiverfall' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000550' ,N'WnT 36');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Sehen)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000551' ,N'WnT 34');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Hören)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000552' ,N'WnT 35');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Fühlen)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000553' ,N'WnT 36');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Schmecken)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000554' ,N'WnT 37');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Licht)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000555' ,N'WnT 38');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Finsternis)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000556' ,N'WnT 39');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Härte)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000557' ,N'WnT 40');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Weichheit)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000558' ,N'WnT 41');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Feuer)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000559' ,N'WnT 42');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Wasser)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000560' ,N'WnT 43');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Erde)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000561' ,N'WnT 44');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Luft)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000562' ,N'WnT 45');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Kampf)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000563' ,N'WnT 46');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Frieden)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000564' ,N'WnT 47');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Körper)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000565' ,N'WnT 48');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Begabung für Rune (Geist)' ,1 ,0 ,0 ,N'int' ,N'Vorteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000566' ,N'WnT 49');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Sehen)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000567' ,N'WnT 36');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Hören)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000568' ,N'WnT 37');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Fühlen)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000569' ,N'WnT 38');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Schmecken)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000570' ,N'WnT 39');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Licht)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000571' ,N'WnT 40');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Finsternis )' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000572' ,N'WnT 41');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Härte)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000573' ,N'WnT 42');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Weichheit)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000574' ,N'WnT 43');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Feuer)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000575' ,N'WnT 44');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Wasser)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000576' ,N'WnT 45');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Erde)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000577' ,N'WnT 46');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Luft)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000578' ,N'WnT 47');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Kampf)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000579' ,N'WnT 48');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Frieden)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000580' ,N'WnT 49');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Körper)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000581' ,N'WnT 50');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Unfähigkeit für Rune (Geist)' ,0 ,1 ,0 ,N'int' ,N'Nachteile' ,N'Tharun' ,'00000000-0000-0000-f024-000000000582' ,N'WnT 51');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (blau)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000583' ,N'WnT 35');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (violett)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000584' ,N'WnT 36');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (weiß)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000585' ,N'WnT 37');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (purpur)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000586' ,N'WnT 38');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (rot)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000587' ,N'WnT 39');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (orange)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000588' ,N'WnT 40');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (gelb)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000589' ,N'WnT 41');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Angst vor Sonnenfarbe (schwarz)' ,0 ,1 ,1 ,N'int' ,N'Nachteile (Schlechte Eigenschaften)' ,N'Tharun' ,'00000000-0000-0000-f024-000000000590' ,N'WnT 42');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Meereskind' ,1 ,0 ,0 ,NULL ,N'Vorteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000591' ,N'MM 203');
INSERT INTO [VorNachteil] (  [Name],  [Vorteil],  [Nachteil],  [HatWert],  [WertTyp],  [Typ],  [Setting],  [VorNachteilGUID],  [Literatur]) 
 VALUES (N'Elementgebunden [Element]' ,0 ,1 ,0 ,NULL ,N'Nachteile' ,N'Myranor' ,'00000000-0000-0000-f024-000000000592' ,N'MM 203');
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor, Thraun' ,[Literatur] = N'WdH 249 / WnM 106 / WnT 34' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000078';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor, Tharun' ,[Literatur] = N'WdH 252 / WnM 110 / WnT 34' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000095';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor, Tharun' ,[Literatur] = N'WdH 252 / WnM 110 / WnT 34' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000096';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor, Tharun' ,[Literatur] = N'WdH 257, 258 / WnM 119, 120 / WnT 35' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000150';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor, Tharun' ,[Literatur] = N'WdH 258 / WnT 35' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000151';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor, Thraun' ,[Literatur] = N'WdH 248 / WnM 104 / WnT 33' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000019';
UPDATE [VorNachteil] SET [Setting] = N'Aventurien, Myranor, Tharun' ,[Literatur] = N'WdH 249 / WnM 105 / WnT 34' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000075';
UPDATE [VorNachteil] SET [Setting] = N'Myranor' ,[Literatur] = N'WnM 120' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000486';
UPDATE [VorNachteil] SET [Setting] = N'Myranor' ,[Literatur] = N'WnM 120' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000487';
UPDATE [VorNachteil] SET [Setting] = N'Myranor, Tharun' ,[Literatur] = N'WnM 124 / WnT 35' WHERE [VorNachteilGUID]='00000000-0000-0000-f024-000000000535';

-- Rassen
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000053' ,N'Braunpelzork' ,N'Morgan' ,0 ,2 ,175 ,N'5W6' ,-85 ,0 ,0 ,0 ,-1 ,0 ,0 ,0 ,1 ,2 ,1 ,0 ,0 ,0 ,N'Rakshazar 43' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000054' ,N'Braunpelzork' ,N'Braunpelzork-Mischling' ,0 ,6 ,150 ,N'3W6' ,-90 ,1 ,-1 ,0 ,-1 ,-1 ,0 ,1 ,1 ,6 ,8 ,0 ,-3 ,0 ,N'Rakshazar 43' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000055' ,N'Brokthar' ,N'Brokthar-Mann' ,0 ,11 ,190 ,N'3W20' ,-90 ,1 ,0 ,-1 ,0 ,-1 ,-1 ,2 ,3 ,13 ,17 ,0 ,-6 ,0 ,N'Rakshazar 45' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000056' ,N'Brokthar' ,N'Brokthar-Frau' ,0 ,11 ,180 ,N'2W20' ,-100 ,1 ,0 ,-1 ,0 ,0 ,0 ,0 ,2 ,13 ,17 ,0 ,-6 ,0 ,N'Rakshazar 45' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000057' ,N'Donari' ,N'Donari' ,0 ,10 ,158 ,N'2W20' ,-110 ,0 ,0 ,1 ,0 ,0 ,1 ,0 ,-1 ,7 ,10 ,0 ,-2 ,0 ,N'Rakshazar 47' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000058' ,N'Donari' ,N'Donari-Mischling' ,0 ,4 ,158 ,N'2W20' ,-110 ,0 ,0 ,1 ,0 ,0 ,1 ,0 ,-1 ,3 ,5 ,0 ,-1 ,0 ,N'Rakshazar 47' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000059' ,N'Faulzwerge' ,N'Faulzwerge' ,0 ,1 ,125 ,N'2W6' ,-90 ,0 ,0 ,0 ,0 ,1 ,-1 ,0 ,1 ,10 ,12 ,0 ,-4 ,0 ,N'Rakshazar 49' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000061' ,N'Nedermann' ,N'Nedermann' ,0 ,2 ,155 ,N'2W20' ,-90 ,0 ,-3 ,0 ,-2 ,0 ,0 ,2 ,1 ,13 ,13 ,0 ,-7 ,0 ,N'Rakshazar 53' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000062' ,N'Nordländer' ,N'Nordländer' ,0 ,5 ,165 ,N'2W20' ,-110 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,11 ,11 ,0 ,-5 ,0 ,N'Rakshazar 55' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000063' ,N'Nordländer' ,N'Nordländer-Mischling' ,0 ,1 ,165 ,N'2W20' ,-110 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,5 ,5 ,0 ,-2 ,0 ,N'Rakshazar 55' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000064' ,N'Sanskitaren' ,N'Sanskitaren' ,0 ,2 ,150 ,N'2W20' ,-105 ,0 ,0 ,1 ,1 ,1 ,1 ,0 ,0 ,10 ,10 ,0 ,-4 ,0 ,N'Rakshazar 57' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000065' ,N'Sanskitaren' ,N'Sanskitaren-Mischling' ,0 ,2 ,150 ,N'2W20' ,-105 ,0 ,0 ,1 ,1 ,1 ,1 ,0 ,0 ,5 ,5 ,0 ,-2 ,0 ,N'Rakshazar 57' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000066' ,N'Schwarzpelzork' ,N'Schwarzpelzork' ,0 ,13 ,145 ,N'3W6' ,-95 ,2 ,-2 ,0 ,-2 ,-1 ,1 ,2 ,1 ,12 ,18 ,-7 ,0 ,0 ,N'Rakshazar 59' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000067' ,N'Schwarzpelzork' ,N'Schwarzpelzork-Mischling' ,0 ,6 ,145 ,N'3W6' ,-95 ,1 ,-1 ,0 ,-1 ,-1 ,1 ,1 ,1 ,6 ,9 ,-7 ,-3 ,0 ,N'Rakshazar 59' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000068' ,N'Sirdak' ,N'Sirdak' ,0 ,11 ,143 ,N'3W6' ,-105 ,-1 ,-1 ,0 ,0 ,0 ,3 ,-1 ,-1 ,8 ,10 ,0 ,-4 ,0 ,N'Rakshazar 61' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000069' ,N'Tharai' ,N'Tharai' ,0 ,4 ,173 ,N'3W6' ,-90 ,1 ,-1 ,0 ,-1 ,-1 ,1 ,1 ,0 ,10 ,9 ,0 ,-4 ,0 ,N'Rakshazar 63' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000070' ,N'Uthurium' ,N'Uthurium' ,0 ,5 ,165 ,N'1W20' ,-95 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,11 ,12 ,0 ,-5 ,0 ,N'Rakshazar 65' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000071' ,N'Uthurium' ,N'Uthurium-Mischling' ,0 ,2 ,165 ,N'1W20' ,-95 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,5 ,6 ,0 ,-2 ,0 ,N'Rakshazar 65' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000072' ,N'Vaesten' ,N'Vaesten' ,0 ,4 ,155 ,N'4W6' ,-100 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,10 ,12 ,0 ,-4 ,0 ,N'Rakshazar 67' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000073' ,N'Vaesten' ,N'Vaesten-Mischling' ,0 ,2 ,155 ,N'4W6' ,-100 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,10 ,12 ,0 ,-4 ,0 ,N'Rakshazar 67' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000074' ,N'Weisspelzorks' ,N'Weisspelzorks' ,0 ,6 ,140 ,N'3W6' ,-90 ,2 ,-2 ,0 ,-2 ,0 ,1 ,1 ,0 ,11 ,15 ,0 ,-6 ,0 ,N'Rakshazar 69' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000075' ,N'Weisspelzorks' ,N'Weisspelzorks-Mischling' ,0 ,1 ,140 ,N'3W6' ,-90 ,1 ,-1 ,0 ,-1 ,0 ,1 ,1 ,0 ,5 ,7 ,0 ,-3 ,0 ,N'Rakshazar 69' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000076' ,N'Xhul' ,N'Xhul' ,0 ,8 ,172 ,N'2W20' ,-105 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,11 ,15 ,0 ,-4 ,0 ,N'Rakshazar 71' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000077' ,N'Xhul' ,N'Xhul-Mischling' ,0 ,2 ,172 ,N'2W20' ,-105 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,5 ,7 ,0 ,-2 ,0 ,N'Rakshazar 71' ,N'Rakshazar' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000078' ,N'Waldmensch' ,N'Tapasuul' ,0 ,7 ,2 ,N'1W20' ,-130 ,0 ,0 ,0 ,1 ,0 ,1 ,1 ,-1 ,8 ,13 ,0 ,-4 ,0 ,N'WdH 28' ,N'Aventurien, Dunkle Zeiten' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000079' ,N'Mensch ' ,N'Dorinterher' ,0 ,0 ,144 ,N'2W20' ,-95 ,0 ,0 ,0 ,0 ,0 ,-1 ,0 ,1 ,10 ,10 ,0 ,-4 ,0 ,N'WnM 29' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000080' ,N'Mensch ' ,N'Hjaldinger' ,0 ,2 ,172 ,N'2W20' ,-90 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,11 ,10 ,0 ,-5 ,0 ,N'WnM 30' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000081' ,N'Mensch ' ,N'Bansumiter' ,0 ,2 ,156 ,N'2W20' ,-100 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,10 ,10 ,0 ,-4 ,0 ,N'WnM 30' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000082' ,N'Mensch ' ,N'Vesai' ,0 ,0 ,155 ,N'2W20' ,-115 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,-1 ,10 ,12 ,0 ,-5 ,0 ,N'WnM 30' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000083' ,N'Mensch ' ,N'Ban Bargui' ,0 ,0 ,160 ,N'2W20' ,-110 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,10 ,11 ,0 ,-5 ,0 ,N'WnM 30' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000084' ,N'Katzenwesen' ,N'Amaunir' ,0 ,12 ,148 ,N'2W20' ,-110 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,11 ,11 ,0 ,-5 ,0 ,N'WnM 33' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000085' ,N'Chimärenspezies' ,N'Ashariel' ,0 ,16 ,230 ,N'2W20' ,-170 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,9 ,20 ,0 ,-3 ,0 ,N'WnM 33, 34' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000088' ,N'Tier-Mensch-Chimäre' ,N'Kynokephalen-Mann' ,0 ,9 ,150 ,N'5W6' ,-70 ,-1 ,0 ,1 ,-1 ,1 ,0 ,0 ,2 ,10 ,12 ,0 ,-4 ,0 ,N'WnM 36' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000089' ,N'Tier-Mensch-Chimäre' ,N'Kynokephalen-Frau' ,0 ,9 ,150 ,N'5W6' ,-65 ,-1 ,0 ,1 ,-1 ,1 ,0 ,0 ,2 ,10 ,12 ,0 ,-4 ,0 ,N'WnM 36' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000090' ,N'humanoide Löwen' ,N'Leonir-Mann' ,0 ,16 ,210 ,N'2W20' ,-90 ,2 ,0 ,0 ,0 ,-2 ,1 ,2 ,2 ,12 ,10 ,0 ,-6 ,0 ,N'WnM 37' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000091' ,N'humanoide Löwen' ,N'Leonir-Frau' ,0 ,16 ,170 ,N'2W20' ,-110 ,2 ,0 ,0 ,0 ,-2 ,1 ,3 ,1 ,11 ,12 ,0 ,-6 ,0 ,N'WnM 37' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000092' ,N'Fischwesen' ,N'Loualil' ,0 ,8 ,148 ,N'2W20' ,-110 ,0 ,0 ,1 ,1 ,0 ,1 ,0 ,-1 ,9 ,15 ,0 ,-3 ,0 ,N'WnM 39' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000093' ,N'Otterwesen' ,N'Lutraa' ,0 ,16 ,120 ,N'1W20' ,-90 ,0 ,0 ,1 ,0 ,-2 ,2 ,1 ,-1 ,7 ,18 ,0 ,-5 ,0 ,N'WnM 39' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000094' ,N'humanoide Luchse' ,N'Lyncil' ,0 ,12 ,139 ,N'1W20' ,-70 ,0 ,0 ,0 ,0 ,0 ,-1 ,2 ,0 ,13 ,12 ,0 ,-5 ,0 ,N'WnM 40' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000095' ,N'Stierwesen' ,N'Minotaure' ,0 ,8 ,208 ,N'2W20' ,-90 ,0 ,-1 ,0 ,0 ,-1 ,0 ,2 ,2 ,12 ,18 ,0 ,-4 ,0 ,N'WnM 41' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000096' ,N'vieamirges humanoides Wesen' ,N'Neristu ' ,0 ,16 ,142 ,N'3W6' ,-110 ,0 ,0 ,1 ,0 ,2 ,0 ,-1 ,-2 ,7 ,10 ,0 ,-2 ,0 ,N'WnM 42' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000097' ,N'Katzenwesen' ,N'Padir' ,0 ,6 ,170 ,N'2W20' ,-120 ,1 ,-2 ,2 ,0 ,-2 ,1 ,2 ,0 ,12 ,12 ,0 ,-6 ,0 ,N'WnM 43' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000098' ,N'Katzenwesen' ,N'Schneepadir' ,0 ,6 ,170 ,N'2W20' ,-100 ,1 ,-1 ,1 ,0 ,-2 ,1 ,2 ,0 ,12 ,13 ,0 ,-6 ,0 ,N'WnM 44' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000099' ,N'menschliches Zwitterwesen' ,N'Ravesaran' ,0 ,14 ,165 ,N'1W20' ,-115 ,0 ,0 ,1 ,2 ,0 ,0 ,0 ,-2 ,10 ,10 ,0 ,-2 ,0 ,N'WnM 45' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000100' ,N'Fischwesen' ,N'Risso' ,0 ,6 ,163 ,N'2W6' ,-110 ,0 ,0 ,0 ,-2 ,-2 ,1 ,1 ,2 ,5 ,10 ,0 ,-4 ,0 ,N'WnM 45, 46' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000101' ,N'humanoide Mischwesen' ,N'Satyar' ,0 ,14 ,140 ,N'W20' ,-100 ,0 ,0 ,1 ,1 ,0 ,0 ,0 ,-1 ,12 ,13 ,0 ,-2 ,0 ,N'WnM 46' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000102' ,N'Chamäleonwesen' ,N'Shingwa' ,0 ,15 ,130 ,N'3W6' ,-110 ,0 ,0 ,0 ,0 ,-1 ,2 ,-1 ,-1 ,6 ,12 ,0 ,-5 ,0 ,N'WnM 47' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000103' ,N'Echsenwesen' ,N'Shinoqi' ,0 ,19 ,260 ,N'2W20' ,-100 ,0 ,0 ,0 ,0 ,-2 ,0 ,2 ,0 ,13 ,13 ,0 ,-4 ,0 ,N'WnM 48' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000104' ,N'Katzenwesen' ,N'Tighrir' ,0 ,19 ,260 ,N'2W20' ,-100 ,0 ,0 ,0 ,0 ,2 ,1 ,2 ,3 ,16 ,11 ,0 ,-5 ,0 ,N'WnM 50' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000106' ,N'Zwerg' ,N'Zwerg' ,0 ,15 ,128 ,N'2W6' ,-80 ,0 ,0 ,0 ,0 ,0 ,-1 ,2 ,2 ,21 ,16 ,0 ,-4 ,0 ,N'WnM 52' ,N'Myranor' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000107' ,N'Hashandris' ,N'Hashandris' ,0 ,3 ,160 ,N'2W20' ,-105 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,10 ,12 ,0 ,-3 ,0 ,N'WnT 7,8' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000108' ,N'Kuumis' ,N'Kuumis' ,0 ,4 ,153 ,N'2W20' ,-100 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,10 ,15 ,0 ,-3 ,0 ,N'WnT 8' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000109' ,N'Conossis' ,N'Conossis' ,0 ,4 ,155 ,N'2W20' ,-100 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,11 ,12 ,0 ,-3 ,0 ,N'WnT 8' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000110' ,N'Lanier' ,N'Lanier' ,0 ,3 ,155 ,N'2W20' ,-110 ,0 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,10 ,12 ,0 ,-3 ,0 ,N'WnT 8' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000111' ,N'Jüiten' ,N'Jüiten' ,0 ,3 ,165 ,N'2W20' ,-105 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,10 ,12 ,0 ,-3 ,0 ,N'WnT 8' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000112' ,N'Memonhabiter' ,N'Memonhabiter' ,0 ,3 ,150 ,N'2W20' ,-105 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,10 ,12 ,0 ,-3 ,0 ,N'WnT 8' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000113' ,N'Thuaris' ,N'Thuaris' ,0 ,3 ,155 ,N'2W20' ,-100 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,10 ,12 ,0 ,-3 ,0 ,N'WnT 9' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000114' ,N'Ilshiten' ,N'Ilshiten' ,0 ,3 ,150 ,N'2W20' ,-110 ,0 ,0 ,0 ,0 ,1 ,0 ,0 ,0 ,10 ,12 ,0 ,-3 ,0 ,N'WnT 9' ,N'Tharun' ,N'+');
INSERT INTO [Rasse] (  [RasseGUID],  [Name],  [Variante],  [Unspielbar],  [GP],  [Größe],  [GrößeMod],  [Gewicht],  [MUMod],  [KLMod],  [INMod],  [CHMod],  [FFMod],  [GEMod],  [KOMod],  [KKMod],  [LEMod],  [AUMod],  [AEMod],  [MRMod],  [INIMod],  [Literatur],  [Setting],  [GewichtOperator]) 
 VALUES ('00000000-0000-0000-0000-000000000115' ,N'Tharuner' ,N'Tharuner' ,0 ,3 ,145 ,N'3W20' ,-105 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,10 ,12 ,0 ,-3 ,0 ,N'WnT 9' ,N'Tharun' ,N'+');

 -- Handelsgüter
 INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003481' ,N'Allgemeine Schiffsglocke' ,NULL ,NULL ,N'Zeitungen' ,N'Schiffsregister' ,N'Herkunft: Festum; Druckerei: eigene; Erscheinung: Vierterjährlich; Alles rund um Seefahrt und ausführliches Schiffsregister. Berüchtigt für Dreimonats-Wettervorhersagen.' ,N'AvAlm 3 / DLBW 131 / LdsB 94' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003482' ,N'Aventurischer Bote' ,NULL ,NULL ,N'Zeitungen' ,N'Politik' ,N'Herkunft: Gareth; Druckerei: Kaiserliche Garethische Botendruckerei in Gareth, Druckhaus Ya Cerrano in Kuslik. Erscheinung: 2 Monate; Eine der ersten Zeitungen Aventuriens, Mittelreichs- und kaisertreu, druckt auch viele Nachrichten aus kleineren Zeitungen aventurienweit. Preis gilt pro Ausgabe, ist in Gareth kostenlos. Sonst 2 S pro vorgelesenem Artikel.' ,N'AvLSA 32 / AB100 13 / AvAlm 50 / DKG 12 / HdR 86, 89 / VG 15-21' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003483' ,N'Baburische Landpostille' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Baburin; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003484' ,N'Balihoer-Bilder-Brevier' ,NULL ,NULL ,N'Zeitungen' ,N'Illustrierte' ,N'Herkunft: Baliho; Druckerei: k.A.; Erscheinung: k.A.; Zeitung mit vielen Illustrationen und wenig Text.' ,N'AvAlm 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003485' ,N'Belenas-Bote' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Mengbilla; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003486' ,N'Belhankaner Beobachter' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Belhanka; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003487' ,N'Bosparanisches Blatt' ,NULL ,NULL ,N'Zeitungen' ,N'Politik' ,N'Herkunft: Vinsalt; Druckerei: eigene; Erscheinung: 2 Monate; Überregional und horastreu. Nüchtern-akribische Einstellung mit profunden Kenntnissen der horasischen Politik.' ,N'AvAlm 12, 50 / RdH 131, 133' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003488' ,N'Bosparan-Herold' ,NULL ,NULL ,N'Zeitungen' ,N'Gendarmarie Gazette, Handelsblatt' ,N'Herkunft: Vinsalt; Druckerei: Vinsalter Horasdruckerei; Erscheinung: wöchentlich; Sehr horasisch patriotisch-reisserische Einstellung, Gedarmarie-Gazette und Handelsblatt.' ,N'AvAlm 12, 50 / AB100 14 / DKaY 33 / DRdH 76 / RdH 131' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003489' ,N'Brabaker Bilderpostille' ,NULL ,NULL ,N'Zeitungen' ,N'Holzschnitt-Illustrierte' ,N'Herkunft: Brabak; Druckerei: Werkstätte vom Bund des Roten Salamanders; Erscheinung: 2 Monate; Fast ausschließlich aus einfachen Holzschnitten bestehend, berüchtigt für Karikaturen hochgestellter Persönlichkeiten, in fast allen südaventurischen Hafenstädte und bis Festum und Havena verbreitet.' ,N'AvAlm 12, 50 / AB100 12 / AB130 20 / IdDM 84, 92' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003490' ,N'Das Spektakel' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Vinsalt; Druckerei: k.A.; Erscheinung: k.A.; Befasst sich mit Kunst und Theater. Einzelne Berichte im Aventurischen Boten.' ,N'' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003491' ,N'Der Greifenbalg' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Mendena; Druckerei: eigene; Erscheinung: k.A.; Kriecherische Propagande für die Heptarchen. Einzelne Berichte im Aventurischen Boten.' ,N'Scl 105, 108' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003492' ,N'Der Optolith' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Yol-Ghurmak; Druckerei: k.A.; Erscheinung: k.A.; Das Transysilische Tagesblatt, herausgegeben von einer ehemalingen Korrespondentin des Aventurischen Boten. Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003493' ,N'Fantholi' ,NULL ,NULL ,N'Zeitungen' ,N'Mitteilungsblatt' ,N'Herkunft: Trallop; Druckerei: k.A.; Erscheinung: vierteljährlich; Provinzpostille des Herzogtum Weidens.' ,N'AvAlm 22, 50 / http://www.herzogtum-weiden.net/fantholi' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003494' ,N'Festumer Flagge' ,NULL ,NULL ,N'Zeitungen' ,N'Politik' ,N'Herkunft: Festum; Druckerei: eigene; Erscheinung: 2 Monate; Setzt sich mit örtliche und aventurische Politik kritisch auseinander.' ,N'AvAlm 23, 50 / AB100 12 / DLBW 135 / LdsB 94' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003495' ,N'Freie Trommel' ,NULL ,NULL ,N'Zeitungen' ,N'Propaganda' ,N'Herkunft: Andergast; Druckerei: eigene; Erscheinung: unregelmäßig; Negative Berichtserstattung gegen Nostria, schlechte Qualität.' ,N'AvAlm 50 / AB100 12' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003496' ,N'Garether & Märker Herold' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: ; Druckerei: ; Erscheinung: ; Befasst sich mit Geschehnissen im Königreich Garetien und den Markgrafschaften Greifenfurt und Perricum. Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003497' ,N'Garether Tagespostille' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Gareth; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003498' ,N'Gratenfelser Gazette' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: k.A.; Druckerei: k.A.; Erscheinung: k.A.; Geschehnisse der Landgrafschaft Gratenfels. Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003499' ,N'Harika' ,NULL ,NULL ,N'Zeitungen' ,N'Modezeitschrift' ,N'Herkunft: Belhanka; Druckerei: k.A.; Erscheinung: jährlich; Modezeitschrift der Reederei Fiaga ya Terdilions.' ,N'GdE 38' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003500' ,N'Havena-Fanfare' ,NULL ,NULL ,N'Zeitungen' ,N'Amtsblatt' ,N'Herkunft: Havena; Druckerei: Dreikronen-Druckerei; Erscheinung: 1010BF noch wöchentlich, seit 1029F monatlich und Tendenz fallend; Älteste Zeitung Aventuriens mit 1-4 Seiten Sensationsartikel und Mitteilungen. Alberniatreu.' ,N'AB100 13 / DFA 53 / AGF 80 / AvLSA 121' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003501' ,N'Hesindespiegel' ,NULL ,NULL ,N'Zeitungen' ,N'Fachzeitschrift, Heft' ,N'Herkunft: Kuslik; Druckerei: Druckhaus Ya Cerrano; Erscheinung: vierteljährlich; Fachzeitschrift für wissenschaftlichen, speziell magischen Themen von der Halle der Weisheit.' ,N'AvAlm 30, 50 / AB100 13 / WdZ 102, 103' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003502' ,N'Hexenwissen' ,NULL ,NULL ,N'Zeitungen' ,N'Schriftrollen' ,N'Herkunft: k.A.; Druckerei: k.A.; Erscheinung: unregelmäßig bis 1010BF; Allerlei magische Tratsch und Klatsch, jeweils 4 Schriftrollen.' ,N'WdZ 104' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003503' ,N'Journal für angewandte Hermetik' ,NULL ,NULL ,N'Zeitungen' ,N'Jahrbuch' ,N'Herkunft: Bethana; Druckerei: Bleywercker Druckerei; Erscheinung: jährlich; Jahrbuch der Akademie Bethana für Kampfzauberei. Neueste Forschungsergebnisse und Sammlung magischer Erscheinungen und Kuriositäten.' ,N'AB100 12 / WdZ 104 / DMB 23' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003504' ,N'Kosch-Kurier' ,NULL ,NULL ,N'Zeitungen' ,N'Gazette' ,N'Herkunft: Ferdok; Druckerei: eigene in Steinbrücken; Erscheinung: unregelmäßig; Reichstreue Gazette des Fürstentums Kosch.' ,N'AvLSA 149 / AvAlm 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003505' ,N'Kusliker Kurier' ,NULL ,NULL ,N'Zeitungen' ,N'Gazette' ,N'Herkunft: Kuslik; Druckerei: Druckhaus Ya Cerrano; Erscheinung: wöchentlich; Gazette des Buchhändlers Robert Bardun, nur in Kuslik weit verbreitet.' ,N'AB100 13 / AvAlm 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003506' ,N'Lowanger Lanze' ,NULL ,NULL ,N'Zeitungen' ,N'Periodikum' ,N'Herkunft: Lowangen; Druckerei: Svelltland-Presse; Erscheinung: k.A.; Aventurienweit berühmtes Periodikum der einzigen Druckerei des ganzen Svelltlandes.' ,N'AB100 13 / AvAlm 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003507' ,N'Nordmärker Nachrichten' ,NULL ,NULL ,N'Zeitungen' ,N'Mitteilungsblatt' ,N'Herkunft: k.A.; Druckerei: k.A.; Erscheinung: unregelmäßig; Zeitschrift des Herzogtums Nordmarken.' ,N'www.nordmarken.de' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003508' ,N'Nostrianische Kriegsposaune' ,NULL ,NULL ,N'Zeitungen' ,N'Propaganda' ,N'Herkunft: Nostria; Druckerei: Nostrianer Kartoffeldruckerei Tommelstomp & Tochter; Erscheinung: unregelmäßig; Propagandablatt Nostrias, wahrheitsverzerrende Berichte und Andergast Hetze, nur in Nostria selbst richtig verbreitet.' ,N'AB100 14 / AvAlm 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003509' ,N'Premer Platt' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Prem; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003510' ,N'Rabenschwinge' ,NULL ,NULL ,N'Zeitungen' ,N'Propaganda' ,N'Herkunft: k.A.; Druckerei: k.A.; Erscheinung: k.A.; Propagandablatt des Kemi-Reiches. ' ,N'AvAlm 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003511' ,N'Rommilyser Bulle (eh. Darpatischer Landbote)' ,NULL ,NULL ,N'Zeitungen' ,N'Mitteilungsblatt' ,N'Herkunft: Rommilys; Druckerei: k.A.; Erscheinung: 2 Monate; Postille der Rommilyser Mark, ging aus dem Darpatischen Landboten hervor, nach der Auflösung des Fürstentum Darpatiens 1028BF.' ,N'AvAlm 17, 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003512' ,N'Salamander' ,NULL ,NULL ,N'Zeitungen' ,N'Fachblatt' ,N'Herkunft: Brabak; Druckerei: Werkstätte vom Bund des Roten Salamanders; Erscheinung: vierteljährlich; Fachzeitschrift des Roten Salamanders, kompetent und wirklichkeitsnahe.' ,N'AB100 12 / AvAlm 50 / WdZ 103' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003513' ,N'Seewind' ,NULL ,NULL ,N'Zeitungen' ,N'Schiffsregister' ,N'Herkunft: Bethana; Druckerei: Bleywercker Druckerei; Erscheinung: monatlich; Von der Bruderschaft von Wind und Wogen und dem Efferd-Tempel herausgebracht, bestgeführte Schiffsregister der Westküste. Auch Berichte über technische Fortschritte, neue Fahrrouten und Seemansgarn.' ,N'AB100 12 / AvAlm 50, 59' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003514' ,N'Selemer Anzeiger' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Selem; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003515' ,N'Selemer Ausrufer' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Selem; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003516' ,N'Sheniloer Hesindeblatt' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Shenilo; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003517' ,N'Spiegel der Schwarzmagie' ,NULL ,NULL ,N'Zeitungen' ,N'Fachblatt' ,N'Herkunft: Brabak; Druckerei: Werkstätte vom Bund des Roten Salamanders; Erscheinung: vierteljährlich; Im Auftrag der Dunklen Halle der Geister gedruckt, obskures Magazin über die Schwarze Kunst ohne wirklich lästerliche Inhalte, von ältlichen erfolglosen Schwarzmagiern geschrieben.' ,N'AB100 12 / WdZ 103 / DMB 23' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003518' ,N'Südwind' ,NULL ,NULL ,N'Zeitungen' ,N'Zeitschrift' ,N'Herkunft: Brabak; Druckerei: Werkstätte vom Bund des Roten Salamanders; Erscheinung: k.A.; Lokales Blatt der Brabaker Oberschicht.' ,N'AB130 20 / IdDM 92' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003519' ,N'Tempelrufer' ,NULL ,NULL ,N'Zeitungen' ,N'Periodikum' ,N'Herkunft: Al''Anfa; Druckerei: Universitätsdruckerei; Erscheinung: monatlich; Einziges in Al''Anfa zugelassenes Periodikum, von der Stadt des Schweigens kontrolliert, arrogant-frivole Texte über Träume von Lust und Macht.' ,N'AvAlm 50 / IdDM 59, 67' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003520' ,N'Tobrisches Wolfshorn' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: k.A.; Druckerei: k.A.; Erscheinung: k.A.; Provinz-Gazette des Herzogtums Tobrien. Einzelne Berichte im Aventurischen Boten.' ,N'www.herzogtum-tobrien.de/Wolfshorn.htm' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003521' ,N'Tradj' ,NULL ,NULL ,N'Zeitungen' ,N'Gazette' ,N'Herkunft: Sinoda; Druckerei: k.A.; Erscheinung: k.A.; Erst 1022BF gegründet und befasst sich mit Geschehnissen auf Maraskan und im Shîkanydad von Sinoda.' ,N'Scl 170, 172' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003522' ,N'Unterfelser Yaquirkurier' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: k.A.; Druckerei: k.A.; Erscheinung: k.A.; Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003523' ,N'Xeledonischer Spiegel' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Kuslik; Druckerei: k.A.; Erscheinung: k.A.; Zeitschrift mit hesindegefälligen Themen. Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003524' ,N'Yaquirblick' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Punin; Druckerei: k.A.; Erscheinung: unregelmäßig; Souveräne Journaille Almadas, berichtet in bunter Boulervard-Manier über aktuellen Geschehnissen in der Provinz und angrenzenden Gebieten. Einzelne Berichte im Aventurischen Boten.' ,N'wiki.punin.de' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003525' ,N'Zorganer Kurier' ,NULL ,NULL ,N'Zeitungen' ,N'Berichterstattung für Aventurischen Boten' ,N'Herkunft: Zorgan; Druckerei: k.A.; Erscheinung: k.A.; Befasst sich hauptsächlich mit Geschehnisse in Zorgan und Aranien. Einzelne Berichte im Aventurischen Boten.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003526' ,N'Aschenbusch' ,NULL ,N'Portion' ,N'Kräuter' ,N'Giftpflanze' ,NULL ,N'Uhrwerk3 29, 30, 31' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003527' ,N'Atermatea Funginus/Baramuns Liebling' ,NULL ,N'Portion' ,N'Kräuter' ,N'Giftpflanze/Nutzpflanze' ,N'8 Ob pro Portion, Preis außerhalb der Erntezeit 1 bis 2 Ag, wobei Baramunen auch deutlich mehr zahlen oder sie mit Gewalt an sich bringen' ,N'Uhrwerk3 31, 32' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003528' ,N'Batuunuur/Blutgewürz' ,NULL ,N'Portion' ,N'Kräuter' ,N'Parasit/Nutzpflanze' ,N'Preis: 5 Ag unter Wasser, an Land ab 8 Ag pro Portion (2 Unzen entsprechen einer Sporenladung)' ,N'Uhrwerk3 32, 33' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003529' ,N'Blutpilz' ,NULL ,NULL ,N'Kräuter' ,N'Giftpflanze/gefährliche Pflanze' ,NULL ,N'Uhrwerk3 33, 34' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003530' ,N'Brajankelch (Nektar oder Fruchtsaft)' ,NULL ,N'Dosis Nektar' ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk3 34' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003531' ,N'Brajankelch  (Frucht)' ,NULL ,N'Frucht' ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk3 34' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003532' ,N'Brajanlieb/Kompasskraut ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk3 34, 35' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003533' ,N'Chrysirhaube' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk3 34' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003534' ,N'Echobaum (Leuchtstoff)' ,NULL ,NULL ,N'Kräuter' ,N'übernatürliche Pflanze' ,NULL ,N'Uhrwerk3 35 ,36' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003535' ,N'Echobaum (Pulver)' ,NULL ,NULL ,N'Kräuter' ,N'übernatürliche Pflanze' ,NULL ,N'Uhrwerk3 35 ,36' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003536' ,N'Goldhaut ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk3 36' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003537' ,N'Incendium Herba (Samen)' ,NULL ,N'Samen' ,N'Kräuter' ,N'übernatürliche Pflanze' ,NULL ,N'Uhrwerk4 37' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003538' ,N'Incendium Herba (Pulver)' ,NULL ,N'Pulver' ,N'Kräuter' ,N'übernatürliche Pflanze' ,NULL ,N'Uhrwerk4 37' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003539' ,N'Laufkraut ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,N'Preis außerhalb der Erntezeit das Dreifache' ,N'Uhrwerk4 37, 38' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003540' ,N'Lebensmoos ' ,NULL ,NULL ,N'Kräuter' ,N'Heilpflanze' ,NULL ,N'Uhrwerk4 38,39' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003541' ,N'Loruoor/Lebenskoralle ' ,NULL ,N'Portion' ,N'Kräuter' ,N'Heilpflanze/Giftpflanze' ,NULL ,N'Uhrwerk4 39' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003542' ,N'Nachtaugen ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk4 40' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003543' ,N'Sandklaue' ,NULL ,N'Stein' ,N'Kräuter' ,N'Wildpflanze/Nutzpflanze' ,N'Bei Waffen und Rüstungen aus dem Holz steigt der Preis x4, aber nicht unter 10 Au.' ,N'Uhrwerk4 40, 41, 42' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003544' ,N'Satyarenbaum ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk4 42' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003545' ,N'Schneemoos ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Uhrwerk4 42' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003546' ,N'Tanzbusch' ,NULL ,NULL ,N'Kräuter' ,N'Giftpflanze' ,NULL ,N'Uhrwerk4 43, 44' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003547' ,N'Tyrannenbaum' ,NULL ,N'Unze' ,N'Kräuter' ,N'übernatürliche Pflanze' ,N'Preis pro Unze bzw. 1 Blüte bzw. Samenflaum' ,N'Uhrwerk4 44, 45' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003548' ,N'Waldkönig' ,NULL ,N'Unze' ,N'Kräuter' ,N'übernatürliche Pflanze' ,N'Preis pro Unze bzw. 1 Blüte bzw. Samenflaum' ,N'Uhrwerk4 44, 45' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003549' ,N'Witwenmacher' ,NULL ,NULL ,N'Kräuter' ,N'Heilpflanze' ,NULL ,N'Uhrwerk4 45' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003550' ,N'Wolfsblatt' ,NULL ,NULL ,N'Kräuter' ,N'Wildpflanze/Nutzpflanze' ,N'Preis gilt für denn Winter und Frühjahr; im Spätsommer mehr' ,N'Uhrwerk4 46' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003551' ,N'Lauftrunk' ,NULL ,NULL ,N'Kräuterprodukte' ,N'Tränke' ,N'Besteht aus gekochten Blütenblättern des Laufkrauts' ,N'Uhrwerk4 38' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003552' ,N'Chrysirs Dauerlauf' ,NULL ,NULL ,N'Kräuterprodukte' ,N'Tränke' ,N'Besteht aus gekochten Blütenblättern des Laufkrauts' ,N'Uhrwerk4 38' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003553' ,N'Lebenspaste' ,NULL ,NULL ,N'Kräuterprodukte' ,N'Tränke' ,N'Lebenspaste besteht Lebensmoos' ,N'Uhrwerk4 39' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003554' ,N'Schwarmtrunk' ,NULL ,NULL ,N'Kräuterprodukte' ,N'Tränke' ,N'Schwarmtrunk wird aus Nachtaugen gewonnen' ,N'Uhrwerk4 40' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003555' ,N'Schlafender Tod' ,NULL ,NULL ,N'Gift' ,N'Kontaktgift (Schlaf 8)' ,NULL ,N'Uhrwerk3 32' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003556' ,N'Tanzender Tod' ,NULL ,NULL ,N'Gift' ,NULL ,NULL ,N'Uhrwerk4 44' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003557' ,N'Todestänzer' ,NULL ,NULL ,N'Gift' ,NULL ,NULL ,N'Uhrwerk4 44' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003558' ,N'Lebensbewahrer-Trank' ,NULL ,NULL ,N'Gift' ,N'Gegengift' ,NULL ,N'Uhrwerk4 45, 46' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003559' ,N'Albensöff' ,NULL ,NULL ,N'Tränke' ,NULL ,N'wie die Flüssigkeit in die das Pulver gegeben wird' ,N'Uhrwerk4 46' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003560' ,N'Toga aus dem Blütenkleid des Brajankelches ' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,NULL ,NULL ,N'Uhrwerk3 34' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003561' ,N'Tunika (ohne Ärmel) aus dem Blütenkleid des Brajankelches' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,NULL ,NULL ,N'Uhrwerk3 34' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003562' ,N'Balihoer Bärenbier' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Rund um den Neunaugensee bekannt. Selten getrunken, und die Brauerei in Baliho steht kurz vor der Pleite. Der Bärentod ist bekannt, das Bier eher weniger Dabei ist es von guter Qualität und rund um den Neunaugensee bekannt. Wird aber vom Ferdoker Übertrumpft. Schade eigentlich, denn dieses helle Bier ist wirklich trinkbar' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003563' ,N'Bodirtaler' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Angbodirtal und Umland. Aus einer kleinen Dorfbrauerei im Bodirtal kommt dieses kraftvolle thorwaler Schwarzbier. Für die örtichen Bauern und Fischer ein Genuss aber eben nur dort und allerhöchstens noch in Thorwal erhätlich.        ' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003564' ,N'Brabrakbräu' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Nur in Brabak und Umgebung erhältlich. Wenn dann nur von Bauern und Matrosen getrunken. Der Hit in jeder Partyrunde. Das es in Brabak sooo wenig Ratten gibt wird auch diesem Bier zugeschrieben. Ob Ratten Aber wirklich Bestandteil dieser dunklen, schweren Brühe sind ist ungeklärt. Äußerst gewöhnungsbedürftig und schwer. Nur in Brabak und Umgebung erhältlich. Die Brauerei ist so pleite wie die Schatzkammern von Brabak was allerdings nicht verwunderlich ist.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003565' ,N'Eslamsbräu/ Kaiserbier' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Überall in Aventurien zu finden. Eines der bekannteren Biere. Ein obergäriger Gerstenbier was nach dem Ferdoker das beliebeste Aventuriens ist. Es fällt allerdings vom Geschmack her nicht so gut aus wie selbiges, ist dafür auch etwas süßer (was im Vergleich zum bitteren Ferdoker aber auch nicht schwer ist) und billiger. ' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003566' ,N'Ferdoker Dunkel' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Nur in der Gegend um Ferdok. Verbreitet: V.a bei der Landbe völkerung beliebt. Dieses Ferdokerbier ist dunkel und süffig. Es ist etwas schwerer und für ungeübte Trinker nicht zu empfehlen. Aber trotzdem ein sehr leckeres Vollbier.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003567' ,N'Ferdoker Hell' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Bekanntestes Bier Aventuriens. Praktisch überall zu erhalten auch in guten Häusern angesehen. Das schnelle Helle ist überall in Aventurien Geschätzt und beliebt. Es ist leicht, sehr bitter und in jeder Kneipe im Mittlereich und nördlicher zu erhalten. Im Süden muss man schon eine Hafenkneipe besuchen um das bekannteste Bier Aventuriens zu bestellen' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003568' ,N'Goldenes Garether' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Im Mittelreich verbreitet v.a Gareth, Rommilys und Wehrheim. Das Bier der Hauptstadt ist goldgelb und von eher durschschnittlcher Qualität. Ein Bier für Städter schwach im Geschmack schwach im Alkohol' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003569' ,N'Gratenfelser Gagel' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Nur in der Gegend um Gratenfels und allerhöchstens noch Honingen zu erhalten. Bei der örtlichen Bevölkerung sehr beliebt.  Etwas ganz seltenes. Ein dunkler, rauchiges Bier was sehr gewöhnsbedürftig ist. Oder um es mit anderen Worten zu sagen: Die ersten 3 schmecken schlecht aber mit jedem folgenden wird es besser. Gilt als Spezilität, und kostet nur in Gratenfels 5 H                   ' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003570' ,N'Havennas Pralles' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Nur Havenna dort geschätzt v.a von Reisenden. Dieses Bier ist ein, mit einigen Kräuter versetzt . Sehr leichtes aber superbes Bier. Es ist nur in der  Brauereischänke „ Zum Prallen Netz“ in Havenna zu finden wo es von den Bürger gern getrunken wird . Ist allerdings dementsprechend teuer.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003571' ,N'Nordasker Weizengold' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Liebliches Feld und Mengbilla beliebt. Im Mittelreich praktisch nicht existent. Das in Mengibilla gebraute Weizen ist dort und im lieblichen Feld zu einer Alternative zum Ferdoker oder zum Kaiserbier geworden. Allerdings nur dort ansonsten wird das helle Weizen kaum getrunken und kaum bedacht.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003572' ,N'Phexmet' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Phexcaer und Umgebung eventuell noch Andergaster Raum oder Thorwal Dort aber von der Landbe bevölkerung geschätzt. Das in Phexcaer gebraute Bier nennt sich gern Bier der Diebe, wird allerdings meistens von den einheimischen Bauern getrunken. Es ist eher als schwach(Geschmach/Wirkung) zu beschreiben. Eher mindere billige Qualität' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003573' ,N'Quassetz' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Nur im Bornland erhältich  und dort mehr auf dem Land. Ist das Sauerbrottbier der Börnländer Bauern und Nivesen. Nur schwach alkohlisch und von den Thorwallern verächtlich Als Bornwasser bezeichnet ist es dennoch die Nr.1 im Bornland        beliebt und getrunken.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003574' ,N'Störrebrandt Privat ' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'In guten Hotels&Häusern v.a im Bornland erhätlich. Nobelbier. Eines der teueresten Biere Aventuriens aus Störrebrandts Privatbrauerei. Hell und Herb, ein Bierchen das man unter Geschäftkollegen mal trinken kann. Aufgrund der Bitterkeit gewöhnungsbedürftigt für Mittelreichische Seelen.' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003575' ,N'Waskir-Bier' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Zwischen Salza und Olport überall zu finden, im Mittel reich als zu stark befunden. Das Bier das Throwaler ist ein schweres, starkes Dinkelbier. Es ist (natürlich ) stark alkohlisch und für ungeübte Kehlen definitiv ungeeignet, Thorwaler allerdings benötigen keine Übung da sie der Legende nach nicht mit Milch sondern mit Waskir-Bier gesäugt worden sind' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003576' ,N'Zwergenbier' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'Selten und nur in den Zwergenkönigreichen und agrenzenden Städten vorhanden. Das Bier der Zwerge ist wohl nur in Angbar oder Xorlosch oder anderen von Zwergen geprägten Orten zu erhalten. Für Zwergenkönigreichen und die Zwerge das Beste vom Besten ist dieses obergäriges Hopfenbier für Menschen wohl nur als stinkende , undurchsichtige Brühe bekannt. Von den Thorwalern wird es häufig auch „ Kurzes“ genannt, ein Begriff dem man in einer Zwergenkneipe vermeiden sollte.  ' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003577' ,N'Zwergenbier II  ' ,NULL ,N'5 Schank' ,NULL ,N'Bier, Trinken' ,N'überall wo`s Zwerge gibt !!! Dieses Bier ist zwar auch von, aber nicht für Zwerge gebraut. Einige Braumeister ( v.a Angbar ) sind eben auf die Idee gekommen mit den Menschen noch etwas mehr Geld zu verdienen und brauen so ein leckeres, starkes Bier' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003578' ,N'Atholisbaum' ,NULL ,NULL ,N'Kräuter' ,N'Wildpflanze' ,NULL ,N'Myranor  260' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003579' ,N'Auijaja-Baum' ,NULL ,NULL ,N'Kräuter' ,N'Gefährlich' ,NULL ,N'Myranor  260' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003580' ,N'Azulie /Neristu-Rose' ,NULL ,NULL ,N'Kräuter' ,N'Droge' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003581' ,N'Belyabels Schleier' ,NULL ,NULL ,N'Kräuter' ,N'Parasit' ,NULL ,N'HuW 91' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003582' ,N'Boinurre' ,NULL ,NULL ,N'Kräuter' ,N'Gefährlich' ,NULL ,N'Myranor  260' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003583' ,N'Cassava' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze / Giftpflanze' ,NULL ,N'Myranor  260' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003584' ,N'Claqua-Rose' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  264' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003585' ,N'Geistblüten' ,NULL ,NULL ,N'Kräuter' ,N'Übernatürlich' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003586' ,N'Ghorlenklaue ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003587' ,N'Gyldaraswacht ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  264' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003588' ,N'Himmelszeder' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  260' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003589' ,N'Isnea' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  264' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003590' ,N'Jungfernhüter' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  264' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003591' ,N'Maulbaum' ,NULL ,NULL ,N'Kräuter' ,N'Gefährlich' ,NULL ,N'CM 86 / MyMo 124' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003592' ,N'Neretonie' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  260' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003593' ,N'Pardiriske' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  264' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003594' ,N'Pfeilsamenbaum ' ,NULL ,NULL ,N'Kräuter' ,N'Gefährlich' ,NULL ,N'HuW 90' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003595' ,N'Phrenophoren-Katkus ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'CM 69' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003596' ,N'Ramara-Diestel ' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  264' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003597' ,N'Spährentod' ,NULL ,NULL ,N'Kräuter' ,N'Giftpflanze' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003598' ,N'Sternapfelbaum' ,NULL ,NULL ,N'Kräuter' ,N'Heilpflanze' ,NULL ,N'CM 121' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003599' ,N'Tarnaille (-n) /Taschenblüte' ,NULL ,NULL ,N'Kräuter' ,N'Heilpflanze' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003600' ,N'Therkalbaum /Krüppelzeder' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  260' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003601' ,N'Tilians-Gruß' ,NULL ,NULL ,N'Kräuter' ,N'Giftpflanze' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003602' ,N'Wächter-Efeu' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003603' ,N'Wächterhecke' ,NULL ,NULL ,N'Kräuter' ,N'Nutzpflanze ' ,NULL ,N'Myranor  263' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003604' ,N'Leichte Ballista' ,200 ,NULL ,N'Belagerungswaffen' ,NULL ,NULL ,N'AA 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003605' ,N'Schwere Ballista' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,NULL ,N'AA 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003606' ,N'Harpunen-Ballista' ,330 ,NULL ,N'Belagerungswaffen' ,NULL ,NULL ,N'AA 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003607' ,N'Drachenzunge' ,1000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Gegen hölzerne Bauelemente beträgt der Anfangsschaden 1W+6 pro schuss, gefolgt von einem Brandschaden von 1W+4 TP pro SR. Gegen lebende Ziele beträgt der Folgeschaden 1W+4 pro Kampfrunde.' ,N'AA 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003608' ,N'Hornisse' ,500 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Das Befüllen des Trichtermagazins (üblicherweise 20 Schuss) erfordert 10 Aktionen, was mit einer FF-Probe auf 8 Aktionen verkürzt werden kann. Es ist möglich, die Hornisse zu laden, ohne das der Beschuss unterbrochen wird. Davon unabhängig dauert das Span' ,N'AA 50' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003609' ,N'Onager' ,500 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Onager benötigt eine Mindestreichweite von 100 Schritt.' ,N'AA 51' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003610' ,N'Schwerer Onager' ,1200 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Onager benötigt eine Mindestreichweite von 100 Schritt.' ,N'AA 51' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003611' ,N'Leichte Rotze ' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. Für auftreffende Kugeln mit Hylailer Feuer gilt jeweils der halbierte Schaden, zu dem noch 6W Brandauswirkungen ' ,N'AA 51' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003612' ,N'Mittlere Rotze ' ,500 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. Für auftreffende Kugeln mit Hylailer Feuer gilt jeweils der halbierte Schaden, zu dem noch 6W Brandauswirkungen ' ,N'AA 51' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003613' ,N'Schwere Rotze' ,700 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. Für auftreffende Kugeln mit Hylailer Feuer gilt jeweils der halbierte Schaden, zu dem noch 6W Brandauswirkungen ' ,N'AA 52' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003614' ,N'Überschwere Rotze ' ,1000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. Für auftreffende Kugeln mit Hylailer Feuer gilt jeweils der halbierte Schaden, zu dem noch 6W Brandauswirkungen ' ,N'AA 52' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003615' ,N'Leichter Skorpion' ,350 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Harpunen-Skorpion benötigt 8 SR mehr Aufbauzeit (Mechanik-Probe +10 zum verkürzen bzw. Nachjustieren) und 50 KR längere Ladezeit, Harpunenspeere kosten (incl. Seil) 12 Dukaten pro Stück.' ,N'AA 52, 53' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003616' ,N'Schwerer Skorpion' ,500 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Harpunen-Skorpion benötigt 8 SR mehr Aufbauzeit (Mechanik-Probe +10 zum verkürzen bzw. Nachjustieren) und 50 KR längere Ladezeit, Harpunenspeere kosten (incl. Seil) 12 Dukaten pro Stück.' ,N'AA 53' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003617' ,N'Harpunen Skorpion' ,550 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Harpunen-Skorpion benötigt 8 SR mehr Aufbauzeit (Mechanik-Probe +10 zum verkürzen bzw. Nachjustieren) und 50 KR längere Ladezeit, Harpunenspeere kosten (incl. Seil) 12 Dukaten pro Stück.' ,N'AA 53' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003618' ,N'Kleiner Zyklop' ,250 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Kleiner Zyklop benötigt eine Mindestreichweite von 150 Schritt. Ein großer Zyklop benötigt eine Mindestreichweite von 180 Schritt.' ,N'AA 53' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003619' ,N'Großer Zyklop' ,100 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Kleiner Zyklop benötigt eine Mindestreichweite von 150 Schritt. Ein großer Zyklop benötigt eine Mindestreichweite von 180 Schritt.' ,N'AA 53' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003620' ,N'Manuballista (myranisch)' ,200 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Zum Verschießen von Harpunen benötigt eine Carroballista eine zusätzliche Bedienungsperson und 30 KR längere Ladezeit; passende Harpunenspeere kosten (incl. Seil) 30 Argental pro Stück.' ,N'MyA 117, 118' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003621' ,N'Carroballista (myranisch)' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Zum Verschießen von Harpunen benötigt eine Carroballista eine zusätzliche Bedienungsperson und 30 KR längere Ladezeit; passende Harpunenspeere kosten (incl. Seil) 30 Argental pro Stück.' ,N'MyA 118' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003622' ,N'Harpunen-Carroballista (myranisch)' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Zum Verschießen von Harpunen benötigt eine Carroballista eine zusätzliche Bedienungsperson und 30 KR längere Ladezeit; passende Harpunenspeere kosten (incl. Seil) 30 Argental pro Stück.' ,N'MyA 118' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003623' ,N'Draglossa (myranisch)' ,100 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Gegen hölzerne Bauelemente beträgt der Anfangsschaden 1W+6 pro Schuss, gefolgt von einem Brandschaden von 1W+4 TP pro SR. Gegen lebende Ziele beträgt der Folgeschaden 1W+4 pro Kampfrunde.' ,N'MyA 119' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003624' ,N'Onager (myranisch)' ,100 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Onager benötigt eine Mindestreichweite von 100 Schritt.' ,N'MyA 119, 120' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003625' ,N'Polybela (myranisch)' ,500 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Das Befüllen des Trichtermagazins (üblicherweise 24 Schuss) erfordert 10 Aktionen, was mit einer FF-Probe auf 8 Aktionen verkürzt werden kann. Es ist möglich, die Polybela zu laden, ohne das der Beschuss unterbrochen wird. Davon unabhängig dauert das Span' ,N'MyA 120' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003626' ,N'Viertelgeschütz, Tormenta Quartana, serovisch Kartaune (myranisch) ' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. Für auftreffende Kugeln mit alchimistischen Elixieren gilt jeweils der halbierte Schaden, zu dem noch die zusätz' ,N'MyA 121' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003627' ,N'Mittelgeschütz, Tormenta Media, serovisch Metze (myranisch) ' ,600 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. Für auftreffende Kugeln mit alchimistischen Elixieren gilt jeweils der halbierte Schaden, zu dem noch die zusätz' ,N'MyA 121' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003628' ,N'Großgeschütz, Tormenta Magna, serovisch Maid (myranisch) ' ,1000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. Für auftreffende Kugeln mit alchimistischen Elixieren gilt jeweils der halbierte Schaden, zu dem noch die zusätz' ,N'MyA 121' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003629' ,N'Leichtes Donnerrohr (myranisch) (Steinkugeln)' ,1000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. ' ,N'MyA 118' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003630' ,N'Leichtes Donnerrohr (myranisch) ' ,1000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. ' ,N'MyA 118' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003631' ,N'Schweres Donnerrohr (myranisch) ' ,2000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. ' ,N'MyA 118' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003632' ,N'Überschweres Donnerrohr (myranisch) ' ,5000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Die Schadenswerte sind diejenigen für Granitkugeln; Eisenkugeln richten 3 TP bzw. 2 TP* mehr an, sind jedoch auch 20% teurer und 10% schwerer. ' ,N'MyA 119' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003633' ,N'Morfu mit leichten Pfeilen (riesländisch)' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,N'"Ein Treffer mit einem Morfu-Pfeil richtet automatisch eine Wunde an, sobald er beim Opfer
mehr als KO/2 SP (DSA4) bzw. KO/2-2 SP (DSA4.1) erzeugt (usw.)."' ,N'BdK 114' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003634' ,N'Morfu (riesländisch)' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,N'"Ein Treffer mit einem Morfu-Pfeil richtet automatisch eine Wunde an, sobald er beim Opfer
mehr als KO/2 SP (DSA4) bzw. KO/2-2 SP (DSA4.1) erzeugt (usw.)."' ,N'BdK 114' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003635' ,N'Schwere Gastraphete (riesländisch)' ,NULL ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Treffer mit der Schweren Gastraphete richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2 SP (DSA4) bzw. KO/2-2 SP (DSA4.1) erzeugt (usw.).' ,N'BdK 114, 115' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003636' ,N'Schwere Doppel-Gastraphete (riesländisch)' ,NULL ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Treffer mit der Schweren Gastraphete richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2 SP (DSA4) bzw. KO/2-2 SP (DSA4.1) erzeugt (usw.). Um bei einer Doppel- oder Dreifachgastrapheten das Ziel mit allen Pfeilen zutreffen ist eine F' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003637' ,N'Schwere Dreifach-Gastraphete (riesländisch)' ,NULL ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Treffer mit der Schweren Gastraphete richtet automatisch eine Wunde an, sobald er beim Opfer mehr als KO/2 SP (DSA4) bzw. KO/2-2 SP (DSA4.1) erzeugt (usw.). Um bei einer Doppel- oder Dreifachgastrapheten das Ziel mit allen Pfeilen zutreffen ist eine F' ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003638' ,N'Überschwere Ballista (riesländisch)' ,800 ,NULL ,N'Belagerungswaffen' ,NULL ,N'"Ein Treffer mit der Überschweren Ballista richtet automatisch eine Wunde an, sobald er beim
Opfer mehr als KO/2 SP (DSA4) bzw. KO/2-2 SP (DSA4.1) erzeugt (usw.)."' ,N'BdK 115' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003639' ,N'Überschwere Harpunen-Ballista (riesländisch)' ,800 ,NULL ,N'Belagerungswaffen' ,NULL ,N'"Ein Treffer mit der Überschweren Ballista richtet automatisch eine Wunde an, sobald er beim
Opfer mehr als KO/2 SP (DSA4) bzw. KO/2-2 SP (DSA4.1) erzeugt (usw.). Zum Verschießen von Harpunen wird eine zusätzliche Bedienungsperson und 30 KR längere Ladezei"' ,N'BdK 115' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003640' ,N'Leichte Ballista (riesländisch)' ,200 ,NULL ,N'Belagerungswaffen' ,NULL ,NULL ,N'BdK 115' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003641' ,N'Schwere Ballista (riesländisch)' ,300 ,NULL ,N'Belagerungswaffen' ,NULL ,NULL ,N'BdK 115' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003642' ,N'Drachenzunge (riesländisch)' ,1000 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Gegen hölzerne Bauelemente beträgt der Anfangsschaden 1W+6 pro schuss, gefolgt von einem Brandschaden von 1W+4 TP pro SR. Gegen lebende Ziele beträgt der Folgeschaden 1W+4 pro Kampfrunde.' ,N'BdK 115' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003643' ,N'Onager (riesländisch)' ,500 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Onager benötigt eine Mindestreichweite von 100 Schritt.' ,N'BdK 115' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur],  [Verpackungseinheit]) 
 VALUES ('00000000-0000-0000-002a-000000003644' ,N'Schwerer Onager (riesländisch)' ,1200 ,NULL ,N'Belagerungswaffen' ,NULL ,N'Ein Onager benötigt eine Mindestreichweite von 100 Schritt.' ,N'BdK 115' ,NULL);
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000642';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000643';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000644';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000645';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000646';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000647';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000648';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000650';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000652';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000653';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000654';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000655';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000656';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000657';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000658';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000659';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000660';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000661';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000662';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000621';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000622';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000623';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000624';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000625';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000626';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000627';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000628';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000629';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000630';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000631';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000632';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000633';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000634';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000635';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000636';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000637';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000638';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000639';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000640';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000641';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000706';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000707';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000708';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000709';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000710';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000711';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000712';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000713';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000714';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000715';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000716';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000717';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000718';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000719';
UPDATE [Handelsgut] SET [Name] = N'Cuina-Blüte (wild)' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002510';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002990';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002991';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002992';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002993';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002994';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002995';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002996';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002997';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002998';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002999';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003000';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' ,[Tags] = N'Likör' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003001';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' ,[Bemerkung] = N'Übernachtung' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003002';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003003';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003004';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003005';
UPDATE [Handelsgut] SET [Kategorie] = N'Taverne' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000003006';

-- Handelsgut_Setting
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000359' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000947' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000948' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000950' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000959' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000961' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000979' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000981' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000986' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000989' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000000999' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001000' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001002' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001005' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001006' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001008' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001015' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001016' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001017' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001019' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001025' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001037' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001048' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001049' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000001924' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003405' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003456' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003481' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003482' ,'00000000-0000-0000-5e77-000000000001' ,N'20' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003483' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003484' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003485' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003486' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003487' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003488' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003489' ,'00000000-0000-0000-5e77-000000000001' ,N'0,2' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003490' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003491' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003492' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003493' ,'00000000-0000-0000-5e77-000000000001' ,N'0,7' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003494' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003495' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003496' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003497' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003498' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003499' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003500' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003501' ,'00000000-0000-0000-5e77-000000000001' ,N'50' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003502' ,'00000000-0000-0000-5e77-000000000001' ,N'100' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003503' ,'00000000-0000-0000-5e77-000000000001' ,N'250' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003504' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003505' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003506' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003507' ,'00000000-0000-0000-5e77-000000000001' ,N'0,3' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003508' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003509' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003510' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003511' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003512' ,'00000000-0000-0000-5e77-000000000001' ,N'40' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003513' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003514' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003515' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003516' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003517' ,'00000000-0000-0000-5e77-000000000001' ,N'40' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003518' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003519' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003520' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003521' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003522' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003523' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003524' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003525' ,'00000000-0000-0000-5e77-000000000001' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003526' ,'00000000-0000-0000-5e77-000000000003' ,N'200' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003527' ,'00000000-0000-0000-5e77-000000000003' ,N'0,08' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003528' ,'00000000-0000-0000-5e77-000000000003' ,N'8' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003529' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003530' ,'00000000-0000-0000-5e77-000000000003' ,N'50' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003531' ,'00000000-0000-0000-5e77-000000000003' ,N'10' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003532' ,'00000000-0000-0000-5e77-000000000003' ,N'20' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003533' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003534' ,'00000000-0000-0000-5e77-000000000003' ,N'30' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003535' ,'00000000-0000-0000-5e77-000000000003' ,N'100' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003536' ,'00000000-0000-0000-5e77-000000000003' ,N'80' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003537' ,'00000000-0000-0000-5e77-000000000003' ,N'40' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003538' ,'00000000-0000-0000-5e77-000000000003' ,N'50' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003539' ,'00000000-0000-0000-5e77-000000000003' ,N'2' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003540' ,'00000000-0000-0000-5e77-000000000003' ,N'20' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003541' ,'00000000-0000-0000-5e77-000000000003' ,N'0,5' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003542' ,'00000000-0000-0000-5e77-000000000003' ,N'2' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003543' ,'00000000-0000-0000-5e77-000000000003' ,N'50' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003544' ,'00000000-0000-0000-5e77-000000000003' ,N'8' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003545' ,'00000000-0000-0000-5e77-000000000003' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003546' ,'00000000-0000-0000-5e77-000000000003' ,N'80' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003547' ,'00000000-0000-0000-5e77-000000000003' ,N'10' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003548' ,'00000000-0000-0000-5e77-000000000003' ,N'10' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003549' ,'00000000-0000-0000-5e77-000000000003' ,N'50' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003550' ,'00000000-0000-0000-5e77-000000000003' ,N'5' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003551' ,'00000000-0000-0000-5e77-000000000003' ,N'8' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003552' ,'00000000-0000-0000-5e77-000000000003' ,N'40' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003553' ,'00000000-0000-0000-5e77-000000000003' ,N'8' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003554' ,'00000000-0000-0000-5e77-000000000003' ,N'100' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003555' ,'00000000-0000-0000-5e77-000000000003' ,N'250' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003556' ,'00000000-0000-0000-5e77-000000000003' ,N'160' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003557' ,'00000000-0000-0000-5e77-000000000003' ,N'250' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003558' ,'00000000-0000-0000-5e77-000000000003' ,N'20' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003559' ,'00000000-0000-0000-5e77-000000000003' ,N'8' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003560' ,'00000000-0000-0000-5e77-000000000003' ,N'100' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003561' ,'00000000-0000-0000-5e77-000000000003' ,N'180' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003562' ,'00000000-0000-0000-5e77-000000000001' ,N'0,5 - 0,7' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003563' ,'00000000-0000-0000-5e77-000000000001' ,N'0,1' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003564' ,'00000000-0000-0000-5e77-000000000001' ,N'0,06' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003565' ,'00000000-0000-0000-5e77-000000000001' ,N'0,4 - 0,7' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003566' ,'00000000-0000-0000-5e77-000000000001' ,N'0,3' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003567' ,'00000000-0000-0000-5e77-000000000001' ,N'0,7-1' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003568' ,'00000000-0000-0000-5e77-000000000001' ,N'0,9 – 1,3 ' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003569' ,'00000000-0000-0000-5e77-000000000001' ,N'0,5' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003570' ,'00000000-0000-0000-5e77-000000000001' ,N'7' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003571' ,'00000000-0000-0000-5e77-000000000001' ,N'0,5 - 0,8' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003572' ,'00000000-0000-0000-5e77-000000000001' ,N'0,2' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003573' ,'00000000-0000-0000-5e77-000000000001' ,N'0,06 - 0,08' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003574' ,'00000000-0000-0000-5e77-000000000001' ,N'8-10' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003575' ,'00000000-0000-0000-5e77-000000000001' ,N'0,1 - 0,2' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003576' ,'00000000-0000-0000-5e77-000000000001' ,N'0,2' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003577' ,'00000000-0000-0000-5e77-000000000001' ,N'1 - 3' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003578' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003579' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003580' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003581' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003582' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003583' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003584' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003585' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003586' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003587' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003588' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003589' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003590' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003591' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003592' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003593' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003594' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003595' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003596' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003597' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003598' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003599' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003600' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003601' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003602' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003603' ,'00000000-0000-0000-5e77-000000000003' ,N'k.A.' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003604' ,'00000000-0000-0000-5e77-000000000001' ,N'8000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003605' ,'00000000-0000-0000-5e77-000000000001' ,N'10000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003606' ,'00000000-0000-0000-5e77-000000000001' ,N'10000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003607' ,'00000000-0000-0000-5e77-000000000001' ,N'50000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003608' ,'00000000-0000-0000-5e77-000000000001' ,N'1200' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003609' ,'00000000-0000-0000-5e77-000000000001' ,N'15000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003610' ,'00000000-0000-0000-5e77-000000000001' ,N'25000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003611' ,'00000000-0000-0000-5e77-000000000001' ,N'8000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003612' ,'00000000-0000-0000-5e77-000000000001' ,N'15000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003613' ,'00000000-0000-0000-5e77-000000000001' ,N'25000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003614' ,'00000000-0000-0000-5e77-000000000001' ,N'40000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003615' ,'00000000-0000-0000-5e77-000000000001' ,N'10000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003616' ,'00000000-0000-0000-5e77-000000000001' ,N'20000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003617' ,'00000000-0000-0000-5e77-000000000001' ,N'20000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003618' ,'00000000-0000-0000-5e77-000000000001' ,N'8000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003619' ,'00000000-0000-0000-5e77-000000000001' ,N'100000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003620' ,'00000000-0000-0000-5e77-000000000003' ,N'1500' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003621' ,'00000000-0000-0000-5e77-000000000003' ,N'2000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003622' ,'00000000-0000-0000-5e77-000000000003' ,N'2000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003623' ,'00000000-0000-0000-5e77-000000000003' ,N'10000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003624' ,'00000000-0000-0000-5e77-000000000003' ,N'40000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003625' ,'00000000-0000-0000-5e77-000000000003' ,N'250' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003626' ,'00000000-0000-0000-5e77-000000000003' ,N'1600' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003627' ,'00000000-0000-0000-5e77-000000000003' ,N'3000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003628' ,'00000000-0000-0000-5e77-000000000003' ,N'8000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003629' ,'00000000-0000-0000-5e77-000000000003' ,N'1000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003630' ,'00000000-0000-0000-5e77-000000000003' ,N'1000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003631' ,'00000000-0000-0000-5e77-000000000003' ,N'2500' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003632' ,'00000000-0000-0000-5e77-000000000003' ,N'5000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003633' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003634' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003635' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003636' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003637' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003638' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003639' ,'00000000-0000-0000-5e77-000000000004' ,NULL ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003640' ,'00000000-0000-0000-5e77-000000000004' ,N'8000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003641' ,'00000000-0000-0000-5e77-000000000004' ,N'10000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003642' ,'00000000-0000-0000-5e77-000000000004' ,N'50000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003643' ,'00000000-0000-0000-5e77-000000000004' ,N'15000' ,N'');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis],  [Name]) 
 VALUES ('00000000-0000-0000-002a-000000003644' ,'00000000-0000-0000-5e77-000000000004' ,N'25000' ,N'');
