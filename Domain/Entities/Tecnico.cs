namespace Domain.Entities
{
    /// <summary>
    /// Representa um técnico que realiza manutenções e diagnósticos em equipamentos.
    /// </summary>
    public class Tecnico : Pessoa
    {
        /// <summary>Área de especialidade do técnico</summary>
        public string? Especialidade { get; set; }

        /// <summary>Lista de ordens de serviço atribuídas ao técnico</summary>
        public List<OrdemServico> Ordens { get; set; } = new();
        
        /// <summary>
        /// Realiza manutenção em um equipamento
        /// </summary>
        public void RealizarServico(Equipamento equipamento)
        {
            if (equipamento == null)
                throw new ArgumentNullException(nameof(equipamento), "Equipamento não pode ser nulo");

            Console.WriteLine($"Técnico {Nome} ({Especialidade}) realizando manutenção em {equipamento.Descricao}.");
            equipamento.RealizarManutencao();
        }

        /// <summary>
        /// Realiza diagnóstico em um equipamento
        /// </summary>
        public void RealizarDiagnostico(Equipamento equipamento)
        {
            if (equipamento == null)
                throw new ArgumentNullException(nameof(equipamento), "Equipamento não pode ser nulo");

            Console.WriteLine($"Técnico {Nome} diagnosticando {equipamento.Descricao}.");
            equipamento.Diagnosticar();
        }

        /// <summary>
        /// Adiciona uma ordem de serviço ao técnico
        /// </summary>
        public void AtribuirOrdem(OrdemServico ordem)
        {
            if (ordem == null)
                throw new ArgumentNullException(nameof(ordem), "Ordem não pode ser nula");

            Ordens.Add(ordem);
            Console.WriteLine($"Ordem {ordem.Id} atribuída ao técnico {Nome}.");
        }

        /// <summary>
        /// Lista todas as ordens de serviço do técnico
        /// </summary>
        public void ListarOrdens()
        {
            if (!Ordens.Any())
            {
                Console.WriteLine($"Técnico {Nome} não possui ordens atribuídas.");
                return;
            }

            Console.WriteLine($"\n=== Ordens do Técnico {Nome} ===");
            foreach (var ordem in Ordens)
            {
                Console.WriteLine($"  Ordem {ordem.Id}: {ordem.Servico} - Status: {ordem.Status}");
            }
        }

        /// <summary>
        /// Retorna o número total de ordens do técnico
        /// </summary>
        public int ContarOrdens() => Ordens.Count;
    }
}
