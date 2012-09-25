// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	public enum ServiceTypeEnumeration
	{
		Normal = 1,
		New,
		Destroy,
		// Relation
		Insertion,
		Deletion,
		// Inheritance
		Carrier,
		Liberator
	}
}

