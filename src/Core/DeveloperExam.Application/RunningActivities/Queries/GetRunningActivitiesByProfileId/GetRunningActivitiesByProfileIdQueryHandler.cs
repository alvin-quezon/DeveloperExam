using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;

namespace DeveloperExam.Application.RunningActivities.Queries.GetRunningActivitiesByProfileId;
public sealed class GetRunningActivitiesByProfileIdQueryHandler : IQueryHandler<GetRunningActivitiesByProfileIdQuery, IEnumerable<RunningActivityResponse>>
{
    private readonly IRunningActivityRepository _runningActivityRepository;

    public GetRunningActivitiesByProfileIdQueryHandler(IRunningActivityRepository runningActivityRepository)
    {
        _runningActivityRepository = runningActivityRepository;
    }

    public async Task<IEnumerable<RunningActivityResponse>> Handle(GetRunningActivitiesByProfileIdQuery request, CancellationToken cancellationToken)
    {
        var runningActivities = await _runningActivityRepository.GetRunningActivitiesByUserProfileIdAsync(request.Id, cancellationToken);

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