using Alumisoft.Pagamento.Core.Constants;
using Alumisoft.Pagamento.Domain.Entities;
using Alumisoft.Pagamento.Domain.Http.Request;
using Alumisoft.Pagamento.Domain.Http.Response;
using Alumisoft.Pagamento.Domain.Repositories;
using Alumisoft.Pagamento.Infrastructure.Transactions;
using AutoMapper;
using Esterdigi.Core.Db.Domain.Model;
using Esterdigi.Core.Lib.Commands;
using Esterdigi.Core.Lib.Helpers.Encrypt;
using Esterdigi.Core.Lib.Notifications;

namespace Alumisoft.Pagamento.Service
{
    public class ClienteApplication : Notifiable,
                ICommandHandler<ClienteRegisterRequest>,
                ICommandHandler<ClienteUpdateRequest>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public ClienteApplication(IClienteRepository repository, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            
        }

        public async Task<ICommandResult> Handle(AuthenticationRequest request)
        {
            var entity = await _repository.GetAsync(x => x.Email == request.Email && x.Password == Cryptography.EncryptPassword(request.Password) && x.Ativo);

            if (entity is null)
            {
                AddNotification(Consts.PropertyMsgError, "Cliente ou senha inválidos ou cliente não ativo");
                return default;
            }

            if (!IsValid) return default;

            return _mapper.Map<ClienteResponse>(entity);
        }

        public async Task<PagedResponse<ClienteResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, ClienteFilter filter)
        {
            //filter.Ativo = true;

            var entity = await _repository.SearchPagedAsync(filter, paginationFilter, filter.SortBy);

            if (entity.Data.Count() <= 0) AddNotification(Consts.PropertyMsgError, Consts.RegisterNotFound);

            if (!IsValid) return default;

            return new PagedResponse<ClienteResponse, PagedResult>(_mapper.Map<List<ClienteResponse>>(entity.Data.OrderBy(x => x.Nome)), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetAsync(x => x.Id == id && x.Ativo);

            if (entity is null) AddNotification(Consts.PropertyMsgError, "Cliente não encontrado");

            if (!IsValid) return default;

            return _mapper.Map<ClienteResponse>(entity);
        }

        public async Task<ICommandResult> Handle(ClienteRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new Cliente(default, request.Nome, Cryptography.EncryptPassword(request.Password), request.Email);

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<ClienteResponse>(entity);
        }

        public async Task<ICommandResult> Handle(ClienteUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new Cliente(request.Id, request.Nome, Cryptography.EncryptPassword(request.Password), 
                request.Email, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            if (validation.Ativo) entity.Activate();

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<ClienteResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid clienteId)
        {
            var entity = await _repository.GetAsync(x => x.Id == clienteId);

            if (entity is null) AddNotification(Consts.PropertyMsgError, "Cliente não encontrado");

            if (!IsValid) return default;

            entity.Deactivate();

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<ClienteResponse>(entity);
        }

        private async Task<Cliente> ValidateInsert(ClienteRegisterRequest request)
        {
            var entity = await _repository.GetAsync(x => x.Email.ToLower() == request.Email.ToLower());
            if (entity is not null) AddNotification(Consts.PropertyMsgError, "Já existe um registro com esse e-mail cadastrado");

            return entity;
        }

        private async Task<Cliente> ValidateUpdate(ClienteUpdateRequest request)
        {
            var entity = await _repository.GetAsync(x => x.Id != request.Id && x.Email.ToLower() == request.Email.ToLower());
            if (entity is not null) AddNotification(Consts.PropertyMsgError, "Já existe um registro com esse e-mail cadastrado");

            entity = await _repository.GetAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification(Consts.PropertyMsgError, "Cliente não encontrado");

            return entity;
        }

        

    }
}