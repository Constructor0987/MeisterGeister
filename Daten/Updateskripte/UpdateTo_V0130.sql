
-- HUE Lampen Szenen
CREATE TABLE [HUE_Szene] (
	[HUE_SzeneGUID] uniqueidentifier NOT NULL, 
	[Name] nvarchar(254),
	CONSTRAINT [PK_HUE_Szene] PRIMARY KEY ([HUE_SzeneGUID])
)
GO

CREATE TABLE [HUE_LampeColor] (
	[HUE_LampeColorGUID] uniqueidentifier NOT NULL, 
	[Lampenname] nvarchar(254) NOT NULL,
	[Color] nvarchar(32),
	CONSTRAINT [PK_HUE_LampeColor] PRIMARY KEY ([HUE_LampeColorGUID])
)
GO

CREATE TABLE [HUE_Szene_LampeColor] (
	[HUE_SzeneGUID] uniqueidentifier NOT NULL , 
	[HUE_LampeColorGUID] uniqueidentifier NOT NULL , 
	FOREIGN KEY ([HUE_SzeneGUID])
		REFERENCES [HUE_Szene] ([HUE_SzeneGUID])
		ON UPDATE NO ACTION ON DELETE CASCADE, 
	FOREIGN KEY ([HUE_LampeColorGUID])
		REFERENCES [HUE_LampeColor] ([HUE_LampeColorGUID])
		ON UPDATE NO ACTION ON DELETE CASCADE
)
GO

-- Add Audio-Theme Verknüpfung
ALTER TABLE [Audio_Theme] ADD [HUE_SzeneGUID] uniqueidentifier NULL
GO

ALTER TABLE [Audio_Theme] ADD 
	FOREIGN KEY ([HUE_SzeneGUID])
		REFERENCES [HUE_Szene] ([HUE_SzeneGUID])
		ON UPDATE NO ACTION ON DELETE CASCADE
		
GO