using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthDAL
{
    public interface IHealthDALOperation
    {
        Task<bool> InsertFileIntoDBAsync(FileDAL fileDAL);
        Task<IEnumerable<FileDAL>> GetFilesAsync(string userName);
        Task<IEnumerable<FileDAL>> GetSearchedFilesAsync(string searchKey);
        Task<byte[]> GetFileAsync(string documentId);
        Task<bool> AddSubscriptionAsync(SubscriberDAL subscriberDAL);
        Task<bool> RemoveSubscriptionAsync(string documentId);
        Task<IEnumerable<FileDAL>> GetSubscribedFilesAsync(string userName);
    }
}
