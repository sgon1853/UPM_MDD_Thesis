// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using SIGEM.Business.Attributes;
using SIGEM.Business.Action;
using SIGEM.Business.Server;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Collection;

namespace SIGEM.Business
{
	/// <summary>
	/// Specialization of OnContext that stores the context of the execution of a service.
	/// </summary>
	internal class ONServiceContext : ONContext
	{
		#region Members
		// Stack of the transaction
		public Stack<ONInstance> TransactionStack;
		// Stack of the operation
		public Stack<ONInstance> OperationStack;
		// Class Modified
		public Dictionary<ONOid, ONAction> ModifiedClass;
		// Triggers
		public List<ONTrigger> AcceptedTriggers;
		// Shared Event
		public bool InSharedContext; // Is in shared context
        // Initial values
        public Dictionary<ONOid, ONInstance> InstancesInitialValues;
		public Dictionary<String, ONChangeDetectionInfo> ChangeDetectionElements;
		public bool NeedsVerification; 
        #endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONServiceContext()
		{
			TransactionStack = new Stack<ONInstance>();
			OperationStack = new Stack<ONInstance>();
			ModifiedClass = new Dictionary<ONOid, ONAction>();
			AcceptedTriggers = new List<ONTrigger>();
			ChangeDetectionElements = new Dictionary<String, ONChangeDetectionInfo>();
			NeedsVerification = false; 
			InSharedContext = false;
		}
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="onContext">Context to copy from</param>
		public ONServiceContext(ONServiceContext onContext)
			: base(onContext)
		{
			TransactionStack = onContext.TransactionStack;
			OperationStack = onContext.OperationStack;
			ModifiedClass = new Dictionary<ONOid, ONAction>(onContext.ModifiedClass);
			AcceptedTriggers = onContext.AcceptedTriggers;
			ChangeDetectionElements = onContext.ChangeDetectionElements;
			NeedsVerification = onContext.NeedsVerification;
			InSharedContext = false;
		}
		#endregion

		#region ModifiedClass
		/// <summary>
		/// Marks the action class as modified to control what classes has been changed since the point of entry.
		/// The instance is putted if it's marked as modified
		/// </summary>
		/// <param name="filter">Action component that represents the class that is modified</param>
		public void AddModifiedClass(ONAction action)
		{
			if (action.Instance.Modified)
			{
				ModifiedClass.Remove(action.Instance.Oid);
				ModifiedClass.Add(action.Instance.Oid, action);
			}
		}
		/// <summary>
		/// Puts the instance in the modified class list to check integrity constraints and trigger conditions
		/// The instance is putted thought it's not marked as modified (only in insert / deletion events)
		/// </summary>
		/// <param name="filter">Action component that represents the class that is modified</param>
		public void AddUnmodifiedClass(ONAction action)
		{
			ModifiedClass.Remove(action.Instance.Oid);
			ModifiedClass.Add(action.Instance.Oid, action);
		}
		/// <summary>
		/// Puts the Oid in the modified class list to check integrity constraints and trigger conditions
		/// The Oid is added thought it's not marked as modified
		/// </summary>
		/// <param name="oid">Oid that represents the instance to be added</param>
		public void AddUnmodifiedClass(ONOid oid)
		{
			ModifiedClass.Remove(oid);
			ONAction lAction = GetComponent_Action(oid.ClassName, this);
			lAction.Instance = GetComponent_Instance(oid.ClassName, this);
			ModifiedClass.Add(oid, lAction);
		}
		/// <summary>
		/// Puts each instance of the collection in the modified class list to check integrity constraints and trigger conditions
		/// The instances are added thought they are not marked as modified
		/// </summary>
		/// <param name="collection">Oid that represents the instance to be added</param>
		public void AddUnmodifiedClass(ONCollection collection)
		{
			foreach (ONInstance lInstance in collection)
			AddUnmodifiedClass(lInstance.Oid);
		}
		#endregion ModifiedClass

		#region IntegrityConstraints
		/// <summary>
		/// Controls the integrity constraints of the model object
		/// </summary>
		public void CheckIntegrityConstraints()
		{
			foreach (KeyValuePair<ONOid, ONAction> lDictionaryEntry in ModifiedClass)
			{
				ONAction lAction = lDictionaryEntry.Value;

				ONActionCacheItem lActionCacheItem = ONActionCacheItem.Get("Action", lAction.ClassName);
				lActionCacheItem.InvoqueIntegrityConstraints(lAction, new object[] { lDictionaryEntry.Key });

                ONInstance lInstance = (lDictionaryEntry.Key as ONOid).GetInstance(lAction.OnContext);
                foreach (ONInstance lNetInstance in lInstance.GetFacets())
                {
                    if (((object)lNetInstance != null) && (lNetInstance.ClassName != lAction.Instance.ClassName))
                    {
                        // Create action class
                        ONAction lNetAction = ONContext.GetComponent_Action(lNetInstance.ClassName, lNetInstance.OnContext);
                        lNetAction.Instance = lNetInstance;
                        ONActionCacheItem lNetActionCacheItem = ONActionCacheItem.Get("Action", lNetInstance.ClassName);
                        lNetActionCacheItem.InvoqueIntegrityConstraints(lNetAction, new object[] { lNetInstance.Oid });
                    }
                }
			}
		}
		#endregion

