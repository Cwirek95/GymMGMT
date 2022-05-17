namespace GymMGMT.Application.CQRS.Auth.Queries.GetRolesList
{
    public class RolesInListViewModel
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public IEnumerable<string> Users { get; set; }
    }
}