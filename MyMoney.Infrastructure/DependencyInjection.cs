using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Infrastructure.Persistence;
using MyMoney.Infrastructure.Security;
using MyMoney.Infrastructure.Common.Services;
using MyMoney.Infrastructure.Common.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMoney.Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyMoney.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddServices()
            .AddBackgroundServices(configuration)
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEmailNotifications(configuration);

        return services;
    }

    private static IServiceCollection AddEmailNotifications(this IServiceCollection services, IConfiguration configuration)
    {
        //EmailSettings emailSettings = new();
        //configuration.Bind(EmailSettings.Section, emailSettings);

        //if (!emailSettings.EnableEmailNotification)
        //{
        //    return services;
        //}

        //services.AddHostedService<EmailBackgroundService>();

        //services
        //    .AddFluentEmail(emailSettings.DefaultFromEmail)
        //    .AddSmtpSender(new SmtpClient(emailSettings.SmtpSettings.Server)
        //    {
        //        Port = emailSettings.SmtpSettings.Port,
        //        Credentials = new NetworkCredential(
        //            emailSettings.SmtpSettings.UserName,
        //            emailSettings.SmtpSettings.Password)
        //    });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DevConnection")));

        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();

        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();
        services.AddSingleton<IPermissionProvider, PermissionProvider>();
        services.AddSingleton<IRoleProvider, RoleProvider>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        services.AddScoped<IJwtGenerator, JwtGenerator>();

        services
            .ConfigureOptions<JwtBearerValidationConfiguration>()
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}

