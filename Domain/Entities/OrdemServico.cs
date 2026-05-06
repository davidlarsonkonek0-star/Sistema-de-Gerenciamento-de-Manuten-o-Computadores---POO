using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa a ordem de serviço de manutenção.
    /// </summary>
    public class OrdemServico
    {
        public int Id { get; set; }
        public string DataEntrada { get; set; } = string.Empty;
        public string Status { get; set; } = "Nova";
        public string Servico { get; set; } = string.Empty;
        public string Prioridade { get; set; } = "Normal";
        public Money ValorTotal { get; set; } = new Money(0, "BRL");
        public Pagamento? Pagamento { get; private set; }

        /// <summary>Exibe o resumo da ordem de serviço.</summary>
        public void ExibirResumo()
        {
            Console.WriteLine($"Ordem {Id} | Serviço: {Servico} | Prioridade: {Prioridade} | Status: {Status} | Valor: {ValorTotal}");
            if (Pagamento != null)
            {
                Console.WriteLine($"Pagamento registrado: {Pagamento.ExibirResumo()}");
            }
        }

        /// <summary>Retorna o orçamento calculado para a ordem.</summary>
        public Money CalcularOrcamento()
        {
            return ValorTotal;
        }

        /// <summary>Registra o pagamento associado à ordem.</summary>
        public void RegistrarPagamento(Pagamento pagamento)
        {
            Pagamento = pagamento;
            Status = "Pago";
            Console.WriteLine($"Pagamento registrado para ordem {Id}.");
        }
    }
}
