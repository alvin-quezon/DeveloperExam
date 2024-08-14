using DeveloperExam.Domain.Entities;

namespace DeveloperExam.Domain.Abstractions;

public interface IRunningActivityRepository
{
    Task AddAsync(RunningActivity runningActivity);
    void Update(RunningActivity runningActivity);
    void Delete(RunningActivity runningActivity);
    void DeleteRange(IEnumerable<RunningActivity> runningActivities);
    Task<RunningActivity> GetByIdAsync(Guid id);
    Task<IEnumerable<RunningActivity>> GetRunningActivitiesByUserProfileIdAsync(Guid userProfileId, CancellationToken cancellationToken);
    Task<IEnumerable<RunningActivity>> GetAllRunningActivitiesAsync(CancellationToken cancellationToken);
}
