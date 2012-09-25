CREATE TABLE NaveNodriza (id_NaveNodriza INT NOT NULL, estadoobj CHAR(15) NOT NULL, fum DATETIME NOT NULL, Nombre_NaveNodriza VARCHAR(100) NOT NULL);

CREATE TABLE Aeronave (id_Aeronave INT NOT NULL, estadoobj CHAR(15) NOT NULL, fum DATETIME NOT NULL, Nombre TEXT NOT NULL, MaximoPasajeros INT NOT NULL, Origen TEXT NOT NULL, Destino TEXT NOT NULL);

CREATE TABLE Pasajero (id_Pasajero INT NOT NULL, estadoobj CHAR(15) NOT NULL, fum DATETIME NOT NULL, Nombre TEXT NOT NULL);

CREATE TABLE PasajeroAeronave (id_PasajeroAeronave INT NOT NULL, fk_Aeronave_1 INT NULL, fk_Pasajero_1 INT NULL, estadoobj CHAR(15) NOT NULL, fum DATETIME NOT NULL, NombreAeronave VARCHAR(100) NOT NULL, NombrePasajero VARCHAR(100) NOT NULL);

CREATE TABLE Revision (id_RevisarAeronave INT NOT NULL, estadoobj CHAR(15) NOT NULL, fum DATETIME NOT NULL, NombreRevisor VARCHAR(100) NOT NULL, FechaRevision DATETIME NOT NULL, Id_Aeronave VARCHAR(100) NOT NULL);

CREATE TABLE RevisionPasajero (id_RevisionPasajero INT NOT NULL, fk_Revision_1 INT NOT NULL, fk_PasajeroAero_1 INT NOT NULL, estadoobj CHAR(15) NOT NULL, fum DATETIME NOT NULL);

CREATE TABLE Administrador (id_Administrador INT NOT NULL, PassWord VARCHAR(30) NOT NULL, estadoobj CHAR(15) NOT NULL, fum DATETIME NOT NULL);
