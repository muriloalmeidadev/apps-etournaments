using Fwks.Core.Domain;
using Caesareum.ETournamentsApp.Core.Enums;

namespace Caesareum.ETournamentsApp.Core.Requests;

public sealed record GetCustomerByNameRequest(DbType DbType, string Name = "") : BasePageQuery;

public sealed record AddCustomerRequest(string Name, string DateOfBirth, string Email, string PhoneNumber);