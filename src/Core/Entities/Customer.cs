﻿using System;
using Fwks.Core.Domain;

namespace Caesareum.ETournamentsApp.Core.Entities;

public sealed class CustomerDocument : Entity<Guid>
{
    public CustomerDocument()
    {
        Id = Guid.NewGuid();
    }

    public required string Name { get; set; }
    public string DateOfBirth { get; set; }
    public required string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public sealed class CustomerEntity : Entity<int>
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public string PhoneNumber { get; set; }
}