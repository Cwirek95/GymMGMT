using GymMGMT.Domain.Exceptions;

namespace GymMGMT.Application.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message)
            : base("Conflict", message)
        {
        }
    }
}
