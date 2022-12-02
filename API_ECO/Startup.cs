using Business_Eco;
using Entidades_Eco;
using Logs_Eco;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NBitcoin.Logging;
//using NBitcoin.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_ECO
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

            //services.AddControllers();

            APIConfig miConfiguracion = this.Configuration.GetSection("aplicacion").Get<APIConfig>();
            services.AddTransient<IBussinessPendientes, BussinessPendientes>();
            //services.AddLogger();
            //services.AddSingleton<Logs_Eco.ILogger, Logger>(objeto => new Logger(miConfiguracion.configuracionLog.rutaArchivoLog, miConfiguracion.configuracionLog.nivel));
            services.AddControllers()
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddJsonOptions(JsonOptions =>
                    JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddLogger();
            //services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_ECO", Version = "v1" });
            });

            services.AddHttpClient("yourServerName").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "API CREDITO ECO");

            });

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
