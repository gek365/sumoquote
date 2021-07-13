using BestDealWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BestDealWebApp.Services;
using BestDealWebApp.Services.InputDataService;
using BestDealWebApp.Services.Service1;
using BestDealWebApp.Services.Service2;
using BestDealWebApp.Services.Service3;

namespace BestDealWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IQuoteService, Api1Service>();
            services.AddTransient<IQuoteService, Api2Service>();
            services.AddTransient<IQuoteService, Api3Service>();

            services.AddScoped<IXmlMapper<Api3Request, Api3Response>, Api3Mapper>();
            services.AddTransient<IDataService, LocalDataService>();

            services.AddHttpClient();

            services.AddOptions();


            // (GK) loading settings for each services
            services.Configure<Api1ServiceSettings>(Configuration.GetSection("Api1"));
            services.Configure<Api2ServiceSettings>(Configuration.GetSection("Api2"));
            services.Configure<Api3ServiceSettings>(Configuration.GetSection("Api3"));

            services.AddControllers().AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
