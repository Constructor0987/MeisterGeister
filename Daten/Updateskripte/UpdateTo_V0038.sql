-- NscMerkmal Tabelle ist neu
CREATE TABLE [NscMerkmal] (
	[NscMerkmalId] bigint NOT NULL IDENTITY, 
	[Kategorie] nvarchar(100) NOT NULL DEFAULT 'Gesten', 
	[Merkmal] nvarchar(500),
	CONSTRAINT [PK_NscMerkmal] PRIMARY KEY ([NscMerkmalId])
)
GO
CREATE UNIQUE INDEX [NscMerkmal_Unique] ON [NscMerkmal] (
	[Kategorie], 
	[Merkmal]
)
GO