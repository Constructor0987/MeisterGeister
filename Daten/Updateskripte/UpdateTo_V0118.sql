CREATE TABLE [Ausrüstungsset] (
	[AusrüstungssetGUID] uniqueidentifier NOT NULL DEFAULT newid() PRIMARY KEY, 
	[Name] nvarchar(200)
)
-- Held_Zauber um Hauszauber ergänzen
ALTER TABLE Held_Zauber add Hauszauber bit DEFAULT 0
GO

CREATE TABLE [Held_Ausrüstung_Ausrüstungsset] (
	[HeldAusrüstungGUID] uniqueidentifier NOT NULL,
	[AusrüstungssetGUID] uniqueidentifier NOT NULL,
	CONSTRAINT [PK_Held_Ausrüstung_Ausrüstungsset] PRIMARY KEY ([HeldAusrüstungGUID], [AusrüstungssetGUID]), 
	FOREIGN KEY ([AusrüstungssetGUID])
		REFERENCES [Ausrüstungsset] ([AusrüstungssetGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	FOREIGN KEY ([HeldAusrüstungGUID])
		REFERENCES [Held_Ausrüstung] ([HeldAusrüstungGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO--Panzerstecher hat TP/KK 13/3 und nicht 12/3
UPDATE Waffe SET TPKKSchwelle = 13 WHERE WaffeGUID = '00000000-0000-0000-0001-000000000094'
GO
UPDATE Ausrüstung SET Literatur = 'AA 28 / AA Errata 2' WHERE AusrüstungGUID = '00000000-0000-0000-0001-000000000094'
GO
