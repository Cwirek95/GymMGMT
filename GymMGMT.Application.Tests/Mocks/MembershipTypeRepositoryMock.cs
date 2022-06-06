using AutoFixture;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Tests.Mocks
{
    public class MembershipTypeRepositoryMock
    {
        public static Mock<IMembershipTypeRepository> GetMembershipTypeRepository()
        {
            var membershipTypes = GetMembershipTypes();
            var mockMembershipTypeRepository = new Mock<IMembershipTypeRepository>();

            mockMembershipTypeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(membershipTypes);
            mockMembershipTypeRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var membershipType = membershipTypes.FirstOrDefault(x => x.Id == id);

                    return membershipType;
                });
            mockMembershipTypeRepository.Setup(x => x.AddAsync(It.IsAny<MembershipType>())).ReturnsAsync(
                (MembershipType membershipType) =>
                {
                    membershipTypes.Add(membershipType);

                    return membershipType;
                });
            mockMembershipTypeRepository.Setup(x => x.UpdateAsync(It.IsAny<MembershipType>())).Callback<MembershipType>(
                (MembershipType membershipType) =>
                {
                    var existMembershipType = membershipTypes.FirstOrDefault(x => x.Id == membershipType.Id);
                    existMembershipType.Name = membershipType.Name;
                    existMembershipType.DefaultPrice = membershipType.DefaultPrice;
                    existMembershipType.DurationInDays = membershipType.DurationInDays;
                    existMembershipType.Status = membershipType.Status;
                });
            mockMembershipTypeRepository.Setup(x => x.DeleteAsync(It.IsAny<MembershipType>())).Callback<MembershipType>(
                (MembershipType membershipType) =>
                {
                    membershipTypes.Remove(membershipType);
                });

            return mockMembershipTypeRepository;
        }

        private static List<MembershipType> GetMembershipTypes()
        {
            Fixture fixture = new Fixture();
            var membershipTypes = fixture.Build<MembershipType>().Without(x => x.Memberships).CreateMany(10).ToList();

            return membershipTypes;
        }
    }
}