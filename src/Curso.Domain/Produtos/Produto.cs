namespace Curso.Domain.Produtos
{
    public class Produto
    {
        public string Descricao { get; set; }

        public decimal PrecoVenda { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Descricao)
                    && PrecoVenda > 0;
            }
        }

        public int Id { get; set; }
    }
}
