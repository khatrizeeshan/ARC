using System.Threading.Tasks;

namespace ARC.Domain
{
    public interface IDocumentService<T> 
        where T : IRequestDocument
    {
        Task<T> CreateAsync(T document);
        Task<T> SendAsync(T documentId);
        Task<T> ResendAsync(T documentId);
        Task<T> GetAsync(string documentId);
    }
}
