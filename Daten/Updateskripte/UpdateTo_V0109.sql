-- Neue Struktur für Held_Ausrüstung-Beziehung, neue Tabelle Muntion_Setting

--PflanzenTyp in eigene Tabelle auslagern

--Zuerst die Tabelle anlegen
CREATE TABLE [Pflanze_Typ] (
	[PflanzeGUID] uniqueidentifier NOT NULL, 
	[Typ] nvarchar(200) NOT NULL,
	CONSTRAINT [PK_Pflanze_Typ] PRIMARY KEY ([PflanzeGUID], [Typ]), 
	FOREIGN KEY ([PflanzeGUID])
		REFERENCES [Pflanze] ([PflanzeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

--Anschließend die Typen eintragen
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000001',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000002',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000003',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000004',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000005',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000006',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000007',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000008',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000009',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000010',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000011',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000013',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000014',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000015',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000016',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000017',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000018',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000019',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000020',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000021',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000022',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000023',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000025',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000026',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000028',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000029',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000032',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000033',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000038',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000039',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000040',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000041',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000042',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000043',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000044',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000045',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000047',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000049',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000051',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000052',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000053',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000054',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000055',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000058',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000060',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000061',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000063',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000064',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000066',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000067',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000068',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000069',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000072',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000073',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000074',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000075',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000076',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000077',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000078',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000079',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000080',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000081',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000082',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000109',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000145',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000083',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000086',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000087',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000088',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000089',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000090',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000091',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000093',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000094',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000046',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000037',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000027',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000095',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000096',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000097',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000098',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000100',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000101',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000102',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000104',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000105',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000106',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000107',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000110',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000111',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000112',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000113',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000117',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000119',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000120',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000126',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000129',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000130',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000131',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000134',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000135',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000138',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000139',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000142',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'Wildpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000108',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000146',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000148',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000150',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000152',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000154',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000156',N'Wildpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000157',N'Wildpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000158',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000159',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000160',N'Parasit');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000161',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000162',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000163',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000164',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000165',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000166',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000167',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000168',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000169',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000170',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000171',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000172',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000173',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000174',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000175',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000176',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000177',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000178',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000179',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000180',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000181',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000182',N'Nutzpflanze ');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000183',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000185',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000186',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000187',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000188',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000190',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000191',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000192',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000194',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000195',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000196',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000197',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000199',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000200',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000201',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000202',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000203',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000204',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000205',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000206',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000207',N'Übernatürliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000208',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000199',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000156',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000124',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000115',N'Heilpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000209',N'Gefährliche Pflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000204',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000201',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000191',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000162',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000143',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000140',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000121',N'Parasit');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000120',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000118',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000092',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000085',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000084',N'Droge');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000070',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000057',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000056',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000038',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000036',N'Giftpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000035',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000034',N'Nutzpflanze');
GO
INSERT INTO [Pflanze_Typ] ([PflanzeGUID],[Typ]) VALUES (N'00000000-0000-0000-00ff-000000000030',N'Nutzpflanze');
GO

--Und die alten Spalte löschen
ALTER TABLE Pflanze DROP COLUMN Kategorie;


-- Spalten hinzufügen
ALTER TABLE Held_Ausrüstung ADD [Name] nvarchar(200) NULL;

--Held_Ausrüstung nur noch einmal JE ausrüstung.
ALTER TABLE Held_Ausrüstung DROP CONSTRAINT PK_Held_Ausrüstung;
ALTER TABLE Held_Ausrüstung ADD [HeldAusrüstungGUID] uniqueidentifier NOT NULL DEFAULT newid();
ALTER TABLE Held_Ausrüstung ADD CONSTRAINT PK_Held_Ausrüstung PRIMARY KEY (HeldAusrüstungGUID);
-- Held_Ausrüstung mit Anzahl > 1
--#WHILE;
SELECT COUNT(*) FROM Held_Ausrüstung WHERE Anzahl > 1 and Anzahl is not null;
--#DO;
INSERT INTO Held_Ausrüstung (HeldGUID, AusrüstungGUID, Angelegt, TalentGUID, BF, TrageortGUID)
SELECT 
		[HeldGUID],
		[AusrüstungGUID],
		Angelegt, TalentGUID, BF, TrageortGUID
	FROM [Held_Ausrüstung] where Anzahl > 1 and Anzahl is not null;
UPDATE Held_Ausrüstung SET Anzahl=Anzahl-1 WHERE  Anzahl > 1 and Anzahl is not null;
--#END;
ALTER TABLE Held_Ausrüstung DROP COLUMN Anzahl;

-- alte ForeignKeys löschen
ALTER TABLE Held_Ausrüstung DROP CONSTRAINT fk_HeldAusrüstung_Ausrüstung;
ALTER TABLE Held_Ausrüstung DROP CONSTRAINT fk_HeldAusrüstung_Talent;

