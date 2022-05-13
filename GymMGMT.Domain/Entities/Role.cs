using GymMGMT.Domain.Common;

namespace GymMGMT.Domain.Entities
{
    public class Role : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }


        public IEnumerable<User>? Users { get; set; }
    }
}
