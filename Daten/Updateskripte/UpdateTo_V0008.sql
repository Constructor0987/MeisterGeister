-- Sonderfertigkeiten bereinigen
UPDATE Held_Sonderfertigkeit SET SonderfertigkeitID = 380 WHERE SonderfertigkeitID = 388;
DELETE FROM Sonderfertigkeit WHERE SonderfertigkeitID = 388;

-- VorNachteil-Tabelle um Typ erweitern
ALTER TABLE VorNachteil ADD Typ nvarchar(50);
UPDATE VorNachteil SET Typ = 'Vorteile' WHERE Vorteil = 1;
UPDATE VorNachteil SET Typ = 'Nachteile' WHERE Nachteil = 1;
UPDATE VorNachteil SET Typ = 'Nachteile (Schlechte Eigenschaften)' WHERE VorNachteilID = 163 OR VorNachteilID = 337 OR VorNachteilID = 165 OR VorNachteilID = 167 OR VorNachteilID = 168 OR VorNachteilID = 171 OR VorNachteilID = 173 OR VorNachteilID = 175 OR VorNachteilID = 328 OR VorNachteilID = 176 OR VorNachteilID = 188 OR VorNachteilID = 196 OR VorNachteilID = 197 OR VorNachteilID = 201 OR VorNachteilID = 339 OR VorNachteilID = 202 OR VorNachteilID = 326 OR VorNachteilID = 205 OR VorNachteilID = 333 OR VorNachteilID = 207 OR VorNachteilID = 338 OR VorNachteilID = 213 OR VorNachteilID = 327 OR VorNachteilID = 223 OR VorNachteilID = 336 OR VorNachteilID = 334 OR VorNachteilID = 229 OR VorNachteilID = 331 OR VorNachteilID = 230 OR VorNachteilID = 235 OR VorNachteilID = 237 OR VorNachteilID = 240 OR VorNachteilID = 329 OR VorNachteilID = 332 OR VorNachteilID = 335 OR VorNachteilID = 254 OR VorNachteilID = 256 OR VorNachteilID = 258 OR VorNachteilID = 262 OR VorNachteilID = 267 OR VorNachteilID = 330 OR VorNachteilID = 309 OR VorNachteilID = 310 OR VorNachteilID = 311 OR VorNachteilID = 314;

-- Name-Tabelle
CREATE TABLE Name (NameID int IDENTITY (1, 1) NOT NULL PRIMARY KEY, Name nvarchar(100), Herkunft nvarchar(300), Art nvarchar(150), Bedeutung nvarchar(300), Geschlecht nvarchar(3), KeineVorsilbe bit DEFAULT 0, KeineNachsilbe bit DEFAULT 0);