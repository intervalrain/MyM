using MyMoney.Domain.Common;

namespace MyMoney.Domain;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Dictionary<string, Guid> AccountIds { get; set; } = new();

    public User(Guid? id, string firstName, string lastName, string email)
        : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private User()
    {
    }
}