@echo off
cd %~dp0
%~d0
echo Temporaere Datei loeschen.
IF EXIST DatabaseDSAModel.edmx del DatabaseDSAModel.edmx
echo Edmx aus Datenbank generieren.
EdmGen2.exe /ModelGen "Data Source=..\..\Daten\DatabaseDSA.sdf;Password=m3ist3rg3ist3r" "System.Data.SqlServerCe.4.0" DatabaseDSA 3.0 includeFKs
move /Y DatabaseDSA.edmx MeisterGeisterModel.edmx
set dir=%~dp0
set dir=%dir:~0,-1%
echo Upper-Funktion eintragen (1).
fnr.exe --cl --dir "%dir%" --fileMask "MeisterGeisterModel.edmx" --find "</Schema>\n</edmx:StorageModels>" --replace "  <Function Name=\"upper\" Aggregate=\"false\" BuiltIn=\"true\" NiladicFunction=\"false\" IsComposable=\"true\" ParameterTypeSemantics=\"AllowImplicitConversion\" ReturnType=\"nvarchar\">\n    <Parameter Name=\"guid\" Type=\"uniqueidentifier\" Mode=\"In\" />\n  </Function>\n</Schema>\n</edmx:StorageModels>"
echo Navigation von Ort auf Strecke anpassen (4).
fnr.exe --cl --dir "%dir%" --fileMask "MeisterGeisterModel.edmx" --find "<NavigationProperty Name=\"Strecke\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Start\" FromRole=\"Ort\" ToRole=\"Strecke\" />" --replace "<NavigationProperty Name=\"StartStrecke\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Start\" FromRole=\"Ort\" ToRole=\"Strecke\" />"
fnr.exe --cl --dir "%dir%" --fileMask "MeisterGeisterModel.edmx" --find "<NavigationProperty Name=\"Strecke1\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Ziel\" FromRole=\"Ort\" ToRole=\"Strecke\" />" --replace "<NavigationProperty Name=\"ZielStrecke\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Ziel\" FromRole=\"Ort\" ToRole=\"Strecke\" />"
fnr.exe --cl --dir "%dir%" --fileMask "MeisterGeisterModel.edmx" --find "<NavigationProperty Name=\"Ort\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Start\" FromRole=\"Strecke\" ToRole=\"Ort\" />" --replace "<NavigationProperty Name=\"StartOrt\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Start\" FromRole=\"Strecke\" ToRole=\"Ort\" />"
fnr.exe --cl --dir "%dir%" --fileMask "MeisterGeisterModel.edmx" --find "<NavigationProperty Name=\"Ort1\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Ziel\" FromRole=\"Strecke\" ToRole=\"Ort\" />" --replace "<NavigationProperty Name=\"ZielOrt\" Relationship=\"DatabaseDSAModel.fk_Strecke_Ort_Ziel\" FromRole=\"Strecke\" ToRole=\"Ort\" />"
echo Ergebnis verschieben.
move /Y MeisterGeisterModel.edmx ..\..\Model\MeisterGeisterModel.edmx
pause