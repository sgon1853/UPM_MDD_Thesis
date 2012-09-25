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
	internal class RevisionInstance : ONInstance
	{
		#region Members
		public ONString NombreRevisorAttrTemp;
		public ONDate FechaRevisionAttrTemp;
		public ONString Id_AeronaveAttrTemp;
		public RevisionPasajeroCollection RevisionPasajeroRoleTemp;
		public ONString StateObjAttrTemp;
		#endregion


		#region Properties
		#region Oid
		/// <summary>
		/// Oid of the instance
		/// </summary>
		public new RevisionOid Oid
		{
			get
			{
				return base.Oid as RevisionOid;
			}
			set
			{
				base.Oid = value;
			}
		}
		#endregion Oid
		
		#region Attribute id_RevisarAeronave (id_RevisarAeronave)
		/// <summary>
		/// RevisarAeronave's identification function
		/// </summary>
		[ONAttribute("id_RevisarAeronave", "autonumeric", Visibility = "Administrador", FacetOfField = "Revision", FieldName = CtesBD.FLD_REVISION_ID_REVISARAERONAVE)]
		public ONInt Id_RevisarAeronaveAttr
		{
			get
			{
				return Oid.Id_RevisarAeronaveAttr;
			}
	}
		#endregion Attribute id_RevisarAeronave (id_RevisarAeronave)

		#region Attribute NombreRevisor (NombreRevisor)
		/// <summary>
		/// NombreRevisor
		/// </summary>
		public bool NombreRevisorAttrModified = false;

		/// <summary>
		/// NombreRevisor
		/// </summary>
		[ONAttribute("NombreRevisor", "string", Visibility = "Administrador", FacetOfField = "Revision", FieldName = CtesBD.FLD_REVISION_NOMBREREVISOR)]
		public ONString NombreRevisorAttr
		{
			get
			{


				return NombreRevisorAttrTemp;
			}
			set
			{
				if ((NombreRevisorAttrTemp == null) || (NombreRevisorAttrTemp != value))
				{
					NombreRevisorAttrTemp = value;
					Modified = true;
					NombreRevisorAttrModified = true;
				}
			}
	}
		#endregion Attribute NombreRevisor (NombreRevisor)

		#region Attribute FechaRevision (FechaRevision)
		/// <summary>
		/// FechaRevision
		/// </summary>
		public bool FechaRevisionAttrModified = false;

		/// <summary>
		/// FechaRevision
		/// </summary>
		[ONAttribute("FechaRevision", "date", Visibility = "Administrador", FacetOfField = "Revision", FieldName = CtesBD.FLD_REVISION_FECHAREVISION)]
		public ONDate FechaRevisionAttr
		{
			get
			{


				return FechaRevisionAttrTemp;
			}
			set
			{
				if ((FechaRevisionAttrTemp == null) || (FechaRevisionAttrTemp != value))
				{
					FechaRevisionAttrTemp = value;
					Modified = true;
					FechaRevisionAttrModified = true;
				}
			}
	}
		#endregion Attribute FechaRevision (FechaRevision)

		#region Attribute Id_Aeronave (Id_Aeronave)
		/// <summary>
		/// Id_Aeronave
		/// </summary>
		public bool Id_AeronaveAttrModified = false;

		/// <summary>
		/// Id_Aeronave
		/// </summary>
		[ONAttribute("Id_Aeronave", "string", Visibility = "Administrador", FacetOfField = "Revision", FieldName = CtesBD.FLD_REVISION_ID_AERONAVE)]
		public ONString Id_AeronaveAttr
		{
			get
			{


				return Id_AeronaveAttrTemp;
			}
			set
			{
				if ((Id_AeronaveAttrTemp == null) || (Id_AeronaveAttrTemp != value))
				{
					Id_AeronaveAttrTemp = value;
					Modified = true;
					Id_AeronaveAttrModified = true;
				}
			}
	}
		#endregion Attribute Id_Aeronave (Id_Aeronave)



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

		#region Role RevisionPasajero

		[ONRole("RevisionPasajero", "RevisionPasajero", "Revision", "Revision", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public RevisionPasajeroCollection RevisionPasajeroRole
		{
			get
			{
				if (RevisionPasajeroRoleTemp == null)
				{
					RevisionPasajeroData ldata = new RevisionPasajeroData(OnContext);
					RevisionPasajeroRoleTemp = ldata.RevisionRole(Oid) as RevisionPasajeroCollection;
				}
				
				return RevisionPasajeroRoleTemp;
			}
			set
			{
				// Set role Oid
				RevisionPasajeroRoleTemp = value;
			}
		}

		public RevisionPasajeroCollection RevisionPasajeroRoleHV()
		{
			RevisionPasajeroData ldata = new RevisionPasajeroData(OnContext);

			//Fix related instance
			ONLinkedToList lLinkedTo = new ONLinkedToList();
			lLinkedTo["Revision"] = Oid;
			
			ONFilterList lFilterList = new ONFilterList();
			lFilterList.Add("HorizontalVisibility", new RevisionPasajeroHorizontalVisibility());

			//Execute
			return ldata.ExecuteQuery(lLinkedTo, lFilterList, null, null, null, 1) as RevisionPasajeroCollection;
		
		}
		#endregion  RevisionPasajero



		#region Modified
		public override bool Modified
		{
			set
			{
				if (value == false)
				{
					NombreRevisorAttrModified = value;
					FechaRevisionAttrModified = value;
					Id_AeronaveAttrModified = value;
				}				
				base.Modified = value;
			}
		}
		#endregion Modified

		#endregion Properties

		#region Constructors
		/// <summary>Default Constructor</summary>
		public  RevisionInstance(ONContext onContext) : base(onContext, "Revision", "Clas_1348178542592347_Alias")
		{
			Oid = new RevisionOid();
			NombreRevisorAttr = ONString.Null;
			FechaRevisionAttr = ONDate.Null;
			Id_AeronaveAttr = ONString.Null;
			RevisionPasajeroRoleTemp = null;
			StateObjAttrTemp = null;
		}

		public override void Copy(ONInstance instance)
		{
			RevisionInstance linstance = instance as RevisionInstance;
		
			Oid = new RevisionOid(linstance.Oid);

			NombreRevisorAttr = new ONString(linstance.NombreRevisorAttr);
			FechaRevisionAttr = new ONDate(linstance.FechaRevisionAttr);
			Id_AeronaveAttr = new ONString(linstance.Id_AeronaveAttr);
			RevisionPasajeroRoleTemp = linstance.RevisionPasajeroRoleTemp;
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
			RevisionPasajeroRole = null;
			//Clean cache of the facets
		}
		#endregion

		#region Inheritance Castings
		#endregion
		
		#region Association Operators
		#endregion

		#region LoadTexts
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
