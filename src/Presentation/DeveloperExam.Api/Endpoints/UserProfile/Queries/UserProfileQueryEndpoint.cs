using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using DeveloperExam.Application.UserProfiles.Queries.GetUserProfileById;
using DeveloperExam.Application.UserProfiles.Queries.GetAllUserProfiles;

namespace DeveloperExam.Api.Endpoints.UserProfile.Queries;

public static class UserProfileQueryEndpoint
{
    public static void UserProfileQueryEndpoints(this IEndpointRouteBuilder builder)
    {
        ApiVersionSet apiVersionSet = builder.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();
        RouteGroupBuilder groupRouteBuilder = builder.MapGroup("api/v{apiVersion:apiVersion}/query/userprofile").WithApiVersionSet(apiVersionSet).WithTags("User Profile Queries");

        groupRouteBuilder.MapGet("all", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var users = await sender.Send(new GetAllUserProfilesQuery(), cancellationToken);
            return Results.Ok(users);
        });

        groupRouteBuilder.MapGet("{id}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var user = await sender.Send(new GetUserProfileByIdQuery(id), cancellationToken);
            return Results.Ok(user);
        });
    }
}
