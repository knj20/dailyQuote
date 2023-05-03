using dailyQuote.Services;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace dailyQuote.DailyQuote
{
    public class DailyQuoteRunner : IDailyQuoteRunner
    {
        private readonly ITableStorageService _tableStorageService;
        public DailyQuoteRunner(ITableStorageService tableStorageService)
        {
            _tableStorageService = tableStorageService;
        }
        public async Task RunAsync(ILogger logger)
        {
            try 
            {
                await _tableStorageService.SendEmailsAsync();
            }
            catch(Exception ex) 
            {
                logger.LogError($"error : {ex.Message}");
            }
            
        }
    }
}