-- Neue Tabellen anlegen
CREATE TABLE [Held_BFAusrüstung] (
	[HeldAusrüstungGUID] uniqueidentifier NOT NULL, 
	[BF] int NOT NULL DEFAULT 0, 
	[StartBF] int NOT NULL DEFAULT 0, 
	CONSTRAINT [pk_Held_BFAusrüstung] PRIMARY KEY ([HeldAusrüstungGUID]),
	CONSTRAINT [fk_Held_BFAusrüstung_Held_Ausrüstung] FOREIGN KEY ([HeldAusrüstungGUID])
		REFERENCES [Held_Ausrüstung] ([HeldAusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE [Held_Schild] (
	[HeldAusrüstungGUID] uniqueidentifier NOT NULL, 
	[SchildGUID] uniqueidentifier NOT NULL, 
	CONSTRAINT [pk_Held_Schild] PRIMARY KEY ([HeldAusrüstungGUID]),
	CONSTRAINT [fk_Held_Schild_Held_BFAusrüstung] FOREIGN KEY ([HeldAusrüstungGUID])
		REFERENCES [Held_BFAusrüstung] ([HeldAusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT [fk_Held_Schild_Schild] FOREIGN KEY ([SchildGUID])
		REFERENCES [Schild] ([SchildGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE [Held_Waffe] (
	[HeldAusrüstungGUID] uniqueidentifier NOT NULL, 
	[WaffeGUID] uniqueidentifier NOT NULL, 
	[TalentGUID] uniqueidentifier NOT NULL,
	[INI] int NOT NULL DEFAULT 0, 
	[TPBonus] int NOT NULL, 
	[WMAT] int NOT NULL DEFAULT 0, 
	[WMPA] int NOT NULL DEFAULT 0,
	CONSTRAINT [pk_Held_Waffe] PRIMARY KEY ([HeldAusrüstungGUID]),
	CONSTRAINT [fk_Held_Waffe_Held_Ausrüstung] FOREIGN KEY ([HeldAusrüstungGUID])
		REFERENCES [Held_BFAusrüstung] ([HeldAusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT [fk_Held_Waffe_Waffe] FOREIGN KEY ([WaffeGUID])
		REFERENCES [Waffe] ([WaffeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [fk_Held_Waffe_Talent] FOREIGN KEY ([TalentGUID])
		REFERENCES [Talent] ([TalentGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE [Held_Rüstung] (
	[HeldAusrüstungGUID] uniqueidentifier NOT NULL, 
	[RüstungGUID] uniqueidentifier NOT NULL,
	[Strukturpunkte] int NOT NULL DEFAULT 0,
	[StartStrukturpunkte] int NOT NULL DEFAULT 0,
	CONSTRAINT [pk_Held_Rüstung] PRIMARY KEY ([HeldAusrüstungGUID]),
	CONSTRAINT [fk_Held_Rüstung_Held_Ausrüstung] FOREIGN KEY ([HeldAusrüstungGUID])
		REFERENCES [Held_Ausrüstung] ([HeldAusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT [fk_Held_Rüstung_Rüstung] FOREIGN KEY ([RüstungGUID])
		REFERENCES [Rüstung] ([RüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE [Held_Fernkampfwaffe] (
	[HeldAusrüstungGUID] uniqueidentifier NOT NULL, 
	[FernkampfwaffeGUID] uniqueidentifier NOT NULL, 
	[TalentGUID] uniqueidentifier NOT NULL,
	[FKErleichterung] int NOT NULL DEFAULT 0, 
	[KKErleichterung] bit NOT NULL DEFAULT 0, 
	CONSTRAINT [pk_Held_Fernkampfwaffe] PRIMARY KEY ([HeldAusrüstungGUID]),
	CONSTRAINT [fk_Held_Fernkampfwaffe_Fernkampfwaffe] FOREIGN KEY ([FernkampfwaffeGUID])
		REFERENCES [Fernkampfwaffe] ([FernkampfwaffeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT [fk_Held_Fernkampfwaffe_Held_Ausrüstung] FOREIGN KEY ([HeldAusrüstungGUID])
		REFERENCES [Held_Ausrüstung] ([HeldAusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [fk_Held_Fernkampfwaffe_Talent] FOREIGN KEY ([TalentGUID])
		REFERENCES [Talent] ([TalentGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
);

-- Daten umkopieren
INSERT INTO Held_BFAusrüstung SELECT [HeldAusrüstungGUID], COALESCE(W.BF, S.BF, 0), COALESCE(W.BF, S.BF, 0) from Held_Ausrüstung HA LEFT JOIN Waffe W ON HA.AUsrüstungGUID=W.WaffeGUID LEFT JOIN Schild S on HA.AusrüstungGUID=S.SchildGUID where S.SchildGUID IS NOT NULL or W.WaffeGUID is NOT NULL;
INSERT INTO Held_Schild SELECT [HeldAusrüstungGUID], HA.AusrüstungGUID from Held_Ausrüstung HA, Schild W where HA.AusrüstungGUID=W.SchildGUID;
INSERT INTO Held_Waffe SELECT [HeldAusrüstungGUID], HA.AusrüstungGUID, HA.TalentGUID, W.INI, W.TPBonus, W.WMAT, W.WMPA from Held_Ausrüstung HA, Waffe W where HA.AusrüstungGUID=W.WaffeGUID;
INSERT INTO Held_Rüstung SELECT [HeldAusrüstungGUID], HA.AusrüstungGUID, COALESCE(RS*10, (2 * (Kopf + LBein + RBein) + 4.0 * (Bauch + Brust + Rücken) + (LArm + RArm)) / 20, 0),COALESCE(RS*10, (2 * (Kopf + LBein + RBein) + 4.0 * (Bauch + Brust + Rücken) + (LArm + RArm)) / 20, 0) from Held_Ausrüstung HA, Rüstung W where HA.AusrüstungGUID=W.RüstungGUID;
INSERT INTO Held_Fernkampfwaffe SELECT [HeldAusrüstungGUID], HA.AusrüstungGUID, HA.TalentGUID, 0, 0 from Held_Ausrüstung HA, Rüstung W where HA.AusrüstungGUID=W.RüstungGUID;


--Spalten löschen
ALTER TABLE Held_Ausrüstung DROP COLUMN AusrüstungGUID;
ALTER TABLE Held_Ausrüstung DROP COLUMN TalentGUID;
ALTER TABLE Held_Ausrüstung DROP COLUMN BF;


--Munition-Setting neu
CREATE TABLE [Munition_Setting] (
	[MunitionGUID] uniqueidentifier NOT NULL, 
	[SettingGUID] uniqueidentifier NOT NULL, 
	[Name] nvarchar(100) NULL, 
	[Preismodifikator] int NULL,
	CONSTRAINT [PK_Munition_Setting] PRIMARY KEY ([MunitionGUID], [SettingGUID]),
	CONSTRAINT [fk_Munition_Setting_Munition] FOREIGN KEY ([MunitionGUID])
		REFERENCES [Munition] ([MunitionGUID])
		ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [fk_Munition_Setting_Setting] FOREIGN KEY ([SettingGUID])
		REFERENCES [Setting] ([MunitionGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
);

--Altes Setting weg
ALTER TABLE Munition DROP COLUMN Setting;

--Daten
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000001','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000002','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000003','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000004','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000006','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000007','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000008','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000009','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000011','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000012','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000014','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000015','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000016','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000017','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000018','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000019','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000020','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000021','00000000-0000-0000-5e77-000000000001');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000001','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000002','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000003','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000004','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000006','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000007','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000008','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000009','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000011','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000012','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000014','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000015','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000016','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000017','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000018','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000019','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000020','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000021','00000000-0000-0000-5e77-000000000002');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000001','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000002','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000003','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID, Name) VALUES ('00000000-0000-0000-000f-000000000004','00000000-0000-0000-5e77-000000000003', 'Widerhaken-Pfeile');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000006','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000007','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000008','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000009','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000011','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID, Name) VALUES ('00000000-0000-0000-000f-000000000012','00000000-0000-0000-5e77-000000000003', 'Widerhaken-Bolzen');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000014','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000015','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000016','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000017','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000018','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000019','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000020','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000021','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000001','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000002','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000003','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000004','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000006','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000007','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000008','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000009','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000011','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000012','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000014','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000015','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000016','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000017','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000018','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000019','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000020','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000021','00000000-0000-0000-5e77-000000000004');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000001','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000002','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000003','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000004','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000006','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000007','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000008','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000009','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000011','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000012','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000014','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000015','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000016','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000017','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000018','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000019','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000020','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000021','00000000-0000-0000-5e77-000000000005');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000001','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000002','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000003','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000004','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000006','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000007','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000008','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000009','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000011','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000012','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000014','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000015','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000016','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000017','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000018','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000019','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000020','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000021','00000000-0000-0000-5e77-000000000006');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000022','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000025','00000000-0000-0000-5e77-000000000003');
INSERT INTO Munition_Setting (MunitionGUID, SettingGUID) VALUES ('00000000-0000-0000-000f-000000000010','00000000-0000-0000-5e77-000000000001');
UPDATE Munition SET Name = 'Kriegspfeil' WHERE MunitionGUID='00000000-0000-0000-000f-000000000004';
UPDATE Munition SET Name = 'Kriegsbolzen' WHERE MunitionGUID='00000000-0000-0000-000f-000000000012';
