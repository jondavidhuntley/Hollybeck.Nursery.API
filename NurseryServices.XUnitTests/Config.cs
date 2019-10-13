using Microsoft.Extensions.Configuration;

namespace NurseryServices.XUnitTests
{
    public static class Config
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            return config;
        }
    }
}