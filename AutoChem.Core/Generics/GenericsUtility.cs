/*
**
**COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**    Copyright © 2006 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoChem.Core.Generics
{
    /// <summary>
    /// Provides utility methods for dealing with generics.
    /// </summary>
    public static class GenericsUtility
    {
        /// <summary>
        /// Gets the values of a dictionary and returns them as an array of type TValue.
        /// </summary>
        public static TValue[] GetDictionaryValuesAsArray<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary)
        {
            return GetCollectionAsArray(dictionary.Values);
        }

        /// <summary>
        /// Gets the items in the collection as an array of type T.
        /// </summary>
        public static T[] GetCollectionAsArray<T>(ICollection<T> collection)
        {
            T[] values = new T[collection.Count];
            collection.CopyTo(values, 0);
            return values;
        }

        /// <summary>
        /// Gets the items in the collection as an array of type T.
        /// </summary>
        public static T[] GetCollectionAsArray<T>(ICollection collection)
        {
            T[] values = new T[collection.Count];
            collection.CopyTo(values, 0);
            return values;
        }

        /// <summary>
        /// Gets the dictionary value for the specified key and returns null if the 
        /// dictionary does not contain the value.
        /// </summary>
        public static TValue GetDictionaryValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : class
        {
            TValue value;
            bool gotValue;
            gotValue = dictionary.TryGetValue(key, out value);
            if (gotValue)
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Used to clone one dictionary to another.
        /// </summary>
        public static void CopyDictionary<TKey, TValue>(IDictionary<TKey, TValue> dictionaryToCopyFrom, 
                                                        IDictionary<TKey, TValue> dictionaryToCopyTo)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionaryToCopyFrom)
            {
                dictionaryToCopyTo.Add(pair);
            }
        }

        /// <summary>
        /// Provides a virtual filtered enumeration of the provided collection where only
        /// items of type T in the collection are enumerated through.  
        /// This also has the affect of filtering out any null values.
        /// </summary>
        public static IEnumerable<T> FilterByType<T>(IEnumerable collection) where T : class
        {
            foreach (object obj in collection)
            {
                T tObj = obj as T;
                if (tObj != null)
                {
                    yield return tObj;
                }
            }
        }

        /// <summary>
        /// Returns an dictionary where the keys in the dictionary have been created by the keyCreator method being
        /// called on the items in the collection and the values in the dictionary have been created by the valueCeator method
        /// being called on the items in the collection.
        /// </summary>
        public static Dictionary<TKey, TValue> GetIndexFromCollection<T, TKey, TValue>(IEnumerable<T> collection,
            Converter<T, TKey> keyCreator, Converter<T, TValue> valueCreator)
        {
            Dictionary<TKey, TValue> index = new Dictionary<TKey, TValue>();

            foreach (T item in collection)
            {
                index.Add(keyCreator(item), valueCreator(item));
            }

            return index;
        }

#if !SILVERLIGHT

        /// <summary>
        /// Returns true if the length of the lists are the same and the values are equal for each corresponding element.
        /// </summary>
        public static bool ListEquals<T>(this IReadOnlyList<T> list1, IReadOnlyList<T> list2) where T: IEquatable<T>
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            var listsEqual = Enumerable.Range(0, list1.Count).
                All(index => list1[index]?.Equals(list2[index]) ?? list2[index] == null);

            return listsEqual;
        }

        /// <summary>
        /// If the collection is already a IReadOnlyList the collection is returned.
        /// Otherwise the colleciton is coppied into a colleciton implementing IReadOnlyList.
        /// </summary>
        public static IReadOnlyList<T> AsList<T>(this IEnumerable<T> collection)
        {
            var list = collection as IReadOnlyList<T>;

            if (list == null && collection != null)
            {
                list = collection.ToArray();
            }

            return list;
        }

        /// <summary>
        /// Returns the items from the list in the range specified by the starting index and count.
        /// The items are returned in a new list.
        /// </summary>
        public static List<T> GetRange<T>(this IReadOnlyList<T> list, int startingIndex, int count)
        {
            var rangeList = new List<T>(count);
            foreach(var index in Enumerable.Range(startingIndex, count))
            {
                rangeList.Add(list[index]);
            }

            return rangeList;
        }
#endif

        /// <summary>
        /// This is a null Converter.  The original thinking is that this would be used with the GetIndexFromList method often 
        /// for the valueCreator mothod.  This method returns the object that is passed to it.
        /// </summary>
        public static T ConvertToSelf<T>(T self)
        {
            return self;
        }

        /// <summary>
        /// Checks if two values are equal using the objects equal method and handles the case that value 1 is null.
        /// </summary>
        public static bool EqualsWithNullCheck<T>(this T value1, T value2) where T : class
        {
            if (Object.ReferenceEquals(value1, null))
            {
                if (Object.ReferenceEquals(value2, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (value1.Equals(value2));
            }
        }

        /// <summary>
        /// Gets a copy of the original collection while locking on the provided lock.
        /// </summary>
        public static IEnumerable<T> GetCopyLocked<T>(this IEnumerable<T> originalCollection, object lockObject)
        {
            T[] copy;

            lock (lockObject)
            {
                copy = originalCollection.ToArray();
            }

            return copy;
        }

        /// <summary>
        /// Gets the transpose of a two-dimensional collection.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(
            this IEnumerable<IEnumerable<T>> source)
        {
            var enumerators = source.Select(e => e.GetEnumerator()).ToArray();
            try
            {
                while (enumerators.All(e => e.MoveNext()))
                {
                    yield return enumerators.Select(e => e.Current).ToArray();
                }
            }
            finally
            {
                Array.ForEach(enumerators, e => e.Dispose());
            }
        }
        /// <summary>
        /// If the input is not null then the value of the fucntion is returned otherwise default(TReturn) is returned
        /// </summary>
        public static TReturn ValueOrDefault<T, TReturn>(this T input, Func<T, TReturn> function) where T : class
        {
            if (input != null)
            {
                return function(input);
            }
            else
            {
                return default(TReturn);
            }
        }

        /// <summary>
        /// If the input is not null then the value of the fucntion is returned otherwise defaultValue is returned
        /// </summary>
        public static TReturn ValueOrDefault<T, TReturn>(this T input, Func<T, TReturn> function, TReturn defaultValue) where T : class
        {
            if (input != null)
            {
                return function(input);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns true if item 1 and item 2 are equal and handles item1 or item2 being null (or both).
        /// And returns the expected results
        /// both null returns true; one but not both null returns false; both not null returns item1.Equals(item2) based on the IEquatable&lt;T&gt;.Equals"/>"/>
        /// </summary>
        public static bool SafeEquals<T>(this T item1, T item2) where T : class, IEquatable<T>
        {
            bool item1Null = ReferenceEquals(item1, null);
            bool item2Null = ReferenceEquals(item2, null);
            if (item1Null && item2Null)
            {
                return true;
            }
            else if (item1Null || item2Null)
            {
                return false;
            }
            else
            {
                return item1.Equals(item2);
            }
        }

        /// <summary>
        /// Yields all of the values in values that match the condition on the predicate.
        /// The predicate is provided the value and the previous value.
        /// </summary>
        public static IEnumerable<T> WhereWithPrevious<T>(this IEnumerable<T> values, Func<T, T?, bool> predicate)
            where T : struct
        {
            T? previous = null;
            foreach (var value in values)
            {
                if (predicate(value, previous))
                {
                    yield return value;
                    previous = value;
                }
            }
        }

        /// <summary>
        /// Performs BinarySearch for object in list
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for this to work properly and should not have duplicate values (equal values)
        /// Note Array does have a BinarySearch but that it only works on arrays not IList
        /// </summary>
        public static int BinarySearch<T>(this IList<T> list, T objectToFind)
        {
            return BinarySearch(list, objectToFind, Comparer<T>.Default);
        }

        /// <summary>
        /// Performs BinarySearch for object in list
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for this to work properly and should not have duplicate values (equal values)
        /// Note Array does have a BinarySearch but that it only works on arrays not IList
        /// </summary>
        public static int BinarySearch<T>(this IList<T> list, T objectToFind, IComparer<T> comparer)
        {
            int count = list.Count;
            int upperBound = count - 1;
            int lowerBound = 0;
            int currentIndex;
            int comparisonResult;

            while (lowerBound <= upperBound)
            {
                currentIndex = (lowerBound + upperBound) / 2;
                comparisonResult = comparer.Compare(list[currentIndex], objectToFind);

                if (comparisonResult == 0)
                {
                    return currentIndex;
                }

                // if (list[currentIndex] > objectToFind)
                if (comparisonResult > 0)
                {
                    upperBound = currentIndex - 1;
                }
                else // else list[currentIndex] < objectToFind
                {
                    lowerBound = currentIndex + 1;
                }
            }

            // if Here we did not find it return complement of lowerBound which is index of first element larger than or 
            // 1 + last index if object to find is greater than all existing elements
            int complement = ~lowerBound;

            return complement;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Performs BinarySearch for object in IReadOnlyList.  Note that this has to be separate from the IList implementation because IList does not 
        /// derive from IReadOnlyList
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for this to work properly and should not have duplicate values (equal values)
        /// Note Array does have a BinarySearch but that it only works on arrays not IList
        /// </summary>
        public static int BinarySearch<T>(this IReadOnlyList<T> list, T objectToFind)
        {
            return BinarySearch(list, objectToFind, Comparer<T>.Default);
        }

        /// <summary>
        /// Performs BinarySearch for object in IReadOnlyList.  Note that this has to be separate from the IList implementation because IList does not 
        /// derive from IReadOnlyList
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for this to work properly and should not have duplicate values (equal values)
        /// Note Array does have a BinarySearch but that it only works on arrays not IList
        /// </summary>
        public static int BinarySearch<T>(this IReadOnlyList<T> list, T objectToFind, IComparer<T> comparer)
        {
            int count = list.Count;
            int upperBound = count - 1;
            int lowerBound = 0;
            int currentIndex;
            int comparisonResult;

            while (lowerBound <= upperBound)
            {
                currentIndex = (lowerBound + upperBound) / 2;
                comparisonResult = comparer.Compare(list[currentIndex], objectToFind);

                if (comparisonResult == 0)
                {
                    return currentIndex;
                }

                // if (list[currentIndex] > objectToFind)
                if (comparisonResult > 0)
                {
                    upperBound = currentIndex - 1;
                }
                else // else list[currentIndex] < objectToFind
                {
                    lowerBound = currentIndex + 1;
                }
            }

            // if Here we did not find it return complement of lowerBound which is index of first element larger than or 
            // 1 + last index if object to find is greater than all existing elements
            int complement = ~lowerBound;

            return complement;
        }
#endif
        /// <summary>
        /// Performs BinarySearch for object in list with a particular value.
        ///
        /// Returns the index of the specified value, if the value is found
        /// or a negative number if the item is not found.
        /// The negative number is the bitwise complement of the index of the first element that is larger than value.
        /// If the value is larger than all the values, then
        /// the bitwise complement of the index of the last element + 1 is returned.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for the property specified for this to work properly.
        /// If the list has duplicate entries the index returned among the duplicates is not deterministic.
        /// </summary>
        public static int ValueBinarySearch<TItem, TValue>(this IList<TItem> list, Func<TItem, TValue> valueAccessor, TValue valueToFind)
        {
            return ValueBinarySearch(list, valueAccessor, valueToFind, Comparer<TValue>.Default);
        }

        /// <summary>
        /// Performs BinarySearch for object in list with a particular value
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for the property specified for this to work properly.
        /// If the list has duplicate entries the index returned among the duplicates is not deterministic.
        /// </summary>
        public static int ValueBinarySearch<TItem, TValue>(this IList<TItem> list, Func<TItem, TValue> valueAccessor, TValue valueToFind, IComparer<TValue> comparer)
        {
            int count = list.Count;
            int upperBound = count - 1;
            int lowerBound = 0;

            var isAccendingList = IsAccendingList(list, valueAccessor, comparer);

            while (lowerBound <= upperBound)
            {
                var currentIndex = (lowerBound + upperBound) / 2;
                var comparisonResult = comparer.Compare(valueAccessor(list[currentIndex]), valueToFind);

                if (comparisonResult == 0)
                {
                    return currentIndex;
                }

                if (isAccendingList)
                {
                    if (comparisonResult > 0)
                    {
                        upperBound = currentIndex - 1;
                    }
                    else
                    {
                        lowerBound = currentIndex + 1;
                    }
                }
                else
                {
                    if (comparisonResult > 0)
                    {
                        lowerBound = currentIndex + 1;
                    }
                    else{
                        upperBound = currentIndex - 1;
                    }
                }
            }

            // if Here we did not find it return complement of lowerBound which is index of first element larger than or 
            // 1 + last index if object to find is greater than all existing elements
            int complement = ~lowerBound;

            return complement;
        }

        /// <summary>
        /// Performs BinarySearch for object in list with a particular value
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for the property specified for this to work properly.
        /// If the list has duplicate entries the index returned among the duplicates is not deterministic.
        /// </summary>
        public static int ValueBinarySearch<TItem, TValue>(this IList<TItem> list, Func<TItem, TValue, int> comparer, TValue valueToFind)
        {
            int count = list.Count;
            int upperBound = count - 1;
            int lowerBound = 0;
            int currentIndex;
            int comparisonResult;

            while (lowerBound <= upperBound)
            {
                currentIndex = (lowerBound + upperBound) / 2;
                comparisonResult = comparer(list[currentIndex], valueToFind);

                if (comparisonResult == 0)
                {
                    return currentIndex;
                }

                // if (list[currentIndex] > valueToFind)
                if (comparisonResult > 0)
                {
                    upperBound = currentIndex - 1;
                }
                else // else list[currentIndex] < valueToFind
                {
                    lowerBound = currentIndex + 1;
                }
            }

            // if Here we did not find it return complement of lowerBound which is index of first element larger than or 
            // 1 + last index if object to find is greater than all existing elements
            int complement = ~lowerBound;

            return complement;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Performs BinarySearch for object in list with a particular value
        /// Note ReadOnly is included in the method name to avoid conflicts where objects implement both IList and IReadOnlyList.
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for the property specified for this to work properly.
        /// If the list has duplicate entries the index returned among the duplicates is not deterministic.
        /// </summary>
        public static int ValueBinarySearchReadOnly<TItem, TValue>(this IReadOnlyList<TItem> list, Func<TItem, TValue, int> comparer, TValue valueToFind)
        {
            int count = list.Count;
            int upperBound = count - 1;
            int lowerBound = 0;
            int currentIndex;
            int comparisonResult;

            while (lowerBound <= upperBound)
            {
                currentIndex = (lowerBound + upperBound) / 2;
                comparisonResult = comparer(list[currentIndex], valueToFind);

                if (comparisonResult == 0)
                {
                    return currentIndex;
                }

                // if (list[currentIndex] > valueToFind)
                if (comparisonResult > 0)
                {
                    upperBound = currentIndex - 1;
                }
                else // else list[currentIndex] < valueToFind
                {
                    lowerBound = currentIndex + 1;
                }
            }

            // if Here we did not find it return complement of lowerBound which is index of first element larger than or 
            // 1 + last index if object to find is greater than all existing elements
            int complement = ~lowerBound;

            return complement;
        }

        /// <summary>
        /// Performs BinarySearch for object in list with a particular value.
        ///
        /// Returns the index of the specified value, if the value is found
        /// or a negative number if the item is not found.
        /// The negative number is the bitwise complement of the index of the first element that is larger than value.
        /// If the value is larger than all the values, then
        /// the bitwise complement of the index of the last element + 1 is returned.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for the property specified for this to work properly.
        /// If the list has duplicate entries the index returned among the duplicates is not deterministic.
        /// </summary>
        public static int ValueBinarySearchReadOnly<TItem, TValue>(this IReadOnlyList<TItem> list, Func<TItem, TValue> valueAccessor, TValue valueToFind)
        {
            return ValueBinarySearchReadOnly(list, valueAccessor, valueToFind, Comparer<TValue>.Default);
        }

        /// <summary>
        /// Performs BinarySearch for object in list with a particular value
        ///
        /// Returns the index of the specified value, if value is found.
        /// or a negative number if the item is not found
        /// which is the bitwise complement of the index of the first element that is larger than value, if value is not found and value is less than one or more elements in the list.
        /// of the bitwise complement of (the index of the last element + 1), if value is not found and value is greater than any of the elements in array.
        ///
        /// Note the bitwise complement is used as oppose to the inverse if the number so that
        /// zero can be represented as 0xFFFFFFFF (inverse of zero is zero)
        ///
        /// Note the list must be in sorted order for the property specified for this to work properly.
        /// If the list has duplicate entries the index returned among the duplicates is not deterministic.
        /// </summary>
        public static int ValueBinarySearchReadOnly<TItem, TValue>(this IReadOnlyList<TItem> list, Func<TItem, TValue> valueAccessor, TValue valueToFind, IComparer<TValue> comparer)
        {
            Func<TItem, TValue, int> itemValueComparer = (item, value) =>
            {
                var itemValue = valueAccessor(item);
                return comparer.Compare(itemValue, value);
            };
            return ValueBinarySearchReadOnly(list, itemValueComparer, valueToFind);
        }

#endif

        /// <summary>
        /// Updates an existing collection of items based on the latest list of items.
        /// This is useful when you want the list to to be stable for existing items.
        /// That is you don't want a new collection and you want to keep the existing items in the list.
        /// </summary>
        /// <typeparam name="TValue">The type of the item in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key used for checking if items are in a list</typeparam>
        /// <param name="existingCollection">The existing collection of items.</param>
        /// <param name="latestValues">The latest values to update the collection with</param>
        /// <param name="keyGenerator">A function to generate a key with from a value</param>
        /// <param name="updateExistingValue">A method to update existing records from new records</param>
        /// <param name="keyComparer">The comparer used for comparing keys</param>
        /// <returns>True if a record was added or removed from the collection</returns>
        public static bool UpdateList<TValue, TKey>(IList<TValue> existingCollection, IEnumerable<TValue> latestValues,
            Func<TValue, TKey> keyGenerator, Action<TValue, TValue> updateExistingValue = null,
            IEqualityComparer<TKey> keyComparer = null)
        {
            return UpdateList(existingCollection, latestValues, keyGenerator, false, updateExistingValue, keyComparer);
        }

        /// <summary>
        /// Updates an existing collection of items based on the latest list of items.
        /// This is useful when you want the list to to be stable for existing items.
        /// That is you don't want a new collection and you want to keep the existing items in the list.
        /// </summary>
        /// <typeparam name="TValue">The type of the item in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key used for checking if items are in a list</typeparam>
        /// <param name="existingCollection">The existing collection of items.</param>
        /// <param name="latestValues">The latest values to update the collection with</param>
        /// <param name="keyGenerator">A function to generate a key with from a value</param>
        /// <param name="checkForMovedItems">If true, the order of the items will be checked in case an item may 
        /// have been moved in the latestValues with respect to items in the list both prior to and after the update.</param>
        /// <param name="updateExistingValue">A method to update existing records from new records</param>
        /// <param name="keyComparer">The comparer used for comparing keys</param>
        /// <returns>True if a record was added or removed from the collection</returns>
        public static bool UpdateList<TValue, TKey>(IList<TValue> existingCollection, IEnumerable<TValue> latestValues,
        Func<TValue, TKey> keyGenerator, bool checkForMovedItems, Action<TValue, TValue> updateExistingValue = null, 
        IEqualityComparer<TKey> keyComparer = null)
        {
            if (keyGenerator == null)
            {
                throw new ArgumentNullException("keyGenerator", "A key generator needs to be supplied.");
            }

            if (existingCollection == null)
            {
                //nothing to update
                return false;
            }

            if (latestValues == null)
            {
                //new collection is null so clear the existing
                bool retVal = existingCollection.Any();
                existingCollection.Clear();
                return retVal;  // true if at least one item existed prior to the clear
            }

            var latestValuesArray = latestValues.ToArray();

            if (latestValuesArray.Any(item => item == null))
            {
                bool retVal = existingCollection.Any();
                existingCollection.Clear();
                return retVal;  // true if at least one item existed prior to the clear
            }

            bool recordAddedOrDeleted = false;
            //use a dictionary to make this efficient
            var existingValuesByKey = existingCollection.ToDictionary(keyGenerator, keyComparer);
            var newValuesByKey = latestValuesArray.ToDictionary(keyGenerator, keyComparer);

            var itemsToAdd =
                from itemWithIndex in
                    latestValuesArray.Select((item, index) => new { Value = item, Index = index })
                where !existingValuesByKey.ContainsKey(keyGenerator(itemWithIndex.Value))
                select itemWithIndex;

            var itemsToDelete =
                (from itemWithIndex in
                    existingCollection.Select((item, index) => new { Value = item, Index = index })
                where !newValuesByKey.ContainsKey(keyGenerator(itemWithIndex.Value))
                select itemWithIndex).ToArray(); // Since we are using and will be modifying existingCollection we need a copy.

            // Note we don't want this to be a copy because we want it to be up to date each time we use it.
            var existingItemsKeyAndIndex = existingCollection.Select((item, index) => new { Key = keyGenerator(item), Index = index });

            var itemsToUpdate =
                from keyWithIndex in existingItemsKeyAndIndex
                where newValuesByKey.ContainsKey(keyWithIndex.Key)
                select keyWithIndex;

            // Do updates first because the indexes for update are based on the current index
            // in existingCollection
            if (updateExistingValue != null)
            {
                foreach (var keyWithIndex in itemsToUpdate)
                {
                    updateExistingValue(existingCollection[keyWithIndex.Index], newValuesByKey[keyWithIndex.Key]);
                }
            }

            // Go through the items backwards and remove them by index.
            // Going in reverse maintains the index for items not yet removed
            // and causes less items to be moved overall.
            foreach (var itemToDelete in itemsToDelete.Reverse())
            {
                existingCollection.RemoveAt(itemToDelete.Index);
                recordAddedOrDeleted = true; // record was deleted
            }

            // Do add last because its indexes are based on the order of the 
            // items in the new list.
            foreach (var itemWithIndex in itemsToAdd)
            {
                existingCollection.Insert(itemWithIndex.Index, itemWithIndex.Value);
                recordAddedOrDeleted = true; // record was added
            }

            if (checkForMovedItems)
            {
                // If requested check for items that have moved to a different location.
                Action<int, int> moveItem = GetCollectionMoveMethod(existingCollection);
                keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;

                for (int index = 0; index < latestValuesArray.Length; index++)
                {
                    var item = latestValuesArray[index];
                    var existingItemAtIndex = existingCollection[index];

                    var itemKey = keyGenerator(item);
                    var existingItemKey = keyGenerator(existingItemAtIndex);

                    var sameItem = keyComparer.Equals(itemKey, existingItemKey);

                    if (!sameItem)
                    {
                        // Find the index. Note that determining these ahead of time
                        // is not straight forward because the current index changes
                        // with each move of an item. Therefore, we just determine the 
                        // the index for each item that needs to be moved independently.
                        var oldIndex = (from keyAndIndex in existingItemsKeyAndIndex
                                        where keyComparer.Equals(keyAndIndex.Key, itemKey)
                                        select keyAndIndex.Index).First();

                        moveItem(oldIndex, index);
                    }
                }
            }

            return recordAddedOrDeleted;
        }

        private static Action<int, int> GetCollectionMoveMethod<TValue>(IList<TValue> existingCollection)
        {
            Action<int, int> moveItem;

            var observableCollection = existingCollection as ObservableCollection<TValue>;

            if (observableCollection != null)
            {
                moveItem = observableCollection.Move;
            }
            else
            {
                moveItem = (oldIndex, newIndex) =>
                {
                    var item = existingCollection[oldIndex];
                    existingCollection.RemoveAt(oldIndex);
                    existingCollection.Insert(newIndex, item);
                };
            }

            return moveItem;
        }


        /// <summary>
        /// Gets the additions and removals from the existing collection.
        /// </summary>
        /// <typeparam name="TValue">The type of the item in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key used for checking if items are in a list</typeparam>
        /// <param name="existingCollection">The existing collection of items.</param>
        /// <param name="latestValues">The latest values to update the collection with</param>
        /// <param name="keyGenerator">A function to generate a key with from a value</param>
        /// <param name="itemsAdded">The items added to the latestValues</param>
        /// <param name="itemsRemoved">The Items removed in the latestValues</param>
        /// <param name="keyComparer">The comparer used for comparing keys</param>
        public static void GetCollectionUpdates<TValue, TKey>(IEnumerable<TValue> existingCollection, IEnumerable<TValue> latestValues,
            Func<TValue, TKey> keyGenerator, out IEnumerable<TValue> itemsAdded, out IEnumerable<TValue> itemsRemoved, 
            IEqualityComparer<TKey> keyComparer = null)
        {
            var comparer = new KeyComparer<TValue, TKey>(keyGenerator, keyComparer);

            itemsAdded = latestValues.Except(existingCollection, comparer).ToArray();

            itemsRemoved = existingCollection.Except(latestValues, comparer).ToArray();
        }

        private static bool IsAccendingList<TItem, TValue>(IList<TItem> list, Func<TItem, TValue> valueAccessor, IComparer<TValue> comparer)
        {
            if (list.Count == 0)
            {
                return true;
            }

            var comparisonResult = comparer.Compare(valueAccessor(list[0]), valueAccessor(list[list.Count - 1]));
            return comparisonResult != 1;
        }

        private class KeyComparer<TValue, TKey> : IEqualityComparer<TValue>
        {
            private Func<TValue, TKey> m_KeyGenerator;
            private IEqualityComparer<TKey> m_KeyComparer;

            public KeyComparer(Func<TValue, TKey> keyGenerator, IEqualityComparer<TKey> keyComparer)
            {
                m_KeyGenerator = keyGenerator;
                if (keyComparer != null)
                {
                    m_KeyComparer = keyComparer;
                }
                else
                {
                    m_KeyComparer = EqualityComparer<TKey>.Default;
                }
            }

            public bool Equals(TValue x, TValue y)
            {
                bool xNull = ReferenceEquals(x, null);
                bool yNull = ReferenceEquals(y, null);

                if (xNull)
                {
                    return xNull == yNull;
                }
                else
                {
                    var xKey = m_KeyGenerator(x);
                    var yKey = m_KeyGenerator(y);

                    return m_KeyComparer.Equals(xKey, yKey);
                }
            }

            public int GetHashCode(TValue obj)
            {
                bool objNull = ReferenceEquals(obj, null);

                if (objNull)
                {
                    return 0;
                }
                else
                {
                    var objKey = m_KeyGenerator(obj);
                    return m_KeyComparer.GetHashCode(objKey);
                }
            }
        }
    }
}
