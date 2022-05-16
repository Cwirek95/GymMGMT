using GymMGMT.Domain.Exceptions;

namespace GymMGMT.Application.Security.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message)
            : base("Unauthorized", message)
        {
        }
    }
}
