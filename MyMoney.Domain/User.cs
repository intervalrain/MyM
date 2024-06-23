using ErrorOr;

using MyMoney.Domain.Common;
using MyMoney.Domain.Enums;
using MyMoney.Domain.Events;

namespace MyMoney.Domain;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }  
    public List<Guid> AccountIds { get; set; } = new();

    public User(Guid? id, string firstName, string lastName, string email)
        : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public ErrorOr<Success> AddAccount(Account account)
    {
        AccountIds.Add(account.Id);
        _domainEvents.Add(new AccountSetEvent(account));

        return Result.Success;
    }

    private User()
    {
    }
}