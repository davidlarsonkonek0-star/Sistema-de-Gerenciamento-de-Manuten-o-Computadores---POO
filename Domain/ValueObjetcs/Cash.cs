namespace Domain.ValueObjects
{
    /// <summary>
    /// Representa um valor monetário em centavos em espécie.
    /// </summary>
    public class Cash
    {
        public long AmountMinor { get; private set; }
        public string Currency { get; private set; }

        public Cash(long amountMinor, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Não pode ser vazio");

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
