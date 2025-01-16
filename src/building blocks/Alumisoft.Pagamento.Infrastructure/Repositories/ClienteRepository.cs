using Alumisoft.Pagamento.Domain.Entities;
using Alumisoft.Pagamento.Domain.Repositories;
using Alumisoft.Pagamento.Infrastructure.Contexts;
using Esterdigi.Core.Db.Infrastructure.Repository;

namespace Alumisoft.Pagamento.Infrastructure.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDataContext context) : base(context)
        {

        }
    }
}
