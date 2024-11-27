using System.Collections.Generic;

namespace HeavyCavStudios.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns a random element from the list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to get a random element from.</param>
        /// <returns>A random element from the list, or the default value if the list is empty.</returns>
        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }

            var random = new System.Random();
            var randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }
    }
}
