using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HeavyCavStudios.Core.Collections
{
    /// <summary>
    /// Represents an ordered dictionary that maintains the order of elements as they are added.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public class OrderedDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        readonly Dictionary<TKey, TValue> m_Dictionary = new Dictionary<TKey, TValue>();
        readonly List<TKey> m_Keys = new List<TKey>();

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public TValue this[TKey key]
        {
            get => m_Dictionary[key];
            set
            {
                Add(key, value);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the dictionary.
        /// </summary>
        public int Count => m_Dictionary.Count;

        /// <summary>
        /// Gets a list of all values in the dictionary in the order of their keys.
        /// </summary>
        public List<TValue> Values => m_Dictionary.Values.ToList();

        /// <summary>
        /// Adds a key-value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <exception cref="ArgumentException">Thrown when an element with the same key already exists in the dictionary.</exception>
        public void Add(TKey key, TValue value)
        {
            m_Dictionary.Add(key, value);

            if (!m_Keys.Contains(key))
            {
                m_Keys.Add(key);
            }
        }

        /// <summary>
        /// Removes the key-value pair with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>True if the element was successfully removed; otherwise, false.</returns>
        public bool Remove(TKey key)
        {
            if (m_Dictionary.Remove(key))
            {
                m_Keys.Remove(key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears all elements in the dictionary.
        /// </summary>
        public void Clear()
        {
            m_Keys.Clear();
            m_Dictionary.Clear();
        }

        /// <summary>
        /// Retrieves the value at the specified index or returns the default value if the index is out of range.
        /// </summary>
        /// <param name="index">The index of the value to retrieve.</param>
        /// <returns>The value at the specified index or the default value if the index is out of range.</returns>
        public TValue GetAtOrDefault(int index)
        {
            if (Count <= index)
            {
                return default(TValue);
            }

            return m_Dictionary[m_Keys[index]];
        }

        /// <summary>
        /// Returns an enumerator that iterates through the ordered key-value pairs.
        /// </summary>
        /// <returns>An enumerator for the ordered dictionary.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var key in m_Keys)
            {
                yield return new KeyValuePair<TKey, TValue>(key, m_Dictionary[key]);
            }
        }

        /// <summary>
        /// Attempts to get the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if it exists.</param>
        /// <returns>True if the key was found; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_Dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
