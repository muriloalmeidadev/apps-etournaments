using System.Collections.Generic;
using Fwks.AspNetCore.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Caesareum.ETournamentsApp.App.Api.Configuration.Swagger;

internal sealed class SwaggerParametersOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.AddCorrelationIdParameter();
    }
}