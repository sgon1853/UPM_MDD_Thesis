// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.SQL;
using SIGEM.Business.Data;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.Query
{
	///<summary>
	///This class adds to the SQL statement the formula of horizontal visibility
	///</summary>
	internal partial class AdministradorHorizontalVisibility : ONFilter
	{
		#region Filter In Data
		public override void FilterInData(ONSqlSelect onSql, ONDBData data)
		{
			if (IsUnableToFilterInData(data.OnContext))
				return;

			#region Horizontal visibility for agent 'Administrador'
			if(data.OnContext.LeafActiveAgentFacets.Contains("Administrador"))
			{
				// No Horizontal Visibility formula
			}
			#endregion Horizontal visibility for agent 'Administrador'

		}
		#endregion Filter In Data
	}
}
