// 3.4.4.5

using System;
using System.Xml;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Query;

namespace SIGEM.Business.XML
{
	[ONGateXMLAttribute("Function")]
	internal class FunctionXml : ONXml
	{
		#region Constructors
		public  FunctionXml() : base("Function")
		{
		}
		#endregion


	}
}
