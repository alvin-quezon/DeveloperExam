using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace DeveloperExam.Infrastructure.Context;

public class DeveloperExamContext : DbContext, IUnitOfWork
{
    public DeveloperExamContext(DbContextOptions<DeveloperExamContext> options) : base(options) { }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<RunningActivity> RunningActivities { get; set; }
}