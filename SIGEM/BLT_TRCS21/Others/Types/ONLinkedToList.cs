// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using SIGEM.Business.OID;

namespace SIGEM.Business.Types
{
	/// <summary>
	/// ONLinkedToList.
	/// </summary>
	internal class ONLinkedToList
	{
		#region Members
		public Dictionary<ONPath, ONOid> mLinkedToList = new Dictionary<ONPath, ONOid>();
		#endregion

		#region Properties
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONLinkedToList()
		{
		}
		#endregion

		#region Operations
		public IDictionaryEnumerator GetEnumerator()
		{
			return mLinkedToList.GetEnumerator();
		}
		public ONOid this [ONPath path]
		{
			get
			{
				return mLinkedToList[path];
			}
			set
			{
				mLinkedToList.Add(path, value);
			}
		}
		public ONOid this [string path]
		{
			get
			{
				return mLinkedToList[new ONPath(path)];
			}
			set
			{
				mLinkedToList.Add(new ONPath(path), value);
			}
		}
		#endregion
	}
}

