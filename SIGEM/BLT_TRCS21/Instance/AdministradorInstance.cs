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
	internal class AdministradorInstance : ONInstance
	{
		#region Members
		public ONString StateObjAttrTemp;
		public ONString PassWordAttrTemp;
		#endregion


		#region Properties
		#region Oid
		/// <summary>
		/// Oid of the instance
		/// </summary>
		public new AdministradorOid Oid
		{
			get
			{
				return base.Oid as AdministradorOid;
			}
			set
			{
				base.Oid = value;
			}
		}
		#endregion Oid
		
		#region Attribute id_Administrador (id_Administrador)
		/// <summary>
		/// Administrador's identification function
		/// </summary>
		[ONAttribute("id_Administrador", "autonumeric", Visibility = "Administrador", FacetOfField = "Administrador", FieldName = CtesBD.FLD_ADMINISTRADOR_ID_ADMINISTRADOR)]
		public ONInt Id_AdministradorAttr
		{
			get
			{
				return Oid.Id_AdministradorAttr;
			}
	}
		#endregion Attribute id_Administrador (id_Administrador)



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

		#region Attribute Password
		public bool PassWordAttrModified = false;
		/// <summary>
		/// Password of the agent
		/// </summary>
		[ONAttribute("Password", "Password", Visibility = "Administrador")]
		public ONString PassWordAttr
		{
			get
			{
				return PassWordAttrTemp;
			}
			set
			{
				if ((PassWordAttrTemp == null) || (PassWordAttrTemp != value))
				{
					PassWordAttrTemp = value;
					Modified = true;
					PassWordAttrModified = true;
				}
			}
		}	
		#endregion Attribute Password



		#region Modified
		public override bool Modified
		{
			set
			{
				if (value == false)
				{
				}				
				base.Modified = value;
			}
		}
		#endregion Modified

		#endregion Properties

		#region Constructors
		/// <summary>Default Constructor</summary>
		public  AdministradorInstance(ONContext onContext) : base(onContext, "Administrador", "Clas_1348605050880238_Alias")
		{
			Oid = new AdministradorOid();
			StateObjAttrTemp = null;
		}

		public override void Copy(ONInstance instance)
		{
			AdministradorInstance linstance = instance as AdministradorInstance;
		
			Oid = new AdministradorOid(linstance.Oid);

			PassWordAttr = linstance.PassWordAttr;
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
