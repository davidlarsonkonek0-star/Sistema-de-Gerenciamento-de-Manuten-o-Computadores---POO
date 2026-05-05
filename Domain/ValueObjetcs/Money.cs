namespace Domain.ValueObjects
{
    public class Money
    {
        public long AmountMinor { get; private set; }
        public string Currency { get; private set; }

        public Money(long amountMinor, string currency)
        {
            AmountMinor = amountMinor;
            Currency = currency;
        }

        public decimal ToDecimal()
        {
            return AmountMinor / 100m;
        }

        public override string ToString()
        {
            return $"{Currency} {ToDecimal()}";
        }
    }
}
