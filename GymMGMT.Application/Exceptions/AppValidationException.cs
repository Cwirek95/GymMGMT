using GymMGMT.Domain.Exceptions;

namespace GymMGMT.Application.Exceptions
{
    public class AppValidationException : AppException
    {
        public AppValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Validation Failure", "One or more validation errors occurred") => ErrorsDictionary = errorsDictionary;

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}