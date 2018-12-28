using Curso.Domain.Produtos;

namespace Curso.Domain.Vendas
{
    public class VendaDetalhe
    {
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoVendido { get; set; }

        public bool IsValid
        {
            get
            {
                return Produto != null && Produto.IsValid
                    && Quantidade > 0
                    && PrecoVendido > 0;
            }
        }
    }
}