		#region Triggers
		/// <summary>
		/// Controls the triggers of the model object
		/// </summary>
		public void CheckTriggers()
		{
			foreach (KeyValuePair<ONOid, ONAction> lDictionaryEntry in ModifiedClass)
			{
				ONAction lAction = lDictionaryEntry.Value;

				ONActionCacheItem lActionCacheItem = ONActionCacheItem.Get("Action", lAction.ClassName);
				lActionCacheItem.InvoqueTriggers(lAction, new object[] { lDictionaryEntry.Key });
                
                ONInstance lInstance = (lDictionaryEntry.Key as ONOid).GetInstance(lAction.OnContext);
                foreach (ONInstance lNetInstance in lInstance.GetFacets())
                {
                    if (((object)lNetInstance != null) && (lNetInstance.ClassName != lAction.Instance.ClassName))
                    {
                        // Create action class
                        ONAction lNetAction = ONContext.GetComponent_Action(lNetInstance.ClassName, lNetInstance.OnContext);
                        lNetAction.Instance = lNetInstance;
                        ONActionCacheItem lNetActionCacheItem = ONActionCacheItem.Get("Action", lNetInstance.ClassName);
                        lNetActionCacheItem.InvoqueTriggers(lNetAction, new object[] { lNetInstance.Oid });
                    }
                }
			}
		}
		/// <summary>
		/// Marks the trigger as accepted to be thrown at the end of the execution of the service
		/// </summary>
		/// <param name="filter">Trigger accepted and waiting to be throwed</param>
		public void AddAcceptedTrigger(ONTrigger onTrigger)
		{
			AcceptedTriggers.Add(onTrigger);
		}
		/// <summary>
		/// Throws the triggers that has been accepted
		/// </summary>
		public void ThrowAcceptedTriggers()
		{
			List<ONTrigger> lAcceptedTriggers = AcceptedTriggers;
			AcceptedTriggers = new List<ONTrigger>();

			foreach (ONTrigger lOnTrigger in lAcceptedTriggers)
			{
                try
                {
                    object[] lParameters = new object[lOnTrigger.InputParameters.Count + lOnTrigger.OutputParameters.Count];
    				int i = 0;


					ONInstance lThis = null;

					foreach (DictionaryEntry lDictionaryEntry in lOnTrigger.InputParameters)
					{
						lParameters[i++] = lDictionaryEntry.Value;
						
						if (string.Compare((lDictionaryEntry.Key as string), lOnTrigger.ArgumentThisName, true) == 0)
							lThis = (lDictionaryEntry.Value as ONOid).GetInstance(this);
					}

	    			foreach (DictionaryEntry lDictionaryEntry in lOnTrigger.OutputParameters)
    					lParameters[i++] = lDictionaryEntry.Value;

		    		ONServiceContext lContext = new ONServiceContext();
		    		lContext.OidAgent = OidAgent;
				    ONServer lComponent = ONContext.GetComponent_Server(lOnTrigger.ClassName, this, lThis);
				    ONContext.InvoqueMethod(lComponent, typeof(ONServiceAttribute), "<Class>" + lOnTrigger.ClassName + "</Class><Service>" + lOnTrigger.ServiceName + "</Service>", lParameters);
                }
                catch {}
			}
		}
		#endregion

		#region Initial Instances Values
		public void GetInitialValues(ONAction action, object[] arguments)
		{
			object[] lArgumentsActualValue = new object[arguments.Length];
			int j = 0;
			while (j <= arguments.Length - 1)
			{
				if (arguments[j] is ONOid)
					lArgumentsActualValue[j] = (arguments[j] as ONOid).GetInstance(action.OnContext);
				j++;
			}
			object lReturn = ONContext.InvoqueMethod(action, "CopyInstances", new object[] { lArgumentsActualValue });
			InstancesInitialValues = lReturn as Dictionary<ONOid, ONInstance>;
		}
		#endregion

		#region Change Detection Differences
		public Dictionary<String, ONChangeDetectionInfo> ChangeDetectionDifferences()
		{
			Dictionary<String, ONChangeDetectionInfo> lDifferences = new Dictionary<string, ONChangeDetectionInfo>();

			foreach (KeyValuePair<string, ONChangeDetectionInfo> lChangeDetection in ChangeDetectionElements)
			{
				ONChangeDetectionInfo lChange = lChangeDetection.Value;
				if (!lChange.CheckValues())
					lDifferences.Add(lChange.Key, lChange);
			}
			return lDifferences;
		}
		#endregion
	}
}
