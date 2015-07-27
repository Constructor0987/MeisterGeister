-- Korrektur Basardaten
UPDATE [Handelsgut] SET [Gewicht] = 0.4, [Name] = 'Kohlestift' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001953';
UPDATE [Handelsgut_Setting] SET [Preis] = '0,01' WHERE [HandelsgutGUID] = '00000000-0000-0000-002a-000000001953';
-- TODO: evtl. noch weitere Handelsgüter umrechnen

--TODO MT: Datenanpassungen Literatur


--TODO MT: Anpassungen für DSA5