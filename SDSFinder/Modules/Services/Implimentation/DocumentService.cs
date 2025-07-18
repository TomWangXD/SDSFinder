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
    public async Task<List<Document>> GetAll()
    {
        using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        var result = await _documentRepository.GetAll(context);
        return result;
    }
    public async Task<byte[]> GetPdfAsync(string filePath)
    {   

        return await File.ReadAllBytesAsync($"{filePath}");
    }

    public async Task Create(Document document)
    {
        using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        await _documentRepository.Create(document, context);
    }
    public async Task Update(Document document)
    {
        using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        await _documentRepository.Update(document, context);
    }
    public async Task Delete(Document document, User user)
    {
        using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        await _documentRepository.Delete(document, context, user);
    }


}
