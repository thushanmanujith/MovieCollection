using Microsoft.AspNetCore.Mvc;
using MovieCollection.UserAdministration.Domain.Constant;

namespace MovieCollection.WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        public BaseController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        ///     Gets the client ip address.
        /// </summary>
        /// <returns>The client ip address.</returns>
        protected string GetClientIP() => _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

        protected int GetUserId()
        {
            if (_accessor.HttpContext.User == null)
                throw new ArgumentNullException(nameof(_accessor.HttpContext.User));

            int.TryParse(_accessor.HttpContext.User.FindFirst(UserClaims.UserId)?.Value, out var userId);
            return userId;
        }
    }
}
