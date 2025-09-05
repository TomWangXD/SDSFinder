using SDSFinder.Modules.Repositories;
using System.Linq;
using System.Threading;

namespace SDSFinder.Modules.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public Task<(string lang1, string lang2, string lang3, string lang4, string lang5)> GetGHSLanguagesAsync(string job, string site, CancellationToken ct = default)
            => _languageRepository.GetGHSLanguagesAsync(job, site, ct);

        public string? GetGHSLanguage((string lang1, string lang2, string lang3, string lang4, string lang5) ghslanguages)
        {
            return new[] { ghslanguages.lang1, ghslanguages.lang2, ghslanguages.lang3, ghslanguages.lang4, ghslanguages.lang5 }
                .LastOrDefault(l => !string.IsNullOrWhiteSpace(l));
        }

        public Task<(string? SDSLang, string? SDSRegion)> GetLanguageFromCountryLanguageTable(string lang)
        => _languageRepository.GetLanguageFromCountryLanguageTable(lang);

        public string GetLanguage(string fileName)
        {
            string languageCode = !string.IsNullOrWhiteSpace(fileName) && fileName.Length >= 2 ?
                fileName.Substring(0, 2) : fileName;
            return languageCode switch
            { 
                "cz" => "Czech",
                "ko" => "Korean",
                "de" => "German",
                "en" => "English",
                "es" => "Spanish",
                "fi" => "Finnish",
                "fr" => "French",
                "hu" => "Hungarian",
                "it" => "Italian",
                "ms" => "Malay",
                "no" => "Norwegian",
                "pl" => "Polish",
                "pt" => "Portuguese",
                "ro" => "Romanian",
                "ru" => "Russian",
                "sv" => "Swedish",
                "zf" => "Chinese (Simplified)",
                "zh" => "Chinese",
                _ => "Unsupported language"
            };
        }
        public string GetLanguageFromFormattedString(string formattedLanguage)
        {
            if (string.IsNullOrWhiteSpace(formattedLanguage))
                return "Unsupported language";
            int start = formattedLanguage.IndexOf('(');
            int end = formattedLanguage.IndexOf(')');
            if (start >= 0 && end > start)
            {
                string code = formattedLanguage.Substring(start + 1, end - start - 1);
                return GetLanguage(code);
            }
            return "Unsupported language";
        }

        public string GetLanguageCodeFromFormattedString(string formattedLanguage)
        {
            if (string.IsNullOrWhiteSpace(formattedLanguage))
                return "Unsupported language";
            int start = formattedLanguage.IndexOf('(');
            int end = formattedLanguage.IndexOf(')');
            if (start >= 0 && end > start)
            {
                string code = formattedLanguage.Substring(start + 1, end - start - 1);
                return code;
            }
            return "Unsupported language";
        }

        public string GetCountryBeforeParenthesis(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            int index = input.IndexOf('(');
            if (index > 0)
            {
                return input.Substring(0, index).Trim();
            }
            return input.Trim();
        }
    }
}
