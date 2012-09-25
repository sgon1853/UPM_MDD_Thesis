// 3.4.4.5

using System;
using System.Collections.Specialized;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONRoleAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	internal class ONRoleAttribute: Attribute, IONAttribute
	{
		#region Attribute
		public string Role;
		public string RoleInv;
		public string Domain;
		public string DomainInv;
		public string Visibility = "";
		public bool IsLegacy = false;
		public string HorizontalVisibility = "";
		public string EmptyHorizontalVisibility = "";
		#endregion

		#region Properties
		#endregion

		#region Constructors
		public ONRoleAttribute(string role, string domain, string roleInv, string domainInv)
		{
			Role = role;
			RoleInv = roleInv;
			Domain = domain;
			DomainInv = domainInv;
		}
		#endregion

		#region IsVisible
		public bool IsVisible(ONContext onContext)
		{
			char[] lDelimiter = {','};
			string[] lAgents = Visibility.Split(lDelimiter);

			for (int i = 0; i < lAgents.Length; i++)
			{
				if (onContext.LeafActiveAgentFacets.Contains(lAgents[i].Trim()))
					return true;
			}

			return false;
		}
		#endregion

		#region HasHorizontalVisibility
		public bool HasHorizontalVisibility(StringCollection activeAgentFacets)
		{
			char[] lDelimiter = { ',' };
			string[] lAgents = EmptyHorizontalVisibility.Split(lDelimiter);
		
			for (int i = 0; i < lAgents.Length; i++)
			{
				if (activeAgentFacets.Contains(lAgents[i].Trim()))
					return false;
			}
			
			lAgents = HorizontalVisibility.Split(lDelimiter);
			for (int i = 0; i < lAgents.Length; i++)
			{
				if (activeAgentFacets.Contains(lAgents[i].Trim()))
					return true;
			}
		
			return false;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Role>" + Role + "</Role>";
		}
		#endregion
	}
}

