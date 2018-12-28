using System.Text.RegularExpressions;

namespace Curso.Domain.Clientes
{
    public class Cliente
    {
        #region Regex
        private readonly Regex emailRegex = new Regex(@"(^[\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex cpfRegex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        #endregion

        private string nome;
        private string email;
        private string cpf;

        #region Acessors
        public int Id { get; set; }

        public string Nome
        {
            get { return nome; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new DomainException("Nome não pode ser nulo ou vazio");

                nome = value;
            }
        }

        public string Email
        {
            get { return email; }

            set
            {
                if (string.IsNullOrEmpty(value) || !emailRegex.Match(value).Success)
                    throw new DomainException("Email deve ser válido");

                email = value;
            }
        }

        public string Cpf
        {
            get { return cpf; }
            set
            {
                if (string.IsNullOrEmpty(value) || !cpfRegex.Match(value).Success)
                    throw new DomainException("Cpf deve ser válido");

                cpf = value;
            }
        }

        public ClienteTipo Tipo { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(nome)
                    && !string.IsNullOrEmpty(Email) && emailRegex.Match(Email).Success
                    && !string.IsNullOrEmpty(Cpf) && cpfRegex.Match(Cpf).Success;

            }
        }
        #endregion
    }
}
