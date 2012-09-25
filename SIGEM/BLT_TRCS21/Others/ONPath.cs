// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;

namespace SIGEM.Business
{
	/// <summary>
	/// Class that represents role path of the model.
	/// </summary>
	public class ONPath
	{
		#region Members
		public List<string> Roles;
		#endregion

		#region Properties
		/// <summary>
		/// Number of  roles that has the path
		/// </summary>
		public int Count
		{
			get
			{
				return Roles.Count;
			}
		}
		/// <summary>
		/// Role path
		/// </summary>
		public string Path
		{
			get
			{
				string lPath = "";
				foreach (string lRole in Roles)
				{
					if (lPath != "")
						lPath += ".";

					lPath += lRole;
				}

				return lPath;
			}
			set
			{
				if (value == "")
					Roles = new List<string>();
				else
				{
					char[] lDelimiter = {'.'};
					Roles = new List<string>(value.Split(lDelimiter));
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="roles">Role path to be treated</param>
		public ONPath(List<string> roles)
		{
			Roles = new List<string>(roles);
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="onPath">Role path to be treated</param>
		public ONPath(ONPath onPath)
		{
			if ((onPath as object) == null)
				Roles = new List<string>();
			else
				Roles = new List<string>(onPath.Roles);
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="roles">Role path to be treated</param>
		public ONPath(string[] roles)
		{
			Roles = new List<string>(roles);
		}
		public ONPath(string path)
		{
			Path = path;
		}
		#endregion

		#region Functions
		/// <summary>
		/// Removes the first role of the path
		/// </summary>
		public string RemoveHead()
		{
			object lObj = Roles[0];
			Roles.RemoveAt(0);
			return (lObj as string);
		}
		/// <summary>
		/// Removes the last role of the path
		/// </summary>
		public string RemoveTail()
		{
			object lObj = Roles[Roles.Count - 1];
			Roles.RemoveAt(Roles.Count - 1);
			return (lObj as string);
		}
		#endregion

		#region Operator (== !=)
		public override bool Equals(object obj)
		{
			if (obj is ONPath)
				return (string.Compare(Path, (obj as ONPath).Path, true) == 0);
			//return Path.Equals((obj as ONPath).Path);

			if (obj is string)
				return (string.Compare(Path, obj as string, true) == 0);
			//return Path.Equals(obj);

			return false;
		}
		public override int GetHashCode()
		{
			return Path.ToLower().GetHashCode();
		}
		public static bool operator==(ONPath obj1, ONPath obj2)
		{
			if ((((object) obj1 == null) || (obj1.Roles == null)) && 
				(((object) obj2 == null) || (obj2.Roles == null)))
				return true;

			if (((object) obj1 == null) || (obj1.Roles == null))
				return false;

			if (((object) obj2 == null) || (obj2.Roles == null))
				return false;

			return (string.Compare(obj1.Path, obj2.Path, true) == 0);
		}
		public static bool operator!=(ONPath obj1, ONPath obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region Operator (+)
		public static ONPath operator+(ONPath obj1, string obj2)
		{
			ONPath lPath = new ONPath(obj1);

			char[] lDelimiter = {'.'};
			string[] lRoles = obj2.Split(lDelimiter);
			foreach (string lRol in lRoles)
				lPath.Roles.Add(lRol);

			return lPath;
		}
		public static ONPath operator+(ONPath obj1, ONPath obj2)
		{
			ONPath lPath = new ONPath(obj1);
			lPath.Roles.AddRange(obj2.Roles);

			return lPath;
		}
		#endregion
	}
}

