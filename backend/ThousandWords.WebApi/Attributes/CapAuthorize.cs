using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ThousandWords.WebApi.Attributes;

public class CapAuthorize : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userIdentity = context.HttpContext.User.Identity;
        if (userIdentity != null && userIdentity.IsAuthenticated)
            return;
        context.Result = new UnauthorizedResult();
        base.OnActionExecuting(context);
    }
}