using System;

namespace Caesareum.ETournamentsApp.Core.Responses;

public sealed record CustomerCreatedResponse(Guid Guid);

public sealed record CustomerResponse(Guid Guid, string Name, string DateOfBirth, string Email, string PhoneNumber);