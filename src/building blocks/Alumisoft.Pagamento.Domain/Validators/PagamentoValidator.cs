using Alumisoft.Pagamento.Domain.Entities;
using FluentValidation;

namespace Alumisoft.Pagamento.Domain.Validators
{
    public class PagamentoValidator : AbstractValidator<PagamentoCliente>
    {
        public PagamentoValidator()
        {
            //RuleFor(entity => entity.Valor)
            //    .NotEmpty().WithMessage("O nome deve ser informado")
            //;
        }
    }


}
