using System;

namespace HeavyCavStudios.Core.Patterns.Singleton
{
    /// <summary>
    /// Abstract base class for creating non-MonoBehaviour singleton instances.
    /// </summary>
    /// <typeparam name="T">The type of the singleton class.</typeparam>
    public abstract class AbstractSingleton<T> where T : AbstractSingleton<T>, new()
    {
        static T s_Instance;

        /// <summary>
        /// Gets the singleton instance, creating it if it does not already exist.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new T();
                    s_Instance.Initialize();
                }

                return s_Instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSingleton{T}"/> class.
        /// </summary>
        /// <exception cref="Exception">Thrown if an instance of the singleton already exists.</exception>
        protected AbstractSingleton()
        {
            if (s_Instance != null)
            {
                throw new Exception($"Singleton of type {typeof(T)} cannot be instantiated multiple times");
            }
        }

        /// <summary>
        /// Abstract method for initializing the singleton instance.
        /// </summary>
        protected abstract void Initialize();
    }
}
