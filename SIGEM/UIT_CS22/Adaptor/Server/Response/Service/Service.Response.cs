// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using SIGEM.Client.Adaptor.Exceptions;
using SIGEM.Client.Adaptor.DataFormats;

namespace SIGEM.Client.Adaptor
{
	#region <Service.Response>.
	internal class ServiceResponse
	{
		private Oids.Oid mOid = null;
		public Oids.Oid Oid
		{
			get
			{
				return mOid;
			}
			set
			{
				mOid = value;
			}
		}
		private Arguments mArguments = null;
		public Arguments Arguments
		{
			get
			{
				return mArguments;
			}
			set
			{
				mArguments = value;
			}
		}
	}
	#endregion <Service.Response>.
}

