using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Esterdigi.Core.Lib.Commands;

namespace Alumisoft.Pagamento.Domain.Http.Request
{
    public class ClienteRegisterRequest :  ICommand
    {
        public string Nome { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class ClienteUpdateRequest : ClienteRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

    public class ClienteFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
        public string Nome { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string Email { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public bool? Ativo { get; set; }

        public string SortBy { get; set; }
    }
}