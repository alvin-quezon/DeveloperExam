using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using DeveloperExam.Application.RunningActivities.Queries.GetRunningActivityById;
using DeveloperExam.Application.RunningActivities.Queries.GetRunningActivitiesByProfileId;
using DeveloperExam.Application.RunningActivities.Queries.GetAllRunningActivities;

namespace DeveloperExam.Api.Endpoints.RunningActivity.Queries
{
    public static class RunningActivityQueryEndpoint
    {
        public static void RunningActivityQueryEndpoints(this IEndpointRouteBuilder builder)
        {
            ApiVersionSet apiVersionSet = builder.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();
            RouteGroupBuilder groupRouteBuilder = builder.MapGroup("api/v{apiVersion:apiVersion}/query/runningactivity").WithApiVersionSet(apiVersionSet).WithTags("Running Activity Queries");


            groupRouteBuilder.MapGet("all", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetAllRunningActivitiesQuery();
                var result = await sender.Send(query, cancellationToken);

                return Results.Ok(result);
            });
            groupRouteBuilder.MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetRunningActivityByIdQuery(id);
                var result = await sender.Send(query, cancellationToken);

                if (result == null)
                    return Results.NotFound();

                return Results.Ok(result);
            });

            groupRouteBuilder.MapGet("userprofile/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetRunningActivitiesByProfileIdQuery(id);
                var result = await sender.Send(query, cancellationToken);

                if (result == null)
                    return Results.NotFound();

                return Results.Ok(result);
            });
        }
    }
}
