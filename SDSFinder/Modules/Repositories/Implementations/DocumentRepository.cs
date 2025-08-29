using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using User = SDSFinder.Shared.User;

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
    public async Task Create(Document document, SDSFinderContext context)
    {
        List<Document> documents = context.Documents.Where(x => x.FileName == document.FileName && x.IsDeleted == false).ToList();

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
    public async Task Update(Document document, SDSFinderContext context)
    {
        document.ModifiedDate = DateTime.Now;   
        
        context.Documents.Update(document);
        await context.SaveChangesAsync();
    }
    public async Task Delete(Document document, SDSFinderContext context, User user)
    {
        document.IsDeleted = true;
        document.ModifiedDate = DateTime.Now;
        document.ModifiedBy = user.Employee.Id;
        await Update(document, context);
    }
    
}
