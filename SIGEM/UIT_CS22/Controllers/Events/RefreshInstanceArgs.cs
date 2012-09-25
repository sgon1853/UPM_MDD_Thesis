// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Service Result event.
	/// </summary>
	public class RefreshInstanceArgs : EventArgs
	{
        public Oid CurrentOid;
        public RefreshInstanceArgs(Oid currentOid)
			: base()
		{
            CurrentOid = currentOid;    
		}
	}
}


