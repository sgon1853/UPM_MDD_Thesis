// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Client.Adaptor;

namespace SIGEM.Client.Oids
{
	public interface IOidField
	{
		string Name { get; set;}
		ModelType Type { get; }
		object Value { get;set;}
		int MaxLength { get;set;}
	}
}

