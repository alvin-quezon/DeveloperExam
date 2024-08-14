using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.UserProfiles.Queries.GetUserProfileById;

public sealed class GetUserProfileByIdQueryHandler : IQueryHandler<GetUserProfileByIdQuery, UserProfileResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    public GetUserProfileByIdQueryHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<UserProfileResponse> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdAsync(request.Id);

        if (user is null)
            throw new UserProfileNotFoundException(request.Id);

        return new UserProfileResponse
        {
            Id = user.Id,
            Name = user.Name,
            Weight = user.Weight,
            Height = user.Height,
            BirthDate = user.BirthDate.ToString("MM/dd/yyyy"),
            Age = $"{user.Age} y/o",
            BodyMassIndex = $"{Math.Round(user.BodyMassIndex, 2)} BMI"
        };
    }
}
