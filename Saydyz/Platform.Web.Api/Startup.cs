using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Platform.Data.Repositories;
using Platform.Data.Repositories.Context;
using Platform.Data.Repositories.Interfaces;
using Platform.Data.Repositories.Repositories;
using Platform.Services.ConfigurationService;
using Platform.Services.LogService;
using Platform.Services.OrderService;
using Swashbuckle.AspNetCore.Filters;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Platform.Web.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment CurrentEnvironment { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddControllers();

                if (Configuration.GetValue<string>("Swagger") == "on")
                {
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Saydyz Platform", Version = "V1" });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        c.IncludeXmlComments(xmlPath);
                        c.OperationFilter<SecurityRequirementsOperationFilter>();
                    });
                }

                var connectionString = Configuration["connectionStrings:DbConnection"];

                // Configuring Context
                Console.WriteLine("Configuring contexts...");

                services.AddDbContext<OrderContext>(o => o.UseSqlServer(connectionString));

                services.AddDbContext<OrderContext>(o =>
                {
                    o.UseSqlServer(connectionString);
                    /*throws NullReferenceException when executing migration*/
                    if (CurrentEnvironment != null && CurrentEnvironment.IsDevelopment())
                        o.EnableSensitiveDataLogging();
                });
                services.AddDbContext<ConfigurationContext>(o => o.UseSqlServer(connectionString));
                services.AddDbContext<LogContext>(o => o.UseSqlServer(connectionString));
                
                Console.WriteLine("Contexts configured...");

                // Configuring UOWs
                Console.WriteLine("Configuring UOWs...");

                // Config context
                services.AddScoped<IUnitOfWork<ConfigurationContext>, UnitOfWork<ConfigurationContext>>();

                // log context
                services.AddScoped<IUnitOfWork<LogContext>, UnitOfWork<LogContext>>();

                // Order context
                services.AddScoped<IUnitOfWork<OrderContext>, UnitOfWork<OrderContext>>();
                Console.WriteLine("UOWs configured...");

                // Configuring services
                Console.WriteLine("Configuring Services...");

                //Framework services
                services.AddAutoMapper(typeof(Startup));
                services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

                // Config services
                services.AddScoped<IConfigurationService, ConfigurationService>();

                // Order service
                services.AddScoped<IOrderService, OrderService>();

                // log services
                services.AddScoped<ILogService, LogService>();
                Console.WriteLine("Services configured");

                // Configuring Repositories
                Console.WriteLine("Configuring Repositories...");

                // Config repositories
                services.AddScoped<IConfigurationProfileRepository, ConfigurationProfileRepository>();
                services.AddScoped<IConfigurationParameterRepository, ConfigurationParameterRepository>();

                // Order repository
                services.AddScoped<IOrderRepository, OrderRepository>();

                //logging repository
                services.AddScoped<ILogRepository, LogRepository>();

                Console.WriteLine("Repositories Configured");

                services.AddMvc(options => options.EnableEndpointRouting = false);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed configuring services. Reason: " + ex.Message);
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration.GetValue<string>("Swagger") == "on")
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseCors(builder =>
                builder.WithOrigins(configuration.GetSection("Urls:AllowedCors").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());


            app.UseStatusCodePages();
            app.UseAuthorization();
            app.UseMvc();
        }
    }
}
