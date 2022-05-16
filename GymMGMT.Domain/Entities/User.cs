using GymMGMT.Domain.Common;

namespace GymMGMT.Domain.Entities
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset RegisteredAt { get; set; }


        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
