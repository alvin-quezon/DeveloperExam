using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivityById;

internal sealed class DeleteRunningActivityByIdCommandHandler : ICommandHandler<DeleteRunningActivityByIdCommand, ServiceResponse>
{
    private readonly IRunningActivityRepository _runningActivityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRunningActivityByIdCommandHandler(IRunningActivityRepository runningActivityRepository, IUnitOfWork unitOfWork)
    {
        _runningActivityRepository = runningActivityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Handle(DeleteRunningActivityByIdCommand request, CancellationToken cancellationToken)
    {
        var runningActivity = await _runningActivityRepository.GetByIdAsync(request.Id);
        if (runningActivity is null)
            throw new RunningActivityNotFoundException(request.Id);

        _runningActivityRepository.Delete(runningActivity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ServiceResponse(true, "Running activity deleted successfully");
    }
}