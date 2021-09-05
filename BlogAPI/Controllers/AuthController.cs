using BlogAPI.Filters;
using BlogAPI.Models.Auth;
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
	public class AuthController : ControllerBase
	{
		private IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		[TypeFilter(typeof(LoginFilter))]
		public async Task<IActionResult> Check([FromHeader] string UID, AuthCheckModel model)
		{
			if (await _authService.Check(UID, new string[] { model.role }))
			{
				return Ok();
			}
			return StatusCode(403);
		}
		[HttpPost]
		[TypeFilter(typeof(BlogEnabledFilter))]
		public async Task<IActionResult> CheckBlogEnabled(BlogEnabledModel model)
		{
			return Ok();
		}

	}
}
