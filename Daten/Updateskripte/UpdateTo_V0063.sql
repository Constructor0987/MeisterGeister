/* Erweiterung der Setting-Tabelle */
ALTER TABLE Setting ADD Aktiv bit not null default 0;
UPDATE Setting SET Aktiv=1 WHERE SettingGUID='00000000-0000-0000-5e77-000000000001';
UPDATE Kultur set Setting='Aventurien' WHERE Setting is NULL;
UPDATE Kultur SET Name='Tulamidenlande',Variante='Tulamidenlande'  WHERE KulturGUID='00000000-0000-0000-0000-000000000280';