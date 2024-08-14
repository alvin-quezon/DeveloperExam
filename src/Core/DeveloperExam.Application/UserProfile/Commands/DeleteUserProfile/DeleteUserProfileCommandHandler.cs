using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.UserProfile.Commands.DeleteUserProfile;

internal sealed class DeleteUserProfileCommandHandler : ICommandHandler<DeleteUserProfileCommand, ServiceResponse>
{
    private readonly IUserProfileRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserProfileCommandHandler(IUserProfileRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _repository.GetByIdAsync(request.Id);
        if (userProfile is null)
            throw new UserProfileNotFoundException(request.Id);

        _repository.Delete(userProfile);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ServiceResponse(true, "User profile deleted successfully");
    }
}