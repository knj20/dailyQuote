using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace dailyQuote.DailyQuote
{
    public interface IDailyQuoteRunner
    {
        Task RunAsync(ILogger log);
    }
}
