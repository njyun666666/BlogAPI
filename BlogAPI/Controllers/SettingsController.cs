using BlogAPI.Filters;
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
		public SettingsController()
		{

		}


	}
}
