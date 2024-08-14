using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivitiesByProfileId;

internal sealed class DeleteRunningActivityByProfileIdCommandHandler
{
    private readonly IRunningActivityRepository _runningActivityRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRunningActivityByProfileIdCommandHandler(IRunningActivityRepository runningActivityRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    {
        _runningActivityRepository = runningActivityRepository;
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteRunningActivitiesByProfileIdResponse> Handle(DeleteRunningActivitiesByProfileIdCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetByIdAsync(request.UserProfileId);
        if (userProfile is null)
            throw new UserProfileNotFoundException(request.UserProfileId);

        var runningActivities = await _runningActivityRepository.GetRunningActivitiesByUserProfileIdAsync(request.UserProfileId, cancellationToken);
        _runningActivityRepository.DeleteRange(runningActivities);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeleteRunningActivitiesByProfileIdResponse(request.UserProfileId);
    }
}