// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlScalar.
	/// </summary>
	public class ONSqlScalar : ONSqlSelect
	{
		#region Members
		#region Distinct fields
		/// <summary>
		/// Collection of Distinct fields
		/// </summary>
		protected List<string> mDistinct = new List<string>();
		#endregion Distinct fields
		#endregion Members

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlScalar()
		{
		}
		public ONSqlScalar(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlScalar(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion Constructors

		#region AddMethods
		#region Distinct sentence
		/// <summary>
		/// Add a distinct column
		/// </summary>
		/// <param name="distinct">Column to add</param>
		public virtual void AddDistinct(string distinct)
		{
			mIsDistinct = true;
			mDistinct.Add(distinct);
		}
		#endregion Distinct sentence
		#endregion AddMethods
	}
}

