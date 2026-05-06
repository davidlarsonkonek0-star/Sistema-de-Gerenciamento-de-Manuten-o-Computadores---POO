namespace Domain.ValueObjects
{
    /// <summary>
    /// Representa um valor monetário em centavos com moeda.
    /// </summary>
    public class Money
    {
        public long AmountMinor { get; private set; }
        public string Currency { get; private set; }

        public Money(long amountMinor, string currency)
        {
            AmountMinor = amountMinor;
            Currency = currency;
        }

        /// <summary>Converte o valor para decimal usando centavos.</summary>
        public decimal ToDecimal()
        {
            return AmountMinor / 100m;
        }

        /// <summary>Retorna o valor monetário como string formatada.</summary>
        public override string ToString()
        {
            return $"{Currency} {ToDecimal()}";
        }
    }
}