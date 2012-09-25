// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Oids
{
	public interface IOid
	{
		string ClassName{ get; }
		IList<IOidField> Fields{get;}
		void ClearValues();
		void SetValues(IList<object> values);
		void SetValues(object[] values);
		void SetValue(int index, object value);

		object[] GetValues();
		IList<KeyValuePair<ModelType, object>> GetFields();
	}
}

