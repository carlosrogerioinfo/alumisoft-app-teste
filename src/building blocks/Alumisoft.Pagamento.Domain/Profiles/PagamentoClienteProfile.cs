using Alumisoft.Pagamento.Domain.Entities;
using Alumisoft.Pagamento.Domain.Http.Response;
using AutoMapper;
using Esterdigi.Core.Lib.Extensions;

namespace Alumisoft.Pagamento.Domain.Profiles
{
    public class PagamentoClienteProfile : Profile
    {
        public PagamentoClienteProfile()
        {
            CreateMap<PagamentoCliente, PagamentoClienteResponse>()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente.Nome))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.Status)))
                .ReverseMap();
        }

    }
}