// 3.4.4.5
using System;
using System.Collections;
using System.Security.Permissions;
using System.Collections.Specialized;
using System.Collections.Generic;
using SIGEM.Business.Attributes;
using SIGEM.Business.Query;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Server;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Data;
using SIGEM.Business.Executive;

namespace SIGEM.Business.Server
{
	[ONServerClass("")]
	[ONInterception()]
	internal class GlobalTransactionServer : ONServer
	{
		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="onContext">This parameter has the current context</param>
		public  GlobalTransactionServer(ONServiceContext onContext) : base(onContext, null, "")
		{
		}
		#endregion

	}
}
