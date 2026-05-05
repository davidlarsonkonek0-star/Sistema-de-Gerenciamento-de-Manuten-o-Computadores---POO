using Domain.Entities;
using Domain.Interface;
using Domain.Services;
using Domain.ValueObjects;
using Xunit;

namespace Tests.System
{
    public class CenarioRealSystemTests
    {
        /// <summary>
        /// Teste de sistema: Cenário real de uma empresa de TI
        /// Simula o dia a dia operacional
        /// </summary>
        [Fact]
        public void CenarioReal_EmpresaDeTI()
        {
            // ===== INICIALIZAÇÃO DO SISTEMA =====
            var sistema = new SistemaManutencao();
            var repository = new InMemoryOrdemServicoRepository();
            var service = new OrdemServicoService(repository);

            var tecnico1 = new Tecnico { Id = "T001", Nome = "João Silva", Especialidade = "Hardware" };
            var tecnico2 = new Tecnico { Id = "T002", Nome = "Maria Santos", Especialidade = "Redes" };
            var cliente = new Cliente { Id = "CL01", Nome = "Empresa TechCorp", Telefone = "1140001234" };

            // ===== MANHÃ: CADASTRO DE EQUIPAMENTOS =====
            var equipamentosNovos = new List<Equipamento>
            {
                new Notebook { Descricao = "NB-DEV-001", Marca = "Dell" },
                new Notebook { Descricao = "NB-DEV-002", Marca = "HP" },
                new Desktop { Descricao = "DT-ADMIN-01", TipoGabinete = "ATX" },
                new Desktop { Descricao = "DT-ADMIN-02", TipoGabinete = "Mini" },
                new Impressora { Descricao = "IMP-SALA01", NivelToner = 0.5 },
                new Servidor { Descricao = "SRV-PRINCIPAL", QtdRacks = 8 }
            };

            foreach (var eq in equipamentosNovos)
            {
                sistema.CadastrarEquipamento(eq);
            }

            Assert.Equal(6, sistema.Equipamentos.Count);

            // ===== CLIENTE USA OS EQUIPAMENTOS =====
            cliente.AdicionarEquipamento(equipamentosNovos[0]); // Notebook 1
            cliente.AdicionarEquipamento(equipamentosNovos[1]); // Notebook 2
            cliente.AdicionarEquipamento(equipamentosNovos[3]); // Desktop 2

            Assert.Equal(3, cliente.Equipamentos.Count);

            // ===== MEIO DO DIA: PROBLEMAS REPORTADOS =====
            var ordemDia1 = new OrdemServico
            {
                Servico = "Manutenção preventiva nos notebooks",
                Prioridade = "Normal",
                ValorTotal = new Money(12000, "BRL")
            };

            var ordemDia2 = new OrdemServico
            {
                Servico = "Verificação de conectividade de rede",
                Prioridade = "Alta",
                ValorTotal = new Money(5000, "BRL")
            };

            sistema.AbrirOrdem(ordemDia1);
            sistema.AbrirOrdem(ordemDia2);
            service.AbrirOrdem(ordemDia1);
            service.AbrirOrdem(ordemDia2);

            Assert.Equal(2, sistema.Ordens.Count);

            // ===== TARDE: TÉCNICOS EXECUTAM SERVIÇOS =====
            // Técnico 1 realiza manutenção
            tecnico1.RealizarServico(equipamentosNovos[0]);
            tecnico1.RealizarServico(equipamentosNovos[1]);

            // Técnico 2 verifica redes
            tecnico2.RealizarServico(equipamentosNovos[5]); // Servidor

            Assert.Equal("Manutenção concluída", equipamentosNovos[0].Status);
            Assert.Equal("Manutenção concluída", equipamentosNovos[1].Status);
            Assert.Equal("Manutenção concluída", equipamentosNovos[5].Status);

            // ===== FIM DO DIA: PROCESSAMENTO DE PAGAMENTOS =====
            var pagamento1 = new PagamentoPix
            {
                Pagador = "TechCorp",
                Valor = new Money(12000, "BRL"),
                ChavePix = "techcorp@empresa"
            };

            var pagamento2 = new PagamentoCartao
            {
                Pagador = "TechCorp",
                Valor = new Money(5000, "BRL"),
                NumeroMascarado = "****-****-****-5678"
            };

            sistema.RegistrarPagamento(pagamento1, 1);
            sistema.RegistrarPagamento(pagamento2, 2);

            // ===== VALIDA ESTADOS FINAIS =====
            Assert.Equal(2, sistema.Pagamentos.Count);
            
            var ordem1Final = sistema.GetOrdemById(1);
            var ordem2Final = sistema.GetOrdemById(2);
            
            Assert.Equal("Pago", ordem1Final?.Status);
            Assert.Equal("Pago", ordem2Final?.Status);

            // ===== RELATÓRIO EXECUTIVO =====
            var todasAsOrdens = service.ListarOrdens();
            Assert.Equal(2, todasAsOrdens.Count);

            var orcamentoTotal = sistema.Ordens.Sum(o => o.ValorTotal.AmountMinor);
            Assert.Equal(17000, orcamentoTotal);

            // ===== AUDITORIAS FINAIS =====
            Assert.Equal(3, cliente.Equipamentos.Count);
            // Nota: tecnico1.Ordens não é preenchido por RealizarServico
            // Seria necessário adicionar ordens explicitamente ou criar um serviço que gerencia isso
            Assert.Equal(6, sistema.Equipamentos.Count);
        }

        /// <summary>
        /// Teste de sistema: Tratamento de erros em operações críticas
        /// </summary>
        [Fact]
        public void CenarioReal_TratamentoDeErros()
        {
            // SETUP
            var sistema = new SistemaManutencao();

            // TESTE 1: Ordem inexistente
            var resultado = sistema.GetOrdemById(9999);
            Assert.Null(resultado);

            // TESTE 2: Pagamento em ordem inexistente
            var pagamento = new PagamentoPix
            {
                Pagador = "Cliente",
                Valor = new Money(1000, "BRL"),
                ChavePix = "chave@pix"
            };

            sistema.RegistrarPagamento(pagamento, 9999); // Não deve lançar exceção

            // TESTE 3: Listar quando não há equipamentos
            var equipamentosVazio = sistema.Equipamentos;
            Assert.Empty(equipamentosVazio);

            // TESTE 4: Construir dados inválidos
            var notebook = new Notebook { Descricao = "", Marca = "" };
            sistema.CadastrarEquipamento(notebook);

            // Mesmo com dados vazios, foi cadastrado
            Assert.Single(sistema.Equipamentos);
        }

        /// <summary>
        /// Teste de sistema: Geração de relatórios
        /// </summary>
        [Fact]
        public void CenarioReal_RelatorioGeral()
        {
            // SETUP
            var sistema = new SistemaManutencao();

            var notebook = new Notebook { Descricao = "Notebook Teste", Marca = "Dell" };
            sistema.CadastrarEquipamento(notebook);

            var ordem = new OrdemServico
            {
                Servico = "Manutenção",
                Prioridade = "Alta",
                ValorTotal = new Money(10000, "BRL")
            };
            sistema.AbrirOrdem(ordem);

            // EXECUTA: Não deve lançar exceção
            sistema.GerarRelatorioGeral();

            // VALIDA: Estados estão corretos
            Assert.Single(sistema.Equipamentos);
            Assert.Single(sistema.Ordens);
        }
    }
}
