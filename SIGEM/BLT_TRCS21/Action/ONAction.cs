// 3.4.4.5

using System;
using System.Collections.Generic;
using SIGEM.Business;
using SIGEM.Business.Instance;
using SIGEM.Business.OID;

namespace SIGEM.Business.Action
{
	/// <summary>
	/// Superclass of Action
	/// </summary>
	internal class ONAction : ContextBoundObject
	{
		#region Members
		private string mClassName;
		private ONInstance mInstance;
		private ONServiceContext mOnContext;
		#endregion

		#region Properties
		/// <summary>
		/// Class name of the Action class
		/// </summary>
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			set
			{
				mClassName = value;
			}
		}
		/// <summary>
		/// Represents THIS object
		/// </summary>
		public ONInstance Instance
		{
			get
			{
				return mInstance;
			}
			set
			{
				mInstance = value;
			}
		}
		/// <summary>
		/// Context necessary to execute a service
		/// </summary>
		public ONServiceContext OnContext
		{
			get
			{
				return mOnContext;
			}
			set
			{
				mOnContext = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONAction(ONServiceContext onContext, string className)
		{
			ClassName = className;
			OnContext = onContext;
		}
		#endregion

        public Dictionary<ONOid, ONInstance> CopyInstances(object[] ActualValues)
        {
            Dictionary<ONOid, ONInstance> AntValues = new Dictionary<ONOid, ONInstance>();
            foreach (ONInstance lInstance in ActualValues)
            {
      	        if ((lInstance != null) && (!AntValues.ContainsKey(lInstance.Oid)))
                {
                    ONInstance lCopyInstance = ONContext.GetComponent_Instance(lInstance.ClassName, OnContext);
                    lCopyInstance.Copy(lInstance);
                    AntValues.Add(lCopyInstance.Oid, lCopyInstance);
                }
            }
            return AntValues;
        }
	}
}

