using System.Threading.Tasks;
using Fwks.Core.Domain;
using Caesareum.ETournamentsApp.Core.Requests;
using Caesareum.ETournamentsApp.Core.Responses;

namespace Caesareum.ETournamentsApp.Core.Abstractions.Services;

public interface ICustomerService
{
    Task<CustomerCreatedResponse> AddAsync(AddCustomerRequest request);
    Task<Page<CustomerResponse>> FindByFilterAsync(GetCustomerByNameRequest filter);
}