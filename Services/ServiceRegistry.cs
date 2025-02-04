using System;
using System.Collections.Generic;
using UnityEngine;

namespace HeavyCavStudios.Core.Services
{
    /// <summary>
    /// A centralized registry for managing services that implement the <see cref="IService"/> interface.
    /// The ServiceRegistry allows you to register, retrieve, initialize, and unregister services globally,
    /// providing a flexible and decoupled approach to service management in Unity projects.
    /// 
    /// This class is designed to follow the Service Locator pattern, ensuring that services are
    /// accessible from anywhere within your application.
    /// 
    /// Example Usage:
    /// 
    /// Registering a Service:
    /// <code>
    /// ServiceRegistry.Register<IAudioService>(new AudioService());
    /// </code>
    /// 
    /// Retrieving a Service:
    /// <code>
    /// var audioService = ServiceRegistry.Get<IAudioService>();
    /// audioService?.PlaySound(audioClip);
    /// </code>
    /// 
    /// Initializing All Services:
    /// <code>
    /// ServiceRegistry.InitializeAll();
    /// </code>
    /// 
    /// Unregistering a Service:
    /// <code>
    /// ServiceRegistry.Unregister<IAudioService>();
    /// </code>
    /// 
    /// </summary>
    public static class ServiceRegistry

    {
        static readonly Dictionary<Type, IService> m_Services = new Dictionary<Type, IService>();

        /// <summary>
        /// Registers a service instance. If a service of the same type already exists, it will not be overwritten.
        /// </summary>
        /// <typeparam name="T">The type of service to register.</typeparam>
        /// <param name="service">The service instance.</param>
        public static void Register<T>(T service, bool raise = false) where T : class, IService
        {
            var type = typeof(T);

            if (m_Services.ContainsKey(type))
            {
                if (raise)
                {
                    throw new ServiceAlreadyRegisteredException($"Service {type} is already registered");
                }
                
                return;
            }

            m_Services[type] = service;
        }

        /// <summary>
        /// Retrieves a registered service.
        /// </summary>
        /// <typeparam name="T">The type of service to retrieve.</typeparam>
        /// <returns>The service instance, or null if it is not registered.</returns>
        public static T Get<T>() where T : class, IService
        {
            var type = typeof(T);

            if (m_Services.TryGetValue(type, out var service))
            {
                return service as T;
            }
            
            return null;
        }

        /// <summary>
        /// Removes a service of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of service to remove.</typeparam>
        public static void Unregister<T>(bool raise = false) where T : class, IService
        {
            var type = typeof(T);

            if (m_Services.Remove(type))
            {
                Debug.Log($"Service {type} unregistered");
            }
            else
            {
                if (raise)
                {
                    throw new ServiceNotFoundException($"ServiceRegistry: No service of type {type.Name} found to unregister.");
                }
            }
        }

        /// <summary>
        /// Initializes all registered services that implement the Initialize method.
        /// </summary>
        public static void InitializeAll()
        {
            foreach (var service in m_Services.Values)
            {
                service.Initialize();
            }

            Debug.Log("ServiceRegistry: All services initialized.");
        }

        /// <summary>
        /// Clears all registered services from the registry.
        /// </summary>
        public static void Clear()
        {
            m_Services.Clear();
            Debug.Log("ServiceRegistry: Cleared all registered services.");
        }
    }
}