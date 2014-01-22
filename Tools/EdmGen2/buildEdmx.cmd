@echo off
cd %~dp0
%~d0
echo Temporäre Datei löschen.
del DatabaseDSAModel.edmx
echo Edmx aus Datenbank generieren.
EdmGen2.exe /ModelGen "Data Source=..\..\Daten\DatabaseDSA.sdf;Password=m3ist3rg3ist3r" "System.Data.SqlServerCe.4.0" DatabaseDSA 3.0 includeFKs
move /Y DatabaseDSA.edmx MeisterGeisterModel.edmx
set dir=%~dp0
set dir=%dir:~0,-1%
echo Upper-Funktion eintragen.
fnr.exe --cl --dir "%dir%" --fileMask "MeisterGeisterModel.edmx" --find "</Schema>\n</edmx:StorageModels>" --replace "  <Function Name=\"upper\" Aggregate=\"false\" BuiltIn=\"true\" NiladicFunction=\"false\" IsComposable=\"true\" ParameterTypeSemantics=\"AllowImplicitConversion\" ReturnType=\"nvarchar\">\n    <Parameter Name=\"guid\" Type=\"uniqueidentifier\" Mode=\"In\" />\n  </Function>\n</Schema>\n</edmx:StorageModels>"
echo Ergebnis verschieben.
move /Y MeisterGeisterModel.edmx ..\..\Model\MeisterGeisterModel.edmx
pause