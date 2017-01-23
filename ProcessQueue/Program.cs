using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Newtonsoft.Json;

namespace ProcessQueue
{
    public class CloudAppItem
    {
        public string url { get; set; }
        public string app { get; set; }
        public string email { get; set; }
        public string dbId { get; set; }
        public DateTime started { get; set; }
        public DateTime ended { get; set; }
    }

    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(GetConnectionString());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueueClient clnt = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = clnt.GetQueueReference("beam-queue");

            CloudAppItem item = JsonConvert.DeserializeObject<CloudAppItem>(queue.GetMessage().AsString);
            
        }

        public static string GetConnectionString()
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["StorageConnectionString"];
                if (string.IsNullOrEmpty(connString))
                {
                    return "UseDevelopmentStorage=true";
                }
                else
                {
                    return connString;
                }

            }
            catch (Exception)
            {
                return "UseDevelopmentStorage=true";
            }
        }
    }
}
