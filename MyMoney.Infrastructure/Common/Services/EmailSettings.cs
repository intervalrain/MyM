namespace MyMoney.Infrastructure.Common.Services;

public class EmailSettings
{
    public const string Section = nameof(EmailSettings);

    public bool EnableEmailNotification { get; init; }

    public string DefaultFromEmail { get; init; } = null!;

    public SmtpSettings SmtpSettings { get; init; } = null!;
}

