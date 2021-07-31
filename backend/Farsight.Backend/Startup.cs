using System;
using Farsight.Backend.BackgroundServices;
using Farsight.Backend.Mappings;
using Farsight.Backend.Persistence;
using Farsight.Backend.Requirements;
using Farsight.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Farsight.Backend
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Farsight.Backend", Version = "v1" });
            });

            services.AddDbContext<FarsightBackendDbContext>(optionsAction => optionsAction.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IHoldingRepository, HoldingRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IStockService, StockService>();

            services.AddAutoMapper(configAction => configAction.AddProfile<MappingProfile>(), typeof(Startup));

            services.AddCors(setupAction =>
                {
                    setupAction.AddPolicy("farsight", configurePolicy =>
                    {
                        configurePolicy
                            .WithOrigins(Configuration.GetSection("Security:AllowedOrigins").Get<string[]>())
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });

            services.AddAuthentication(configureOptions =>
                {
                    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.Authority = Configuration["Authentication:Authority"];
                    configureOptions.Audience = Configuration["Authentication:Audience"];
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read", policy => policy.Requirements.Add(new HasPermissionRequirement("read", Configuration["Authentication:Authority"])));
                options.AddPolicy("write", policy => policy.Requirements.Add(new HasPermissionRequirement("write", Configuration["Authentication:Authority"])));
            });

            services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();

            services.AddHttpClient("polygon", configureClient =>
            {
                configureClient.BaseAddress = new Uri(Configuration["Polygon:Url"]);
            });

            services.AddMemoryCache();

            services.AddHostedService<StockDataSetupBackgroundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farsight.Backend v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("farsight");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
