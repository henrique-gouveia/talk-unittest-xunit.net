namespace Curso.Domain.Clientes
{
    public class ClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public void Salvar(Cliente cliente)
        {
            if (!cliente.IsValid)
                throw new DomainException("Cliente inválido.");

            if (clienteRepository.Existe(cliente))
                clienteRepository.Atualizar(cliente);
            else
                clienteRepository.Inserir(cliente);
        }
    }
}
