using Domain.Entities;
using Domain.ValueObjects;

using Xunit;

namespace Tests.Unit
{
    public class EquipamentoTests
    {
        [Fact]
        public void Notebook_Diagnosticar_DeveAlterarStatusParaDiagnostico()
        {
            // Arrange
            var notebook = new Notebook { Descricao = "Notebook Dell", Marca = "Dell" };

            // Act
            notebook.Diagnosticar();

            // Assert
            Assert.Equal("Diagnóstico em andamento", notebook.Status);
        }

        [Fact]
        public void Notebook_RealizarManutencao_DeveAlterarStatusParaConcluida()
        {
            // Arrange
            var notebook = new Notebook { Descricao = "Notebook Dell", Marca = "Dell" };

            // Act
            notebook.RealizarManutencao();

            // Assert
            Assert.Equal("Manutenção concluída", notebook.Status);
        }

        [Fact]
        public void Desktop_GerarRelatorio_DeveRetornarDescricao()
        {
            // Arrange
            var desktop = new Desktop { Descricao = "Desktop HP", TipoGabinete = "ATX" };

            // Act
            string relatorio = desktop.GerarRelatorio();

            // Assert
            Assert.Contains("Desktop HP", relatorio);
            Assert.Contains("ATX", relatorio);
        }

        [Fact]
        public void Impressora_VerificarStatus_DeveRetornarStatusAtual()
        {
            // Arrange
            var impressora = new Impressora { Descricao = "Impressora Xerox", Status = "Aguardando" };

            // Act
            string status = impressora.VerificarStatus();

            // Assert
            Assert.Equal("Aguardando", status);
        }

        [Fact]
        public void Servidor_ExibirInfo_DeveExibirInformacoes()
        {
            // Arrange
            var servidor = new Servidor
            {
                Id = 1,
                Descricao = "Servidor Principal",
                QtdRacks = 4,
                DataEntrada = "2026-05-01"
            };

            // Act & Assert (Não lança exceção)
            servidor.ExibirInfo();
            Assert.NotNull(servidor);
        }

        [Fact]
        public void Equipamento_VerificarStatus_DeveRetornarStatusAguardando()
        {
            // Arrange
            var notebook = new Notebook { Descricao = "Notebook Desk" };

            // Act
            string status = notebook.VerificarStatus();

            // Assert
            Assert.Equal("Aguardando", status);
        }
    }
}
