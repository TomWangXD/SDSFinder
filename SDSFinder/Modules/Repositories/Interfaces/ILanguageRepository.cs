using System.Data;
using System.Threading;

namespace SDSFinder.Modules.Repositories;

public interface ILanguageRepository
{
    Task<(string lang1, string lang2, string lang3, string lang4, string lang5)> GetGHSLanguagesAsync(string job, string site, CancellationToken ct = default);
    Task<(string? SDSLang, string? SDSRegion)> GetLanguageFromCountryLanguageTable(string lang);
}
