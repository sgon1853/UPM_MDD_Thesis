// 3.4.4.5
using System;
using System.Security.Permissions;
using System.EnterpriseServices;
using System.Collections.Specialized;
using System.Collections.Generic;
using SIGEM.Business.XML;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Data;
using SIGEM.Business.Server;
using SIGEM.Business.Executive;
using SIGEM.Business.Collection;

namespace SIGEM.Business.Action
{
	/// <summary>This class solves the Global Transactions</summary>
	[ONActionClass("")]
	[ONInterception()]
	internal class GlobalTransactionAction : ONAction
	{
		#region Constructors
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public GlobalTransactionAction(ONServiceContext onContext) : base(onContext, "")
		{
		}
		#endregion
		
	}
}
