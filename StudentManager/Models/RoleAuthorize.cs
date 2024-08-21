using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentManager.Models
{
    public class RoleAuthorize : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var role = context.HttpContext.Session.GetString("SessionRole");
            if (string.IsNullOrEmpty(role))
            {
                context.Result = new ViewResult
                {
                    ViewName = "NotFound"
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
