namespace Domain.Entities
{
    public class PagamentoPix : Pagamento
    {
        public string ChavePix { get; set; } = string.Empty;

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
