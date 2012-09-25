// v3.8.4.5.b
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Data;
using System.Data.Common;

using SIGEM.Client.Oids;
using SIGEM.Client.Adaptor.DataFormats;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Adaptor
{
	#region ServerConnection -> Aux Method OID creator
	public partial class ServerConnection
	{
		#region Create an OID
		/// <summary>
		/// This function calls to the constructor of Oid.
		/// </summary>
		/// <param name="className">Class where will be the Oid.</param>
		/// <returns>Oid instance.</returns>
		public static Oid CreateOid(string className)
		{
			return Oid.Create(className);
		}
		/// <summary>
		/// This function calls to the constructor of Oid.
		/// </summary>
		/// <param name="className">Class where will be the Oid.</param>
		/// <param name="fields">Fields of the Oid.</param>
		/// <returns>Oid instance.</returns>
		public static Oid CreateOid(
			string className,
			List<KeyValuePair<ModelType,object>> fields)
		{
			return Oid.Create(className,fields);
		}
		#endregion Create an OID
	}
	#endregion ServerConnection -> Aux Method OID creator
}

