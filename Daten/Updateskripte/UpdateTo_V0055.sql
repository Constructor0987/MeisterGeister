-- Zauber-Daten korrigiert
INSERT INTO [Zauber] ([Name],[Eigenschaft1],[Eigenschaft2],[Eigenschaft3],[Komplex],[Repräsentationen],[Merkmale]) VALUES ('Metamorpho Felsenform','KL','FF','KK','C','Elf 4, Mag 2','Einfluss')
GO
UPDATE [Zauber] SET [Name] = 'Belzhorashbann' WHERE [ZauberID] = 178
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Ach 6, Mag 5, Dru 2(Mag)' WHERE [ZauberID] = 3
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Mag 2, Geo 2, Dru 2(Mag)' WHERE [ZauberID] = 16
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Mag 2, Geo 2, Dru 2(Mag)' WHERE [ZauberID] = 21
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Ach 6, Mag 4, Dru 2(Mag)' WHERE [ZauberID] = 57
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Dru 3, Mag 2, Mag 2(Dru)' WHERE [ZauberID] = 75
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Mag 2, Geo 2, Dru 2(Mag)' WHERE [ZauberID] = 91
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Bor 2, Mag 2, Dru 2(Mag)' WHERE [ZauberID] = 100
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Alh 7' WHERE [ZauberID] = 316
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Mag 2, Geo 2, Dru 2(Mag)' WHERE [ZauberID] = 122
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Mag 3, Ach 2, Geo 2, Dru 2(Mag)' WHERE [ZauberID] = 124
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Hex 4, Elf 2, Dru 2(Hex), Mag 2' WHERE [ZauberID] = 158
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Sch 7, Geo 5, Dru (Geo)2' WHERE [ZauberID] = 172
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Mag 2, Geo 2, Dru 2(Mag)' WHERE [ZauberID] = 195
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Kop 2' WHERE [ZauberID] = 325
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Alh 3' WHERE [ZauberID] = 329
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Kop 2' WHERE [ZauberID] = 330
GO
UPDATE [Zauber] SET [Repräsentationen] = 'Elf 6, Mag 2, Dru 2(Mag)' WHERE [ZauberID] = 294
GO

