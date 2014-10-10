--Daten-Update: TODO - Gegner Uthuria und Untote

--Daten-Update: TODO - Rassen, Kulturen und Namen (vor allem für Riesland, Myranor und Uthuria)


-- Audio-Player TitelKategorie in die Theme-Liste integrieren
ALTER TABLE [Audio_Theme] ADD [Kategorie] nvarchar(500) NULL;

-- Audio Player Shuffle & Repeat Funktion speichern
ALTER TABLE [Audio_Playlist] ADD "Fading" bit NOT NULL DEFAULT 1;