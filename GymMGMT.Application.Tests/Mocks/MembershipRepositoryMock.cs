using AutoFixture;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Tests.Mocks
{
    public class MembershipRepositoryMock
    {
        public static Mock<IMembershipRepository> GetMembershipRepository()
        {
            var memberships = GetMemberships();
            var mockMembershipRepository = new Mock<IMembershipRepository>();

            mockMembershipRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(memberships);
            mockMembershipRepository.Setup(x => x.GetAllWithDetailsAsync()).ReturnsAsync(memberships);
            mockMembershipRepository.Setup(x => x.GetAllByMembershipTypeIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(memberships);
            mockMembershipRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var membership = memberships.FirstOrDefault(x => x.Id == id);

                    return membership;
                });
            mockMembershipRepository.Setup(x => x.GetByIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var membership = memberships.FirstOrDefault(x => x.Id == id);

                    return membership;
                });
            mockMembershipRepository.Setup(x => x.GetByMemberIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(
                (int memberId) =>
                {
                    var membership = memberships.FirstOrDefault(x => x.MemberId == memberId);

                    return membership;
                });
            mockMembershipRepository.Setup(x => x.AddAsync(It.IsAny<Membership>())).ReturnsAsync(
                (Membership membership) =>
                {
                    memberships.Add(membership);

                    return membership;
                });
            mockMembershipRepository.Setup(x => x.UpdateAsync(It.IsAny<Membership>())).Callback<Membership>(
                (Membership membership) =>
                {
                    var existMembership = memberships.FirstOrDefault(x => x.Id == membership.Id);
                    existMembership.StartDate = membership.StartDate;
                    existMembership.LastExtension = membership.LastExtension;
                    existMembership.EndDate = membership.EndDate;
                    existMembership.Price = membership.Price;
                    existMembership.MemberId = membership.MemberId;
                    existMembership.MembershipTypeId = membership.MembershipTypeId;
                    existMembership.Status = membership.Status;
                });
            mockMembershipRepository.Setup(x => x.DeleteAsync(It.IsAny<Membership>())).Callback<Membership>(
                (Membership membership) =>
                {
                    memberships.Remove(membership);
                });

            return mockMembershipRepository;
        }

        private static List<Membership> GetMemberships()
        {
            Fixture fixture = new Fixture();
            var memberships = fixture.Build<Membership>().Without(x => x.Member).Without(x => x.MembershipType).CreateMany(10).ToList();

            return memberships;
        }
    }
}