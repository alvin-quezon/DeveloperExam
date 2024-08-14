using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using User = DeveloperExam.Domain.Entities.UserProfile;

namespace DeveloperExam.Application.UserProfiles.Commands.CreateUserProfile;

public sealed class CreateUserProfileCommandHandler : ICommandHandler<CreateUserProfileCommand, ServiceResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserProfileCommandHandler(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    {
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = new User(request.Name, request.Weight, request.Height, request.BirthDate.ToDateTime(TimeOnly.MinValue));
        await _userProfileRepository.AddAsync(userProfile);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return new ServiceResponse(false, "User profile failed to create");

        return new ServiceResponse(true, "User profile created successfully");
    }
}
