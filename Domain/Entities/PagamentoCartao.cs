namespace Domain.Entities
{
    public class PagamentoCartao : Pagamento
    {
        public string NumeroMascarado { get; set; } = string.Empty;

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
