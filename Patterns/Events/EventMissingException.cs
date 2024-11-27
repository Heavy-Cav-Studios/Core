using System;

namespace HeavyCavStudios.Core.Patterns.Events
{
    /// <summary>
    /// Exception thrown when an event is missing in the event lookup.
    /// </summary>
    public class EventMissingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventMissingException"/> class.
        /// </summary>
        /// <param name="msg">The message describing the exception.</param>
        public EventMissingException(string msg)
            : base(msg)
        {
        }
    }
}
