using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Havadis.WebJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        async static Task Run()
        {
            using (HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://havadis.azurewebsites.net/")
            })
            {
                var request = await client.GetAsync("/api/hot");
                if (!request.IsSuccessStatusCode) return;
                WriteToCloud(await request.Content?.ReadAsStringAsync());
            }
        }

        static void WriteToCloud(string json)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    ConfigurationManager.AppSettings["StorageConStr"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("havadis");

            container.CreateIfNotExists();
            container.SetPermissions(
    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{DateTime.Now.ToString("ddMMyyyy")}.json");

            blockBlob.UploadText(json);
        }
    }
}
