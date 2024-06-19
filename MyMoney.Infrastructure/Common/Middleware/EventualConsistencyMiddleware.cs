﻿using MediatR;

using Microsoft.AspNetCore.Http;

using MyMoney.Domain.Common;
using MyMoney.Infrastructure.Common.Persistence;

namespace MyMoney.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware
{
    public const string DomainEventsKey = nameof(DomainEventsKey);

    private readonly RequestDelegate _next;

    public EventualConsistencyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, AppDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
                {
                    while (domainEvents.TryDequeue(out var nextEvent))
                    {
                        await publisher.Publish(nextEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });
        await _next(context);
    }
}

