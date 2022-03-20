using HealthDAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HealthBAL
{
    public class HealthBALOperation : IHealthBALOperation
    {
        private readonly IHealthDALOperation _healthDALOperation;

        public HealthBALOperation(IHealthDALOperation healthDALOperation)
        {
            _healthDALOperation = healthDALOperation ?? throw new ArgumentNullException(nameof(healthDALOperation));
        }

        public async Task<bool> AddSubscription(string documentId, string username)
        {
            var subscriptionDAL = new SubscriberDAL
            {
                DocumentId = int.Parse(documentId),
                Name = username.ToLower(),
                SubscriberId = 0
            };

            return await _healthDALOperation.AddSubscriptionAsync(subscriptionDAL);
        }

        public async Task<byte[]> GetFileAsync(string documentId)
        {
            return await _healthDALOperation.GetFileAsync(documentId);
        }

        public async Task<IEnumerable<FileBAL>> GetHealthJournals(string userName)
        {
            var responseBAL = new List<FileBAL>();
            var responseDAL = await _healthDALOperation.GetFilesAsync(userName);
            foreach (var dal in responseDAL)
            {
                responseBAL.Add(new FileBAL
                {
                    CreatedBy = dal.CreatedBy,
                    CreatedOn = dal.CreatedOn,
                    DataFiles = dal.DataFiles,
                    DocumentId = dal.DocumentId,
                    FileType = dal.FileType,
                    Name = dal.Name
                });
            }
            return responseBAL;
        }

        public async Task<IEnumerable<FileBAL>> GetHealthJournalsBySearchKey(string searchKey)
        {
            var responseBAL = new List<FileBAL>();
            var responseDAL = await _healthDALOperation.GetSearchedFilesAsync(searchKey);
            foreach (var dal in responseDAL)
            {
                responseBAL.Add(new FileBAL
                {
                    CreatedBy = dal.CreatedBy,
                    CreatedOn = dal.CreatedOn,
                    DataFiles = dal.DataFiles,
                    DocumentId = dal.DocumentId,
                    FileType = dal.FileType,
                    Name = dal.Name
                });
            }
            return responseBAL;
        }

        public async Task<IEnumerable<FileBAL>> GetSubscribedHealthJournals(string userName)
        {
            var responseBAL = new List<FileBAL>();
            var responseDAL = await _healthDALOperation.GetSubscribedFilesAsync(userName);
            foreach (var dal in responseDAL)
            {
                responseBAL.Add(new FileBAL
                {
                    CreatedBy = dal.CreatedBy,
                    CreatedOn = dal.CreatedOn,
                    DataFiles = dal.DataFiles,
                    DocumentId = dal.DocumentId,
                    FileType = dal.FileType,
                    Name = dal.Name
                });
            }
            return responseBAL;
        }

        public async Task<bool> InsertHealthJournals(IFormFile files, string userName)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    var objfiles = new FileDAL()
                    {
                        DocumentId = 0,
                        Name = fileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now,
                        CreatedBy = userName.ToLowerInvariant()
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }
                    await _healthDALOperation.InsertFileIntoDBAsync(objfiles);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveSubscription(string documentId)
        {
            return await _healthDALOperation.RemoveSubscriptionAsync(documentId);
        }
    }
}
