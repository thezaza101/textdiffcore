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
    public class DiffEntry<T>
    {
        #region Enumerations
        public enum DiffEntryType
        {
            Remove,
            Add,
            Equal
        };
        #endregion

        #region Fields
        private DiffEntryType _entryType;
        private T _obj;
        private int _count;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of this entry.
        /// </summary>
        public DiffEntryType EntryType { get { return _entryType; } }
        /// <summary>
        /// Gets the associated object for Add/Remove entries.
        /// </summary>
        public T Object
        {
            get
            {
                if (_entryType == DiffEntry<T>.DiffEntryType.Equal)
                    throw new InvalidOperationException("Object is only valid for Add/Remove entries");

                return _obj;
            }
        }
        /// <summary>
        /// Gets the number of Equal entries.
        /// </summary>
        public int Count
        {
            get
            {
                if (_entryType != DiffEntry<T>.DiffEntryType.Equal)
                    throw new InvalidOperationException("Count is only valid for Equal entries");

                return _count;
            }
            set
            {
                _count = value;
            }
        }
        #endregion

        #region Constructors
        internal DiffEntry(DiffEntryType entryType, T obj)
        {
            _entryType = entryType;
            _obj = obj;
        }

        internal DiffEntry()
        {
            _entryType = DiffEntryType.Equal;
            _count = 1;
        }
        #endregion
    }
}
