using System;
using System.Collections.Generic;
using System.Text;

namespace Curso.Domain.Produtos
{
    public class ProdutoEqualityComparer : IEqualityComparer<Produto>
    {
        public bool Equals(Produto x, Produto y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Descricao == y.Descricao
                && x.PrecoVenda == y.PrecoVenda;
        }

        public int GetHashCode(Produto obj)
        {
            var hCode = obj.Id.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
