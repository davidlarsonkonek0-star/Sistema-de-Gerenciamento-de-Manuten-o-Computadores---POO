using Domain.Entities;
using Domain.ValueObjects;

using Xunit;

namespace Tests.System
{
    public class SistemaManutencaoEndToEndTests
    {
        /// <summary>
        /// Teste de sistema completo: Fluxo de atendimento do cliente
        /// 1. Cadastra equipamentos
        /// 2. Abre ordem de serviço
        /// 3. Realiza diagnóstico e manutenção
        /// 4. Registra pagamento
        /// 5. Valida relatório final
        /// </summary>
        [Fact]
        public void FluxoCompleto_AtendimentoDeEquipamento()
        {
            // SETUP - Inicializa o sistema
            var sistema = new SistemaManutencao();
            var cliente = new Cliente { Id = "C001", Nome = "Empresa XYZ", Telefone = "1133334444" };
            var tecnico = new Tecnico { Id = "T001", Nome = "João Técnico", Especialidade = "Hardware" };

            // STAGE 1: CADASTRO DE EQUIPAMENTOS
            var notebook = new Notebook
            {
                Descricao = "Notebook Dell Latitude",
                Marca = "Dell"
            };

            var desktop = new Desktop
            {
                Descricao = "Desktop HP Compaq",
                TipoGabinete = "Mini Tower"
            };

            var impressora = new Impressora
            {
                Descricao = "Impressora Xerox WorkCentre",
                NivelToner = 0.25
            };

            sistema.CadastrarEquipamento(notebook);
            sistema.CadastrarEquipamento(desktop);
            sistema.CadastrarEquipamento(impressora);

            // VALIDAÇÃO: Equipamentos foram cadastrados
            Assert.Equal(3, sistema.Equipamentos.Count);

            // STAGE 2: CLIENTE ADICIONA EQUIPAMENTOS E ABRE ORDEM
            cliente.AdicionarEquipamento(notebook);
            cliente.AdicionarEquipamento(impressora);

            var ordem = new OrdemServico
            {
                Servico = "Manutenção preventiva e limpeza",
                Prioridade = "Normal",
                ValorTotal = new Money(8000, "BRL") // 80 reais
            };

            sistema.AbrirOrdem(ordem);

            // VALIDAÇÃO: Ordem foi aberta
            Assert.Single(sistema.Ordens);
            Assert.Equal("Aberta", sistema.Ordens.First().Status);

            // STAGE 3: TÉCNICO REALIZA DIAGNÓSTICO E MANUTENÇÃO
            tecnico.RealizarServico(notebook);
            tecnico.RealizarServico(impressora);

            // VALIDAÇÃO: Equipamentos foram processados
            Assert.Equal("Manutenção concluída", notebook.Status);
            Assert.Equal("Manutenção concluída", impressora.Status);

            // STAGE 4: REGISTRA PAGAMENTO
            var pagamentoPix = new PagamentoPix
            {
                Pagador = "Empresa XYZ",
                Valor = new Money(8000, "BRL"),
                ChavePix = "empresa@pix.brasileira"
            };

            sistema.RegistrarPagamento(pagamentoPix, 1);

            // VALIDAÇÃO: Pagamento foi registrado
            Assert.Single(sistema.Pagamentos);
            var ordemPaga = sistema.GetOrdemById(1);
            Assert.NotNull(ordemPaga?.Pagamento);
            Assert.Equal("Pago", ordemPaga?.Status);

            // STAGE 5: GERA RELATÓRIO
            var orcamento = ordemPaga?.CalcularOrcamento();

            // VALIDAÇÕES FINAIS
            Assert.NotNull(orcamento);
            Assert.Equal(8000, orcamento?.AmountMinor);
            Assert.Equal(2, cliente.Equipamentos.Count);
            // Nota: tecnico.Ordens não é preenchido automaticamente no fluxo atual
            // Seria necessário adicionar a ordem manualmente ou através de um serviço
        }

