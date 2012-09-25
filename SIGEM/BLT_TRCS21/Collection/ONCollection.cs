// 3.4.4.5

using System;
using System.Reflection;
using System.Collections;
using SIGEM.Business.Attributes;
using SIGEM.Business.Query;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Data;

namespace SIGEM.Business.Collection
{
    /// <summary>
    /// Superclass of Collections
    /// </summary>
    
    internal abstract class ONCollection : ONList<ONOid, ONInstance>, IDisposable
    {
        #region Members
        public ONContext OnContext;
        public string ClassName;
        public bool IsLegacyView;
        public int totalNumInstances = -1;
        public ONInstance[] Array
        {
            get
            {
                ONInstance[] lReturn = new ONInstance[Count];
                int lCount = 0;

                foreach (ONInstance lObject in this)
                {
                    lReturn[lCount] = lObject;
                    lCount++;
                }

                return lReturn;
            }
            set
            {
                foreach (ONInstance lObject in value)
                    Add(lObject);

            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="onContext">Context with all the information about the execution of the request</param>
        /// <param name="className">Name of the class that represents the instance</param>
        public ONCollection(ONContext onContext, string className, bool isLegacyView)
        {
            if (onContext != null)
                OnContext = onContext;
            else
                OnContext = new ONContext();
            ClassName = className;
            IsLegacyView = isLegacyView;
        }
        #endregion

        #region IDisposable Methods
        /// <summary>
        /// Destroy all the resources that are associated with the object
        /// </summary>
        public void Dispose()
        {
            if (OnContext != null)
            {
                OnContext.Dispose();
                OnContext = null;
            }
        }
        #endregion

        #region Operators
        public static ONCollection operator +(ONCollection collection, ONInstance instance)
        {
            ONCollection lCollection = ONContext.GetComponent_Collection(collection.ClassName, collection.OnContext);

            lCollection.Union(collection);
            lCollection.Add(instance);

            return lCollection;
        }
        public static ONCollection operator +(ONCollection collection1, ONCollection collection2)
        {
			return Union(collection1, collection2);
        }
        public static ONCollection Union(ONCollection collection1, ONCollection collection2)
        {
			ONCollection lCollection = collection1.Clone() as ONCollection;

			lCollection.Union(collection2);

			return lCollection;
        }
        public static ONCollection Intersection(ONCollection collection1, ONCollection collection2)
        {
            ONCollection lCollection = ONContext.GetComponent_Collection(collection1.ClassName, collection1.OnContext);

            if ((collection1 == null) || (collection2 == null))
                return lCollection;

            foreach (ONInstance lInstance in collection1)
                if (!collection2.Contains(lInstance))
                    lCollection.Add(lInstance);

            return lCollection;
        }
        #endregion

        #region Indexer
        public object this[string onPath]
        {
            get
            {
                return this[new ONPath(onPath)];
            }
        }
        public object this[ONPath onPath]
        {
            get
            {
                if ((onPath == null) || (onPath.Count == 0))
                    return this;

                string lRol = onPath.RemoveHead();
                PropertyInfo lProperty = null;

                // Last unique role (like attributes)
                if (onPath.Count == 0)
                {
                    lProperty = ONContext.GetPropertyInfoWithAttribute(GetType(), typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>");
                    if (lProperty != null)
                        return (lProperty.GetValue(this, null));
                }

                // Roles
                lProperty = ONContext.GetPropertyInfoWithAttribute(GetType(), typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>");
                if (lProperty != null)
                {
                    if (onPath.Count == 0)
                        return (lProperty.GetValue(this, null));
                    else
                        return (lProperty.GetValue(this, null) as ONCollection)[onPath];
                }

                return null;
            }
        }
		public override ONInstance this[int index]
		{
			get
			{
				if (List.Count <= index)
					return null;

				ONListDictionaryEnumerator<ONOid, ONInstance> lEnumerator = List.GetEnumerator();
				for (int i = -1; i < index; i++)
					lEnumerator.MoveNext();

				return lEnumerator.Current.Value;
			}
			set
			{
				if ((List.Count <= index) || (value == null))
					return;

				ONListDictionaryEnumerator<ONOid, ONInstance> lEnumerator = List.GetEnumerator();
				for (int i = -1; i < index; i++)
					lEnumerator.MoveNext();

				lEnumerator.Current.Key = value.Oid;
				lEnumerator.Current.Value = value;
			}
		}
		#endregion Indexer

		#region IndexOf
		public int IndexOf(ONOid oid)
        {
			List.ContainsKey(oid);

            int i = 0;
            foreach (ONInstance lInstance in this)
                if (lInstance.Oid == oid)
                    return i;
                else
                    i++;

            return -1;
        }
        #endregion IndexOf
		
        #region Sort
        /// <summary>
        /// Sorts the instances of the collection atending to a comparer
        /// </summary>
        /// <param name="comparer">Element needed to compare the instances of the collection</param>
        public void Sort(IComparer comparer)
        {
            ONOrderCriteria lComparer = comparer as ONOrderCriteria;

            // Only SQL sort
            if ((lComparer == null) || !lComparer.InMemory)
                return;

            // Initialize Temp variables
            ONInstance[] lA = Array;
            long[] lP = new long[Count];
            for (long i = 0; i < Count; i++)
                lP[i] = i;
            long[] lQ = new long[Count];

            // Initialize ONComparer
            if ((lComparer == null) || !lComparer.InDataIni)
                Sort(comparer as ONOrderCriteria, 0, Count - 1, lA, lP, lQ);
            else
            {
                // Sort if there's SQL optimization
                long lIni = 0;
                long lEnd = 0;
                ONInstance lInstanceIni = null;
                foreach (ONInstance lInstanceEnd in this)
                {
                    if (lComparer.CompareSql(lInstanceEnd, lInstanceIni) != 0)
                    {
                        if (lIni < lEnd - 1)
                            Sort(lComparer, lIni, lEnd - 1, lA, lP, lQ);

                        lIni = lEnd;
                        lInstanceIni = lInstanceEnd;
                    }

                    lEnd++;
                }

                if (lIni < lEnd - 1)
                    Sort(lComparer, lIni, lEnd - 1, lA, lP, lQ);
            }

            // Update the list
            Clear();
            for (long i = 0; i < lA.Length; i++)
                Add(lA[lP[i]]);
        }
        /// <summary>
        /// Sorts the instances of the collection atending to a comparer
        /// </summary>
        /// <param name="comparer">Element needed to compare the instances of the collection</param>
        /// <param name="ini">Initial instance to start the sort of the instances</param>
        /// <param name="end">Final innstace to delimit the sort of the instances</param>
        /// <param name="A"></param>
        /// <param name="P"></param>
        /// <param name="Q"></param>
        public void Sort(ONOrderCriteria comparer, long ini, long end, ONInstance[] A, long[] P, long[] Q)
        {
            ShuttleMergeSort(comparer, ini, end, A, P, Q);
        }
        #endregion Sort

		#region Add Ordered
		/// <summary>
		/// Adds elements to the collection from other collection in the order that the Order Criteria sets.
		/// </summary>
		/// <param name="list">Collection to add</param>
		/// <param name="comparer">Order criteria to apply</param>
		/// <param name="onContext">context of the query</param>
        public void AddRangeOrdered(ONCollection list, ONOrderCriteria comparer, ONContext onContext)
		{
			// Empty lists
			if (list.Count == 0)
				return;
			if (Count == 0)
			{
				AddRange(list);
				return;
			}

			int i = 0;
			int j = 0;
			int lComparation = 0;

			// Clone and clear collection
			ONCollection lList = Clone() as ONCollection;
			this.Clear();

			// Create data component for default comparation
			ONData lData = null;
			if (comparer == null)
				lData = ONContext.GetComponent_Data(ClassName, onContext);

			ONInstance lInstance1 = lList.Array[i];
			ONInstance lInstance2 = list.Array[j];
			while ((i < lList.Count) && (j < list.Count))
			{
				if (comparer != null)
					lComparation = comparer.CompareUnion(lInstance1, lInstance2);
				else
					lComparation = lData.CompareUnionOID(lInstance1, lInstance2);

				if (lComparation < 0)
				{
					Add(lInstance1);
					i += 1;
					if (lList.Count > i)
						lInstance1 = lList.Array[i];
				}
				else if (lComparation > 0)
				{
					Add(lInstance2);
					j += 1;
					if (list.Count > j)
						lInstance2 = list.Array[j];
				}
				else
				{
					Add(lInstance1);
					Add(lInstance2);
					i += 1;
					j += 1;
					if (lList.Count > i)
						lInstance1 = lList.Array[i];
					if (list.Count > j)
						lInstance2 = list.Array[j];
				}
			}

			AddRange(lList);
			AddRange(list);
		}
		/// <summary>
		/// Adds an instance to the collection in the order that the Order Criteria sets.
		/// </summary>
		/// <param name="instance">Instance to add</param>
		/// <param name="comparer">Order criteria to apply</param>
		/// <param name="onContext">context of the query</param>
        public void AddOrdered(ONInstance instance, ONOrderCriteria comparer, ONContext onContext)
		{
			// Empty lists
			if (Count == 0)
			{
				Add(instance);
				return;
			}

			int i = 0;
			int j = 0;
			int lComparation = 0;

			// Clone and clear collection
			ONCollection lList = Clone() as ONCollection;
			this.Clear();

			// Create data component for default comparation
			ONData lData = null;
			if (comparer == null)
				lData = ONContext.GetComponent_Data(ClassName, onContext);

			ONInstance lInstance1 = lList.Array[i];
			while ((i < lList.Count) && (j < 1))
			{
				if (comparer != null)
					lComparation = comparer.CompareUnion(lInstance1, instance);
				else
					lComparation = lData.CompareUnionOID(lInstance1, instance);

				if (lComparation < 0)
				{
					Add(lInstance1);
					i += 1;
					if (lList.Count > i)
						lInstance1 = lList.Array[i];
				}
				else if (lComparation > 0)
				{
					Add(instance);
					j += 1;
				}
				else
				{
					Add(lInstance1);
					Add(instance);
					i += 1;
					j += 1;
				}
			}

			AddRange(lList);
			if (j == 0)
				Add(instance);
		}
		#endregion Add Ordered

		#region Contains
		/*/// <summary>
		/// Returns if exist an instance with the Oid
		/// </summary>
		/// <param name="oid">oid to search</param>
		/// <returns></returns>
		public bool Contains(ONOid oid)
		{
			return List.ContainsKey(oid);
		}*/
		/// <summary>
		/// Returns if exist an instance with the Oid
		/// </summary>
		/// <param name="oid">oid to search</param>
		/// <returns></returns>
		public override bool Contains(ONInstance instance)
		{
			return Contains(instance.Oid);
		}
		#endregion Contains

		#region Add
		/// <summary>
		/// Add an instance to the collection only if the collection does not already contains this instance.
		/// </summary>
		/// <param name="instance"></param>
		public override void Add(ONInstance instance)
		{
			Add(instance.Oid, instance);
		}
		#endregion Add

		#region Remove
		/// <summary>
		/// Remove an instance with the Oid
		/// </summary>
		/// <param name="oid">oid of the instance to remove</param>
		public override bool Remove(ONInstance instance)
		{
			return Remove(instance.Oid);
		}
		#endregion Remove

		#region Clone
		public override object Clone()
		{
			ONCollection lCollection = ONContext.GetComponent_Collection(ClassName, OnContext);
			lCollection.Union(this);

			return lCollection;
		}

		#endregion Clone

		#region InsertionSort
		// InsertionSort.  A simple routine with minimal overhead.  Should never be used 
        // to sort long lists because of its O(N^2) behavior,
        // but is the method of choice for sorting short (5-50 key) lists or long lists 
        // that have been mostly sorted by a faster algorithm.  InsertionSort is faster 
        // than either Bubble or SelectionSort and should be used anywhere you would 
        // consider using those.  Sorts in place (no extra memory needed) and is stable 
        // (preserves the original order of records with equal keys).  Works by creating 
        // a sorted list at the beginning of the array of keys.  As each unsorted key to 
        // the right is examined, it is compared back thru the sorted list until the 
        // right position to insert it is found.  Two versions are given.  pInsertS is 
        // an indirect (pointerized) version for strings,
        // which can be adapted to doubles by changing the declaration of A().  InsertL 
        // is a direct version for longs, which can be adapted to integers.
        // 
        // Speed:  Abysmally slow for anything but short lists.
        //
        // Bottom line:  should be used only to finish up for faster sorts with higher 
        // overhead; for that purpose, this is the sort to choose.

        public void InsertionSort(ONOrderCriteria comparer, long l, long r)
        {
            long[] lP = new long[r + 1];
            for (int i = 0; i < r; i++)
                lP[i] = i;
            InsertionSort(comparer, l, r, Array, lP);
        }
        private static void InsertionSort(ONOrderCriteria comparer, long l, long r, ONInstance[] a, long[] p)
        {
            long lLp;
            long lRp;
            long lTmp;
            ONInstance lT;

            //RP points to the first unsorted key.
            for (lRp = l + 1; lRp <= r; lRp++)
            {
                //Get the new value.
                lTmp = p[lRp];
                lT = a[lTmp];

                //Compare it back thru the sorted part as long as it's bigger.
                for (lLp = lRp; lLp <= l + 1; lLp--)
                {
                    if (comparer.Compare(lT, a[p[lLp - 1]]) < 0)
                        p[lLp] = p[lLp - 1];
                    else
                        break;
                }

                //It's bigger than all keys to the left, so insert it here.
                p[lLp] = lTmp;
            }
        }
        #endregion

        #region ShuttleMergeSort
        // 2/4/03.  The previous version of ShuttleMergeSort failed on very short lists. 
        // The code below corrects the problem and eliminates a couple of unnecessary 
        // variables.  Sorting times for one million random longs,
        // double or strings are 67, 90 and 95 seconds (Excel 2001 / 800 mhz PowerBook /
        // MacOS 10.2.3).

        // 1/7/03.  Here is a 20-25% faster version of MergeSort.  The old version 
        // merged into an auxiliary array, and copied the result back to the primary 
        // array at the end of each pass.  This version plans ahead for an even number 
        // of passes, and alternates direction each time, first merging to the auxiliary 
        // array and then back to the primary array.  It also replaces recursive calls 
        // with an explicit stack, and calls to a separate InsertionSort with in-line 
        // code.  Because of the back and forth merging,
        // I call this version "ShuttleMergeSort".

        // Another frequently proposed optimization for MergeSort is to set runs up in 
        // alternating directions (low to high, then high to low).  This allows 
        // replacing separate boundary tests for LP and RP with a single test for LP 
        // crossing RP.  I tried this, and it wasn//t faster in practice.  Probably the 
        // gain from fewer loop tests was offset by time spent in extra comparisons; in 
        // the simpler version, when one run is used up, the rest of the other run is 
        // copied to the output array with no further comparisons.  Also,
        // the run-alternating version was significantly slower on presorted inputs,
        // which often occur in practice.

        // QuickSort is still faster for strings (64 sec vs. 95),
        // but MergeSort is faster for doubles (90 sec vs. 162) and longs (67 sec vs. 
        // 116).  Given that MergeSort is stable and guaranteed NlogN,
        // while QuickSort is unstable and always has an N^2 worst case,
        // MergeSort is my choice for a single all-purpose sort.

        // The first example below is a pointerized version for strings.  It can be 
        // adapted to doubles by changing the declaration of A() and T.  The second 
        // example is a direct version for longs that can be adapted to integers.

        public void ShuttleMergeSort(ONOrderCriteria comparer, long l, long r)
        {
            long[] lP = new long[r + 1];
            for (long i = l; i <= r; i++)
                lP[i] = i;
            long[] lQ = new long[r + 1];
            ONInstance[] lA = Array;

            // Sort
            ShuttleMergeSort(comparer, l, r, lA, lP, lQ);

            // Update the list
            Clear();
            for (long i = l; i <= r; i++)
                Add(lA[lP[i]]);
        }
        private static void ShuttleMergeSort(ONOrderCriteria comparer, long lO, long hI, ONInstance[] a, long[] p, long[] q)
        {
            //lO and hI point to first and last keys; a() is the buffer of string keys.
            //p() and q() are buffers of pointers to the keys in a()
            double lLength;    //length of initial runs to be made by InsertionSort
            long lNRuns;        //the number of runs at each stage
            long[] lStack;        //bookkeeping stack for merge passes
            long lI;
            long lL;            //left limit
            long lR;            //right limit
            long lLp;            //left pointer
            long lRp;            //right pointer
            long lOp;            //other pointer
            ONInstance lTmp;
            long lLongTmp;
            bool lForward;    //toggle for direction of alternate merge passes

            //Calculate how many merge passes will be needed.
            //Each back & forth pair of merges will convert 4N sublists into N.
            lLength = 1 + hI - lO;
            lNRuns = 1;
            while (lLength > 20)
            {
                lLength = lLength / 4;
                lNRuns = lNRuns * 4;
            }

            //Set up stack to keep track of sublists being merged.
            lStack = new long[lNRuns];
            for (lI = 0; lI < lNRuns - 1; lI++)
                lStack[lI] = lO + Convert.ToInt64(lLength * (lI + 1));
            lStack[lNRuns - 1] = hI;

            //Build short runs using low overhead InsertionSort.
            lL = lO;
            for (lI = 0; lI < lNRuns; lI++)
            {
                lR = lStack[lI];
                for (lRp = lL + 1; lRp <= lR; lRp++)
                {
                    lOp = p[lRp];
                    lTmp = a[lOp];
                    for (lLp = lRp; lLp >= lL + 1; lLp--)
                    {
                        if (comparer.Compare(lTmp, a[p[lLp - 1]]) < 0)
                            p[lLp] = p[lLp - 1];
                        else
                            break;
                    }
                    p[lLp] = lOp;
                }
                lL = lR + 1;
            }

            //Make back & forth passes of MergeSort until all runs are merged.
            lForward = true;
            while (lNRuns > 1)
            {
                lR = lO - 1;
                if (lForward)
                {
                    //Half the passes are forward, merging from p() into q().
                    for (lI = 1; lI < lNRuns; lI += 2)
                    {
                        lLp = lR + 1;
                        lOp = lLp;
                        lL = lStack[lI - 1];
                        lRp = lL + 1;
                        lR = lStack[lI];
                        do
                        {
                            if (comparer.Compare(a[p[lLp]], a[p[lRp]]) <= 0)
                            {
                                q[lOp] = p[lLp];
                                lOp = lOp + 1;
                                lLp = lLp + 1;
                                if (lLp > lL)
                                {
                                    do
                                    {
                                        q[lOp] = p[lRp];
                                        lOp = lOp + 1;
                                        lRp = lRp + 1;
                                    } while (!(lRp > lR));
                                    break;
                                }
                            }
                            else
                            {
                                q[lOp] = p[lRp];
                                lOp = lOp + 1;
                                lRp = lRp + 1;
                                if (lRp > lR)
                                {
                                    do
                                    {
                                        q[lOp] = p[lLp];
                                        lOp = lOp + 1;
                                        lLp = lLp + 1;
                                    } while (!(lLp > lL));
                                    break;
                                }
                            }
                        } while (true);
                        lStack[Math.DivRem(lI + 1, 2, out lLongTmp) - 1] = lR;
                    }
                }
                else
                {
                    //Half the passes are backward, merging from q() into p().
                    for (lI = 1; lI < lNRuns; lI += 2)
                    {
                        lLp = lR + 1;
                        lOp = lLp;
                        lL = lStack[lI - 1];
                        lRp = lL + 1;
                        lR = lStack[lI];
                        do
                        {
                            if (comparer.Compare(a[q[lLp]], a[q[lRp]]) <= 0)
                            {
                                p[lOp] = q[lLp];
                                lOp = lOp + 1;
                                lLp = lLp + 1;
                                if (lLp > lL)
                                {
                                    do
                                    {
                                        p[lOp] = q[lRp];
                                        lOp = lOp + 1;
                                        lRp = lRp + 1;
                                    } while (!(lRp > lR));
                                    break;
                                }
                            }
                            else
                            {
                                p[lOp] = q[lRp];
                                lOp = lOp + 1;
                                lRp = lRp + 1;
                                if (lRp > lR)
                                {
                                    do
                                    {
                                        p[lOp] = q[lLp];
                                        lOp = lOp + 1;
                                        lLp = lLp + 1;
                                    } while (!(lLp > lL));
                                    break;
                                }
                            }
                        } while (true);
                        lStack[Math.DivRem(lI + 1, 2, out lLongTmp) - 1] = lR;
                    }
                }

                //After each merge, we have half as many runs and we switch direction.
                lNRuns = Math.DivRem(lNRuns, 2, out lLongTmp);
                lForward = !lForward;
            }
        }
        #endregion
    }
}

