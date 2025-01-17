using Alumisoft.Pagamento.Domain.Enums;
using Esterdigi.Core.Lib.Commands;

namespace Alumisoft.Pagamento.Domain.Http.Response
{
    public class PagamentoClienteResponse : ICommandResult
    {
        public Guid ClienteId { get; set; }
        public string Cliente { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }

    }

}