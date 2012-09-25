// 3.4.4.5
using System;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Query;
using SIGEM.Business.Data;
using SIGEM.Business.Collection;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.SQL;

namespace SIGEM.Business.Instance
{
	internal class AeronaveInstance : ONInstance
	{
		#region Members
		public ONText NombreAttrTemp;
		public ONInt MaximoPasajerosAttrTemp;
		public ONText OrigenAttrTemp;
		public ONText DestinoAttrTemp;
		public PasajeroAeronaveCollection PasajeroAeronaveRoleTemp;
		public ONString StateObjAttrTemp;
		#endregion


		#region Properties
		#region Oid
		/// <summary>
		/// Oid of the instance
		/// </summary>
		public new AeronaveOid Oid
		{
			get
			{
				return base.Oid as AeronaveOid;
			}
			set
			{
				base.Oid = value;
			}
		}
		#endregion Oid
		
		#region Attribute id_Aeronave (id_Aeronave)
		/// <summary>
		/// Aeronave's identification function
		/// </summary>
		[ONAttribute("id_Aeronave", "autonumeric", Visibility = "Administrador", FacetOfField = "Aeronave", FieldName = CtesBD.FLD_AERONAVE_ID_AERONAVE)]
		public ONInt Id_AeronaveAttr
		{
			get
			{
				return Oid.Id_AeronaveAttr;
			}
	}
		#endregion Attribute id_Aeronave (id_Aeronave)

		#region Attribute Nombre (Nombre)
		/// <summary>
		/// Nombre
		/// </summary>
		public bool NombreAttrModified = false;

		/// <summary>
		/// Nombre
		/// </summary>
		[ONAttribute("Nombre", "text", Visibility = "Administrador", FacetOfField = "Aeronave", FieldName = CtesBD.FLD_AERONAVE_NOMBRE)]
		public ONText NombreAttr
		{
			get
			{
				try
				{
					if ((object) NombreAttrTemp == null)
					{
						//Effect: Nombre
						NombreAttrTemp = LoadTextNombre(OnContext, Oid);
					}
					return NombreAttrTemp;
				}
				catch
				{
					return ONText.Null;
				}
			}
			set
			{
				if ((NombreAttrTemp == null) || (NombreAttrTemp != value))
				{
					NombreAttrTemp = value;
					Modified = true;
					NombreAttrModified = true;
				}
			}
	}
		#endregion Attribute Nombre (Nombre)

		#region Attribute MaximoPasajeros (MaximoPasajeros)
		/// <summary>
		/// MaximoPasajeros
		/// </summary>
		public bool MaximoPasajerosAttrModified = false;

		/// <summary>
		/// MaximoPasajeros
		/// </summary>
		[ONAttribute("MaximoPasajeros", "int", Visibility = "Administrador", FacetOfField = "Aeronave", FieldName = CtesBD.FLD_AERONAVE_MAXIMOPASAJEROS)]
		public ONInt MaximoPasajerosAttr
		{
			get
			{


				return MaximoPasajerosAttrTemp;
			}
			set
			{
				if ((MaximoPasajerosAttrTemp == null) || (MaximoPasajerosAttrTemp != value))
				{
					MaximoPasajerosAttrTemp = value;
					Modified = true;
					MaximoPasajerosAttrModified = true;
				}
			}
	}
		#endregion Attribute MaximoPasajeros (MaximoPasajeros)

		#region Attribute Origen (Origen)
		/// <summary>
		/// Origen
		/// </summary>
		public bool OrigenAttrModified = false;

		/// <summary>
		/// Origen
		/// </summary>
		[ONAttribute("Origen", "text", Visibility = "Administrador", FacetOfField = "Aeronave", FieldName = CtesBD.FLD_AERONAVE_ORIGEN)]
		public ONText OrigenAttr
		{
			get
			{
				try
				{
					if ((object) OrigenAttrTemp == null)
					{
						//Effect: Origen
						OrigenAttrTemp = LoadTextOrigen(OnContext, Oid);
					}
					return OrigenAttrTemp;
				}
				catch
				{
					return ONText.Null;
				}
			}
			set
			{
				if ((OrigenAttrTemp == null) || (OrigenAttrTemp != value))
				{
					OrigenAttrTemp = value;
					Modified = true;
					OrigenAttrModified = true;
				}
			}
	}
		#endregion Attribute Origen (Origen)

		#region Attribute Destino (Destino)
		/// <summary>
		/// Destino
		/// </summary>
		public bool DestinoAttrModified = false;

		/// <summary>
		/// Destino
		/// </summary>
		[ONAttribute("Destino", "text", Visibility = "Administrador", FacetOfField = "Aeronave", FieldName = CtesBD.FLD_AERONAVE_DESTINO)]
		public ONText DestinoAttr
		{
			get
			{
				try
				{
					if ((object) DestinoAttrTemp == null)
					{
						//Effect: Destino
						DestinoAttrTemp = LoadTextDestino(OnContext, Oid);
					}
					return DestinoAttrTemp;
				}
				catch
				{
					return ONText.Null;
				}
			}
			set
			{
				if ((DestinoAttrTemp == null) || (DestinoAttrTemp != value))
				{
					DestinoAttrTemp = value;
					Modified = true;
					DestinoAttrModified = true;
				}
			}
	}
		#endregion Attribute Destino (Destino)



		#region Attribute STD field
		/// <summary>
		/// State of the instance
		/// </summary>
		public ONString StateObj
		{
			get
			{
				return StateObjAttrTemp;
			}
			set
			{
				if ((StateObjAttrTemp == null) || (StateObjAttrTemp != value))
				{
					StateObjAttrTemp = value;
					Modified = true;
				}
			}
		}
		#endregion Attribute STD field

