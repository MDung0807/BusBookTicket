using Microsoft.Extensions.Configuration;

namespace BusBookTicket.Core.Common.GetConfigs
{
    public class ConfigApp : IConfigApp
    {
        private readonly IConfiguration _configuration;
        private static ConfigApp _instance;
        private static readonly object _lock = new object();

        // Private constructor to prevent direct instantiation
        public ConfigApp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Public property to get the singleton instance
        public static ConfigApp Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("ConfigApp is not initialized. Call Initialize(IConfiguration) method first.");
                }
                return _instance;
            }
        }

        // Method to initialize the singleton instance
        public static void Initialize(IConfiguration configuration)
        {
            if (_instance != null) return;
            lock (_lock)
            {
                _instance ??= new ConfigApp(configuration);
            }
        }

        // Method to get configuration value
        public string GetConfig(string path)
        {
            return _configuration[path];
        }
    }
}