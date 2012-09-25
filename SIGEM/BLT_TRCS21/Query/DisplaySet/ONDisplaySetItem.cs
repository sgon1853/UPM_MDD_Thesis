// 3.4.4.5
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Query
{
	public class ONDisplaySetItem
	{
		#region Members
		public string Path;
		public bool InMemory = false;
		public bool InData = false;
		public bool InLegacy = false;
		public bool HasHV = false;
		public VisibilityState Visibility = VisibilityState.NotChecked;

		#endregion Members

		#region Constructors
		public ONDisplaySetItem(string className, string path, StringCollection activeAgentFacets)
		{
			Path = path;

			Type lInstanceType = ONContext.GetType_Instance(className);
			if (!ONInstance.IsOptimized(lInstanceType, Path))
				InMemory = true;
			else
			{
				if (ONInstance.IsLocal(lInstanceType, Path))
					InData = true;

				if (ONInstance.IsLegacy(lInstanceType, Path))
					InLegacy = true;
			}

			if (Path.Contains("."))
				HasHV = ONInstance.HasHorizontalVisibility(lInstanceType, Path, activeAgentFacets);

		}
			
		public ONDisplaySetItem(ONDisplaySetItem lElement)
		{
			Path = lElement.Path;
			InMemory = lElement.InMemory;
			InData = lElement.InData;
			InLegacy = lElement.InLegacy;
			HasHV = lElement.HasHV;
		}

		public ONDisplaySetItem(string name)
		{
			Path = name;
			InData = true;
			Visibility = VisibilityState.Visible;
		}
		#endregion Constructos
	}
}
