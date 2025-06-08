using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Api.Factories;
using Bazario.AspNetCore.Shared.Authorization.Attributes;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.Users.Application.UseCases.Users.Commands.BanUser;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Application.UseCases.Users.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public sealed class UsersController(
        ProblemDetailsFactory problemDetailsFactory) : ControllerBase
    {
        #region Queries


        [HasRole(Role.User)]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser(
            [FromServices] IQueryHandler<GetCurrentUserQuery, UserResponse> queryHandler,
            CancellationToken cancellationToken)
        {
            var queryResult = await queryHandler.Handle(
                new GetCurrentUserQuery(),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }


        #endregion

        #region Commands


        [HasRole(Role.Admin)]
        [HttpPost("ban")]
        public async Task<IActionResult> BanUser(
            [FromServices] ICommandHandler<BanUserCommand> commandHandler,
            [FromBody] BanUserCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await commandHandler.Handle(
                command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }


        #endregion
    }
}
