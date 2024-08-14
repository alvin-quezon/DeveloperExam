using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;

namespace DeveloperExam.Application.RunningActivities.Commands.CreateRunningActivity;

internal sealed class CreateRunningActivityCommandHandler : ICommandHandler<CreateRunningActivityCommand, ServiceResponse>
{
    private readonly IRunningActivityRepository _runningActivityRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateRunningActivityCommandHandler(IRunningActivityRepository runningActivityRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    {
        _runningActivityRepository = runningActivityRepository;
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Handle(CreateRunningActivityCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetByIdAsync(request.UserProfileId);
        if(userProfile is null)
            throw new UserProfileNotFoundException(request.UserProfileId);


        var runningActivity = new Activity(request.UserProfileId, request.Location, request.Start, request.End, request.Distance);
        await _runningActivityRepository.AddAsync(runningActivity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ServiceResponse(true, $"Running activity created successfully for user {request.UserProfileId}");
    }
}