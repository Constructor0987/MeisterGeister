UPDATE Pflanze_Ernte SET Von = Von - 1;
UPDATE Pflanze_Ernte SET Bis = Bis - 1;
UPDATE Pflanze_Ernte SET Bis = Bis + 1 where FLOOR(Bis) = CEILING(Bis);
UPDATE Pflanze_Ernte SET Bis = 0 where Bis = 13;