using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.RunningActivities.Commands.UpdateRunningActivity;

public sealed class UpdateRunningActivityCommandHandler : ICommandHandler<UpdateRunningActivityCommand, ServiceResponse>
{
    private readonly IRunningActivityRepository _runningActivityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRunningActivityCommandHandler(IRunningActivityRepository runningActivityRepository, IUnitOfWork unitOfWork)
    {
        _runningActivityRepository = runningActivityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Handle(UpdateRunningActivityCommand request, CancellationToken cancellationToken)
    {
        var runningActivity = await _runningActivityRepository.GetByIdAsync(request.Id);
        if(runningActivity is null)
            throw new RunningActivityNotFoundException(request.Id);

        runningActivity.Location = request.Location;
        runningActivity.Start = request.Start;
        runningActivity.End = request.End;
        runningActivity.Distance = request.Distance;

        _runningActivityRepository.Update(runningActivity);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return new ServiceResponse(false, "Failed to update running activity");

        return new ServiceResponse(true, "Running activity updated");
    }
}