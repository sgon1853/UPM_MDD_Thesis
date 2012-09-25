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
	internal class PasajeroInstance : ONInstance
	{
		#region Members
		public ONText NombreAttrTemp;
		public PasajeroAeronaveCollection PasajeroAeronaveRoleTemp;
		public ONString StateObjAttrTemp;
		#endregion


		#region Properties
		#region Oid
		/// <summary>
		/// Oid of the instance
		/// </summary>
		public new PasajeroOid Oid
		{
			get
			{
				return base.Oid as PasajeroOid;
			}
			set
			{
				base.Oid = value;
			}
		}
		#endregion Oid
		
		#region Attribute id_Pasajero (id_Pasajero)
		/// <summary>
		/// Pasajero's identification function
		/// </summary>
		[ONAttribute("id_Pasajero", "autonumeric", Visibility = "Administrador", FacetOfField = "Pasajero", FieldName = CtesBD.FLD_PASAJERO_ID_PASAJERO)]
		public ONInt Id_PasajeroAttr
		{
			get
			{
				return Oid.Id_PasajeroAttr;
			}
	}
		#endregion Attribute id_Pasajero (id_Pasajero)

		#region Attribute Nombre (Nombre)
		/// <summary>
		/// Nombre
		/// </summary>
		public bool NombreAttrModified = false;

		/// <summary>
		/// Nombre
		/// </summary>
		[ONAttribute("Nombre", "text", Visibility = "Administrador", FacetOfField = "Pasajero", FieldName = CtesBD.FLD_PASAJERO_NOMBRE)]
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

		[ONRole("PasajeroAeronave", "PasajeroAeronave", "Pasajero", "Pasajero", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public PasajeroAeronaveCollection PasajeroAeronaveRole
		{
			get
			{
				if (PasajeroAeronaveRoleTemp == null)
				{
					PasajeroAeronaveData ldata = new PasajeroAeronaveData(OnContext);
					PasajeroAeronaveRoleTemp = ldata.PasajeroRole(Oid) as PasajeroAeronaveCollection;
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
			lLinkedTo["Pasajero"] = Oid;
			
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
				}				
				base.Modified = value;
			}
		}
		#endregion Modified

		#endregion Properties

		#region Constructors
		/// <summary>Default Constructor</summary>
		public  PasajeroInstance(ONContext onContext) : base(onContext, "Pasajero", "Clas_1348178542592658_Alias")
		{
			Oid = new PasajeroOid();
			NombreAttr = null;
			PasajeroAeronaveRoleTemp = null;
			StateObjAttrTemp = null;
		}

		public override void Copy(ONInstance instance)
		{
			PasajeroInstance linstance = instance as PasajeroInstance;
		
			Oid = new PasajeroOid(linstance.Oid);

			if ((object) linstance.NombreAttrTemp != null)
				NombreAttrTemp = new ONText(linstance.NombreAttrTemp);
			else
				NombreAttrTemp = null;
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
		public static ONText LoadTextNombre(ONContext onContext, PasajeroOid oid)
		{
			return PasajeroData.LoadTextNombre(onContext,oid);
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
