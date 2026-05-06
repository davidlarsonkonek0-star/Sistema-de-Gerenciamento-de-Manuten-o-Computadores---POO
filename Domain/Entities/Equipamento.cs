using Domain.Interface;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um equipamento que pode ser diagnosticado e mantido.
    /// </summary>
    public abstract class Equipamento : IManutencao, IDiagnostico
    {
        /// <summary>Identificador do equipamento.</summary>
        public int Id { get; set; }

        /// <summary>Descrição do problema ou serviço a ser realizado.</summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>Nome do cliente proprietário do equipamento.</summary>
        public string NomeCliente { get; set; } = string.Empty;

        /// <summary>Data de entrada do equipamento no serviço.</summary>
        public string DataEntrada { get; set; } = string.Empty;

        /// <summary>Status atual do equipamento.</summary>
        public string Status { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Descricao { get; protected set; }
        protected string NomeCliente { get; set; }
        public string DataEntrada { get; set; }
        protected string Status { get; set; }

        public Equipamento(int id, string descricao, string nomeCliente, string dataEntrada, string status)
        {
            Id = id;
            Descricao = descricao;
            NomeCliente = nomeCliente;
            DataEntrada = dataEntrada;
            Status = status;
        }

        /// <summary>Retorna o identificador do equipamento.</summary>
        public int GetId()
        {
            return Id;
        }

        /// <summary>Atualiza o status do equipamento.</summary>
        public void SetStatus(string status)
        {
            Status = status;
        }

        /// <summary>Exibe informações básicas do equipamento.</summary>
        public virtual void ExibirInfo()
        {
            Console.WriteLine($"  ID:         {Id}");
            Console.WriteLine($"  Cliente:    {NomeCliente}");
            Console.WriteLine($"  Problema:   {Descricao}");
            Console.WriteLine($"  Status:     {Status}");
            Console.WriteLine($"  Entrada:    {DataEntrada}");
        }

        /// <summary>Realiza a manutenção específica do equipamento.</summary>
        public abstract void RealizarManutencao();

        /// <summary>Executa o diagnóstico do equipamento.</summary>
        public abstract void Diagnosticar();

        /// <summary>Gera um relatório de status do equipamento.</summary>
        public abstract string GerarRelatorio();

        /// <summary>Retorna o status atual do equipamento.</summary>
        public abstract string VerificarStatus();
    }
}
