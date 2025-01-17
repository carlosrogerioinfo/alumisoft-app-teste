using Alumisoft.Pagamento.Domain.Enums;
using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Esterdigi.Core.Lib.Commands;

namespace Alumisoft.Pagamento.Domain.Http.Request
{
    public class PagamentoClienteRegisterRequest :  ICommand
    {
        public Guid ClienteId { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public StatusPagamento Status { get; set; }
    }

    public class PagamentoClienteUpdateStatusRequest : ICommand
    {
        public StatusPagamento Status { get; set; }
    }

    public class PagamentoClienteFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? ClienteId { get; set; }

        public string SortBy { get; set; }
    }
}