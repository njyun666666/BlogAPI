﻿using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface IAuthService
	{
		public string CreateToken(string uid, string key);
		public TokenModel TokenDecrypt(string token);
		public bool TokenKeyCheck(TokenModel model);
	}
}
