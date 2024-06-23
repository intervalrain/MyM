namespace MyMoney.Infrastructure.Common.Services;

public class SmtpSettings
{
    public string Server { get; init; } = null!;
    public int Port { get; init; }
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}

