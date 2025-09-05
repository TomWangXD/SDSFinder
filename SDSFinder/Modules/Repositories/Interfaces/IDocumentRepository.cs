using System.Linq.Expressions;
using User = SDSFinder.Shared.User;

namespace SDSFinder.Modules.Repositories;

public interface IDocumentRepository
{
    Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector);
    Task<Document?> GetByFileLocation(string fileLocation);
    Task<List<Document>> GetAll();
    Task Create(Document document);
    Task Delete(Document document, User user);
}
