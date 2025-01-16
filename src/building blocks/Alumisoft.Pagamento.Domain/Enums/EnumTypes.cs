using System.ComponentModel;

namespace Alumisoft.Pagamento.Domain.Enums
{
    public enum StatusPagamento
    {
        [Description("Pago")]
        Pago = 1,
        [Description("Pendente")]
        Pendente = 0,
        [Description("Cancelado")]
        Cancelado = 2
    }
}
