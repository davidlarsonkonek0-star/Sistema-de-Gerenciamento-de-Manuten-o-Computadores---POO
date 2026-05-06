namespace Domain.Entities
{
    /// <summary>
    /// Representa uma impressora submetida a manutenção.
    /// </summary>
    public class Impressora : Equipamento
    {
        private string TipoImpressao { get; set; }
        private double NivelToner { get; set; }

        public Impressora(int id, string descricao, string nomeCliente, string dataEntrada, string status, string tipoImpressao, double nivelToner)
            : base(id, descricao, nomeCliente, dataEntrada, status)
        {
            TipoImpressao = tipoImpressao;
            NivelToner = nivelToner;
        }

        /// <summary>Exibe informações detalhadas da impressora.</summary>
        /// <summary>Exibe informações detalhadas da impressora.</summary>
        public override void ExibirInfo()
        {
            Console.WriteLine("  Tipo:       Impressora");
            base.ExibirInfo();
            Console.WriteLine($"  Impressão:  {TipoImpressao}");
            Console.WriteLine($"  Toner:      {NivelToner}%");
        }

        /// <summary>Realiza a manutenção da impressora.</summary>
        /// <summary>Realiza a manutenção da impressora.</summary>
        public override void RealizarManutencao()
        {
            Console.WriteLine($"[Impressora - {NomeCliente}] Limpando cabeçote e verificando nível de toner...");
        }

        /// <summary>Executa o diagnóstico da impressora.</summary>
        /// <summary>Executa o diagnóstico da impressora.</summary>
        public override void Diagnosticar()
        {
            string alerta = NivelToner < 20 ? "NÍVEL CRÍTICO, reposição urgente!" : "nível adequado.";
            Console.WriteLine($"[Impressora - {NomeCliente}] Toner em {NivelToner}% - {alerta}");
        }

        /// <summary>Gera relatório resumido da impressora.</summary>
        /// <summary>Gera relatório resumido da impressora.</summary>
        public override string GerarRelatorio()
        {
            return $"Impressora | Cliente: {NomeCliente} | Tipo: {TipoImpressao} | Toner: {NivelToner}% | Status: {Status}";
        }

        /// <summary>Retorna o status atual da impressora.</summary>
        /// <summary>Retorna o status atual da impressora.</summary>
        public override string VerificarStatus()
        {
            return $"[Impressora - ID {Id}] Toner: {NivelToner}% | Status atual: {Status}";
        }
    }
}