using System;
using UnityEngine;

namespace HeavyCavStudios.Core.Serialization
{
    /// <summary>
    /// Represents a serializable nullable integer.
    /// </summary>
    [Serializable]
    public class NullableInt
    {
        [SerializeField]
        bool m_HasValue;
        [SerializeField]
        int m_Value;

        /// <summary>
        /// Gets or sets the nullable value.
        /// </summary>
        public int? Value
        {
            get => m_HasValue ? m_Value : null;
            set
            {
                m_HasValue = value.HasValue;
                m_Value = value.GetValueOrDefault();
            }
        }

        /// <summary>
        /// Checks if the current instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            NullableInt other = (NullableInt)obj;
            return (m_HasValue == other.m_HasValue) && (!m_HasValue || m_Value == other.m_Value);
        }

        /// <summary>
        /// Gets the hash code for the current instance.
        /// </summary>
        /// <returns>The hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + m_HasValue.GetHashCode();
                if (m_HasValue)
                {
                    hash = hash * 23 + m_Value.GetHashCode();
                }
                return hash;
            }
        }

        /// <summary>
        /// Checks if the current instance is equal to another NullableInt instance.
        /// </summary>
        /// <param name="other">The other NullableInt instance to compare with.</param>
        /// <returns>True if the instances are equal; otherwise, false.</returns>
        public bool Equals(NullableInt other)
        {
            if (other == null)
                return false;

            return (m_HasValue == other.m_HasValue) && (!m_HasValue || m_Value == other.m_Value);
        }
    }
}
