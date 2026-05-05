using Domain.ValueObjects;

using Xunit;

namespace Tests.Unit
{
    public class MoneyTests
    {
        [Fact]
        public void Construtor_DeveSetarAmountECurrency()
        {
            // Arrange
            long amountMinor = 10000; // 100.00
            string currency = "BRL";

            // Act
            var money = new Money(amountMinor, currency);

            // Assert
            Assert.Equal(amountMinor, money.AmountMinor);
            Assert.Equal(currency, money.Currency);
        }

        [Fact]
        public void ToDecimal_DeveConverterCentavosParaDecimal()
        {
            // Arrange
            var money = new Money(10000, "BRL");

            // Act
            decimal result = money.ToDecimal();

            // Assert
            Assert.Equal(100m, result);
        }

        [Fact]
        public void ToDecimal_ComValorZero_DeveRetornarZero()
        {
            // Arrange
            var money = new Money(0, "BRL");

            // Act
            decimal result = money.ToDecimal();

            // Assert
            Assert.Equal(0m, result);
        }

        [Fact]
        public void ToString_DeveRetornarFormatoCorreto()
        {
            // Arrange
            var money = new Money(5050, "BRL");

            // Act
            string result = money.ToString();

            // Assert
            // Aceita ambos os formatos: ponto (en-US) e vírgula (pt-BR)
            Assert.True(result == "BRL 50.5" || result == "BRL 50,5", $"Formato inesperado: {result}");
        }

        [Theory]
        [InlineData(100, "BRL", 1)]
        [InlineData(50, "USD", 0.5)]
        [InlineData(25000, "EUR", 250)]
        public void ToDecimal_ComDiferentesValores_DeveConverterCorretamente(long centavos, string currency, decimal esperado)
        {
            // Arrange
            var money = new Money(centavos, currency);

            // Act
            decimal result = money.ToDecimal();

            // Assert
            Assert.Equal(esperado, result);
        }
    }
}
