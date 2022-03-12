                README

******************************************
*                                        *
*        DSA MeisterGeister              *
*                                        *
*         Version 2.6.0.0                *
*                                        *
******************************************

Copyright © MeisterGeister-Team 2010-2021
http://www.meistergeister.org/
Kontakt: info@meistergeister.org


******************************************
Über das Tool
******************************************

DSA MeisterGeister ist eine Software zur Unterstützung des Meisters während der Spielsitzung. Es vereinigt verschiedene Tools, die dem Spielleiter beim Meistern helfen, sodass er sich über das "Handwerkszeug" weniger Gedanken machen muss.


******************************************
Partner und Unterstützer
******************************************

Silver Style Studios (Herokon-Online)
Die Silver Style Studios (http://www.silver-style-studios.com/), die Macher des Browser-MMORPG Herokon-Online (https://www.herokon-online.com/), haben die meisten der Icon-Grafiken und Bilder in MeisterGeister für uns erstellt. Für diese Kooperation bedanken wir uns recht herzlich.

DereGlobus-Projekt
DSA MeisterGeister steht in enger Kooperation mit dem DereGlobus-Projekts (http://www.dereglobus.org/), um die Vorteile beider Projekte miteinander zu verknüpfen.

Für die Implementierung des NSC-Tools wurden uns von Benjamin Ernst (aus seiner Spielhilfe 1001 NSC (http://www.wiki-aventurica.de/wiki/1001NPC)) und Peter Diefenbach (aus seinem DSATool (http://www.pdiefenbach.de/dsatool/)) freundlicherweise ihre umfangreichen Daten zur Verfügung gestellt. Wir bedanken uns sehr für diese Unterstützung!


******************************************
Lizenzhinweise
******************************************

MeisterGeister ist ein nicht-kommerzielles Fan-Projekt zum Pen&Paper Rollenspiel "Das Schwarze Auge". 
Seit August 2021 ist das Programm Open-Source. Das Programm darf somit frei kopiert, verändert und verteilt werden.
(Bitte hieru die 'LICENSE.md' beachten.)
Wir bitten alle Änderungen mit dem MeisterGeister-Team zu teilen, damit die gesamte Community von der Verbesserungen, Anpassungen und Neuerungen profitieren kann.

Das Urheberrecht der MeisterGeister-Software und des gesamten Quellcodes und allen darin enthaltenen Dateien liegt beim MeisterGeister-Team (falls nicht anders angegeben):

Projektgründer: Markus Traut / Projektleiter: Jürgen Bos / Entwickler: Jürgen Bos / Tester und Datenaufbereitung: Matthias Egenolf / inaktive Projektleiter: Jonas Tampier / inaktive Entwickler: Markus Traut, Marianus Ifland, Fabian Kretzschmar, Michael Prim, Manuel Poppe, Christopher Syben, Dominic Winterstein / Grafik: Joachim C. Fink / Jingle: Martin Zimny / Weitere Mitarbeiter: Florian Oldach, Marc Scharff, Bruno Stentzler, Andreas Widmann.

Einige Icon-Grafiken wurden uns freundlicherweise vom DereGlobus-Projekts zur Verfügung gestellt. Diese unterliegen der DereGlobus-Lizenz (http://www.dereglobus.org/lizenz).

Das Globus- und das Kalender-Tool bindet das 'DG-Suche PlugIn' des DereGlobus-Projekts ein. Es unterliegt der DereGlobus-Lizenz (http://www.dereglobus.org/lizenz).

Das Urheberrecht des 'ArtefaktGenerator' PlugIns liegt bei Mario Rauschenberg (http://www.dsa-hamburg.de/artefaktgen.html).

Das Urheberrecht des 'Ares-Controller' PlugIns liegt bei Jörg Rüdenauer (http://aresrpg.sourceforge.net/).


DAS SCHWARZE AUGE, AVENTURIEN, DERE, MYRANOR, THARUN, UTHURIA und RIESLAND sind eingetragene Marken der Significant Fantasy Medienrechte GbR. Ohne vorherige schriftliche Genehmigung der Ulisses Medien und Spiel Distribution GmbH ist eine Verwendung der genannten Markenzeichen nicht gestattet.

Die Datei 'ImpromptuInterface.dll' unterliegt der Apache License (siehe 'License\Apache License.txt').


******************************************
Voraussetzungen
******************************************

Microsoft .NET Framework 4.8
https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-web-installer

Microsoft SQL Server Compact 4.0 SP1
http://www.microsoft.com/de-de/download/details.aspx?id=30709

Windows Media Player ab Version 10 (für Audio-Funktionen).

Microsoft Access Database Engine 2010 (für Import von Heldenblatt.ch-Dateien)
http://www.microsoft.com/de-de/download/details.aspx?id=13255

Das .NET Framework ist eine Plattform für Microsoft Windows Betriebssysteme. 
Unter Linux funktioniert DSA MeisterGeister leider auf absehbare Zeit nicht, da die Linux-Portierung Mono einige zentrale Komponenten nicht unterstützt.


******************************************
Installation
******************************************

Zur Installation muss die ZIP-Datei lediglich nach dem Download entpackt werden. Das Programm kann danach direkt gestartet werden. Eine Installation ist nicht notwendig.

Es ist zu beachten, dass DSA MeisterGeister in einem Verzeichnis mit Schreib-Rechten liegen muss. Unter Windows Vista und Windows 7 oder höher führt dies ggf. dazu, dass DSA MeisterGeister nicht funktioniert, wenn es in den "Programme-Ordner" kopiert wird.

Bei einem Update darf die Datenbank-Datei "Daten\DatabaseDSA.sdf", in der z.B. die Helden gespeichert sind, nicht ausgetauscht werden, da sonst alle gespeicherten Daten verloren gehen. Die Datenbank wird durch die neue Programm-Version ggf. automatisch aktualisert.
