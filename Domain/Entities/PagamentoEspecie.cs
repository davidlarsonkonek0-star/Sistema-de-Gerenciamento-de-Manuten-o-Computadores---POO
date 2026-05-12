using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um pagamento realizado em espécie.
    /// </summary>
    public class PagamentoEspecie : Pagamento
    {
        public string ValorMonetario { get; private set; } = string.Empty;

        public PagamentoEspecie (string pagador, decimal valor) : base (pagador, new Money((long) Math.Round (valor * 100), "BRL"))
        {
            ValorMonetario = new Money((long) Math.Round (valor * 100), "BRL").ToString();
        }

        public override void Processar()
        {
            Console.WriteLine($"Processando pagamento em Espécie para {Pagador} no valor de {ValorMonetario}.");
        }

        public override string ExibirResumo()
        {
            return $"Pagador: {Pagador} - Valor: {ValorMonetario}";
        }
    }
}
