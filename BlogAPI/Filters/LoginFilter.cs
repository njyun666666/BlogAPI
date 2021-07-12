using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Filters
{
	public class LoginFilter : ActionFilterAttribute
    {

        public LoginFilter(IConfiguration config)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string uid = context.HttpContext.Request.Headers["UID"];

            if (string.IsNullOrWhiteSpace(uid))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

        }
    }
}
