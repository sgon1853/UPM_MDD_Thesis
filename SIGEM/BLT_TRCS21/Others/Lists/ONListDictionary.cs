// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Business
{
	/// <summary>
	/// Dictionary that returns the instance in insertion order
	/// </summary>
	/// <typeparam name="TKey">Type of the collection item identificators. It is used for optimizing searchs</typeparam>
	/// <typeparam name="TValue">Type of the collection items</typeparam>
	public class ONListDictionary<TKey, TValue> : IEnumerable<ONListDictionaryEntry<TKey, TValue>>, IDictionary<TKey, TValue>
	{
		#region Members
		/// <summary>
		/// Last index assigned
		/// </summary>
		private int mMaxIndex;
		/// <summary>
		/// Hash of instances indexed by the key
		/// </summary>
		private Dictionary<TKey, ONListDictionaryEntry<TKey, TValue>> mItemsByKey;
		/// <summary>
		/// Hash of instances indexed by the index
		/// </summary>
		private Dictionary<int, ONListDictionaryEntry<TKey, TValue>> mItemsByIndex;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		public ONListDictionary()
		{
			mItemsByKey = new Dictionary<TKey, ONListDictionaryEntry<TKey, TValue>>();
			mItemsByIndex = new Dictionary<int, ONListDictionaryEntry<TKey, TValue>>();
			mMaxIndex = -1;
		}
		#endregion Constructors

		#region IEnumerable<ONListDictionaryEntry<TKey, TValue>>
		/// <summary>
		/// Returns the enumerator in insertion order
		/// </summary>
		/// <returns>Enumerator in insertion order</returns>
		IEnumerator<ONListDictionaryEntry<TKey, TValue>> IEnumerable<ONListDictionaryEntry<TKey, TValue>>.GetEnumerator()
		{
			return (new ONListDictionaryEnumerator<TKey, TValue>(mItemsByIndex, mMaxIndex));
		}
		#endregion IEnumerable<ONListDictionaryEntry<TKey, TValue>>

		#region IDictionary<TKey, TValue> Interface
		/// <summary>
		/// Returns the value by key
		/// </summary>
		public TValue this[TKey key]
		{
			get
			{
				ONListDictionaryEntry<TKey, TValue> lEntry = mItemsByKey[key];
				if (lEntry == null)
					return default(TValue);
				else
					return lEntry.Value;
			}
			set
			{
				ONListDictionaryEntry<TKey, TValue> lEntry = mItemsByKey[key];
				if (lEntry == null)
					Add(key, value);
				else
					lEntry.Value = value;
			}
		}
		/// <summary>
		/// Returns a collection with the keys in insertion order
		/// </summary>
		public ICollection<TKey> Keys
		{
			get
			{
				List<TKey> lResult = new List<TKey>(Count);
				foreach (ONListDictionaryEntry<TKey, TValue> lEntry in this)
					lResult.Add(lEntry.Key);

				return lResult;
			}
		}
		/// <summary>
		/// Returns a collection with the values in insertion order
		/// </summary>
		public ICollection<TValue> Values
		{
			get
			{
				List<TValue> lResult = new List<TValue>(Count);
				foreach (ONListDictionaryEntry<TKey, TValue> lEntry in this)
					lResult.Add(lEntry.Value);

				return lResult;
			}
		}
		/// <summary>
		/// Adds an object to the dictionary
		/// </summary>
		/// <param name="key">Key of the object</param>
		/// <param name="value">Object to add</param>
		public void Add(TKey key, TValue value)
		{
			mMaxIndex++;

			ONListDictionaryEntry<TKey, TValue> lEntry = new ONListDictionaryEntry<TKey, TValue>(mMaxIndex, key, value);

			mItemsByKey.Add(lEntry.Key, lEntry);
			mItemsByIndex.Add(lEntry.Index, lEntry);
		}
		/// <summary>
		/// Returns if the Dictionary contains the specified key
		/// </summary>
		/// <param name="key">Key of object to search</param>
		/// <returns>If the key is in the dictionary</returns>
		public bool ContainsKey(TKey key)
		{
			return mItemsByKey.ContainsKey(key);
		}
		/// <summary>
		/// Remove an item by its key of the distionary
		/// </summary>
		/// <param name="key">Key of item to remove</param>
		public bool Remove(TKey key)
		{
			// Remove the object from the hashtable, and also remove the corresponding item from the
			// key list... adjust all the indexes appropriately
			if (mItemsByKey.ContainsKey(key))
			{
				mItemsByIndex.Remove(mItemsByKey[key].Index);
				mItemsByKey.Remove(key);

				return true;
			}

			return false;
		}
		/// <summary>
		/// Returns value of a key
		/// </summary>
		/// <param name="key">Key of the object</param>
		/// <param name="value">Object to return</param>
		/// <returns>If it is contained</returns>
		public bool TryGetValue (TKey key, out TValue value)
		{
			value = this[key];

			return ((object) value != null);
		}
		#endregion IDictionary<TKey, TValue> Interface

		#region ICollection<KeyValuePair<TKey, TValue>> Interface
		/// <summary>
		/// Adds an object to the dictionary
		/// </summary>
		/// <param name="pair">Key-Value pair of the object</param>
		public void Add(KeyValuePair<TKey, TValue> pair)
		{
			Add(pair.Key, pair.Value);
		}
		/// <summary>
		/// Clears the dictionary
		/// </summary>
		public void Clear()
		{
			mItemsByIndex.Clear();
			mItemsByKey.Clear();
			mMaxIndex = -1;
		}
		/// <summary>
		/// Returns if the Dictionary contains the specified key
		/// </summary>
		/// <param name="pair">Key-Value pair of the object to search</param>
		/// <returns>If the key is in the dictionary</returns>
		public bool Contains(KeyValuePair<TKey, TValue> pair)
		{
			return ContainsKey(pair.Key);
		}
		/// <summary>
		/// Removes an item by its key
		/// </summary>
		/// <param name="pair">Key-Value pair to remove</param>
		public bool Remove(KeyValuePair<TKey, TValue> pair)
		{
			return Remove(pair.Key);
		}
		/// <summary>
		/// Checks if the dictionary is read only
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}
		/// <summary>
		/// Returns the number of items in the dictionary
		/// </summary>
		public int Count
		{
			get
			{
				return mItemsByKey.Count;
			}
		}
		/// <summary>
		/// Copies the Key-Value pairs in insertion order to an array.
		/// </summary>
		/// <param name="array">Array to insert the items</param>
		/// <param name="index">Index of the first item to insert</param>
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			int i = 0;
			foreach (ONListDictionaryEntry<TKey, TValue> lEntry in this)
				if (i >= index)
					array[i++] = new KeyValuePair<TKey, TValue>(lEntry.Key, lEntry.Value);
				else
					i++;
		}
		/// <summary>
		/// Returns an enumerator with Key-Value pairs with insertion order (it is hidden)
		/// </summary>
		/// <returns>LargeListDictionaryEnumerator</returns>
		IEnumerator<KeyValuePair<TKey, TValue>> System.Collections.Generic.IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			List<KeyValuePair<TKey, TValue>> lList = new List<KeyValuePair<TKey, TValue>>();

			foreach (ONListDictionaryEntry<TKey, TValue> lEntry in this)
				lList.Add(new KeyValuePair<TKey, TValue>(lEntry.Key, lEntry.Value));

			return lList.GetEnumerator(); ;
		}
		#endregion ICollection<KeyValuePair<TKey, TValue>> Interface

		#region IEnumerable<KeyValuePair<TKey, TValue> Interface
		/// <summary>
		/// Returns an enumerator with Key-Value pairs with insertion order (it is hidden)
		/// </summary>
		/// <returns>Hidden enumerator</returns>
		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (new ONListDictionaryEnumerator<TKey, TValue> (mItemsByIndex, mMaxIndex));
		}
		#endregion IEnumerable<KeyValuePair<TKey, TValue> Interface

		#region Other methods
		/// <summary>
		/// Returns an enumerator with insertion order
		/// </summary>
		/// <returns>Enumerator of a ONListDictionary</returns>
		public ONListDictionaryEnumerator<TKey, TValue> GetEnumerator()
		{
			return (new ONListDictionaryEnumerator<TKey, TValue>(mItemsByIndex, mMaxIndex));
		}
		/// <summary>
		/// Unions 2 dictionaries
		/// </summary>
		/// <param name="list1">First dictionary where the values have to be added from</param>
		/// <param name="list2">Second dictionary where the values have to be added from</param>
		/// <returns>The union list</returns>
		public ONListDictionary<TKey, TValue> Union(ONListDictionary<TKey, TValue> list1, ONListDictionary<TKey, TValue> list2)
		{
			ONListDictionary<TKey, TValue> lList = new ONListDictionary<TKey, TValue>();

			lList.Union(list1);
			lList.Union(list2);

			return lList;
		}
		/// <summary>
		/// Unions the dictionary with other dictionary
		/// </summary>
		/// <param name="list">List where the values have to be added from</param>
		public void Union(ONListDictionary<TKey, TValue> list)
		{
			if (list == null)
				return;

			if (Count == 0)
				foreach (ONListDictionaryEntry<TKey, TValue> lEntry in list)
					Add(lEntry.Key, lEntry.Value);
			else
				foreach (ONListDictionaryEntry<TKey, TValue> lEntry in list)
					if (!ContainsKey(lEntry.Key))
						Add(lEntry.Key, lEntry.Value);
		}
		/// <summary>
		/// Intersect two lists
		/// </summary>
		/// <param name="list1">First dictionary of instances to intersect</param>
		/// <param name="list2">Second dictionary of instances to intersect</param>
		public static ONListDictionary<TKey, TValue> Intersection(ONListDictionary<TKey, TValue> list1, ONListDictionary<TKey, TValue> list2)
		{
			ONListDictionary<TKey, TValue> lList = new ONListDictionary<TKey, TValue>();

			if ((list1 == null) || (list2 == null))
				return lList;

			foreach (ONListDictionaryEntry<TKey, TValue> lEntry in list1)
				if (list2.ContainsKey(lEntry.Key))
					lList.Add(lEntry.Key, lEntry.Value);

			return lList;
		}
		/// <summary>
		/// Intersects the dictionary with other dictionary
		/// </summary>
		/// <param name="list">Dictionary to intersect</param>
		public void Intersection(ONListDictionary<TKey, TValue> list)
		{
			if (list == null)
			{
				Clear();
				return;
			}

			int mMaxIndex = -1;
			foreach (ONListDictionaryEntry<TKey, TValue> lEntry in this)
			{
				if (!list.ContainsKey(lEntry.Key))
				{
					Remove(lEntry.Key);
				}
				else
				{
					mMaxIndex++;
					lEntry.Index = mMaxIndex;
				}
			}
		}
		/// <summary>
		/// Checks if the dictionary is equals to other dictionary
		/// </summary>
		/// <param name="item">Dictionary to check</param>
		/// <returns>If the dictionaries are equals</returns>
		public override bool Equals(object item)
		{
			ONListDictionary<TKey, TValue> lList = item as ONListDictionary<TKey, TValue>;

			if ((lList == null) || (Count != lList.Count))
				return false;

			ONListDictionaryEnumerator<TKey, TValue> lEnum1 = GetEnumerator();
			ONListDictionaryEnumerator<TKey, TValue> lEnum2 = lList.GetEnumerator();

			while (lEnum1.MoveNext())
			{
				lEnum2.MoveNext();

				if (!lEnum1.Current.Value.Equals(lEnum2.Current.Value))
					return false;
			}

			return true;
		}
		/// <summary>
		/// Hashs of the dictionary
		/// </summary>
		/// <returns>Hash value of the dictionary</returns>
		public override int GetHashCode()
		{
			return Count;
		}
		#endregion Other methods
	}
}
