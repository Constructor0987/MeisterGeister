# .NET-10-Portierung (in Arbeit)

Die Solution `MeisterGeister.sln` verwendet `MeisterGeister.Net10.csproj`, ein Windows-Projekt im SDK-Format für .NET 10. Das bisherige `MeisterGeister.csproj` bleibt als Framework-Projekt erhalten, wird aber nicht mehr von der Solution gebaut. Der .NET-10-Build ist noch kein vollständiger Ersatz für die bisherige Version.

## Bauen und prüfen

- .NET 10 SDK und Windows Desktop Runtime installieren.
- `dotnet build MeisterGeister.sln` baut die Debug-Version nach `bin/Net10/Debug/net10.0-windows/`; alternativ kann `MeisterGeister.Net10.csproj` direkt gebaut werden.
- `dotnet test Tests/WebServer/MeisterGeister.Web.Tests.csproj` prüft die HTTP-Endpunkte und den Serverlebenszyklus.
- `dotnet test Tests/Compatibility/MeisterGeister.Compatibility.Tests.csproj` prüft Serialisierung, dynamische Aufrufe und die vorhandenen Plug-ins. Der Ares-Test schlägt derzeit fehl.

## Daten

Der .NET-10-Build verwendet vorerst weiterhin SQL Server Compact und das vorhandene EF6-Modell. Diese Provider sind nicht für .NET 10 vorgesehen; ein erfolgreicher Start oder eine erfolgreiche Datenbankabfrage ist keine Garantie für alle Funktionen. Ein Ersatz der Datenbank und eine vollständige Datenmigration sind noch nicht implementiert.

Die Standarddatenbank wird beim ersten Start in das Datenverzeichnis des .NET-10-Builds kopiert. Die Anwendung aktualisiert ältere Datenbanken und fordert danach einen Neustart. Bestehende Benutzerdaten werden nicht automatisch aus einer anderen Installation übernommen. Vor Versuchen mit eigenen Daten die ursprüngliche Installation und insbesondere `Daten/DatabaseDSA.sdf` sichern; nur mit einer Kopie arbeiten und die alte Installation nicht überschreiben.

## Bekannte Einschränkungen

- Das Ares-Controller-Plug-in lässt sich nicht initialisieren: Seine Binärressourcen benötigen BinaryFormatter, der unter .NET 10 deaktiviert ist. Ein funktionsgleicher Ersatz fehlt. BinaryFormatter wird nicht wieder aktiviert.
- SQL Server Compact, dessen EF-Provider, WPFToolkit und Sparrow.Chart.Wpf stammen weiterhin aus .NET-Framework-Paketen; NuGet meldet dafür Kompatibilitätswarnungen.
- Der Artefaktgenerator ließ sich instanziieren, seine vollständige Funktion und die übrigen binären Module sind noch nicht umfassend geprüft.
- Der .NET-10-Webserver ersetzt Nancy/OWIN durch `HttpListener` und behält die vorhandenen Endpunkte bei. Eine URL-Reservierung kann wie bisher Administratorrechte erfordern.
