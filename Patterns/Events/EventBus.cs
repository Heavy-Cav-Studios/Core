using System;
using System.Collections.Generic;
using HeavyCavStudios.Core.Patterns.Singleton;

namespace HeavyCavStudios.Core.Patterns.Events
{
    /// <summary>
    /// Represents a singleton event bus that allows subscribing to, raising, and managing events of different types.
    /// </summary>
    public sealed class EventBus : AbstractSingleton<EventBus>
    {
        Dictionary<Type, EventWrapper> m_EventLookup = new Dictionary<Type, EventWrapper>();

        /// <summary>
        /// Subscribes to an event of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of event to subscribe to.</typeparam>
        /// <param name="name">The name of the event.</param>
        /// <param name="handler">The event handler to be called when the event is raised.</param>
        public void Subscribe<T>(string name, EventHandler<T> handler) where T : class, IEvent
        {
            if (!m_EventLookup.TryGetValue(typeof(T), out var @event))
            {
                @event = new EventWrapper(name, typeof(T));
                m_EventLookup[typeof(T)] = @event;
            }

            @event.AddHandler(handler);
        }

        /// <summary>
        /// Unsubscribes from an event of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of event to unsubscribe from.</typeparam>
        /// <param name="name">The name of the event.</param>
        /// <param name="handler">The event handler to be removed.</param>
        /// <exception cref="EventMissingException">Thrown when the event does not exist in the event lookup.</exception>
        public void Unsubscribe<T>(string name, EventHandler<T> handler) where T : class, IEvent
        {
            if (!m_EventLookup.TryGetValue(typeof(T), out var @event))
            {
                throw new EventMissingException($"Event of type {typeof(T)} was never subscribed");
            }

            @event.RemoveHandler(handler);
        }

        /// <summary>
        /// Raises an event of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of event to raise.</typeparam>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <param name="raise">If true, raises the event; otherwise, does nothing if the event does not exist.</param>
        /// <exception cref="EventMissingException">Thrown when the event does not exist in the event lookup and raise is true.</exception>
        public void Raise<T>(object sender, T args, bool raise = true) where T : class, IEvent
        {
            if (!m_EventLookup.TryGetValue(typeof(T), out var @event))
            {
                if (raise)
                {
                    throw new EventMissingException($"Event of type {typeof(T)} was never subscribed");
                }
                else
                {
                    return;
                }
            }

            @event.Invoke(sender, args);
        }
        
        /// <summary>
        /// Subscribes to an event of the specified type.
        /// </summary>
        /// <param name="eventType">The type of event to subscribe to.</param>
        /// <param name="name">The name of the event.</param>
        /// <param name="handler">The event handler to be called when the event is raised.</param>
        public void Subscribe(Type eventType, string name, Delegate handler)
        {
            if (!typeof(IEvent).IsAssignableFrom(eventType))
            {
                throw new ArgumentException($"Type {eventType.FullName} does not implement IEvent.");
            }

            if (!m_EventLookup.TryGetValue(eventType, out var @event))
            {
                @event = new EventWrapper(name, eventType);
                m_EventLookup[eventType] = @event;
            }

            @event.AddHandler(handler);
        }

        /// <summary>
        /// Unsubscribes from an event of the specified type.
        /// </summary>
        /// <param name="eventType">The type of event to unsubscribe from.</param>
        /// <param name="name">The name of the event.</param>
        /// <param name="handler">The event handler to be removed.</param>
        /// <exception cref="EventMissingException">Thrown when the event does not exist in the event lookup.</exception>
        public void Unsubscribe(Type eventType, string name, Delegate handler)
        {
            if (!typeof(IEvent).IsAssignableFrom(eventType))
            {
                throw new ArgumentException($"Type {eventType.FullName} does not implement IEvent.");
            }

            if (!m_EventLookup.TryGetValue(eventType, out var @event))
            {
                throw new EventMissingException($"Event of type {eventType.FullName} was never subscribed.");
            }

            @event.RemoveHandler(handler);
        }

        /// <summary>
        /// Raises an event of the specified type.
        /// </summary>
        /// <param name="eventType">The type of event to raise.</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <param name="raise">If true, raises the event; otherwise, does nothing if the event does not exist.</param>
        /// <exception cref="EventMissingException">Thrown when the event does not exist in the event lookup and raise is true.</exception>
        public void Raise(Type eventType, object sender, object args, bool raise = true)
        {
            if (!typeof(IEvent).IsAssignableFrom(eventType))
            {
                throw new ArgumentException($"Type {eventType.FullName} does not implement IEvent.");
            }

            if (!m_EventLookup.TryGetValue(eventType, out var @event))
            {
                if (raise)
                {
                    throw new EventMissingException($"Event of type {eventType.FullName} was never subscribed.");
                }
                else
                {
                    return;
                }
            }

            @event.Invoke(sender, args);
        }


        /// <summary>
        /// Clears all subscribed events from the event lookup.
        /// </summary>
        public void ClearAll()
        {
            m_EventLookup.Clear();
        }

        /// <summary>
        /// Initializes the EventBus instance.
        /// </summary>
        protected override void Initialize()
        {
        }
    }
}
