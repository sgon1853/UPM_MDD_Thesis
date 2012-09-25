// 3.4.4.5
using System;
using System.Xml;
using System.Collections;
using System.Security;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Server;

namespace SIGEM.Business.XML
{
	[ONGateXMLAttribute("")]
	internal class GlobalTransactionXml : ONXml
	{
		#region Constructors
		/// <summary>Default Constructor</summary>
		public  GlobalTransactionXml() : base("GlobalTransaction")
		{
		}
		#endregion
		
	}
}
