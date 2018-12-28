using System.Linq;
using ExpectedObjects;
using Moq;
using Xunit;

using Curso.Domain.Clientes;
using Curso.Domain.Produtos;
using Curso.Domain.Test.Clientes;
using Curso.Domain.Vendas;
using Curso.Domain.Test.Produtos;

namespace Curso.Domain.Test.Vendas
{
    public class VendaServiceTest
    {
        private readonly static int CLIENTE_ID_EXISTS = 1;
        private readonly static int PRODUTO_ID_EXISTS_ONE = 1;
        private readonly static int PRODUTO_ID_EXISTS_TWO = 2;
        private readonly static int VENDA_ID_EXISTS = 1;

        private readonly Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
        private readonly Mock<IProdutoRepository>produtoRepositoryMock = new Mock<IProdutoRepository>();
        private readonly Mock<IVendaRepository> vendaRepositoryMock = new Mock<IVendaRepository>();

        private readonly VendaService vendaService;

        public VendaServiceTest()
        {
            clienteRepositoryMock
                .Setup(r => r.Existe(It.Is<Cliente>(c => c.Id == CLIENTE_ID_EXISTS)))
                .Returns(true);

            produtoRepositoryMock
                .Setup(r => r.Existe(It.Is<Produto>(p => p.Id == PRODUTO_ID_EXISTS_ONE || p.Id == PRODUTO_ID_EXISTS_TWO)))
                .Returns(true);

            vendaRepositoryMock
                .Setup(r => r.Existe(It.Is<Venda>(v => v.Id == VENDA_ID_EXISTS)))
                .Returns(true);

            vendaService = new VendaService(
                clienteRepositoryMock.Object,
                produtoRepositoryMock.Object,
                vendaRepositoryMock.Object);
        }

        // 01. Deve inicar uma nova venda
        [Fact]
        public void DeveIniciarNovaVenda()
        {
            var clienteExpected = ClienteBuilder.NewInstance().WithId(CLIENTE_ID_EXISTS).Build();
            var produtoExpected = ProdutoBuilder.NewInstance().WithId(PRODUTO_ID_EXISTS_ONE).Build();

            var vendaExpected = new Venda(clienteExpected);
            vendaExpected.AddProduto(produtoExpected, 1);

            var venda = vendaService.Iniciar(clienteExpected, produtoExpected, 1);

            vendaExpected.ToExpectedObject().ShouldEqual(venda);
        }

        // 02. Deve iniciar uma nova venda adicionando produto com desconto do cliente azul
        [Fact]
        public void DeveIniciarNovaVendaAdicionandoProdutoComDescontoClienteAzul()
        {
            var clienteExpected = ClienteBuilder
                .NewInstance()
                .WithId(CLIENTE_ID_EXISTS)
                .WithTipo(ClienteTipo.Azul)
                .Build();

            var produtoExpected = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_ONE)
                .WithPrecoVenda(10)
                .Build();

            var venda = vendaService.Iniciar(clienteExpected, produtoExpected, 1);

            Assert.Equal(9, venda.Detalhes.FirstOrDefault().PrecoVendido);
        }

        // 03. Deve iniciar uma nova venda adicionando produto com desconto do cliente ouro
        [Fact]
        public void DeveIniciarNovaVendaAdicionandoProdutoComDescontoClienteOuro()
        {
            var clienteExpected = ClienteBuilder
                .NewInstance()
                .WithId(CLIENTE_ID_EXISTS)
                .WithTipo(ClienteTipo.Ouro)
                .Build();
            var produtoExpected = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_ONE)
                .WithPrecoVenda(10)
                .Build();

            var venda = vendaService.Iniciar(clienteExpected, produtoExpected, 1);

