// 3.4.4.5

using System;
using System.Collections;
using SIGEM.Business.Instance;
using SIGEM.Business.SQL;
using SIGEM.Business.Data;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// Super class of the filters defined in the model
	/// </summary>
	internal abstract partial class ONFilter
	{
        #region Members
        public bool InData = false;
		public bool PreloadRelatedAttributes = true;
        #endregion

		#region Filter
		/// <summary>
		/// Makes the data checks
		/// </summary>
		/// <param name="onSql">Sql to execute</param>
		/// <param name="data">Data componente of the class</param>
		public virtual void FilterInData(ONSqlSelect onSql, ONDBData data)
		{
		}
		#endregion Filter
	}
}

