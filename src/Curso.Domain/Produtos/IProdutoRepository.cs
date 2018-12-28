namespace Curso.Domain.Produtos
{
    public interface IProdutoRepository
    {
        bool Existe(Produto produto);
    }
}
