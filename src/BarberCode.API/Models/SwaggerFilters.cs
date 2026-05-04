using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BarberCode.API.Models;

public class SwaggerFilters : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var endpointMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;

		var hasAllowAnnonymous = endpointMetadata.Any(m => m is IAllowAnonymous);

		var hasAuthorize = endpointMetadata.Any(m => m is IAuthorizeData);

		if (hasAllowAnnonymous || !hasAuthorize)
			return;

		var scheme = new OpenApiSecurityScheme
		{
			Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
		};

		operation.Security = new List<OpenApiSecurityRequirement>
			{
				new OpenApiSecurityRequirement { [scheme] = Array.Empty<string>() }
			};
	}
}
