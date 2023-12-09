using MovieCollection.Core.Exceptions;
using MovieCollection.Core.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.WebAPI.Exceptions;

namespace MovieCollection.WebAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private const string DefaultErrorMessage = "Something went wrong. Please try again";
        private readonly IWebHostEnvironment _environment;

        public ErrorController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        ///     Triggered when there is an unhandled exception
        /// </summary>
        /// <returns></returns>
        [Route("/errors")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrors()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = StatusCodes.Status500InternalServerError;

            if (context == null)
                return StatusCode(statusCode, new ApiProblem(statusCode, DefaultErrorMessage, string.Empty));

            var exception = context.Error;
            var details = _environment.IsDevelopment() ? exception.StackTrace ?? string.Empty : string.Empty;

            if (exception is ErrorCodeException customError)
            {
                statusCode = (int)customError.ErrorCode.ToHttpStatusCode();
                var customProblem =
                    new ApiProblem(statusCode, customError.Message, details, customError.ErrorCode);

                return StatusCode(statusCode, customProblem);
            }

            var problem = new ApiProblem(statusCode, _environment.IsDevelopment() ? exception.Message : DefaultErrorMessage, details);
            return StatusCode(statusCode, problem);
        }
    }
}
