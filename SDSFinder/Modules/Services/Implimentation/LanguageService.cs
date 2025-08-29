using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using SDSFinder.EFContexts;
using SDSFinder.EFModels;
using SDSFinder.Modules.Repositories;
using System.Data;

namespace SDSFinder.Modules.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IDbContextFactory<IndAppContext> _contextFactory;

        public LanguageService(IDbContextFactory<IndAppContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<(string lang1, string lang2, string lang3, string lang4, string lang5)>
        GetGHSLanguagesAsync( string job, string site, CancellationToken ct = default)
        {
            using IndAppContext context = await _contextFactory.CreateDbContextAsync();
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
                    // fully-qualify to avoid default-DB confusion
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
        public string? GetGHSLanguage((string lang1, string lang2, string lang3, string lang4, string lang5) ghslanguages)
        {
            string lastLang = new[] { ghslanguages.lang1, ghslanguages.lang2, ghslanguages.lang3, ghslanguages.lang4, ghslanguages.lang5 }
                .LastOrDefault(l => !string.IsNullOrWhiteSpace(l));
            return lastLang;
        }

        public async Task<(string? SDSLang,string? SDSRegion)> GetLanguageFromCountryLanguageTable(string lang)
        {
            using IndAppContext context = await _contextFactory.CreateDbContextAsync();

            var country = GetCountryBeforeParenthesis(lang);
            var languageCode = GetLanguageFromFormattedString(lang);
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

        public string GetLanguage(string fileName)
        {
            string languageCode = !String.IsNullOrWhiteSpace(fileName) && fileName.Length >= 2 ?
            fileName.Substring(0, 2) : fileName;
            var result = languageCode switch
            {
                "cs" => "",
                "de" => "German",
                "en" => "English",
                "es" => "Spanish",
                "et" => "",
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
            return result;
        }
        public string GetLanguageFromFormattedString(string formattedLanguage)
        {
            if (string.IsNullOrWhiteSpace(formattedLanguage))
                return "Unsupported language";

            // Find the code inside parentheses
            int start = formattedLanguage.IndexOf('(');
            int end = formattedLanguage.IndexOf(')');

            if (start >= 0 && end > start)
            {
                string code = formattedLanguage.Substring(start + 1, end - start - 1);
                return GetLanguage(code); // use your existing switch method
            }

            return "Unsupported language"; // fallback if format is invalid
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

            return input.Trim(); // return full string if no '(' found
        }
    }
}
