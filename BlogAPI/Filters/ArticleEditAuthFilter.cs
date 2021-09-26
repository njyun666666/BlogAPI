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
	public class ArticleEditAuthFilter : IAsyncActionFilter
	{
		IAuthService _authService;
		public ArticleEditAuthFilter(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			string uid = context.HttpContext.Request.Headers["UID"];
			ArticleEditAuthModel model = (ArticleEditAuthModel)context.ActionArguments["model"];

			if (!await _authService.ArticleEditAuth(uid, model.article_id))
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
				return;
			}

			var resultContext = await next();
		}
	}
}
