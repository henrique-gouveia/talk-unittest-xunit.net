using Curso.Domain.Clientes;
using Moq;
using Xunit;

namespace Curso.Domain.Test.Clientes
{
    public class ClienteServiceTest
    {
        private readonly Mock<IClienteRepository> clienteRepositoryMock;
        private readonly ClienteService clienteService;

        public ClienteServiceTest()
        {
            clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteService = new ClienteService(clienteRepositoryMock.Object);
        }

        [Fact]
        public void NaoDeveSalvarClienteQuandoInvalido()
        {
            var cliente = new Cliente();
            Assert.Throws<DomainException>(() => clienteService.Salvar(cliente));
        }

        [Fact]
        public void DeveInserirClienteQuandoNaoExistir()
        {
            // Cenário
            var cliente = ClienteBuilder.NewInstance().Build();

            clienteRepositoryMock
                .Setup(r => r.Existe(cliente))
                .Returns(false);

            // Ação
            clienteService.Salvar(cliente);

            // Validação
            clienteRepositoryMock.Verify(r => r.Inserir(cliente));
        }

        [Fact]
        public void DeveAtualizarClienteQuandoExistir()
        {
            var cliente = ClienteBuilder.NewInstance().Build();

            clienteRepositoryMock
                .Setup(r => r.Existe(cliente))
                .Returns(true);

            clienteService.Salvar(cliente);

            clienteRepositoryMock.Verify(r => r.Atualizar(cliente));
        }
    }
}
