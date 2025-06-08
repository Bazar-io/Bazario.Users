using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Authorization.Attributes;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.Users.Application.UseCases.Users.Commands.BanAdmin;
using Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Application.UseCases.Users.Queries.GetAdminById;
using Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins;
using Bazario.Users.Application.UseCases.Users.Queries.GetCurrentAdmin;
using Bazario.Users.WebAPI.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminsController(
        #region Handlers
        IQueryHandler<GetAllAdminsQuery, IEnumerable<UserResponse>> getAllAdminsQueryHandler,
        IQueryHandler<GetAdminByIdQuery, UserResponse> getAdminByIdQueryHandler,
        IQueryHandler<GetCurrentAdminQuery, UserResponse> getCurrentAdminQueryHandler,
        ICommandHandler<DeleteAdminCommand> deleteAdminCommandHandler,
        ICommandHandler<BanAdminCommand> banAdminCommandHandler,
        #endregion
        ProblemDetailsFactory problemDetailsFactory) : ControllerBase
    {
        #region Queries


        [HasRole(Role.Owner)]
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins(
            CancellationToken cancellationToken)
        {
            var queryResult = await getAllAdminsQueryHandler.Handle(
                new GetAllAdminsQuery(),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }

        [HasRole(Role.Owner)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAdminById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var queryResult = await getAdminByIdQueryHandler.Handle(
                new GetAdminByIdQuery(id),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }

        [HasRole(Role.Admin)]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentAdmin(
            CancellationToken cancellationToken)
        {
            var queryResult = await getCurrentAdminQueryHandler.Handle(
                new GetCurrentAdminQuery(),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }


        #endregion

        #region Commands


        [HasRole(Role.Owner)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAdmin(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var commandResult = await deleteAdminCommandHandler.Handle(
                new DeleteAdminCommand(id),
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }

        [HasRole(Role.Owner)]
        [HttpPost("ban")]
        public async Task<IActionResult> BanAdmin(
            [FromBody] BanAdminCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await banAdminCommandHandler.Handle(
                command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }


        #endregion
    }
}
