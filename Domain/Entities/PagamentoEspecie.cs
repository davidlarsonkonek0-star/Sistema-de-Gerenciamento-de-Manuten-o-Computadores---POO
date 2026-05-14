using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um pagamento realizado em espécie.
    /// </summary>
    public class PagamentoEspecie : Pagamento
    {
        public Cash? ValorEmEspecie { get; private set; }

        public PagamentoEspecie (string pagador, decimal valor) : base (pagador, new Money((long) Math.Round (valor * 100), "BRL"))
        {
            ValorEmEspecie = new Cash((long) Math.Round (valor * 100), "BRL");
        }

        public override void Processar()
        {
            Console.WriteLine($"Processando pagamento em Espécie para {Pagador} no valor de {ValorEmEspecie}.");
        }

        public override string ExibirResumo()
        {
            return $"Pagador: {Pagador} - Valor: {ValorEmEspecie}";
        }
    }
}
