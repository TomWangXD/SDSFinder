using DevExpress.Blazor.Internal.Grid;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto;
using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;
namespace SDSFinder.Modules.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDbContextFactory<SDSFinderContext> _contextFactory;
    private readonly IConfiguration _configuration;

    public DocumentService(IDocumentRepository documentRepository, IDbContextFactory<SDSFinderContext> contextFactory, IConfiguration configuration )
    {
        _documentRepository = documentRepository;
        _contextFactory = contextFactory;
        _configuration = configuration;
    }


    public async Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector)
    {
        using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        List<Document> result = await _documentRepository.GetListBy(selector, context);
        return result;
    }

    public async Task<Document?> GetByFileLocation(string fileLocation)
    {
        using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        Document? result = await _documentRepository.GetByFileLocation(fileLocation, context);
        return result;
    }
    public async Task<byte[]> GetPdfAsync(string filename)
    {

        return await File.ReadAllBytesAsync($"{_configuration["ResourcePath"]}\\{filename}.pdf");
    }


}
