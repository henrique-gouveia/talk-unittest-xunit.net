using Curso.Domain.Clientes;
using Curso.Domain.Produtos;

namespace Curso.Domain.Vendas
{
    public class VendaService
    {
        public readonly IClienteRepository clienteRepository;

        public readonly IProdutoRepository produtoRepository;

        private readonly IVendaRepository vendaRepository;

        public VendaService(
            IClienteRepository clienteRepository, 
            IProdutoRepository produtoRepository,
            IVendaRepository vendaRepository)
        {
            this.clienteRepository = clienteRepository;
            this.produtoRepository = produtoRepository;
            this.vendaRepository = vendaRepository;
        }

        public Venda Iniciar(Cliente cliente, Produto produto, int quantidade)
        {
            if (!clienteRepository.Existe(cliente))
                throw new DomainException($"Cliente {cliente.Nome} não existe.");

            var venda = new Venda(cliente);
            venda = AdicionarProduto(venda, produto, quantidade);

            return venda;
        }

        public Venda AdicionarProduto(Venda venda, Produto produto, int quantidade)
        {
            if (venda.Id > 0 && !vendaRepository.Existe(venda))
                throw new DomainException($"Venda {venda.Id} não existe.");

            if (!produtoRepository.Existe(produto))
                throw new DomainException($"Produto {produto.Descricao} não existe.");

            venda.AddProduto(produto, quantidade);
            vendaRepository.Salvar(venda);

            return venda;
        }
    }
}
