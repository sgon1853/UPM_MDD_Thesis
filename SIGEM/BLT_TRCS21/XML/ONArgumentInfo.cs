// 3.4.4.5

using System;
using SIGEM.Business.Types;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Summary description for ONArgumentInfo.
	/// </summary>
	public class ONArgumentInfo
	{
		private string mName;
		private bool mNull;
		private DataTypeEnumerator mType;
		private int mMaxLength;
		private string mClassName;
		private IONType mValue;
		private string mIdArgument;
		private string mAlias;

		#region Properties
		public IONType Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}

		public string Name
		{
			get
			{
				return mName;
			}
		}
		public bool Null
		{
			get
			{
				return mNull;
			}
		}
		public DataTypeEnumerator Type
		{
			get
			{
				return mType;
			}
		}
		public int MaxLength
		{
			get
			{
				return mMaxLength;
			}
		}
		public string ClassName
		{
			get
			{
				return mClassName;
			}
		}
		public string IdArgument
		{
			get
			{
				return mIdArgument;
			}
		}
		public string Alias
		{
			get
			{
				return mAlias;
			}
		}

		#endregion

		public ONArgumentInfo(string name, bool Null, DataTypeEnumerator type, int maxLength, string className, string idArgument, string alias)
		{
			mName = name;
			mNull = Null;
			mType = type;
			mMaxLength = maxLength;
			mClassName = className;
			mIdArgument = idArgument;
			mAlias = alias;
		}

	}
}

