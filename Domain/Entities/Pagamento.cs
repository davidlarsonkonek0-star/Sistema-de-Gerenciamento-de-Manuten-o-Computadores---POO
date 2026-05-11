using Domain.Interface;
using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um pagamento genérico associado a uma ordem de serviço.
    /// </summary>
    public abstract class Pagamento : IPagamento
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Pagador { get; protected set; } = string.Empty;
        public Money Valor { get; protected set; } = new Money(0, "BRL");

        public Pagamento (string pagador, Money valor)
        {
            Pagador = pagador;
            Valor = valor;
        }

        public abstract void Processar();
        public abstract string ExibirResumo();
    }
}