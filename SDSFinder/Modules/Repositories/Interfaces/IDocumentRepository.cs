using System.Linq.Expressions;
using User = SDSFinder.Shared.User;

namespace SDSFinder.Modules.Repositories;

public interface IDocumentRepository
{
    Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector, SDSFinderContext context);
    Task<Document?> GetByFileLocation(string fileLocation, SDSFinderContext context);
    Task<List<Document>> GetAll(SDSFinderContext context);
    Task Create(Document document, SDSFinderContext context);
    Task Update(Document document, SDSFinderContext context);
    Task Delete(Document document, SDSFinderContext context, User user);
}
