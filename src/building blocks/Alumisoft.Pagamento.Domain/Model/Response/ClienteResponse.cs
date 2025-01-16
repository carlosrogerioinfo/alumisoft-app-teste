using Esterdigi.Core.Lib.Commands;

namespace Alumisoft.Pagamento.Domain.Http.Response
{
    public class ClienteResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

    }

    public class ClienteValidationResponse : ICommandResult
    {
        public string Message { get; set; }
    }
}