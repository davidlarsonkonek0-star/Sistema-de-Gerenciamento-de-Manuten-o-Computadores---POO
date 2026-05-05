using Domain.Entities;
using Domain.ValueObjects;

using Xunit;

namespace Tests.Unit
{
    public class OrdemServicoTests
    {
        [Fact]
        public void OrdemServico_Construtor_DeveInicializarComValoresPadrao()
        {
            // Arrange & Act
            var ordem = new OrdemServico();

            // Assert
            Assert.Equal("Nova", ordem.Status);
            Assert.NotNull(ordem.ValorTotal);
            Assert.Null(ordem.Pagamento);
        }

        [Fact]
        public void OrdemServico_CalcularOrcamento_DeveRetornarValorTotal()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Id = 1,
                ValorTotal = new Money(10000, "BRL")
            };

            // Act
            var orcamento = ordem.CalcularOrcamento();

            // Assert
            Assert.Equal(10000, orcamento.AmountMinor);
        }

        [Fact]
        public void OrdemServico_RegistrarPagamento_DeveAlterarStatusParaPago()
        {
            // Arrange
            var ordem = new OrdemServico { Id = 1, Status = "Aberta" };
            var pagamento = new PagamentoPix
            {
                Pagador = "Cliente",
                Valor = new Money(5000, "BRL"),
                ChavePix = "chave@pix"
            };

            // Act
            ordem.RegistrarPagamento(pagamento);

            // Assert
            Assert.Equal("Pago", ordem.Status);
            Assert.NotNull(ordem.Pagamento);
            Assert.Equal(pagamento.Id, ordem.Pagamento.Id);
        }

        [Fact]
        public void OrdemServico_ExibirResumo_DeveExibirInformacoes()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Id = 1,
                Servico = "Manutenção",
                Prioridade = "Alta",
                Status = "Aberta",
                ValorTotal = new Money(5000, "BRL")
            };

            // Act & Assert (Não lança exceção)
            ordem.ExibirResumo();
            Assert.NotNull(ordem);
        }
    }
}
