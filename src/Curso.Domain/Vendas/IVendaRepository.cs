namespace Curso.Domain.Vendas
{
    public interface IVendaRepository
    {
        Venda BuscarPorId(int id);

        bool Existe(Venda venda); 

        void Salvar(Venda venda);
    }
}
