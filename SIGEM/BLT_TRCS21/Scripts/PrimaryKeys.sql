ALTER TABLE NaveNodriza ADD CONSTRAINT pk_NaveNodriza PRIMARY KEY (id_NaveNodriza);

ALTER TABLE Aeronave ADD CONSTRAINT pk_Aeronave PRIMARY KEY (id_Aeronave);

ALTER TABLE Pasajero ADD CONSTRAINT pk_Pasajero PRIMARY KEY (id_Pasajero);

ALTER TABLE PasajeroAeronave ADD CONSTRAINT pk_PasajeroAeronav PRIMARY KEY (id_PasajeroAeronave);

ALTER TABLE Revision ADD CONSTRAINT pk_Revision PRIMARY KEY (id_RevisarAeronave);

ALTER TABLE RevisionPasajero ADD CONSTRAINT pk_RevisionPasajer PRIMARY KEY (id_RevisionPasajero);

ALTER TABLE Administrador ADD CONSTRAINT pk_Administrador PRIMARY KEY (id_Administrador);
