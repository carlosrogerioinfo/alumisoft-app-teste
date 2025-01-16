using Alumisoft.Pagamento.Domain.Entities;
using Alumisoft.Pagamento.Domain.Http.Response;
using AutoMapper;

namespace Alumisoft.Pagamento.Domain.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteResponse>()
                .ReverseMap();
        }

    }
}