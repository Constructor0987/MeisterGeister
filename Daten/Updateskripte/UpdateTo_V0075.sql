--Unterthemes für Themes im Audioplayer
CREATE TABLE [Audio_Theme_Theme] (
  [Audio_ThemeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
, [Audio_UnterThemeGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
)
GO
ALTER TABLE [Audio_Theme_Playlist] ADD CONSTRAINT [PK_Audio_Theme_Theme] PRIMARY KEY ([Audio_ThemeGUID],[Audio_UnterThemeGUID])
GO
ALTER TABLE [Audio_Theme_Playlist] ADD CONSTRAINT [fk_Audio_Theme_Theme_Theme1] FOREIGN KEY ([Audio_ThemeGUID]) REFERENCES [Audio_Theme]([Audio_ThemeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Audio_Theme_Playlist] ADD CONSTRAINT [fk_Audio_Theme_Theme_Theme2] FOREIGN KEY ([Audio_UnterThemeGUID]) REFERENCES [Audio_Theme]([Audio_ThemeGUID]) ON DELETE CASCADE ON UPDATE CASCADE
GO

