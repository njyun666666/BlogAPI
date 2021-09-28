using BlogAPI.Common;
using BlogAPI.DB.BlogDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		IMyService _myservice;
		IDB_Test _dB_Test;
		public TestController(IDB_Test dB_Test, IMyService myService)
		{
			_myservice = myService;
			_dB_Test = dB_Test;
		}
		public async Task<IActionResult> Get()
		{
			return Ok(new
			{
				appsettingName = _myservice.AppsettingName(),
				blogDB = await _dB_Test.BlogDBTest()
			});
		}
	}
}
