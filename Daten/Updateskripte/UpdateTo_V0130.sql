
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
	[HUE_SzeneGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[HUE_LampeColorGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	CONSTRAINT [PK_HUE_Szene_LampeColor] PRIMARY KEY ([HUE_SzeneGUID], [HUE_LampeColorGUID]), 
	CONSTRAINT [FK_HUE_Szene_LampeColor_HUE_Szene] FOREIGN KEY ([HUE_SzeneGUID]) 
		REFERENCES [HUE_Szene] ([HUE_SzeneGUID])
		ON UPDATE NO ACTION ON DELETE CASCADE, 
	CONSTRAINT [FK_HUE_Szene_LampeColor_HUE_LampeColor] FOREIGN KEY ([HUE_LampeColorGUID]) 
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
