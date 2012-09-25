// 3.4.4.5

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using SIGEM.Business.Query;
using SIGEM.Business.Collection;
using SIGEM.Business.Instance;
using SIGEM.Business.Data;

namespace SIGEM.Business
{
	/// <summary>
	/// List of elements
	/// </summary>
	/// <typeparam name="TKey">Type of the collection item identificators. It is used for optimizing searchs</typeparam>
	/// <typeparam name="TValue">Type of the collection items</typeparam>
	public abstract class ONList<TKey, TValue> : IEnumerable<TValue>, ICollection<TValue>, ICloneable
	{
		#region Members
		/// <summary>
		/// Dictionary that mantains the insertion order
		/// </summary>
		private ONListDictionary<TKey, TValue> mList;
		#endregion Members

		#region Properties
		/// <summary>
		/// Dictionary that mantains the insertion order
		/// </summary>
		public ONListDictionary<TKey, TValue> List
		{
			get
			{
				return mList;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructor of the list
		/// </summary>
		public ONList()
		{
			mList = new ONListDictionary<TKey, TValue>();
		}
		#endregion Constructors

		#region IEnumerable<TValue> Interface
		/// <summary>
		/// Method that returns the values in insertion order
		/// </summary>
		/// <returns>Values in insertion order</returns>
		public IEnumerator<TValue> GetEnumerator()
		{
			return List.Values.GetEnumerator();
		}
		#endregion IEnumerable<TValue> Interface

		#region IEnumerable Interface
		/// <summary>
		/// Method that returns the values in insertion order
		/// </summary>
		/// <returns>Values in insertion order</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return List.Values.GetEnumerator();
		}
		#endregion IEnumerable Interface

		#region ICollection<TValue> Interface
		/// <summary>
		/// Returns the number of elements in the list
		/// </summary>
		public int Count
		{
			get
			{
				return List.Count;
			}
		}
		/// <summary>
		/// Returns if the list is read only
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return List.IsReadOnly;
			}
		}
		/// <summary>
		/// Add an element to the list
		/// </summary>
		/// <param name="item">element to add</param>
		public abstract void Add(TValue item);
		/// <summary>
		/// Removes all the elements of the list
		/// </summary>
		public void Clear()
		{
			List.Clear();
		}
		/// <summary>
		/// Checks if an element is in the list
		/// </summary>
		/// <param name="item">Element to search</param>
		/// <returns>If the element is in the list</returns>
		public abstract bool Contains(TValue item);
		/// <summary>
		/// Copies the elements of the list to an array in the insertion order starting in a particular index
		/// </summary>
		/// <param name="array">List to copy</param>
		/// <param name="arrayIndex">Start index</param>
		public void CopyTo(TValue[] array, int arrayIndex)
		{
			int i = 0;
			foreach (ONListDictionaryEntry<TKey, TValue> lEntry in List)
			{
				if (i >= arrayIndex)
					array[i - arrayIndex] = lEntry.Value;

				i++;
			}
		}
		/// <summary>
		/// Removes an element of the list
		/// </summary>
		/// <param name="item">element to remove</param>
		/// <returns>If it was removed</returns>
		public abstract bool Remove(TValue item);
		#endregion ICollection<TValue> Interface

		#region IClonable UInterface
		/// <summary>
		/// Clones a List
		/// </summary>
		/// <returns>Cloned list</returns>
		public abstract object Clone();
		#endregion IClonable UInterface

		#region Other Methods
		/// <summary>
		/// List Indexer
		/// </summary>
		/// <param name="index">Index to search</param>
		/// <returns>Instance that is in the index position</returns>
		public abstract TValue this[int index] { get; set; }
		/// <summary>
		/// Adds elements to the list from other list
		/// </summary>
		/// <param name="list">List where elements have to be added from</param>
		public void AddRange(ONList<TKey, TValue> list)
		{
			List.Union(list.List);
		}
		/// <summary>
		/// Intersects the list with other list of elements
		/// </summary>
		/// <param name="list">List to intersect</param>
		public void Intersection(ONList<TKey, TValue> list)
		{
			ONListDictionary<TKey, TValue> lList = List;
			mList = new ONListDictionary<TKey, TValue>();

			if (list == null)
				return;

			foreach (ONListDictionaryEntry<TKey, TValue> lEntry in lList)
				if (list.Contains(lEntry.Value))
					Add(lEntry.Value);
		}
		/// <summary>
		/// Unions the list with other list of elements
		/// </summary>
		/// <param name="list">List to union</param>
		public void Union(ONList<TKey, TValue> list)
		{
			if (list != null)
				List.Union(list.List);
		}
		/// <summary>
		/// Adds an instance to the list
		/// </summary>
		/// <param name="key">Identificator of the instance to add</param>
		/// <param name="item">Instance to add</param>
		/// <returns>if the instance has been added</returns>
		public bool Add(TKey key, TValue item)
		{
			if (!Contains(key))
			{
				List.Add(key, item);
				return true;
			}

			return false;
		}
		/// <summary>
		/// Removes an instance from the list
		/// </summary>
		/// <param name="key">Identificator of the instance to remove</param>
		/// <returns>If the instance has been removed</returns>
		public bool Remove(TKey key)
		{
			return List.Remove(key);
		}
		/// <summary>
		/// Checks if an instance is in the list
		/// </summary>
		/// <param name="key">Identificator of the instance to search</param>
		/// <returns>If the instance is contained</returns>
		public bool Contains(TKey key)
		{
			return List.ContainsKey(key);
		}
		/// <summary>
		/// Checks if the list is equal to other list
		/// </summary>
		/// <param name="item">List to check if they are the same</param>
		/// <returns>If the list is equal to other list</returns>
		public override bool Equals(object item)
		{
			ONList<TKey, TValue> lList = item as ONList<TKey, TValue>;

			if (lList == null)
				return false;

			return List.Equals(lList.List);
		}
		/// <summary>
		/// Hashs of the list
		/// </summary>
		/// <returns>Hash value of the list</returns>
		public override int GetHashCode()
		{
			return List.Count;
		}
		#endregion Other Methods
	}
}