            Assert.Equal(8, venda.Detalhes.FirstOrDefault().PrecoVendido);
        }

        // 04. Deve iniciar uma nova venda adicionando produto com desconto do cliente senior
        [Fact]
        public void DeveIniciarNovaVendaAdicionandoProdutoComDescontoClienteSenior()
        {
            var clienteExpected = ClienteBuilder
                .NewInstance()
                .WithId(CLIENTE_ID_EXISTS)
                .WithTipo(ClienteTipo.Senior)
                .Build();
            var produtoExpected = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_ONE)
                .WithPrecoVenda(10)
                .Build();

            var venda = vendaService.Iniciar(clienteExpected, produtoExpected, 1);

            Assert.Equal(7, venda.Detalhes.FirstOrDefault().PrecoVendido);
        }

        // 05. Não deve iniciar uma nova venda quando cliente não existe
        [Fact]
        public void NaoDeveIniciarNovaVendaQuandoClienteNaoExiste()
        {
            var clienteExpected = ClienteBuilder
                .NewInstance()
                .Build();

            var exception = Assert.Throws<DomainException>(() => vendaService.Iniciar(clienteExpected, null, 1));
            Assert.Equal($"Cliente {clienteExpected.Nome} não existe.", exception.Message);
        }

        // 06. Não deve iniciar uma nova venda quando produto não existe
        [Fact]
        public void NaoDeveIniciarNovaVendaQuandoProdutoNaoExiste()
        {
            var clienteExpected = ClienteBuilder.NewInstance().WithId(CLIENTE_ID_EXISTS).Build();
            var produtoExpected = ProdutoBuilder
                .NewInstance()
                .Build();

            var exception = Assert.Throws<DomainException>(() => vendaService.Iniciar(clienteExpected, produtoExpected, 1));
            Assert.Equal($"Produto {produtoExpected.Descricao} não existe.", exception.Message);
        }

        // 07. Não deve iniciar uma nova venda quando produto inválido
        [Fact]
        public void NaoDeveIniciarNovaVendaQuandoProdutoInvalido()
        {
            var clienteExpected = ClienteBuilder.NewInstance().WithId(CLIENTE_ID_EXISTS).Build();
            var produtoExpected = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_ONE)
                .WithPrecoVenda(0)
                .Build();

            var exception = Assert.Throws<DomainException>(() => vendaService.Iniciar(clienteExpected, produtoExpected, 1));
            Assert.Equal($"Produto inválido.", exception.Message);
        }

        // 08. Não deve iniciar uma nova venda quando quantidade inválida
        [Fact]
        public void NaoDeveIniciarNovaVendaQuandoQuantidadeInvalida()
        {
            var clienteExpected = ClienteBuilder.NewInstance().WithId(CLIENTE_ID_EXISTS).Build();
            var produtoExpected = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_ONE)
                .Build();

            var exception = Assert.Throws<DomainException>(() => vendaService.Iniciar(clienteExpected, produtoExpected, 0));
            Assert.Equal($"Quantidade inválida.", exception.Message);
        }

        // 09. Deve adicionar produto em uma venda existente
        [Fact]
        public void DeveAdicionarProdutoEmUmaVendaExistente()
        {
            var clienteExpected = ClienteBuilder.NewInstance().WithId(CLIENTE_ID_EXISTS).Build();

            var produtoExpectedOne = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_ONE)
                .WithPrecoVenda(10)
                .Build();
            var produtoExpectedTwo = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_TWO)
                .WithPrecoVenda(5)
                .Build();

            var vendaExpected = new Venda(clienteExpected);
            vendaExpected.Id = VENDA_ID_EXISTS;
            vendaExpected.AddProduto(produtoExpectedOne, 1);
            vendaExpected.AddProduto(produtoExpectedTwo, 1);

            vendaRepositoryMock
                .Setup(r => r.Salvar(It.IsAny<Venda>()))
                .Callback<Venda>(v =>
                {
                    v.Id = VENDA_ID_EXISTS;
                });

            var venda = vendaService.Iniciar(clienteExpected, produtoExpectedOne, 1);
            venda = vendaService.AdicionarProduto(venda, produtoExpectedTwo, 1);

            vendaExpected.ToExpectedObject().ShouldEqual(venda);
        }

        // 10. Não deve adicionar produto a venda quando venda não existe
        [Fact]
        public void NaoDeveAdicionarProdutoEmUmaVendaQuandoVendaNaoExiste()
        {
            var clienteExpected = ClienteBuilder.NewInstance().WithId(CLIENTE_ID_EXISTS).Build();

            var produtoExpectedOne = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_ONE)
                .WithPrecoVenda(10)
                .Build();
            var produtoExpectedTwo = ProdutoBuilder
                .NewInstance()
                .WithId(PRODUTO_ID_EXISTS_TWO)
                .WithPrecoVenda(5)
                .Build();

            vendaRepositoryMock
                .Setup(r => r.Salvar(It.IsAny<Venda>()))
                .Callback<Venda>(v =>
                {
                    v.Id = 2;
                });

            var venda = vendaService.Iniciar(clienteExpected, produtoExpectedOne, 1);
            var exception = Assert.Throws<DomainException>(() => vendaService.AdicionarProduto(venda, produtoExpectedTwo, 1));
            Assert.Equal($"Venda {venda.Id} não existe.", exception.Message);
        }
    }
}
