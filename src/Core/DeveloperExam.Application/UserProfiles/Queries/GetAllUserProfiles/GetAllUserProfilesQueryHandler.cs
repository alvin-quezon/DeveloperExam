using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;

namespace DeveloperExam.Application.UserProfiles.Queries.GetAllUserProfiles;

public sealed class GetAllUserProfilesQueryHandler : IQueryHandler<GetAllUserProfilesQuery, IEnumerable<UserProfileResponse>>
{
    private readonly IUserProfileRepository _userProfileRepository;
    public GetAllUserProfilesQueryHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<IEnumerable<UserProfileResponse>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetAllAsync(cancellationToken);

        return user.Select(x => 
            new UserProfileResponse
            { 
                Id = x.Id, 
                Name = x.Name, 
                Height = x.Height,
                Weight = x.Weight,
                BirthDate = x.BirthDate.ToString("MM/dd/yyyy"),
                Age = $"{x.Age} y/o", 
                BodyMassIndex = $"{Math.Round(x.BodyMassIndex, 2)} BMI"
            });
    }
}