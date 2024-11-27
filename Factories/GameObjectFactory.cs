using System;
using HeavyCavStudios.Core.Patterns.Factory;
using UnityEngine;

namespace HeavyCavStudios.Core.Factories
{
    /// <summary>
    /// Factory class for creating instances of GameObjects using a custom creation function.
    /// </summary>
    public class GameObjectFactory : IFactory<GameObject>
    {
        Func<GameObject, GameObject> m_CreateFunc;
        GameObject m_Prefab;

        /// <summary>
        /// Initializes a new instance of the GameObjectFactory class.
        /// </summary>
        /// <param name="prefab">The prefab GameObject to be used for creating instances.</param>
        /// <param name="createFunc">The function used to create the GameObject instances.</param>
        public GameObjectFactory(GameObject prefab, Func<GameObject, GameObject> createFunc)
        {
            m_Prefab = prefab;
            m_CreateFunc = createFunc;
        }

        /// <summary>
        /// Creates an instance of the GameObject using the custom creation function.
        /// </summary>
        /// <returns>An instance of the GameObject.</returns>
        public GameObject Create()
        {
            return m_CreateFunc(m_Prefab);
        }
    }
}
