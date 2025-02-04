namespace HeavyCavStudios.Core.Localization
{
    public interface ILocalizationProvider
    {
        /// <summary>
        /// Gets the localized string for the specified key.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <returns>The localized string.</returns>
        string GetLocalizedString(string key);
    }
}
