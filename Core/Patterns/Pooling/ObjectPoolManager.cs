using System;
using System.Collections.Generic;
using HeavyCavStudios.Core.Patterns.Factory;
using HeavyCavStudios.Core.Patterns.Singleton;

namespace HeavyCavStudios.Core.Patterns.Pooling
{
    /// <summary>
    /// Manages multiple object pools, allowing for the creation, retrieval, and reuse of objects of different types.
    /// </summary>
    public class ObjectPoolManager : AbstractSingleton<ObjectPoolManager>
    {
        const int k_DefaultSize = 10;
        Dictionary<Type, object> m_TypeToPool;

        /// <summary>
        /// Initializes the object pool manager.
        /// </summary>
        protected override void Initialize()
        {
            m_TypeToPool = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Creates a new object pool of the specified type with a given factory and initial size.
        /// </summary>
        /// <typeparam name="T">The type of objects to pool.</typeparam>
        /// <param name="factory">The factory used to create new instances of <typeparamref name="T"/>.</param>
        /// <param name="initialSize">The initial number of objects to be created in the pool.</param>
        /// <exception cref="Exception">Thrown if a pool of the specified type already exists.</exception>
        public void CreatePool<T>(IFactory<T> factory, int initialSize)
        {
            if (!m_TypeToPool.ContainsKey(typeof(T)))
            {
                var pool = new ObjectPool<T>(initialSize, factory);
                m_TypeToPool.Add(typeof(T), pool);
                return;
            }

            throw new Exception($"Pool of type: {typeof(T)} already exists.");
        }

        /// <summary>
        /// Checks if a pool of the specified type exists.
        /// </summary>
        /// <typeparam name="T">The type of objects to check for.</typeparam>
        /// <returns>True if a pool of the specified type exists; otherwise, false.</returns>
        public bool PoolOfTypeExists<T>()
        {
            return m_TypeToPool.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Retrieves an object of the specified type from the corresponding pool.
        /// </summary>
        /// <typeparam name="T">The type of object to retrieve.</typeparam>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception">Thrown if a pool of the specified type does not exist.</exception>
        public T GetObject<T>()
        {
            if (m_TypeToPool.TryGetValue(typeof(T), out object pool))
            {
                return ((ObjectPool<T>)pool).GetObject();
            }

            throw new Exception($"Pool of type: {typeof(T)} is not found.");
        }

        /// <summary>
        /// Returns an object to the corresponding pool for future reuse.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="obj">The object to return to the pool.</param>
        public void ReturnObject<T>(T obj)
        {
            if (m_TypeToPool.TryGetValue(typeof(T), out object pool))
            {
                ((ObjectPool<T>)pool).ReturnObject(obj);
            }
        }

        /// <summary>
        /// Clears all pools managed by the pool manager.
        /// </summary>
        public void Clear()
        {
            m_TypeToPool.Clear();
        }
    }
}
