using System.Linq;
using Domain.ValueObjects;

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

        public void CadastrarEquipamento(Equipamento equipamento)
        {
            equipamento.Id = Equipamentos.Count + 1;
            equipamento.DataEntrada = DateTime.Now.ToString("yyyy-MM-dd");
            Equipamentos.Add(equipamento);
            Console.WriteLine($"Equipamento cadastrado: {equipamento.Descricao} (ID {equipamento.Id}).");
        }

        public int GetProximoId()
        {
            return Equipamentos.Any() ? Equipamentos.Max(e => e.Id) + 1 : 1;
        }

        public Equipamento? BuscarPorId(int id)
        {
            return Equipamentos.FirstOrDefault(e => e.Id == id);
        }

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

        public void AbrirOrdem(OrdemServico ordem)
        {
            ordem.Id = Ordens.Count + 1;
            ordem.DataEntrada = DateTime.Now.ToString("yyyy-MM-dd");
            ordem.Status = "Aberta";
            Ordens.Add(ordem);
            Console.WriteLine($"Ordem de serviço aberta: {ordem.Id}.");
        }

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

        public OrdemServico? GetOrdemById(int id)
        {
            return Ordens.FirstOrDefault(ordem => ordem.Id == id);
        }
    }
}
