namespace Curso.Domain.Clientes
{
    public interface IClienteRepository
    {
        void Atualizar(Cliente cliente);

        bool Existe(Cliente cliente);

        void Inserir(Cliente cliente);
    }
}
