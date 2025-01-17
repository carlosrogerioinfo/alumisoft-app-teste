using Alumisoft.Pagamento.Domain.Entities;
using Alumisoft.Pagamento.Domain.Repositories;
using Alumisoft.Pagamento.Infrastructure.Contexts;
using Esterdigi.Core.Db.Infrastructure.Repository;

namespace Alumisoft.Pagamento.Infrastructure.Repositories
{
    public class PagamentoClienteRepository : BaseRepository<PagamentoCliente>, IPagamentoClienteRepository
    {
        public PagamentoClienteRepository(ApplicationDataContext context) : base(context)
        {

        }
    }
}
