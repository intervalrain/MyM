namespace MyMoney.TestCommon.TestConstants;

public static partial class Constants
{
    public static class Account
    {
        public static readonly Guid AccountId = Guid.NewGuid();
        public static readonly string AccountName = "Bank";
        public const int InitialAmount = 0;
    }
}