--gehärtete sätze aus munitionstabelle löschen, vollständig kategorisierte rüstungen, aktualisierte handelsgüter

/* Ausrüstung */

UPDATE [Ausrüstung] SET [Bemerkung] = N'Kälterüstungsschutz: 3, BE: 0' ,[Tags] = N'Kälteschutz, Winterkleidung' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000021';
UPDATE [Ausrüstung] SET [Name] = N'hervorragender Ringmantel (Brabakmantel)' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000142';
UPDATE [Ausrüstung] SET [Name] = N'hervorragende Kettenbeinlinge, Paar' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000144';
UPDATE [Ausrüstung] SET [Name] = N'hervorragende Kettenhandschuhe, Paar' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000145';
UPDATE [Ausrüstung] SET [Name] = N'hervorragende Kettenhaube' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000146';
UPDATE [Ausrüstung] SET [Name] = N'hervorragendes Kettenzeug' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000147';
UPDATE [Ausrüstung] SET [Name] = N'hervorragende Kettenhaube, mit Gesichtsschutz' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000148';
UPDATE [Ausrüstung] SET [Name] = N'hervorragendes Kettenhemd, 1/2 Arm' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000149';
UPDATE [Ausrüstung] SET [Name] = N'hervorragendes Kettenhemd, lang' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000150';
UPDATE [Ausrüstung] SET [Name] = N'hervorragender Kettenmantel' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000151';
UPDATE [Ausrüstung] SET [Name] = N'hervorragende Kettenweste' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000152';
UPDATE [Ausrüstung] SET [Name] = N'hervorragender Ringelpanzer' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000153';
UPDATE [Ausrüstung] SET [Name] = N'hervorragender Spiegelpanzer' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000154';
UPDATE [Ausrüstung] SET [Name] = N'hervorragender Kettenkragen' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000156';
UPDATE [Ausrüstung] SET [Name] = N'hervorragende Löwenmähne' WHERE [AusrüstungGUID]='00000000-0000-0000-0004-000000000157';


/* Handelsgut */

INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003301' ,N'Kaputzenmanetel, lang ' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Trödler oder Krämer) (schweres Wolltuch) ; Kälterüstungsschutz: 3, BE: 1' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003302' ,N'Mantel, kurz (Wolle)' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Trödler oder Krämer)Kälterüstungsschutz: 2, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003303' ,N'Pelzgefütterte Stiefel' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Trödler oder Krämer)Kälterüstungsschutz: 2, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003304' ,N'Unterzeug' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Trödler oder Krämer)Kälterüstungsschutz: 1, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003309' ,N'Kapuzenmanetel, lang ' ,80 ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Maßgeschneidert) (schweres Wolltuch) ; Kälterüstungsschutz: 3, BE: 1' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003310' ,N'Mantel, kurz (Wolle)' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Maßgeschneidert) Kälterüstungsschutz: 2, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003311' ,N'Pelzgefütterte Stiefel' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Maßgeschneidert) Kälterüstungsschutz: 2, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003312' ,N'Unterzeug' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Maßgeschneidert) Kälterüstungsschutz: 1, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003317' ,N'Beinkleider, Fell' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Trödler oder Krämer) ; Kälterüstungsschutz: 2, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003318' ,N'Beinkleider, Fell' ,NULL ,NULL ,N'Kleidung & Schuhwerk' ,N'Kälteschutz, Winterkleidung' ,N'(Maßgeschneidert) ; Kälterüstungsschutz: 2, BE: 0' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003319' ,N'aranisches Früchtebrot' ,NULL ,NULL ,N'Nahrungs- & Genussmittel' ,NULL ,N'Nahrungsmittel, teuer' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003320' ,N'Zierlamuchenfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'verbreitet' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003321' ,N'Lamukfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'(billige Felle, Häute & Pelze) ' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003322' ,N'Alpschmeichlerfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003323' ,N'Pökelfleisch' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel
' ,NULL ,N'Fleisch, einfach' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003324' ,N'Blutbüffelfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'verbreitet' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003325' ,N'Schweinespeck' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel' ,NULL ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003326' ,N'Munarifell (Sommerfell)' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'verbreitet' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003327' ,N'Kohl' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel' ,NULL ,N'Nahrungsmittel, einfach' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003328' ,N'Miraghfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'teuer' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003329' ,N'Shadarfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus, Preis, je nach Zeichnung' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003331' ,N'Nahrungs- & Genussmittel' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel' ,NULL ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003332' ,N'Wildzebrafell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'teuer' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003333' ,N'Aprikosen' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel
' ,NULL ,N'Nahrungsmittel, teuer' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003334' ,N'Silberpelzfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'teuer, Fell der Samtratte, über Vinshina zu beziehen' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003335' ,N'Mandrassfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003336' ,N'Riesenrenosterfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'teuer' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003337' ,N'Ghaal Haut' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'verbreitet' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003338' ,N'Nantharfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003339' ,N'Grubentotter Haut' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003340' ,N'Shirdrak Haut' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003341' ,N'Eisenwolffell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003342' ,N'Troll- und Mostbirnen' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel
' ,NULL ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003343' ,N'Levius-Hirschfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003344' ,N'Ghorlafell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003345' ,N'Maultierfleisch' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel' ,NULL ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003346' ,N'Liebfelder Purpurmeisen' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel
' ,NULL ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003347' ,N'Schmalz' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel' ,NULL ,NULL ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003348' ,N'Jharranoth Haut' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'teuer' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003349' ,N'Raubchamäleonfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003350' ,N'Amarellen' ,40 ,N'Stein' ,N'Nahrungs- & Genussmittel
' ,NULL ,N'Sauerkirschen, Nahrungsmittel, teuer' ,NULL);
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003351' ,N'Danthrekifell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'verbreitet' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003352' ,N'Munari-Winterfell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'teuer' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003353' ,N'Zingdurufell' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'luxus' ,N'Myranor 280');
INSERT INTO [Handelsgut] (  [HandelsgutGUID],  [Name],  [Gewicht],  [ME],  [Kategorie],  [Tags],  [Bemerkung],  [Literatur]) 
 VALUES ('00000000-0000-0000-002a-000000003354' ,N'Hurdarfell/ Panzernasenschwein Fell ' ,NULL ,N'Rechtschritt' ,N'Häute, Felle & Pelze' ,NULL ,N'verbreitet' ,N'Myranor 280');
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000058';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000060';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000062';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000068';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000072';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000073';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000078';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000095';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000098';
UPDATE [Handelsgut] SET [Gewicht] = 40 WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000100';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000107';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000127';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000129';
UPDATE [Handelsgut] SET [ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000130';
UPDATE [Handelsgut] SET [Bemerkung] = N'Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000683';
UPDATE [Handelsgut] SET [ME] = N'' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000634';
UPDATE [Handelsgut] SET [Bemerkung] = N'(Maßgeschneidert), Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000748';
UPDATE [Handelsgut] SET [Bemerkung] = N' (Trödler oder Krämer), Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000749';
UPDATE [Handelsgut] SET [Bemerkung] = N'(Maßgeschneidert), Kälterüstungsschutz: 2, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000754';
UPDATE [Handelsgut] SET [Bemerkung] = N' (Trödler oder Krämer), Kälterüstungsschutz: 2, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000755';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' ,[Bemerkung] = N'(Trödler oder Krämer) (schweres Wolltuch) ; Kälterüstungsschutz: 3, BE: 1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000758';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' ,[Bemerkung] = N'Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000795';
UPDATE [Handelsgut] SET [Bemerkung] = N'Kälterüstungsschutz: 1, BE: 0, FF-5' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000802';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000803';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000804';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' ,[Bemerkung] = N'(Maßgeschneidert), Kälterüstungsschutz: 3, BE: 1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000805';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' ,[Bemerkung] = N'(Trödler oder Krämer), Kälterüstungsschutz: 3, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000806';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' ,[Bemerkung] = N' (Maßgeschneidert), Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000828';
UPDATE [Handelsgut] SET [Tags] = N'Kälteschutz, Winterkleidung' ,[Bemerkung] = N' (Trödler oder Krämer), Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000829';
UPDATE [Handelsgut] SET [Tags] = N'Lager, Wildnisleben, Kälteschutz, Winterkleidung' ,[Bemerkung] = N'(Maßgeschneidert), Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000839';
UPDATE [Handelsgut] SET [Tags] = N'Lager, Wildnisleben, Kälteschutz, Winterkleidung' ,[Bemerkung] = N'(Trödler oder Krämer), Kälterüstungsschutz: 1, BE: 0' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000000840';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001344';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001352';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001367';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001369';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001373';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001388';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001418';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001419';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001420';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001438';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001440';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001442';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'Hafer/ Wildgetreide, ungeschrotet' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001457';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001459';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001463';
UPDATE [Handelsgut] SET [Name] = N'geröstete Kastanien' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001467';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001468';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001470';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001475';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001477';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001490';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001494';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001495';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001503';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001509';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001513';
UPDATE [Handelsgut] SET [Name] = N'Ikra ' ,[Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'Rogen des Bornstörs' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001520';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001529';
UPDATE [Handelsgut] SET [Name] = N'Karpfen ' ,[Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'zum Verzehr' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001536';
UPDATE [Handelsgut] SET [Name] = N'Kirschen' ,[Bemerkung] = N'(wild Vogelkirsche)' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001543';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001555';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001557';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001563';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001574';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001598';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001613';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001616';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001621';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001623';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001646';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001651';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001654';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001658';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001660';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001664';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001670';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001671';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001676';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001680';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001681';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'Kuhmilch' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001684';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001687';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001694';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001696';
UPDATE [Handelsgut] SET [Name] = N'Rind, Schaf, Gans, Truthahn, Pökelfleisch (meist Rindfleisch), Schweinespeck, Schinken, Schmalz, Pemmikan ' ,[Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'(gemahlenes Fleisch, zumeist vom Karen)' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001697';
UPDATE [Handelsgut] SET [Name] = N'Roter Krebs ' ,[Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'ellenlang' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001708';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001712';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001714';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001718';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001719';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001721';
UPDATE [Handelsgut] SET [ME] = N'Stein' ,[Bemerkung] = N'Roggen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001723';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001727';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001735';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001737';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001742';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001743';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001744';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001745';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Tags] = N'Salami' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001749';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001757';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001762';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001764';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001765';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001776';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001777';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001778';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'Tabu für Swafnirgläubige' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001805';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'aus Kuhmilch; Schimmel- oder Schmierkäse' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001811';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001814';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001816';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001817';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001819';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001827';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001829';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001830';
UPDATE [Handelsgut] SET [Name] = N'Zuckerbrot' ,[Gewicht] = 40 ,[ME] = N'Stein' ,[Bemerkung] = N'Weizen' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001834';
UPDATE [Handelsgut] SET [Bemerkung] = N'aus Weizenschrot' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001843';
UPDATE [Handelsgut] SET [Bemerkung] = N'aus Weizenschrot' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001844';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002393';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002394';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002395';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002396';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002397';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002398';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002399';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002400';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002401';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002402';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002403';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002404';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002405';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002406';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002407';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002408';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002409';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002410';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002411';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002412';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002413';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002414';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002415';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002416';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002417';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002418';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002419';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002420';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002421';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002458';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002459';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002460';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002461';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002462';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002463';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002464';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002465';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002466';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002467';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002468';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002469';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002470';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002471';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002472';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002473';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002474';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002475';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002476';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002477';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002454';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002455';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002456';
UPDATE [Handelsgut] SET [Kategorie] = N'Kleidung & Schuhwerk' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002457';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002571';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002572';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002573';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002574';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002575';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002576';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002577';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002578';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002579';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002580';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002581';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002582';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002583';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002584';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002610';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002611';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002612';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002613';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002614';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002615';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002616';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002617';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002618';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002619';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002620';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002621';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002622';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002623';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002624';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002625';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002626';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002627';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002628';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002629';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002630';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002631';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002632';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002633';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002634';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002635';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002636';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002585';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002586';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002587';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002588';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002589';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002590';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002591';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002592';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002593';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002594';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002595';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002596';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002597';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002598';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002599';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002600';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002601';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002602';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002603';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002604';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002605';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002606';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002607';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002608';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002609';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002637';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002638';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002639';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002640';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002641';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002642';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002643';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002644';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002645';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002646';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002647';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002648';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002649';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002650';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002651';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002652';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002653';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002654';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002655';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002656';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002657';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002658';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002659';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002660';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002661';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002662';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002663';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002664';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002665';
UPDATE [Handelsgut] SET [Gewicht] = 40 ,[ME] = N'Stein' ,[Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002666';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002667';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002668';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002669';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002670';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002671';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002672';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002673';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002674';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002675';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002676';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002677';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002678';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002679';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002680';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002681';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002682';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002683';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002684';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002685';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002686';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002687';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002688';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002689';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002690';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002691';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002692';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002693';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002694';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002695';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002696';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002697';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002698';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002699';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002700';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002701';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002702';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002703';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002704';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002705';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002706';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002707';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002708';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002709';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002710';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002711';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002712';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002713';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002714';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002715';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002716';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002717';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002718';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002719';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002720';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002721';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002722';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002723';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002724';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002725';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002726';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002727';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002728';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002729';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002730';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002731';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002732';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002733';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002734';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002735';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002736';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002737';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002738';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002739';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002740';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002741';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002742';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002743';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002744';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002745';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002746';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002747';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002748';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002749';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002750';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002751';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002752';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002753';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002754';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002755';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002756';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002757';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002758';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002759';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002760';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002761';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002762';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002763';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002764';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002765';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002766';
UPDATE [Handelsgut] SET [Kategorie] = N'Nahrungs- & Genussmittel
' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002767';
UPDATE [Handelsgut] SET [Kategorie] = N'Seile, Netze, Ketten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002854';
UPDATE [Handelsgut] SET [Kategorie] = N'Seile, Netze, Ketten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002855';
UPDATE [Handelsgut] SET [Kategorie] = N'Seile, Netze, Ketten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002856';
UPDATE [Handelsgut] SET [Kategorie] = N'Seile, Netze, Ketten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002857';
UPDATE [Handelsgut] SET [Kategorie] = N'Seile, Netze, Ketten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002858';
UPDATE [Handelsgut] SET [Kategorie] = N'Seile, Netze, Ketten' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002859';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002989';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002971';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002972';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002973';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002974';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002975';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002976';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002977';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002978';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002979';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002980';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002981';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002982';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002983';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002984';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002985';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002986';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002987';
UPDATE [Handelsgut] SET [Kategorie] = N'Spielzeug, Dekoration, Luxusartikel und religiöse Gegenstände' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000002988';
UPDATE [Handelsgut] SET [HandelsgutGUID]='00000000-0000-0000-002a-000000003330' WHERE [HandelsgutGUID]='d5e75160-c73d-4615-bfa9-45eaee810567';

/* Handelsgut_Setting */

INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003302' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003302' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003302' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003303' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003303' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003303' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003304' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003304' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003304' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003309' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003309' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003309' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003310' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003310' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003310' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003311' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003311' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003311' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003312' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003312' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003312' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003317' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003317' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003317' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003318' ,'00000000-0000-0000-5e77-000000000001' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003318' ,'00000000-0000-0000-5e77-000000000002' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003318' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003319' ,'00000000-0000-0000-5e77-000000000001' ,N'1-5');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003320' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003321' ,'00000000-0000-0000-5e77-000000000003' ,N'0,1-50');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003322' ,'00000000-0000-0000-5e77-000000000003' ,N'40');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003323' ,'00000000-0000-0000-5e77-000000000001' ,N'1-2');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003324' ,'00000000-0000-0000-5e77-000000000003' ,N'1-15');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003326' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003327' ,'00000000-0000-0000-5e77-000000000001' ,N'0,1-0,05');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003328' ,'00000000-0000-0000-5e77-000000000003' ,N'1');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003329' ,'00000000-0000-0000-5e77-000000000003' ,N'4');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003332' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003333' ,'00000000-0000-0000-5e77-000000000001' ,N'1-5');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003334' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003335' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003336' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003337' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003338' ,'00000000-0000-0000-5e77-000000000003' ,N'10');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003339' ,'00000000-0000-0000-5e77-000000000003' ,N'6');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003340' ,'00000000-0000-0000-5e77-000000000003' ,N'35');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003341' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003342' ,'00000000-0000-0000-5e77-000000000001' ,N'0,03-0,09');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003343' ,'00000000-0000-0000-5e77-000000000003' ,N'35');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003344' ,'00000000-0000-0000-5e77-000000000003' ,N'25');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003345' ,'00000000-0000-0000-5e77-000000000001' ,N'0,6-0,9');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003346' ,'00000000-0000-0000-5e77-000000000001' ,N'6');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003347' ,'00000000-0000-0000-5e77-000000000001' ,N'1-2');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003348' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003349' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003350' ,'00000000-0000-0000-5e77-000000000001' ,N'1-5');
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003351' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003352' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003353' ,'00000000-0000-0000-5e77-000000000003' ,NULL);
INSERT INTO [Handelsgut_Setting] (  [HandelsgutGUID],  [SettingGUID],  [Preis]) 
 VALUES ('00000000-0000-0000-002a-000000003354' ,'00000000-0000-0000-5e77-000000000003' ,N'3');
