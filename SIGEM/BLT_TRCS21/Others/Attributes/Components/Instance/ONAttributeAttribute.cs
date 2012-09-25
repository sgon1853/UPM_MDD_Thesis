// 3.4.4.5

using System;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONAttributeAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	internal class ONAttributeAttribute: Attribute, IONAttribute
	{
		#region Attributes
		public string Name;
		public string Type;
		public string FieldName;
		public string Visibility = null;
		public bool IsLegacy = false;
		public bool IsOptimized = true;
		public string FacetOfField;
		#endregion

		#region Gets / Sets
		#endregion

		#region Constructors
		public ONAttributeAttribute(string name, string type)
		{
			Name = name;
			Type = type;
			Visibility = null;
		}
		#endregion

		#region IsVisible
		public bool IsVisible(ONContext onContext)
		{
			// Agent of all class
			if (Visibility == null)
				return true;

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

		#region ToString
		public override string ToString()
		{
			return "<Attribute>" + Name + "</Attribute>";
		}
		#endregion
	}
}

