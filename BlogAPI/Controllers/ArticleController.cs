using BlogAPI.Filters;
using BlogAPI.Models;
using BlogAPI.Models.Article;
using BlogAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ArticleController : ControllerBase
	{
		IArticleService _articleService;
		IAuthService _authService;
		public ArticleController(IArticleService articleService, IAuthService authService)
		{
			_articleService = articleService;
			_authService = authService;
		}
		[HttpPost]
		[TypeFilter(typeof(AuthFilter), Arguments = new object[] { "Blogger" })]
		public async Task<IActionResult> AddArticle([FromHeader] string UID, ArticleListModel model)
		{
			// 修改文章 檢查權限
			if (model.ID.HasValue)
			{
				if(!await _authService.ArticleEditAuth(UID, model.ID.Value))
				{
					return StatusCode(403);
				}
			}


			Int64 id = await _articleService.AddArticle(UID, model);

			if (id > 0)
			{
				return Ok(new OkReturn(id));
			}
			else
			{
				return Ok(new FailReturn());
			}

		}
		[HttpPost]
		public async Task<IActionResult> GetIndexList([FromHeader] string UID, ArticleListRequestModel model)
		{
			return Ok(await _articleService.GetIndexList(UID, model));
		}
		[HttpPost]
		public async Task<IActionResult> GetArticle([FromHeader]string UID, ArticleRequestModel model)
		{
			return Ok(await _articleService.GetArticle(UID, model));
		}
	}
}
