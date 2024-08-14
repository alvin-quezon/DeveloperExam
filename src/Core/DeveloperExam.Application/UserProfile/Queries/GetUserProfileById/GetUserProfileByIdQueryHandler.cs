using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.UserProfile.Queries.GetUserProfileById;

internal sealed class GetUserProfileByIdQueryHandler : IQueryHandler<GetUserProfileByIdQuery, GetUserProfileByIdResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    public GetUserProfileByIdQueryHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<GetUserProfileByIdResponse> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdAsync(request.Id);

        if (user is null)
            throw new UserProfileNotFoundException(request.Id);

        return new GetUserProfileByIdResponse(user.Id, user.Name, user.Weight, user.Height, user.BirthDate, user.Age, user.BodyMassIndex);
    }
}
