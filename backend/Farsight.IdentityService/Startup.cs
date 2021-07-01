using Farsight.IdentityService.ExtensionGrantValidators;
using Farsight.IdentityService.Models;
using Farsight.IdentityService.Persistence;
using Farsight.IdentityService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Farsight.IdentityService
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Farsight.IdentityService", Version = "v1" });
            });

            services.AddDbContext<FarsightIdentityServiceDbContext>(optionsAction => optionsAction.UseNpgsql(Configuration.GetConnectionString("Default")));

            services.AddIdentity<FarsightUser, IdentityRole>()
                .AddEntityFrameworkStores<FarsightIdentityServiceDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddInMemoryIdentityResources(Config.IdentityResources())
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
                .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
                .AddAspNetIdentity<FarsightUser>()
                .AddDeveloperSigningCredential()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();

            services.AddTransient<IEmailService, EmailService>();
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGrid"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farsight.IdentityService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}