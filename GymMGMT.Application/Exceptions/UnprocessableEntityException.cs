using GymMGMT.Domain.Exceptions;

namespace GymMGMT.Application.Exceptions
{
    public class UnprocessableEntityException : AppException
    {
        public UnprocessableEntityException(string title, string message)
            : base(title, message)
        {
        }
    }
}