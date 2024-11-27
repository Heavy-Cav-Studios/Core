using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HeavyCavStudios.Core.Reflection
{
    /// <summary>
    /// Utility class for handling reflection-related operations.
    /// </summary>
    public static class ReflectionUtility
    {
        /// <summary>
        /// Gets constructors for types that implement a non-generic interface.
        /// </summary>
        /// <param name="assembly">The assembly to search in.</param>
        /// <param name="interfaceType">The non-generic interface type.</param>
        /// <param name="attributesToIgnore">A list of attributes to ignore.</param>
        /// <returns>A list of functions for invoking constructors of types implementing the given interface.</returns>
        public static List<Func<object[], object>> GetImplementedConstructorsForNonGenericInterface(
            Assembly assembly,
            Type interfaceType,
            List<Type> attributesToIgnore)
        {
            var implementations = new List<Func<object[], object>>();
            var assemblyTypes = assembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                if (ContainsAnyOfAttributes(type, attributesToIgnore))
                {
                    continue;
                }

                if (interfaceType.IsAssignableFrom(type) && !ContainsGenericInterfaceInAncestors(type) && !type.IsInterface && !type.IsAbstract)
                {
                    var ctor = type.GetConstructors().FirstOrDefault();
                    if (ctor != null)
                    {
                        implementations.Add(args => ctor.Invoke(args));
                    }
                }
            }

            return implementations;
        }

        /// <summary>
        /// Gets constructors for types that implement a generic interface.
        /// </summary>
        /// <param name="assembly">The assembly to search in.</param>
        /// <param name="genericInterfaceType">The generic interface type.</param>
        /// <param name="attributesToIgnore">A list of attributes to ignore.</param>
        /// <returns>A dictionary mapping each generic type argument to a list of constructors for types implementing the given interface.</returns>
        public static Dictionary<Type, List<Func<object[], object>>> GetImplementedConstructorsForGenericInterface(
            Assembly assembly,
            Type genericInterfaceType,
            List<Type> attributesToIgnore)
        {
            var implementations = new Dictionary<Type, List<Func<object[], object>>>();
            var assemblyTypes = assembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                if (ContainsAnyOfAttributes(type, attributesToIgnore))
                {
                    continue;
                }

                foreach (var i in type.GetInterfaces())
                {
                    if (i.IsGenericType && (i.GetGenericTypeDefinition() == genericInterfaceType || i.GetGenericTypeDefinition().BaseType == genericInterfaceType))
                    {
                        var genericTypeArgument = i.GetGenericArguments()[0];
                        var ctor = type.GetConstructors().FirstOrDefault();
                        if (ctor != null)
                        {
                            if (!implementations.ContainsKey(genericTypeArgument))
                            {
                                implementations[genericTypeArgument] = new List<Func<object[], object>>();
                            }
                            implementations[genericTypeArgument].Add(args => ctor.Invoke(args));
                        }
                    }
                }
            }

            return implementations;
        }

        /// <summary>
        /// Checks if an object implements a specific generic interface.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="genericInterfaceType">The generic interface type to check for.</param>
        /// <returns>True if the object implements the specified generic interface; otherwise, false.</returns>
        public static bool ImplementsGenericInterface(object obj, Type genericInterfaceType)
        {
            var interfaces = obj.GetType().GetInterfaces();

            foreach (var i in interfaces)
            {
                if (i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Extracts the type argument from a type that implements a generic interface.
        /// </summary>
        /// <param name="type">The type to examine.</param>
        /// <param name="genericInterface">The generic interface type.</param>
        /// <returns>The extracted type argument, or null if not found.</returns>
        public static Type ExtractGenericArgumentType(Type type, Type genericInterface)
        {
            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterface))
            {
                var interfaceType = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterface);
                return interfaceType.GenericTypeArguments[0];
            }

            return null;
        }

        /// <summary>
        /// Attempts to set a property on an object by name.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">The value to set.</param>
        /// <returns>True if the property was successfully set; otherwise, false.</returns>
        public static bool TrySetProperty(object target, string propertyName, object propertyValue)
        {
            var propertyInfo = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfo?.SetValue(target, propertyValue);

            return propertyInfo != null;
        }

        /// <summary>
        /// Gets a property value of the specified type from an object.
        /// </summary>
        /// <typeparam name="T">The expected type of the property.</typeparam>
        /// <param name="target">The target object.</param>
        /// <param name="property">The name of the property.</param>
        /// <returns>The value of the property if found; otherwise, null.</returns>
        public static T GetProperty<T>(object target, string property) where T : class
        {
            var propertyInfo = target.GetType().GetProperty(property);

            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(target) as T;
            }

            return null;
        }

        /// <summary>
        /// Gets a property value from an object.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="property">The name of the property.</param>
        /// <returns>The value of the property if found; otherwise, null.</returns>
        public static object GetProperty(object target, string property)
        {
            var propertyInfo = target.GetType().GetProperty(property);

            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(target);
            }

            return null;
        }

        /// <summary>
        /// Gets all types implementing a specified interface and optionally instantiates them if they have parameterless constructors.
        /// </summary>
        /// <typeparam name="TInterface">The interface type to search for.</typeparam>
        /// <param name="assemblies">The assemblies to search within.</param>
        /// <param name="instantiate">If true, instantiates the found types (requires a parameterless constructor).</param>
        /// <returns>A list of implementations or instances of the specified interface.</returns>
        public static List<TInterface> GetImplementationsOfInterface<TInterface>(IEnumerable<Assembly> assemblies, bool instantiate = false)
        {
            var implementations = new List<TInterface>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(TInterface).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        if (instantiate)
                        {
                            var ctor = type.GetConstructor(Type.EmptyTypes);
                            if (ctor != null)
                            {
                                var instance = (TInterface)ctor.Invoke(null);
                                implementations.Add(instance);
                            }
                        }
                        else
                        {
                            implementations.Add((TInterface)(object)type);
                        }
                    }
                }
            }

            return implementations;
        }

        /// <summary>
        /// Checks if the specified type contains any of the given generic interfaces in its ancestors.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if any of the ancestors contain a generic interface; otherwise, false.</returns>
        static bool ContainsGenericInterfaceInAncestors(Type type)
        {
            foreach (var parentInterface in type.GetInterfaces())
            {
                if (parentInterface.IsGenericType)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the specified type contains any of the given attributes.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="attributes">A list of attributes to check for.</param>
        /// <returns>True if any of the given attributes are found; otherwise, false.</returns>
        static bool ContainsAnyOfAttributes(Type type, List<Type> attributes)
        {
            var customAttributes = type.GetCustomAttributesData().Select(data => data.AttributeType).ToList();
            return attributes.Any(attribute => customAttributes.Contains(attribute));
        }
    }
}
