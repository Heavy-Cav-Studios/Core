using UnityEngine;

namespace HeavyCavStudios.Core.Serialization
{
    /// <summary>
    /// A ScriptableObject that creates a runtime instance of itself for modifications without affecting the original asset.
    /// </summary>
    public class RuntimeScriptableObject : ScriptableObject
    {
        [SerializeField, HideInInspector]
        ScriptableObject m_Original;

        [SerializeField, HideInInspector]
        ScriptableObject m_RuntimeInstance;

        /// <summary>
        /// Gets or sets the original ScriptableObject asset.
        /// When set, a runtime instance is created based on the original.
        /// </summary>
        internal ScriptableObject Original
        {
            get { return m_Original; }
            set
            {
                m_Original = value;
                CreateRuntimeInstance();
            }
        }

        /// <summary>
        /// Creates a runtime instance of the original ScriptableObject.
        /// </summary>
        void CreateRuntimeInstance()
        {
            if (m_Original != null)
            {
                m_RuntimeInstance = Instantiate(m_Original);
                Debug.Log("Runtime instance created.");
            }
            else
            {
                m_RuntimeInstance = null;
                Debug.LogWarning("Original ScriptableObject is null, runtime instance not created.");
            }
        }

        /// <summary>
        /// Retrieves the runtime instance cast to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to which the runtime instance should be cast.</typeparam>
        /// <returns>The runtime instance cast to type <typeparamref name="T"/>, or null if the cast fails.</returns>
        public T Get<T>() where T : ScriptableObject
        {
            if (m_RuntimeInstance is T castedInstance)
            {
                return castedInstance;
            }
            else
            {
                Debug.LogWarning("Runtime instance is not of the expected type.");
                return null;
            }
        }

        /// <summary>
        /// Retrieves the runtime instance.
        /// </summary>
        /// <returns>The runtime instance of the ScriptableObject.</returns>
        public ScriptableObject Get()
        {
            return m_RuntimeInstance;
        }
    }
}