UPDATE [Handelsgut_Setting] SET [Preis] = N'0,6-1,1' WHERE [HandelsgutGUID]='00000000-0000-0000-002a-000000001816' AND [SettingGUID]='00000000-0000-0000-5e77-000000000001';

/* Munition */

DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000005';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000013';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000023';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000024';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000026';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000027';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000028';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000029';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000030';
DELETE FROM [Munition] WHERE [MunitionGUID]='00000000-0000-0000-000f-000000000031';


/* Rüstung */

UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000012';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000013';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000020';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000022';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000030';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000031';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000032';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000033';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000034';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000035';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000036';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000037';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000038';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000039';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000050';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000061';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000064';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000065';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000066';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000076';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000077';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000078';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000079';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000080';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000081';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000082';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000083';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000084';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000085';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000086';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000087';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000088';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000089';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000090';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000091';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000093';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000094';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000095';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000096';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000097';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000098';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000099';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000100';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000101';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000102';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000103';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000104';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000105';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000106';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000108';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000109';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000110';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000111';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000112';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000113';
UPDATE [Rüstung] SET [Gruppe] = N'Kleidung' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000114';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000115';
UPDATE [Rüstung] SET [Gruppe] = N'Kette' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000116';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000118';
UPDATE [Rüstung] SET [Gruppe] = N'Schuppe' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000119';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000120';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000121';
UPDATE [Rüstung] SET [Gruppe] = N'Lederrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000122';
UPDATE [Rüstung] SET [Gruppe] = N'Tuchrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000123';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000124';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000125';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000126';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000127';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000128';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000130';
UPDATE [Rüstung] SET [Gruppe] = N'Tuchrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000131';
UPDATE [Rüstung] SET [Gruppe] = N'Kleidung' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000133';
UPDATE [Rüstung] SET [Gruppe] = N'Tuchrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000135';
UPDATE [Rüstung] SET [Gruppe] = N'Tuchrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000136';
UPDATE [Rüstung] SET [Gruppe] = N'Kleidung' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000138';
UPDATE [Rüstung] SET [Gruppe] = N'Tuchrüstungen' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000139';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000140';
UPDATE [Rüstung] SET [Gruppe] = N'Plattenrüstungen' ,[Steif] = 1 WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000141';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000142';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000144';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000145';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000146';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000147';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000148';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000149';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000150';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000151';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000152';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000153';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000154';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000156';
UPDATE [Rüstung] SET [Gruppe] = N'hervorragende Kette' WHERE [RüstungGUID]='00000000-0000-0000-0004-000000000157';


