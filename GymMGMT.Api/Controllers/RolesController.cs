using GymMGMT.Application.CQRS.Auth.Commands.ChangeRoleStatus;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS.Auth.Commands.DeleteRole;
using GymMGMT.Application.CQRS.Auth.Commands.UpdateRole;
using GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetRolesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymMGMT.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ISender _mediator;

        public RolesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]")]
        public async Task<ActionResult<RolesInListViewModel>> GetAll()
        {
            var response = await _mediator.Send(new GetRolesListQuery());

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]/detail")]
        public async Task<ActionResult<RoleDetailViewModel>> Detail([FromBody] GetRoleDetailQuery query)
        {
            var getRoleDetail = new GetRoleDetailQuery()
            {
                Id = query.Id,
            };
            var response = await _mediator.Send(getRoleDetail);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("[controller]")]
        public async Task<ActionResult<RoleDetailViewModel>> Create([FromBody] CreateRoleCommand command)
        {
            var createRoleCommand = new CreateRoleCommand()
            {
                Name = command.Name,
            };
            var response = await _mediator.Send(createRoleCommand);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]")]
        public async Task<ActionResult<RoleDetailViewModel>> Update([FromBody] UpdateRoleCommand command)
        {
            var updateRoleCommand = new UpdateRoleCommand()
            {
                Id = command.Id,
                Name = command.Name,
            };
            var response = await _mediator.Send(updateRoleCommand);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]/changeStatus", Name = "ChangeRoleStatus")]
        public async Task<ActionResult<RoleDetailViewModel>> ChangeStatus([FromBody] ChangeRoleStatusCommand command)
        {
            var changeRoleStatusCommand = new ChangeRoleStatusCommand()
            {
                Id = command.Id,
            };
            var response = await _mediator.Send(changeRoleStatusCommand);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("[controller]")]
        public async Task<ActionResult<RoleDetailViewModel>> Delete([FromBody] DeleteRoleCommand command)
        {
            var deleteRoleCommand = new DeleteRoleCommand()
            {
                Id = command.Id,
            };
            var response = await _mediator.Send(deleteRoleCommand);

            return Ok(response);
        }
    }
}
