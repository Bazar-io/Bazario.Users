using Bazario.Users.Application.UseCases.Users.Queries.GetAdminById;
using Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins;
using Bazario.Users.WebAPI.Factories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            ISender sender,
            ProblemDetailsFactory problemDetailsFactory,
            ILogger<UsersController> logger)
        {
            _sender = sender;
            _problemDetailsFactory = problemDetailsFactory;
            _logger = logger;
        }

        #region Queries


        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins(
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("GetAllAdmins endpoint hit.");

            var queryResult = await _sender.Send(
                new GetAllAdminsQuery(),
                cancellationToken);

            _logger.LogTrace("Returning response for GetAllAdmins Endpoint.");

            return queryResult.IsSuccess ? Ok(queryResult.Value) : _problemDetailsFactory.GetProblemDetails(queryResult);
        }

        [HttpGet("admins/{id:guid}")]
        public async Task<IActionResult> GetAdminById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("GetAdminById endpoint hit.");

            var queryResult = await _sender.Send(
                new GetAdminByIdQuery(id),
                cancellationToken);

            _logger.LogTrace("Returning response for GetAdminById Endpoint.");

            return queryResult.IsSuccess ? Ok(queryResult.Value) : _problemDetailsFactory.GetProblemDetails(queryResult);
        }


        #endregion
    }
}
