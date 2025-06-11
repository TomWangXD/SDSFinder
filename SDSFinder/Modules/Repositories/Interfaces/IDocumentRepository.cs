using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories;

public interface IDocumentRepository
{
    Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector, SDSFinderContext context);
    Task<Document?> GetByFileLocation(string fileLocation, SDSFinderContext context);
}
