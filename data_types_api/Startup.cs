using data_types_api.Data;
using data_types_api.Repositories;
using data_types_api.Repositories.Interfaces;
using data_types_api.Settings;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace data_types_api
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

            services.AddControllers();

            services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var redisConnectionString = Configuration.GetSection("itdx_data_type_cache_Settings")["ConnectionString"];
            

            services.Configure<Data_Type_Db_Settings>(Configuration.GetSection(nameof(Data_Type_Db_Settings)));

            services.AddSingleton<IData_Type_Db_Settings>(sp =>
                sp.GetRequiredService<IOptions<Data_Type_Db_Settings>>().Value);

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(redisConnectionString, true);
                return ConnectionMultiplexer.Connect(configuration);
            });



            services.AddTransient<IData_Type_Context, Data_Type_Context>();
            services.AddTransient<IData_Type_Repository, Data_Type_Repository>();

            services.AddSwaggerGen();

            services.AddHealthChecks()
                .AddRedis(redisConnectionString: redisConnectionString,
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy);

            services.AddHealthChecksUI(opt =>
            {
                opt.AddHealthCheckEndpoint("ITDX DataTypes API", Configuration.GetSection("healthcheck")["endpoint"]);
            })
            .AddInMemoryStorage();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "itdx_data_types_api v1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI();
                endpoints.MapHealthChecks("api/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
