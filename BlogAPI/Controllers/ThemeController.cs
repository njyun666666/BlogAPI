using BlogAPI.Models.Theme;
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
	public class ThemeController : ControllerBase
	{
		IThemeService _themeService;
		public ThemeController(IThemeService themeService)
		{
			_themeService = themeService;
		}
		[HttpPost]
		public async Task<IActionResult> GetThemeData([FromHeader]string uid, ThemeDataRequestModel model)
		{
			if (model.Self == 1 && string.IsNullOrWhiteSpace(uid))
			{
				return StatusCode(401);
			}

			return Ok(await _themeService.GetThemeData(uid, model));
		}
	}
}
