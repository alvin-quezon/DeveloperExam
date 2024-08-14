using Asp.Versioning.Builder;
using Asp.Versioning;
using System.Runtime.CompilerServices;
using DeveloperExam.Application.RunningActivities.Commands.CreateRunningActivity;
using MediatR;
using DeveloperExam.Application.RunningActivities.Commands.UpdateRunningActivity;
using DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivityById;
using DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivitiesByProfileId;

namespace DeveloperExam.Api.Endpoints.RunningActivity.Commands
{
    public static class RunningActivityCommandEndpoint
    {
        public static void RunningActivityCommandEndpoints(this IEndpointRouteBuilder builder)
        {
            ApiVersionSet apiVersionSet = builder.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();
            RouteGroupBuilder groupRouteBuilder = builder.MapGroup("api/v{apiVersion:apiVersion}/command/runningactivity").WithApiVersionSet(apiVersionSet).WithTags("Running Activity Commands");

            groupRouteBuilder.MapPost(string.Empty, async (CreateRunningActivityRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateRunningActivityCommand(request.UserProfileId, request.Location, request.Start, request.End, request.Distance);
                var result = await sender.Send(command, cancellationToken);

                if (!result.Success)
                    return Results.BadRequest(result.Message);

                return Results.Created();
            });

            groupRouteBuilder.MapPut("{id:guid}", async (Guid id, UpdateRunningActivityRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new UpdateRunningActivityCommand(id, request.Location, request.Start, request.End, request.Distance);
                var result = await sender.Send(command, cancellationToken);

                if (!result.Success)
                    return Results.BadRequest(result.Message);

                return Results.Ok();
            });

            groupRouteBuilder.MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteRunningActivityByIdCommand(id);
                var result = await sender.Send(command, cancellationToken);

                if (!result.Success)
                    return Results.BadRequest(result.Message);

                return Results.NoContent();
            });

            groupRouteBuilder.MapDelete("userprofile/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteRunningActivitiesByProfileIdCommand(id);
                var result = await sender.Send(command, cancellationToken);

                if (!result.Success)
                    return Results.BadRequest(result.Message);

                return Results.NoContent();
            });
        }
    }
}
