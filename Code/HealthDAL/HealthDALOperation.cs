using HealthConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthDAL
{
    public class HealthDALOperation : IHealthDALOperation
    {
        private DbContextOptions<FileDBContext> _dbContextOptions;

        public HealthDALOperation(IOptionsSnapshot<Settings> options, DbContextOptions<FileDBContext> dbContextOptions)
        {
            dbContextOptions = new DbContextOptionsBuilder<FileDBContext>()
                .UseSqlServer(options.Value.DBConnectionString)
                .Options;

            _dbContextOptions = dbContextOptions;
        }

        public async Task<bool> InsertFileIntoDBAsync(FileDAL fileDAL)
        {
            using var dbContext = new FileDBContext(_dbContextOptions);
            try
            {
                dbContext.Files.Add(fileDAL);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<FileDAL>> GetFilesAsync(string userName)
        {
            using var dbContext = new FileDBContext(_dbContextOptions);
            try
            {
                userName = userName.ToLower();
                var result = await dbContext.Files
                    .Where(f => f.CreatedBy.Equals(userName))
                    .ToListAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public async Task<byte[]> GetFileAsync(string documentId)
        {
            using var dbContext = new FileDBContext(_dbContextOptions);
            try
            {
                int id = int.Parse(documentId);
                var result = dbContext.Files
                    .Where(f => f.DocumentId.Equals(id))
                    .FirstOrDefault();
                return await Task.FromResult(result.DataFiles);
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
