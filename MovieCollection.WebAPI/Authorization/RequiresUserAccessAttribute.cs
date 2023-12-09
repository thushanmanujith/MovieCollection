using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieCollection.Security.Constants;

namespace MovieCollection.WebAPI.Authorization
{
    public class RequiresUserAccessAttribute : TypeFilterAttribute
    {
        public RequiresUserAccessAttribute() : base(typeof(RequiresUserAccessAttributeImpl))
        {
        }

        private class RequiresUserAccessAttributeImpl : Attribute, IAsyncResourceFilter
        {
            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                if (context.HttpContext.User.IsInRole(Roles.User))
                    await next();
                else
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
