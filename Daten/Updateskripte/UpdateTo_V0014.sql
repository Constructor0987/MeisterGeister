﻿-- Standard-Standort korrigieren
UPDATE Einstellungen SET WertText = 'Gareth#29.79180235685203#3.735098459067687' WHERE Name = 'Standort' AND WertText LIKE 'Gareth#3.735098459067687#29.79180235685203';