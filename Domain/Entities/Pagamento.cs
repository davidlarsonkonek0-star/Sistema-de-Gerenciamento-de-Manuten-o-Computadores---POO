using Domain.Interface;
using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um pagamento genérico associado a uma ordem de serviço.
    /// </summary>
    public abstract class Pagamento : IPagamento
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Pagador { get; set; } = string.Empty;
        public Money Valor { get; set; } = new Money(0, "BRL");

        public abstract void Processar();
        public abstract string ExibirResumo();
    }
}
