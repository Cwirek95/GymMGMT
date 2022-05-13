namespace GymMGMT.Domain.Exceptions
{
    public class AppException : Exception
    {
        public string Title { get; }

        protected AppException(string title, string message) 
            : base(message) => Title = title;
    }
}
