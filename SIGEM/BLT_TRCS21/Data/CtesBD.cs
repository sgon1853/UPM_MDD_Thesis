// 3.4.4.5
using System;

namespace SIGEM.Business
{
	///<summary>
	///Keeps the constant names for the database such as tables and fields
	///</summary>
	internal abstract class CtesBD
	{
		#region Connection String
		public const string ConnectionString = "SIGEMDB";
		#endregion Connection String
		
		#region Tables
		///<summary>
		///Table name for class 'NaveNodriza'
		///</summary>
		public const string TBL_NAVENODRIZA = "NaveNodriza"; 
		///<summary>
		///Table name for class 'Aeronave'
		///</summary>
		public const string TBL_AERONAVE = "Aeronave"; 
		///<summary>
		///Table name for class 'Pasajero'
		///</summary>
		public const string TBL_PASAJERO = "Pasajero"; 
		///<summary>
		///Table name for class 'PasajeroAeronave'
		///</summary>
		public const string TBL_PASAJEROAERONAVE = "PasajeroAeronave"; 
		///<summary>
		///Table name for class 'Revision'
		///</summary>
		public const string TBL_REVISION = "Revision"; 
		///<summary>
		///Table name for class 'RevisionPasajero'
		///</summary>
		public const string TBL_REVISIONPASAJERO = "RevisionPasajero"; 
		///<summary>
		///Table name for class 'Administrador'
		///</summary>
		public const string TBL_ADMINISTRADOR = "Administrador"; 
		#endregion Tables
		
		#region Fields
		///<summary>
		///Field name for identification attribute 'id_NaveNodriza'
		///</summary>	
		public const string FLD_NAVENODRIZA_ID_NAVENODRIZA = "id_NaveNodriza";
		///<summary>
		///DTEControl Field name
		///</summary>	
		public const string FLD_NAVENODRIZA_ESTADOOBJ = "estadoobj";
		///<summary>
		///LastModification Field name
		///</summary>	
		public const string FLD_NAVENODRIZA_FUM = "fum";
		///<summary>
		///Field name for attribute 'Nombre_NaveNodriza'
		///</summary>	
		public const string FLD_NAVENODRIZA_NOMBRE_NAVENODRIZA = "Nombre_NaveNodriza";

		///<summary>
		///Field name for identification attribute 'id_Aeronave'
		///</summary>	
		public const string FLD_AERONAVE_ID_AERONAVE = "id_Aeronave";
		///<summary>
		///DTEControl Field name
		///</summary>	
		public const string FLD_AERONAVE_ESTADOOBJ = "estadoobj";
		///<summary>
		///LastModification Field name
		///</summary>	
		public const string FLD_AERONAVE_FUM = "fum";
		///<summary>
		///Field name for attribute 'Nombre'
		///</summary>	
		public const string FLD_AERONAVE_NOMBRE = "Nombre";
		///<summary>
		///Field name for attribute 'MaximoPasajeros'
		///</summary>	
		public const string FLD_AERONAVE_MAXIMOPASAJEROS = "MaximoPasajeros";
		///<summary>
		///Field name for attribute 'Origen'
		///</summary>	
		public const string FLD_AERONAVE_ORIGEN = "Origen";
		///<summary>
		///Field name for attribute 'Destino'
		///</summary>	
		public const string FLD_AERONAVE_DESTINO = "Destino";

		///<summary>
		///Field name for identification attribute 'id_Pasajero'
		///</summary>	
		public const string FLD_PASAJERO_ID_PASAJERO = "id_Pasajero";
		///<summary>
		///DTEControl Field name
		///</summary>	
		public const string FLD_PASAJERO_ESTADOOBJ = "estadoobj";
		///<summary>
		///LastModification Field name
		///</summary>	
		public const string FLD_PASAJERO_FUM = "fum";
		///<summary>
		///Field name for attribute 'Nombre'
		///</summary>	
		public const string FLD_PASAJERO_NOMBRE = "Nombre";

