--Wesen <-> Playlist

CREATE TABLE [GegnerBase_Audio_Playlist] (
  [Audio_PlaylistGUID] uniqueidentifier DEFAULT '00000000-0000-0000-0000-000000000000' NOT NULL
, [GegnerBaseGUID] uniqueidentifier DEFAULT '00000000-0000-0000-0000-000000000000' NOT NULL
, [Kategorie] nvarchar(100) DEFAULT 'Tod' NOT NULL
);
ALTER TABLE [GegnerBase_Audio_Playlist] ADD CONSTRAINT [PK_GegnerBase_Audio_Playlist] PRIMARY KEY ([Audio_PlaylistGUID], [GegnerBaseGUID], [Kategorie]);
ALTER TABLE [GegnerBase_Audio_Playlist] ADD CONSTRAINT [FK_GegnerBase_Audio_Playlist_GegnerBase] FOREIGN KEY ([GegnerBaseGUID]) REFERENCES [GegnerBase]([GegnerBaseGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE [GegnerBase_Audio_Playlist] ADD CONSTRAINT [FK_GegnerBase_Audio_Playlist_Audio_Playlist] FOREIGN KEY ([Audio_PlaylistGUID]) REFERENCES [Audio_Playlist]([Audio_PlaylistGUID]) ON DELETE CASCADE ON UPDATE CASCADE;

CREATE TABLE [Held_Audio_Playlist] (
  [Audio_PlaylistGUID] uniqueidentifier DEFAULT '00000000-0000-0000-0000-000000000000' NOT NULL
, [HeldGUID] uniqueidentifier DEFAULT '00000000-0000-0000-0000-000000000000' NOT NULL
, [Kategorie] nvarchar(100) DEFAULT 'Tod' NOT NULL
);
ALTER TABLE [Held_Audio_Playlist] ADD CONSTRAINT [PK_Held_Audio_Playlist] PRIMARY KEY ([Audio_PlaylistGUID], [HeldGUID], [Kategorie]);
ALTER TABLE [Held_Audio_Playlist] ADD CONSTRAINT [FK_Held_Audio_Playlist_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE [Held_Audio_Playlist] ADD CONSTRAINT [FK_Held_Audio_Playlist_Audio_Playlist] FOREIGN KEY ([Audio_PlaylistGUID]) REFERENCES [Audio_Playlist]([Audio_PlaylistGUID]) ON DELETE CASCADE ON UPDATE CASCADE;