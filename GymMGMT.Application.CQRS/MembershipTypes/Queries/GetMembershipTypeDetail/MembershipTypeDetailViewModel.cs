using GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail;

namespace GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypeDetail
{
    public class MembershipTypeDetailViewModel
    {
        public string Name { get; set; }
        public double DefaultPrice { get; set; }
        public int DurationInDays { get; set; }
        public bool Status { get; set; }


        public IEnumerable<MembershipDto> Memberships { get; set; }
    }
}