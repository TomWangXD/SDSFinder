using System.Linq.Expressions;
namespace SDSFinder.Modules.Services;

public interface IDocumentService
{
    Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector);
    Task<Document?> GetByFileLocation(string fileLocation);
    Task<byte[]> GetPdfAsync(string filePath);
    Task<List<Document>> GetAll();
    Task Delete(Document document);
    Task Create(Document document);
}
