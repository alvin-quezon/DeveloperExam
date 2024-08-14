using Asp.Versioning.Builder;
using Asp.Versioning;
using DeveloperExam.Application.UserProfiles.Commands.CreateUserProfile;
using MediatR;
using DeveloperExam.Application.UserProfiles.Commands.UpdateUserProfile;
using DeveloperExam.Application.UserProfiles.Commands.DeleteUserProfile;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperExam.Api.Endpoints.UserProfile.Commands;

public static class UserProfileCommandEndpoint
{
    public static void UserProfileCommandEndpoints(this IEndpointRouteBuilder builder)
    {
        ApiVersionSet apiVersionSet = builder.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();
        RouteGroupBuilder groupRouteBuilder = builder.MapGroup("api/v{apiVersion:apiVersion}/command/userprofile").WithApiVersionSet(apiVersionSet).WithTags("User Profile Commands");

        groupRouteBuilder.MapPost(string.Empty, async (CreateUserProfileRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateUserProfileCommand(request.Name, request.Weight, request.Height, request.BirthDate);
            var result = await sender.Send(command, cancellationToken);

            if (!result.Success)
                return Results.BadRequest(result.Message);

            return Results.Created();
        });

        groupRouteBuilder.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateUserProfileRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateUserProfileCommand(id, request.Name, request.Weight, request.Height, request.BirthDate);
            var result = await sender.Send(command, cancellationToken);

            if (!result.Success)
                return Results.BadRequest(result.Message);

            return Results.Ok();
        });

        groupRouteBuilder.MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteUserProfileCommand(id);
            var result = await sender.Send(command, cancellationToken);

            if (!result.Success)
                return Results.BadRequest(result.Message);

            return Results.NoContent();
        });
    }
}
