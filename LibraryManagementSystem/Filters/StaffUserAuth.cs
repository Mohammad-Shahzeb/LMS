using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagementSystem.Filters
{
    public class StaffUserAuth : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Log("OnActionExecuted", FilterContext.RouteData);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var result = filterContext.HttpContext.Session.GetInt32(SessionKeys.StaffId);
            
            if (result is  null)
            {
                filterContext.Result = new RedirectResult("/Account/StaffLogin");
            }

        }


        //   Log("OnActionExecuting", filterContext.RouteData);
    }

    public class ResultAuth : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            // throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            // throw new NotImplementedException();
        }
    }



    public class XAuth : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            /// throw new NotImplementedException();
        }
    }


    public class Exceptionhandler : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
