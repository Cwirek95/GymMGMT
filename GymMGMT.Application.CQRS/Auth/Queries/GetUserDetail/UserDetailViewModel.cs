namespace GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail
{
    public class UserDetailViewModel
    {
        public string Email { get; set; }
        public DateTimeOffset RegisteredAt { get; set; }
        public bool Status { get; set; }
    }
}