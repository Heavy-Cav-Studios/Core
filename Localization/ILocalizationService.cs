namespace HeavyCavStudios.Core.Localization
{
    /// <summary>
    /// Represents a localization service interface designed to provide localized strings
    /// based on specific keys. Implementations of this interface can handle
    /// different localization systems, allowing for flexible integration of localization
    /// frameworks or custom solutions.
    /// 
    /// This interface is intended to be implemented by any localization service used
    /// in the application, enabling a consistent API for retrieving localized text.
    /// 
    /// Example Usage:
    /// 
    /// Implementing a Custom Localization Service:
    /// <code>
    /// public class CustomLocalizationService : ILocalizationService
    /// {
    ///     private Dictionary<string, string> m_LocalizationData = new Dictionary<string, string>()
    ///     {
    ///         { "hello", "Hello, World!" },
    ///         { "welcome", "Welcome to Heavy Cav Studios!" }
    ///     };
    /// 
    ///     public string GetLocalizedString(string key)
    ///     {
    ///         return m_LocalizationData.TryGetValue(key, out var value) ? value : $"[Missing: {key}]";
    ///     }
    /// }
    /// </code>
    /// 
    /// Registering the Service:
    /// <code>
    /// ServiceRegistry.Register<ILocalizationService>(new CustomLocalizationService());
    /// </code>
    /// 
    /// Retrieving Localized Text:
    /// <code>
    /// var localizationService = ServiceRegistry.Get<ILocalizationService>();
    /// string welcomeMessage = localizationService?.GetLocalizedString("welcome");
    /// Debug.Log(welcomeMessage);
    /// </code>
    /// 
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Retrieves the localized string for the specified key.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <returns>
        /// The localized string corresponding to the provided key.
        /// If the key is not found, the implementation may return a default value
        /// or indicate that the key is missing.
        /// </returns>
        string GetLocalizedString(string key);
    }
}
