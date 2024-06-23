using MyMoney.Domain;

namespace MyMoney.Application.Dtos;

public record AccountDto(
        Guid UserId,
        Guid AccountId,
        string AccountName,
        int InitialAmount)
{
    public static AccountDto ToDto(Account account)
    {
        return new AccountDto(
            account.UserId,
            account.Id,
            account.AccountName,
            account.InitialAmount);
    }
}