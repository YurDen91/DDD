using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;

using ErrorOr;

using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Persistence.Repositories;

public class GymsRepository : IGymsRepository
{
    private readonly DomeGymDbContext _dbContext;

    public GymsRepository(DomeGymDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddGymAsync(Gym gym)
    {
        await _dbContext.Gyms.AddAsync(gym);
        await _dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Guid id)
        => _dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);

    public Task<Gym?> GetByIdAsync(Guid id)
        => _dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);

    public Task<List<Gym>> ListSubscriptionGymsAsync(Guid subscriptionId)
        => _dbContext.Gyms
            .Where(gym => gym.SubscriptionId == subscriptionId)
            .ToListAsync();

    public async Task UpdateAsync(Gym gym)
    {
        _dbContext.Update(gym);
        await _dbContext.SaveChangesAsync();
    }
}
