using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class LoginService : ILoginService
	{
		public IBlogDB_Login db_login;

		public LoginService(IBlogDB_Login blogDB_Login)
		{
			db_login = blogDB_Login;
		}



	}
}
