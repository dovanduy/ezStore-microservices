﻿using Microservice.Core;
using Microservice.Core.Interfaces;
using Microservice.Logging.API.Application.Queries;
using Microservice.Logging.API.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microservice.Logging.API
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
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options => { options.AddPolicy("SiteCorsPolicy", corsBuilder.Build()); });

            // Add configuration for Swagger
            services.AddSwaggerCommon();

            // Add framework services.
            services.AddMvc();

            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<ServiceMapperProfile>();
            //    cfg.AddProfile<Microservice.Service.UserService.Mapper.ServiceMapperProfile>();
            //});

            //// Add application services.
            services.AddTransient<ICommandBus, CommandBus>();
            services.AddTransient<ILoggingQueries, LoggingQueries>();
            //services.AddTransient<ICacheService>(i => new RedisCacheService(Configuration.GetConnectionString("RedisAddress")));
            ServiceConfiguration.ConfigureServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            app.UseCors("SiteCorsPolicy");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwaggerCommon();
        }
    }
}