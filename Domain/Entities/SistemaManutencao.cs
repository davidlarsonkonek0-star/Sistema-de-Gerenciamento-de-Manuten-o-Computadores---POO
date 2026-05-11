using System.Linq;

namespace Domain.Entities
{
    /// <summary>
    /// Representa o sistema de gerenciamento de manutenção.
    /// </summary>
    public class SistemaManutencao
    {
        public List<Equipamento> Equipamentos { get; private set; } = new();
        public List<OrdemServico> Ordens { get; private set; } = new();
        public List<Pagamento> Pagamentos { get; private set; } = new();

        /// <summary>Adiciona um equipamento ao sistema.</summary>
        public void CadastrarEquipamento(Equipamento equipamento)
        {
            equipamento.Id = Equipamentos.Count + 1;
            equipamento.DataEntrada = DateTime.Now;
            Equipamentos.Add(equipamento);
            Console.WriteLine($"Equipamento cadastrado: {equipamento.Descricao} (ID {equipamento.Id}).");
        }

        public int GetProximoId()
        {
            return Equipamentos.Any() ? Equipamentos.Max(e => e.Id) + 1 : 1;
        }

        /// <summary>Retorna o próximo identificador disponível para equipamentos.</summary>
        public Equipamento? BuscarPorId(int id)
        {
            return Equipamentos.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>Altera o status de um equipamento existente.</summary>
        public void AlterarStatus(int id, string status)
        {
            var equipamento = BuscarPorId(id);
            if (equipamento == null)
            {
                Console.WriteLine($"Equipamento {id} não encontrado.");
                return;
            }

            equipamento.SetStatus(status);
            Console.WriteLine($"Status do equipamento {id} alterado para {status}.");
        }

        /// <summary>Remove um equipamento do sistema.</summary>
        public void RemoverEquipamento(int id)
        {
            var equipamento = BuscarPorId(id);
            if (equipamento == null)
            {
                Console.WriteLine($"Equipamento {id} não encontrado.");
                return;
            }

            Equipamentos.Remove(equipamento);
            Console.WriteLine($"Equipamento {id} removido.");
        }

        /// <summary>Executa diagnóstico em todos os equipamentos cadastrados.</summary>
        public void DiagnosticarTodos()
        {
            if (!Equipamentos.Any())
            {
                Console.WriteLine("Nenhum equipamento cadastrado para diagnosticar.");
                return;
            }

            Console.WriteLine("--- Diagnóstico de todos os equipamentos ---");
            foreach (var equipamento in Equipamentos)
            {
                equipamento.Diagnosticar();
                Console.WriteLine(equipamento.VerificarStatus());
            }
        }

        /// <summary>Lista todos os equipamentos cadastrados no sistema.</summary>
        public void ListarEquipamentos()
        {
            if (!Equipamentos.Any())
            {
                Console.WriteLine("Nenhum equipamento cadastrado.");
                return;
            }

            foreach (var equipamento in Equipamentos)
            {
                equipamento.ExibirInfo();
            }
        }

        /// <summary>Abre uma nova ordem de serviço no sistema.</summary>
        public void AbrirOrdem(OrdemServico ordem)
        {
            ordem.Id = Ordens.Count + 1;
            ordem.DataEntrada = DateTime.Now.ToString("yyyy-MM-dd");
            ordem.Status = "Aberta";
            Ordens.Add(ordem);
            Console.WriteLine($"Ordem de serviço aberta: {ordem.Id}.");
        }

        /// <summary>Registra um pagamento para uma ordem existente.</summary>
        public void RegistrarPagamento(Pagamento pagamento, int ordemId)
        {
            var ordem = GetOrdemById(ordemId);
            if (ordem == null)
            {
                Console.WriteLine($"Ordem {ordemId} não encontrada.");
                return;
            }

            pagamento.Processar();
            Pagamentos.Add(pagamento);
            ordem.RegistrarPagamento(pagamento);
        }

        /// <summary>Imprime o relatório geral do sistema.</summary>
        public void GerarRelatorioGeral()
        {
            Console.WriteLine("--- Relatório Geral do Sistema ---");
            Console.WriteLine($"Equipamentos cadastrados: {Equipamentos.Count}");
            Console.WriteLine($"Ordens de serviço abertas: {Ordens.Count}");
            Console.WriteLine($"Pagamentos registrados: {Pagamentos.Count}");

            if (Ordens.Any())
            {
                Console.WriteLine("\nOrdens de serviço:");
                foreach (var ordem in Ordens)
                {
                    ordem.ExibirResumo();
                }
            }
        }

        /// <summary>Busca uma ordem de serviço pelo ID.</summary>
        public OrdemServico? GetOrdemById(int id)
        {
            return Ordens.FirstOrDefault(ordem => ordem.Id == id);
        }
    }
}