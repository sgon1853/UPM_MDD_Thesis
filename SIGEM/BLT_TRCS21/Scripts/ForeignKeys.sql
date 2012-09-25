ALTER TABLE PasajeroAeronave ADD CONSTRAINT fkc_PasajeroAer_41 FOREIGN KEY (fk_Aeronave_1) REFERENCES Aeronave;

ALTER TABLE PasajeroAeronave ADD CONSTRAINT fkc_PasajeroAer_42 FOREIGN KEY (fk_Pasajero_1) REFERENCES Pasajero;

ALTER TABLE RevisionPasajero ADD CONSTRAINT fkc_RevisionPas_61 FOREIGN KEY (fk_Revision_1) REFERENCES Revision;

ALTER TABLE RevisionPasajero ADD CONSTRAINT fkc_RevisionPas_62 FOREIGN KEY (fk_PasajeroAero_1) REFERENCES PasajeroAeronave;
