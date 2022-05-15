using GymMGMT.Domain.Exceptions;

namespace GymMGMT.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base("Not Found", message)
        {
        }

        public NotFoundException(string name, int id)
            : base("Not Found", $"The {name} with the identifier {id} was not found")
        {
        }

        public NotFoundException(string name, Guid id)
            : base("Not Found", $"The {name} with the identifier {id} was not found")
        {
        }
    }
}
