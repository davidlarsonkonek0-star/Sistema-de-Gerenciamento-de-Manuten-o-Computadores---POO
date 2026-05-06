using Domain.Interface;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um equipamento que pode ser diagnosticado e mantido.
    /// </summary>
    public abstract class Equipamento : IManutencao, IDiagnostico
    {
        public int Id { get; private set; }

        /// <summary>Descrição do problema ou serviço a ser realizado.</summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>Nome do cliente proprietário do equipamento.</summary>
        public string NomeCliente { get; set; } = string.Empty;

        /// <summary>Data de entrada do equipamento no serviço.</summary>
        public DateTime DataEntrada { get; set; }

        /// <summary>Status atual do equipamento.</summary>
        public string Status { get; set; } = string.Empty;

        public Equipamento(int id, string descricao, string nomeCliente, DateTime dataEntrada, string status)
        {
            Id = id;
            Descricao = descricao;
            NomeCliente = nomeCliente;
            DataEntrada = dataEntrada;
            Status = status;
        }

        /// <summary>Exibe informações básicas do equipamento.</summary>
        public virtual string ExibirInfo()
        {
          return $"ID: {Id} | Cliente: {NomeCliente} | Problema: {Descricao} | Status: {Status}";
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
