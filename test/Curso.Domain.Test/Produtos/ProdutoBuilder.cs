using System;
using Bogus;
using Curso.Domain.Produtos;

namespace Curso.Domain.Test.Produtos
{
    public class ProdutoBuilder
    {
        private readonly Produto produto;

        public ProdutoBuilder()
        {
            var faker = new Faker();

            produto = new Produto()
            {
                Descricao = faker.Commerce.ProductName(),
                PrecoVenda = Convert.ToDecimal(faker.Commerce.Price())
            };
        }

        public static ProdutoBuilder NewInstance() => new ProdutoBuilder();

        public ProdutoBuilder WithId(int id)
        {
            produto.Id = id;
            return this;
        }

        public ProdutoBuilder WitDescricao(string descricao)
        {
            produto.Descricao = descricao;
            return this;
        }

        public ProdutoBuilder WithPrecoVenda(decimal precoVenda)
        {
            produto.PrecoVenda = precoVenda;
            return this;
        }

        public Produto Build() => produto;
    }
}
