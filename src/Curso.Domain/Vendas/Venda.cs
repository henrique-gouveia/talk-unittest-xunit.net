using System.Collections.Generic;
using System.Linq;

using Curso.Domain.Clientes;
using Curso.Domain.Produtos;

namespace Curso.Domain.Vendas
{
    public class Venda
    {
        private readonly List<VendaDetalhe> detalhes;

        public Venda(Cliente cliente)
        {
            Cliente = cliente;
            detalhes = new List<VendaDetalhe>();
        }

        public VendaDetalhe AddProduto(Produto produto, int quantidade)
        {
            if (produto == null || !produto.IsValid)
                throw new DomainException("Produto inválido.");

            if (quantidade <= 0)
                throw new DomainException("Quantidade inválida.");

            return InternalAddProduto(produto, quantidade);
        }

        private VendaDetalhe InternalAddProduto(Produto produto, int quantidade)
        {
            var detalhe = new VendaDetalhe()
            {
                Produto = produto,
                Quantidade = quantidade,
                PrecoVendido = produto.PrecoVenda - Cliente.Tipo.Desconto(produto.PrecoVenda)
            };

            detalhes.Add(detalhe);
            return detalhe;
        }

        public int Id { get; set; }

        public Cliente Cliente { get; private set; }

        public IReadOnlyCollection<VendaDetalhe> Detalhes
        {
            get { return detalhes.AsReadOnly(); }
        }

        public bool IsValid
        {
            get
            {
                return Cliente != null
                    && Cliente.IsValid
                    && Detalhes != null
                    && Detalhes.Any();
            }
        }
    }
}
