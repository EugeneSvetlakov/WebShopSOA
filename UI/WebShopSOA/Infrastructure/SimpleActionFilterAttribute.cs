using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WebShopSOA.Infrastructure
{
    public class SimpleActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}