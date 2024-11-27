#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HeavyCavStudios.Core.Editor
{
    /// <summary>
    /// Provides utility methods for working with assets in the Unity Editor.
    /// </summary>
    public static class AssetUtilities
    {
        /// <summary>
        /// Loads all assets of the specified type from the project.
        /// </summary>
        /// <typeparam name="T">The type of asset to load. Must be a <see cref="ScriptableObject"/>.</typeparam>
        /// <returns>A list of all assets of the specified type found in the project.</returns>
        public static List<T> LoadAllAssetsOfType<T>() where T : ScriptableObject
        {
            // Find all assets of type T
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            var assets = new List<T>();

            foreach (string guid in guids)
            {
                // Convert the GUID to a file path
                var path = AssetDatabase.GUIDToAssetPath(guid);

                // Load the asset at the path
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);

                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            return assets;
        }
    }
}
#endif
