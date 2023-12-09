using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieCollection.Security.Constants;

namespace MovieCollection.WebAPI.Authorization
{
    public class RequiresAdminAccessAttribute : TypeFilterAttribute
    {
        public RequiresAdminAccessAttribute() : base(typeof(RequiresAdminAccessAttributeImpl))
        {
        }

        private class RequiresAdminAccessAttributeImpl : Attribute, IAsyncResourceFilter
        {
            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                if (context.HttpContext.User.IsInRole(Roles.Admin))
                    await next();
                else
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
