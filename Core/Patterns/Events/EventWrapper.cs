using System;
using System.Collections.Generic;
using System.Linq;

namespace HeavyCavStudios.Core.Patterns.Events
{
    /// <summary>
    /// Represents a wrapper for managing event handlers of a specific type.
    /// </summary>
    class EventWrapper
    {
        string m_Name;
        Type m_Type;
        List<object> m_Handlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventWrapper"/> class.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="t">The type of the event.</param>
        public EventWrapper(string name, Type t)
        {
            m_Name = name;
            m_Type = t;
            m_Handlers = new List<object>();
        }

        /// <summary>
        /// Adds a handler to the event.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="handler">The event handler to be added.</param>
        /// <exception cref="TypeMismatchException">Thrown when the handler type does not match the event type.</exception>
        public void AddHandler<T>(EventHandler<T> handler)
        {
            if (typeof(T) != m_Type)
            {
                throw new TypeMismatchException($"Event {m_Name} is of type {m_Type} cannot add a listener of type {typeof(T)}");
            }

            m_Handlers.Add(handler);
        }

        /// <summary>
        /// Removes a handler from the event.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="handler">The event handler to be removed.</param>
        /// <exception cref="TypeMismatchException">Thrown when the handler type does not match the event type.</exception>
        public void RemoveHandler<T>(EventHandler<T> handler)
        {
            if (typeof(T) != m_Type)
            {
                throw new TypeMismatchException($"Event {m_Name} is of type {m_Type} cannot remove a listener of type {typeof(T)}");
            }

            m_Handlers.Remove(handler);
        }

        /// <summary>
        /// Invokes all handlers for the event.
        /// </summary>
        /// <typeparam name="T">The type of the event arguments.</typeparam>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <exception cref="TypeMismatchException">Thrown when the argument type does not match the event type.</exception>
        public void Invoke<T>(object sender, T args)
        {
            if (typeof(T) != m_Type)
            {
                throw new TypeMismatchException($"Event {m_Name} is of type {m_Type} cannot be invoked with argument of type {typeof(T)}");
            }

            var handlersCopy = m_Handlers.ToList();

            foreach (EventHandler<T> handler in handlersCopy)
            {
                handler?.Invoke(sender, args);
            }
        }
    }
}
