
namespace Domain.Entities
{
    /// <summary>
    /// Representa um notebook submetido a manutenção.
    /// </summary>
    public class Notebook : Equipamento
    {
        private string Marca { get; set; }
        private string ModeloBateria { get; set; }

        public Notebook(int id, string descricao, string nomeCliente, string dataEntrada, string status, string marca, string modeloBateria)
            : base(id, descricao, nomeCliente, dataEntrada, status)
        {
            Marca = marca;
            ModeloBateria = modeloBateria;
        }
        public Notebook(int id, string descricao, string nomeCliente, string dataEntrada, string status, string marca, string modeloBateria)
            : base(id, descricao, nomeCliente, dataEntrada, status)
        {
            Marca = marca;
            ModeloBateria = modeloBateria;
        }

        /// <summary>Exibe informações detalhadas do notebook.</summary>
        public override void ExibirInfo()
        {
            Console.WriteLine("  Tipo:       Notebook");
            base.ExibirInfo();
            Console.WriteLine($"  Marca:      {Marca}");
            Console.WriteLine($"  Bateria:    {ModeloBateria}");
        }
        /// <summary>Exibe informações detalhadas do notebook.</summary>
        public override void ExibirInfo()
        {
            Console.WriteLine("  Tipo:       Notebook");
            base.ExibirInfo();
            Console.WriteLine($"  Marca:      {Marca}");
            Console.WriteLine($"  Bateria:    {ModeloBateria}");
        }

        /// <summary>Realiza a manutenção do notebook.</summary>
        public override void RealizarManutencao()
        {
            Console.WriteLine($"[Notebook - {NomeCliente}] Realizando limpeza interna e verificação de hardware...");
        }
        /// <summary>Realiza a manutenção do notebook.</summary>
        public override void RealizarManutencao()
        {
            Console.WriteLine($"[Notebook - {NomeCliente}] Realizando limpeza interna e verificação de hardware...");
        }

        /// <summary>Executa o diagnóstico do notebook.</summary>
        public override void Diagnosticar()
        {
            Console.WriteLine($"[Notebook - {NomeCliente}] Verificando bateria {ModeloBateria}... possível desgaste, troca recomendada.");
        }
        /// <summary>Executa o diagnóstico do notebook.</summary>
        public override void Diagnosticar()
        {
            Console.WriteLine($"[Notebook - {NomeCliente}] Verificando bateria {ModeloBateria}... possível desgaste, troca recomendada.");
        }

        /// <summary>Gera relatório resumido do notebook.</summary>
        public override string GerarRelatorio()
        {
            return $"Notebook | Cliente: {NomeCliente} | Marca: {Marca} | Bateria: {ModeloBateria} | Status: {Status}";
        }
        /// <summary>Gera relatório resumido do notebook.</summary>
        public override string GerarRelatorio()
        {
            return $"Notebook | Cliente: {NomeCliente} | Marca: {Marca} | Bateria: {ModeloBateria} | Status: {Status}";
        }

        /// <summary>Retorna o status atual do notebook.</summary>
        public override string VerificarStatus()
        {
            return $"[Notebook - ID {Id}] Status atual: {Status}";
        }
    }
}