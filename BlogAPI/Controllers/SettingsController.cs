using BlogAPI.Enums;
using BlogAPI.Filters;
using BlogAPI.Models;
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
	[TypeFilter(typeof(AuthFilter), Arguments = new object[] { "Blogger" })]
	public class SettingsController : ControllerBase
	{
		private ISettingsService _settingService;

		public SettingsController(ISettingsService settingsService)
		{
			_settingService = settingsService;
		}

		[HttpPost]
		public IActionResult Get([FromHeader] string UID)
		{
			return Ok(_settingService.GetBlogSetting(UID));
		}

		[HttpPost]
		public IActionResult Edit([FromHeader] string UID, BlogSettingModel model)
		{
			if (string.IsNullOrWhiteSpace(model.Title))
			{
				return Ok(new ParamErrorReturn());
			}

			if (_settingService.Edit(UID, model, UID) == 0)
			{
				return Ok(new FailReturn());
			}

			return Ok();
		}


	}
}
