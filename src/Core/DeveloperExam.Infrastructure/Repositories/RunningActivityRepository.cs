using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Entities;
using DeveloperExam.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperExam.Infrastructure.Repositories;

public class RunningActivityRepository : IRunningActivityRepository
{
    private readonly DeveloperExamContext _context;
    public RunningActivityRepository(DeveloperExamContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RunningActivity runningActivity)
        => await _context.RunningActivities.AddAsync(runningActivity);

    public void Update(RunningActivity runningActivity)
    => _context.RunningActivities.Update(runningActivity);

    public void Delete(RunningActivity runningActivity)
        => _context.RunningActivities.Remove(runningActivity);

    public void DeleteRange(IEnumerable<RunningActivity> runningActivities)
        => _context.RunningActivities.RemoveRange(runningActivities);

    public async Task<RunningActivity> GetByIdAsync(Guid id)
    => await _context.RunningActivities.FindAsync(id);

    public async Task<IEnumerable<RunningActivity>> GetRunningActivitiesByUserProfileIdAsync(Guid userProfileId, CancellationToken cancellationToken)
        => await _context.RunningActivities.Where(x => x.UserProfileId == userProfileId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<RunningActivity>> GetAllRunningActivitiesAsync(CancellationToken cancellationToken)
        => await _context.RunningActivities.AsNoTracking().ToListAsync(cancellationToken);

}