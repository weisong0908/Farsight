using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.Backend.Mappings;
using Farsight.Backend.Persistence;
using Farsight.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            services.AddHttpClient("stock service", configureClient =>
            {
                configureClient.BaseAddress = new Uri(Configuration["StockService:Url"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farsight.Backend v1"));
            }

            app.UseHttpsRedirection();

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
