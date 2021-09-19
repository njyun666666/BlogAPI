using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface IAuthService
	{
		public string CreateToken(string uid, string tokenKey);
		public TokenModel TokenDecrypt(string token);
		public Task<bool> TokenKeyCheck(TokenModel model);
		public Task<bool> Check(string uid, string[] roles);
		public Task<bool> CheckBlogEnabled(string uid, string account);
		public Task<bool> CheckArticleEnabled(string uid, string account, Int64 articleID);
	}
}
