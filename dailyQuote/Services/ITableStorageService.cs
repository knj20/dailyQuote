using System.Threading.Tasks;
using dailyQuote.Models;

namespace dailyQuote.Services
{
    public interface ITableStorageService
    {
        Task<Quote> GetRandomQuoteAsync();
        Task SendEmailsAsync();
    }
}
