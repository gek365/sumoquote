namespace BestDealWebApp.Services
{
    public abstract class SettingsBase
    {
        public string ApiKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ApiEndpoint { get; set; }
    }
}
