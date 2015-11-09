--Audio Wesen Playlist Icon - Pfade
CREATE TABLE [Audio_WesenIcon] (
	[Audio_WesenIconGUID] uniqueidentifier NOT NULL DEFAULT newid(), 
	[Pfad] nvarchar(254) NOT NULL
)
GO
ALTER TABLE [Audio_WesenIcon] ADD CONSTRAINT [PK_Audio_WesenIcon] PRIMARY KEY ([Audio_WesenIconGUID]);


--TODO Waffenset
