namespace GymMGMT.Application.Models.Responses

{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}