using BlogAPI.Models.Article;
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
	public class ArticleEnabledFilter : IAsyncActionFilter
	{
		IAuthService _authService;
		public ArticleEnabledFilter(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			string uid = context.HttpContext.Request.Headers["UID"];
			ArticleEnabledModel model = (ArticleEnabledModel)context.ActionArguments["model"];

			if (!await _authService.CheckArticleEnabled(uid, model.Account, model.Article_id))
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
				return;
			}

			var resultContext = await next();
		}
	}
}
