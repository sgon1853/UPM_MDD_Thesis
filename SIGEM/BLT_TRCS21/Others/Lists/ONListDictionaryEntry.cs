// 3.4.4.5

using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Business
{
	/// <summary>
	/// Struct for storing the key-value pair with an index
	/// </summary>
	/// <typeparam name="TKey">Type of the collection item identificators. It is used for optimizing searchs</typeparam>
	/// <typeparam name="TValue">Type of the collection items</typeparam>
	public class ONListDictionaryEntry<TKey, TValue>
	{
		#region Members
		/// <summary>
		/// Index of the item in the dictionary
		/// </summary>
		private int mIndex;
		/// <summary>
		/// Item
		/// </summary>
		private TValue mValue;
		/// <summary>
		/// Key of the item
		/// </summary>
		private TKey mKey;
		#endregion Members

		#region Properties
		/// <summary>
		/// Item
		/// </summary>
		public TValue Value
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
		/// <summary>
		/// Index of the item in the dictionary
		/// </summary>
		public int Index
		{
			get
			{
				return mIndex;
			}
			set
			{
				mIndex = value;
			}
		}
		/// <summary>
		/// Key of the item
		/// </summary>
		public TKey Key
		{
			get
			{
				return mKey;
			}
			set
			{
				mKey = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="index"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public ONListDictionaryEntry(int index, TKey key, TValue value)
		{
			mIndex = index;
			mValue = value;
			mKey = key;
		}
		#endregion Constructors
	}
}
