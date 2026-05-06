using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um pagamento realizado via PIX.
    /// </summary>
    public class PagamentoPix : Pagamento
    {
        public string ChavePix { get; set; } = string.Empty;

        public PagamentoPix (string pagador, decimal valor, string chavepix) : base (pagador, new Money((long) Math.Round (valor * 100), "BRL"))
        {
            ChavePix = chavepix;
        }

        public override void Processar()
        {
            Console.WriteLine($"Processando pagamento via PIX para {Pagador} usando chave {ChavePix}.");
        }

        public override string ExibirResumo()
        {
            return $"PIX ({ChavePix}) - Pagador: {Pagador} - Valor: {Valor}";
        }
    }
}