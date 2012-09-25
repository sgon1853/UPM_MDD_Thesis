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
	internal class RevisionPasajeroInstance : ONInstance
	{
		#region Members
		public RevisionCollection RevisionRoleTemp;
		private RevisionOid mRevisionRoleOidTemp;
		public PasajeroAeronaveCollection PasajeroAeronaveRoleTemp;
		private PasajeroAeronaveOid mPasajeroAeronaveRoleOidTemp;
		public ONString StateObjAttrTemp;
		#endregion


		#region Properties
		#region Oid
		/// <summary>
		/// Oid of the instance
		/// </summary>
		public new RevisionPasajeroOid Oid
		{
			get
			{
				return base.Oid as RevisionPasajeroOid;
			}
			set
			{
				base.Oid = value;
			}
		}
		#endregion Oid
		
		#region Attribute id_RevisionPasajero (id_RevisionPasajero)
		/// <summary>
		/// RevisionPasajero's identification function
		/// </summary>
		[ONAttribute("id_RevisionPasajero", "autonumeric", Visibility = "Administrador", FacetOfField = "RevisionPasajero", FieldName = CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO)]
		public ONInt Id_RevisionPasajeroAttr
		{
			get
			{
				return Oid.Id_RevisionPasajeroAttr;
			}
	}
		#endregion Attribute id_RevisionPasajero (id_RevisionPasajero)



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

		#region Role Revision
		public bool RevisionRoleAttrModified = false;
		
		public RevisionOid RevisionRoleOidTemp
		{
			get
			{
				return mRevisionRoleOidTemp;
			}
			set
			{
				mRevisionRoleOidTemp = value;
				RevisionRoleTemp = null;
				RevisionRoleAttrModified = ((object)value != null);
			}
		}

		[ONRole("Revision", "Revision", "RevisionPasajero", "RevisionPasajero", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public RevisionCollection RevisionRole
		{
			get
			{
				if (RevisionRoleTemp == null)
				{
					RevisionRoleTemp = new RevisionCollection(OnContext);
					
					if ((object) RevisionRoleOidTemp == null)
					{
						// Get Oid from the data
						RevisionData lData = new RevisionData(OnContext);
						RevisionRole = lData.RevisionPasajeroRole(Oid) as RevisionCollection;
					}
					else
					{						// Get Instance
						RevisionRoleTemp = new RevisionCollection(OnContext);
					}
					if (RevisionRoleOidTemp != null)
						RevisionRoleTemp.Add(RevisionRoleOidTemp.GetInstance(OnContext));
				}
				
				return RevisionRoleTemp;
			}
			set
			{
				// Set Oid temp
				if ((value == null) || (value.Count == 0))
					RevisionRoleOidTemp = null;
				else
					RevisionRoleOidTemp = value[0].Oid;

				// Set role Oid
				RevisionRoleTemp = value;
			}
		}


		public RevisionCollection RevisionRoleHV()
		{
			RevisionData ldata = new RevisionData(OnContext);
			
			//Fix related instance
			ONLinkedToList lLinkedTo = new ONLinkedToList();
			lLinkedTo["RevisionPasajero"] = Oid;
			
			ONFilterList lFilterList = new ONFilterList();
			lFilterList.Add("HorizontalVisibility", new RevisionHorizontalVisibility());

			//Execute
			return ldata.ExecuteQuery(lLinkedTo, lFilterList, null, null, null, 1) as RevisionCollection;
		}
		#endregion  Role Revision

		#region Role PasajeroAeronave
		public bool PasajeroAeronaveRoleAttrModified = false;
		
		public PasajeroAeronaveOid PasajeroAeronaveRoleOidTemp
		{
			get
			{
				return mPasajeroAeronaveRoleOidTemp;
			}
			set
			{
				mPasajeroAeronaveRoleOidTemp = value;
				PasajeroAeronaveRoleTemp = null;
				PasajeroAeronaveRoleAttrModified = ((object)value != null);
			}
		}

		[ONRole("PasajeroAeronave", "PasajeroAeronave", "RevisionPasajero", "RevisionPasajero", Visibility = "Administrador", IsLegacy = false, HorizontalVisibility = "", EmptyHorizontalVisibility = "Administrador")]
		public PasajeroAeronaveCollection PasajeroAeronaveRole
		{
			get
			{
				if (PasajeroAeronaveRoleTemp == null)
				{
					PasajeroAeronaveRoleTemp = new PasajeroAeronaveCollection(OnContext);
					
					if ((object) PasajeroAeronaveRoleOidTemp == null)
					{
						// Get Oid from the data
						PasajeroAeronaveData lData = new PasajeroAeronaveData(OnContext);
						PasajeroAeronaveRole = lData.RevisionPasajeroRole(Oid) as PasajeroAeronaveCollection;
					}
					else
					{						// Get Instance
						PasajeroAeronaveRoleTemp = new PasajeroAeronaveCollection(OnContext);
					}
					if (PasajeroAeronaveRoleOidTemp != null)
						PasajeroAeronaveRoleTemp.Add(PasajeroAeronaveRoleOidTemp.GetInstance(OnContext));
				}
				
				return PasajeroAeronaveRoleTemp;
			}
			set
			{
				// Set Oid temp
				if ((value == null) || (value.Count == 0))
					PasajeroAeronaveRoleOidTemp = null;
				else
					PasajeroAeronaveRoleOidTemp = value[0].Oid;

				// Set role Oid
				PasajeroAeronaveRoleTemp = value;
			}
		}


		public PasajeroAeronaveCollection PasajeroAeronaveRoleHV()
		{
			PasajeroAeronaveData ldata = new PasajeroAeronaveData(OnContext);
			
			//Fix related instance
			ONLinkedToList lLinkedTo = new ONLinkedToList();
			lLinkedTo["RevisionPasajero"] = Oid;
			
			ONFilterList lFilterList = new ONFilterList();
			lFilterList.Add("HorizontalVisibility", new PasajeroAeronaveHorizontalVisibility());

			//Execute
			return ldata.ExecuteQuery(lLinkedTo, lFilterList, null, null, null, 1) as PasajeroAeronaveCollection;
		}
		#endregion  Role PasajeroAeronave



		#region Modified
		public override bool Modified
		{
			set
			{
				if (value == false)
				{
					RevisionRoleAttrModified = value;
					PasajeroAeronaveRoleAttrModified = value;
				}				
				base.Modified = value;
			}
		}
		#endregion Modified

		#endregion Properties

		#region Constructors
		/// <summary>Default Constructor</summary>
		public  RevisionPasajeroInstance(ONContext onContext) : base(onContext, "RevisionPasajero", "Clas_1348178673664478_Alias")
		{
			Oid = new RevisionPasajeroOid();
			RevisionRoleTemp = null;
			RevisionRoleOidTemp = null;
			PasajeroAeronaveRoleTemp = null;
			PasajeroAeronaveRoleOidTemp = null;
			StateObjAttrTemp = null;
		}

		public override void Copy(ONInstance instance)
		{
			RevisionPasajeroInstance linstance = instance as RevisionPasajeroInstance;
		
			Oid = new RevisionPasajeroOid(linstance.Oid);

			RevisionRoleTemp = linstance.RevisionRoleTemp;
			if (linstance.RevisionRoleOidTemp != null)
				RevisionRoleOidTemp = new RevisionOid(linstance.RevisionRoleOidTemp);
			PasajeroAeronaveRoleTemp = linstance.PasajeroAeronaveRoleTemp;
			if (linstance.PasajeroAeronaveRoleOidTemp != null)
				PasajeroAeronaveRoleOidTemp = new PasajeroAeronaveOid(linstance.PasajeroAeronaveRoleOidTemp);
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
			RevisionRole = null;
			PasajeroAeronaveRole = null;
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
