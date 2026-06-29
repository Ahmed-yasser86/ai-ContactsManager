using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Areas.Contacts.Filters.AuthFilters
{
    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
       

        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Cookies.ContainsKey("Auth-key") == false)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                return;
            }


            if (context.HttpContext.Request.Cookies["Auth-key"]!="1000")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                return;
            }

        }
    }
}
