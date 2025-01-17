using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Persistence.Repositories;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly DomeGymDbContext _dbContext;

    public SubscriptionsRepository(DomeGymDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await _dbContext.Subscriptions.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Guid id)
        => _dbContext.Subscriptions
            .AsNoTracking()
            .AnyAsync(subscription => subscription.Id == id);

    public Task<Subscription?> GetByIdAsync(Guid id)
        => _dbContext.Subscriptions.FirstOrDefaultAsync(subscription => subscription.Id == id);

    public Task<List<Subscription>> ListAsync()
        => _dbContext.Subscriptions.ToListAsync();

    public async Task UpdateAsync(Subscription subscription)
    {
        _dbContext.Update(subscription);
        await _dbContext.SaveChangesAsync();
    }
}