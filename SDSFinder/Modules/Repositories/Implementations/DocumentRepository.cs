using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace SDSFinder.Modules.Repositories;

public class DocumentRepository : IDocumentRepository
{
    public async Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector, SDSFinderContext context)
    {
        List<Document> result = await context.Documents.Where(selector).OrderBy(x => x.Id).ToListAsync();
        
        return result;

    }
    public async Task<Document?> GetByFileLocation(string fileLocation, SDSFinderContext context)
    {
        Document? result = await context.Documents.Where(x => x.FileLocation.Equals(fileLocation)).FirstOrDefaultAsync();
        return result;
    }
    public async Task<List<Document>> GetAll(SDSFinderContext context)
    {
        List<Document> result = await context.Documents
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Id)
            .ToListAsync();
        return result;
    }

}