		///<summary>
		///Field name for identification attribute 'id_PasajeroAeronave'
		///</summary>	
		public const string FLD_PASAJEROAERONAVE_ID_PASAJEROAERONAVE = "id_PasajeroAeronave";
		///<summary>
		///Foreign key 'fk_PasajeroAeronav'
		///</summary>	
		public const string FLD_PASAJEROAERONAVE_FK_AERONAVE_1 = "fk_Aeronave_1";
		///<summary>
		///Foreign key 'fk_PasajeroAeronav_0'
		///</summary>	
		public const string FLD_PASAJEROAERONAVE_FK_PASAJERO_1 = "fk_Pasajero_1";
		///<summary>
		///DTEControl Field name
		///</summary>	
		public const string FLD_PASAJEROAERONAVE_ESTADOOBJ = "estadoobj";
		///<summary>
		///LastModification Field name
		///</summary>	
		public const string FLD_PASAJEROAERONAVE_FUM = "fum";
		///<summary>
		///Field name for attribute 'NombreAeronave'
		///</summary>	
		public const string FLD_PASAJEROAERONAVE_NOMBREAERONAVE = "NombreAeronave";
		///<summary>
		///Field name for attribute 'NombrePasajero'
		///</summary>	
		public const string FLD_PASAJEROAERONAVE_NOMBREPASAJERO = "NombrePasajero";

		///<summary>
		///Field name for identification attribute 'id_RevisarAeronave'
		///</summary>	
		public const string FLD_REVISION_ID_REVISARAERONAVE = "id_RevisarAeronave";
		///<summary>
		///DTEControl Field name
		///</summary>	
		public const string FLD_REVISION_ESTADOOBJ = "estadoobj";
		///<summary>
		///LastModification Field name
		///</summary>	
		public const string FLD_REVISION_FUM = "fum";
		///<summary>
		///Field name for attribute 'NombreRevisor'
		///</summary>	
		public const string FLD_REVISION_NOMBREREVISOR = "NombreRevisor";
		///<summary>
		///Field name for attribute 'FechaRevision'
		///</summary>	
		public const string FLD_REVISION_FECHAREVISION = "FechaRevision";
		///<summary>
		///Field name for attribute 'Id_Aeronave'
		///</summary>	
		public const string FLD_REVISION_ID_AERONAVE = "Id_Aeronave";

		///<summary>
		///Field name for identification attribute 'id_RevisionPasajero'
		///</summary>	
		public const string FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO = "id_RevisionPasajero";
		///<summary>
		///Foreign key 'fk_RevisionPasajer'
		///</summary>	
		public const string FLD_REVISIONPASAJERO_FK_REVISION_1 = "fk_Revision_1";
		///<summary>
		///Foreign key 'fk_RevisionPasajer_0'
		///</summary>	
		public const string FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1 = "fk_PasajeroAero_1";
		///<summary>
		///DTEControl Field name
		///</summary>	
		public const string FLD_REVISIONPASAJERO_ESTADOOBJ = "estadoobj";
		///<summary>
		///LastModification Field name
		///</summary>	
		public const string FLD_REVISIONPASAJERO_FUM = "fum";

		///<summary>
		///Field name for identification attribute 'id_Administrador'
		///</summary>	
		public const string FLD_ADMINISTRADOR_ID_ADMINISTRADOR = "id_Administrador";
		///<summary>
		///PassWord Field name
		///</summary>	
		public const string FLD_ADMINISTRADOR_PASSWORD = "PassWord";
		///<summary>
		///DTEControl Field name
		///</summary>	
		public const string FLD_ADMINISTRADOR_ESTADOOBJ = "estadoobj";
		///<summary>
		///LastModification Field name
		///</summary>	
		public const string FLD_ADMINISTRADOR_FUM = "fum";

		#endregion Fields
	}
}
