using GymMGMT.Domain.Exceptions;

namespace GymMGMT.Application.Security.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message)
            : base("Forbidden", message)
        {
        }
    }
}