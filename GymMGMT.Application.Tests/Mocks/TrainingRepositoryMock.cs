using AutoFixture;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Tests.Mocks
{
    public class TrainingRepositoryMock
    {
        public static Mock<ITrainingRepository> GetTrainingRepository()
        {
            var trainings = GetTrainings();
            var mockTrainingRepository = new Mock<ITrainingRepository>();

            mockTrainingRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(trainings);
            mockTrainingRepository.Setup(x => x.GetAllWithDetailsAsync()).ReturnsAsync(trainings);
            mockTrainingRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var training = trainings.FirstOrDefault(x => x.Id == id);

                    return training;
                });
            mockTrainingRepository.Setup(x => x.GetByIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var training = trainings.FirstOrDefault(x => x.Id == id);

                    return training;
                });
            mockTrainingRepository.Setup(x => x.AddAsync(It.IsAny<Training>())).ReturnsAsync(
                (Training training) =>
                {
                    trainings.Add(training);

                    return training;
                });
            mockTrainingRepository.Setup(x => x.UpdateAsync(It.IsAny<Training>())).Callback<Training>(
                (Training training) =>
                {
                    var existTraining = trainings.FirstOrDefault(x => x.Id == training.Id);
                    existTraining.StartDate = training.StartDate;
                    existTraining.EndDate = training.EndDate;
                    existTraining.Price = training.Price;
                    existTraining.Status = training.Status;
                    existTraining.TrainingType = training.TrainingType;
                    existTraining.TrainerId = training.TrainerId;
                    existTraining.Members = training.Members;
                });
            mockTrainingRepository.Setup(x => x.DeleteAsync(It.IsAny<Training>())).Callback<Training>(
                (Training training) =>
                {
                    trainings.Remove(training);
                });

            return mockTrainingRepository;
        }

            private static List<Training> GetTrainings()
        {
            Fixture fixture = new Fixture();
            var trainings = fixture.Build<Training>()
                .Without(x => x.Members)
                .Without(x => x.Trainer)
                .CreateMany(10).ToList();

            return trainings;
        }
    }
}