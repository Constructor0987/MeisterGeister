-- GegnerBase_Zauber Möglichkeit zur Farbmarkierung hinzugefügt
-- alter table GegnerBase_Zauber add Farbmarkierung nvarchar(50);
-- GO
--Gegner Eigenschaftswerte & SO hinzufügen (kannn NULL bleiben wenn keine verfügbar)
Alter Table GegnerBase Add "MU" int;
Alter Table GegnerBase Add "IN" int;
Alter Table GegnerBase Add "KL" int;
Alter Table GegnerBase Add "CH" int;
Alter Table GegnerBase Add "FF" int;
Alter Table GegnerBase Add "GE" int;
Alter Table GegnerBase Add "KK" int;
Alter Table GegnerBase Add "SO" int;
