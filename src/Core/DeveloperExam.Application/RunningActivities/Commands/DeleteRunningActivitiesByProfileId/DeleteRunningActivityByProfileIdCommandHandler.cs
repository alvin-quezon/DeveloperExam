using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivitiesByProfileId;

public sealed class DeleteRunningActivityByProfileIdCommandHandler : ICommandHandler<DeleteRunningActivitiesByProfileIdCommand, ServiceResponse>
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

    public async Task<ServiceResponse> Handle(DeleteRunningActivitiesByProfileIdCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetByIdAsync(request.UserProfileId);
        if (userProfile is null)
            throw new UserProfileNotFoundException(request.UserProfileId);

        var runningActivities = await _runningActivityRepository.GetRunningActivitiesByUserProfileIdAsync(request.UserProfileId, cancellationToken);
        _runningActivityRepository.DeleteRange(runningActivities);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return new ServiceResponse(false, "Failed to delete running activities");

        return new ServiceResponse(true);
    }
}