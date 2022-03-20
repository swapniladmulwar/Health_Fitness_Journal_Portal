using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthDAL
{
    public interface IHealthDALOperation
    {
        Task<bool> InsertFileIntoDBAsync(FileDAL fileDAL);
        Task<IEnumerable<FileDAL>> GetFilesAsync(string userNamee);
        Task<byte[]> GetFileAsync(string documentId);
    }
}
