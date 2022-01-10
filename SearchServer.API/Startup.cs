using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SearchServer.Services;
using SearchServer.Services.TextSearcher;

namespace SearchServer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServiceDependencies();
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Smart Search",
                    Description = "Rest API which reads from AWS Elastic Search server.",
                    Version = "v1"
                });
            });
            services.AddHttpClient<ITextSearcher, TextSearcher>(client =>
            {
                string userName = ConfigurationFactory.GetConfiguration().GetValue<string>("elastic:userName");
                string password = ConfigurationFactory.GetConfiguration().GetValue<string>("elastic:password");
                var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", userName, password)));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
            });
        }

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
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
