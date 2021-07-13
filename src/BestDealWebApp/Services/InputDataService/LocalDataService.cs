using System.IO;
using System.Text.Json;
using BestDealWebApp.Helpers;
using BestDealWebApp.Models;

namespace BestDealWebApp.Services.InputDataService
{
    public class LocalDataService : IDataService
    {
        public QuoteRequest GetRequest() =>
            // (GK) sample only, the file location should be injected/set in config
            JsonSerializer.Deserialize<QuoteRequest>(File.ReadAllText("sampleInput.json"), SerializerHelper.SerializerOptions);
    }
}
