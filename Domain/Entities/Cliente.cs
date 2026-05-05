
namespace Domain.Entities
{
    /// <summary>
    /// Representa um cliente que pode ter múltiplos equipamentos
    /// </summary>
    public class Cliente : Pessoa
    {
        /// <summary>Telefone de contato do cliente</summary>
        public string? Telefone { get; set; }

        /// <summary>Lista de equipamentos do cliente</summary>
        public List<Equipamento> Equipamentos { get; set; } = new();

        /// <summary>
        /// Adiciona um equipamento à lista do cliente
        /// </summary>
        public void AdicionarEquipamento(Equipamento equipamento)
        {
            if (equipamento == null)
                throw new ArgumentNullException(nameof(equipamento), "Equipamento não pode ser nulo");
            
            Equipamentos.Add(equipamento);
            Console.WriteLine($"Equipamento '{equipamento.Descricao}' adicionado ao cliente {Nome}.");
        }

        /// <summary>
        /// Remove um equipamento da lista do cliente
        /// </summary>
        public void RemoverEquipamento(int equipamentoId)
        {
            var equipamento = Equipamentos.FirstOrDefault(e => e.Id == equipamentoId);
            if (equipamento != null)
            {
                Equipamentos.Remove(equipamento);
                Console.WriteLine($"Equipamento {equipamentoId} removido.");
            }
        }

        /// <summary>
        /// Lista todos os equipamentos do cliente formatados
        /// </summary>
        public void ListarEquipamentos()
        {
            if (!Equipamentos.Any())
            {
                Console.WriteLine($"Cliente {Nome} não possui equipamentos cadastrados.");
                return;
            }

            Console.WriteLine($"\n=== Equipamentos do Cliente {Nome} ===");
            foreach (var equipamento in Equipamentos)
            {
                equipamento.ExibirInfo();
            }
        }

        /// <summary>
        /// Retorna o número total de equipamentos do cliente
        /// </summary>
        public int ContarEquipamentos() => Equipamentos.Count;
    }
}