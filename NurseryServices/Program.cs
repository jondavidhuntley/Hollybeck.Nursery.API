using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace NurseryServices
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            // BuildWebHost(args).Run();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            string portKey = "Fabric_Endpoint_NurseryServiceTypeEndpoint";

            Configuration = builder.Build();

            string portNumber = "1500";  // DEFAULT PORT

            if (Configuration["Port"] != null)
            {
                portNumber = Configuration["Port"];
            }

            /*
            * From Service Manifest Environment Var for PortNumber is Endpoint Name 
            * prefixed with: Fabric_Endpoint_
            * 
            *  <Endpoints>               
            *       <Endpoint Name="ActivityBookingOptionsTypeEndpoint" UriScheme="http" Protocol="http" Type="Input" />
            *  </Endpoints>
            *          
            */            
            // When running under Service Fab check for EndPoint PortNumber (dynamically assigned)
            // and config Kestrel to Match
            var evars = Environment.GetEnvironmentVariables();

            if (evars.Contains(portKey))
            {
                portNumber = evars[portKey].ToString();
            }

            WebHost.CreateDefaultBuilder(args)
               //.ConfigureLogging(ConfigureLogging)
               .UseStartup(Assembly.GetExecutingAssembly().FullName)
               .UseSetting(WebHostDefaults.ApplicationKey, Assembly.GetEntryAssembly().FullName)
               .UseUrls(string.Format("http://+:{0}", portNumber))
               .UseApplicationInsights()
               .Build()
               .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
