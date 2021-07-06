using BlogAPI.Common;
using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Models;
using BlogAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class AuthService : IAuthService
	{
		private readonly IMyService _myService;
		private IGoogleLoginService _googleLoginService;
		private IBlogDB_Login db_Login;


		public AuthService(IMyService myService, IGoogleLoginService googleLoginService, IBlogDB_Login blogDB_Login)
		{
			_myService = myService;
			_googleLoginService = googleLoginService;
			db_Login = blogDB_Login;
		}

		public string CreateToken(string uid, string key)
		{
			TokenModel payload = new TokenModel()
			{
				UID = uid,
				GID = _googleLoginService.GetUser().Subject,
				TokenKey = key
			};

			string iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
			string json = JsonSerializer.Serialize(payload);
			string payload_str = CommonTool.AESEncrypt(json, _myService.BlogAPI_Key(), iv);
			return $"{iv}.{payload_str}";
		}

		public TokenModel TokenDecrypt(string token)
		{
			try
			{
				string[] token_arr = token.Split('.');
				string iv = token_arr[0];
				string payload_str = token_arr[1];

				string payload_decrypt = CommonTool.AESDecrypt(payload_str, _myService.BlogAPI_Key(), iv);

				TokenModel payload = JsonSerializer.Deserialize<TokenModel>(payload_decrypt);
				

				if (payload.GID != _googleLoginService.GetUser().Subject)
				{
					return null;
				}

				return payload;
			}
			catch (Exception ex)
			{
				
			}

			return null;
		}

		public bool TokenKeyCheck(TokenModel model)
		{
			return db_Login.TokenKeyCheck(model.TokenKey, model.UID);
		}

	}
}
