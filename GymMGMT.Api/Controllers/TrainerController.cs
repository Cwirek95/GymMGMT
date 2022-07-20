using GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining;
using GymMGMT.Application.CQRS.Trainings.Commands.AddTraining;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingType;
using GymMGMT.Application.CQRS.Trainings.Commands.DeleteMemberFromTraining;
using GymMGMT.Application.CQRS.Trainings.Commands.DeleteTraining;
using GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail;
using GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMGMT.Api.Controllers
{
    [Route("api/trainer")]
    [ApiController]
    [Authorize(Roles = "Admin,Trainer")]
    public class TrainerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrainerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("trainings")]
        public async Task<ActionResult<TrainingsInListViewModel>> GetTrainingsList()
        {
            var response = await _mediator.Send(new GetTrainingsListQuery());

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("trainings/{id}")]
        public async Task<ActionResult<TrainingDetailViewModel>> TrainingDetail(int id)
        {
            var query = new GetTrainingDetailQuery()
            {
                Id = id
            };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("trainings")]
        public async Task<ActionResult<Guid>> AddTraining([FromBody] AddTrainingCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("trainings/date")]
        public async Task<ActionResult> ChangeTrainingDate([FromBody] ChangeTrainingDateCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("trainings/price")]
        public async Task<ActionResult> ChangeTrainingPrice([FromBody] ChangeTrainingPriceCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("trainings/type")]
        public async Task<ActionResult> ChangeTrainingType([FromBody] ChangeTrainingTypeCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("trainings/member")]
        public async Task<ActionResult> AddMemberToTraining([FromBody] AddMemberToTrainingCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("trainings/member/delete")]
        public async Task<ActionResult> DeleteMemberFromTraining([FromBody] DeleteMemberFromTrainingCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("trainings/{id}")]
        public async Task<ActionResult> DeleteTraining(int id)
        {
            var command = new DeleteTrainingCommand()
            {
                Id = id
            };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
