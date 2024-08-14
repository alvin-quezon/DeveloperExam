using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;

namespace DeveloperExam.Application.RunningActivities.Queries.GetAllRunningActivities;

public sealed class GetAllRunningActivitiesQueryHandler : IQueryHandler<GetAllRunningActivitiesQuery, IEnumerable<RunningActivityResponse>>
{
    private readonly IRunningActivityRepository _runningActivityRepository;

    public GetAllRunningActivitiesQueryHandler(IRunningActivityRepository runningActivityRepository)
    {
        _runningActivityRepository = runningActivityRepository;
    }

    public async Task<IEnumerable<RunningActivityResponse>> Handle(GetAllRunningActivitiesQuery request, CancellationToken cancellationToken)
    {
        var runningActivities = await _runningActivityRepository.GetAllRunningActivitiesAsync(cancellationToken);

        return runningActivities.Select(x =>
            new RunningActivityResponse
            {
                Id = x.Id,
                UserProfileId = x.UserProfileId,
                Location = x.Location,
                Start = x.Start,
                End = x.End,
                Distance = $"{x.Distance} km",
                Duration = x.Duration.ToString(@"h'h 'm'm'"),
                AveragePace = $"{Math.Round(x.AveragePace, 2)} km/hr"
            });
    }
}