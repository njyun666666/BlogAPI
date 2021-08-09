﻿using BlogAPI.DB.BlogDB;
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
		IDB_Test _dB_Test;
		public TestController(IDB_Test dB_Test)
		{
			_dB_Test = dB_Test;
		}
		public IActionResult Get()
		{
			return Ok(new { BlogDB = _dB_Test.BlogDBTest() });
		}
	}
}