using AutoFixture;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Tests.Mocks
{
    public class TrainerRepositoryMock
    {
        public static Mock<ITrainerRepository> GetTrainerRepository()
        {
            var trainers = GetTrainers();
            var mockTrainerRepository = new Mock<ITrainerRepository>();

            mockTrainerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(trainers);
            mockTrainerRepository.Setup(x => x.GetAllWithDetailsAsync()).ReturnsAsync(trainers);
            mockTrainerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var trainer = trainers.FirstOrDefault(x => x.Id == id);

                    return trainer;
                });
            mockTrainerRepository.Setup(x => x.GetByIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var trainer = trainers.FirstOrDefault(x => x.Id == id);

                    return trainer;
                });
            mockTrainerRepository.Setup(x => x.AddAsync(It.IsAny<Trainer>())).ReturnsAsync(
                (Trainer trainer) =>
                {
                    trainers.Add(trainer);

                    return trainer;
                });
            mockTrainerRepository.Setup(x => x.UpdateAsync(It.IsAny<Trainer>())).Callback<Trainer>(
                (Trainer trainer) =>
                {
                    var existTrainer = trainers.FirstOrDefault(x => x.Id == trainer.Id);
                    existTrainer.FirstName = trainer.FirstName;
                    existTrainer.LastName = trainer.LastName;
                    existTrainer.Status = trainer.Status;
                    existTrainer.UserId = trainer.UserId;
                });
            mockTrainerRepository.Setup(x => x.DeleteAsync(It.IsAny<Trainer>())).Callback<Trainer>(
                (Trainer trainer) =>
                {
                    trainers.Remove(trainer);
                });

            return mockTrainerRepository;
        }

            private static List<Trainer> GetTrainers()
        {
            Fixture fixture = new Fixture();
            var trainers = fixture.Build<Trainer>().Without(x => x.User).Without(x => x.Trainings).CreateMany(10).ToList();

            return trainers;
        }
    }
}
