namespace Core.Patterns.Cloning
{
    /// <summary>
    /// Represents a cloneable interface for creating deep copies of objects.
    /// </summary>
    /// <typeparam name="T">The type of object to be cloned.</typeparam>
    public interface ICloneable<T>
    {
        /// <summary>
        /// Creates a new instance of the object with the same values as the current instance.
        /// </summary>
        /// <returns>A clone of the current object.</returns>
        T Clone();
    }
}
