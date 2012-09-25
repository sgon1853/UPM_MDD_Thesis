// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SIGEM.Business;
using SIGEM.Business.Attributes;

namespace SIGEM.Business
{
	/// <summary>
	/// This component is used to cache service information
	/// </summary>
	public class ONActionCacheItem
	{
		#region Members
		private string mComponentType;
		private string mClassName;

		private List<MethodInfo> mMethodInfoTriggers = new List<MethodInfo>();
		private List<MethodInfo> mMethodInfoIntegrityConstraints = new List<MethodInfo>();

		public static Dictionary<string, ONActionCacheItem> Cache = new Dictionary<string, ONActionCacheItem>(StringComparer.CurrentCultureIgnoreCase);
		#endregion

		#region Constructors
		public ONActionCacheItem()
		{
		}
		public ONActionCacheItem(string componentType, string className)
		{
			mComponentType = componentType;
			mClassName = className;
		}
		#endregion

		#region Get
		public static ONActionCacheItem Get(string componentType, string className)
		{
			ONActionCacheItem lActionCacheItem = null;
			string lXml = "<Class>" + className + "</Class>";

			if (Cache.ContainsKey(lXml))
				lActionCacheItem = Cache[lXml];

			if (lActionCacheItem == null)
			{
				lActionCacheItem = new ONActionCacheItem(componentType, className);
				if (lActionCacheItem.Load())
					Cache.Add(lXml, lActionCacheItem);
			}

			return lActionCacheItem;
		}
		#endregion

		#region Load
		public bool Load()
		{
			Type lType = ONContext.GetType(mComponentType, mClassName);
			string lXmlAttribute = "<Class>" + mClassName + "</Class>";

			bool lFinded = false;
			foreach (MethodInfo lMethodInfo in lType.GetMethods())
			{
				foreach (IONAttribute lAttribute in lMethodInfo.GetCustomAttributes(typeof(IONAttribute), true))
				{
					Type lAttributeType = lAttribute.GetType();
					lFinded = true;

					// Triggers
					if ((lAttributeType == typeof(ONTriggerAttribute) || lAttributeType.IsSubclassOf(typeof(ONTriggerAttribute))) && (string.Compare(lAttribute.ToString(), lXmlAttribute, true) == 0))
						mMethodInfoTriggers.Add(lMethodInfo);

					// IntegrityConstraints
					if ((lAttributeType == typeof(ONIntegrityConstraintAttribute) || lAttributeType.IsSubclassOf(typeof(ONIntegrityConstraintAttribute))) && (string.Compare(lAttribute.ToString(), lXmlAttribute, true) == 0))
						mMethodInfoIntegrityConstraints.Add(lMethodInfo);
				}
			}

			return lFinded;
		}
		#endregion

		#region Invoque
		public object InvoqueTriggers(object component, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in mMethodInfoTriggers)
				lReturn = ONContext.InvoqueMethod(component, lMethodInfo, parameters);

			return lReturn;
		}
		public object InvoqueIntegrityConstraints(object component, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in mMethodInfoIntegrityConstraints)
				lReturn = ONContext.InvoqueMethod(component, lMethodInfo, parameters);

			return lReturn;
		}
		#endregion
	}
}

