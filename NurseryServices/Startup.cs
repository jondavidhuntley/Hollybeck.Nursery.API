using Common.Logging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.AzureAppServices;
using NurseryServices.Services.DataServices;
using NurseryServices.Services.DataServices.ADO;
using NurseryServices.Services.DataServices.DAL.SQL;
using System;
using System.IO;
using System.Reflection;

namespace NurseryServices
{
    public class Startup
    {
        private string _swaggerTitle;
        private string _swaggerVersion;
        private string _swaggerDescription;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var dbConnection = Environment.GetEnvironmentVariable("DbConnection");

            services.AddSingleton<IPlantDal>(srv =>
            {
                var logger = srv.GetService<ILogger<PlantDal>>();

                return new PlantDal(dbConnection, logger);
            });

            services.AddSingleton<IPlantDataService, PlantDataService>();

            _swaggerTitle = this.Configuration["SwaggerTitle"];
            _swaggerVersion = this.Configuration["SwaggerVersion"];
            _swaggerDescription = this.Configuration["SwaggerDescription"];

            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc(_swaggerVersion, new Swashbuckle.AspNetCore.Swagger.Info { Title = _swaggerTitle, Version = _swaggerVersion, Description = _swaggerDescription});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Establish log4Net.config Path
            string logConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "log4net.config");
            loggerFactory.AddLog4Net(logConfig);

            //loggerFactory.AddAzureWebAppDiagnostics
            //(
            //    new AzureAppServicesDiagnosticsSettings
            //    {
            //        OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level}] {RequestId}-{SourceContext}: {Message}{NewLine}{Exception}"
            //    }
            //);                      

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint(string.Format("/swagger/{0}/swagger.json", _swaggerVersion), _swaggerTitle);
            });

            app.UseMvc();
        }
    }

    public class Account
    {
        public Access AccountAccess { get; set; }

        public Account()
        {
            this.AccountAccess = Access.Modify;            
        }
    }

    [Flags]
    public enum Access
    {
        Delete,
        Publish,
        Submit,
        Comment,
        Modify
    }
}