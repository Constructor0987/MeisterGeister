-- Fremdschlüssel-Einschränkung für Held_Talent
ALTER TABLE Held_Talent DROP CONSTRAINT Talent_FK;
ALTER TABLE Held_Talent ADD CONSTRAINT Talent_FK FOREIGN KEY (Talentname) REFERENCES Talent (Talentname) ON DELETE cascade ON UPDATE cascade;