using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryManagementSystem.Filters
{
    public class StudentUserAuth : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var result = context.HttpContext.Session.GetInt32(SessionKeys.StudentId);
            if(result is null)
            {
                context.Result = new RedirectResult("/Account/StudentLogin");
            }
            //throw new NotImplementedException();
        }
    }
}
