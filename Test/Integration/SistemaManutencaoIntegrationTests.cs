using Domain.Entities;
using Domain.Interface;
using Domain.Services;
using Domain.ValueObjects;
using Xunit;

namespace Tests.Integration
{
    public class SistemaManutencaoIntegrationTests
    {
        private readonly SistemaManutencao _sistema;

        public SistemaManutencaoIntegrationTests()
        {
            _sistema = new SistemaManutencao();
        }

        [Fact]
        public void CadastrarEquipamento_DeveAdicionarAoSistema()
        {
            // Arrange
            var notebook = new Notebook { Descricao = "Notebook para teste", Marca = "Dell" };

            // Act
            _sistema.CadastrarEquipamento(notebook);

            // Assert
            Assert.Single(_sistema.Equipamentos);
            Assert.Equal("Notebook para teste", _sistema.Equipamentos.First().Descricao);
        }

        [Fact]
        public void CadastrarMultiplosEquipamentos_DeveAdicionarTodos()
        {
            // Arrange
            var notebook = new Notebook { Descricao = "Notebook", Marca = "Dell" };
            var desktop = new Desktop { Descricao = "Desktop", TipoGabinete = "ATX" };
            var impressora = new Impressora { Descricao = "Impressora", NivelToner = 0.8 };

            // Act
            _sistema.CadastrarEquipamento(notebook);
            _sistema.CadastrarEquipamento(desktop);
            _sistema.CadastrarEquipamento(impressora);

            // Assert
            Assert.Equal(3, _sistema.Equipamentos.Count);
        }

        [Fact]
        public void AbrirOrdem_DeveAdicionarAoSistema()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Servico = "Manutenção",
                Prioridade = "Alta",
                ValorTotal = new Money(10000, "BRL")
            };

            // Act
            _sistema.AbrirOrdem(ordem);

            // Assert
            Assert.Single(_sistema.Ordens);
            Assert.Equal("Aberta", _sistema.Ordens.First().Status);
        }

        [Fact]
        public void GetOrdemById_DeveRetornarOrdenCorreta()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Servico = "Diagnóstico",
                ValorTotal = new Money(5000, "BRL")
            };
            _sistema.AbrirOrdem(ordem);

            // Act
            var ordemRecuperada = _sistema.GetOrdemById(1);

            // Assert
            Assert.NotNull(ordemRecuperada);
            Assert.Equal("Diagnóstico", ordemRecuperada.Servico);
        }

        [Fact]
        public void RegistrarPagamento_DeveAssociarOrdemComPagamento()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Servico = "Manutenção completa",
                ValorTotal = new Money(15000, "BRL")
            };
            _sistema.AbrirOrdem(ordem);

            var pagamento = new PagamentoPix
            {
                Pagador = "Cliente XYZ",
                Valor = new Money(15000, "BRL"),
                ChavePix = "123456@pix"
            };

            // Act
            _sistema.RegistrarPagamento(pagamento, 1);

            // Assert
            Assert.Single(_sistema.Pagamentos);
            var ordemAtualizada = _sistema.GetOrdemById(1);
            Assert.NotNull(ordemAtualizada?.Pagamento);
            Assert.Equal("Pago", ordemAtualizada?.Status);
        }
    }
}
