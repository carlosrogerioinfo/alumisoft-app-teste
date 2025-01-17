using Alumisoft.Pagamento.Domain.Profiles;

namespace Alumisoft.Pagamento.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ClienteProfile),
                typeof(PagamentoClienteProfile)
            );

            return services;
        }
    }
}
