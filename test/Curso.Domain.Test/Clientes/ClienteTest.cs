using Bogus;
using Bogus.Extensions.Brazil;
using Curso.Domain.Clientes;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Curso.Domain.Test.Clientes
{
    public class ClienteTest : IDisposable
    {
        private readonly ITestOutputHelper output;

        private readonly Faker faker;

        public ClienteTest(ITestOutputHelper output)
        {
            this.output = output;
            this.output.WriteLine("Executa antes de cada test");

            faker = new Faker();
        }

        [Fact]
        public void DeveCriarCliente()
        {
            var faker = new Faker();

            var clienteEsperado = new
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Cpf = faker.Person.Cpf()
            };

            var cliente = new Cliente()
            {
                Nome = clienteEsperado.Nome,
                Email = clienteEsperado.Email,
                Cpf = clienteEsperado.Cpf
            };

            clienteEsperado.ToExpectedObject().ShouldMatch(cliente);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email invalido")]
        [InlineData("email@invalido")]
        public void NaoDeveClienteTerUmEmailInvalido(string email)
        {
            var cliente = ClienteBuilder.NewInstance().Build();
            Assert.Throws<DomainException>(() => cliente.Email = email);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.output.WriteLine("Executa depois de cada test...");
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
