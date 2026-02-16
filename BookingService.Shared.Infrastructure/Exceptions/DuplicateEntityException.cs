using System.Runtime.Serialization;

namespace BookingService.Shared.Infrastructure.Exceptions;

public class DuplicateEntityException : Exception
{
    public DuplicateEntityException()
    {
    }

    public DuplicateEntityException(string? message) : base(message)
    {
    }

    public DuplicateEntityException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}