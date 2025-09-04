namespace SDSFinder.Modules.Services
{
    public interface ILanguageService
    {
        Task<(string lang1, string lang2, string lang3, string lang4, string lang5)> GetGHSLanguagesAsync(string job, string site, CancellationToken ct = default);
        string GetLanguage(string fileName);
        string GetGHSLanguage((string lang1, string lang2, string lang3, string lang4, string lang5) ghsLanguages);
        string GetLanguageFromFormattedString(string formattedLanguage);
        string GetCountryBeforeParenthesis(string input);
        string GetLanguageCodeFromFormattedString(string formattedLanguage);
        Task<(string? SDSLang, string? SDSRegion)> GetLanguageFromCountryLanguageTable(string lang);
    }
}
