﻿namespace MyMoney.Infrastructure.Security.Tokens;

public class JwtSettings
{
    public const string Section = nameof(JwtSettings);

    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int TokenExpirationInMinutes { get; set; }
}