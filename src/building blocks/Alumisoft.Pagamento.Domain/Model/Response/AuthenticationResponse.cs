using Esterdigi.Core.Lib.Commands;

namespace Alumisoft.Pagamento.Domain.Http.Response
{
    public class AuthenticationResponse : ICommandResult
    {
        public string Token { get; set; }
    }
}