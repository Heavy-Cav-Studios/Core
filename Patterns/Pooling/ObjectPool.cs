using System.Collections.Generic;
using HeavyCavStudios.Core.Patterns.Factory;

namespace HeavyCavStudios.Core.Patterns.Pooling
{
    /// <summary>
    /// Represents an object pool for reusing instances of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of objects to pool.</typeparam>
    public class ObjectPool<T>
    {
        Stack<T> m_Pool;
        IFactory<T> m_Factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class with a specified initial size and a factory to create new instances.
        /// </summary>
        /// <param name="initialSize">The initial number of objects to be created in the pool.</param>
        /// <param name="factory">The factory used to create new instances of <typeparamref name="T"/>.</param>
        public ObjectPool(int initialSize, IFactory<T> factory)
        {
            m_Pool = new Stack<T>();
            m_Factory = factory;

            for (var i = 0; i < initialSize; i++)
            {
                m_Pool.Push(factory.Create());
            }
        }

        /// <summary>
        /// Retrieves an object from the pool. If no objects are available, a new instance is created.
        /// </summary>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        public T GetObject()
        {
            return (m_Pool.Count > 0) ? m_Pool.Pop() : m_Factory.Create();
        }

        /// <summary>
        /// Returns an object to the pool for future reuse.
        /// </summary>
        /// <param name="obj">The object to return to the pool.</param>
        public void ReturnObject(T obj)
        {
            m_Pool.Push(obj);
        }
    }
}
