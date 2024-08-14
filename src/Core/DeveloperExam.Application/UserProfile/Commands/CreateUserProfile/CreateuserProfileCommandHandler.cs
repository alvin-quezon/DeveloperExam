using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using User = DeveloperExam.Domain.Entities.UserProfile;

namespace DeveloperExam.Application.UserProfile.Commands.CreateUserProfile;

internal sealed class CreateuserProfileCommandHandler : ICommandHandler<CreateUserProfileCommand, ServiceResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateuserProfileCommandHandler(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    {
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = new User(request.Name, request.Weight, request.Height, request.BirthDate);
        await _userProfileRepository.AddAsync(userProfile);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ServiceResponse(true, "User profile created successfully");
    }
}
