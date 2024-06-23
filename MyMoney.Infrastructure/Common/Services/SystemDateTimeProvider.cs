using MyMoney.Application.Common.Interfaces;

namespace MyMoney.Infrastructure.Common.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
