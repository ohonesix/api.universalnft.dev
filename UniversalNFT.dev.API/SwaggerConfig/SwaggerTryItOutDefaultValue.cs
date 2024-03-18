using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UniversalNFT.dev.API.SwaggerConfig
{
    public class SwaggerTryItOutDefaultValue : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.ParameterInfo != null)
            {
                var att = context.ParameterInfo.GetCustomAttribute<SwaggerTryItOutDefaultValueAttribute>();
                if (att != null)
                {
                    schema.Example = new Microsoft.OpenApi.Any.OpenApiString(att.Value.ToString());
                }
            }
        }
    }
}
