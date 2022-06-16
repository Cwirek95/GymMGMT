using GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeDefaultPrice;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeMembershipTypeStatus;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.DeleteMembershipType;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.UpdateMembershipType;
using GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypeDetail;
using GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypesList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMGMT.Api.Controllers.Admin
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class MembershipTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembershipTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]")]
        public async Task<ActionResult<MembershipTypesInListViewModel>> GetAll()
        {
            var response = await _mediator.Send(new GetMembershipTypesListQuery());

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]/{id}")]
        public async Task<ActionResult<MembershipTypeDetailViewModel>> Detail(int id)
        {
            var query = new GetMembershipTypeDetailQuery()
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
        [HttpPost("[controller]")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateMembershipTypeCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]")]
        public async Task<ActionResult> Update([FromBody] UpdateMembershipTypeCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]/status", Name = "ChangeMembershipTypeStatus")]
        public async Task<ActionResult> ChangeStatus([FromBody] ChangeMembershipTypeStatusCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]/price", Name = "ChangeDefaultPrice")]
        public async Task<ActionResult> ChangePrice([FromBody] ChangeDefaultPriceCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("[controller]/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteMembershipTypeCommand()
            {
                Id = id
            };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
