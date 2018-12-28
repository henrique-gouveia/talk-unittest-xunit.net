namespace Curso.Domain.Clientes
{
    public enum ClienteTipo
    {
        Azul = 10,
        Ouro = 20,
        Senior = 30
    }

    public static class DescontoTipoExtensions
    {
        public static decimal Desconto(this ClienteTipo clienteTipo, decimal valor)
        {
            var descontoPercentual = (decimal)clienteTipo;
            return (descontoPercentual / 100) * valor;
        }
    }
}
