using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Domain.Abstractions;

namespace DeveloperExam.Application.UserProfile.Queries.GetAllUserProfiles;

internal sealed class GetAllUserProfilesQueryHandler : IQueryHandler<GetAllUserProfilesQuery, IEnumerable<GetAllUserProfilesResponse>>
{
    private readonly IUserProfileRepository _userProfileRepository;
    public GetAllUserProfilesQueryHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<IEnumerable<GetAllUserProfilesResponse>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetAllAsync(cancellationToken);

        return user.Select(x => 
            new GetAllUserProfilesResponse 
            { 
                Id = x.Id, 
                Name = x.Name, 
                Height = x.Height,
                Weight = x.Weight, 
                Age = x.Age, 
                BirthDate = x.BirthDate, 
                BodyMassIndex = x.BodyMassIndex
            });
    }
}