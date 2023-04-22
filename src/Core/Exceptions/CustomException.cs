using System;

namespace Caesareum.ETournamentsApp.Core.Exceptions;

public class CustomException : Exception
{
    public CustomException()
        : base("This is a custom exception message")
    {

    }
}
