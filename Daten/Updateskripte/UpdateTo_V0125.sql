--AudioTitel können in der Tonhöhe verändert werden
ALTER TABLE [Audio_Playlist_Titel] ADD [Pitch] float DEFAULT 0 NOT NULL
GO
--AudioTitel kann ein Echo hinzugefügt werden
ALTER TABLE [Audio_Playlist_Titel] Add Echo int NOT NULL default 0
GO

//Gegner können Zauber integriert werden
CREATE TABLE [GegnerBase_Zauber] (
	[GegnerBaseGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[ZauberGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000', 
	[ZfW] int DEFAULT 0, 
	[E1TW] int DEFAULT 10, 
	[E2TW] int DEFAULT 10, 
	[E3TW] int DEFAULT 10, 
	[Bemerkung] nvarchar(300),
	CONSTRAINT [PK_GegnerBase_Zauber] PRIMARY KEY ([GegnerBaseGUID], [ZauberGUID]), 
	FOREIGN KEY ([ZauberGUID])
		REFERENCES [Zauber] ([ZauberGUID])
		ON UPDATE CASCADE ON DELETE CASCADE, 
	FOREIGN KEY ([GegnerBaseGUID])
		REFERENCES [GegnerBase] ([GegnerBaseGUID])
		ON UPDATE CASCADE ON DELETE CASCADE
)
GO

--Spezielle Gewichtsangabe bei Rüstungsgegenständen (z.B. 3/4 bei Zwerge)
ALTER TABLE [Held_Ausrüstung] Add SpezGewicht float 
GO