        /// <summary>
        /// Teste de sistema: Múltiplas ordens de serviço simultâneas
        /// </summary>
        [Fact]
        public void FluxoCompleto_MultiplasOrdensSimultaneas()
        {
            // SETUP
            var sistema = new SistemaManutencao();

            // STAGE 1: Cadastra múltiplos equipamentos
            var equipamentos = new List<Equipamento>
            {
                new Notebook { Descricao = "Notebook 1", Marca = "Dell" },
                new Notebook { Descricao = "Notebook 2", Marca = "HP" },
                new Desktop { Descricao = "Desktop 1", TipoGabinete = "ATX" },
                new Impressora { Descricao = "Impressora 1", NivelToner = 0.8 }
            };

            foreach (var eq in equipamentos)
            {
                sistema.CadastrarEquipamento(eq);
            }

            Assert.Equal(4, sistema.Equipamentos.Count);

            // STAGE 2: Abre múltiplas ordens
            var ordens = new List<OrdemServico>
            {
                new() { Servico = "Diagnóstico", Prioridade = "Alta", ValorTotal = new Money(5000, "BRL") },
                new() { Servico = "Manutenção", Prioridade = "Normal", ValorTotal = new Money(8000, "BRL") },
                new() { Servico = "Limpeza", Prioridade = "Baixa", ValorTotal = new Money(2000, "BRL") }
            };

            foreach (var ordem in ordens)
            {
                sistema.AbrirOrdem(ordem);
            }

            Assert.Equal(3, sistema.Ordens.Count);

            // STAGE 3: Processa pagamentos em diferentes métodos
            var pagamentoPix = new PagamentoPix
            {
                Pagador = "Cliente A",
                Valor = new Money(5000, "BRL"),
                ChavePix = "clientea@pix"
            };

            var pagamentoCartao = new PagamentoCartao
            {
                Pagador = "Cliente B",
                Valor = new Money(8000, "BRL"),
                NumeroMascarado = "****-****-****-1234"
            };

            sistema.RegistrarPagamento(pagamentoPix, 1);
            sistema.RegistrarPagamento(pagamentoCartao, 2);

            // STAGE 4: Valida estados finais
            Assert.Equal(2, sistema.Pagamentos.Count);

            var ordem1 = sistema.GetOrdemById(1);
            var ordem2 = sistema.GetOrdemById(2);
            var ordem3 = sistema.GetOrdemById(3);

            Assert.Equal("Pago", ordem1?.Status);
            Assert.Equal("Pago", ordem2?.Status);
            Assert.Equal("Aberta", ordem3?.Status); // Não foi paga
        }

        /// <summary>
        /// Teste de sistema: Fluxo com diferentes tipos de equipamentos
        /// </summary>
        [Fact]
        public void FluxoCompleto_DiferentesEquipamentos()
        {
            // SETUP
            var sistema = new SistemaManutencao();
            var servidor = new Servidor { Descricao = "Servidor DB Principal", QtdRacks = 4 };
            var notebook = new Notebook { Descricao = "Notebook Admin", Marca = "Lenovo" };

            // STAGE 1: Cadastra servidores e notebooks
            sistema.CadastrarEquipamento(servidor);
            sistema.CadastrarEquipamento(notebook);

            // STAGE 2: Realiza operações específicas por tipo
            servidor.Diagnosticar();
            notebook.Diagnosticar();

            // VALIDAÇÃO: Diferentes diagnósticos
            Assert.Equal("Diagnóstico em andamento", servidor.Status);
            Assert.Equal("Diagnóstico em andamento", notebook.Status);

            // STAGE 3: Gera relatórios específicos
            string relatorioServidor = servidor.GerarRelatorio();
            string relatorioNotebook = notebook.GerarRelatorio();

            // VALIDAÇÃO: Relatórios contêm informações corretas
            Assert.Contains("Servidor", relatorioServidor);
            Assert.Contains("Principal", relatorioServidor);
            Assert.Contains("Notebook", relatorioNotebook);
            Assert.Contains("Lenovo", relatorioNotebook);
        }
    }
}
