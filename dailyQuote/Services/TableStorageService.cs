using Azure;
using Azure.Data.Tables;
using dailyQuote.Enums;
using dailyQuote.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace dailyQuote.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string TABLE_NAME_QUOTE = "dailyquotestorage";
        private const string TABLE_NAME_SUBSCRIBER = "dailyquotesubscribersstorage";
        private readonly IConfiguration _configuration;

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Quote> GetRandomQuoteAsync()
        {
           var tableClient = await GetTableClient(TABLE_NAME_QUOTE);
            Random rnd = new Random();
            int number = rnd.Next(1, 10);
            return await tableClient.GetEntityAsync<Quote>(nameof(Entities.quote), number.ToString());
        }

        public async Task SendEmailsAsync()
        {
            var tableClient = await GetTableClient(TABLE_NAME_SUBSCRIBER);
            AsyncPageable<Subscriber> queryResults = tableClient.QueryAsync<Subscriber>(ent => ent.PartitionKey.Equals(nameof(Entities.subscriber)));
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuration["Sender"], _configuration["Password"]),
                EnableSsl = true
            };
            
            var message = new MailMessage
            {
                From = new MailAddress(_configuration["Sender"]),
                Subject = "Daily quote",
            };

            string continuationToken = null;
            await foreach (Page<Subscriber> page in queryResults.AsPages(continuationToken, pageSizeHint: 100))
            {
                message.Body = (await GetRandomQuoteAsync()).Text;

                foreach (Subscriber subscriber in page.Values)
                {
                    message.To.Clear();
                    message.To.Add(new MailAddress(subscriber.Email));
                    smtpClient.Send(message);
                }

                // The continuation token that can be used in AsPages call to resume enumeration
                continuationToken = page.ContinuationToken;
            }
        }

        private async  Task<TableClient> GetTableClient(string tableName)
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);
            var tableClient = serviceClient.GetTableClient(tableName);
            _ = await tableClient.CreateIfNotExistsAsync();

            return tableClient;
        }
    }
}
