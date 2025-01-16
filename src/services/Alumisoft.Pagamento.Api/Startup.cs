using Alumisoft.Pagamento.Api.Configurations;
using Alumisoft.Pagamento.Infrastructure.Contexts;
using Esterdigi.Core.Authorization.Configuration;
using Esterdigi.Core.Authorization.Settings;
using Microsoft.EntityFrameworkCore;

namespace Alumisoft.Pagamento.Api
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
            AddDataContextConfigurations(services);

            services.AddAutoMapperConfiguration();

            services.AddWebApiConfiguration();

            services.AddSwaggerConfiguration();

            services.AddDependencyInjectionConfiguration(Configuration);

            services.AddJWTBearerConfiguration(Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseJWTBearerConfiguration();

            app.UseWebApiConfiguration(true);

            app.UseSwaggerConfiguration(env);
        }

        private void AddDataContextConfigurations(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")
                    
                );
                opt.EnableSensitiveDataLogging();

            }, ServiceLifetime.Scoped);

        }
    }
}