/* Sonderfertigkeit */

INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001457' ,N'Pfadgespür' ,0 ,N'Kampf' ,N'Rakshazar 22' ,N'Gabe Magiegespür oder sonstige magische Wahrnehmung; mind. 2 Jahre Lernzeit innerhalb eines Limbischen Pfads');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001458' ,N'Abgebrühtheit' ,0 ,N'Allgemein' ,N'Rakshazar 20' ,N'MU 14');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001459' ,N'Aufgesessener Kämpfer I' ,0 ,N'Kampf' ,N'Rakshazar 21' ,N'Körperbeherrschung 7');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001460' ,N'Barbarische Drohgebärden' ,0 ,N'Kampf' ,N'Rakshazar 21, 22' ,N'MU 14, GE 15 | KK 15 | KO 15 | CH 15, Überreden 10');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001461' ,N'Defensiver Monsterkampf I' ,0 ,N'Kampf' ,N'Rakshazar 22' ,N'SF Abgebrühtheit, MU 14');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001462' ,N'Defensiver Monsterkampf II' ,0 ,N'Kampf' ,N'Rakshazar 22' ,N'SF Defensiver Monsterkampf I, MU 16, KO 15, PA 9');
INSERT INTO [Sonderfertigkeit] (  [SonderfertigkeitGUID],  [Name],  [HatWert],  [Typ],  [Literatur],  [Voraussetzungen]) 
 VALUES ('00000000-0000-0000-005f-000000001463' ,N'Defensiver Monsterkampf III' ,0 ,N'Kampf' ,N'Rakshazar 22' ,N'SF Defensiver Monsterkampf II, MU 18, KO 16, PA 10');
UPDATE [Sonderfertigkeit] SET [Name] = N'Stabzauber: Doppeltes Maß/Stabverlängerung' WHERE [SonderfertigkeitGUID]='00000000-0000-0000-005f-000000000427';


/* Sonderfertigkeit_Setting */

INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001457' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001458' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001459' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001460' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001461' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001462' ,'00000000-0000-0000-5e77-000000000004' ,NULL);
INSERT INTO [Sonderfertigkeit_Setting] (  [SonderfertigkeitGUID],  [SettingGUID],  [Verbreitung]) 
 VALUES ('00000000-0000-0000-005f-000000001463' ,'00000000-0000-0000-5e77-000000000004' ,NULL);

