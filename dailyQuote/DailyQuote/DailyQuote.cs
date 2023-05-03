using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace dailyQuote.DailyQuote
{
    public class DailyQuote
    {
        private readonly IDailyQuoteRunner _runner;

        public DailyQuote(IDailyQuoteRunner runner)
        {
            _runner = runner;
        }

        [FunctionName(nameof(DailyQuote))]
        public async Task Run([TimerTrigger("0 30 9 * * *")] TimerInfo myTimer, ILogger log)
        {
            await _runner.RunAsync(log);
        }
    }
}
