// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using System.EnterpriseServices;
using System.Reflection;
using SIGEM.Business;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Attributes;
using SIGEM.Business.Collection;
using SIGEM.Business.Data;
using SIGEM.Business.Query;
using SIGEM.Business.Exceptions;
using System.Collections.Generic;

namespace SIGEM.Business.Instance
{
	/// <summary>
	/// Superclass of Instances
	/// </summary>
	internal abstract class ONInstance
	{
		#region Members
		public string ClassName;
		public string IdClass;
		public ONOid Oid;
		public ONDateTime Lmd;
		private bool mModified;
		public bool ModifiedInTransaction = false;
		public ONContext OnContext;
		public Dictionary<string, ONSimpleType> RelatedValues = new Dictionary<string, ONSimpleType>();
		#endregion Members

		#region Properties
		public virtual bool Modified
		{
			get
			{
				return mModified;
			}
			set
			{
				mModified = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="onContext">Context with all the information about the execution of the request</param>
		/// <param name="className">Name of the class that represents the instance</param>
		/// <param name="idClass">Identificator of the class</param>
		public ONInstance(ONContext onContext, string className, string idClass)
		{
			ClassName = className;
			IdClass = idClass;
			Oid = null;
			Lmd = ONDateTime.Null;
			Modified = false;
			OnContext = onContext;
		}
		/// <summary>
		/// Method of Copy
		/// </summary>
		/// <param name="instance">Instance to be copied</param>
		public virtual void Copy(ONInstance instance)
		{
			if (instance != null)
			{
				OnContext = instance.OnContext;
				ClassName = instance.ClassName;
				if ((object)instance.Lmd.Value == null)
					Lmd = null;
				else if (instance.Lmd.Value == null)
					Lmd = ONDateTime.Null;
				else
					Lmd = new ONDateTime(instance.Lmd.TypedValue);
				Modified = instance.Modified;
			}
		}
		#endregion Constructors

		#region Indexer
		public object this [string path]
		{
			get
			{
				return this[new ONPath(path)];
			}
		}
		public object this [ONPath onPath]
		{
			get
			{
				if ((onPath == null) || (onPath.Count == 0))
					return this;

				// Search in preloaded related attributes
				if (RelatedValues.ContainsKey(onPath.Path))
					return RelatedValues[onPath.Path];
					
				string lRol = onPath.RemoveHead();
				PropertyInfo lProperty = null;

				// Last unique role (like attributes)
				if (onPath.Count == 0)
				{
					lProperty = ONContext.GetPropertyInfoWithAttribute(GetType(), typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>");
					if (lProperty != null)
						return (lProperty.GetValue(this, null));
				}

				// Roles
				lProperty = ONContext.GetPropertyInfoWithAttribute(GetType(), typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>");
				if (lProperty != null)
				{
					if (onPath.Count == 0)
						return (lProperty.GetValue(this, null));
					else
						return (lProperty.GetValue(this, null) as ONCollection)[onPath];
				}

				return null;
			}
		}
		#endregion

		#region Attribute and Role Visibility
		/// <summary>
		/// Checks if the attributes of a determinate class are visible according to the agent connected
		/// </summary>
		/// <param name="typeInstance">Type fo the class to check the visibility</param>
		/// <param name="attributeVisibility">Attribute to be checked</param>
		/// <param name="onContext">Request context </param>
		public static bool IsVisible(Type typeInstance, string attributeVisibility, ONContext onContext)
		{
			return IsVisible(typeInstance, new ONPath(attributeVisibility), onContext);
		}
		/// <summary>
		/// Checks if the attributes of a determinate class are visible according to the agent connected
		/// </summary>
		/// <param name="typeInstance">Type fo the class to check the visibility</param>
		/// <param name="attributeVisibility">Attribute to be checked, it is not owned to the class that is being checked</param>
		/// <param name="onContext">Request context </param>
		public static bool IsVisible(Type typeInstance, ONPath attributeVisibility, ONContext onContext)
		{
			if (attributeVisibility.Count == 0)
				return true;

			ONPath lAttributeVisibility = new ONPath(attributeVisibility);
			string lRol = lAttributeVisibility.RemoveHead();

			// Attributes
			if (lAttributeVisibility.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return (lAttributeAttribute.IsVisible(onContext));
			}

			// Roles
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if (lAttributeVisibility.Count == 0)
					return (lRoleAttribute.IsVisible(onContext));
				else if (lRoleAttribute.IsVisible(onContext))
					return IsVisible(ONContext.GetType_Instance(lRoleAttribute.Domain), lAttributeVisibility, onContext);
			}

			return false;
		}
		/// <summary>
		/// Checks if the attributes of a determinate class are visible according to the agent connected
		/// </summary>
		/// <param name="typeInstance">Type fo the class to check the visibility</param>
		/// <param name="attributeVisibility">Attribute to be checked</param>
		/// <param name="onContext">Request context </param>
		public static bool IsVisibleInv(Type typeInstance, string attributeVisibility, ONContext onContext)
		{
			return IsVisibleInv(typeInstance, new ONPath(attributeVisibility), onContext);
		}
		/// <summary>
		/// Checks if the attributes of a determinate class are visible according to the agent connected
		/// </summary>
		/// <param name="typeInstance">Type fo the class to check the visibility</param>
		/// <param name="attributeVisibility">Attribute to be checked, it is not owned to the class that is being checked</param>
		/// <param name="onContext">Request context </param>
		public static bool IsVisibleInv(Type typeInstance, ONPath attributeVisibility, ONContext onContext)
		{
			if (attributeVisibility.Count == 0)
				return true;

			ONPath lAttributeVisibility = new ONPath(attributeVisibility);
			string lRol = lAttributeVisibility.RemoveHead();

			// Attributes
			if (lAttributeVisibility.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return (lAttributeAttribute.IsVisible(onContext));
			}

			// Roles
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				Type lTypeInstanceInv = ONContext.GetType_Instance(lRoleAttribute.Domain);
				return (IsVisible(lTypeInstanceInv, lRoleAttribute.RoleInv, onContext) && IsVisibleInv(lTypeInstanceInv, lAttributeVisibility, onContext));
			}

			return false;
		}
		public static bool IsLegacy(Type typeInstance, string rolePath)
		{
			ONPath lRolePath = new ONPath(rolePath);
			return IsLegacy(typeInstance, lRolePath);
		}
		public static bool IsLegacy(Type typeInstance, ONPath rolePath)
		{
			if (rolePath.Count == 0)
				return true;

			ONPath lRolePath = new ONPath(rolePath);
			string lRol = lRolePath.RemoveHead();

			// Attributes
			if (lRolePath.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return (lAttributeAttribute.IsLegacy);
			}
			
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if (lRolePath.Count == 0)
					return (lRoleAttribute.IsLegacy);
				else 
					return ((lRoleAttribute.IsLegacy) && (IsLegacy(ONContext.GetType_Instance(lRoleAttribute.Domain), lRolePath)));
			}

			return false;
		}
		public static bool IsLocal(Type typeInstance, ONPath rolePath)
		{
			if (rolePath.Count == 0)
				return true;

			ONPath lRolePath = new ONPath(rolePath);
			string lRol = lRolePath.RemoveHead();

			// Attributes
			if (lRolePath.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return (!lAttributeAttribute.IsLegacy);
			}
			
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if (lRolePath.Count == 0)
					return (!lRoleAttribute.IsLegacy);
				else 
					return ((!lRoleAttribute.IsLegacy) && (IsLocal(ONContext.GetType_Instance(lRoleAttribute.Domain), lRolePath)));
			}

			return false;
        }
		public static bool IsLocal(Type typeInstance, string rolePath)//, ONOid agentOid)
		{
			ONPath lRolePath = new ONPath(rolePath);
			return IsLocal(typeInstance, lRolePath);

		}
		public static string InversePath(Type typeInstance, ONPath rolePath)
		{
			ONPath lRolePath = new ONPath(rolePath);
			string lRol = lRolePath.RemoveHead();
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if (lRolePath.Count == 0)
					return (lRoleAttribute.RoleInv);
				else 
					return ((InversePath(ONContext.GetType_Instance(lRoleAttribute.Domain), lRolePath)) + "." + (lRoleAttribute.RoleInv));
			}
			return "";
		}
		/// <summary>
		/// Obtains the data type of an atribute. It is used to construcut the response message XML 
		/// </summary>
		/// <param name="typeInstance">Type fo the class to check the visibility</param>
		/// <param name="attributeInDisplay">Attribute to be checked, it is not owned to the class that is being checked</param>
		public static string GetTypeOfAttribute(Type typeInstance, ONPath attributeInDisplay)
		{
			string lRol = attributeInDisplay.RemoveHead();

			// Attributes
			if (attributeInDisplay.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return (lAttributeAttribute.Type);
			}

			// Roles
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if (attributeInDisplay.Count == 0)
					return ""; 
				else 
					return GetTypeOfAttribute(ONContext.GetType_Instance(lRoleAttribute.Domain), attributeInDisplay);
			}
			return "";
		}

		public static bool HasHorizontalVisibility(Type typeInstance, string rolePath, StringCollection activeAgentFacets)
		{
			ONPath lRolePath = new ONPath(rolePath);
			return HasHorizontalVisibility(typeInstance, lRolePath, activeAgentFacets);
		}
		public static bool HasHorizontalVisibility(Type typeInstance, ONPath rolePath, StringCollection activeAgentFacets)
		{
			ONPath lRolePath = new ONPath(rolePath);
			string lRol = lRolePath.RemoveHead();

			// Attributes
			if (lRolePath.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return false;
			}

			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if(lRoleAttribute.HasHorizontalVisibility(activeAgentFacets))
					return true;

				if (lRolePath.Count == 0)
					return false;
				else
					return (HasHorizontalVisibility(ONContext.GetType_Instance(lRoleAttribute.Domain), lRolePath, activeAgentFacets));
			}

			return false;
		}

		#endregion

		#region DisplaysetItemValue
		public ONSimpleType DisplaysetItemValue(string path)
		{
			return DisplaysetItemValue(new ONPath(path));
		}

		public ONSimpleType DisplaysetItemValue(ONPath displaysetItem)
		{
			if ((displaysetItem == null) || (displaysetItem.Count == 0))
				return null;

			ONPath lDisplaysetItem = new ONPath(displaysetItem);
			string lRol = lDisplaysetItem.RemoveHead();
			PropertyInfo lProperty = null;
			MethodInfo lMethod = null;
			string methodName = lRol + "RoleHV";
			
			// Last unique role (like attributes)
			if (lDisplaysetItem.Count == 0)
			{
				lProperty = ONContext.GetPropertyInfoWithAttribute(GetType(), typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>");
				if (lProperty != null)
					return (lProperty.GetValue(this, null)) as ONSimpleType;
			}

			// Roles			
			lMethod = GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (lMethod != null)
			{
				ONCollection lCollection = lMethod.Invoke(this, null) as ONCollection;
				if (lCollection.Count <= 0)
				{
					return ONSimpleType.Null(ONInstance.GetTypeOfAttribute(GetType(), displaysetItem));
				}

				ONInstance lInstance = lCollection[0];

				return lInstance.DisplaysetItemValue(lDisplaysetItem);
			}

			return null;
		}
		#endregion

		#region Find / Exist
		/// <summary>
		/// Finds the instance that represents this object in persistance tier 
		/// </summary>
		public void Find()
		{
			Find(null);
		}
		/// <summary>
		/// Finds the instance that represents this object in persistance tier. For this reaseon it is used the data component 
		/// </summary>
		public ONInstance Find(ONFilterList onFilterList)
		{
			ONData lData = ONContext.GetComponent_Data(ClassName, OnContext);
			ONCollection lCollection = lData.ExecuteQuery(new ONLinkedToList(), onFilterList, null, null, null, 0);

            ONInstance lInstance = null;
            if (lCollection.Count > 0)
            {
                lInstance = lCollection[0];
                Copy(lInstance);
			}
            return lInstance;
        }
		/// <summary>
		/// Comprobation that instance exists in the persistence tier
		/// </summary>
		public bool Exist()
		{
			return Exist(null);
		}
		/// <summary>
		/// Comprobation that instance exists in the persistence tier
		/// </summary>
		public bool Exist(ONFilterList onFilterList)
		{
			return ONContext.GetComponent_Data(ClassName, OnContext).Exist(Oid, onFilterList);
		}
		#endregion

		#region Operators
		public override bool Equals(object obj)
		{
			ONInstance lObject = obj as ONInstance;

			if (lObject == null)
            {
                if (obj is ONOid)
                    return (((object)Oid == (object) obj) || (Oid.Equals(obj)));
                else
                    return false;
            }

			return (((object) Oid == (object) lObject.Oid) || (Oid.Equals(lObject.Oid)));
		}
		public override int GetHashCode()
		{
			return Oid.GetHashCode();
		}
		public static bool operator==(ONInstance obj1, ONInstance obj2)
		{
			if ((((object) obj1 == null) || (obj1.Oid.Value == null)) && (((object) obj2 == null) || (obj2.Oid.Value == null)))
				return true;

			if (((object) obj1 == null) || (obj1.Oid.Value == null) || ((object) obj2 == null) || (obj2.Oid.Value == null))
				return false;

			ONBool lRes = (obj1.Oid == obj2.Oid);
			return lRes.TypedValue;
		}
		public static bool operator!=(ONInstance obj1, ONInstance obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region Clean Cache
		/// <summary>This method cleans the temporal value of the derived attributes</summary>
		public virtual void CleanDerivationCache()
		{
		}
		/// <summary>This method cleans the temporal value for the roles of the instance</summary>
		public virtual void CleanRoleCache()
		{
		}
		/// <summary>This method cleans the temporal value for the attributes of the instance</summary>
		public virtual void CleanAttributeCache()
		{
		}		
		#endregion

		#region Get Facet
		public ONInstance GetFacet(string className)
		{
			if (string.Compare(className, ClassName, true) == 0)
				return this;

			object lInstance = ONContext.GetPropertyWithAttribute(this, typeof(ONFacetAttribute), "<Facet>" + className + "</Facet>");
			if (lInstance == null)
				return ONContext.GetComponent_Instance(className, OnContext);

			return ONContext.GetPropertyWithAttribute(this, typeof(ONFacetAttribute), "<Facet>" + className + "</Facet>") as ONInstance;
		}
		public ArrayList GetFacets()
		{
			return ONContext.GetPropertiesWithAttribute(this, typeof(ONFacetAttribute));
		}
		#endregion
		
		#region Get Target Class
		public static string GetTargetClass(ONContext onContext, Type typeInstance, ONPath path)
		{
			foreach (string lRole in path.Roles)
			{
				// Attributes
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRole + "</Attribute>") as ONAttributeAttribute;

				if (lAttributeAttribute != null)
					return (lAttributeAttribute.FacetOfField);

				// Roles
				ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRole + "</Role>") as ONRoleAttribute;

				if (lRoleAttribute != null)
					typeInstance = ONContext.GetType_Instance(lRoleAttribute.Domain);
				else
					break;
			}

			if (typeInstance != null)
			{
				object[] lParameters = new object[1];
				lParameters[0] = onContext;

				ONInstance lInstance = Activator.CreateInstance(typeInstance, lParameters) as ONInstance;
				return lInstance.ClassName;
			}
			else
				return "";
		}
		#endregion

		#region GetFieldName
		/// <summary>
		/// Obtains by reflection the name of an attribute in the database 
		/// </summary>
		/// <param name="typeInstance">Type of the class</param>
		/// <param name="attributeInDisplay">Attribute to be checked</param>
		public static string GetFieldNameOfAttribute(Type typeInstance, ONPath attributeInDisplay)
		{
			string lRol = attributeInDisplay.RemoveHead();

			// Attributes
			if (attributeInDisplay.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return (lAttributeAttribute.FieldName);
			}

			// Roles
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if (attributeInDisplay.Count == 0)
					return "";
				else
					return GetFieldNameOfAttribute(ONContext.GetType_Instance(lRoleAttribute.Domain), attributeInDisplay);
			}
			return "";
		}
		#endregion GetFieldName

		#region IsOptimized
		public static bool IsOptimized(Type typeInstance, string path)
		{
			if (path == null)
				return true;

			return IsOptimized(typeInstance, new ONPath(path));
		}
		/// <summary>
		/// Obtains by reflection if an attribute is optimized
		/// </summary>
		/// <param name="typeInstance">Type of the class</param>
		/// <param name="attributeInDisplay">Attribute to be checked</param>
		public static bool IsOptimized(Type typeInstance, ONPath path)
		{
			if (path == null)
				return true;

			if (path.Count == 0)
				return true;

			ONPath lPath = new ONPath(path);
			string lRol = lPath.RemoveHead();

			// Attributes
			if (lPath.Count == 0)
			{
				ONAttributeAttribute lAttributeAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>") as ONAttributeAttribute;
				if (lAttributeAttribute != null)
					return (lAttributeAttribute.IsOptimized);
			}

			// Roles
			ONRoleAttribute lRoleAttribute = ONContext.GetAttributeInProperty(typeInstance, typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>") as ONRoleAttribute;
			if (lRoleAttribute != null)
			{
				if (lPath.Count == 0)
					return (!lRoleAttribute.IsLegacy);
				else if (!lRoleAttribute.IsLegacy)
					return IsOptimized(ONContext.GetType_Instance(lRoleAttribute.Domain), lPath);
			}

			return false;
		}
		#endregion IsOptimized

		#region Root
		public virtual ONInstance Root()
		{
			return this;
		}
		#endregion Root
		
		#region Leaf Active Facets
		public abstract StringCollection LeafActiveFacets();
		#endregion Leaf Active Facets		
	}
}

