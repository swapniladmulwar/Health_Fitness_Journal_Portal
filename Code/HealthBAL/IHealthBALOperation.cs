using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthBAL
{
    public interface IHealthBALOperation
    {
        Task<bool> InsertHealthJournals(IFormFile files, string userName);
        Task<IEnumerable<FileBAL>> GetHealthJournals(string userName);
        Task<IEnumerable<FileBAL>> GetHealthJournalsBySearchKey(string searchKey);
        Task<byte[]> GetFileAsync(string documentId);
        Task<bool> AddSubscription(string documentId, string userName);
        Task<IEnumerable<FileBAL>> GetSubscribedHealthJournals(string userName);
        Task<bool> RemoveSubscription(string documentId);
    }
}
