using AutoFixture;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Tests.Mocks
{
    public class MemberRepositoryMock
    {
        public static Mock<IMemberRepository> GetMemberRepository()
        {
            var members = GetMembers();
            var mockMemberRepository = new Mock<IMemberRepository>();

            mockMemberRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(members);
            mockMemberRepository.Setup(x => x.GetAllWithDetailsAsync()).ReturnsAsync(members);
            mockMemberRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var member = members.FirstOrDefault(x => x.Id == id);

                    return member;
                });
            mockMemberRepository.Setup(x => x.GetByIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var member = members.FirstOrDefault(x => x.Id == id);

                    return member;
                });
            mockMemberRepository.Setup(x => x.AddAsync(It.IsAny<Member>())).ReturnsAsync(
                (Member member) =>
                {
                    members.Add(member);

                    return member;
                });
            mockMemberRepository.Setup(x => x.UpdateAsync(It.IsAny<Member>())).Callback<Member>(
                (Member member) =>
                {
                    var existMember = members.FirstOrDefault(x => x.Id == member.Id);
                    existMember.FirstName = member.FirstName;
                    existMember.LastName = member.LastName;
                    existMember.DateOfBirth = member.DateOfBirth;
                    existMember.PhoneNumber = member.PhoneNumber;
                    existMember.Status = member.Status;
                    existMember.UserId = member.UserId;
                    existMember.MembershipId = member.MembershipId;
                });
            mockMemberRepository.Setup(x => x.DeleteAsync(It.IsAny<Member>())).Callback<Member>(
                (Member member) =>
                {
                    members.Remove(member);
                });

            return mockMemberRepository;
        }
        private static List<Member> GetMembers()
        {
            Fixture fixture = new Fixture();
            var members = fixture.Build<Member>()
                .Without(x => x.User)
                .Without(x => x.Membership)
                .Without(x => x.Trainings)
                .CreateMany(10).ToList();

            return members;
        }
    }
}