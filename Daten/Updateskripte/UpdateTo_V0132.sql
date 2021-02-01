-- Held & Gegner Token ---
ALTER TABLE [Held] ADD [Token] nvarchar(500);
GO
ALTER TABLE [Held] ADD [TokenSizeX] int DEFAULT 1; 
GO
ALTER TABLE [Held] ADD [TokenSizeY] int DEFAULT 1;
GO
ALTER TABLE [Held] ADD [TokenOversize] float DEFAULT 1;
GO
ALTER TABLE [Gegner] ADD [Token] nvarchar(500);
GO
ALTER TABLE [Gegner] ADD [TokenSizeX] int DEFAULT 1; 
GO
ALTER TABLE [Gegner] ADD [TokenSizeY] int DEFAULT 1;
GO
ALTER TABLE [Gegner] ADD [TokenOversize] float DEFAULT 1;
