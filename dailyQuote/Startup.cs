using dailyQuote.DailyQuote;
using dailyQuote.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(dailyQuote.Startup))]
namespace dailyQuote
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IDailyQuoteRunner, DailyQuoteRunner>();
            builder.Services.AddScoped<ITableStorageService, TableStorageService>();
        }
    }
}
