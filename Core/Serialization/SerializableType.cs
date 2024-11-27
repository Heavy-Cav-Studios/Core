using System;
using UnityEngine;

namespace HeavyCavStudios.Core.Serialization
{
    /// <summary>
    /// Represents a serializable wrapper for a <see cref="Type"/> object.
    /// </summary>
    [Serializable]
    public class SerializableType
    {
        [SerializeField]
        string m_TypeName;

        /// <summary>
        /// Gets the <see cref="Type"/> represented by this instance.
        /// </summary>
        public Type Type => Type.GetType(m_TypeName);

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableType"/> class.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to serialize.</param>
        public SerializableType(Type type)
        {
            m_TypeName = type.AssemblyQualifiedName;
        }
    }
}
