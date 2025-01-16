using Alumisoft.Pagamento.Infrastructure.Contexts;

namespace Alumisoft.Pagamento.Infrastructure.Transactions
{
    public class Uow : IUow
    {
        private readonly ApplicationDataContext _context;

        public Uow(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // Do Nothing
        }
    }
}
