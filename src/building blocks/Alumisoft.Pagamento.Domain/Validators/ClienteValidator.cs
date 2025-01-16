using Alumisoft.Pagamento.Domain.Entities;
using FluentValidation;

namespace Alumisoft.Pagamento.Domain.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(entity => entity.Nome)
                .NotEmpty().WithMessage("O nome deve ser informado")
            ;

            RuleFor(entity => entity.Email)
                .NotEmpty().WithMessage("O email deve ser informado")
                .EmailAddress().WithMessage("O e-mail deve ser válido")
            ;

            RuleFor(entity => entity.Password)
                .NotEmpty().WithMessage("A senha deve ser informada")
            ;
        }
    }


}
