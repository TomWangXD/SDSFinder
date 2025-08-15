using System.Linq.Expressions;
using User = SDSFinder.Shared.User;
namespace SDSFinder.Modules.Services;

public interface IDocumentService
{
    Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector);
    Task<Document?> GetByFileLocation(string fileLocation);
    Task<byte[]> GetPdfAsync(string filePath);
    Task<List<Document>> GetAll();
    Task Create(Document document);
    Task Update(Document document);
    Task Delete(Document document, User user);
}
