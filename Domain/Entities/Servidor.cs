namespace Domain.Entities
{
    /// <summary>
    /// Representa um servidor submetido a manutenção.
    /// </summary>
    public class Servidor : Equipamento
        {
        private int NumeroRacks { get; set; }
        private string Criticidade { get; set; }

        public Servidor(int id, string descricao, string nomeCliente, string dataEntrada, string status, int numeroRacks, string criticidade)
            : base(id, descricao, nomeCliente, dataEntrada, status)
        {
            NumeroRacks = numeroRacks;
            Criticidade = criticidade;
        }

        public override void ExibirInfo()
        {
            Console.WriteLine("  Tipo:       Servidor");
            base.ExibirInfo();
            Console.WriteLine($"  Racks:      {NumeroRacks}");
            Console.WriteLine($"  Criticidade:{Criticidade}");
        }

        public override void RealizarManutencao()
        {
            Console.WriteLine($"[Servidor - {NomeCliente}] Manutenção crítica em andamento. Verificando {NumeroRacks} rack(s)...");
        }

        public override void Diagnosticar()
        {
            Console.WriteLine($"[Servidor - {NomeCliente}] Criticidade {Criticidade}. Verificando {NumeroRacks} rack(s) e integridade do sistema...");
        }

        public override string GerarRelatorio()
        {
            return $"Servidor | Cliente: {NomeCliente} | Racks: {NumeroRacks} | Criticidade: {Criticidade} | Status: {Status}";
        }

        public override string VerificarStatus()
        {
            return $"[Servidor - ID {Id}] Criticidade: {Criticidade} | Status atual: {Status}";
        }
    }
}
