// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Business
{
	/// <summary>
	/// Custom enumerator for ONListDictionary. It enumerates ONListDictionaryEntry items
	/// </summary>
	/// <typeparam name="TKey">Type of the collection item identificators. It is used for optimizing searchs</typeparam>
	/// <typeparam name="TValue">Type of the collection items</typeparam>
	public struct ONListDictionaryEnumerator<TKey, TValue> : IEnumerator<ONListDictionaryEntry<TKey, TValue>>
	{
		#region Members
		/// <summary>
		/// Current index
		/// </summary>
		private int mCurrentIndex;
		/// <summary>
		/// Distionary with entries indexed by index
		/// </summary>
		private IDictionary<int, ONListDictionaryEntry<TKey, TValue>> mItemsByIndex;
		/// <summary>
		/// Maximum index
		/// </summary>
		private int mMaxIndex;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="itemsByIndex">Dictionary with the entries indexed by index</param>
		/// <param name="maxIndex">Last index to iterate</param>
		public ONListDictionaryEnumerator(IDictionary<int, ONListDictionaryEntry<TKey, TValue>> itemsByIndex, int maxIndex)
		{
			mItemsByIndex = itemsByIndex;
			mCurrentIndex = -1;
			mMaxIndex = maxIndex;
		}
		#endregion Constructors

		#region IEnumerator<ONListDictionaryEntry<TKey, TValue>> Interface
		/// <summary>
		/// Returns the current entry
		/// </summary>
		public ONListDictionaryEntry<TKey, TValue> Current
		{
			get
			{
				return mItemsByIndex[mCurrentIndex];
			}
		}
		#endregion IEnumerator<ONListDictionaryEntry<TKey, TValue>> Interface

		#region IEnumerator Interface
		/// <summary>
		/// Resets the enumerator
		/// </summary>
		public void Reset()
		{
			mCurrentIndex = -1;
		}
		/// <summary>
		/// Returns the current entry (it is hidden)
		/// </summary>
		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}
		/// <summary>
		/// Moves to next entry
		/// </summary>
		/// <returns>true if another item has been reached, false if the end of the list is hit.</returns>
		public bool MoveNext()
		{
			while (mCurrentIndex <= mMaxIndex)
			{
				mCurrentIndex++;
				if (mItemsByIndex.ContainsKey(mCurrentIndex))
					return true;
			}

			return false;
		}
		#endregion IEnumerator Interface

		#region Dispose
		/// <summary>
		/// Dispose the enumerator
		/// </summary>
		public void Dispose()
		{
		}
		#endregion Dispose
	}
}
