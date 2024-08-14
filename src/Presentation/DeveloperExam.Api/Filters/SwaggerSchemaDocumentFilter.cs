using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeveloperExam.Api.Filters
{
    public class SwaggerSchemaDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var schemasToRemove = new[] { "CreateRunningActivityRequest", "CreateUserProfileRequest", "UpdateRunningActivityRequest", "UpdateUserProfileRequest" };
            foreach(var schema in schemasToRemove)
            {
                swaggerDoc.Components.Schemas.Remove(schema);
            }
        }
    }
}
