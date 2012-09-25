// 3.4.4.5

using System;
using System.Xml;
using System.Data;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.Types
{
    /// <summary>
    /// Basic Type ONBlob
    /// </summary>
    internal class ONBlob : ONSimpleType
    {
		#region Properties
		/// <summary>
		/// Represents the typed value of the object
		/// </summary>
		public byte[] TypedValue
		{
			get
			{
				return Value as byte[];
			}
			set
			{
				Value = value;
			}
		}
		/// <summary>
		/// Represents the Null value of this type.
		/// </summary>
		public new static ONBlob Null
		{
			get
			{
				ONBlob lNull = new ONBlob();
				lNull.Value = null;
				return lNull;
			}
		}

        /// <summary>
		/// Returns the type in SQL of this type of data.
		/// </summary>
		public override SqlDbType SQLType
		{
			get
			{
				return SqlDbType.Image;
			}
		}
		/// <summary>
		/// Represents the default value of this type.
		/// </summary>
		public static ONBlob DefaultValue
		{
			get
			{
				return new ONBlob(new byte[0]);
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONBlob()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONBlob(byte[] val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONBlob(ONBlob val)
		{
			if (val == null)
				Value = null;
			else
				Value = val.Value;
		}
		#endregion

		#region Operator (== !=)
		public override bool Equals(object obj)
		{
			ONBlob lObj2 = obj as ONBlob;

			if ((Value == null) && (((object) lObj2 == null) || (lObj2.Value == null)))
				return true;

			if ((Value == null) || (((object) lObj2 == null) || (lObj2.Value == null)))
				return false;

			byte[] lValue = (byte[]) Value;
			byte[] lValue2 = (byte[]) lObj2.Value;

			if (lValue.Length != lValue2.Length)
				return false;
            for (int lIndex = 0; lIndex < lValue.Length; lIndex++)
			{
				if (lValue[lIndex] != lValue2[lIndex])
					return false;
			}

			return true;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public static ONBool operator==(ONBlob obj1, ONBlob obj2)
		{
			if ((object) obj1 == null && (object) obj2 == null)
				return new ONBool(true);

			if (((object) obj1 == null || obj1.Value == null) &&
				((object) obj2 == null || obj2.Value == null))
				return new ONBool(true);

			if ((object) obj1 == null || (object) obj2 == null)
				return new ONBool(false);

			return new ONBool(false);
		}
		public static ONBool operator!=(ONBlob obj1, ONBlob obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region IComparable methods
		public override int CompareTo(object obj)
		{
			return 0;
		}
		#endregion
    }
}

