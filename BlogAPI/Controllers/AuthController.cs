﻿using BlogAPI.Filters;
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
		public IActionResult Check([FromHeader] string UID, AuthCheckModel model)
		{
			if (_authService.Check(UID, new string[] { model.role }))
			{
				return Ok();
			}
			return StatusCode(403);
		}


	}
}