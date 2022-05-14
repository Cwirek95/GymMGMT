namespace GymMGMT.Application.Responses
{
    public interface ICommandResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Id { get; set; }
    }
}
