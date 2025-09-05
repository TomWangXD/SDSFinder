using Dapper;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace SDSFinder.Modules.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly IDbContextFactory<IndAppContext> _contextFactory;

    public LanguageRepository(IDbContextFactory<IndAppContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<(string lang1, string lang2, string lang3, string lang4, string lang5)> GetGHSLanguagesAsync(string job, string site, CancellationToken ct = default)
    {
        await using IndAppContext context = await _contextFactory.CreateDbContextAsync(ct);
        var conn = context.Database.GetDbConnection();
        var tx = context.Database.CurrentTransaction?.GetDbTransaction();

        var p = new DynamicParameters();
        p.Add("@job", job, DbType.String, ParameterDirection.Input);
        p.Add("@site", site, DbType.String, ParameterDirection.Input);
        p.Add("@lang1", dbType: DbType.String, size: 40, direction: ParameterDirection.Output);
        p.Add("@lang2", dbType: DbType.String, size: 40, direction: ParameterDirection.Output);
        p.Add("@lang3", dbType: DbType.String, size: 40, direction: ParameterDirection.Output);
        p.Add("@lang4", dbType: DbType.String, size: 40, direction: ParameterDirection.Output);
        p.Add("@lang5", dbType: DbType.String, size: 40, direction: ParameterDirection.Output);

        var openedHere = false;
        if (conn.State != ConnectionState.Open) { await conn.OpenAsync(ct); openedHere = true; }
        try
        {
            await conn.ExecuteAsync(
                "[IND_App].[dbo].[IND_GHSSetLanguageSelection]",
                p,
                commandType: CommandType.StoredProcedure,
                transaction: tx
            );

            return (
                p.Get<string>("@lang1"),
                p.Get<string>("@lang2"),
                p.Get<string>("@lang3"),
                p.Get<string>("@lang4"),
                p.Get<string>("@lang5")
            );
        }
        finally
        {
            if (openedHere) await conn.CloseAsync();
        }
    }

    public async Task<(string? SDSLang, string? SDSRegion)> GetLanguageFromCountryLanguageTable(string lang)
    {
        await using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        var country = GetCountryBeforeParenthesis(lang);
        var languageCode = GetLanguageCodeFromFormattedString(lang);
        IndCountryLanguage? countryLanguage = await context.IndCountryLanguages.Where(x => x.IndCountry == country && x.IndLanguageCode == languageCode).FirstOrDefaultAsync();
        if (countryLanguage != null)
        {
            return (countryLanguage.IndSdslanguage, countryLanguage.IndSdsregion);
        }
        else
        {
            return (null, null);
        }
    }

    private static string GetCountryBeforeParenthesis(string input)
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

    private static string GetLanguageCodeFromFormattedString(string formattedLanguage)
    {
        if (string.IsNullOrWhiteSpace(formattedLanguage))
            return string.Empty;
        int start = formattedLanguage.IndexOf('(');
        int end = formattedLanguage.IndexOf(')');
        if (start >= 0 && end > start)
        {
            return formattedLanguage.Substring(start + 1, end - start - 1);
        }
        return string.Empty;
    }
}
