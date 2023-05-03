using Azure;
using Azure.Data.Tables;
using System;

namespace dailyQuote.Models
{
    public class Quote : ITableEntity
    {
        public string Text { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