-- Zauber: Literatur und Setting Daten eingefügt
UPDATE [Zauber] SET [Literatur]='LCD 11',[Setting]='Avenurien' WHERE [ZauberID]=1;
UPDATE [Zauber] SET [Literatur]='LCD 12',[Setting]='Avenurien' WHERE [ZauberID]=2;
UPDATE [Zauber] SET [Literatur]='LCD 13',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=3;
UPDATE [Zauber] SET [Literatur]='LCD 15',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=4;
UPDATE [Zauber] SET [Literatur]='LCD 16',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=5;
UPDATE [Zauber] SET [Literatur]='LCD 18',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=6;
UPDATE [Zauber] SET [Literatur]='LCD 19',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=7;
UPDATE [Zauber] SET [Literatur]='LCD 20',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=8;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=9;
UPDATE [Zauber] SET [Literatur]='LCD 21',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=10;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=11;
UPDATE [Zauber] SET [Literatur]='LCD 22',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=12;
UPDATE [Zauber] SET [Literatur]='LCD 23',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=13;
UPDATE [Zauber] SET [Literatur]='LCD 24',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=14;
UPDATE [Zauber] SET [Literatur]='LCD 25',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=15;
UPDATE [Zauber] SET [Literatur]='LCD 122',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=16;
UPDATE [Zauber] SET [Literatur]='LCD 124',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=306;
UPDATE [Zauber] SET [Literatur]='LCD 26',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=17;
UPDATE [Zauber] SET [Literatur]='LCD 27',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=20;
UPDATE [Zauber] SET [Literatur]='LCD 27',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=19;
UPDATE [Zauber] SET [Literatur]='LCD 27',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=18;
UPDATE [Zauber] SET [Literatur]='LCD 122, 123 ',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=21;
UPDATE [Zauber] SET [Literatur]='LCD 124',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=307;
UPDATE [Zauber] SET [Literatur]='LCD 28',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=22;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=23;
UPDATE [Zauber] SET [Literatur]='LCD 29',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=24;
UPDATE [Zauber] SET [Literatur]='LCD 30',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=25;
UPDATE [Zauber] SET [Literatur]='LCD 31',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=26;
UPDATE [Zauber] SET [Literatur]='LCD 32',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=27;
UPDATE [Zauber] SET [Literatur]='LCD 33',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=28;
UPDATE [Zauber] SET [Literatur]='LCD 35',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=29;
UPDATE [Zauber] SET [Literatur]='LCD 36',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=30;
UPDATE [Zauber] SET [Literatur]='LCD 37',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=31;
UPDATE [Zauber] SET [Literatur]='LCD 38',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=32;
UPDATE [Zauber] SET [Literatur]='LCD 39',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=33;
UPDATE [Zauber] SET [Literatur]='LCD 40',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=34;
UPDATE [Zauber] SET [Literatur]='LCD 41',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=35;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=36;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=37;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=178;
UPDATE [Zauber] SET [Literatur]='LCD 42',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=38;
UPDATE [Zauber] SET [Literatur]='LCD 43',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=39;
UPDATE [Zauber] SET [Literatur]='OiC 67',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=309;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=40;
UPDATE [Zauber] SET [Literatur]='LCD 44',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=41;
UPDATE [Zauber] SET [Literatur]='LCD 45',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=42;
UPDATE [Zauber] SET [Literatur]='LCD 46',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=43;
UPDATE [Zauber] SET [Literatur]='LCD 47',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=44;
UPDATE [Zauber] SET [Literatur]='LCD 48',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=45;
UPDATE [Zauber] SET [Literatur]='LCD 50',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=46;
UPDATE [Zauber] SET [Literatur]='LCD 50',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=47;
UPDATE [Zauber] SET [Literatur]='LCD 51',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=48;
UPDATE [Zauber] SET [Literatur]='LCD 52',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=49;
UPDATE [Zauber] SET [Literatur]='LCD 54',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=50;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=51;
UPDATE [Zauber] SET [Literatur]='LCD 55',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=52;
UPDATE [Zauber] SET [Literatur]='LCD 56',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=53;
UPDATE [Zauber] SET [Literatur]='LCD 57',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=54;
UPDATE [Zauber] SET [Literatur]='LCD 58',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=55;
UPDATE [Zauber] SET [Literatur]='LCD 59',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=56;
UPDATE [Zauber] SET [Literatur]='LCD 60',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=57;
UPDATE [Zauber] SET [Literatur]='LCD 61',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=58;
UPDATE [Zauber] SET [Literatur]='LCD 62',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=59;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=60;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=332;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=333;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=334;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=335;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=336;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=337;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=338;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=339;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=340;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=341;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=342;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=343;
UPDATE [Zauber] SET [Literatur]='LCD 64',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=61;
UPDATE [Zauber] SET [Literatur]='LCD 65',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=62;
UPDATE [Zauber] SET [Literatur]='LCD 66',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=63;
UPDATE [Zauber] SET [Literatur]='LCD 67',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=64;
UPDATE [Zauber] SET [Literatur]='LCD 68',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=65;
UPDATE [Zauber] SET [Literatur]='LCD 69',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=66;
UPDATE [Zauber] SET [Literatur]='LCD 70',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=67;
UPDATE [Zauber] SET [Literatur]='LCD 71, 72',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=68;
UPDATE [Zauber] SET [Literatur]='LCD 73',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=69;
UPDATE [Zauber] SET [Literatur]='LCD 74',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=70;
UPDATE [Zauber] SET [Literatur]='LCD 75',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=71;
UPDATE [Zauber] SET [Literatur]='LCD 76',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=72;
UPDATE [Zauber] SET [Literatur]='LCD 77',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=73;
UPDATE [Zauber] SET [Literatur]='LCD 78',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=74;
UPDATE [Zauber] SET [Literatur]='LCD 172',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=75;
UPDATE [Zauber] SET [Literatur]='LCD 79',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=76;
UPDATE [Zauber] SET [Literatur]='LCD 80',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=77;
UPDATE [Zauber] SET [Literatur]='CS 67',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=78;
UPDATE [Zauber] SET [Literatur]='LCD 81',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=79;
UPDATE [Zauber] SET [Literatur]='OiC 67',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=310;
UPDATE [Zauber] SET [Literatur]='LCD 82',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=80;
UPDATE [Zauber] SET [Literatur]='LCD 83',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=81;
UPDATE [Zauber] SET [Literatur]='LCD 84',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=82;
UPDATE [Zauber] SET [Literatur]='LCD 85',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=83;
UPDATE [Zauber] SET [Literatur]='LCD 162, 163 ',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=84;
UPDATE [Zauber] SET [Literatur]='LCD 172',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=85;
UPDATE [Zauber] SET [Literatur]='LCD 86',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=86;
UPDATE [Zauber] SET [Literatur]='LCD 87',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=87;
UPDATE [Zauber] SET [Literatur]='LCD 88',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=88;
UPDATE [Zauber] SET [Literatur]='LCD 89',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=89;
UPDATE [Zauber] SET [Literatur]='LCD 90',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=90;
UPDATE [Zauber] SET [Literatur]='LCD 122, 123',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=91;
UPDATE [Zauber] SET [Literatur]='LCD 124',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=92;
UPDATE [Zauber] SET [Literatur]='LCD 91',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=93;
UPDATE [Zauber] SET [Literatur]='LCD 92, 93',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=94;
UPDATE [Zauber] SET [Literatur]='OiC 68',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=311;
UPDATE [Zauber] SET [Literatur]='LCD 94',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=95;
UPDATE [Zauber] SET [Literatur]='LCD 95',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=96;
UPDATE [Zauber] SET [Literatur]='LCD 96',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=97;
UPDATE [Zauber] SET [Literatur]='LCD 97',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=98;
UPDATE [Zauber] SET [Literatur]='LCD 98',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=99;
UPDATE [Zauber] SET [Literatur]='OiC 68',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=312;
UPDATE [Zauber] SET [Literatur]='LCD 99',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=100;
UPDATE [Zauber] SET [Literatur]='OiC 69',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=313;
UPDATE [Zauber] SET [Literatur]='LCD 100',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=101;
UPDATE [Zauber] SET [Literatur]='LCD 101',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=102;
UPDATE [Zauber] SET [Literatur]='LCD 102',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=103;
UPDATE [Zauber] SET [Literatur]='LCD 103',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=104;
UPDATE [Zauber] SET [Literatur]='LCD 104',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=105;
UPDATE [Zauber] SET [Literatur]='LCD 105, 106',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=106;
UPDATE [Zauber] SET [Literatur]='OiC 69',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=314;
UPDATE [Zauber] SET [Literatur]='LCD 107',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=107;
UPDATE [Zauber] SET [Literatur]='LCD 108',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=108;
UPDATE [Zauber] SET [Literatur]='LCD 109',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=109;
UPDATE [Zauber] SET [Literatur]='LCD 110',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=110;
UPDATE [Zauber] SET [Literatur]='LCD 111',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=111;
UPDATE [Zauber] SET [Literatur]='OiC 69',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=315;
UPDATE [Zauber] SET [Literatur]='LCD 112',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=112;
UPDATE [Zauber] SET [Literatur]='LCD 113',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=113;
UPDATE [Zauber] SET [Literatur]='LCD 114',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=114;
UPDATE [Zauber] SET [Literatur]='LCD 115',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=115;
UPDATE [Zauber] SET [Literatur]='LCD 116',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=116;
UPDATE [Zauber] SET [Literatur]='LCD 117',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=117;
UPDATE [Zauber] SET [Literatur]='LCD 118',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=118;
UPDATE [Zauber] SET [Literatur]='LCD 119',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=119;
UPDATE [Zauber] SET [Literatur]='LCD 120',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=120;
UPDATE [Zauber] SET [Literatur]='OiC 69',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=316;
UPDATE [Zauber] SET [Literatur]='LCD 121',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=121;
UPDATE [Zauber] SET [Literatur]='LCD 122, 123',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=122;
UPDATE [Zauber] SET [Literatur]='LCD 124',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=308;
UPDATE [Zauber] SET [Literatur]='LCD 122, 123',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=123;
UPDATE [Zauber] SET [Literatur]='OiC 69',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=317;
UPDATE [Zauber] SET [Literatur]='OiC 70',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=318;
UPDATE [Zauber] SET [Literatur]='OiC 70',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=319;
UPDATE [Zauber] SET [Literatur]='LCD 124',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=124;
UPDATE [Zauber] SET [Literatur]='LCD 125',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=125;
UPDATE [Zauber] SET [Literatur]='LCD 126',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=126;
UPDATE [Zauber] SET [Literatur]='LCD 127',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=127;
UPDATE [Zauber] SET [Literatur]='LCD 128',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=128;
UPDATE [Zauber] SET [Literatur]='LCD 129',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=129;
UPDATE [Zauber] SET [Literatur]='LCD 130',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=130;
UPDATE [Zauber] SET [Literatur]='LCD 131',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=131;
UPDATE [Zauber] SET [Literatur]='LCD 132',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=132;
UPDATE [Zauber] SET [Literatur]='LCD 133',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=133;
UPDATE [Zauber] SET [Literatur]='LCD 134',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=134;
UPDATE [Zauber] SET [Literatur]='LCD 135',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=135;
UPDATE [Zauber] SET [Literatur]='LCD 136',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=136;
UPDATE [Zauber] SET [Literatur]='LCD 137',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=137;
UPDATE [Zauber] SET [Literatur]='LCD 138',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=138;
UPDATE [Zauber] SET [Literatur]='LCD 139',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=139;
UPDATE [Zauber] SET [Literatur]='LCD 140',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=140;
UPDATE [Zauber] SET [Literatur]='LCD 141',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=141;
UPDATE [Zauber] SET [Literatur]='LCD 142',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=142;
UPDATE [Zauber] SET [Literatur]='LCD 143',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=143;
UPDATE [Zauber] SET [Literatur]='LCD 144',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=144;
UPDATE [Zauber] SET [Literatur]='LCD 145',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=145;
UPDATE [Zauber] SET [Literatur]='LCD 146, 147',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=146;
UPDATE [Zauber] SET [Literatur]='LCD 148',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=147;
UPDATE [Zauber] SET [Literatur]='LCD 149',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=148;
UPDATE [Zauber] SET [Literatur]='LCD 150',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=149;
UPDATE [Zauber] SET [Literatur]='LCD 151',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=150;
UPDATE [Zauber] SET [Literatur]='LCD 152',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=151;
UPDATE [Zauber] SET [Literatur]='LCD 153',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=152;
UPDATE [Zauber] SET [Literatur]='LCD 154',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=153;
UPDATE [Zauber] SET [Literatur]='OiC 70',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=320;
UPDATE [Zauber] SET [Literatur]='LCD 155, 156',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=154;
UPDATE [Zauber] SET [Literatur]='LCD 157, 158',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=155;
UPDATE [Zauber] SET [Literatur]='LCD 160, 161',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=156;
UPDATE [Zauber] SET [Literatur]='LCD 160, 161 ',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=157;
UPDATE [Zauber] SET [Literatur]='LCD 162, 163',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=158;
UPDATE [Zauber] SET [Literatur]='LCD 164',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=159;
UPDATE [Zauber] SET [Literatur]='LCD 165',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=160;
UPDATE [Zauber] SET [Literatur]='LCD 166',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=161;
UPDATE [Zauber] SET [Literatur]='LCD 167',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=162;
UPDATE [Zauber] SET [Literatur]='LCD 168',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=163;
UPDATE [Zauber] SET [Literatur]='LCD 63',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=164;
UPDATE [Zauber] SET [Literatur]='LCD 169',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=165;
UPDATE [Zauber] SET [Literatur]='LCD 170',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=166;
UPDATE [Zauber] SET [Literatur]='LCD 171',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=167;
UPDATE [Zauber] SET [Literatur]='LCD 172',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=168;
UPDATE [Zauber] SET [Literatur]='LCD 172',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=169;
UPDATE [Zauber] SET [Literatur]='LCD 173',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=170;
UPDATE [Zauber] SET [Literatur]='LCD 174',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=171;
UPDATE [Zauber] SET [Literatur]='LCD 175',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=172;
UPDATE [Zauber] SET [Literatur]='LCD 176',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=173;
UPDATE [Zauber] SET [Literatur]='LCD 177',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=174;
UPDATE [Zauber] SET [Literatur]='LCD 178',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=175;
UPDATE [Zauber] SET [Literatur]='LCD 179',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=176;
UPDATE [Zauber] SET [Literatur]='LCD 180',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=346;
UPDATE [Zauber] SET [Literatur]='LCD 180',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=177;
UPDATE [Zauber] SET [Literatur]='LCD 181, 182',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=179;
UPDATE [Zauber] SET [Literatur]='LCD 183',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=180;
UPDATE [Zauber] SET [Literatur]='LCD 184',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=181;
UPDATE [Zauber] SET [Literatur]='LCD 185',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=182;
UPDATE [Zauber] SET [Literatur]='LCD  63 ',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=183;
UPDATE [Zauber] SET [Literatur]='LCD 186',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=184;
UPDATE [Zauber] SET [Literatur]='LCD 187',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=185;
UPDATE [Zauber] SET [Literatur]='LCD 188',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=186;
UPDATE [Zauber] SET [Literatur]='LCD 189',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=187;
UPDATE [Zauber] SET [Literatur]='LCD 190',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=188;
UPDATE [Zauber] SET [Literatur]='LCD 191',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=189;
UPDATE [Zauber] SET [Literatur]='LCD 192',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=190;
UPDATE [Zauber] SET [Literatur]='LCD 193',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=191;
UPDATE [Zauber] SET [Literatur]='LCD 194, 195',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=192;
UPDATE [Zauber] SET [Literatur]='LCD 196',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=193;
UPDATE [Zauber] SET [Literatur]='LCD 197',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=194;
UPDATE [Zauber] SET [Literatur]='LCD 122, 123 ',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=195;
UPDATE [Zauber] SET [Literatur]='LCD 124',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=196;
UPDATE [Zauber] SET [Literatur]='LCD 199',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=197;
UPDATE [Zauber] SET [Literatur]='LCD 200',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=198;
UPDATE [Zauber] SET [Literatur]='LCD 201',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=199;
UPDATE [Zauber] SET [Literatur]='LCD 202',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=200;
UPDATE [Zauber] SET [Literatur]='LCD 203',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=201;
UPDATE [Zauber] SET [Literatur]='LCD 204',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=202;
UPDATE [Zauber] SET [Literatur]='LCD 205, 206',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=203;
UPDATE [Zauber] SET [Literatur]='LCD 207',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=204;
UPDATE [Zauber] SET [Literatur]='LCD 208, 209',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=208;
UPDATE [Zauber] SET [Literatur]='LCD 208, 209',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=210;
UPDATE [Zauber] SET [Literatur]='LCD 208, 209',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=207;
UPDATE [Zauber] SET [Literatur]='LCD 208, 209',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=205;
UPDATE [Zauber] SET [Literatur]='LCD 208, 209',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=209;
UPDATE [Zauber] SET [Literatur]='LCD 208, 209',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=206;
UPDATE [Zauber] SET [Literatur]='LCD 210',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=211;
UPDATE [Zauber] SET [Literatur]='LCD 211',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=212;
UPDATE [Zauber] SET [Literatur]='LCD 212',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=213;
UPDATE [Zauber] SET [Literatur]='LCD 213',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=214;
UPDATE [Zauber] SET [Literatur]='LCD 214',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=215;
UPDATE [Zauber] SET [Literatur]='LCD 215',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=216;
UPDATE [Zauber] SET [Literatur]='LCD 216',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=217;
UPDATE [Zauber] SET [Literatur]='LCD 217',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=218;
UPDATE [Zauber] SET [Literatur]='LCD 218',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=219;
UPDATE [Zauber] SET [Literatur]='LCD 219',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=220;
UPDATE [Zauber] SET [Literatur]='LCD 220',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=221;
UPDATE [Zauber] SET [Literatur]='LCD 221',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=222;
UPDATE [Zauber] SET [Literatur]='LCD 222',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=223;
UPDATE [Zauber] SET [Literatur]='LCD 223',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=224;
UPDATE [Zauber] SET [Literatur]='LCD 224',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=225;
UPDATE [Zauber] SET [Literatur]='LCD 225',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=226;
UPDATE [Zauber] SET [Literatur]='LCD 226',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=227;
UPDATE [Zauber] SET [Literatur]='LCD 227',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=228;
UPDATE [Zauber] SET [Literatur]='LCD 228',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=229;
UPDATE [Zauber] SET [Literatur]='LCD 229',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=230;
UPDATE [Zauber] SET [Literatur]='LCD 230',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=231;
UPDATE [Zauber] SET [Literatur]='OiC 71',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=321;
UPDATE [Zauber] SET [Literatur]='LCD 231',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=232;
UPDATE [Zauber] SET [Literatur]='LCD 232',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=233;
UPDATE [Zauber] SET [Literatur]='LCD 233',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=234;
UPDATE [Zauber] SET [Literatur]='OiC 71',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=322;
UPDATE [Zauber] SET [Literatur]='LCD 234',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=235;
UPDATE [Zauber] SET [Literatur]='LCD 235',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=236;
UPDATE [Zauber] SET [Literatur]='LCD 236',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=237;
UPDATE [Zauber] SET [Literatur]='LCD 237',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=238;
UPDATE [Zauber] SET [Literatur]='LCD 238',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=239;
UPDATE [Zauber] SET [Literatur]='LCD 239',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=240;
UPDATE [Zauber] SET [Literatur]='LCD 240',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=241;
UPDATE [Zauber] SET [Literatur]='LCD 241',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=242;
UPDATE [Zauber] SET [Literatur]='LCD 242',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=243;
UPDATE [Zauber] SET [Literatur]='LCD 243, 244',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=244;
UPDATE [Zauber] SET [Literatur]='LCD 245',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=245;
UPDATE [Zauber] SET [Literatur]='LCD 246',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=246;
UPDATE [Zauber] SET [Literatur]='OiC 71',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=323;
UPDATE [Zauber] SET [Literatur]='LCD 247',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=247;
UPDATE [Zauber] SET [Literatur]='OiC 72',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=324;
UPDATE [Zauber] SET [Literatur]='OiC 72',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=325;
UPDATE [Zauber] SET [Literatur]='LCD 248',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=248;
UPDATE [Zauber] SET [Literatur]='LCD 240',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=249;
UPDATE [Zauber] SET [Literatur]='LCD 250',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=250;
UPDATE [Zauber] SET [Literatur]='LCD 251',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=251;
UPDATE [Zauber] SET [Literatur]='LCD 252',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=252;
UPDATE [Zauber] SET [Literatur]='LCD 172',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=253;
UPDATE [Zauber] SET [Literatur]='LCD 253',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=254;
UPDATE [Zauber] SET [Literatur]='OiC 73',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=326;
UPDATE [Zauber] SET [Literatur]='LCD  63 ',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=255;
UPDATE [Zauber] SET [Literatur]='LCD 254',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=256;
UPDATE [Zauber] SET [Literatur]='LCD 255',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=257;
UPDATE [Zauber] SET [Literatur]='LCD  63 ',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=258;
UPDATE [Zauber] SET [Literatur]='LCD 256',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=259;
UPDATE [Zauber] SET [Literatur]='LCD 257',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=260;
UPDATE [Zauber] SET [Literatur]='OiC 73',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=327;
UPDATE [Zauber] SET [Literatur]='LCD 258',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=261;
UPDATE [Zauber] SET [Literatur]='LCD 259',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=262;
UPDATE [Zauber] SET [Literatur]='LCD 260',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=263;
UPDATE [Zauber] SET [Literatur]='LCD 261',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=264;
UPDATE [Zauber] SET [Literatur]='LCD 262',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=265;
UPDATE [Zauber] SET [Literatur]='LCD 263',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=266;
UPDATE [Zauber] SET [Literatur]='LCD 264',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=267;
UPDATE [Zauber] SET [Literatur]='LCD 265',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=268;
UPDATE [Zauber] SET [Literatur]='OiC 73',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=328;
UPDATE [Zauber] SET [Literatur]='LCD 266',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=269;
UPDATE [Zauber] SET [Literatur]='OiC 73',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=329;
UPDATE [Zauber] SET [Literatur]='LCD 267',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=270;
UPDATE [Zauber] SET [Literatur]='LCD 268',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=271;
UPDATE [Zauber] SET [Literatur]='LCD 269',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=272;
UPDATE [Zauber] SET [Literatur]='LCD 270',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=273;
UPDATE [Zauber] SET [Literatur]='LCD 271',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=274;
UPDATE [Zauber] SET [Literatur]='LCD 272',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=275;
UPDATE [Zauber] SET [Literatur]='LCD 273',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=276;
UPDATE [Zauber] SET [Literatur]='LCD 274',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=277;
UPDATE [Zauber] SET [Literatur]='LCD 275',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=282;
UPDATE [Zauber] SET [Literatur]='LCD 275',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=283;
UPDATE [Zauber] SET [Literatur]='LCD 275',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=280;
UPDATE [Zauber] SET [Literatur]='LCD 275',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=278;
UPDATE [Zauber] SET [Literatur]='LCD 275',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=281;
UPDATE [Zauber] SET [Literatur]='LCD 275',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=279;
UPDATE [Zauber] SET [Literatur]='LCD 276',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=284;
UPDATE [Zauber] SET [Literatur]='LCD 277',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=285;
UPDATE [Zauber] SET [Literatur]='LCD 278',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=286;
UPDATE [Zauber] SET [Literatur]='LCD 279',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=287;
UPDATE [Zauber] SET [Literatur]='LCD 280',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=288;
UPDATE [Zauber] SET [Literatur]='OiC 73',[Setting]='Dunkle Zeiten' WHERE [ZauberID]=330;
UPDATE [Zauber] SET [Literatur]='LCD 281',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=289;
UPDATE [Zauber] SET [Literatur]='LCD 282',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=290;
UPDATE [Zauber] SET [Literatur]='LCD 283, 284',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=291;
UPDATE [Zauber] SET [Literatur]='LCD 285',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=292;
UPDATE [Zauber] SET [Literatur]='LCD 286',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=293;
UPDATE [Zauber] SET [Literatur]='LCD 287',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=294;
UPDATE [Zauber] SET [Literatur]='LCD 288',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=295;
UPDATE [Zauber] SET [Literatur]='LCD 289',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=296;
UPDATE [Zauber] SET [Literatur]='LCD 290',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=297;
UPDATE [Zauber] SET [Literatur]='LCD 291',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=298;
UPDATE [Zauber] SET [Literatur]='LCD 292',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=299;
UPDATE [Zauber] SET [Literatur]='LCD 293',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=300;
UPDATE [Zauber] SET [Literatur]='LCD 294',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=301;
UPDATE [Zauber] SET [Literatur]='LCD 295',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=302;
UPDATE [Zauber] SET [Literatur]='LCD 296',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=303;
UPDATE [Zauber] SET [Literatur]='LCD 297',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=304;
UPDATE [Zauber] SET [Literatur]='LCD 298',[Setting]='Avenurien, Dunkle Zeiten' WHERE [ZauberID]=305;