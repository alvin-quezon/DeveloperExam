using DeveloperExam.Domain.Entities;

namespace DeveloperExam.Domain.Abstractions;

public interface IUserProfileRepository
{
    Task AddAsync(UserProfile userProfile);
    void Update(UserProfile userProfile);
    void Delete(UserProfile userProfile);
    Task<UserProfile> GetByIdAsync(Guid id);
    Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken cancellationToken);
}