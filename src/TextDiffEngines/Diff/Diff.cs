//
// Copyright (C) 2009  Thomas Bluemel <thomasb@reactsoft.com>
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace textdiffcore.TextDiffEngine
{
    internal static class Diff
    {
        #region Public static methods
        /// <summary>
        /// Creates a list of diff entries that represent the differences between arr1 and arr2.
        /// </summary>
        /// <typeparam name="T">Class that represents the unit to compare</typeparam>
        /// <param name="arr1">Array of units.</param>
        /// <param name="arr2">Array of units.</param>
        /// <param name="skipStart">Number of units to skip at the beginning.</param>
        /// <param name="skipEnd">Number of units to skip at the end.</param>
        /// <returns>List of DiffEntry classes.</returns>
        public static List<DiffEntry<T>> CreateDiff<T>(T[] arr1, T[] arr2, int skipStart, int skipEnd) where T : IComparable
        {
            int start = skipStart;
            int end = skipEnd;

            // Strip off the beginning and end, if it's equal
            while (start < Math.Min(arr1.Length, arr2.Length) - skipEnd)
            {
                if (arr1[start].CompareTo(arr2[start]) != 0)
                    break;
                start++;
            }

            if (start == arr1.Length - skipEnd && start == arr2.Length - skipEnd)
                return new List<DiffEntry<T>>();

            for (int i = 0; i < Math.Min(arr1.Length, arr2.Length) - start - skipEnd; i++)
            {
                if (arr1[arr1.Length - i - skipEnd - 1].CompareTo(arr2[arr2.Length - i - skipEnd - 1]) != 0)
                    break;
                end++;
            }

            int lines1_cnt = arr1.Length - start - end;
            int lines2_cnt = arr2.Length - start - end;

            int[,] lcs = new int[lines1_cnt, lines2_cnt];

            // Calculate longest common sequence
            for (int i = 0; i < lines1_cnt; i++)
            {
                for (int j = 0; j < lines2_cnt; j++)
                {
                    int iVal = i + start;
                    int jVal = j + start;

                    if (arr1[iVal].CompareTo(arr2[jVal]) != 0)
                    {
                        if (i == 0 && j == 0)
                            lcs[i, j] = 0;
                        else if (i == 0 && j != 0)
                            lcs[i, j] = Math.Max(0, lcs[i, j - 1]);
                        else if (i != 0 && j == 0)
                            lcs[i, j] = Math.Max(lcs[i - 1, j], 0);
                        else // if (i != 0 && j != 0)
                            lcs[i, j] = Math.Max(lcs[i - 1, j], lcs[i, j - 1]);
                    }
                    else
                    {
                        if (i == 0 || j == 0)
                            lcs[i, j] = 1;
                        else
                            lcs[i, j] = 1 + lcs[i - 1, j - 1];
                    }
                }
            }

            // Build the list of differences
            Stack<int[]> stck = new Stack<int[]>();
            List<DiffEntry<T>> diffList = new List<DiffEntry<T>>();
            DiffEntry<T> lastEqual = null;

            stck.Push(new int[2] { lines1_cnt - 1, lines2_cnt - 1 });
            do
            {
                int[] data = stck.Pop();

                int i = data[0];
                int j = data[1];

                if (i >= 0 && j >= 0 && arr1[i + start].CompareTo(arr2[j + start]) == 0)
                {
                    stck.Push(new int[2] { i - 1, j - 1 });
                    if (lastEqual != null)
                        lastEqual.Count++;
                    else
                    {
                        lastEqual = new DiffEntry<T>();
                        diffList.Add(lastEqual);
                    }
                }
                else
                {
                    if (j >= 0 && (i <= 0 || j == 0 || lcs[i, j - 1] >= lcs[i - 1, j]))
                    {
                        stck.Push(new int[2] { i, j - 1 });
                        diffList.Add(new DiffEntry<T>(DiffEntry<T>.DiffEntryType.Add, arr2[j + start]));
                        lastEqual = null;
                    }
                    else if (i >= 0 && (j <= 0 || i == 0 || lcs[i, j - 1] < lcs[i - 1, j]))
                    {
                        stck.Push(new int[2] { i - 1, j });
                        diffList.Add(new DiffEntry<T>(DiffEntry<T>.DiffEntryType.Remove, arr1[i + start]));
                        lastEqual = null;
                    }
                }
            } while (stck.Count > 0);

            diffList.Reverse();

            if (skipStart > 0)
            {
                lastEqual = new DiffEntry<T>();
                lastEqual.Count = skipStart;
                diffList.Insert(0, lastEqual);
            }

            if (skipEnd > 0)
            {
                lastEqual = new DiffEntry<T>();
                lastEqual.Count = skipEnd;
                diffList.Add(lastEqual);
            }

            return diffList;
        }

        /// <summary>
        /// Creates a list of diff entries that represent the differences between arr1 and arr2.
        /// </summary>
        /// <typeparam name="T">Class that represents the unit to compare</typeparam>
        /// <param name="arr1">Array of units.</param>
        /// <param name="arr2">Array of units.</param>
        /// <returns>List of DiffEntry classes.</returns>
        public static List<DiffEntry<T>> CreateDiff<T>(T[] arr1, T[] arr2) where T : IComparable
        {
            return CreateDiff<T>(arr1, arr2, 0, 0);
        }

        /// <summary>
        /// Creates a list of diff entries that represent the differences between lst1 and lst2.
        /// </summary>
        /// <typeparam name="T">Class that represents the unit to compare</typeparam>
        /// <param name="lst1">List of units.</param>
        /// <param name="lst2">List of units.</param>
        /// <returns>List of DiffEntry classes.</returns>
        public static List<DiffEntry<T>> CreateDiff<T>(List<T> lst1, List<T> lst2) where T : IComparable
        {
            return CreateDiff<T>(lst1.ToArray<T>(), lst2.ToArray<T>());
        }

        /// <summary>
        /// Creates a list of diff entries that represent the differences between lst1 and lst2.
        /// </summary>
        /// <typeparam name="T">Class that represents the unit to compare</typeparam>
        /// <param name="lst1">List of units.</param>
        /// <param name="lst2">List of units.</param>
        /// <param name="skipStart">Number of units to skip at the beginning.</param>
        /// <param name="skipEnd">Number of units to skip at the end.</param>
        /// <returns>List of DiffEntry classes.</returns>
        public static List<DiffEntry<T>> CreateDiff<T>(List<T> lst1, List<T> lst2, int skipStart, int skipEnd) where T : IComparable
        {
            return CreateDiff<T>(lst1.ToArray<T>(), lst2.ToArray<T>(), skipStart, skipEnd);
        }
        #endregion
    }
}
