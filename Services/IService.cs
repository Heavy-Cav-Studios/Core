namespace HeavyCavStudios.Core.Services
{
    /// <summary>
    /// Represents a basic service interface that all services should implement in order to be managed
    /// by the <see cref="ServiceRegistry"/>.
    /// 
    /// The <see cref="Initialize"/> method is intended to handle any setup or initialization logic
    /// required by the service. This allows for a consistent way to prepare all services
    /// when the application starts or at a specific point in the lifecycle.
    /// 
    /// Example Usage:
    /// 
    /// Implementing a Service:
    /// <code>
    /// public class AudioService : IService
    /// {
    ///     public void Initialize()
    ///     {
    ///         Debug.Log("AudioService initialized.");
    ///     }
    ///     
    ///     public void PlaySound(AudioClip clip)
    ///     {
    ///         Debug.Log("Playing sound.");
    ///     }
    /// }
    /// </code>
    /// 
    /// Registering a Service with the ServiceRegistry:
    /// <code>
    /// ServiceRegistry.Register<IAudioService>(new AudioService());
    /// </code>
    /// 
    /// Initializing All Services:
    /// <code>
    /// ServiceRegistry.InitializeAll();
    /// </code>
    /// 
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Handles the initialization of the service.
        /// This method is intended to be called once during the service lifecycle, typically
        /// after it is registered with the <see cref="ServiceRegistry"/>.
        /// </summary>
        void Initialize();
    }
}
