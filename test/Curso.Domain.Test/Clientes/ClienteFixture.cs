using Curso.Domain.Clientes;
using Moq;

namespace Curso.Domain.Test.Clientes
{
    public class ClienteFixture
    {
        public ClienteFixture()
        {
            ClienteRepositoryMock = new Mock<ClienteRepository>();
            ClienteService = new ClienteService(ClienteRepositoryMock.Object);
        }

        public Mock<ClienteRepository> ClienteRepositoryMock { get; private set; }

        public ClienteService ClienteService { get; private set; }
    }
}
