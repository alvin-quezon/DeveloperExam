using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.RunningActivities.Queries.GetRunningActivityById;

public sealed class GetRunningActivityByIdQueryHandler : IQueryHandler<GetRunningActivityByIdQuery, RunningActivityResponse>
{
    private readonly IRunningActivityRepository _runningActivityRepository;
    public GetRunningActivityByIdQueryHandler(IRunningActivityRepository runningActivityRepository)
    {
        _runningActivityRepository = runningActivityRepository;
    }

    public async Task<RunningActivityResponse> Handle(GetRunningActivityByIdQuery request, CancellationToken cancellationToken)
    {
        var runningActivity = await _runningActivityRepository.GetByIdAsync(request.Id);
        if(runningActivity is null)
            throw new RunningActivityNotFoundException(request.Id);

        return new RunningActivityResponse
        {
            Id = runningActivity.Id,
            UserProfileId = runningActivity.UserProfileId,
            Location = runningActivity.Location,
            Start = runningActivity.Start,
            End = runningActivity.End,
            Distance = $"{runningActivity.Distance} km",
            Duration = runningActivity.Duration.ToString(@"h'h 'm'm'"),
            AveragePace = $"{Math.Round(runningActivity.AveragePace, 2)} km/hr"
        };
    }
}