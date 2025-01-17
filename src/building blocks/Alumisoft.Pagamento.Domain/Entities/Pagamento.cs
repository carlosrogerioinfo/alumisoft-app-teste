using Alumisoft.Pagamento.Domain.Enums;
using Alumisoft.Pagamento.Domain.Validators;
using Esterdigi.Core.Db.Domain.Entities;

namespace Alumisoft.Pagamento.Domain.Entities
{
    public class PagamentoCliente: Entity
    {
        protected PagamentoCliente() { }

        public PagamentoCliente(Guid clienteId, double valor, DateTime dataPagamento, StatusPagamento status, DateTime createdAt = default)
        {
            Id = Guid.NewGuid();

            CreatedAt = DateTime.Now;

            ClienteId = clienteId;
            Valor = valor;
            Data = dataPagamento;
            Status = status;

            Validate();
        }

        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; set; }
        public double Valor { get; private set; }
        public DateTime Data { get; private set; }
        public StatusPagamento Status { get; private set; }


        public void AlterarStatusPagamento(StatusPagamento status)
        {
            if (Status == StatusPagamento.Pago || Status == StatusPagamento.Cancelado) AddNotification("Warning", "Não é possível alterar o status de um pagamento já processado.");
            else Status = status;
        }

        private void Validate()
        {
            var validator = new PagamentoValidator();
            var result = validator.Validate(this);
            AddNotifications(result.Errors);
        }
    }
}
