using BlogAPI.Services.IServices;
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
    public class AuthFilter : ActionFilterAttribute
    {
        private IAuthService _authService;
        private string[] roles;

        public AuthFilter(IConfiguration config, IAuthService authService, string roles_string)
        {
            _authService = authService;
            roles = roles_string.Split(",");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string uid = context.HttpContext.Request.Headers["UID"];

            if (string.IsNullOrWhiteSpace(uid) || !_authService.Check(uid, roles))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

        }
    }
}
