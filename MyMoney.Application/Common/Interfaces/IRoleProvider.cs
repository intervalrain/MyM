namespace MyMoney.Application.Common.Interfaces;

public interface IRoleProvider
{
    IEnumerable<string> GetRoles(Guid guid);
}