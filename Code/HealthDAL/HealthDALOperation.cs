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
        private DbContextOptions<FileDBContext> _dbContextOptionsFile;
        private readonly DbContextOptions<SubscriberContext> _dbContextOptionsSubscriber;

        public HealthDALOperation(IOptionsSnapshot<Settings> options, 
            DbContextOptions<FileDBContext> dbContextOptionsFile,
            DbContextOptions<SubscriberContext> dbContextOptionsSubscriber)
        {
            dbContextOptionsFile = new DbContextOptionsBuilder<FileDBContext>()
                .UseSqlServer(options.Value.DBConnectionString)
                .Options;
            dbContextOptionsSubscriber = new DbContextOptionsBuilder<SubscriberContext>()
                .UseSqlServer(options.Value.DBConnectionString)
                .Options;

            _dbContextOptionsFile = dbContextOptionsFile;
            _dbContextOptionsSubscriber = dbContextOptionsSubscriber;
        }

        public async Task<bool> InsertFileIntoDBAsync(FileDAL fileDAL)
        {
            using var dbContext = new FileDBContext(_dbContextOptionsFile);
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
            using var dbContext = new FileDBContext(_dbContextOptionsFile);
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
            using var dbContext = new FileDBContext(_dbContextOptionsFile);
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

        public async Task<IEnumerable<FileDAL>> GetSearchedFilesAsync(string searchKey)
        {
            using var dbContext = new FileDBContext(_dbContextOptionsFile);
            try
            {
                var result = await dbContext.Files
                    .Where(f => f.Name.Contains(searchKey))
                    .ToListAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddSubscriptionAsync(SubscriberDAL subscriberDAL)
        {
            using var dbContext = new SubscriberContext(_dbContextOptionsSubscriber);
            try
            {
                dbContext.Subscribers.Add(subscriberDAL);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<FileDAL>> GetSubscribedFilesAsync(string userName)
        {
            using var dbContextSub = new SubscriberContext(_dbContextOptionsSubscriber);
            using var dbContextFile = new FileDBContext(_dbContextOptionsFile);
            try
            {
                userName = userName.ToLower();
                var subscribed = await dbContextSub.Subscribers
                    .Where(sub => sub.Name.Equals(userName))
                    .ToListAsync();
                var subscribedFiles = new List<FileDAL>();

                foreach (var sub in subscribed)
                {
                    var file = dbContextFile.Files
                                    .Single(f => f.DocumentId == sub.DocumentId);
                    subscribedFiles.Add(file);
                }

                return subscribedFiles;
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> RemoveSubscriptionAsync(string documentId)
        {
            using var dbContext = new SubscriberContext(_dbContextOptionsSubscriber);
            try
            {
                var subscriberDAL = dbContext.Subscribers.First(c => c.DocumentId == int.Parse(documentId));
                dbContext.Subscribers.Attach(subscriberDAL);
                dbContext.Subscribers.Remove(subscriberDAL);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