		#region Role PasajeroAeronave

		[ONRole("PasajeroAeronave", "PasajeroAeronave", "Aeronave", "Aeronave", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public PasajeroAeronaveCollection PasajeroAeronaveRole
		{
			get
			{
				if (PasajeroAeronaveRoleTemp == null)
				{
					PasajeroAeronaveData ldata = new PasajeroAeronaveData(OnContext);
					PasajeroAeronaveRoleTemp = ldata.AeronaveRole(Oid) as PasajeroAeronaveCollection;
				}
				
				return PasajeroAeronaveRoleTemp;
			}
			set
			{
				// Set role Oid
				PasajeroAeronaveRoleTemp = value;
			}
		}

		public PasajeroAeronaveCollection PasajeroAeronaveRoleHV()
		{
			PasajeroAeronaveData ldata = new PasajeroAeronaveData(OnContext);

			//Fix related instance
			ONLinkedToList lLinkedTo = new ONLinkedToList();
			lLinkedTo["Aeronave"] = Oid;
			
			ONFilterList lFilterList = new ONFilterList();
			lFilterList.Add("HorizontalVisibility", new PasajeroAeronaveHorizontalVisibility());

			//Execute
			return ldata.ExecuteQuery(lLinkedTo, lFilterList, null, null, null, 1) as PasajeroAeronaveCollection;
		
		}
		#endregion  PasajeroAeronave



		#region Modified
		public override bool Modified
		{
			set
			{
				if (value == false)
				{
					NombreAttrModified = value;
					MaximoPasajerosAttrModified = value;
					OrigenAttrModified = value;
					DestinoAttrModified = value;
				}				
				base.Modified = value;
			}
		}
		#endregion Modified

		#endregion Properties

		#region Constructors
		/// <summary>Default Constructor</summary>
		public  AeronaveInstance(ONContext onContext) : base(onContext, "Aeronave", "Clas_1348178411520734_Alias")
		{
			Oid = new AeronaveOid();
			NombreAttr = null;
			MaximoPasajerosAttr = ONInt.Null;
			OrigenAttr = null;
			DestinoAttr = null;
			PasajeroAeronaveRoleTemp = null;
			StateObjAttrTemp = null;
		}

		public override void Copy(ONInstance instance)
		{
			AeronaveInstance linstance = instance as AeronaveInstance;
		
			Oid = new AeronaveOid(linstance.Oid);

			if ((object) linstance.NombreAttrTemp != null)
				NombreAttrTemp = new ONText(linstance.NombreAttrTemp);
			else
				NombreAttrTemp = null;
			MaximoPasajerosAttr = new ONInt(linstance.MaximoPasajerosAttr);
			if ((object) linstance.OrigenAttrTemp != null)
				OrigenAttrTemp = new ONText(linstance.OrigenAttrTemp);
			else
				OrigenAttrTemp = null;
			if ((object) linstance.DestinoAttrTemp != null)
				DestinoAttrTemp = new ONText(linstance.DestinoAttrTemp);
			else
				DestinoAttrTemp = null;
			PasajeroAeronaveRoleTemp = linstance.PasajeroAeronaveRoleTemp;
			StateObj = new ONString(linstance.StateObj);
			
			base.Copy(instance);
		}
		#endregion

		#region Clean Cache
		/// <summary>This method cleans the temporal value of the derived attributes</summary>
		public override void CleanDerivationCache()
		{
			//Clean cache of the facets
		}

		/// <summary>This method cleans the temporal value for the roles of the instance</summary>
		public override void CleanRoleCache()
		{
			PasajeroAeronaveRole = null;
			//Clean cache of the facets
		}
		#endregion

		#region Inheritance Castings
		#endregion
		
		#region Association Operators
		#endregion

		#region LoadTexts
				/// <summary>Load the data retrieved from the Data Base to components of the application</summary>
		/// <param name="onContext">This parameter has the current context</param>
		/// <param name="oid">OID of the instance whose text attribute value is wanted to be loaded</param>
		public static ONText LoadTextNombre(ONContext onContext, AeronaveOid oid)
		{
			return AeronaveData.LoadTextNombre(onContext,oid);
		}
				/// <summary>Load the data retrieved from the Data Base to components of the application</summary>
		/// <param name="onContext">This parameter has the current context</param>
		/// <param name="oid">OID of the instance whose text attribute value is wanted to be loaded</param>
		public static ONText LoadTextOrigen(ONContext onContext, AeronaveOid oid)
		{
			return AeronaveData.LoadTextOrigen(onContext,oid);
		}
				/// <summary>Load the data retrieved from the Data Base to components of the application</summary>
		/// <param name="onContext">This parameter has the current context</param>
		/// <param name="oid">OID of the instance whose text attribute value is wanted to be loaded</param>
		public static ONText LoadTextDestino(ONContext onContext, AeronaveOid oid)
		{
			return AeronaveData.LoadTextDestino(onContext,oid);
		}
		#endregion LoadTexts

		#region LoadBlobs
		#endregion LoadBlobs

		#region Root
		public override ONInstance Root()
		{
			return this;
		}
		#endregion Root

		#region Leaf Active Facets
		public override StringCollection LeafActiveFacets()
		{
			StringCollection lLeafActiveFacets = new StringCollection();
			
			lLeafActiveFacets.Add(ClassName);

			return lLeafActiveFacets;
		}
		#endregion Leaf Active Facets	
	}
}
