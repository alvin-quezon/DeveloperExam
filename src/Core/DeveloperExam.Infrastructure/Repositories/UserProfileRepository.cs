using DeveloperExam.Domain.Entities;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperExam.Infrastructure.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly DeveloperExamContext _context;
    public UserProfileRepository(DeveloperExamContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserProfile userProfile)
        => await _context.UserProfiles.AddAsync(userProfile);

    public void Update(UserProfile userProfile)
        => _context.UserProfiles.Update(userProfile);

    public void Delete(UserProfile userProfile)
        => _context.UserProfiles.Remove(userProfile);

    public async Task<UserProfile> GetByIdAsync(Guid id)
        => await _context.UserProfiles.FindAsync(id);

    public async Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.UserProfiles.AsNoTracking().ToListAsync(cancellationToken);


}