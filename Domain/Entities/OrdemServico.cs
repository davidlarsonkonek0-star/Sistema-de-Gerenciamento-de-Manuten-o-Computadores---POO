using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa a ordem de serviço de manutenção.
    /// </summary>
    public class OrdemServico
    {
        public int Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Status { get; set; } = "Nova";
        public string Servico { get; set; } = string.Empty;
        public string Prioridade { get; set; } = "Normal";
        public Money ValorTotal { get; set; } = new Money(0, "BRL");
        public Pagamento? Pagamento { get; private set; }

        /// <summary>Exibe o resumo da ordem de serviço.</summary>
        public string ExibirResumo()
        {
            return $"Ordem  {Id} | Serviço: {Servico}";
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
        }

        /// <summary>Registra uma nova ordem.</summary>
        public void Abrir()
        {
            Status = "Aberta";
            DataEntrada = DateTime.Now;
        }

        /// <summary>Ordem Aberta para Diagnostica.</summary>
        public void Diagnosticar()
        {
            if (Status != "Aberta")
                throw new InvalidOperationException("A ordem deve estar em status 'Aberta' para diagnosticar.");
            Status = "Diagnosticada";
        }

        /// <summary>Diagnostica a ordem.</summary>
        public void Executar()
        {
            if (Status != "Diagnosticada")
                throw new InvalidOperationException("A ordem deve estar em status 'Diagnosticada' para executar.");
            Status = "Em Execução";
        }

        /// <summary>Finaliza a ordem.</summary>
        public void Finalizar()
        {
            if (Status != "Em Execução" && Status != "Diagnosticada")
                throw new InvalidOperationException("A ordem deve estar em status 'Em Execução' ou 'Diagnosticada' para finalizar.");
            Status = "Finalizada";
        }

    }
}