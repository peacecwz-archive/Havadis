using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Havadis.API.Helpers
{
    public class StorageHelper
    {
        public async static Task<string> GetFile(string filename)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                Startup.WebConfiguration.GetConnectionString("StorageConStr"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("havadis");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

            return await blockBlob.DownloadTextAsync();
        }
    }
}
