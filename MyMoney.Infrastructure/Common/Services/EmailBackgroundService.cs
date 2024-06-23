using FluentEmail.Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Infrastructure.Common.Persistence;

namespace MyMoney.Infrastructure.Common.Services;

public class EmailBackgroundService : IHostedService, IDisposable
{
    private readonly AppDbContext _dbContext;
    private Timer _timer = null!;  
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IFluentEmail _fluentEmail;


    public EmailBackgroundService(IServiceScopeFactory serviceScopeFactory, IDateTimeProvider dateTimeProvider, IFluentEmail fluentEmail)
    {
        _dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        _dateTimeProvider = dateTimeProvider;
        _fluentEmail = fluentEmail;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SendEmailNotifications, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private async void SendEmailNotifications(object? state)
    {
        var now = _dateTimeProvider.UtcNow;
        var oneMinuteFromNow = now.AddMinutes(1);

        //var dueRemindersBySubscription = _dbContext.Reminders
        //    .Where(reminder => reminder.DateTime >= now && reminder.DateTime <= oneMinuteFromNow && !reminder.IsDismissed)
        //    .GroupBy(reminder => reminder.SubscriptionId)
        //    .ToList();

        //var subscriptionToBeNotified = dueRemindersBySubscription.ConvertAll(x => x.Key);

        //var usersToBeNotified = _dbContext.Users
        //    .Where(user => subscriptionToBeNotified.Contains(user.Subscription.Id))
        //    .ToList();

        //foreach (User? user in usersToBeNotified)
        //{
        //    var dueReminders = dueRemindersBySubscription
        //        .Single(x => x.Key == user.Subscription.Id)
        //        .ToList();

        //    await _fluentEmail
        //        .To(user.Email)
        //        .Subject($"{dueReminders.Count} reminders due!")
        //        .Body($"""
        //              Dear {user.FirstName} {user.LastName} from the present.

        //              I hope this email finds you well.

        //              I'm writing you this email to remind you about the following reminders:
        //              {string.Join('\n', dueReminders.Select((reminder, i) => $"{i + 1}. {reminder.Text}"))}

        //              Best,
        //              {user.FirstName} from the past.
        //              """)
        //        .SendAsync();
        //}
    }
}

