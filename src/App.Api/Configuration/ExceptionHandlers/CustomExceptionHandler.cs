using Fwks.AspNetCore.ErrorHandlers;
using Caesareum.ETournamentsApp.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Caesareum.ETournamentsApp.App.Api.Configuration.ExceptionHandlers;

public class CustomExceptionHandler : ExceptionHandler<CustomException>
{
    public override string Message => "A custome exception handler with bad request response";
    public override int StatusCode => StatusCodes.Status400BadRequest;
}