using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Models.Org;
using BlogAPI.Services.IServices;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class LoginService : ILoginService
	{
		private IBlogDB_Org db_Org;
		private IBlogDB_Login db_Login;
		private IBlogDB_Settings db_Settings;


		public LoginService(IBlogDB_Org blogDB_Org, IBlogDB_Login blogDB_Login, IBlogDB_Settings blogDB_Settings)
		{
			db_Org = blogDB_Org;
			db_Login = blogDB_Login;
			db_Settings = blogDB_Settings;
		}

		public async Task<OrgAccountInfoModel> AccountInfoGet(string googleID)
		{
			return await db_Org.AccountInfoGet(googleID);
		}

		public async Task<OrgAccountInfoModel> AccountInfoAdd(GoogleJsonWebSignature.Payload googleUser)
		{
			string uid = Guid.NewGuid().ToString().Replace("-", "").ToLower();
			string account = googleUser.Email.Split('@').First();
			Int16 status = 1;
			int result = await db_Org.AccountInfoAdd(uid, googleUser.Subject, googleUser.Name, googleUser.Email, account, status);

			if (result > 0)
			{
				// 新增預設角色
				await db_Org.SetDefaultRole(uid);

				await db_Settings.InsertNewBlogger(uid, account);

				return new OrgAccountInfoModel()
				{
					UID = uid,
					GID = googleUser.Subject,
					Name = googleUser.Name,
					Email = googleUser.Email,
					Account = account,
					Status = status
				};
			}

			return null;
		}

		public async Task<int> LoginLogAdd(string uid, string key, string ip)
		{
			return await db_Login.LoginLogAdd(key, uid, ip, 1);
		}


	}
}
