
using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;
namespace SDSFinder.Modules.Services;
using User = SDSFinder.Shared.User;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector)
        => _documentRepository.GetListBy(selector);

    public Task<Document?> GetByFileLocation(string fileLocation)
        => _documentRepository.GetByFileLocation(fileLocation);

    public Task<List<Document>> GetAll()
        => _documentRepository.GetAll();

    public Task<byte[]> GetPdfAsync(string filePath)
        => File.ReadAllBytesAsync($"{filePath}");

    public Task Create(Document document)
        => _documentRepository.Create(document);

    public Task Delete(Document document, User user)
        => _documentRepository.Delete(document, user);


}
