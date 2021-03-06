using BlogAPI.Common;
using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Models;
using BlogAPI.Models.Settings;
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
		private ISettingsService _settingsService;
		private IBlogDB_Login db_Login;
		private IBlogDB_Auth db_Auth;


		public AuthService(IMyService myService, IGoogleLoginService googleLoginService, IBlogDB_Login blogDB_Login, IBlogDB_Auth blogDB_Auth, ISettingsService settingsService)
		{
			_myService = myService;
			_googleLoginService = googleLoginService;
			_settingsService = settingsService;
			db_Login = blogDB_Login;
			db_Auth = blogDB_Auth;
		}

		public string CreateToken(string uid, string tokenKey)
		{
			string iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

			TokenModel payload = new TokenModel()
			{
				UID = uid,
				TokenKey = tokenKey
			};

			string json = JsonSerializer.Serialize(payload);
			string payload_str = CommonTool.AESEncrypt(json, _myService.BlogAPI_Key(), iv);
			string signature = CommonTool.ComputeHMACSHA256(payload_str, _myService.BlogAPI_Key());

			return $"{iv}.{payload_str}.{signature}";
		}

		public TokenModel TokenDecrypt(string token)
		{
			try
			{
				string[] token_arr = token.Split('.');
				string iv = token_arr[0];
				string payload_str = token_arr[1];
				string signature = token_arr[2];

				string payload_decrypt = CommonTool.AESDecrypt(payload_str, _myService.BlogAPI_Key(), iv);
				TokenModel payload = JsonSerializer.Deserialize<TokenModel>(payload_decrypt);

				string _signature = CommonTool.ComputeHMACSHA256(payload_str, _myService.BlogAPI_Key());

				if (signature != _signature)
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

		public async Task<bool> TokenKeyCheck(TokenModel model)
		{
			return await db_Login.TokenKeyCheck(model.TokenKey, model.UID);
		}

		public async Task<bool> Check(string uid, string[] roles)
		{
			return await db_Auth.Check(uid, roles);
		}
		public async Task<bool> CheckBlogEnabled(string uid, string account)
		{
			return await db_Auth.CheckBlogEnabled(uid, account);
		}
		public async Task<bool> CheckArticleEnabled(string uid, string account, Int64 articleID)
		{
			BlogSettingAccModel model = await _settingsService.GetSetting(uid, account);
			return await db_Auth.CheckArticleEnabled(model.Setting.UID, articleID, model.Self);
		}
		public async Task<bool> ArticleEditAuth(string uid, Int64 articleID)
		{
			return await db_Auth.ArticleEditAuth(uid, articleID);
		}
	}
}
