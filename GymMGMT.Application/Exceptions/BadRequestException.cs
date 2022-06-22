using GymMGMT.Domain.Exceptions;

namespace GymMGMT.Application.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message)
            : base("Bad Request", message)
        {
        }

        public BadRequestException(string title, string message)
            : base(title, message)
        {
        }
    }
}