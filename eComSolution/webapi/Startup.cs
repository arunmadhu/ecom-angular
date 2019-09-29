using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using order.commandservice.Context;
using order.commandservice.Services;
using order.queryservices.Context;
using order.queryservices.Services;

namespace webapi
{
    public class Startup
    {
        private readonly ILogger<Startup> logger;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration _configuration, ILogger<Startup> _logger)
        {
            Configuration = _configuration;
            logger = _logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowUIOrigin",
                                   builder =>
                                   {
                                       builder.WithOrigins(Configuration["AppSettings:webuiUrl"].ToString());
                                       builder.AllowAnyHeader();
                                       builder.AllowAnyMethod();
                                       builder.AllowCredentials();
                                   });

            });

            services.AddDbContext<queryContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
              );

            services.AddDbContext<commandContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
             );

            services.AddTransient<IOrderQuery, OrderQuery>();
            services.AddTransient<IOrderCommand, OrderCommand>();

            services.AddAutoMapper(typeof(Startup));

            services.AddLogging(lb =>
            {
                lb.AddConfiguration(Configuration.GetSection("Logging"));
                lb.AddConsole();
                lb.AddEventSourceLogger();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowUIOrigin");

            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var ex = context.Features.Get<IExceptionHandlerFeature>();

                        if (ex != null)
                        {
                            logger.LogError(ex.Error, "unhandled error", null);

                            var err = JsonConvert.SerializeObject(new Error()
                            {
                                Message = "There is an issue with the services. Please contact system admin.",
                                SupportEmail = "arun.madhu@outlook.com"
                            });
                            context.Response.StatusCode = 400;
                            await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length).ConfigureAwait(false);
                        }
                    });
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public class Error
    {
        public string Message { get; set; }
        public string SupportEmail { get; set; }
    }
}
