using Esterdigi.Core.Authorization.Services;
using Alumisoft.Pagamento.Domain.Repositories;
using Alumisoft.Pagamento.Infrastructure.Repositories;
using Alumisoft.Pagamento.Infrastructure.Transactions;
using Alumisoft.Pagamento.Service;

namespace Alumisoft.Pagamento.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClienteApplication>();

            return services;
        }
    }

    
}
