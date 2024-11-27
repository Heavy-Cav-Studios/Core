using System;
using UnityEngine;

namespace HeavyCavStudios.Core.Serialization
{
    /// <summary>
    /// Represents a serializable range with minimum and maximum values.
    /// </summary>
    /// <typeparam name="T">The type of values in the range, must implement <see cref="IComparable"/>.</typeparam>
    [Serializable]
    public class SerializableRange<T> where T : IComparable
    {
        [SerializeField]
        T m_MinValue;
        [SerializeField]
        T m_MaxValue;

        /// <summary>
        /// Gets the minimum value of the range.
        /// </summary>
        public T MinValue => m_MinValue;

        /// <summary>
        /// Gets the maximum value of the range.
        /// </summary>
        public T MaxValue => m_MaxValue;

        /// <summary>
        /// Checks if the current instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as SerializableRange<T>);
        }

        /// <summary>
        /// Checks if the current instance is equal to another SerializableRange of the same type.
        /// </summary>
        /// <param name="other">The other range to compare with.</param>
        /// <returns>True if the ranges are equal; otherwise, false.</returns>
        public bool Equals(SerializableRange<T> other)
        {
            return m_MinValue.Equals(other.m_MinValue) && m_MaxValue.Equals(other.m_MaxValue);
        }

        /// <summary>
        /// Gets the hash code for the current instance.
        /// </summary>
        /// <returns>The hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(m_MinValue, m_MaxValue);
        }
    }
}
