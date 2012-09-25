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
	internal class PasajeroAeronaveInstance : ONInstance
	{
		#region Members
		public ONString NombreAeronaveAttrTemp;
		public ONString NombrePasajeroAttrTemp;
		public RevisionPasajeroCollection RevisionPasajeroRoleTemp;
		public AeronaveCollection AeronaveRoleTemp;
		private AeronaveOid mAeronaveRoleOidTemp;
		public PasajeroCollection PasajeroRoleTemp;
		private PasajeroOid mPasajeroRoleOidTemp;
		public ONString StateObjAttrTemp;
		#endregion


		#region Properties
		#region Oid
		/// <summary>
		/// Oid of the instance
		/// </summary>
		public new PasajeroAeronaveOid Oid
		{
			get
			{
				return base.Oid as PasajeroAeronaveOid;
			}
			set
			{
				base.Oid = value;
			}
		}
		#endregion Oid
		
		#region Attribute id_PasajeroAeronave (id_PasajeroAeronave)
		/// <summary>
		/// PasajeroAeronave's identification function
		/// </summary>
		[ONAttribute("id_PasajeroAeronave", "autonumeric", Visibility = "Administrador", FacetOfField = "PasajeroAeronave", FieldName = CtesBD.FLD_PASAJEROAERONAVE_ID_PASAJEROAERONAVE)]
		public ONInt Id_PasajeroAeronaveAttr
		{
			get
			{
				return Oid.Id_PasajeroAeronaveAttr;
			}
	}
		#endregion Attribute id_PasajeroAeronave (id_PasajeroAeronave)

		#region Attribute NombreAeronave (NombreAeronave)
		/// <summary>
		/// NombreAeronave
		/// </summary>
		public bool NombreAeronaveAttrModified = false;

		/// <summary>
		/// NombreAeronave
		/// </summary>
		[ONAttribute("NombreAeronave", "string", Visibility = "Administrador", FacetOfField = "PasajeroAeronave", FieldName = CtesBD.FLD_PASAJEROAERONAVE_NOMBREAERONAVE)]
		public ONString NombreAeronaveAttr
		{
			get
			{


				return NombreAeronaveAttrTemp;
			}
			set
			{
				if ((NombreAeronaveAttrTemp == null) || (NombreAeronaveAttrTemp != value))
				{
					NombreAeronaveAttrTemp = value;
					Modified = true;
					NombreAeronaveAttrModified = true;
				}
			}
	}
		#endregion Attribute NombreAeronave (NombreAeronave)

		#region Attribute NombrePasajero (NombrePasajero)
		/// <summary>
		/// NombrePasajero
		/// </summary>
		public bool NombrePasajeroAttrModified = false;

		/// <summary>
		/// NombrePasajero
		/// </summary>
		[ONAttribute("NombrePasajero", "string", Visibility = "Administrador", FacetOfField = "PasajeroAeronave", FieldName = CtesBD.FLD_PASAJEROAERONAVE_NOMBREPASAJERO)]
		public ONString NombrePasajeroAttr
		{
			get
			{


				return NombrePasajeroAttrTemp;
			}
			set
			{
				if ((NombrePasajeroAttrTemp == null) || (NombrePasajeroAttrTemp != value))
				{
					NombrePasajeroAttrTemp = value;
					Modified = true;
					NombrePasajeroAttrModified = true;
				}
			}
	}
		#endregion Attribute NombrePasajero (NombrePasajero)



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

		[ONRole("RevisionPasajero", "RevisionPasajero", "PasajeroAeronave", "PasajeroAeronave", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public RevisionPasajeroCollection RevisionPasajeroRole
		{
			get
			{
				if (RevisionPasajeroRoleTemp == null)
				{
					RevisionPasajeroData ldata = new RevisionPasajeroData(OnContext);
					RevisionPasajeroRoleTemp = ldata.PasajeroAeronaveRole(Oid) as RevisionPasajeroCollection;
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
			lLinkedTo["PasajeroAeronave"] = Oid;
			
			ONFilterList lFilterList = new ONFilterList();
			lFilterList.Add("HorizontalVisibility", new RevisionPasajeroHorizontalVisibility());

			//Execute
			return ldata.ExecuteQuery(lLinkedTo, lFilterList, null, null, null, 1) as RevisionPasajeroCollection;
		
		}
		#endregion  RevisionPasajero

		#region Role Aeronave
		public bool AeronaveRoleAttrModified = false;
		
		public AeronaveOid AeronaveRoleOidTemp
		{
			get
			{
				return mAeronaveRoleOidTemp;
			}
			set
			{
				mAeronaveRoleOidTemp = value;
				AeronaveRoleTemp = null;
				AeronaveRoleAttrModified = ((object)value != null);
			}
		}

		[ONRole("Aeronave", "Aeronave", "PasajeroAeronave", "PasajeroAeronave", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public AeronaveCollection AeronaveRole
		{
			get
			{
				if (AeronaveRoleTemp == null)
				{
					AeronaveRoleTemp = new AeronaveCollection(OnContext);
					
					if ((object) AeronaveRoleOidTemp == null)
					{
						// Get Oid from the data
						AeronaveData lData = new AeronaveData(OnContext);
						AeronaveRole = lData.PasajeroAeronaveRole(Oid) as AeronaveCollection;
					}
					else
					{						// Get Instance
						AeronaveRoleTemp = new AeronaveCollection(OnContext);
					}
					if (AeronaveRoleOidTemp != null)
						AeronaveRoleTemp.Add(AeronaveRoleOidTemp.GetInstance(OnContext));
				}
				
				return AeronaveRoleTemp;
			}
			set
			{
				// Set Oid temp
				if ((value == null) || (value.Count == 0))
					AeronaveRoleOidTemp = null;
				else
					AeronaveRoleOidTemp = value[0].Oid;

				// Set role Oid
				AeronaveRoleTemp = value;
			}
		}


		public AeronaveCollection AeronaveRoleHV()
		{
			AeronaveData ldata = new AeronaveData(OnContext);
			
			//Fix related instance
			ONLinkedToList lLinkedTo = new ONLinkedToList();
			lLinkedTo["PasajeroAeronave"] = Oid;
			
			ONFilterList lFilterList = new ONFilterList();
			lFilterList.Add("HorizontalVisibility", new AeronaveHorizontalVisibility());

			//Execute
			return ldata.ExecuteQuery(lLinkedTo, lFilterList, null, null, null, 1) as AeronaveCollection;
		}
		#endregion  Role Aeronave

		#region Role Pasajero
		public bool PasajeroRoleAttrModified = false;
		
		public PasajeroOid PasajeroRoleOidTemp
		{
			get
			{
				return mPasajeroRoleOidTemp;
			}
			set
			{
				mPasajeroRoleOidTemp = value;
				PasajeroRoleTemp = null;
				PasajeroRoleAttrModified = ((object)value != null);
			}
		}

		[ONRole("Pasajero", "Pasajero", "PasajeroAeronave", "PasajeroAeronave", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public PasajeroCollection PasajeroRole
		{
			get
			{
				if (PasajeroRoleTemp == null)
				{
					PasajeroRoleTemp = new PasajeroCollection(OnContext);
					
					if ((object) PasajeroRoleOidTemp == null)
					{
						// Get Oid from the data
						PasajeroData lData = new PasajeroData(OnContext);
						PasajeroRole = lData.PasajeroAeronaveRole(Oid) as PasajeroCollection;
					}
					else
					{						// Get Instance
						PasajeroRoleTemp = new PasajeroCollection(OnContext);
					}
					if (PasajeroRoleOidTemp != null)
						PasajeroRoleTemp.Add(PasajeroRoleOidTemp.GetInstance(OnContext));
				}
				
				return PasajeroRoleTemp;
			}
			set
			{
				// Set Oid temp
				if ((value == null) || (value.Count == 0))
					PasajeroRoleOidTemp = null;
				else
					PasajeroRoleOidTemp = value[0].Oid;

				// Set role Oid
				PasajeroRoleTemp = value;
			}
		}


		public PasajeroCollection PasajeroRoleHV()
		{
			PasajeroData ldata = new PasajeroData(OnContext);
			
			//Fix related instance
			ONLinkedToList lLinkedTo = new ONLinkedToList();
			lLinkedTo["PasajeroAeronave"] = Oid;
			
			ONFilterList lFilterList = new ONFilterList();
			lFilterList.Add("HorizontalVisibility", new PasajeroHorizontalVisibility());

			//Execute
			return ldata.ExecuteQuery(lLinkedTo, lFilterList, null, null, null, 1) as PasajeroCollection;
		}
		#endregion  Role Pasajero



		#region Modified
		public override bool Modified
		{
			set
			{
				if (value == false)
				{
					NombreAeronaveAttrModified = value;
					NombrePasajeroAttrModified = value;
					AeronaveRoleAttrModified = value;
					PasajeroRoleAttrModified = value;
				}				
				base.Modified = value;
			}
		}
		#endregion Modified

		#endregion Properties

		#region Constructors
		/// <summary>Default Constructor</summary>
		public  PasajeroAeronaveInstance(ONContext onContext) : base(onContext, "PasajeroAeronave", "Clas_1348178542592177_Alias")
		{
			Oid = new PasajeroAeronaveOid();
			NombreAeronaveAttr = ONString.Null;
			NombrePasajeroAttr = ONString.Null;
			RevisionPasajeroRoleTemp = null;
			AeronaveRoleTemp = null;
			AeronaveRoleOidTemp = null;
			PasajeroRoleTemp = null;
			PasajeroRoleOidTemp = null;
			StateObjAttrTemp = null;
		}

		public override void Copy(ONInstance instance)
		{
			PasajeroAeronaveInstance linstance = instance as PasajeroAeronaveInstance;
		
			Oid = new PasajeroAeronaveOid(linstance.Oid);

			NombreAeronaveAttr = new ONString(linstance.NombreAeronaveAttr);
			NombrePasajeroAttr = new ONString(linstance.NombrePasajeroAttr);
			RevisionPasajeroRoleTemp = linstance.RevisionPasajeroRoleTemp;
			AeronaveRoleTemp = linstance.AeronaveRoleTemp;
			if (linstance.AeronaveRoleOidTemp != null)
				AeronaveRoleOidTemp = new AeronaveOid(linstance.AeronaveRoleOidTemp);
			PasajeroRoleTemp = linstance.PasajeroRoleTemp;
			if (linstance.PasajeroRoleOidTemp != null)
				PasajeroRoleOidTemp = new PasajeroOid(linstance.PasajeroRoleOidTemp);
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
			AeronaveRole = null;
			PasajeroRole = null;
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
