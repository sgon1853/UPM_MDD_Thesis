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
	internal class ONServiceCacheItem
	{
		#region Members
		private string mComponentType;
		private string mClassName;
		private string mServiceName;

		private List<MethodInfo> mMethodInfoSTD = new List<MethodInfo>();
		private List<MethodInfo> mMethodInfoPrecondition = new List<MethodInfo>();
		private List<MethodInfo> mMethodInfoService = new List<MethodInfo>();
		private List<MethodInfo> mMethodInfoOutboundArguments = new List<MethodInfo>();
		private List<MethodInfo> mMethodInfoCheckState = new List<MethodInfo>();

		public static Dictionary<string, ONServiceCacheItem> Cache = new Dictionary<string, ONServiceCacheItem>();
		#endregion

		#region Constructors
		public ONServiceCacheItem()
		{
		}
		public ONServiceCacheItem(string componentType, string className, string serviceName)
		{
			mComponentType = componentType;
			mClassName = className;
			mServiceName = serviceName;
        }
		#endregion

		#region Get
		public static ONServiceCacheItem Get(string componentType, string className, string serviceName)
		{
			ONServiceCacheItem lServiceCacheItem = null;
			string lXml = "<Class>" + className + "</Class><Service>" + serviceName + "</Service>";

			if (Cache.ContainsKey(lXml))
				lServiceCacheItem = Cache[lXml];

			if (lServiceCacheItem == null)
			{
				lServiceCacheItem = new ONServiceCacheItem(componentType, className, serviceName);
				if (lServiceCacheItem.Load())
					Cache.Add(lXml, lServiceCacheItem);
			}

			return lServiceCacheItem;
		}
		#endregion

		#region Load
		public bool Load()
		{
			Type lType = ONContext.GetType(mComponentType, mClassName);
			string lXmlAttribute = "<Class>" + mClassName + "</Class><Service>" + mServiceName + "</Service>";

			bool lFinded = false;
			foreach (MethodInfo lMethodInfo in lType.GetMethods())
			{
				foreach (IONAttribute lAttribute in lMethodInfo.GetCustomAttributes(typeof(IONAttribute), true))
				{
					Type lAttributeType = lAttribute.GetType();
					lFinded = true;

					// STD
					if ((lAttributeType == typeof(ONSTDAttribute) || lAttributeType.IsSubclassOf(typeof(ONSTDAttribute))) && (string.Compare(lAttribute.ToString(), lXmlAttribute, true) == 0))
						mMethodInfoSTD.Add(lMethodInfo);

					// Precondition
					if ((lAttributeType == typeof(ONPreconditionAttribute) || lAttributeType.IsSubclassOf(typeof(ONPreconditionAttribute))) && (string.Compare(lAttribute.ToString(), lXmlAttribute, true) == 0))
						mMethodInfoPrecondition.Add(lMethodInfo);

					// Check State
					if ((lAttributeType == typeof(ONCheckStateAttribute) || lAttributeType.IsSubclassOf(typeof(ONCheckStateAttribute))) && (string.Compare(lAttribute.ToString(), lXmlAttribute, true) == 0))
						mMethodInfoCheckState.Add(lMethodInfo);

					// Service
					if ((lAttributeType == typeof(ONServiceAttribute) || lAttributeType.IsSubclassOf(typeof(ONServiceAttribute))) && (string.Compare(lAttribute.ToString(), lXmlAttribute, true) == 0))
						mMethodInfoService.Add(lMethodInfo);

					// OutboundArguments
					if ((lAttributeType == typeof(ONOutboundArgumentsAttribute) || lAttributeType.IsSubclassOf(typeof(ONOutboundArgumentsAttribute))) && (string.Compare(lAttribute.ToString(), lXmlAttribute, true) == 0))
						mMethodInfoOutboundArguments.Add(lMethodInfo);
				}
			}

			return lFinded;
		}
		#endregion

		#region Invoque
		public object InvoqueSTD(object component, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in mMethodInfoSTD)
				lReturn = ONContext.InvoqueMethod(component, lMethodInfo, parameters);

			return lReturn;
		}
		public object InvoquePrecondition(object component, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in mMethodInfoPrecondition)
				lReturn = ONContext.InvoqueMethod(component, lMethodInfo, parameters);

			return lReturn;
		}
		public object InvoqueService(object component, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in mMethodInfoService)
				lReturn = ONContext.InvoqueMethod(component, lMethodInfo, parameters);

			return lReturn;
		}
		public object InvoqueOutboundArguments(object component, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in mMethodInfoOutboundArguments)
				lReturn = ONContext.InvoqueMethod(component, lMethodInfo, parameters);

			return lReturn;
		}
		public object InvoqueCheckState(object component, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in mMethodInfoCheckState)
				lReturn = ONContext.InvoqueMethod(component, lMethodInfo, parameters);

			return lReturn;
		}
		#endregion
	}
}

