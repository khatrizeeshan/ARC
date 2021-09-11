using System.Threading.Tasks;

namespace ARC.Domain
{
    public interface IDocumentService<T> 
        where T : IRequestDocument
    {
        Task<T> CreateAsync(T document);
        Task SendAsync(string documentId);
        Task ResendAsync(string documentId);
        Task<T> GetAsync(string documentId);
    }
}
