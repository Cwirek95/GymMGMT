namespace GymMGMT.Application.Models.Responses

{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse()
        {
        }

        public AuthenticationResponse(Guid id, string email, string role, string token)
        {
            Id = id;
            Email = email;
            Role = role;
            Token = token;
        }
    }
}