// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONSharedArgumentAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	internal class ONSharedArgumentAttribute: Attribute, IONAttribute
	{
		#region Attributes
		private string mClassName;
		private int mOrder;
		public bool IsThis;
		#endregion

		#region Gets / Sets
		public string ClassName
		{
			get
			{
				return mClassName;
			}
		}
		public int Order
		{
			get
			{
				return mOrder;
			}
		}
		#endregion

		#region Constructors
		public ONSharedArgumentAttribute(string className, int order)
		{
			mClassName = className;
			mOrder = order;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Argument>" + ClassName + "</Argument>";
		}
		#endregion
	}
}

