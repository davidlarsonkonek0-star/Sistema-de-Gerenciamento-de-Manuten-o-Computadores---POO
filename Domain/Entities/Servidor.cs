namespace Domain.Entities

{
    /// <summary>
    /// Representa um servidor submetido a manutenção.
    /// </summary>
    public class Servidor : Equipamento
    {
        public int QtdRacks { get; set; }
        public string Criticidade { get; set; }

        public Servidor(int id, string descricao, string nomeCliente, DateTime dataEntrada, string status, int qtdRacks, string criticidade)
            : base(id, descricao, nomeCliente, dataEntrada, status)
        {
            QtdRacks = qtdRacks;
            Criticidade = criticidade;
        }

        /// <summary>Exibe informações detalhadas do servidor.</summary>
        public override string ExibirInfo()
        {
            return $"[Servidor] {base.ExibirInfo()} | Racks: {QtdRacks} | Criticidade: {Criticidade}";
        }

        /// <summary>Realiza a manutenção do servidor.</summary>
        public override void RealizarManutencao()
        {
            Console.WriteLine($"[Servidor - {NomeCliente}] Manutenção crítica em andamento. Verificando {QtdRacks} rack(s)...");
        }

        /// <summary>Executa o diagnóstico do servidor.</summary>
        public override void Diagnosticar()
        {
            Console.WriteLine($"[Servidor - {NomeCliente}] Criticidade {Criticidade}. Verificando {QtdRacks} rack(s) e integridade do sistema...");
        }

        /// <summary>Gera relatório resumido do servidor.</summary>
        public override string GerarRelatorio()
        {
            return $"Servidor | Cliente: {NomeCliente} | Racks: {QtdRacks} | Criticidade: {Criticidade} | Status: {Status}";
        }

        /// <summary>Retorna o status atual do servidor.</summary>
        public override string VerificarStatus()
        {
            return $"[Servidor - ID {Id}] Criticidade: {Criticidade} | Status atual: {Status}";
        }
    }
}