using DomeGym.Domain.Common;
using DomeGym.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DomeGym.Infrastructure.Middleware;

public class EventualConsistencyMiddleware
{
    public const string DomainEventsKey = "DomainEventsKey";
    
    private readonly RequestDelegate _next;
    
    public async Task InvokeAsync(HttpContext context, IPublisher publisher, DomeGymDbContext dbContext)
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
            catch (EventualConsistencyException e)
            {
                // handle exception
            }
            finally
            {
                await transaction.DisposeAsync();
            }
            await transaction.CommitAsync();
        });
        
        await _next(context);
    }
}