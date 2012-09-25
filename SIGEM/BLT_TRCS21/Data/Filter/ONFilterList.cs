// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using SIGEM.Business.SQL;
using SIGEM.Business.Data;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// List of filters
	/// </summary>
	internal partial class ONFilterList: Dictionary<string, ONFilter>
	{
        #region Properties
        public bool InData
        {
            get
            {
                foreach (ONFilter lOnFilter in this.Values)
                    if (lOnFilter.InData)
                        return true;

                return false;
            }
        }
        #endregion Properties

		#region Filter
		/// <summary>
		/// Makes the data checks
		/// </summary>
		/// <param name="onSql">Sql to execute</param>
		/// <param name="data">Data componente of the class</param>
		public void FilterInData(ONSqlSelect onSql, ONDBData data)
		{
			foreach (ONFilter lOnFilter in Values)
				lOnFilter.FilterInData(onSql, data);
		}
		#endregion Filter
	}
}

