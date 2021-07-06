using BlogAPI.Models.Org;
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
	public class LoginController : ControllerBase
	{
		private ILoginService _loginService;
		private IGoogleLoginService _googleLoginService;
		private IAuthService _authService;

		public LoginController(ILoginService loginService, IGoogleLoginService googleLoginService, IAuthService authService)
		{
			_loginService = loginService;
			_googleLoginService = googleLoginService;
			_authService = authService;
		}

		[HttpPost]
		public IActionResult Login([FromHeader]string IP)
		{
			var googleuser = _googleLoginService.GetUser();

			if (googleuser == null)
			{
				return Forbid();
			}

			// 取得使用者資料
			OrgAccountInfoModel account = _loginService.AccountInfoGet(googleuser.Subject);

			// 沒資料就新增
			if (account == null)
			{
				account = _loginService.AccountInfoAdd(googleuser);
			}

			if (account.Status == 1)
			{
				string key = Guid.NewGuid().ToString().Replace("-", "").ToLower();
				string token = _authService.CreateToken(account.UID, key);

				int add = _loginService.LoginLogAdd(account.UID, key, IP);

				if (add == 1)
				{
					return Ok(new { token });
				}

				

			}


			return Forbid();

		}

	}
}
