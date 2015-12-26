CREATE TABLE [Ausrüstungsset] (
	[AusrüstungssetGUID] uniqueidentifier NOT NULL DEFAULT newid() PRIMARY KEY, 
	[Name] nvarchar(200)
)
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
GO