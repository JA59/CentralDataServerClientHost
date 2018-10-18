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
using System.Collections.Generic;

namespace AutoChem.Core.Arrays
{
    /// <summary>
    /// ArrayUtility includes methods that are helpful when dealing with arrays.
    /// </summary>
    public class ArrayUtility
    {
        /// <summary>
        /// Creates a new array with the elements of the first array followed by the
        /// elements in the second array.  If both of the arrays are of the same type
        /// then the new array will also be of that type.
        /// </summary>
        /// <param name="array1">The first array</param>
        /// <param name="array2">The second array</param>
        /// <returns>The new array with the elements of the first array followed by the elements of the 
        /// second</returns>
        public static Array MergeArrays(Array array1, Array array2)
        {
            if (array1.Rank != 1 || array2.Rank != 1)
            {
                string parameter;
                if (array1.Rank != 1)
                {
                    parameter = "array1";
                }
                else
                {
                    parameter = "array2";
                }
                throw new ArgumentOutOfRangeException(parameter, "The arrays to be merged must be of rank equal to 1.");
            }

            Type type = typeof(object);
            Type array1Type = array1.GetType().GetElementType();
            if (array1Type == array2.GetType().GetElementType())
            {
                type = array1Type;
            }

            Array newArray = Array.CreateInstance(type, array1.Length + array2.Length);
            Array.Copy(array1, 0, newArray, 0, array1.Length);
            Array.Copy(array2, 0, newArray, array1.Length, array2.Length);

            return newArray;
        }

        /// <summary>
        /// Creates a new array with the elements of the first array followed by the
        /// elements in the second array.  
        /// </summary>
        /// <param name="array1">The first array</param>
        /// <param name="array2">The second array</param>
        /// <returns>The new array with the elements of the first array followed by the elements of the 
        /// second</returns>
        public static T[] MergeArrays<T>(T[] array1, T[] array2)
        {
            T[] newArray = new T[array1.Length + array2.Length];

            Array.Copy(array1, 0, newArray, 0, array1.Length);
            Array.Copy(array2, 0, newArray, array1.Length, array2.Length);

            return newArray;
        }

        /// <summary>
        /// Concatenates multiple arrays. If all of the arrays are of the same type,
        /// then the new array will also be of that type.
        /// </summary>
        /// <param name="arrays">List of arrays to be concatenated</param>
        /// <returns></returns>
        public static Array MergeArrays(params Array[] arrays)
        {
            int numArrays = arrays.GetUpperBound(0) + 1;

            if (numArrays == 0 || numArrays == 1)
            {
                throw new ArgumentException("must supply at least two arrays to be merged");
            }

            Array array = arrays[0];

            for (int i = 1; i < numArrays; i++)
            {
                array = MergeArrays(array, arrays[i]);
            }
            return array;
        }
        /// <summary>
        /// Merges two arrays of ordered integers into a single list of ordered integers.
        /// Duplicates are removed (integers that appear in both lists are only copied once 
        /// into the merged list.)
        /// </summary>
        /// <param name="ints1">First ordered array of integers.</param>
        /// <param name="ints2">Second ordered array of integers.</param>
        /// <returns>Merged ordered array of integers.</returns>
        public static int[] MergeOrderedInts(int[] ints1, int[] ints2)
        {
            if (ints1.Length == 0)
            {
                return ints2;
            }

            int int1Index = 0;
            int int2Index = 0;

            List<int> newInts = new List<int>();

            int lastInt = ints1[0];
            int1Index++;
            newInts.Add(lastInt);

            while (int1Index < ints1.Length || int2Index < ints2.Length)
            {
                if (!(int2Index < ints2.Length) ||
                    (int1Index < ints1.Length && ints1[int1Index] < ints2[int2Index]))
                {
                    if (ints1[int1Index] != lastInt)
                    {
                        lastInt = ints1[int1Index];
                        newInts.Add(lastInt);
                    }
                    int1Index++;
                }
                else
                {
                    if (ints2[int2Index] != lastInt)
                    {
                        lastInt = ints2[int2Index];
                        newInts.Add(lastInt);
                    }
                    int2Index++;
                }
            }

            return newInts.ToArray();
        }

        /// <summary>
        /// Converts an array of arrays into a rectangular array
        /// All of the sub-arrays must be the same length, otherwise you could not convert it to a recatngular array.
        /// </summary>
        public static TItem[,] ToRectangularArray<TItem>(TItem[][] input)
        {
            var rows = input.Length;

            if (rows == 0)
            {
                return new TItem[0, 0];
            }

            var itemsPerRow = input[0].Length;

            var rectangularArray = new TItem[rows, itemsPerRow];

            for (int i = 0; i < rows; i++)
            {
                var itemsInThisRow = input[i].Length;
                if (itemsInThisRow != itemsPerRow)
                {
                    throw new ArgumentException("Cannot convert to a rectangular array because the jagged array is jagged");
                }

                for (int j = 0; j < itemsPerRow; j++)
                {
                    rectangularArray[i, j] = input[i][j];
                }
            }

            return rectangularArray;
        }

        /// <summary>
        /// Converts a rectangular array into an array of arrays (aka jagged)
        /// </summary>
        public static TItem[][] ToJaggedArray<TItem>(TItem[,] input)
        {
            // Create a jagged array with the same number of rows as the first dimension of the supplied rectangular array.
            // The array for each row will be allocated individually in the loop.

            var upperIndex = input.GetUpperBound(0);
            TItem[][] jaggedArray = new TItem[upperIndex + 1][];

            var itemsPerRow = input.GetLength(1);

            for (int i = 0; i <= upperIndex; i++)
            {
                var row = new TItem[itemsPerRow];
                for (int j = 0; j < itemsPerRow; j++)
                {
                    row[j] = input[i, j];
                }

                jaggedArray[i] = row;
            }

            return jaggedArray;
        }
    }
}
