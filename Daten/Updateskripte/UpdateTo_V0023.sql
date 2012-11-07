-- Held_Waffe Tabelle
CREATE TABLE [Held_Waffe] (
  [ID] int NOT NULL  IDENTITY (1,1)
, [HeldID] int NOT NULL
, [WaffeId] int NOT NULL
, [Talentname] nvarchar(100) NULL
);
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [PK_Held_Waffe] PRIMARY KEY ([ID]);
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Held] FOREIGN KEY ([HeldID]) REFERENCES [Held]([HeldID]) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Talent] FOREIGN KEY ([Talentname]) REFERENCES [Talent]([Talentname]) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE [Held_Waffe] ADD CONSTRAINT [fk_HeldWaffe_Waffe] FOREIGN KEY ([WaffeId]) REFERENCES [Waffe]([WaffeID]) ON DELETE CASCADE ON UPDATE CASCADE;