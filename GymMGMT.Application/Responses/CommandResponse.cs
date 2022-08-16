namespace GymMGMT.Application.Responses
{
    public class CommandResponse : ICommandResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public dynamic? Id { get; set; }

        public CommandResponse()
        {
            Success = true;
        }

        public CommandResponse(string? message = null)
        {
            Success = true;
            Message = message;
        }

        public CommandResponse(int id)
        {
            Id = id;
            Success = true;
        }

        public CommandResponse(Guid id)
        {
            Id = id;
            Success = true;
        }
    }
}
