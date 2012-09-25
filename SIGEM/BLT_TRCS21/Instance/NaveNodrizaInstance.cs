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
	internal class NaveNodrizaInstance : ONInstance
	{
		#region Members
		public ONString Nombre_NaveNodrizaAttrTemp;
		public ONString StateObjAttrTemp;
		#endregion


		#region Properties
		#region Oid
		/// <summary>
		/// Oid of the instance
		/// </summary>
		public new NaveNodrizaOid Oid
		{
			get
			{
				return base.Oid as NaveNodrizaOid;
			}
			set
			{
				base.Oid = value;
			}
		}
		#endregion Oid
		
		#region Attribute id_NaveNodriza (id_NaveNodriza)
		/// <summary>
		/// NaveNodriza's identification function
		/// </summary>
		[ONAttribute("id_NaveNodriza", "autonumeric", Visibility = "Administrador", FacetOfField = "NaveNodriza", FieldName = CtesBD.FLD_NAVENODRIZA_ID_NAVENODRIZA)]
		public ONInt Id_NaveNodrizaAttr
		{
			get
			{
				return Oid.Id_NaveNodrizaAttr;
			}
	}
		#endregion Attribute id_NaveNodriza (id_NaveNodriza)

		#region Attribute Nombre_NaveNodriza (Nombre_NaveNodriza)
		/// <summary>
		/// Nombre_NaveNodriza
		/// </summary>
		public bool Nombre_NaveNodrizaAttrModified = false;

		/// <summary>
		/// Nombre_NaveNodriza
		/// </summary>
		[ONAttribute("Nombre_NaveNodriza", "string", Visibility = "Administrador", FacetOfField = "NaveNodriza", FieldName = CtesBD.FLD_NAVENODRIZA_NOMBRE_NAVENODRIZA)]
		public ONString Nombre_NaveNodrizaAttr
		{
			get
			{


				return Nombre_NaveNodrizaAttrTemp;
			}
			set
			{
				if ((Nombre_NaveNodrizaAttrTemp == null) || (Nombre_NaveNodrizaAttrTemp != value))
				{
					Nombre_NaveNodrizaAttrTemp = value;
					Modified = true;
					Nombre_NaveNodrizaAttrModified = true;
				}
			}
	}
		#endregion Attribute Nombre_NaveNodriza (Nombre_NaveNodriza)



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



		#region Modified
		public override bool Modified
		{
			set
			{
				if (value == false)
				{
					Nombre_NaveNodrizaAttrModified = value;
				}				
				base.Modified = value;
			}
		}
		#endregion Modified

		#endregion Properties

		#region Constructors
		/// <summary>Default Constructor</summary>
		public  NaveNodrizaInstance(ONContext onContext) : base(onContext, "NaveNodriza", "Clas_1347649273856884_Alias")
		{
			Oid = new NaveNodrizaOid();
			Nombre_NaveNodrizaAttr = ONString.Null;
			StateObjAttrTemp = null;
		}

		public override void Copy(ONInstance instance)
		{
			NaveNodrizaInstance linstance = instance as NaveNodrizaInstance;
		
			Oid = new NaveNodrizaOid(linstance.Oid);

			Nombre_NaveNodrizaAttr = new ONString(linstance.Nombre_NaveNodrizaAttr);
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
