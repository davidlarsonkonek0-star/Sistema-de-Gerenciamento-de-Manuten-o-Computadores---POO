using Domain.Entities;
using Domain.ValueObjects;

using Xunit;

namespace Tests.Unit
{
    public class PagamentoTests
    {
        [Fact]
        public void PagamentoPix_Construtor_DeveSetarPropriedades()
        {
            // Arrange & Act
            var pagamento = new PagamentoPix
            {
                Pagador = "João Silva",
                Valor = new Money(5000, "BRL"),
                ChavePix = "123456789@email.com"
            };

            // Assert
            Assert.Equal("João Silva", pagamento.Pagador);
            Assert.Equal(5000, pagamento.Valor.AmountMinor);
            Assert.Equal("123456789@email.com", pagamento.ChavePix);
        }

        [Fact]
        public void PagamentoPix_ExibirResumo_DeveRetornarResumoFormatado()
        {
            // Arrange
            var pagamento = new PagamentoPix
            {
                Pagador = "João Silva",
                Valor = new Money(5000, "BRL"),
                ChavePix = "chave@email.com"
            };

            // Act
            string resumo = pagamento.ExibirResumo();

            // Assert
            Assert.Contains("PIX", resumo);
            Assert.Contains("João Silva", resumo);
            Assert.Contains("chave@email.com", resumo);
        }

        [Fact]
        public void PagamentoCartao_ExibirResumo_DeveRetornarResumoFormatado()
        {
            // Arrange
            var pagamento = new PagamentoCartao
            {
                Pagador = "Maria Santos",
                Valor = new Money(15000, "BRL"),
                NumeroMascarado = "****-****-****-1234"
            };

            // Act
            string resumo = pagamento.ExibirResumo();

            // Assert
            Assert.Contains("Cartão", resumo);
            Assert.Contains("Maria Santos", resumo);
            Assert.Contains("****-****-****-1234", resumo);
        }

        [Fact]
        public void Pagamento_DeveGeradorIdUnico()
        {
            // Arrange & Act
            var pagamento1 = new PagamentoPix { Pagador = "João" };
            var pagamento2 = new PagamentoPix { Pagador = "Maria" };

            // Assert
            Assert.NotEqual(pagamento1.Id, pagamento2.Id);
        }
    }
}
