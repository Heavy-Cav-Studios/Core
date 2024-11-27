using System.Collections.Generic;
using UnityEngine;

namespace HeavyCavStudios.Core.Serialization
{
    /// <summary>
    /// Represents a dictionary that can be serialized by Unity.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
    public abstract class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        List<TKey> m_KeyData = new List<TKey>();

        [SerializeField, HideInInspector]
        List<TValue> m_ValueData = new List<TValue>();

        /// <summary>
        /// Method called after the object is deserialized. Used to restore the dictionary state.
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            for (int i = 0; i < m_KeyData.Count && i < m_ValueData.Count; i++)
            {
                this[m_KeyData[i]] = m_ValueData[i];
            }
        }

        /// <summary>
        /// Method called before the object is serialized. Used to save the dictionary state.
        /// </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            m_KeyData.Clear();
            m_ValueData.Clear();

            foreach (var item in this)
            {
                m_KeyData.Add(item.Key);
                m_ValueData.Add(item.Value);
            }
        }
    }
}
