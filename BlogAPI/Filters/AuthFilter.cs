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
    public class AuthFilter : IAsyncActionFilter
	{
        private IAuthService _authService;
        private string[] roles;

        public AuthFilter(IAuthService authService, string roles_string)
        {
            _authService = authService;
            roles = roles_string.Split(",");
        }

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			string uid = context.HttpContext.Request.Headers["UID"];

			if (string.IsNullOrWhiteSpace(uid))
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
				return;
			}

			if (!await _authService.Check(uid, roles))
			{
				context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
				return;
			}

			var resultContext = await next();
		}

	}
}
