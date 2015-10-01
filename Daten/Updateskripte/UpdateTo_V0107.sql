--Audio Hotkey Wesen

CREATE TABLE [Audio_HotkeyWesen] (
  [Audio_HotkeyWesenGUID] uniqueidentifier DEFAULT newid() NOT NULL
, [Audio_PListGUID] uniqueidentifier DEFAULT '00000000-0000-0000-0000-000000000000' NOT NULL
, [HeldGUID] uniqueidentifier NULL
, [GegnerBaseGUID] uniqueidentifier NULL
, [IstHeld] bit NOT NULL
, [IstGegner] bit NOT NULL
);
ALTER TABLE [Audio_HotkeyWesen] ADD CONSTRAINT [PK_Audio_HotkeyWesen] PRIMARY KEY ([Audio_HotkeyWesenGUID]);
ALTER TABLE [Audio_HotkeyWesen] ADD CONSTRAINT [FK_Audio_HotkeyWesen_GegnerBase] FOREIGN KEY ([GegnerBaseGUID]) REFERENCES [GegnerBase]([GegnerBaseGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE [Audio_HotkeyWesen] ADD CONSTRAINT [FK_Audio_HotkeyWesen_Held] FOREIGN KEY ([HeldGUID]) REFERENCES [Held]([HeldGUID]) ON DELETE CASCADE ON UPDATE CASCADE;
