using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Api.Factories;
using Bazario.Users.Application.UseCases.Users.Commands.BanUser;
using Bazario.Users.Application.UseCases.Users.Commands.UpdateUser;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Application.UseCases.Users.Queries.GetCurrentUser;
using Bazario.Users.Application.UseCases.Users.Queries.GetPublicInfoById;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public sealed class UsersController(
        ProblemDetailsFactory problemDetailsFactory) : ControllerBase
    {
        #region Queries


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(
            [FromServices] IQueryHandler<GetUserPublicInfoByIdQuery, UserPublicInfoResponse> queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var queryResult = await queryHandler.Handle(
                new GetUserPublicInfoByIdQuery(id),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }

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


        [HttpPut("current")]
        public async Task<IActionResult> UpdateCurrentUser(
            [FromServices] ICommandHandler<UpdateUserCommand> commandHandler,
            [FromBody] UpdateUserCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await commandHandler.Handle(
                command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }

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
