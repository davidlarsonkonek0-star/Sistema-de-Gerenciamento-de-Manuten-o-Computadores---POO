namespace Domain.Entities

{
    /// <summary>
    /// Representa um desktop submetido a manutenção.
    /// </summary>
    public class Desktop : Equipamento
    {
        private string TipoGabinete { get; set; }
        private bool TemFonteRedundante { get; set; }

        public Desktop(int id, string descricao, string nomeCliente, string dataEntrada, string status, string tipoGabinete, bool temFonteRedundante)
            : base(id, descricao, nomeCliente, dataEntrada, status)
        {
            TipoGabinete = tipoGabinete;
            TemFonteRedundante = temFonteRedundante;
        }

        /// <summary>Exibe informações detalhadas do desktop.</summary>
        public override void ExibirInfo()
        {
            Console.WriteLine("  Tipo:       Desktop");
            base.ExibirInfo();
            Console.WriteLine($"  Gabinete:   {TipoGabinete}");
            Console.WriteLine($"  Fonte redundante: {TemFonteRedundante}");
        }

        /// <summary>Realiza a manutenção do desktop.</summary>
        public override void RealizarManutencao()
        {
            Console.WriteLine($"[Desktop - {NomeCliente}] Verificando fonte de alimentação e conexões internas...");
        }

        /// <summary>Executa o diagnóstico do desktop.</summary>
        public override void Diagnosticar()
        {
            Console.WriteLine($"[Desktop - {NomeCliente}] Gabinete {TipoGabinete}. Verificando componentes...");
        }

        /// <summary>Gera relatório resumido do desktop.</summary>
        public override string GerarRelatorio()
        {
            return $"Desktop | Cliente: {NomeCliente} | Gabinete: {TipoGabinete} | Status: {Status}";
        }

        /// <summary>Retorna o status atual do desktop.</summary>
        public override string VerificarStatus()
        {
            return $"[Desktop - ID {Id}] Status atual: {Status}";
        }
    }
}