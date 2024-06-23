namespace MyMoney.TestCommon.TestConstants;

public static partial class Constants
{
    public static class Transaction
    {
        public static readonly Guid TransactionId = Guid.NewGuid();
        public const string Category = "Food";
        public const int Amount = 100;
        public static readonly DateTime DateTime = DateTime.UtcNow
            .AddDays(-1).Date
            .AddHours(-8);
    }
}