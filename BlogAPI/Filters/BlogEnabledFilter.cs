using BlogAPI.Models.Auth;
using BlogAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Filters
{
	public class BlogEnabledFilter : IAsyncActionFilter
	{
		IAuthService _authService;
		public BlogEnabledFilter(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			string uid = context.HttpContext.Request.Headers["UID"];
			BlogEnabledModel model = (BlogEnabledModel)context.ActionArguments["model"];

			if (!string.IsNullOrWhiteSpace(model.Account) && !await _authService.CheckBlogEnabled(uid, model.Account))
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
				return;
			}

			var resultContext = await next();
		}
	}
}
