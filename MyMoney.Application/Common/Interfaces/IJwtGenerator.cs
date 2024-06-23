namespace MyMoney.Application.Common.Interfaces;

public interface IJwtGenerator
{
    Task<string> GenerateToken(
        Guid id,
        string firstName,
        string lastName,
        string email);
}

