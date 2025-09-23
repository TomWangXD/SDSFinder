using System.Linq.Expressions;
using User = SDSFinder.Shared.User;

namespace SDSFinder.Modules.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly IDbContextFactory<SDSFinderContext> _contextFactory;
    public DocumentRepository(IDbContextFactory<SDSFinderContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Document>> GetListBy(Expression<Func<Document, bool>> selector)
    {
        await using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        return await context.Documents.Where(selector).OrderBy(x => x.Id).ToListAsync();
    }

    public async Task<Document?> GetByFileLocation(string fileLocation)
    {
        await using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        return await context.Documents.Where(x => x.FileLocation.Equals(fileLocation)).FirstOrDefaultAsync();
    }

    public async Task<List<Document>> GetAll()
    {
        await using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        return await context.Documents
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task Create(Document document)
    {
        await using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        List<Document> documents = context.Documents
            .Where(x => x.FileName == document.FileName
             && x.FileLocation.ToLower() == document.FileLocation.ToLower()
             && x.IsDeleted == false)
            .ToList();
        if (documents.Count > 0)
        {
            document = documents.First();
            await Update(document, context);
        }
        else
        {
            context.Documents.Add(document);
            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Document document, User user)
    {
        await using SDSFinderContext context = await _contextFactory.CreateDbContextAsync();
        document.IsDeleted = true;
        document.ModifiedDate = DateTime.Now;
        document.ModifiedBy = user.Employee.Id;
        await Update(document, context);
    }

    private static async Task Update(Document document, SDSFinderContext context)
    {
        document.ModifiedDate = DateTime.Now;
        context.Documents.Update(document);
        await context.SaveChangesAsync();
    }

}
