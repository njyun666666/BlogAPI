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


		public LoginService(IBlogDB_Org blogDB_Org, IBlogDB_Login blogDB_Login)
		{
			db_Org = blogDB_Org;
			db_Login = blogDB_Login;
		}

		public OrgAccountInfoModel AccountInfoGet(string googleID)
		{
			return db_Org.AccountInfoGet(googleID);
		}

		public OrgAccountInfoModel AccountInfoAdd(GoogleJsonWebSignature.Payload googleUser)
		{
			string uid = Guid.NewGuid().ToString().Replace("-", "").ToLower();
			string account = googleUser.Email.Split('@').First();
			Int16 status = 1;
			int result = db_Org.AccountInfoAdd(uid, googleUser.Subject, googleUser.Name, googleUser.Email, account, status);

			if (result > 0)
			{
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

		public int LoginLogAdd(string uid, string key, string ip)
		{
			return db_Login.LoginLogAdd(key, uid, ip, 1);
		}


	}
}
