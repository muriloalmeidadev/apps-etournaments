using System.Threading.Tasks;
using Fwks.AspNetCore.Attributes;
using Caesareum.ETournamentsApp.App.Api.Controllers.Common;
using Caesareum.ETournamentsApp.Core.Abstractions.Services;
using Caesareum.ETournamentsApp.Core.Requests;
using Caesareum.ETournamentsApp.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caesareum.ETournamentsApp.App.Api.Controllers.V1;

public sealed class CustomersController : V1Controller
{
    private readonly ICustomerService _service;

    public CustomersController(
        ICustomerService service)
    {
        _service = service;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(CustomerCreatedResponse), StatusCodes.Status200OK)]
    [ProducesApplicationNotificationResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] AddCustomerRequest request)
    {
        return Ok(await _service.AddAsync(request));
    }

    [HttpGet("")]
    [ProducesPagedResponse<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesApplicationNotificationResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByNameRequest request)
    {
        return Ok(await _service.FindByFilterAsync(request));
    }
}