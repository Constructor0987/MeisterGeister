-- AT/PA Zuteilung
ALTER TABLE Held_Talent ADD ZuteilungAT int, ZuteilungPA int;

-- Untertalentgruppen
ALTER TABLE Talent ADD Untergruppe nvarchar(200);
UPDATE Talent SET Untergruppe = 'Bewaffneter Nahkampf' WHERE TalentgruppeID = 1;
UPDATE Talent SET Untergruppe = 'Fernkampf' WHERE Talentname = 'Armbrust' OR Talentname = 'Belagerungswaffen' OR Talentname = 'Blasrohr' OR Talentname = 'Bogen' OR Talentname = 'Diskus' OR Talentname = 'Schleuder' OR Talentname = 'Wurfbeile' OR Talentname = 'Wurfmesser' OR Talentname = 'Wurfspeere';
UPDATE Talent SET Untergruppe = 'Waffenloser Kampf' WHERE Talentname = 'Raufen' OR Talentname = 'Ringen';
UPDATE Talent SET Untergruppe = 'Bewaffnete AT-Technik' WHERE Talentname = 'Lanzenreiten' OR Talentname = 'Peitsche';