using Alumisoft.Pagamento.Domain.Validators;
using Esterdigi.Core.Db.Domain.Entities;
using Esterdigi.Core.Lib.Helpers.Encrypt;

namespace Alumisoft.Pagamento.Domain.Entities
{
    public class Cliente: Entity
    {
        protected Cliente() { }

        public Cliente(Guid id, string name, string password, string email, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Nome = name;
            Password = password;
            Email = email;

            if (id == Guid.Empty) Ativo = true;

            Validate();
        }

        public string Nome { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }


        public bool Authenticate(string email, string password)
        {
            if (Email.Equals(email) && Password.Equals(Cryptography.EncryptPassword(password)))
                return true;

            AddNotification("User", "Cliente ou senha inválidos");
            return false;
        }

        public void ChangePassword(string password) => Password = password;

        public void Activate() => Ativo = true;
        public void Deactivate() => Ativo = false;

        private void Validate()
        {
            var validator = new ClienteValidator();
            var result = validator.Validate(this);
            AddNotifications(result.Errors);
        }
    }
}
