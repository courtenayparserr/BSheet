using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class TransactionOperations : BaseOperations
    {
        public static void BulkAddTransactions(List<QuoteEntity> entities)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(GetConnectionString());

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("transactions");

            // Create the batch operation.
            TableBatchOperation batchOperation = new TableBatchOperation();

            foreach(QuoteEntity ent in entities)
            {
                batchOperation.Insert(ent);
            }
            
            // Execute the batch operation.
            table.ExecuteBatch(batchOperation);
        }

        public static void AddNewTransaction(string accountId, string transId, string time, string _pendingTransaction,
            string _reference_number, double amount, string category, string category_id, DateTimeOffset date, string meta,
            string meta_location_address, string meta_location_city, float meta_location_coordinates_lat, float meta_location_coordinates_lon,
            string meta_location_state, string name, string offset, bool pending, string score, double score_location_address,
            double score_location_city, double score_location_state, double score_name, string type, string type_primary)
        {
            try
            {
                var account = CloudStorageAccount.Parse(GetConnectionString());
                // You could use local development storage 
                //   account = CloudStorageAccount.DevelopmentStorageAccount; 
                // Create the table client. 
                CloudTableClient tableClient = account.CreateCloudTableClient();

                // Create the table if it doesn't exist. 
                CloudTable table = tableClient.GetTableReference("transactions");
                table.CreateIfNotExistsAsync();
                QuoteEntity quoteEntity = new QuoteEntity(transId, accountId, offset, time);                
                quoteEntity._pendingTransaction = _pendingTransaction;
                quoteEntity._reference_number = _reference_number;
                quoteEntity.amount = amount;
                quoteEntity.category = category;
                quoteEntity.category_id = category_id;
                quoteEntity.date = date;
                quoteEntity.meta = meta;
                quoteEntity.meta_location_address = meta_location_address;
                quoteEntity.meta_location_city = meta_location_city;
                quoteEntity.meta_location_coordinates_lat = meta_location_coordinates_lat;
                quoteEntity.meta_location_coordinates_lon = meta_location_coordinates_lon;
                quoteEntity.meta_location_state = meta_location_state;
                quoteEntity.name = name;
                quoteEntity.pending = pending;
                quoteEntity.score = score;
                quoteEntity.score_location_address = score_location_address;
                quoteEntity.score_location_city = score_location_city;
                quoteEntity.score_location_state = score_location_state;
                quoteEntity.score_name = score_name;
                quoteEntity.type = type;
                quoteEntity.type_primary = type_primary;
                quoteEntity.processed = false;

                // Create the TableOperation that inserts the customer entity. 
                TableOperation insertOperation = TableOperation.Insert(quoteEntity);

                // Execute the insert operation. 
                table.Execute(insertOperation);
                
            }
            catch (Exception x)
            { }

        }

        public class QuoteEntity : TableEntity
        {
            private DateTimeOffset FromUnixTime(long unixTime, int offset)
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(unixTime));

                TimeSpan ts = new TimeSpan(0, offset, 0);
                epoch = epoch.AddMinutes(offset);
                return new DateTimeOffset(epoch.Year, epoch.Month, epoch.Day, epoch.Hour, epoch.Minute, epoch.Second, ts);
            }

            private DateTime FromUnixTimeInDate(long unixTime, int offset)
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(unixTime));

                TimeSpan ts = new TimeSpan(0, offset, 0);
                epoch = epoch.AddMinutes(offset);
                return new DateTime(epoch.Year, epoch.Month, epoch.Day, epoch.Hour, epoch.Minute, epoch.Second);
            }

            public QuoteEntity(string transactionId, string accountId, string offset, string time)
            {
                this.PartitionKey = accountId;
                this.RowKey = transactionId;
                this.Timestamp = FromUnixTime(Convert.ToInt64(time), Convert.ToInt32(offset));
            }
            public QuoteEntity() { }
            public string _reference_number { get; set; }
            public string _pendingTransaction { get; set; }
            public double amount { get; set; }
            public DateTimeOffset date { get; set; }
            public string name { get; set; }
            public string meta { get; set; }
            public string meta_location_address { get; set; }
            public string meta_location_city { get; set; }
            public string meta_location_state { get; set; }
            public string meta_location_zip { get; set; }
            public float meta_location_coordinates_lat { get; set; }
            public float meta_location_coordinates_lon { get; set; }
            public bool pending { get; set; }
            public bool processed { get; set; }
            public string type { get; set; }
            public string type_primary { get; set; }
            public string category { get; set; }
            public string category_id { get; set; }
            public string score { get; set; }
            public double score_location_address { get; set; }
            public double score_location_city { get; set; }
            public double score_location_state { get; set; }
            public double score_name { get; set; }
        }
    }
}
