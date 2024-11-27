using System;
using UnityEngine;

namespace HeavyCavStudios.Core.Patterns.Singleton
{
    /// <summary>
    /// Abstract base class for creating MonoBehaviour singletons in Unity.
    /// </summary>
    /// <typeparam name="T">The type of the singleton class.</typeparam>
    public abstract class AbstractMonoSingleton<T> : MonoBehaviour where T : AbstractMonoSingleton<T>
    {
        const string k_InstanceProperty = "Instance";
        const string k_InitializedProperty = "Initialized";

        /// <summary>
        /// Action triggered when the singleton instance is initialized.
        /// </summary>
        public static Action Initialized;

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// Unity's Awake method to handle the singleton instantiation.
        /// </summary>
        protected void Awake()
        {
            // Support DontDestroyOnLoad case
            if (Instance == this)
            {
                return;
            }

            if (Instance == null)
            {
                Instance = (T)this;
                Instantiate();
                Initialized?.Invoke();
                return;
            }

            Debug.LogError($"Singleton {typeof(T)} tried to be instantiated multiple times");
            Destroy(gameObject);
        }

        /// <summary>
        /// Abstract method for initializing the singleton instance.
        /// </summary>
        protected abstract void Instantiate();

        /// <summary>
        /// Unity's OnDestroy method to handle singleton cleanup.
        /// </summary>
        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}
