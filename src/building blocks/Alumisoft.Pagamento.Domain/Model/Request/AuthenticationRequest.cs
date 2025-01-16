using Esterdigi.Core.Lib.Commands;
using System.ComponentModel.DataAnnotations;

namespace Alumisoft.Pagamento.Domain.Http.Request
{
    public class AuthenticationRequest :  ICommand
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class TokenRequest :  ICommand
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}