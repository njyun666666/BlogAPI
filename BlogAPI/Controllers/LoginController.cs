using BlogAPI.Filters;
using BlogAPI.Models.Org;
using BlogAPI.Models.Settings;
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
		private ISettingsService _settingsService;

		public LoginController(ILoginService loginService, IGoogleLoginService googleLoginService, IAuthService authService, ISettingsService settingsService)
		{
			_loginService = loginService;
			_googleLoginService = googleLoginService;
			_authService = authService;
			_settingsService = settingsService;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromHeader]string IP)
		{
			var googleuser = _googleLoginService.GetUser();

			if (googleuser == null)
			{
				return StatusCode(401);
			}

			// 取得使用者資料
			OrgAccountInfoModel account = await _loginService.AccountInfoGet(googleuser.Subject);

			// 沒資料就新增
			if (account == null)
			{
				account = await _loginService.AccountInfoAdd(googleuser);
			}

			if (account.Status == 1)
			{
				string key = Guid.NewGuid().ToString().Replace("-", "").ToLower();
				string token = _authService.CreateToken(account.UID, key);

				// login log
				int add = await _loginService.LoginLogAdd(account.UID, key, IP);

				// IndexDefault
				BlogSettingModel settingModel = await _settingsService.GetBlogSetting(account.UID);

				if (add == 1)
				{
					return Ok(new
					{
						token = token,
						account = settingModel != null && settingModel.IndexDefault == 1 ? "i" : account.Account
					});
				}

				

			}


			return StatusCode(401);

		}

		[HttpPost]
		[LoginFilter]
		public IActionResult Check()
		{
			return Ok();
		}
	}
}
