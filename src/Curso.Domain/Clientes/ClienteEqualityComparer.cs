using System.Collections.Generic;

namespace Curso.Domain.Clientes
{
    public class ClienteEqualityComparer : IEqualityComparer<Cliente>
    {
        public bool Equals(Cliente x, Cliente y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || x == null)
                return false;

            return x.Id == y.Id
                && x.Nome == y.Nome
                && x.Email == y.Email
                && x.Cpf == y.Cpf
                && x.Tipo == y.Tipo;
        }

        public int GetHashCode(Cliente obj)
        {
            var hCode = obj.Id.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
