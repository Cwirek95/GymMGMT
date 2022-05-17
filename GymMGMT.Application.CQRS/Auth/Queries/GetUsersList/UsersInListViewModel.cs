namespace GymMGMT.Application.CQRS.Auth.Queries.GetUsersList
{
    public class UsersInListViewModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTimeOffset RegisteredAt { get; set; }
        public bool Status { get; set; }
    }
}