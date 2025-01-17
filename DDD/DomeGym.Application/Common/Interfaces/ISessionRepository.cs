using DomeGym.Domain.SessionAggregate;

namespace DomeGym.Application.Common.Interfaces
{
    public interface ISessionRepository
    {
        Task AddAsync(Session session);
        Task UpdateAsync(Session session);
        Task<Session> GetByIdAsync(Guid id);
        Task<Session> GetByUserIdAsync(Guid userId);
    }
}
