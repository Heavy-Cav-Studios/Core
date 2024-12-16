using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeavyCavStudios.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with GameObjects.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Gets a child GameObject by name.
        /// </summary>
        /// <param name="gameObject">The parent GameObject.</param>
        /// <param name="name">The name of the child GameObject.</param>
        /// <param name="recursive">If true, searches recursively through all child GameObjects.</param>
        /// <returns>The child GameObject with the specified name, or null if not found.</returns>
        public static GameObject GetChildByName(this GameObject gameObject, string name, bool recursive = false)
        {
            foreach (Transform childTransform in gameObject.transform)
            {
                if (childTransform.gameObject.name == name)
                {
                    return childTransform.gameObject;
                }

                if (recursive)
                {
                    var childGameObject = childTransform.gameObject.GetChildByName(name, recursive);
                    if (childGameObject != null)
                    {
                        return childGameObject;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all child GameObjects that satisfy the specified selector.
        /// </summary>
        /// <param name="gameObject">The parent GameObject.</param>
        /// <param name="selector">The function to determine which children to select.</param>
        /// <param name="recursive">If true, searches recursively through all child GameObjects.</param>
        /// <returns>A list of child GameObjects that match the selector.</returns>
        public static List<GameObject> GetChildren(this GameObject gameObject, Func<GameObject, bool> selector = null, bool recursive = false)
        {
            var children = new List<GameObject>();
            
            foreach (Transform childTransform in gameObject.transform)
            {
                var child = childTransform.gameObject;
                
                if (selector == null || selector(child))
                {
                    children.Add(child);
                }

                if (recursive)
                {
                    children.AddRange(child.GetChildren(selector, recursive));
                }
            }

            return children;
        }

        /// <summary>
        /// Gets a child GameObject by tag.
        /// </summary>
        /// <param name="gameObject">The parent GameObject.</param>
        /// <param name="tag">The tag of the child GameObject.</param>
        /// <param name="recursive">If true, searches recursively through all child GameObjects.</param>
        /// <returns>The child GameObject with the specified tag, or null if not found.</returns>
        public static GameObject GetChildByTag(this GameObject gameObject, string tag, bool recursive = false)
        {
            foreach (Transform childTransform in gameObject.transform)
            {
                if (childTransform.CompareTag(tag))
                {
                    return childTransform.gameObject;
                }

                if (recursive)
                {
                    var childGameObject = childTransform.gameObject.GetChildByTag(tag, recursive);
                    if (childGameObject != null)
                    {
                        return childGameObject;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a component from a child GameObject by name.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <param name="gameObject">The parent GameObject.</param>
        /// <param name="name">The name of the child GameObject.</param>
        /// <param name="recursive">If true, searches recursively through all child GameObjects.</param>
        /// <param name="throwIfChildNotFound">If true, throws an exception if the child is not found.</param>
        /// <returns>The component of type <typeparamref name="T"/> from the child GameObject, or the default value if not found.</returns>
        public static T GetComponentOfChild<T>(this GameObject gameObject, string name, bool recursive = false, bool throwIfChildNotFound = false)
        {
            var child = gameObject.GetChildByName(name, recursive);

            if (throwIfChildNotFound && child == null)
            {
                throw new Exception($"Child {name} of {gameObject.name} not found");
            }

            if (child != null && child.TryGetComponent<T>(out var component))
            {
                return component;
            }

            return default;
        }

        /// <summary>
        /// Checks if a GameObject is a UI element.
        /// </summary>
        /// <param name="gameObject">The GameObject to check.</param>
        /// <returns>True if the GameObject is a UI element; otherwise, false.</returns>
        public static bool IsUIElement(this GameObject gameObject)
        {
            return gameObject.TryGetComponent<RectTransform>(out _);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="includeInactive"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindComponentsInScene<T>(this GameObject gameObject, bool includeInactive = false)
        {
            var components = new List<T>();
            
            foreach (var rootGameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                var foundComponents = rootGameObject.GetComponentsInChildren<T>(includeInactive: includeInactive);

                if (!foundComponents.Any())
                {
                    continue;
                }
                
                components.AddRange(foundComponents);
            }

            return components;
        }
    }
}
