using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caesareum.ETournamentsApp.App.Api.Controllers.Common;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("v{v:apiVersion}/[controller]")]
public class V1Controller : ControllerBase
{

}
