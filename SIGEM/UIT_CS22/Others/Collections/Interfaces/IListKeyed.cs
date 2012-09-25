// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client
{
	/// <summary>
	/// List with key access.
	/// </summary>
	public interface IListKeyed<T> :
		IList<T>
		where T: class
	{
		T this[string name] { get; }

		bool Exist(string name);
	}

}
