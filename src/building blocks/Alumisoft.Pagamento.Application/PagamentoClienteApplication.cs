using Alumisoft.Pagamento.Core.Constants;
using Alumisoft.Pagamento.Domain.Entities;
using Alumisoft.Pagamento.Domain.Http.Request;
using Alumisoft.Pagamento.Domain.Http.Response;
using Alumisoft.Pagamento.Domain.Repositories;
using Alumisoft.Pagamento.Infrastructure.Transactions;
using AutoMapper;
using Esterdigi.Core.Db.Domain.Model;
using Esterdigi.Core.Lib.Commands;
using Esterdigi.Core.Lib.Notifications;

namespace Alumisoft.Pagamento.Service
{
    public class PagamentoClienteApplication : Notifiable,
                ICommandHandler<PagamentoClienteRegisterRequest>
    {
        private readonly IPagamentoClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public PagamentoClienteApplication(IPagamentoClienteRepository repository, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            
        }

        public async Task<PagedResponse<PagamentoClienteResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, PagamentoClienteFilter filter)
        {
            var entity = await _repository.SearchPagedAsync(filter, paginationFilter, filter.SortBy, inc => inc.Cliente);

            if (entity.Data.Count() <= 0) AddNotification(Consts.PropertyMsgError, Consts.RegisterNotFound);

            if (!IsValid) return default;

            return new PagedResponse<PagamentoClienteResponse, PagedResult>(_mapper.Map<List<PagamentoClienteResponse>>(entity.Data.OrderBy(x => x.Data)), entity.Paging);
        }

        public async Task<ICommandResult> Handle(PagamentoClienteRegisterRequest request)
        {
            var entity = new PagamentoCliente(request.ClienteId, request.Valor, request.Data, request.Status);

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<PagamentoClienteResponse>(entity);
        }

        public async Task<ICommandResult> AlterarStatus(PagamentoClienteUpdateStatusRequest request, Guid id)
        {
            var entity = await _repository.GetAsync(x => x.Id == id);

            if (entity is null) AddNotification(Consts.PropertyMsgError, "Pagamento não encontrado");

            entity.AlterarStatusPagamento(request.Status);

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<PagamentoClienteResponse>(entity);
        }


    }
}