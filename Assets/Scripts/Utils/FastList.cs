using System;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    /// <summary>
    /// A custom generic IList implementation that allows to directly access the
    /// underlying array from outside.
	/// </summary>
    public class FastList<T> : IList<T>
    {
        private const int MinCapacity = 4;

        public static implicit operator T[](FastList<T> collection) => collection._array;

        private T[] _array;
        private int _count;

        public bool IsReadOnly => false;
        public bool IsEmpty => _count == 0;

        /// <summary>
        /// The underlying array, exposed for faster iterations.
        /// </summary>
        public T[] InnerArray => _array;


        /// <inheritdoc />
        public int Count => _count;

        /// <summary>
        /// Gets or sets the underlying array's length.
        /// </summary>
        public int Capacity
        {
            get => _array.Length;
            set
            {
                if (_array.Length != value && value >= _count)
                {
                    var destinationArray = new T[value];
                    if (_array == null)
                    {
                        _array = destinationArray;
                    }
                    else
                    {
                        Array.Copy(_array, destinationArray, _count);
                        _array = destinationArray;
                    }
                        
                }
            }
        }

        public T this[int index]
        {
            get => _array[index];
            set
            {
                _array[index] = value;
            }
        }

        #region Constructors

        public FastList() : this(MinCapacity) { }
        public FastList(int capacity)
        {
            _count = 0;
            _array = capacity > 4 ? new T[capacity] : new T[MinCapacity];
        }

        public FastList(T[] sourceArray)
        {
            _array = sourceArray;
            _count = sourceArray.Length;
        }

        public FastList(IEnumerable<T> collection) : this(collection.GetEnumerator()) { }
        public FastList(IEnumerator<T> enumerator)
        {
            _count = 0;
            _array = new T[MinCapacity];

            while (enumerator.MoveNext())
            {
                Add(enumerator.Current);
            }
        }

        #endregion


        /// <summary>
        /// Adds the elements of the specified collection to the end of the FastList.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the FastList. The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                Insert(_count, item);
            }   
        }

        /// <summary>
        /// Inserts the elements of a collection into the List at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection whose elements should be inserted into the List. The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            using var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Insert(index++, enumerator.Current);
            }
        }

        /// <summary>
        /// Ensures the capacity of the underlying array is sufficient by expanding when the array is full.
        /// </summary>
        /// <param name="newCapacity"> The new capacity that the array should aim to reach as a minimum</param>
        public void EnsureCapacity(int newCapacity)
        {
            if (_array.Length < newCapacity)
            {
                var num = (_array.Length <= MinCapacity) ? MinCapacity : (_array.Length * 2);
                if (num < newCapacity)
                {
                    num = newCapacity;
                }
                Capacity = num;
            }
        }

        /// <summary>
        /// Adds an object to the end of the List.
        /// </summary>
        /// <param name="item">The object to be added to the end of the List. The value can be null for reference types.</param>
        public void Add(T item)
        {
            if (_count == Capacity)
            {
                EnsureCapacity(_count + 1);
            }
            
            _array[_count++] = item;
        }

        /// <summary>
        ///Clears the list and frees the memory used by it. 
        /// </summary>
        public void Clear()
        {
            _array = new FastList<T>();
            _count = 0;
        }

        /// <summary>
        ///Determines whether an element is in the List.
        /// </summary>
        /// <param name="item"> The object to locate in the List. The value can be null for reference types</param>
        public bool Contains(T item)
        {
            if (item == null)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (_array[i] == null)
                    {
                        return true;
                    }
                }
                return false;
            }
            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < _count; i++)
            {
                if (comparer.Equals(_array[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copies a range of elements from the underlying array starting at the first index and pastes them to the destination array starting at the specified destination index.
        /// </summary>
        /// <param name="destinationArray">The Array that receives the data.</param>
        /// <param name="destinationIndex">An Integer that represents the index in the destinationArray at which storing begins.</param>
        public void CopyTo(T[] destinationArray, int destinationIndex)
        {
            Array.Copy(_array, 0, destinationArray, destinationIndex, _count);
        }

        /// <summary>
        ///Returns the zero-based index of the first occurrence of a value in the List 
        /// </summary>
        public int IndexOf(T item)
        {
            return Array.IndexOf(_array, item, 0);
        }

        public void Insert(int index, T item)
        {
            if (_count == _array.Length)
            {
                EnsureCapacity(_count + 1);
            }
            if (index < _count)
            {
                Array.Copy(_array, index, _array, index + 1, _count - index);
            }
            _array[index] = item;
            _count++;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the List.
        /// </summary>
        /// <param name="item">The object to remove from the List. The value can be null for reference types.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the List.</returns>
        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index == -1)
            {
                return false;
            }
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            _count--;
            Array.Copy(_array, index + 1, _array, index, _count - index);
            _array[_count] = default(T);
        }

        public T Find(Predicate<T> match)
        {
            for (var i = 0; i < _count; i++)
            {
                if (match(_array[i]))
                {
                    return _array[i];
                }
            }
            return default(T);
        }

        public FastList<T> FindAll(Predicate<T> match)
        {
            var list = new FastList<T>();
            for (var i = 0; i < _count; i++)
            {
                if (match(_array[i]))
                {
                    list.Add(_array[i]);
                }
            }
            return list;
        }

        /// <summary>
        /// Performs the specified action on each element of the underlying array.
        /// </summary>
        /// <param name="action">The Action delegate to perform on each element of the array.</param>
        public void ForEach(Action<T> action)
        {
            for (var i = 0; i < _count; i++)
            {
                action(_array[i]);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
