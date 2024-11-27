using System;

namespace HeavyCavStudios.Core.Patterns.Events
{
    /// <summary>
    /// Exception thrown when there is a type mismatch during event operations.
    /// </summary>
    public class TypeMismatchException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMismatchException"/> class.
        /// </summary>
        /// <param name="msg">The message describing the exception.</param>
        public TypeMismatchException(string msg) : base(msg)
        {
        }
    }
}
