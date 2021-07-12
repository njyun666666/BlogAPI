using BlogAPI.Filters;
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
	public class MenuController : ControllerBase
	{
		private IMenuService _menuService;

		public MenuController(IMenuService menuService)
		{
			_menuService = menuService;
		}

		[HttpGet("{type:int:min(1)}")]
		public IActionResult MenuGet([FromHeader]string UID, [FromRoute] int type)
		{
			return Ok(_menuService.GetMenu(UID, type));
		}

	}
}
