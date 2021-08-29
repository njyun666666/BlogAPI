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
		public ArticleController(IArticleService articleService)
		{
			_articleService = articleService;
		}
		[HttpPost]
		public async Task<IActionResult> AddArticle([FromHeader] string UID, ArticleListModel model)
		{
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
		public async Task<IActionResult> GetList([FromHeader] string UID, ArticleListRequestModel model)
		{
			return Ok(await _articleService.GetList(UID, model));
		}

	}
}
