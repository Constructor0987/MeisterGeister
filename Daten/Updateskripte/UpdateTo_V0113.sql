-- TODO MT: Daten VorNachteile für DSA 4.1 überarbeiten (Kosten, etc.)

-- Held_VorNachteil Kosten eintragen, falls keine individuellen Kosten hinterlegt sind
--#FOREACH;
SELECT KostenGrund, KostenFaktor, VorNachteilGUID FROM VorNachteil;
--#DO;
UPDATE [Held_VorNachteil] SET KostenGrund={0}, KostenFaktor={1} WHERE VorNachteilGUID='{2}' AND KostenGrund = 0 AND KostenFaktor = 0;
--#END;


--TODO Waffenset
