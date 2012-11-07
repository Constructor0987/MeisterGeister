-- Zonenrüstung Helden
ALTER TABLE Held ADD RSKopf int DEFAULT 0, RSBrust int DEFAULT 0, RSRücken int DEFAULT 0, RSArmL int DEFAULT 0, RSArmR int DEFAULT 0, RSBauch int DEFAULT 0, RSBeinL int DEFAULT 0, RSBeinR int DEFAULT 0;
UPDATE Held SET RSKopf = RS, RSBrust = RS, RSRücken = RS, RSArmL = RS, RSArmR = RS, RSBauch = RS, RSBeinL = RS, RSBeinR = RS;