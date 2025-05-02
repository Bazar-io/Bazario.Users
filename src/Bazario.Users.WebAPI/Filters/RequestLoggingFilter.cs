using Microsoft.AspNetCore.Mvc.Filters;

namespace Bazario.Users.WebAPI.Filters
{
    public class RequestLoggingFilter : IAsyncActionFilter
    {
        private readonly ILogger<RequestLoggingFilter> _logger;
        private readonly IWebHostEnvironment _env;

        public RequestLoggingFilter(
            ILogger<RequestLoggingFilter> logger, 
            IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var endpointName = context.ActionDescriptor.DisplayName;

            _logger.LogTrace("{Endpoint} endpoint hit.", endpointName);

            if (_env.IsDevelopment())
            {
                CheckModelBinding(context);
            }

            await next();

            _logger.LogTrace("Returning response for {Endpoint} endpoint.", endpointName);
        }

        private void CheckModelBinding(ActionExecutingContext context)
        {
            foreach (var kvp in context.ActionArguments)
            {
                if (kvp.Value is null)
                {
                    _logger.LogDebug("Model binding: Argument '{Arg}' is null.", kvp.Key);
                }
                else
                {
                    _logger.LogDebug("Model binding: Argument '{Arg}' = {@Value}", kvp.Key, kvp.Value);
                }
            }

            if (!context.ModelState.IsValid)
            {
                _logger.LogDebug("Model state is invalid. Errors:");

                foreach (var modelState in context.ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        _logger.LogDebug(" - {Key}: {Error}", modelState.Key, error.ErrorMessage);
                    }
                }
            }
        }
    }
}
