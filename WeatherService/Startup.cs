using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WeatherService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            InitializeTestData();
        }
        
        public static void InitializeTestData()
        {
            using (var db = new WeatherContext())
            {
                db.Database.Migrate();

                if (!db.Weather.Any())
                {
                    WeatherData[] testData = new WeatherData[] {
                        new WeatherData { Temperature = 72.8, Humidity = 37.1, Pressure = 1005.6, Altitude = 35.5 },
                        new WeatherData { Temperature = 70.2, Humidity = 36.2, Pressure = 1001.0, Altitude = 34.3 },
                        new WeatherData { Temperature = 71.0, Humidity = 38.1, Pressure = 1004.3, Altitude = 35.6 }
                    };
                    db.AddRange(testData);
                    db.SaveChanges();
                }
            }
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WeatherContext>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
