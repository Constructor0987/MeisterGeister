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

--TODO MT: Datenanpassungen Literatur


--TODO MT: Anpassungen für DSA5