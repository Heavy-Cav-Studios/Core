using HeavyCavStudios.Core.Patterns.Factory;
using UnityEngine;

namespace HeavyCavStudios.Core.Factories
{
    /// <summary>
    /// Factory class for creating instances of a component attached to a prefab GameObject.
    /// </summary>
    /// <typeparam name="T">The type of component to create.</typeparam>
    public class GameObjectComponentFactory<T> : IFactory<T> where T : Component
    {
        GameObject m_Prefab;
        Transform m_ParentTransform;
        bool m_IsActiveByDefault;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectComponentFactory{T}"/> class.
        /// </summary>
        /// <param name="prefab">The prefab GameObject from which to create components.</param>
        /// <param name="parentTransform">The transform that will be the parent of the instantiated prefab.</param>
        /// <param name="isActiveByDefault">Determines if the instantiated GameObject is active by default.</param>
        public GameObjectComponentFactory(GameObject prefab, Transform parentTransform, bool isActiveByDefault)
        {
            m_Prefab = prefab;
            m_ParentTransform = parentTransform;
            m_IsActiveByDefault = isActiveByDefault;
        }

        /// <summary>
        /// Creates an instance of the specified component type attached to the prefab.
        /// </summary>
        /// <returns>An instance of the component of type <typeparamref name="T"/>.</returns>
        public T Create()
        {
            var gameObject = GameObject.Instantiate(m_Prefab, m_ParentTransform);
            gameObject.SetActive(m_IsActiveByDefault);
            return gameObject.GetComponent<T>();
        }
    }
}
