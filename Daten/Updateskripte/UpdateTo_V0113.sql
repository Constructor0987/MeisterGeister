-- Held_Inventar mit ID versehen, damit der Trageort verändert werden kann.
ALTER TABLE Held_Inventar add Id bigint IDENTITY NOT NULL;
ALTER TABLE Held_Inventar drop constraint PK_Held_Inventar;
ALTER TABLE Held_Inventar add constraint PK_Held_Inventar PRIMARY KEY (Id);

-- TODO MT: Daten VorNachteile für DSA 4.1 überarbeiten (Kosten, etc.)

-- Held_VorNachteil Kosten eintragen, falls keine individuellen Kosten hinterlegt sind
--#FOREACH;
SELECT KostenGrund, KostenFaktor, VorNachteilGUID FROM VorNachteil;
--#DO;
UPDATE [Held_VorNachteil] SET KostenGrund={0}, KostenFaktor={1} WHERE VorNachteilGUID='{2}' AND KostenGrund = 0 AND KostenFaktor = 0;
--#END;


--TODO Waffenset
