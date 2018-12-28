using Bogus;
using Bogus.Extensions.Brazil;

using Curso.Domain.Clientes;

namespace Curso.Domain.Test.Clientes
{
    public class ClienteBuilder
    {
        private readonly Cliente cliente;

        private ClienteBuilder()
        {
            var faker = new Faker();

            cliente = new Cliente()
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Cpf = faker.Person.Cpf(),
                Tipo = ClienteTipo.Azul
            };
        }

        public static ClienteBuilder NewInstance()
        {
            return new ClienteBuilder();
        }

        public ClienteBuilder WithId(int id)
        {
            cliente.Id = id;
            return this;
        }

        public ClienteBuilder WithNome(string nome)
        {
            cliente.Nome = nome;
            return this;
        }

        public ClienteBuilder WithEmail(string email)
        {
            cliente.Email = email;
            return this;
        }

        public ClienteBuilder WithCpf(string cpf)
        {
            cliente.Cpf = cpf;
            return this;
        }

        public ClienteBuilder WithTipo(ClienteTipo tipo)
        {
            cliente.Tipo = tipo;
            return this;
        }

        public Cliente Build() => cliente;
    }
}
