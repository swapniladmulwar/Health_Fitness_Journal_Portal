using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthBAL
{
    public interface IHealthBALOperation
    {
        Task<bool> InsertHealthJournals(IFormFile files, string userName);
        Task<IEnumerable<FileBAL>> GetHealthJournals(string userName);
        Task<byte[]> GetFileAsync(string documentId);
    }
}
