using System.Text.Json;

namespace BestDealWebApp.Helpers
{
    public static class SerializerHelper
    {
        public static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
