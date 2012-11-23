-- Held_Inventar und Held_Munition an die Trageort-Tabelle angehängt
DROP TABLE Held_Munition
GO

CREATE TABLE [Held_Munition] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[FernkampfwaffeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[MunitionGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[TrageortGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-001A-000000000011',
	[Anzahl] int,
	CONSTRAINT [PK_Held_Munition] PRIMARY KEY ([HeldGUID], [FernkampfwaffeGUID], [MunitionGUID], [TrageortGUID]), 
	CONSTRAINT fk_HeldMunition_Fernkampfwaffe FOREIGN KEY ([FernkampfwaffeGUID])
		REFERENCES Fernkampfwaffe ([FernkampfwaffeGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_HeldMunition_Held FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_HeldMunition_Munition FOREIGN KEY ([MunitionGUID])
		REFERENCES Munition ([MunitionGUID])
		ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT fk_HeldMunition_TrageortGUID FOREIGN KEY ([TrageortGUID])
		REFERENCES Trageort ([TrageortGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

DROP TABLE Held_Inventar
GO

CREATE TABLE [Held_Inventar] (
	[HeldGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Angelegt] bit NOT NULL DEFAULT 0, 
	[TrageortGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-001A-000000000011',
	[InventarGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[Anzahl] int,	CONSTRAINT [PK_Held_Inventar] PRIMARY KEY ([HeldGUID], [TrageortGUID], [InventarGUID]), 
	CONSTRAINT fk_HeldInventar_Held FOREIGN KEY ([HeldGUID])
		REFERENCES Held ([HeldGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	CONSTRAINT fk_HeldInventar_Inventar FOREIGN KEY ([InventarGUID])
		REFERENCES Inventar ([InventarGUID])
		ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT fk_HeldInventar_TrageortGUID FOREIGN KEY ([TrageortGUID])
		REFERENCES Trageort ([TrageortGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO