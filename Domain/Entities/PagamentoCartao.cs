using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um pagamento realizado por cartão.
    /// </summary>
    public class PagamentoCartao : Pagamento
    {
        public string NumeroMascarado { get; set; } = string.Empty;

        public PagamentoCartao (string pagador, string valor, string numeromascarado) : base (pagador, Money)
        {
            NumeroMascarado = numeromascarado;
        }

        public override void Processar()
        {
            Console.WriteLine($"Processando pagamento no cartão {NumeroMascarado} para {Pagador}.");
        }

        public override string ExibirResumo()
        {
            return $"Cartão ({NumeroMascarado}) - Pagador: {Pagador} - Valor: {Valor}";
        }
    }
}