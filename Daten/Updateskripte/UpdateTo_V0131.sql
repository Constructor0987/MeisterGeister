--- Audio Theme HUE Beleuchtung Scene ID zuweisen ---

ALTER TABLE HUE_LampeColor DROP COLUMN Lampenname;
ALTER TABLE HUE_LampeColor DROP COLUMN Color;
ALTER TABLE Audio_Theme ADD HUE_Scene nvarchar(64);

