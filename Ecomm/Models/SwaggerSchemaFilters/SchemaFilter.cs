using Ecomm.DTO;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ecomm.Models.SwaggerSchemaFilters;

public class SchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(SignInDTO))
            schema.Example = new OpenApiObject
            {
                ["email"] = new OpenApiString("medi@medi.com"),
                ["password"] = new OpenApiString("medimedi")
            };
    }
}