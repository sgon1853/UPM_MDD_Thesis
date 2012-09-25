CREATE INDEX Idx_PasajeroAer_41 ON PasajeroAeronave(fk_Aeronave_1) WITH PAD_INDEX;

CREATE INDEX Idx_PasajeroAer_42 ON PasajeroAeronave(fk_Pasajero_1) WITH PAD_INDEX;

CREATE INDEX Idx_RevisionPas_61 ON RevisionPasajero(fk_Revision_1) WITH PAD_INDEX;

CREATE INDEX Idx_RevisionPas_62 ON RevisionPasajero(fk_PasajeroAero_1) WITH PAD_INDEX;
